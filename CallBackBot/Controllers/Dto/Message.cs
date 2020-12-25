using System;
using System.Text.Json.Serialization;

namespace CallBackBot.Controllers.Dto
{
    [Serializable]
    public class Message
    {
        [JsonPropertyName("date")]
        public long Date { get; set; }

        [JsonPropertyName("from_id")]
        public long FromId { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("out")]
        public long Out { get; set; }

        [JsonPropertyName("peer_id")]
        public long PeerId { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("conversation_message_id")]
        public long ConversationMessageId { get; set; }

        [JsonPropertyName("fwd_messages")]
        public object[] FwdMessages { get; set; }

        [JsonPropertyName("important")]
        public bool Important { get; set; }

        [JsonPropertyName("random_id")]
        public long RandomId { get; set; }

        [JsonPropertyName("attachments")]
        public object[] Attachments { get; set; }

        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; set; }
    }
}