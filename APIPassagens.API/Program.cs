using APIBusService.Core.Abstractions;
using APIBusService.Infrastructure.Context;
using APIBusService.Infrastructure.Repositories;
using APIPassagens.Core.Abstractions;
using APIPassagens.Infrastructure.Configuration;
using APIPassagens.Infrastructure.Consumers;
using APIPassagens.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Net;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection("RabbitMq"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultCS")));

// Add services to the container.
builder.Services.AddSingleton(c =>
{
    var config = c.GetRequiredService<IOptions<RabbitMqConfig>>().Value;
    return new ConnectionFactory
    {
        HostName = config.HostName,
        UserName = config.UserName,
        Password = config.Password,
    }.CreateConnection();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IMessageBus, RabbitMqMessageBus>();

var myHandlers = AppDomain.CurrentDomain.Load("APIPassagens.Core");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myHandlers));

builder.Services.AddSingleton<IEmailService>(c =>
{
    var smtpClient = new SmtpClient
    {
        Host = builder.Configuration["Smtp:Host"],
        Port = int.Parse(builder.Configuration["Smtp:Port"]),
        Credentials = new NetworkCredential(builder.Configuration["Smtp:Username"], builder.Configuration["Smtp:Password"]),
        EnableSsl = true
    };
    return new SmtpEmailService(smtpClient, builder.Configuration["Smtp:FromEmail"]);
});

builder.Services.AddHostedService<TicketEmailConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
