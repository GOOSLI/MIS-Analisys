using Newtonsoft.Json;
using System.Collections.Generic;

namespace MisAnalisysWorker.Models.OpenAI
{
    public class ExtractedServicesResponse
    {
        [JsonProperty("service_ids")]
        public List<int> ServiceIds { get; set; }
    }
} 