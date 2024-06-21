using Document.DataAccess.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace Document.DataAccess.Context
{
    public class SnapshotContext : ISnapshotContext
    {
        private readonly IConfiguration _configuration;
        private readonly CosmosClient _client;

        public SnapshotContext(IConfiguration configuration) {
            _configuration = configuration;
            _client = new CosmosClient(_configuration.GetConnectionString("CosmosDB"));
        }

        public async Task<BoardSnapshot> GetSnapshot(uint boardId)
        {
            Database database = _client.GetDatabase(_configuration["CosmosDB:DatabaseName"]);
            Container container = database.GetContainer(_configuration["CosmosDB:ContainerName"]);
            return await container.ReadItemAsync<BoardSnapshot>(boardId.ToString(), new PartitionKey(boardId));
        }
    }
}
