using System.Text.Json.Serialization;

namespace Realtime.API.Entities;

public class Message
{
    [JsonPropertyName("_id")] public string? Id { get; set; } = string.Empty;
    [JsonPropertyName("content")] public string Content { get; set; } = string.Empty;
    [JsonPropertyName("sender")] public string Sender { get; set; } = string.Empty;
    [JsonPropertyName("senderTitle")] public string SenderTitle { get; set; } = string.Empty;
    [JsonPropertyName("createdAt")] public DateTime CreatedAt { get; set; }
    [JsonPropertyName("updatedAt")] public DateTime UpdatedAt { get; set; }
    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;
    [JsonPropertyName("readBy")] public string ReadBy { get; set; } = string.Empty;
    [JsonPropertyName("sentAt")] public DateTime SentAt { get; set; }
    [JsonPropertyName("chat")] public string Chat { get; set; } = string.Empty;
}