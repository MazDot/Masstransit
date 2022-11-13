using ClassLibrary1;
using MassTransit;

namespace WebApplication3
{
    public class SecondSharedMessageConsumerJunior : IConsumer<SecondSharedMessage>
    {
        private readonly IPublishEndpoint _endpoint;
        public SecondSharedMessageConsumerJunior(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public Task Consume(ConsumeContext<SecondSharedMessage> context)
        {
            _endpoint.Publish<SharedMessage>(new { Text = context.Message.Text }, x => x.SetRoutingKey("WA1.Test.WA2"));
            return Task.CompletedTask;
        }
    }
}
