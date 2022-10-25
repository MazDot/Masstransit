using ClassLibrary1;
using MassTransit;
using WebApplication1.Messages;

namespace WebApplication2
{
    public class ThirdConsumer : IConsumer<SharedMessage>
    {
        private readonly IPublishEndpoint _endpoint;
        public ThirdConsumer(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public Task Consume(ConsumeContext<SharedMessage> context)
        {
            _endpoint.Publish<C1Message>(new { Text = "sssss" },l=>l.SetRoutingKey("WA2.Test.WA1"));
            return Task.CompletedTask;
        }
    }

}
