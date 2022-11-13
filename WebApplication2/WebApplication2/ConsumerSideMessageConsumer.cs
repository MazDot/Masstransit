using ClassLibrary1;
using MassTransit;

namespace WebApplication2
{
    public class ConsumerSideMessageConsumer : IConsumer<ConsumerSideMessage>
    {
        public Task Consume(ConsumeContext<ConsumerSideMessage> context)
        {
            return Task.CompletedTask;
        }
    }
}
