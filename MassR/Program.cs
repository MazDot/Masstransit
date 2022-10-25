using System;
//using TopologyContracts;
using MassTransit;
using MassR;

public class Program
{
    public static void Main()
    {
        IPublishEndpoint publishEndpoint;
        var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Publish<PublishMessage>(x =>
            {
                x.Durable = false; // default: true
                x.AutoDelete = true; // default: false
                x.ExchangeType = "fanout"; // default, allows any valid exchange type
            });

            cfg.Publish<PublishIgnoreMessage>(x =>
            {
                x.Exclude = true; // do not create an exchange for this type
            });

            cfg.ReceiveEndpoint("Test_Queue", e =>
            {
                //e.Bind("PublishMessage");
                e.Bind<PublishMessage>();
                e.Consumer<Consumer>();
            });

        });
        busControl.Start();

        Console.WriteLine("Number To publish:");
        var number = Console.ReadLine();

        //publishEndpoint.Publish<PublishMessage>(new PublishMessage { Number = number });


    }
}
