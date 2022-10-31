using ClassLibrary1;
using MassTransit;
using WebApplication1.Messages;

namespace WebApplication2
{
    public class SharedMessageConsumer : IConsumer<SharedMessage>
    {
        private readonly IPublishEndpoint _endpoint;
        public SharedMessageConsumer(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public Task Consume(ConsumeContext<SharedMessage> context)
        {
            _endpoint.Publish<SecondSharedMessage>(new { Text = "sssss" },l=>l.SetRoutingKey("WA2.Test.WA3"));
            return Task.CompletedTask;
        }
    }

}
