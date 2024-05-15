using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.FunctionApp1
{
    public class HttpToast
    {
        private readonly ILogger<HttpToast> _logger;

        public HttpToast(ILogger<HttpToast> logger)
        {
            _logger = logger;
        }

        [Function("HttpToast")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
