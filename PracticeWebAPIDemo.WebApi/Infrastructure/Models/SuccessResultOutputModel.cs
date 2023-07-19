using Newtonsoft.Json;

namespace PracticeWebAPIDemo.WebApi.Infrastructure.Models
{
    public class SuccessResultOutputModel<T>
    {
        [JsonProperty(PropertyName = "id", Required = Required.Default)]
        public Guid? Id { get; set; }

        [JsonProperty(PropertyName = "method", Required = Required.Default)]
        public string Method { get; set; }

        [JsonProperty(PropertyName = "status", Required = Required.Default)]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "data", Required = Required.Default)]
        public T Data { get; set; }

        public SuccessResultOutputModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
