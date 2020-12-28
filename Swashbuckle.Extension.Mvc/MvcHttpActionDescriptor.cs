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
    public class MvcHttpActionDescriptor: ReflectedHttpActionDescriptor
    {
        public MvcHttpActionDescriptor(MethodInfo methodInfo)
        {
            this.MethodInfo = methodInfo;
        }

        /// <summary>
        /// ActionName
        /// </summary>
        public override string ActionName => MethodInfo.Name;

        /// <summary>
        /// ReturnType
        /// </summary>
        public override Type ReturnType => MethodInfo.ReturnType;

        /// <summary>
        /// Return Descriptors of Parameters
        /// </summary>
        /// <returns></returns>
        public override Collection<HttpParameterDescriptor> GetParameters()
        {
            var self = this;
            return new Collection<HttpParameterDescriptor>(
                MethodInfo.GetParameters().Select(p => new MvcHttpActionParameterDescriptor(p, self) as HttpParameterDescriptor).ToList()
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
            var result = MethodInfo.Invoke(controllerContext.Controller, arguments.Select(s => s.Value).ToArray());
            tsc.SetResult(result);
            return tsc.Task;
        }
    }
}
