using MassTransit;
using WebApplication1.Messages;

namespace WebApplication1
{
    public class Consumer : IConsumer<PublishMessage>
    {
        private readonly IPublishEndpoint _endpoint;
        public Consumer(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public Task Consume(ConsumeContext<PublishMessage> context)
        {
            _endpoint.Publish<SecondMessage>(new { Text = context.Message.Text }, x => x.SetRoutingKey("src.event.dest"));
            return Task.CompletedTask;
        }
    }

}
