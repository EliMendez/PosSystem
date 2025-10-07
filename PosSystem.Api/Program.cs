using Microsoft.EntityFrameworkCore;
using PosSystem.Model.Context;
using PosSystem.Repository.Repository;
using AutoMapper;
using PosSystem.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using PosSystem.Model.Model;

var builder = WebApplication.CreateBuilder(args);

// Get and validate the connection to the database
var connectionString = builder.Configuration.GetConnectionString("Connection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("La cadena de conexión 'Connection' no está configurada");
}

builder.Services.AddDbContext<PosSystemContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration ASP.Net Identity
builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<PosSystemContext>()
    .AddDefaultTokenProviders();

// Register Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register repositories with their interfaces
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();
builder.Services.AddScoped<IDocumentNumberRepository, DocumentNumberRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

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
