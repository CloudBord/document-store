using Document.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.DataAccess.Context
{
    public interface ISnapshotContext
    {
        Task<BoardSnapshot> GetSnapshot(uint boardId);
    }
}
