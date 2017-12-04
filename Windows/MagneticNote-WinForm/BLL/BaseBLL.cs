using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using Model;

namespace BLL
{
    public abstract class BaseBLL<T> where T : class
    {
        private HttpHelper HttpHelper { get; set; }
        private String HttpKey { get; set; }

        public BaseBLL()
        {
            HttpHelper = new HttpHelper();
            HttpKey = ConfigurationManager.AppSettings["HttpKey"];
        }

        public IList<T> Get(String url, IEnumerable<KeyValuePair<String, String>> requestData = null)
        {
            HttpHelper.URL = ConfigurationManager.AppSettings["ServerAddress"] + url + "?RequestKey=" + HttpKey;
            return JsonConvert.DeserializeObject<IList<T>>(HttpHelper.GetString(requestData));
        }

        public IList<T> Post(String url, String requestData = null)
        {
            HttpHelper.URL = ConfigurationManager.AppSettings["ServerAddress"] + url + "?RequestKey=" + HttpKey;

            byte[] data = null;

            if(requestData != null)
            {
                data = Encoding.UTF8.GetBytes(requestData);
            }

            return JsonConvert.DeserializeObject<IList<T>>(HttpHelper.PostString(data));
        }

        public bool GetResult(String url, IEnumerable<KeyValuePair<String, String>> requestData = null)
        {
            HttpHelper.URL = ConfigurationManager.AppSettings["ServerAddress"] + url + "?RequestKey=" + HttpKey;
            return JsonConvert.DeserializeObject<Data>(HttpHelper.GetString(requestData)).Result;
        }

        public bool PostResult(String url, String requestData = null)
        {
            HttpHelper.URL = ConfigurationManager.AppSettings["ServerAddress"] + url + "?RequestKey=" + HttpKey;

            byte[] data = null;

            if (requestData != null)
            {
                data = Encoding.UTF8.GetBytes(requestData);
            }

            return JsonConvert.DeserializeObject<Data>(HttpHelper.PostString(data)).Result;
        }
    }
}
