using Document.Store.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Document.Store.Functions
{
    public class GetDocumentFunction(ILogger<GetDocumentFunction> logger, IStoreService storeService)
    {
        private readonly ILogger<GetDocumentFunction> _logger = logger;
        private readonly IStoreService _storeService = storeService;

        [Function("GetSnapshot")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "store/{id}")] HttpRequest req, uint id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var board = await _storeService.GetSnapshotAsync(id);
            if (board == null)
            {
                return new BadRequestObjectResult("Board does not exist");
            }
            return new OkObjectResult(board);
        }
    }
}
