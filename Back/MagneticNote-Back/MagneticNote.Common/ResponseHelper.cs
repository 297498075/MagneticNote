using System;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MagneticNote.Common
{
    public class ResponseHelper
    {
        public static void WriteTrue(HttpResponseBase response)
        {
            response.ContentType = "application/json";
            response.Write(JsonConvert.SerializeObject(new { Result = true }));
            response.End();
        }

        public static void WriteFalse(HttpResponseBase response)
        {
            response.ContentType = "application/json";
            response.Write(JsonConvert.SerializeObject(new { Result = false }));
            response.End();
        }

        public static void WriteNull(HttpResponseBase response)
        {
            response.ContentType = "application/json";
            response.Write(JsonConvert.SerializeObject(new { Result = "No data" }));
            response.End();
        }

        public static void WriteList<T>(HttpResponseBase response, String Value, IList<T> list)
        {
            response.ContentType = "application/json";
            response.Write("{ \"" + Value + "\":" + JsonConvert.SerializeObject(list) + " }");
            response.End();
        }

        public static void WriteObject<T>(HttpResponseBase response, String Value, T obj)
        {
            response.ContentType = "application/json";
            response.Write("{ \"" + Value + "\":" + JsonConvert.SerializeObject(obj) + " }");
            response.End();
        }

        public static void WriteList<T>(HttpResponseBase response, IList<T> Value)
        {
            response.ContentType = "application/json";
            response.Write(JsonConvert.SerializeObject(Value));
            response.End();
        }

        public static void WriteObject<T>(HttpResponseBase response, T Value)
        {
            response.ContentType = "application/json";
            response.Write(JsonConvert.SerializeObject(Value));
            response.End();
        }
    }
}
