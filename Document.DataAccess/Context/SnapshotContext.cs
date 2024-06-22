using Document.DataAccess.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace Document.DataAccess.Context
{
    public class SnapshotContext : ISnapshotContext
    {
        private readonly IConfiguration _configuration;
        private readonly CosmosClient _client;

        public SnapshotContext(IConfiguration configuration, CosmosClient client) {
            _configuration = configuration;
            _client = client;
        }

        public async Task CreateSnapshot(BoardSnapshot snapshot)
        {
            Database database = _client.GetDatabase(_configuration["CosmosDB:DatabaseName"]);
            Container container = database.GetContainer(_configuration["CosmosDB:ContainerName"]);
            await container.CreateItemAsync<BoardSnapshot>(snapshot, new PartitionKey(snapshot.boardId));
        }

        public async Task DeleteSnapshot(uint boardId)
        {
            Database database = _client.GetDatabase(_configuration["CosmosDB:DatabaseName"]);
            Container container = database.GetContainer(_configuration["CosmosDB:ContainerName"]);
            try
            {
                await container.DeleteItemAsync<BoardSnapshot>(boardId.ToString(), new PartitionKey(boardId));
            }
            catch { }
        }

        public async Task<BoardSnapshot> GetSnapshot(uint boardId)
        {
            Database database = _client.GetDatabase(_configuration["CosmosDB:DatabaseName"]);
            Container container = database.GetContainer(_configuration["CosmosDB:ContainerName"]);
            return await container.ReadItemAsync<BoardSnapshot>(boardId.ToString(), new PartitionKey(boardId));
        }
    }
}
