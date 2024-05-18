using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TP_A16_BrunoD_WebAPI.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TP_A16_BrunoD_WebAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TP_A16_BrunoD_WebAPIContext") ?? throw new InvalidOperationException("Connection string 'TP_A16_BrunoD_WebAPIContext' not found.")));

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

app.Run();
