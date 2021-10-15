using App.Services.Services;
using App.Shared;
using CC.IoC.BootStrapper;
using Core.BC.Domain;
using Core.BC.Domain.Interfaces;
using Core.Shared;
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

            Injector.GetServices().AddScoped<IProdutoServices, ProdutoServices>();
            Injector.GetServices().AddScoped<ICatalogoServices, CatalogoServices>();
            Injector.GetServices().AddScoped<IListaPrecoServices, ListaPrecoServices>();

            // End Build Services
            Injector.BuildServiceProvider();

        }
    }
}
