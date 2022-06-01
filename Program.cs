using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneByte.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OneByteDbContext>();
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