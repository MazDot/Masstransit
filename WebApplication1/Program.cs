using ClassLibrary1;
using MassTransit;
using WebApplication1;
using WebApplication1.InterfaceMessage;
using WebApplication1.Messages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    //x.AddConsumer<C1Consumer>();
    x.UsingRabbitMq((ctx, cfg) =>
    {
        #region WorkingSharedMessageConfig
        cfg.Publish<SharedMessage>(d =>
        {
            d.Durable = true;
            d.AutoDelete = true;
            d.ExchangeType = "topic";
        });

        cfg.ReceiveEndpoint("Shared_Queue", e =>
        {
            e.ConfigureConsumeTopology = false;
            e.Bind<SharedMessage>(d =>
            {
                d.ExchangeType = "topic";
                d.RoutingKey = "WA1.Test.WA2";
            });
            e.ExclusiveConsumer = true;
            //e.AutoStart = false;
            //e.PrefetchCount = 0;
        });
        #endregion

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
