using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassR
{
    public class Consumer : IConsumer<PublishMessage>
    {
        public Task Consume(ConsumeContext<PublishMessage> context)
        {
            Console.WriteLine("the nmber is: [0]", context.Message.Number);
            Console.ReadLine();
            return Task.CompletedTask;
        }
    }
}
