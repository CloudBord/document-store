namespace Document.DataAccess.Models
{
    public class BoardSnapshot
    {
        public string Id { get; set; }
        public uint BoardId { get; set; }
        public IEnumerable<uint> MemberIds { get; set; } = [];
        public dynamic? Document { get; set; }
    }
}
