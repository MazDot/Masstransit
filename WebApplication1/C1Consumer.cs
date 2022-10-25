using ClassLibrary1;
using MassTransit;
using WebApplication1.Messages;

namespace WebApplication1
{
    public class C1Consumer : IConsumer<C1Message>
    {
        private readonly IPublishEndpoint _endpoint;
        public C1Consumer(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public Task Consume(ConsumeContext<C1Message> context)
        {
            _endpoint.Publish<SharedMessage>(new { Text = context.Message.Text }, x => x.SetRoutingKey("WA1.Test.WA2"));
            return Task.CompletedTask;
        }
    }

}
