using Newtonsoft.Json;

namespace ARS_FE
{
    public class ODataResponse<T>
    {
        [JsonProperty("value")]
        public T Value { get; set; }
    }
}
