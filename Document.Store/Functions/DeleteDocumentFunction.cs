using Document.Store.Requests;
using Document.Store.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Document.Store.Functions
{
    public class DeleteDocumentFunction
    {
        private readonly ILogger _logger;
        private readonly IStoreService _storeService;

        public DeleteDocumentFunction(ILoggerFactory loggerFactory, IStoreService storeService)
        {
            _logger = loggerFactory.CreateLogger<DeleteDocumentFunction>();
            _storeService = storeService;
        }

        [Function("DeleteDocumentFunction")]
        public async Task Run([RabbitMQTrigger("board-delete", ConnectionStringSetting = "ConnectionStrings:RabbitMQ")] string myQueueItem)
        {
            ObjectResult result = JsonConvert.DeserializeObject<ObjectResult>(myQueueItem.Replace("\r", string.Empty).Replace("\n", string.Empty))!;
            if(result.StatusCode >= 300 || result == null)
            {
                return;
            }
            DeleteDocumentRequest request = JsonConvert.DeserializeObject<DeleteDocumentRequest>(result!.Value!.ToString()!)!;
            if(!request.Result || request.BoardId <= 0)
            {
                return;
            }
            await _storeService.DeleteSnapshotAsync(request.BoardId);
        }
    }
}
