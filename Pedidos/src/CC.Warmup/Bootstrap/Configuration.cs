using CC.Common;
using Extension.Primitives;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace CC.Warmup
{
    public static class Configuration
    {
        #region [ Attr ]

        private static AppEnvironment currentEnvironment = AppEnvironment.Development;

        private static IConfigurationBuilder configurationBuilder;

        #endregion

        #region [ Properties ]

        public static string SolutionName { get; set; }

        public static string Version { get; set; }

        #endregion

        #region [ Calculate Properties ]

        /// <summary>
        /// Retorna o Tipo de Ambiente que a aplicação está rodando
        /// </summary>
        public static AppEnvironment CurrentEnvironment => currentEnvironment;

        /// <summary>
        /// Retorna as configurações carregadas do Ambiente corrente
        /// </summary>
        public static IConfiguration AppConfiguration { get; set; }

        #endregion

        #region [ BootStrap Methods ]

        /// <summary>
        /// Faz as configurações padrões no Warmup do App
        /// </summary>
        public static IConfigurationBuilder BootstrapConfig(this IWebHostEnvironment environment)
        {
            // Seta o ambiente da Aplicação
            Environment(environment);
            // Seta as informacoes sobre a Solution
            LoadProductSettings();
            // Carrega o arquivo de Settings conforme Environment
            LoadAppSettings(environment);

            return configurationBuilder;
        }

        #endregion

        #region [ Methods ]

        internal static void LoadProductSettings()
        {
            var solution = Assembly.GetCallingAssembly();

            Configuration.SolutionName = solution.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
            Configuration.Version = solution.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

        }

        #region [ Environment]

        /// <summary>
        /// Determina o Ambiente a ser utilizado na aplicação - Usado para definir os apontamentos
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        internal static void Environment(IWebHostEnvironment environment)
        {
            #region [ Code ]

            if (environment.IsProduction())
                currentEnvironment = AppEnvironment.Production;
            else if (environment.IsStaging())
                currentEnvironment = AppEnvironment.Staging;
            else if (environment.EnvironmentName.ToLower().Equals("testing"))
                currentEnvironment = AppEnvironment.Testing;
            else
                currentEnvironment = AppEnvironment.Development;

            environment.EnvironmentName = currentEnvironment.ToString();

            #endregion

        }

        internal static void LoadAppSettings(IWebHostEnvironment environment)
        {
            configurationBuilder = new ConfigurationBuilder()
               .SetBasePath(environment.ContentRootPath)
               .AddJsonFile("appsettings.json", true, true) // Configurações comuns a todos os ambientes
               .AddJsonFile($"appsettings.{environment.EnvironmentName.UpperCamelCase().ToString()}.json", true) // Configurações do Ambiente
               .AddJsonFile($"appinfo.json", true); // Informações de indetificação do App que esta sendo Carregado - Dados para Documentação da Mesma            
        }

        #endregion

        #endregion

    }
}
