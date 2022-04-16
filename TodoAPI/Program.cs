using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TodoAPI.Context;
using Microsoft.Extensions.DependencyInjection;
using TodoAPI.Repository.Interfaces;
using TodoAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using TodoAPI;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// For Authentication

var key = Encoding.ASCII.GetBytes(Settings.Secret);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters { 
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = false,
    }; });

builder.Services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


// Config do Banco de Dados
builder.Services.AddDbContext<ContextDatabase>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Pooling=true;Database=Todo;User Id=postgres;Password=123;",
        assembly => assembly.MigrationsAssembly(typeof(ContextDatabase).Assembly.FullName));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => x.AllowAnyMethod().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();
