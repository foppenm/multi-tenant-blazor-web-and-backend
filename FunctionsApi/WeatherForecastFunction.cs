using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FunctionsApi
{
    public class WeatherForecastFunction
    {
        [FunctionName("WeatherForecastFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ClaimsPrincipal claimsPrincipal,
            ILogger log)
        {
            var weatherForecasts = GetWeatherData();
            return new OkObjectResult(weatherForecasts);
        }

        private List<WeatherForecast> GetWeatherData()
        {
            return new List<WeatherForecast> {
                new WeatherForecast
                {
                    Date = DateTime.Parse("2018-05-06"),
                    Summary = "Freezing",
                    TemperatureC = 33
                },
                new WeatherForecast
                {
                    Date = DateTime.Parse("2018-05-07"),
                    Summary = "Bracing",
                    TemperatureC = 14
                },
                new WeatherForecast
                {
                    Date = DateTime.Parse("2018-05-08"),
                    Summary = "Freezing",
                    TemperatureC = -13
                }
            };
        }
    }
}
