using Newtonsoft.Json;

namespace Application.Helper
{
    public static class JSONSerializerHelper
    {
        public static T Deserialize<T>(object data)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(data));
        }
    }
}