using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.Reflection;
using System;

namespace Util
{
    public static class Json
    {
        public static string GetJson(string content)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{\"Head\":[");
            if (content.Length > 0) builder.Append(content);
            builder.Append("]}");
            return builder.ToString();
        }
        public static string GetJsonOne<T>(T obj)
        {
            try
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    json.WriteObject(stream, obj);
                    string szJson = Encoding.UTF8.GetString(stream.ToArray());
                    return szJson;
                }
            }
            catch
            {
                return null;
            }
        }
        public static string GetJsonList<T>(IEnumerable<T> list)
        {
            if (list == null || list.Count() == 0) return string.Empty;

            StringBuilder builder = new StringBuilder();
            bool first = true;
            foreach (T obj in list)
            {
                if (!first) builder.Append(",");
                builder.Append(GetJsonOne<T>(obj));
                first = false;
            }
            return builder.ToString();
        }
        public static T Parse<T>(string json) where T : class
        {
            try
            {
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
                }
            }
            catch
            {
                return null;
            }
        }
        public static string ScriptSerialize(object obj)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = int.MaxValue;
                return js.Serialize(obj);
            }
            catch
            {
                return string.Empty;
            }
        }
        public static T ScriptDeserialize<T>(string json) where T : class
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = int.MaxValue;
                return js.Deserialize<T>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}
