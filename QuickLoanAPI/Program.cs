using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuickLoanAPI.Infrastructure;
using NLog;
using NLog.Web;
using QuickLoanAPI.Infrastructure.DbManage.Interfaces;
using QuickLoanAPI.Infrastructure.DbManage;
using QuickLoanAPI.Application.Services.Interfaces;
using QuickLoanAPI.Application.Services;
using QuickLoanAPI.Middlewares;
using QuickLoanAPI.Application.Helpers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var _logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
var customDebug = new LogEventInfo(NLog.LogLevel.Debug, _logger.Name, "Custome Log info");
customDebug.Properties["message"] = "init main";
_logger.Log(customDebug);

try
{
  var builder = WebApplication.CreateBuilder(args);

  // Add services to the container.

  //var services = new ServiceCollection();

  builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
  var configuration = builder.Configuration;

  builder.Services.AddControllers();

  builder.Services.AddDbContext<LoanDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("LoanConnectionString")));

  builder.Services.AddScoped<ICustomerService, CustomerService>();
  builder.Services.AddScoped<IUserService, UserService>();
  builder.Services.AddScoped<IAdminService, AdminService>();

  builder.Services.AddScoped<IUserRepository, UserRepository>();
  builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
  builder.Services.AddScoped<IAdminRepository, AdminRepository>();

  // Configure CORS
  builder.Services.AddCors(options =>
  {
    options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
  });

  // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();

  var appSettingsSection = builder.Configuration.GetSection("AppSettings");
  builder.Services.Configure<AppSettings>(appSettingsSection);
  var appSettings = appSettingsSection.Get<AppSettings>();
  var key = Encoding.ASCII.GetBytes(appSettings.Secret);
  builder.Services.AddAuthentication(x =>
  {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  })
         .AddJwtBearer(x =>
         {
           x.RequireHttpsMetadata = false;
           x.SaveToken = true;
           x.TokenValidationParameters = new TokenValidationParameters
           {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(key),
             ValidateIssuer = false,
             ValidateAudience = false
           };
         });


  var app = builder.Build();

  // Configure the HTTP request pipeline.
  if (app.Environment.IsDevelopment())
  {
    app.UseSwagger();
    app.UseSwaggerUI();
  }

  app.UseMiddleware<ExceptionMiddleware>();

  app.UseCors("CorsPolicy");

  app.UseHttpsRedirection();

  app.UseAuthorization();

  app.MapControllers();

  app.Run();

}
catch (Exception exception)
{
  // NLog: catch setup errors
  var customError = new LogEventInfo(NLog.LogLevel.Error, _logger.Name, "Custome Log info");
  customError.Properties["message"] = "Stopped program because of exception" + exception.Message;
  _logger.Log(customError);

}
finally
{
  // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
  NLog.LogManager.Shutdown();
}