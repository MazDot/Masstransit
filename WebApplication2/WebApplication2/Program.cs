using MassTransit;
using WebApplication2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ThirdConsumer>();
    x.UsingRabbitMq((ctx, cfg) =>
    {

        cfg.ReceiveEndpoint("Third_Queue", e =>
        {
            //e.Bind("WebApplication1.Messages:ThirdMessage", d =>
            //{
            //    d.ExchangeType = "topic";
            //    d.RoutingKey = "WA1.Test.WA2";
            //});
            //e.Bind<ThirdMessage>(d =>
            //{
            //    d.ExchangeType = "topic";
            //    d.RoutingKey = "WA1.Test.WA2";
            //});

            e.Consumer<ThirdConsumer>(ctx);
            //e.ConfigureConsumer<ThirdConsumer>(ctx);

            //e.Bind<PublishMessage>();
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
