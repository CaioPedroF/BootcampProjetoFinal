using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoramentoEquipamentosPesados.Data;
using MonitoramentoEquipamentosPesados.Models;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();


// PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("DefaultConnection"); 
    options.UseNpgsql(cs);
});



AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
