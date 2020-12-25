using System;
using System.Text.Json.Serialization;

namespace CallBackBot.Controllers.Dto
{
    [Serializable]
    public class Updates
    {
        [JsonPropertyName("type")] 
        public string Type { get; set; }

        [JsonPropertyName("object")] 
        public VkObject Object { get; set; }

        [JsonPropertyName("group_id")] 
        public long GroupId { get; set; }

        [JsonPropertyName("event_id")] 
        public string EventId { get; set; }
        
        [JsonPropertyName("test")]
        public long Test { get; set; }
    }
}