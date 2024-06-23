using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Store.Requests
{
    public class CreateDocumentRequest
    {
        public uint BoardId { get; set; }
        public IEnumerable<Guid> MemberIds { get; set; } = [];
    }
}
