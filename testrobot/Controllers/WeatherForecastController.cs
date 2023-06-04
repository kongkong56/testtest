using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.Work.Entities.Request.KF;
using testrobot.Utils;

namespace testrobot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task< IEnumerable<WeatherForecast> > Get()
        {

            string response = await ChatUtils.TextMessageResponseAsync("ceshi", "https://api.chatuapi.com", "vxjgUAGFcF6cdW0Zzg1jWQU4GaU4zy1yGJrR10xD9I9");
            _logger.LogInformation(response);
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}