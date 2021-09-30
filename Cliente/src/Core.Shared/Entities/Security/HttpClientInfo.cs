using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Shared
{
    public class HttpClientInfo
    {

        #region [ Properties ]

        public string Ip { get; set; }

        public string Agent { get; set; }

        public string ServerIp { get; set; }

        public string UrlRequested { get; set; }

        public string Method { get; set; }

        public string Token { get; set; }

        public string ContentType { get; set; }

        #endregion

        #region [ Ctor ]

        public HttpClientInfo()
        {

        }

        #endregion

    }
}
