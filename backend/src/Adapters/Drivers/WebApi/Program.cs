
using CadastroClienteRommanel.Adapters.Driven.Infrastructure.Persistence;
using CadastroClienteRommanel.Adapters.Driven.Infrastructure.Repositories;
using CadastroClienteRommanel.Adapters.Drivers.WebApi.Middleware;
using CadastroClienteRommanel.Application.UseCases.RegistrarCliente;
using CadastroClienteRommanel.Core.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
    sql => sql.EnableRetryOnFailure()));


builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddMediatR(cfg =>
{   
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);    
    cfg.RegisterServicesFromAssembly(typeof(RegistrarClienteHandler).Assembly);
});

builder.Services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<Program>();

builder.Services
   .AddControllers()
   .AddJsonOptions(opts =>
   {
       opts.JsonSerializerOptions.Converters.Add(
         new System.Text.Json.Serialization.JsonStringEnumConverter());
   });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")  
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<DomainExceptionFilter>();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}


app.UseCors("AllowLocalAngular");

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
