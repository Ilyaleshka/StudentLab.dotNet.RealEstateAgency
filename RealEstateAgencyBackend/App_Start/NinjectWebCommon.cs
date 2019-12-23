using AutoMapper;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using RealEstateAgencyBackend;
using RealEstateAgencyBackend.BLL.Interfaces;
using RealEstateAgencyBackend.BLL.Services;
using RealEstateAgencyBackend.DAL.Contexts;
using RealEstateAgencyBackend.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace RealEstateAgencyBackend
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }
        private static void RegisterServices(IKernel kernel)
        {

            kernel.Bind<AppDbContext>().To<AppDbContext>().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IRentalRequestService>().To<RentalRequestService>().InRequestScope();
            kernel.Bind<IRentalAnnouncementService>().To<RentalAnnouncementService>().InRequestScope();

            
            var mapperConfiguration = MapperConfig.CreateConfiguration();
            kernel.Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();
            kernel.Bind<IMapper>().ToMethod(ctx =>
                 new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }
    }
}
