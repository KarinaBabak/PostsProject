using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace Posts.WebAPI.App_Helpers
{
    public class WebApiDependencyScope : IDependencyScope
    {
        protected readonly IUnityContainer _container;

        public WebApiDependencyScope(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }
            this._container = container;
        }

        public object GetService(Type serviceType)
        {
            if (!_container.IsRegistered(serviceType))
            {
                if (serviceType.IsAbstract || serviceType.IsInterface)
                {
                    return null;
                }
            }

            return _container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.IsRegistered(serviceType) ? _container.ResolveAll(serviceType) : new List<object>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}