using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneByte.Data;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddSwaggerGen();

var app = builder.Build();
if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();