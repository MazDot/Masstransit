using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Messages;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IPublishEndpoint _endpoint;

        public WeatherForecastController(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        [HttpGet("{Text}/GetWeatherForecast")]
        public async Task<IActionResult> Get(string Text)
        {
            await _endpoint.Publish<PublishMessage>(new { Text = Text });

            return Ok();
        }
    }
}