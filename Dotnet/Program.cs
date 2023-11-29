using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Dotnet.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TestContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TestContext") ?? throw new InvalidOperationException("Connection string 'TestContext' not found.")));

builder.Services.AddDbContext<ChatContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TestContext") ?? throw new InvalidOperationException("Connection string 'TestContext' not found.")));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Run();
