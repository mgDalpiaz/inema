using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Extension.Abstracts
{
    /// <summary>
    /// Extende System.Enum
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Retorna a Descricao informado na notacoes do Enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this System.Enum value)
        {
            #region [ Code ]
            Type type = value.GetType();

            string name = System.Enum.GetName(type, value);

            MemberInfo member = type
                .GetMembers()
                .FirstOrDefault(w => w.Name == name);

            DescriptionAttribute attribute = member != null
                ? member
                    .GetCustomAttributes(true)
                    .FirstOrDefault(w => w.GetType() == typeof(DescriptionAttribute)) as DescriptionAttribute
                : null;

            return attribute != null ? attribute.Description : name;
            #endregion
        }

        
        /// <summary>
        /// Retorna o enum valorado como uma string considerando o valor textual.
        /// </summary>
        public static string TryToString(this System.Enum value) => Convert.ToString(value);

 
    }
}
