using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Common.Models.Responses
{
    [JsonObject(NamingStrategyType = typeof(DefaultNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Response<T>
    {
        public T Data { get; set; }

        public Response()  { }
        public Response(T data)
        {
            Data = data;
        }
    }
}
