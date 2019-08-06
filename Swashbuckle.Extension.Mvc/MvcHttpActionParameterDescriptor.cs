using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Swashbuckle.Extension.Mvc
{
    /// <summary>
    /// Action Parameter Descripto for MVC
    /// </summary>
    public class MvcHttpActionParameterDescriptor : HttpParameterDescriptor
    {
        private ParameterInfo _parameterInfo;
        public MvcHttpActionParameterDescriptor(ParameterInfo parameterInfo, HttpActionDescriptor actionDescriptor) : base(actionDescriptor)
        {
            _parameterInfo = parameterInfo;
        }

        /// <summary>
        /// ParameterName
        /// </summary>
        public override string ParameterName => _parameterInfo.Name;

        /// <summary>
        /// ParameterType
        /// </summary>
        public override Type ParameterType => _parameterInfo.ParameterType;
    }
}
