using Document.DataAccess.Models;
using Document.DataAccess.Repositories;

namespace Document.Store.Services
{
    public class StoreService(ISnapshotRepository snapshotRepository) : IStoreService
    {
        private readonly ISnapshotRepository _snapshotRepository = snapshotRepository;

        public async Task CreateSnapshot(BoardSnapshot snapshot)
        {
            await _snapshotRepository.CreateSnapshot(snapshot);
        }

        public async Task DeleteSnapshotAsync(uint boardId)
        {
            await _snapshotRepository.DeleteSnapshotAsync(boardId);
        }

        public async Task<BoardSnapshot?> GetSnapshotAsync(uint boardId)
        {
            return await _snapshotRepository.GetSnapshotAsync(boardId);
        }

        public async Task UpdateSnapshotAsync(uint boardId, dynamic document)
        {
            BoardSnapshot? boardSnapshot = await GetSnapshotAsync(boardId);

            if (boardSnapshot == null)
            {
                throw new NullReferenceException("Board does not exist!");
            }
            await _snapshotRepository.UpdateSnapshotAsync(boardSnapshot);
        }
    }
}
