using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Function;

public static class AuthorizedFunctionWithParams
{
    private record Player(string Name, string Surname, string Country);

    [FunctionName("AuthorizedFunctionWithParams")]
    public static async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.User, "get", "post", Route = null)]
        HttpRequest req, ILogger log)
    {
        var players = new[]
        {
            new Player("John", "Brown", "USA"),
            new Player("Sasza", "Smirnov", "RUS"),
            new Player("Dima", "Denaturov", "RUS"),
            new Player("Adam", "Malysz", "PL"),
            new Player("Kamil", "Stoch", "PL")
        };

        log.LogInformation("C# HTTP trigger function processed a request.");

        string country = req.Query["country"];

        return country != null
            ? (ActionResult) new OkObjectResult(players.Where(p => p.Country == country.ToUpper()).ToArray())
            : new BadRequestObjectResult("Please pass a country on the query string or in the request body");
    }
}