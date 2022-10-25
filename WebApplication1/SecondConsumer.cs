using MassTransit;
using WebApplication1.Messages;

namespace WebApplication1
{
    public class SecondConsumer : IConsumer<SecondMessage>
    {
        private readonly IPublishEndpoint _endpoint;
        public SecondConsumer(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public Task Consume(ConsumeContext<SecondMessage> context)
        {
            _endpoint.Publish<ThirdMessage>(new { Text = context.Message.Text }, x => x.SetRoutingKey("WA1.Test.WA2"));
            return Task.CompletedTask;
        }
    }

}
