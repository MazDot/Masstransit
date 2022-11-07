using MassTransit;
using WebApplication3;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SecondSharedMessageConsumer>();
    x.AddConsumer<SecondSharedMessageConsumerJunior>();
    x.UsingRabbitMq((ctx, cfg) =>
    {

        #region MultipleConsumerForTopicExchangeConfig
        cfg.ReceiveEndpoint("Second_Shared_Queue", e =>
        {
            e.ConfigureConsumeTopology = false;
            e.ConfigureConsumer<SecondSharedMessageConsumer>(ctx);
            e.ConfigureConsumer<SecondSharedMessageConsumerJunior>(ctx);
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
