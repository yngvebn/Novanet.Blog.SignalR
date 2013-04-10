using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Ninject.Web.Mvc;
using Web.Infrastructure.Repository;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Web.App_Start.NinjectWebCommon), "Stop")]

namespace Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel _kernel;
        public static IKernel Kernel
        {
            get { return _kernel; }
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            _kernel = kernel;
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IPeopleRepository>().To<PeopleRepository>();
        }        
    }

    public class SignalRNinjectDependencyResolver : Microsoft.AspNet.SignalR.DefaultDependencyResolver
    {

        private readonly IKernel _kernel;

        public SignalRNinjectDependencyResolver(IKernel kernel)
        {

            if (kernel == null)
            {

                throw new ArgumentNullException("kernel");

            }

            _kernel = kernel;

        }

        public override object GetService(Type serviceType)
        {

            return _kernel.TryGet(serviceType) ?? base.GetService(serviceType);

        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {

            return _kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));

        }

    }
}
