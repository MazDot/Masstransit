using ClassLibrary1;
using MassTransit;
using WebApplication1.Messages;
using WebApplication2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SharedMessageConsumer>();
    x.UsingRabbitMq((ctx, cfg) =>
    {

        #region WorkingSharedMessageConfig
        cfg.ReceiveEndpoint("Shared_Queue", e =>
        {
            e.ConfigureConsumeTopology = false;
            //also one more config : x.AddConsumer<ThirdConsumer>(); before x.UsingRabbitMq(...)
            e.ConfigureConsumer<SharedMessageConsumer>(ctx);
        });
        #endregion

        //cfg.Publish<C1Message>(d =>
        //{
        //    d.Durable = true;
        //    d.AutoDelete = true;
        //    d.ExchangeType = "topic";
        //});

        //cfg.ReceiveEndpoint("C1_Queue", e =>
        //{
        //    e.ConfigureConsumeTopology = false;
        //    e.Bind<C1Message>(d =>
        //    {
        //        d.ExchangeType = "topic";
        //        d.RoutingKey = "WA2.Test.WA1";
        //    });
        //});

        cfg.Publish<SecondSharedMessage>(d =>
        {
            d.Durable = true;
            d.AutoDelete = true;
            d.ExchangeType = "topic";
        });

        cfg.ReceiveEndpoint("SecondShared_Queue", e =>
        {
            e.ConfigureConsumeTopology = false;
            e.Bind<SecondSharedMessage>(d =>
            {
                d.ExchangeType = "topic";
                d.RoutingKey = "WA2.Test.WA3";
            });
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
