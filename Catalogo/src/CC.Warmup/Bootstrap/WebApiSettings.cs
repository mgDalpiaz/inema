using CC.Common;
using CC.Common.SchemaFilters;
using CC.Extension.Json;
using CC.IoC.BootStrapper;
using CC.Warmup.Filters;
using Extension.Collections;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CC.Warmup
{
    public static class WebApiSettings
    {
        /// <summary>
        /// Inicia as configurações de Serviços da API 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection BootstrapServices(this IServiceCollection services, Type type)
        {
            #region [ Code ]
            AddDomain(services, type);

            services
               .ConfigureMemoryCache(type)
               .ConfigureApiDoc() // Carrega as Config de Docs
               .ConfigureJwtAuthentication()
               .AddCors()
               .AddMediatR(type)
               .AddControllers(options =>
               {
                   options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                   options.Filters.Add(new AuthorizeFilter("SafeIpList"));
               }
                )
               .AddFluentValidation(opt =>
               {
                   opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
               })
               .AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.ConfigureJsonSerializerSettings();
               })
               .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // Injeção de Dependências 
            Injector.RegisterDefaultWebAPIServices(services);

            AppSettingsInject(type);

            return services;

            #endregion
        }

        /// <summary>
        /// Configura os padrões da API, Cors, MVC
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder BootstrapConfigure(this IApplicationBuilder app, Type type = null)
        {
            #region [ Code ]

            if (Configuration.CurrentEnvironment == AppEnvironment.Development)
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app
                .UseRouting()
                .UseCors(c =>
                {
                    c.AllowAnyHeader();
                    c.AllowAnyMethod();
                    c.AllowAnyOrigin();
                })
             .UseAuthentication()
             .UseAuthorization()
             //.UseMiddleware(typeof(AuthorizeHandlingMiddleware))
             //.UseMiddleware(typeof(ErrorHandlingMiddleware))
             .UseEndpoints(endpoints =>
             {
                 endpoints.MapControllers();
             })
             .ConfigureApiDoc();

            return app;

            #endregion
        }


        /// <summary>
        /// Configura os padrões da API, Cors, MVC
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static ILoggerFactory LoggerConfigure(this ILoggerFactory loggerFactory, Type type = null)
        {
            #region [ Code ]

            loggerFactory.AddFile("Logs/{Date}.txt");

            return loggerFactory;

            #endregion
        }


        /// <summary>
        /// Configura o padrão de Formatação do Json da API - Usar em JsonMvcOptions
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static JsonSerializerSettings ConfigureJsonSerializerSettings(this JsonSerializerSettings settings)
        {
            #region [ Code ]

            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            settings.Converters.Add(new IntegerJsonConverter());
            settings.Converters.Add(new DecimalJsonConverter());
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Culture = new CultureInfo("en-US");
            settings.Formatting = Formatting.Indented;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            return settings;

            #endregion
        }

        /// <summary>
        /// Configura a Doc da Api - padrão usa OpenAPI (Swagger)
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection ConfigureApiDoc(this IServiceCollection services)
        {
            #region [ Code ]

            var infoApp = new OpenApiInfo();

            Configuration.AppConfiguration.GetSection("AppInfo").Bind(infoApp);

            services.AddSwaggerGen(s =>
            {
                s.SchemaFilter<EnumStringSchemaFilter>();
                s.SwaggerDoc("docs", infoApp);
                s.EnableAnnotations(true);
                s.CustomSchemaIds(x => x.FullName);
                s.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName));
                s.TagActionsBy(api => api.GroupName);
            });

            return services;

            #endregion
        }

        /// <summary>
        /// Habilita o Uso da documentação na API
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IApplicationBuilder ConfigureApiDoc(this IApplicationBuilder app)
        {
            #region [ Code ]

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
                });

            });
            app.UseReDoc(s =>
            {
                s.SpecUrl = "swagger/docs/swagger.json";
                s.RoutePrefix = string.Empty;
                s.EnableUntrustedSpec();
                s.ScrollYOffset(10);
                s.HideDownloadButton();
                s.ExpandResponses("200,201");
                s.RequiredPropsFirst();
                s.NativeScrollbars();
                s.SortPropsAlphabetically();
            });

            return app;

            #endregion
        }

        #region [ Private Methods ]

        private static void AppSettingsInject(Type type)
        {
            string applicationAssemblyName = $"{Configuration.SolutionName}.App.Services";

            var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

            var loader = assembly.GetTypes().Where(x => x.FullName.Contains(".Configuration.") && !x.Name.Contains("<>"));

            loader.ForEach(result => {
                Activator.CreateInstance(result);
            });

        }

        /// <summary>
        /// Carrega o Mediator para a WebAPI
        /// </summary>
        /// <param name="services"></param>
        private static void AddDomain(IServiceCollection services, Type type)
        {
            #region [ Code ]

            string applicationAssemblyName = $"{Configuration.SolutionName}.Core.BC.Domain";

            var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

            AssemblyScanner
                .FindValidatorsInAssembly(assembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            #endregion

        }

        /// <summary>
        /// Retorna o nome do Pojeto
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string ResolveProjectBaseName(Type type)
        {
            #region [ Code ]

            var assemblyName = type.Namespace.Split('.');
            return assemblyName.LastOrDefault();

            #endregion
        }

        private static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection services)
        {
            // AUthentication Ip Safe
            services.AddSingleton<IAuthorizationHandler, IpCheckHandler>();

            // Configura a Geracao e Controle do Token
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("5838DB85BB629EF3633891852ACF190DF7D98342")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false
                };
            });


            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("SafeIpList",
                    policy => policy.Requirements.Add(new IpCheckRequirement { IpClaimRequired = true }));

                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            return services;
        }

        private static IServiceCollection ConfigureMemoryCache(this IServiceCollection services, Type type)
        {
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration =
                    Configuration.AppConfiguration.GetConnectionString("CacheConnection");
                options.InstanceName = ResolveProjectBaseName(type);
            });

            return services;
        }

        #endregion


    }
}
