using Alura_JornadaMilhas.Data;
using Alura_JornadaMilhas.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =============================================================================================
// MYSQL
var connectionString = builder.Configuration.GetConnectionString("JornadaConnectionString");
builder.Services.AddDbContext<MilhasContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// AUTOMAPPER
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add Custom Services
builder.Services.AddScoped<DepoimentoService>();
builder.Services.AddScoped<DestinoService>();

// =============================================================================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ---------------------------
app.UseStaticFiles();   // IWebHostEnvironment retornar WebRootPath (precisa criar uma pasta wwwroot na raiz
app.UseCors();          // Usar a configuração de CORS
// ---------------------------

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
