using Infra.Repository.JsonFile.Interfaces;
using Infra.Repository.Shared.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace Infra.Repository.JsonFile
{
    public class JsonFileRepository : AbstractRepository, IJsonFileRepository
    {

        #region [ Properties ]

        protected IHostingEnvironment HostingEnvironment { get; }

        #endregion

        #region [ Ctor ]

        /// <summary>
        /// Construtor que recebe do Startup as informações da Aplicação para poder carregar sob demanda os arquivos de configuração
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        /// <param name="configuration"></param>
        public JsonFileRepository(IHostingEnvironment hostingEnvironment, IConfiguration configuration )
            : base(hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
        }

        #endregion

        #region [ Methods ]

        public void Save<T>(string file, T obj)
        {
            string path = this.HostingEnvironment.ContentRootPath;

            var filepath = $"{path}/{GetFlieWithEnviroment(file)}";

            File.WriteAllText(filepath, JsonConvert.SerializeObject(obj, new JsonSerializerSettings {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            }));

        }

        /// <summary>
        /// Carrega um arquivo Json de Configuração da Aplicação
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <returns></returns>
        public T Read<T>(string file)
        {
            string path = this.HostingEnvironment.ContentRootPath;

            var filepath = $"{path}/{file}";

            if (!File.Exists(filepath))
            {
                path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                filepath = $"{path}/{file}";
            }

           var json = File.ReadAllText(filepath);
           return JsonConvert.DeserializeObject<T>(json, CreateSerializerSettings());
        }

        /// <summary>
        /// Carrega um arquivo de configuração com base no Envirement da Aplicação
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <returns></returns>
        public T Read2Environment<T>(string file) => this.Read<T>(GetFlieWithEnviroment(file));

        #endregion

        #region [ internal Methods ]

        private string GetFlieWithEnviroment(string filename) => filename.Replace(".json", $".{this.Environment}.json");

        #endregion

    }
}
