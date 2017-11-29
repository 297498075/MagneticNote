using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Common
{
    public class HttpHelper
    {
        public static String URL { get; set; }

        public static String GetString(String requestData = null)
        {
            HttpWebRequest request = null;
            if (requestData != null)
            {
                request = (HttpWebRequest)WebRequest.Create(URL + "?" + requestData);
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

        public static String PostString(byte[] responseData)
        {
            var request = (HttpWebRequest)WebRequest.Create(URL);

            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/x-www-form-urlencoded";
            request.GetRequestStream().Write(responseData, 0, responseData.Length);

            var response = (HttpWebResponse)request.GetResponse();

            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);

            return sr.ReadToEnd();
        }
    }
}
