using System;
using System.Text.Json.Serialization;

namespace CallBackBot.Controllers.Dto
{
    [Serializable]
    public class ClientInfo
    {
        [JsonPropertyName("button_actions")]
        public string[] ButtonActions { get; set; }

        [JsonPropertyName("keyboard")]
        public bool Keyboard { get; set; }

        [JsonPropertyName("inline_keyboard")]
        public bool InlineKeyboard { get; set; }

        [JsonPropertyName("carousel")]
        public bool Carousel { get; set; }

        [JsonPropertyName("lang_id")]
        public long LangId { get; set; }
    }
}