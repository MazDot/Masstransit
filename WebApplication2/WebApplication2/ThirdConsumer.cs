using MassTransit;

namespace WebApplication2
{
    public class ThirdConsumer : IConsumer<ThirdMessage>
    {
        private readonly IPublishEndpoint _endpoint;
        public ThirdConsumer(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public Task Consume(ConsumeContext<ThirdMessage> context)
        {
            return Task.CompletedTask;
        }
    }

}
