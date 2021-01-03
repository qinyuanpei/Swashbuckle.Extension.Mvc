# Swashbuckle.Extension.Mvc
Extension of Swashbuckle for ASP.NET MVC4

# Quick Start
* Pull Source Code & Build Project:
```
git clone git@github.com:qinyuanpei/Swashbuckle.Extension.Mvc.git
```
* Add Reference To Your Project:
```CSharp
using Swashbuckle.Extension.Mvc;
```
* Replace `IApiExplore` in `Global.asax.cs`:
```CSharp
var assembly = typeof(DefaultMvcProject.MvcApplication).Assembly;
var apiExplorer = new MvcApiExplorer(assembly, GlobalConfiguration.Configuration);
GlobalConfiguration.Configuration.Services.Replace(typeof(IApiExplorer), apiExplorer);
```
* Add `SwaggerConfig.cs` in `App_Start`. See more details at [Swashbuckle.WebApi](https://github.com/domaindrivendev/Swashbuckle.WebApi):

* Enjoy Swagger in ASP.NET MVC :)

![支持ASP.NET MVC的Swagger](https://github.com/qinyuanpei/Swashbuckle.Extension.Mvc/blob/master/DefaultMvcProject.png?raw=true)




