using MassTransit;
using WebApplication1.Messages;

namespace WebApplication3
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
            return Task.CompletedTask;
        }
    }
}
