using CRM.API.Middlewares;
using CRM.Service;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .ConfigureSwagger();

builder
    .Services
    .AddControllers();

builder
    .Services
    .AddEndpointsApiExplorer();

builder
    .Services
    .AddSwaggerGen();

builder
    .Services
    .AddHttpContextAccessor();

builder
    .Services
    .ConfigureApplication(builder.Configuration);

builder
    .Services
    .AddServices();

builder
    .Services
    .CreateAutomapper();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();