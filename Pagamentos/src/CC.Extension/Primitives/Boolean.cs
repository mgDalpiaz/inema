using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Extension.Primitives
{
    public static class BooleanExtension
    {

        /// <summary>
        /// Convert um boleano para sua representacao em numero inteiro 0 e 1
        /// </summary>
        /// <param name="bool"></param>
        /// <returns></returns>
        public static int ToInt(this bool @bool) => @bool ? 1 : 0;



    }
}
