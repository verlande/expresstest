using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace Company.FunctionApp1
{
    public class HttpTrigger2
    {
        private readonly ILogger<HttpTrigger2> _logger;

        public HttpTrigger2(ILogger<HttpTrigger2> logger)
        {
            _logger = logger;
        }

        [Function("HttpTrigger2")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var name = req.Query["name"];
            var ip = req.HttpContext.Connection.RemoteIpAddress?.ToString();
            
            return string.IsNullOrEmpty(name) || name.Count == 0
                ? new BadRequestObjectResult("Please pass a name on the query string")
                : new OkObjectResult($"Hello, {name} from {ip}");
        }
    }
}
