using System;
using System.Text.Json.Serialization;

namespace CallBackBot.Controllers.Dto
{
    [Serializable]
    public class VkObject
    {
        [JsonPropertyName("message")]
        public Message Message { get; set; }

        [JsonPropertyName("client_info")]
        public ClientInfo ClientInfo { get; set; }
    }
}