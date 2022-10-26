using MassTransit;
using WebApplication1.InterfaceMessage;

namespace WebApplication2.Consumer
{
    public class InterfaceInProjConsumer : IConsumer<InterfaceWA1Message>
    {
        public Task Consume(ConsumeContext<InterfaceWA1Message> context)
        {
            return Task.CompletedTask;
        }
    }
}
