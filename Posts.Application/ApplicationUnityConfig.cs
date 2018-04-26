using System;
using Microsoft.Practices.Unity;
using Posts.Persistence.Interface;
using Posts.Persistence.Implementation;
using Posts.Application.Interface;
using Posts.Application.Implementation;

namespace Posts.Application
{
    public static partial class ApplicationUnityConfig
    {
        /// <summary>
        /// Registers the common components.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns>Type IUnityContainer that contains all registered settings.</returns>
        public static IUnityContainer RegisterAppDataModule(this IUnityContainer container)
        {
            container
                // Data Access Objects
                .RegisterType<IPostRepository, PostRepository>(new HierarchicalLifetimeManager())
                .RegisterType<ICommentRepository, CommentRepository>(new HierarchicalLifetimeManager())

                // Application Services
                .RegisterType<IPostService, PostService>(new HierarchicalLifetimeManager())
                .RegisterType<ICommentService, CommentService>(new HierarchicalLifetimeManager())
                ;

            return container;
        }
    }
}
