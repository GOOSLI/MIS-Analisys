using Newtonsoft.Json;
using System.Collections.Generic;

namespace MisAnalisysWorker.Models.OpenAI
{
    public class DeepseekRequest
    {
        [JsonProperty("model")]
        public string Model { get; set; } = "deepseek-chat";

        [JsonProperty("messages")]
        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

        [JsonProperty("temperature")]
        public float Temperature { get; set; } = 0f;

        [JsonProperty("max_tokens")]
        public int MaxTokens { get; set; } = 500;
    }
} 