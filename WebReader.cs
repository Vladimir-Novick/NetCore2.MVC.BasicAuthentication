using System;
using System.IO;
using System.Net;

namespace Crawler
{
    public class WebReader
    {

        private CredentialCache GetCredential(string url,string WebUserName,string WebPassword)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            CredentialCache credentialCache = new CredentialCache();
            credentialCache.Add(new System.Uri(url), "Basic", new NetworkCredential(WebUserName,
                 WebPassword));
            return credentialCache;
        }

        public string SendHttpReq(string sUrl,string WebUserName,string WebPassword)
        {
            var encoding = System.Text.Encoding.UTF8;

            string lcHtml = null;

            try
            {

                // *** Set properties
                var loHttp = (HttpWebRequest)WebRequest.Create(sUrl);

                loHttp.Timeout = 5350000;

                loHttp.Credentials = GetCredential(sUrl,WebUserName,WebPassword);
                loHttp.PreAuthenticate = true;

                    // *** Retrieve request info headers
                    using (var loWebResponse = (HttpWebResponse)loHttp.GetResponse())
                    {
                        using (var responseStream = loWebResponse.GetResponseStream())
                        {

                            using (var loResponseStream = new StreamReader(loWebResponse.GetResponseStream(), encoding))
                            {

                                lcHtml = loResponseStream.ReadToEnd();
                                loResponseStream.Close();

                            }
                        }
                    }
            } catch ( Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lcHtml;

        }
    }
}
