using Newtonsoft.Json;
using System.Collections.Generic;

namespace MisAnalisysWorker.Models.OpenAI
{
    public class ChatGptRequest
    {
        [JsonProperty("model")]
        public string Model { get; set; } = "gpt-3.5-turbo";

        [JsonProperty("messages")]
        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

        [JsonProperty("temperature")]
        public float Temperature { get; set; } = 0.2f;

        [JsonProperty("max_tokens")]
        public int MaxTokens { get; set; } = 500;
    }

    public class ChatMessage
    {
        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        public ChatMessage(string role, string content)
        {
            Role = role;
            Content = content;
        }
    }
} 