using Document.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.DataAccess.Repositories
{
    public interface ISnapshotRepository
    {
        Task<BoardSnapshot?> GetSnapshotAsync(uint boardId);
        Task<BoardSnapshot> CreateSnapshot(BoardSnapshot snapshot);
        Task UpdateSnapshotAsync(BoardSnapshot boardSnapshot);
        Task DeleteSnapshotAsync(uint boardId);
    }
}
