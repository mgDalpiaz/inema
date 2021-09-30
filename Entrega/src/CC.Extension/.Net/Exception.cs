using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace Extension.Net
{
    /// <summary>
    /// Extende System.Exception
    /// </summary>
    public static class ExceptionExtension
    {

        /// <summary>
        /// Olha Exception por Exception Gerada 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="iterator"></param>
        public static void VisitEachException(this System.Exception ex, Action<System.Exception> iterator)
        {
            #region [ Code ]
            var current = ex;

            if (ex is ReflectionTypeLoadException) // Se for exceção de reflection, logar o trace do LoaderExceptions
            {
                foreach (var e in ((ReflectionTypeLoadException)ex).LoaderExceptions)
                    iterator(e);
            }

            while (current != null)
            {
                iterator(current);
                current = current.InnerException;
            }
            #endregion
        }

        /// <summary>
        /// Gera a mensagem de erro da Exception com Trace do Problema com ou sem o StackTrace e sem uma mensagem adicional
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="forceTraceStack"></param>
        /// <returns></returns>
        public static string ToTraceMessage(this System.Exception ex, bool forceTraceStack) => ex.ToTraceMessage(forceTraceStack, string.Empty);

        /// <summary>
        /// Gera uma mensagem de Exception sem forcar o StackTrace
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string ToTraceMessage(this System.Exception ex) => ex.ToTraceMessage(false, String.Empty);

        /// <summary>
        /// Gera a mensagem de erro da Exception com Trace do Problema sem o StackTrace e com mensagem e valores para replace {0}, {1}, etc...
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="shortMessage"></param>
        /// <param name="formatArgs"></param>
        /// <returns></returns>
        public static string ToTraceMessage(this System.Exception ex, string shortMessage, params object[] formatArgs) => ex.ToTraceMessage(false, shortMessage, formatArgs);

        /// <summary>
        /// Gera a mensagem de erro da Exception com Trace do Problema
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="forceTraceStackTraceEnabled"></param>
        /// <param name="shortMessage"></param>
        /// <param name="formatArgs"></param>
        /// <returns></returns>
        public static string ToTraceMessage(this System.Exception ex, bool forceTraceStackTraceEnabled, string shortMessage, params object[] formatArgs)
        {
            #region [ Code ]
            if (ex == null)
                return shortMessage;

            var msg = new StringBuilder();

            // Adiciona a mensage formatada
            if (!string.IsNullOrEmpty(shortMessage))
                msg.AppendLine(TryFormat(shortMessage, formatArgs));

            // Append das exceptions
            ex.VisitEachException(e =>
            {
                // Adiciona a mensagem da exceção
                e.AppendExceptionMessage(msg, forceTraceStackTraceEnabled);
            });

            return msg.ToString();
            #endregion
        }

        /// <summary>
        /// Valida se existe uma Exception do Tipo T e se a expressao passada e verdadeira, como por exemplo buscar uma exception com uma mensagem de erro especifica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        /// <param name="validation"></param>
        /// <returns></returns>
        public static bool HasException<T>(this System.Exception ex, Func<T, bool> validation) where T : System.Exception
        {
            #region [ Code ]
            var result = false;

            ex.VisitEachException((e) =>
            {
                if (e is T)
                    result = result || validation((T)e);
            });

            return result;
            #endregion
        }

        /// <summary>
        /// Valida se existe uma Exception do Tipo T passado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static bool HasException<T>(this System.Exception ex) where T : System.Exception
        {
            #region [ Code ]
            var result = false;

            ex.VisitEachException((e) =>
            {
                if (e is T)
                    result = true;
            });

            return result;
            #endregion
        }
        #region [ Internal Methods ]
        /// <summary>
        /// Realiza a formatacao de uma mensagem ciom base nos parametros passados
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        internal static string TryFormat(string format, params object[] args)
        {
            #region [ Code ]
            try
            {
                return string.Format(format, args);
            }
            catch (System.Exception)
            {
                var argv = args ?? new object[0];

                return string.Concat(format, " - ",
                    argv.Aggregate(string.Empty, (a, x) => a += Convert.ToString(x) + " - ", x => x.TrimEnd(' ', '-')));
            }
            #endregion
        }

        /// <summary>
        /// Realiza o Append de uma Exceptrion e seu StackTrace a Lista de Exceptions Geradas na Aplicacao
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="builder"></param>
        /// <param name="appendStackTrace"></param>
        internal static void AppendExceptionMessage(this System.Exception ex, StringBuilder builder, bool appendStackTrace)
        {
            #region [ Code ]
            builder.AppendLine(ex.GetType().FullName + ": " + ex.Message);
            // Caso for uma webexception, adiciona o conteúdo retornado
            if (ex is WebException && ((WebException)ex).Response != null)
            {
                try
                {
                    var body = new System.IO.StreamReader(((WebException)ex).Response.GetResponseStream()).ReadToEnd();
                    builder.AppendLine("ResponseBody: " + body);
                }
                catch (System.Exception)
                {
                }
            }
            // Adiciona o stack trace à mensagem
            if (appendStackTrace && ex.StackTrace != null)
                builder.AppendLine(ex.StackTrace);
            #endregion
        }


        #endregion

    }
}
