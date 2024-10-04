using Newtonsoft.Json;

namespace CNF.Business.Model.Login
{
    public sealed class AntiForgeryTokenModel
    {
        [JsonProperty("antiForgeryToken")]
        public string AntiForgeryToken { get; set; }
    }
}
