using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;

namespace Common
{
    public class HttpHelper
    {
        public static String URL { get; set; }

        public static String GetString(IEnumerable<KeyValuePair<String, String>> requestData = null)
        {
            HttpWebRequest request = null;
            if (requestData != null)
            {
                StringBuilder data = new StringBuilder();
                foreach (var i in requestData)
                {
                    data.Append("&" + i.Key + "=" + i.Value);
                }

                //url编码.
                string encode = HttpUtility.UrlEncode(data.ToString(), Encoding.UTF8);

                request = (HttpWebRequest)WebRequest.Create(URL + encode);
            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create(URL);
            }
            request.ContentType = "application/x-www-form-urlencoded";
            var response = (HttpWebResponse)request.GetResponse();

            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);

            return sr.ReadToEnd();
        }

        public static String PostString(byte[] requestData)
        {
            var request = (HttpWebRequest)WebRequest.Create(URL);

            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/x-www-form-urlencoded";
            request.GetRequestStream().Write(requestData, 0, requestData.Length);

            var response = (HttpWebResponse)request.GetResponse();

            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);

            return sr.ReadToEnd();
        }
    }
}
