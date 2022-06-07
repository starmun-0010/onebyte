using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OneByte.Data;
using OneByte.Middlewares;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var configuration = builder.Configuration;
builder.Host.UseSerilog((context, config)=>{
    config.Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .ReadFrom.Configuration(configuration)
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearch:Url"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
        NumberOfReplicas = 1,
        EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog,
        NumberOfShards = 2,
        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
        TypeName = null,
        DetectElasticsearchVersion = true,
        RegisterTemplateFailure = RegisterTemplateRecovery.IndexAnyway  
    })
    .WriteTo.Console(); 
});

builder.Services.AddDbContext<OneByteDbContext>(options=>{
    options.UseNpgsql(builder.Configuration.GetConnectionString("OneByteDatabase"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = false;
}).AddEntityFrameworkStores<OneByteDbContext>();
builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["Jwt:Audience"], 
        ValidIssuer = configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
    };
});
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
ExecuteMigrations();
app.Run();

void ExecuteMigrations()
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<OneByteDbContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
}

