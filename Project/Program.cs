using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

using AppointmentAPI.Application.Services;
using AppointmentAPI.Domain;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();


builder.Services.AddScoped<AppointmentAPI.Application.Interfaces.IAppointmentService, AppointmentService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    
    app.UseSwagger();    

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "QWIIK API v1");  
        options.DocumentTitle = "QWIIK Project";  
    });
}
else
{
    
    app.UseExceptionHandler("/error");
    app.UseHsts();
}


app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.Run();
