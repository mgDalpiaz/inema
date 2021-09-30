using System;
using System.ComponentModel;
using System.Web;

namespace Extension.Net
{
    public static class UriExtension
    {       

        /// <summary>
        /// Adiciona a Uri uma nova rota no final /route
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        public static System.Uri AddRoute(this System.Uri uri, string route)
        {
            #region [ Code ]

            var ub = new UriBuilder(uri);

            route = route[0] == '/' ? route : $"/{route}";

            ub.Path += uri.AbsolutePath.EndsWith("/") ? route.Substring(1) : route;


            return ub.Uri;
            #endregion
        }

        /// <summary>
        /// Procura palavras na rota e substitui as mesmas
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="find"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static System.Uri ReplaceRoute(this System.Uri uri, string find, string replace)
        {
            #region [ Code ]
            var ub = new UriBuilder(uri);

            ub.Path = ub.Path.Replace(find.Replace("{", "%7B").Replace("}", "%7D"), replace);

            return ub.Uri;
            #endregion
        }


        public static System.Uri ReflectionParameter<TParameter>(this System.Uri uri, TParameter parameter )
        {
            #region [ Code ]
            String[] spearator = { "%3F" };

            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(parameter))
            {
                var value = prop.GetValue(parameter);

                if (value != null)
                    uri = uri.ReplaceRoute("{" + prop.Name.ToLower() + "}", value.ToString());
            }

            if(uri.AbsoluteUri.Contains("%3F"))
            {
                var query = uri.AbsoluteUri.Split(spearator, StringSplitOptions.None);
                var newUri = new UriBuilder(query[0]);
                newUri.Query = query[1];

                return newUri.Uri;
            }

            return new UriBuilder(uri).Uri;

            #endregion
        }

    }
}
