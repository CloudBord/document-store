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

        public Task DeleteSnapshotAsync(uint boardId)
        {
            throw new NotImplementedException();
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
                await CreateSnapshot(new BoardSnapshot
                {
                    Id = Guid.NewGuid().ToString(),
                    BoardId = boardId,
                    Document = document
                });
                //throw new NullReferenceException("Board does not exist!");
            }
            else
            {
                await _snapshotRepository.UpdateSnapshotAsync(boardSnapshot);
            }
        }
    }
}
