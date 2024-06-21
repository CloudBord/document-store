using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Document.Store.Functions
{
    public class DeleteDocumentFunction
    {
        private readonly ILogger _logger;

        public DeleteDocumentFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DeleteDocumentFunction>();
        }

        [Function("DeleteDocumentFunction")]
        public void Run([RabbitMQTrigger("document-delete", ConnectionStringSetting = "")] string myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
