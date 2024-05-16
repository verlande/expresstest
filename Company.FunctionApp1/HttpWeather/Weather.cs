using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Company.FunctionApp1.HttpWeather;

public class Weather
{
    private readonly ILogger _logger;

    public Weather(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<Weather>();
    }

    [Function("Weather")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        var location = req.Query["location"];
        
        if (string.IsNullOrWhiteSpace(location))
        {
            await response.WriteStringAsync("Please pass a location on the query string");
            return response;
        }
        _logger.LogInformation("Function parameters: {Parameters}", location);
        var client = new HttpClient();
        var res = await client.GetStringAsync($"https://wttr.in/{location?.ToUpperInvariant()}?format=%C+%t+%w");
        
        await response.WriteStringAsync($"The weather in {location} is {res}");

        return response;
        
    }
}