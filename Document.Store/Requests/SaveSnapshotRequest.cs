using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Document.Store.Requests
{
    public class SaveSnapshotRequest
    {
        [JsonProperty("boardId")]
        public required uint BoardId { get; set; }
        [JsonProperty("document")]
        public dynamic? Document { get; set; }

        public SaveSnapshotRequest() { }
    }
}
