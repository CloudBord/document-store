using Document.Store.Requests;
using Document.Store.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Document.Store.Functions
{
    public class SaveDocumentFunction(ILogger<SaveDocumentFunction> logger, IStoreService storeService)
    {
        private readonly ILogger<SaveDocumentFunction> _logger = logger;
        private readonly IStoreService _storeService = storeService;

        [Function("SaveSnapshot")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route="store")] HttpRequest req)
        {
            if (req == null || req.Body == null)
            {
                return new BadRequestObjectResult("Invalid input");
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            SaveSnapshotRequest? request = JsonConvert.DeserializeObject<SaveSnapshotRequest>(requestBody);
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                await _storeService.UpdateSnapshotAsync(request.BoardId, request.Document);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Unauthorized document update attempted: " + ex.Message);
                return new BadRequestObjectResult("No board found");
            }
            
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
