using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PosSystem.Api.Middlewares;
using PosSystem.Model.Context;
using PosSystem.Model.Model;
using PosSystem.Repository.Interface;
using PosSystem.Repository.Repository;
using PosSystem.Service.Interface;
using PosSystem.Service.Service;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Get and validate the connection to the database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
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

// Configurate DTO validation
builder.Services.AddValidatorsFromAssembly(Assembly.Load("PosSystem.Dto"));

// Configurate JWT
var secretKey = builder.Configuration["Jwt:SecretKey"];
if(string.IsNullOrEmpty(secretKey))
{
    throw new InvalidOperationException("La clave secreta de JWT no está configurada");
}

var key = Encoding.UTF8.GetBytes(secretKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

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

// Register services with their interfaces
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IBusinessService, BusinessService>();
builder.Services.AddScoped<IDocumentNumberService, DocumentNumberService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISaleService, SaleService>();

// Configuring CORS to allow requests from Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AccessAngular", policy =>
    {
        // Configure the policy to allow requests from the specified origin
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Exceptions custom middleware
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply CORS policies
app.UseCors("AccessAngular");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
