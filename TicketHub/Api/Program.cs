using Api.Extensions;
using Application.UseCases.CustomerUseCases.CreateCustomer;
using Application.UseCases.CustomerUseCases.GetCustomer;
using Application.UseCases.EventUseCases.CreateEvent;
using Application.UseCases.PartnerUseCases.CreatePartner;
using Application.UseCases.PartnerUseCases.GetPartner;
using Application.UseCases.TicketUseCases;
using Data.EntityFramework;
using Domain.Customers.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataEfCore(builder.Configuration);

// Use Cases
builder.Services.AddTransient<ICreateCustomerUseCase, CreateCustomerUseCase>();
builder.Services.AddTransient<IGetCustomerByIdUseCase, GetCustomerByIdUseCase>();
builder.Services.AddTransient<ICreateEventUseCase, CreateEventUseCase>();
builder.Services.AddTransient<ICreatePartnerUseCase, CreatePartnerUseCase>();
builder.Services.AddTransient<IGetPartnerByIdUseCase, GetPartnerByIdUseCase>();
builder.Services.AddTransient<ISubscribeCustomerToEventUseCase, SubscribeCustomerToEventUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
