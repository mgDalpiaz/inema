using App.Services.Services;
using App.Shared;
using CC.IoC.BootStrapper;
using Core.BC.Domain;
using Core.Shared;
using Infra.Queue.ServiceBus;
using Infra.Queue.Shared.Interfaces;
using Infra.Repository.Shared.Interfaces;
using Infra.Repository.SqlServer;
using Infra.Repository.SqlServer.Core;
using Infra.Repository.SqlServer.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace App.Services.Configuration
{
    public class DependencyResolver : IDependencyResolver
    {
        static DependencyResolver()
        {
            // UnitOfWork DataBase
            Injector.GetServices().AddScoped<AbstractContext, Context>();
            Injector.GetServices().AddTransient<IUnitOfWork, Infra.Repository.SqlServer.UnitOfWork>();
            Injector.GetServices().AddTransient(typeof(IRepository<>), typeof(Repository<>));

            // Queue
            Injector.GetServices().AddScoped<IQueueProducer, ServiceBusProducerQueueService>();
            Injector.GetServices().AddScoped<IQueueConsumer, ServiceBusConsumerQueueService>();

            // Domain Services
            Injector.GetServices().AddScoped<IAppService<BusinessUnit>, BusinessUnitServices>();
            Injector.GetServices().AddScoped<IBusinessUnitServices, BusinessUnitServices>();

            Injector.GetServices().AddScoped<IAppService<BusinessUnit>, BusinessUnitIntegrationServices>();
            Injector.GetServices().AddScoped<IBusinessUnitIntegrationServices, BusinessUnitIntegrationServices>();

            Injector.GetServices().AddScoped<IAppService<Region>, RegionServices>();
            Injector.GetServices().AddScoped<IRegionServices, RegionServices>();
            
            Injector.GetServices().AddScoped<IAppService<Photo>, PhotoServices>();

            Injector.GetServices().AddScoped<IAppService<BusinessUnitAddress>, BusinessUnitAddressServices>();

            Injector.GetServices().AddScoped<IAppService<BusinessUnitContact>, BusinessUnitContactServices>();
            Injector.GetServices().AddScoped<IBusinessUnitContactServices, BusinessUnitContactServices>();

            Injector.GetServices().AddScoped<IAppService<Photo>, PhotoServices>();
            Injector.GetServices().AddScoped<IPhotoServices, PhotoServices>();

            // End Build Services
            Injector.BuildServiceProvider();

        }
    }
}
