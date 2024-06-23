using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Store.Requests
{
    public class DeleteDocumentRequest
    {
        [JsonProperty("result")]
        public bool Result { get; set; }
        [JsonProperty("boardId")]
        public uint BoardId { get; set; }
    }
}
