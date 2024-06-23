using Document.DataAccess.Models;
using Document.DataAccess.Repositories;

namespace Document.Store.Services
{
    public class StoreService(ISnapshotRepository snapshotRepository) : IStoreService
    {
        private readonly ISnapshotRepository _snapshotRepository = snapshotRepository;

        public async Task CreateSnapshot(BoardSnapshot snapshot)
        {
            ValidateBoardId(snapshot.boardId);
            await _snapshotRepository.CreateSnapshot(snapshot);
        }

        public async Task DeleteSnapshotAsync(uint boardId)
        {
            ValidateBoardId(boardId);
            await _snapshotRepository.DeleteSnapshotAsync(boardId);
        }

        public async Task<BoardSnapshot?> GetSnapshotAsync(uint boardId)
        {
            ValidateBoardId(boardId);
            return await _snapshotRepository.GetSnapshotAsync(boardId);
        }

        public async Task UpdateSnapshotAsync(uint boardId, dynamic document)
        {
            ValidateBoardId(boardId);
            BoardSnapshot? boardSnapshot = await GetSnapshotAsync(boardId);
            if (boardSnapshot == null)
            {
                throw new NullReferenceException("Board does not exist!");
            }
            await _snapshotRepository.UpdateSnapshotAsync(boardSnapshot);
        }

        private void ValidateBoardId(uint boardId)
        {
            if (boardId < 1)
            {
                throw new ArgumentOutOfRangeException("boardId", boardId, "BoardId must not be lower than 1");
            }
        }
    }
}
