using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace Posts.WebAPI.App_Helpers
{
    public class WebApiIoCContainer : WebApiDependencyScope, IDependencyResolver
    {
        public WebApiIoCContainer(IUnityContainer container) : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new WebApiDependencyScope(child);
        }
    }
}