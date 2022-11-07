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
            return Task.CompletedTask;
        }
    }
}
