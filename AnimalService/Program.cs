using AnimalService.Database;
using Carter;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var assembly = typeof(Program).Assembly;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AnimalDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
builder.Services.AddCarter();

// Configure RabbitMQ
builder.Services.AddMassTransit(x =>
{
    //Add outbox
    //x.AddEntityFrameworkOutbox<AnimalDbContext>(o =>
    //{
    //    o.QueryDelay = TimeSpan.FromSeconds(10);

    //    o.UseSqlServer();
    //    o.UseBusOutbox();
    //});

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("animal", false));

    // Setup RabbitMQ Endpoint
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:username", "guest"));
            host.Username(builder.Configuration.GetValue("RabbitMq:username", "guest"));
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCarter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
