using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Utility
{
    public class JsonUtility
    {
        public static List<T> JsonDeserialize<T>(string json)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            List<T> objs = Serializer.Deserialize<List<T>>(json);
            return objs;
        }
    }
}
