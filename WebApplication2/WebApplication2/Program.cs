using ClassLibrary1;
using MassTransit;
using WebApplication1.InterfaceMessage;
using WebApplication1.Messages;
using WebApplication2;
using WebApplication2.Consumer;
using WebApplication2.InterfaceMessage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ThirdConsumer>();
    x.AddConsumer<InterfaceInProjConsumer>();
    x.AddConsumer<InterfaceSharedConsumer>();
    x.UsingRabbitMq((ctx, cfg) =>
    {

        cfg.ReceiveEndpoint("Third_Queue", e =>
        {
            //e.Bind("WebApplication1.Messages:ThirdMessage", d =>
            //{
            //    d.ExchangeType = "topic";
            //    d.RoutingKey = "WA1.Test.WA2";
            ////});
            //e.Bind<ThirdMessage>(d =>
            //{
            //    d.ExchangeType = "topic";
            //    d.RoutingKey = "WA1.Test.WA2";
            //});

            e.Consumer<ThirdConsumer>(ctx);
            //e.ConfigureConsumer<ThirdConsumer>(ctx);
        });

        cfg.ReceiveEndpoint("C1_Queue", e =>
        {
            e.Bind<C1Message>(d =>
            {
                d.ExchangeType = "topic";
                d.RoutingKey = "WA2.Test.WA1";
            });
        });

        cfg.Publish<C1Message>(d =>
        {
            d.Durable = false;
            d.AutoDelete = true;
            d.ExchangeType = "topic";
        });

        cfg.ReceiveEndpoint("interfaceSharedTopic", e =>
        {
            //e.Bind<InterfaceSharedMessage>(d =>
            //{
            //    d.ExchangeType = "topic";
            //    d.RoutingKey = "WA1.InterfaceShared.WA2";
            //});
            e.Consumer<InterfaceSharedConsumer>(ctx);
        });

        cfg.ReceiveEndpoint("interfaceInProjTopic", e =>
        {
            //e.Bind<InterfaceWA1Message>(d =>
            //{
            //    d.ExchangeType = "topic";
            //    d.RoutingKey = "WA1.InterfaceInProj.WA2";
            //});
            e.Consumer<InterfaceInProjConsumer>(ctx);
        });


    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
