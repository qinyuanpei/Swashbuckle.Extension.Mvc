# Swashbuckle.Extension.Mvc
Extension of Swashbuckle for ASP.NET MVC4

# Quick Start
* Pull Source Code & Build Project
```
git clone git@github.com:qinyuanpei/Swashbuckle.Extension.Mvc.git
```
* Add Reference To Your Project
```CSharp
using Swashbuckle.Extension.Mvc;
```
* Replace `IApiExplore` in `Global.asax.cs`
```CSharp
var assembly = typeof(DefaultMvcProject.MvcApplication).Assembly;
var apiExplorer = new MvcApiExplorer(assembly, GlobalConfiguration.Configuration);
GlobalConfiguration.Configuration.Services.Replace(typeof(IApiExplorer), apiExplorer);
```
* Add `SwaggerConfig.cs` in `App_Start`. See more details at [Swashbuckle.WebApi](https://github.com/domaindrivendev/Swashbuckle.WebApi)
```CSharp
using System.Web.Http;
using WebActivatorEx;
using DefaultMvcProject;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace DefaultMvcProject
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "DefaultMvcProject");
                        c.IncludeXmlComments($"{System.AppDomain.CurrentDomain.BaseDirectory}/bin/DefaultMvcProject.XML");
                    })
                .EnableSwaggerUi(c =>
                    {
                        
                    });
        }
    }
}
```
* Enjoy Swagger in ASP.NET MVC :)

![支持ASP.NET MVC的Swagger](https://github.com/qinyuanpei/Swashbuckle.Extension.Mvc/blob/master/DefaultMvcProject.png?raw=true)




