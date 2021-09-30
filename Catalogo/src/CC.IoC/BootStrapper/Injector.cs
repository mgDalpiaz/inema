using App.Shared;
using CC.Extension.Net;
using Core.Shared;
using Core.Shared.Messages;
using Infra.External.API;
using Infra.Repository.JsonFile;
using Infra.Repository.JsonFile.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CC.IoC.BootStrapper
{
    public class Injector
    {
        private static IServiceCollection services;
        private static ServiceProvider serviceProvider;

        #region [ Properties ]



        #endregion

        #region [ Ctor ]

        /// <summary>
        /// Cria o serviço do container para injetar as dependencias
        /// </summary>
        /// <param name="services"></param>
        public Injector(IServiceCollection services)
        {
            SetServices(services);
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Controla o Serviço de Injeção
        /// </summary>
        private static void SetServices(IServiceCollection value)
        {
            services = value;
        }

        /// <summary>
        /// Controla o Serviço de Injeção
        /// </summary>
        public static IServiceCollection GetServices()
        {
            return services;
        }

        public static ServiceProvider GetServiceProvider()
        {
            return serviceProvider;
        }

        public static void BuildServiceProvider()
        {
            serviceProvider = services.BuildServiceProvider();
        }

        #endregion

        #region [ Factory ]

        /// <summary>
        /// Injeta as classes padrões para HTTPContext
        /// </summary>
        public static Injector RegisterDefaultWebAPIServices(IServiceCollection services)
        {
            // HTTTP Settings

            HttpClientExtensions.EnableSupportTLS();

            var injector = new Injector(services);

            // ASP.NET HttpContext dependency
            GetServices().AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                        
            //External API
            GetServices().AddScoped<IJsonFileRepository, JsonFileRepository>();
            GetServices().AddHttpClient<IExternalAPI, ExternalAPI>();

            // Domain Bus (Mediator)
            services.AddScoped<IDomainEventBus, DomainEvent>();

            // Notifications
            GetServices().AddScoped<INotificationHandler<Notification>, NotificationHandler>();
            GetServices().AddScoped<INotificationHandler<CrossMessage>, CrossMessageHandler>();

            return injector;
        }

        #endregion

    }
}
