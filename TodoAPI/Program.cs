using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TodoAPI.Context;
using Microsoft.Extensions.DependencyInjection;
using TodoAPI.Repository.Interfaces;
using TodoAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

app.UseAuthorization();
app.UseCors(x => x.AllowAnyMethod().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();
