using ClassLibrary1;
using MassTransit;

namespace WebApplication1
{
    public class TheOtherConsumer : IConsumer<SharedMessage>
    {
        private readonly IPublishEndpoint _endpoint;

        public TheOtherConsumer(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public Task Consume(ConsumeContext<SharedMessage> context)
        {
            _endpoint.Publish<SharedMessage>(new { Text = context.Message.Text }, x => x.SetRoutingKey("WA1.Test.WA2"));
            return Task.CompletedTask;
        }
    }
}
