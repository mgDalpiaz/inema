using CC.Common;
using Extension.Primitives;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace Infra.Repository.Shared.Entities
{
    public abstract class AbstractRepository
    {

        #region [ Properties ]

        /// <summary>
        /// Determina o Ambiente que esta rodando a Aplicação para carregamento correto das configurações
        /// </summary>
        public AppEnvironment Environment { get; set; }

        #endregion

        #region [ Ctor ]

        public AbstractRepository()
        {

        }

        public AbstractRepository(IHostingEnvironment environment)
        {
            this.Environment = environment.EnvironmentName.ToEnum<AppEnvironment>();
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Cria o padrão de configurações de serialização json.
        /// </summary>
        /// <returns></returns>
        protected virtual JsonSerializerSettings CreateSerializerSettings()
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };

            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            return settings;
        }

        #endregion

    }
}
