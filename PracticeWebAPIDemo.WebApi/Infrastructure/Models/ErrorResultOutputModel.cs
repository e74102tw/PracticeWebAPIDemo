using Newtonsoft.Json;

namespace PracticeWebAPIDemo.WebApi.Infrastructure.Models
{
    public class ErrorResultOutputModel
    {
        [JsonProperty(PropertyName = "id", Required = Required.Default)]
        public Guid? Id { get; set; }

        [JsonProperty(PropertyName = "method", Required = Required.Default)]
        public string Method { get; set; }

        [JsonProperty(PropertyName = "status", Required = Required.Default)]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "data", Required = Required.Default)]
        public object Data { get; set; }

        public ErrorResultOutputModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
