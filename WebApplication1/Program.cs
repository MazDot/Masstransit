using MassTransit;
using WebApplication1;
using WebApplication1.Messages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<Consumer>();
    x.AddConsumer<SecondConsumer>();
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Publish<PublishMessage>(d =>
        {
            d.Durable = false; 
            d.AutoDelete = true; 
            d.ExchangeType = "fanout"; 
        });

        cfg.Publish<SecondMessage>(d =>
        {
            d.Durable = false; 
            d.AutoDelete = true; 
            d.ExchangeType = "topic"; 
        });

        cfg.Publish<ThirdMessage>(d =>
        {
            d.Durable = false;
            d.AutoDelete = true; 
            d.ExchangeType = "topic";
        });

        cfg.Publish<IgnoreMessage>(d =>
        {
            d.Exclude = true;
        });

        cfg.ReceiveEndpoint("First_Queue", e =>
        {
            //e.Bind("PublishMessage");
            e.Bind<PublishMessage>();
            e.ConfigureConsumer<Consumer>(ctx);
        });

        cfg.ReceiveEndpoint("Second_Queue", e =>
        {
            //e.Bind("SecondMessage", d =>
            //{
            //    d.ExchangeType = "topic";
            //    d.RoutingKey = "src.event.dest";
            //});
            e.Bind<SecondMessage>(d =>
            {
                d.ExchangeType = "topic";
                d.RoutingKey = "src.event.dest";
            });
            e.ConfigureConsumer<SecondConsumer>(ctx);
            //e.Bind<PublishMessage>();
        });

        cfg.ReceiveEndpoint("Third_Queue", e =>
        {
            e.Bind<ThirdMessage>(d =>
            {
                d.ExchangeType = "topic";
                d.RoutingKey = "WA1.Test.WA2";
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
