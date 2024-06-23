using System.Text.Json.Serialization;

namespace Document.DataAccess.Models
{
    public class BoardSnapshot
    {
        [JsonPropertyName("id")]
        public string? id { get; set; }
        [JsonPropertyName("boardId")]
        public uint boardId { get; set; }
        [JsonPropertyName("memberIds")]
        public IEnumerable<Guid> memberIds { get; set; } = [];
        [JsonPropertyName("document")]
        public dynamic? document { get; set; }
    }
}
