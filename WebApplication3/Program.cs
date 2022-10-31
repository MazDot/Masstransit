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
    x.AddConsumer<ThirdSharedMessageConsumer>();
    x.UsingRabbitMq((ctx, cfg) =>
    {

        cfg.ReceiveEndpoint("SecondShared_Queue", e =>
        {
            e.ConfigureConsumeTopology = false;
            e.ConfigureConsumer<SecondSharedMessageConsumer>(ctx);
        });

        cfg.ReceiveEndpoint("ThirdShared_Queue", e =>
        {
            e.ConfigureConsumeTopology = false;
            e.ConfigureConsumer<ThirdSharedMessageConsumer>(ctx);
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
