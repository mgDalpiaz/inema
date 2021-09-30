using Extension.Collections;
using Extension.Net;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Core.Shared.Base
{
    /// <summary>
    /// Entidade Básica utilizada pelas Entidades do Core.Shared
    /// </summary>
    public abstract class BaseEntity
    {
        #region [ Basic Properties ]

        /// <summary>
        /// Codigo de Integracao com o Sistema de Origem
        /// </summary>
        public string IntegrationCode { get; set; }

        /// <summary>
        /// Identificador unico da Entidade de Dominio
        /// </summary>
        public Guid? Id { get; set; }

        #endregion

        #region [ State Properties ]

        /// <summary>
        /// Data de Insercao do Objeto
        /// </summary>
        public DateTime? InsertedAt { get; set; }

        /// <summary>
        /// Data da Ultima Atualizacao de dados do Objeto
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Data de Delecao logica
        /// </summary>
        public DateTime? RemovedAt { get; set; }

        /// <summary>
        /// Usuário que fez o cadastro
        /// </summary>
        public string InsertedUser { get; set; }

        /// <summary>
        /// Guid do Usuário que fez o cadastro
        /// </summary>
        public Guid? InsertedUserId { get; set; }

        /// <summary>
        /// Usuário que fez a última alteração
        /// </summary>
        public string UpdatedUser { get; set; }

        /// <summary>
        /// Guid do Usuário que fez a última alteração
        /// </summary>
        public Guid? UpdatedUserId { get; set; }

        /// <summary>
        /// Usado para confirmar se o processo inteiro executado 
        /// por um lote ou encadeamento de MicroServices foi executado
        /// </summary>
        public bool Commited { get; set; } = true;

        #endregion

        #region [ Control Properties ]

        /// <summary>
        /// Determina se a entidade e nova ou nao. Se true e pq esta sendo adicionada 
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public bool IsAdd { get; set; } = false;

        [JsonIgnore]
        [NotMapped]
        public bool IsUpdate { get; set; } = false;


        #endregion

        #region [ Calculate Properties ]

        /// <summary>
        /// Retorna o Assembly com todo NameSpace
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public string AssemblyFullName => GetType().FullName;

        /// <summary>
        /// Identifica se o objeto está ativo ou Inativo
        /// </summary>
        [NotMapped]
        public bool IsActive => this.RemovedAt == null && this.Commited;

        [NotMapped]
        public bool IsRemoved => this.RemovedAt == null;

        #endregion

        #region [ Ctor ]

        /// <summary>
        /// Inicia a Entidade com o um Id Unico
        /// </summary>
        public BaseEntity()
        {
        }

        #endregion

        #region [ Check Methods ]

        /// <summary>
        /// Compara se um Objeto é igual ao outro, facilitando a identificação de comando de Update
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var compareTo = obj as BaseEntity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public void PopulateDiffBySource<T>(T source) where T : class
        {
            try
            {
                var properties = source.GetType().GetProperties().Where(x => x.CanWrite && x.Name != "IsUpdate" && x.Name != "Id");
                properties.ForEach(x =>
                {
                    var src = (x.GetValue(source) ?? null);
                    var dst = (x.GetValue(this) ?? null);

                    if (ObjectExtension.IsCollectionType(x))
                        return;

                    if (x.PropertyType.IsClass && x.PropertyType.GetMethod("PopulateDiffBySource") != null)
                    {
                        if (src == null)
                            return;

                        MethodInfo toInvoke = x.PropertyType.GetMethod("PopulateDiffBySource");
                        toInvoke = toInvoke.MakeGenericMethod(x.PropertyType);

                        if (dst == null)
                            x.SetValue(this, Activator.CreateInstance(x.PropertyType));

                        toInvoke.Invoke(dst, new object[] { src });
                    }
                    else if ((src != null && dst == null) || (!Object.Equals(src, dst) && src != null))
                    {
                        if (src == null && dst == null)
                            return;

                        x.SetValue(this, x.GetValue(source));
                        this.IsUpdate = true;
                    }
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region [ Operator Methods ]

        /// <summary>
        /// Realiza a comparação atraves da operação == entre duas entidades
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(BaseEntity a, BaseEntity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// Valida se duas entidades são diferentes usando o operador !=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(BaseEntity a, BaseEntity b)
        {
            return !(a == b);
        }


        #endregion

        #region [Actions by Entity]

        /// <summary>
        /// Realiza a ativação de um objeto deletado Logicamente
        /// </summary>
        public void ToActivate(string username, Guid userId)
        {
            this.ToUpdated(username, userId);
            this.RemovedAt = null;
        }

        /// <summary>
        /// Realiza a Inativação de um objeto de forma logica
        /// </summary>
        public void ToInactivate(string username, Guid userId)
        {
             this.ToUpdated(username, userId);
             this.RemovedAt = DateTime.Now;
        }

        /// <summary>
        /// Determina que o Objeto está sendo atualizado
        /// </summary>
        public void ToUpdated(string username, Guid userId)
        {
            this.UpdatedAt = DateTime.Now;
            this.UpdatedUser = username ?? string.Empty;
            this.UpdatedUserId = userId;
            this.IsUpdate = true;
            this.IsAdd = false;
        }

        /// <summary>
        /// Determina que o Objeto está sendo criado
        /// </summary>
        public void ToCreate(string username, Guid userId)
        {
            this.RemovedAt = null;
            this.UpdatedAt = null;
            this.UpdatedUser = null;
            this.UpdatedUserId = null;
            this.InsertedUser = username ?? string.Empty;
            this.InsertedUserId = userId;
            this.Id = this.Id != Guid.Empty && this.Id != null ? this.Id : Guid.NewGuid();
            this.InsertedAt = DateTime.Now;
            this.IsAdd = true;
        }

        #endregion

    }
}
