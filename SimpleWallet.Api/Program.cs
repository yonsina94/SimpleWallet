using Microsoft.EntityFrameworkCore;
using SimpleWallet.Application.Interfaces;
using SimpleWallet.Application.Mappings;
using SimpleWallet.Application.Services;
using SimpleWallet.Infraestructure.Context;
using SimpleWallet.Infraestructure.Repositories.Implementation;
using SimpleWallet.Infraestructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper configuration
builder.Services.AddAutoMapper(typeof(GeneralProfile).Assembly);

// DbContext configuration
builder.Services.AddDbContext<SimpleWalletDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        opt => opt.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));

// Dependency Injection configuration
builder.Services.AddScoped<SimpleWalletDbContext>();
builder.Services.AddScoped<IMovementService, MovementService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<IMovementRepository, MovementRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();