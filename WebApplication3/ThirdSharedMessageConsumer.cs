using ClassLibrary1;
using MassTransit;

namespace WebApplication3
{
    public class ThirdSharedMessageConsumer : IConsumer<ThirdSharedMessage>
    {
        private readonly IPublishEndpoint _endpoint;
        public ThirdSharedMessageConsumer(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public Task Consume(ConsumeContext<ThirdSharedMessage> context)
        {
            return Task.CompletedTask;
        }
    }
}
