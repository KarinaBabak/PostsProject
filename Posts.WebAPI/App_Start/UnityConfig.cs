using Microsoft.Practices.Unity;
using Posts.WebAPI.App_Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Unity.WebApi;
using System.Data.Entity;
using Posts.Entities;
using Posts.Application;

namespace Posts.WebAPI.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<DbContext, PostsDbContext>();

            container
                .RegisterAppDataModule(); // Register common application components

            GlobalConfiguration.Configuration.DependencyResolver = new WebApiIoCContainer(container);
        }

    }
}