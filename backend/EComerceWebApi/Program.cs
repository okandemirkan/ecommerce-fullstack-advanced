using Application.Extensions;
using EComerceWebApi.Extensions;
using EComerceWebApi.Middlewares;
using Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationService();
builder.Services.AddSwaggerServices();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true; //.Net otomatik 500 döndürmesin, result pattern gerekli hatayı döndürsün.
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
 options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddOpenApi();
builder.Services.AddLogging();
var app = builder.Build();
app.UseCors("AllowAll");
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
