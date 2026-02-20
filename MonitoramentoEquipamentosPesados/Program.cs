using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoramentoEquipamentosPesados.Data;
using System.Text.Json.Serialization; // 👈 IMPORTANTE

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(cs);
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// ✅ CONFIGURAÇÃO CORRETA PARA ENUM COMO STRING
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();