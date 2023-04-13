using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuickLoanAPI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = new ServiceCollection();

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddDbContext<LoanDbContext>(options =>
      options.UseSqlServer(configuration.GetConnectionString("LoanConnectionString")));

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
