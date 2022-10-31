using ClassLibrary1;
using MassTransit;

namespace WebApplication3
{
    public class SecondSharedMessageConsumer : IConsumer<SecondSharedMessage>
    {
        private readonly IPublishEndpoint _endpoint;
        public SecondSharedMessageConsumer(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public Task Consume(ConsumeContext<SecondSharedMessage> context)
        {
            return Task.CompletedTask;
        }
    }
}
