using Document.DataAccess.Models;

namespace Document.Store.Services
{
    public interface IStoreService
    {
        Task<BoardSnapshot?> GetSnapshotAsync(uint boardId);
        Task CreateSnapshot(BoardSnapshot snapshot);
        Task UpdateSnapshotAsync(uint boardId, dynamic document);
        Task DeleteSnapshotAsync(uint boardId);
    }
}
