using Document.DataAccess.Models;
using Document.DataAccess.Context;

namespace Document.DataAccess.Repositories
{
    public class SnapshotRepository : ISnapshotRepository
    {
        private readonly ISnapshotContext _snapshotContext;

        public SnapshotRepository(ISnapshotContext snapshotDbContext) 
        {
            _snapshotContext = snapshotDbContext;
        }

        public async Task<BoardSnapshot> CreateSnapshot(BoardSnapshot snapshot)
        {
            await _snapshotContext.CreateSnapshot(snapshot);
            //await _snapshotContext.Snapshots.AddAsync(snapshot);
            //await _snapshotContext.SaveChangesAsync();
            return snapshot;
        }

        public async Task DeleteSnapshotAsync(uint boardId)
        {
            await _snapshotContext.DeleteSnapshot(boardId);
            //var content = await GetSnapshotAsync(boardId);
            //if (content == null) throw new Exception();
            ////await _snapshots.DeleteOneAsync(x => x.BoardId == boardId);
            //_snapshotContext.Snapshots.Remove(content);
            //await _snapshotContext.SaveChangesAsync();
        }

        public async Task<BoardSnapshot?> GetSnapshotAsync(uint boardId)
        {
            return await _snapshotContext.GetSnapshot(boardId);
        }

        public Task UpdateSnapshotAsync( BoardSnapshot boardSnapshot)
        {
            //await _snapshots.ReplaceOneAsync(x => x.Id == boardSnapshot.Id, boardSnapshot);
            //await _snapshotContext.Snapshots.AddAsync(boardSnapshot);
            //await _snapshotContext.SaveChangesAsync();
            throw new NotImplementedException();
        }
    }
}
