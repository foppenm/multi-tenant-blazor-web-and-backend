using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FunctionsApi
{
    public class AuthenticatedFunction
    {
        [FunctionName("AuthenticatedFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ClaimsPrincipal claimsPrincipal,
            ILogger log)
        {
            var claims = claimsPrincipal.Claims.Select(x => new { x.Type, x.Value });

            foreach (var item in claimsPrincipal.Claims)
                log.LogInformation($"Type: {item.Type} Value: {item.Value}");

            return new OkObjectResult(claims);
        }
    }
}
