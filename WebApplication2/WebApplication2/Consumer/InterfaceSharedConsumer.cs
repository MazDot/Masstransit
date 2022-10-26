using ClassLibrary1;
using MassTransit;
using WebApplication2.InterfaceMessage;

namespace WebApplication2.Consumer
{
    public class InterfaceSharedConsumer : IConsumer<InterfaceSharedMessage>
    {
        private readonly IPublishEndpoint _endpoint;

        public InterfaceSharedConsumer(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public Task Consume(ConsumeContext<InterfaceSharedMessage> context)
        {
            return Task.CompletedTask;
        }
    }
}
