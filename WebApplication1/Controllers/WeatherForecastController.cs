using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Messages;
using ClassLibrary1;
using WebApplication1.InterfaceMessage;

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
            await _endpoint.Publish<SharedMessage>(new { Text = Text }, x => 
            {
                x.SetRoutingKey("WA1.Test.WA2");
                
            });

            return Ok();
        }

        [HttpGet("{Text}/GetAnother")]
        public async Task<IActionResult> GetAnother(string Text)
        {
            await _endpoint.Publish<ConsumerSideMessage>(new { Text = Text }, x =>
            {
                x.SetRoutingKey("WA1.ConsumerSide.WA2");
            });

            return Ok();
        }

    }
}