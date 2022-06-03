using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OneByte.Data;
using OneByte.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration)=>{
     configuration.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .WriteTo.Console();
});

builder.Services.AddDbContext<OneByteDbContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options=>{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = false;
}).AddEntityFrameworkStores<OneByteDbContext>();
builder.Services.AddControllers();

builder.Services.AddAuthentication(options=>{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options=>{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = "OneByte",
        ValidIssuer = "OneByte",
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("kIoAeLFFzuJwJDGTfv4BBQg0QMQfkFH7ALBaOFrBu528ixZBb3bJ6mbRansUgoD"))
    };
});
builder.Services.AddSwaggerGen();

var app = builder.Build();
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();