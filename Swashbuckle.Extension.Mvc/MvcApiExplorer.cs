﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Collections.ObjectModel;

namespace Swashbuckle.Extension.Mvc
{
    /// <summary>
    /// ApiExplorer for MVC
    /// </summary>
    public class MvcApiExplorer : ApiExplorer
    {
        /// <summary>
        /// HttpConfiguration
        /// </summary>
        private HttpConfiguration _configuration;
        
        /// <summary>
        /// Mapping of Http Methods
        /// </summary>
        private Dictionary<Type, HttpMethod> _httpMethodsMap => 
            new Dictionary<Type, HttpMethod> {
                { typeof(System.Web.Mvc.HttpGetAttribute), HttpMethod.Get },
                { typeof(System.Web.Mvc.HttpDeleteAttribute), HttpMethod.Delete },
                { typeof(System.Web.Mvc.HttpHeadAttribute), HttpMethod.Head },
                { typeof(System.Web.Mvc.HttpOptionsAttribute), HttpMethod.Options },
                { typeof(System.Web.Mvc.HttpPutAttribute), HttpMethod.Put },
                { typeof(System.Web.Mvc.HttpPostAttribute), HttpMethod.Post },
        };

        public MvcApiExplorer(Assembly assembly, HttpConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            assembly.GetTypes()
               .Where(type => typeof(IController).IsAssignableFrom(type) && type.Name != "ErrorController" && type.BaseType != typeof(ApiController))
               .ToList().ForEach(controller =>
               {
                   base.ApiDescriptions.AddRange(BuildControllerApiDescription(controller));
               });
        }

        /// <summary>
        /// ApiExolorer for Action is visible
        /// </summary>
        /// <param name="actionVariableValue"></param>
        /// <param name="actionDescriptor"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        public override bool ShouldExploreAction(string actionVariableValue, HttpActionDescriptor actionDescriptor, IHttpRoute route) => true;

        /// <summary>
        /// ApiExolorer for Controller is visible
        /// </summary>
        /// <param name="controllerVariableValue"></param>
        /// <param name="controllerDescriptor"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        public override bool ShouldExploreController(string controllerVariableValue, HttpControllerDescriptor controllerDescriptor, IHttpRoute route) => true;

        private List<ApiDescription> BuildControllerApiDescription(Type type)
        {
            var controllerName = type.Name.Replace("Controller", "");
            var methods = type.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(m => typeof(ActionResult).IsAssignableFrom(m.ReturnType));

            var list = new List<ApiDescription>();
            foreach (var method in methods)
            {
                var apiDescription = new ApiDescription();
                apiDescription.ActionDescriptor = new MvcHttpActionDescriptor(method);
                apiDescription.ActionDescriptor.ControllerDescriptor = new HttpControllerDescriptor(_configuration, controllerName, type);

                //判断请求类型(暂时不支持Patch，Trace)
                var actionMethod = method.GetCustomAttribute<ActionMethodSelectorAttribute>();
                if (actionMethod == null)
                {
                    apiDescription.HttpMethod = HttpMethod.Get;
                }
                else
                {
                    if (_httpMethodsMap.ContainsKey(actionMethod.GetType()))
                        apiDescription.HttpMethod = _httpMethodsMap[actionMethod.GetType()];
                    else
                        continue;
                }

                apiDescription.Route = new HttpRoute(string.Format("{0}/{1}", controllerName, method.Name));
                apiDescription.RelativePath = string.Format("{0}/{1}", controllerName, method.Name);
                apiDescription.Documentation = string.Empty;
                typeof(ApiDescription).GetProperty("ParameterDescriptions").SetValue(apiDescription, BuildApiParameters(method));
                typeof(ApiDescription).GetProperty("ResponseDescription").SetValue(apiDescription, new ResponseDescription()
                {
                    ResponseType = method.ReturnType,
                    DeclaredType = method.DeclaringType,
                    Documentation = string.Empty
                });
                list.Add(apiDescription);
            }

            return list;
        }

        private Collection<ApiParameterDescription> BuildApiParameters(MethodInfo methodInfo)
        {
            return new Collection<ApiParameterDescription>(
            methodInfo.GetParameters().Select(p => new ApiParameterDescription()
            {
                Name = p.Name,
                Documentation = string.Empty,
                Source = ApiParameterSource.Unknown,
                ParameterDescriptor = new MvcHttpActionParameterDescriptor(p, new MvcHttpActionDescriptor(methodInfo)),
            }).ToList());
        }
    }
}
