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
            await _endpoint.Publish<PublishMessage>(new { Text = Text });

            return Ok();
        }

        [HttpGet("{Text}/InterfaceSharedPublish")]
        public async Task<IActionResult> GetSharedTopic(string Text)
        {
            await _endpoint.Publish<InterfaceSharedMessage>(new { Text = Text }, x => x.SetRoutingKey("WA1.InterfaceShared.WA2"));

            return Ok();
        }

        [HttpGet("{Text}/InterfaceInProjPublish")]
        public async Task<IActionResult> GetInProjTopic(string Text)
        {
            await _endpoint.Publish<InterfaceWA1Message>(new { Text = Text }, x => x.SetRoutingKey("WA1.InterfaceInProj.WA2"));

            return Ok();
        }
    }
}