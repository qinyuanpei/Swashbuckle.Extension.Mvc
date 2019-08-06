using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Threading;

namespace Swashbuckle.Extension.Mvc
{
    /// <summary>
    /// Action Descriptor for MVC
    /// </summary>
    public class MvcHttpActionDescriptor: HttpActionDescriptor
    {
        private MethodInfo _methodInfo;
        public MvcHttpActionDescriptor(MethodInfo methodInfo)
        {
            _methodInfo = methodInfo;
        }

        /// <summary>
        /// ActionName
        /// </summary>
        public override string ActionName => _methodInfo.Name;

        /// <summary>
        /// ReturnType
        /// </summary>
        public override Type ReturnType => _methodInfo.ReturnType;

        /// <summary>
        /// Return Descriptors of Parameters
        /// </summary>
        /// <returns></returns>
        public override Collection<HttpParameterDescriptor> GetParameters()
        {
            var self = this;
            return new Collection<HttpParameterDescriptor>(
                _methodInfo.GetParameters().Select(p => new MvcHttpActionParameterDescriptor(p, self) as HttpParameterDescriptor).ToList()
            );
        }

        /// <summary>
        /// Execute Method via Invoke
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="arguments"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<object> ExecuteAsync(HttpControllerContext controllerContext, IDictionary<string, object> arguments, CancellationToken cancellationToken)
        {
            var tsc = new TaskCompletionSource<object>();
            var result = _methodInfo.Invoke(controllerContext.Controller, arguments.Select(s => s.Value).ToArray());
            tsc.SetResult(result);
            return tsc.Task;
        }
    }
}
