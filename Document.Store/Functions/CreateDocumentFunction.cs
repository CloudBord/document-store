using Document.DataAccess.Models;
using Document.Store.Requests;
using Document.Store.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Document.Store.Functions
{
    public class CreateDocumentFunction
    {
        private readonly ILogger _logger;
        private readonly IStoreService _storeService;

        public CreateDocumentFunction(ILoggerFactory loggerFactory, IStoreService storeService)
        {
            _logger = loggerFactory.CreateLogger<CreateDocumentFunction>();
            _storeService = storeService;
        }

        [Function("CreateDocumentFunction")]
        public async Task<IActionResult> Run([RabbitMQTrigger("board-create", ConnectionStringSetting = "ConnectionStrings:RabbitMQ")] string myQueueItem)
        {
            ObjectResult result = JsonConvert.DeserializeObject<ObjectResult>(myQueueItem.Replace("\r", string.Empty).Replace("\n", string.Empty))!;
            if (result.StatusCode >= 300 || result == null)
            {
                return new BadRequestResult();
            }

            CreateDocumentRequest request = JsonConvert.DeserializeObject<CreateDocumentRequest>(result!.Value!.ToString()!)!;
            try
            {
                BoardSnapshot snapshot = new BoardSnapshot
                {
                    id = request.BoardId.ToString(),
                    boardId = request.BoardId,
                    memberIds = request.MemberIds.ToArray()
                };
                await _storeService.CreateSnapshot(snapshot);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Could not register new board: " + ex.Message, ex);
                return new BadRequestResult();
            }

            return new OkResult();
        }
    }
}
