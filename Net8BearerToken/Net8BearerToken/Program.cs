using Microsoft.OpenApi.Models;
using System.Security.Cryptography.Xml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    // Configura��o da documenta��o do Swagger para a vers�o "v1"  da API
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "apinet8", Version = "v1" });

    // Adiciona a defini��o de seguran�a para usar o esquema "Bearer"
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",                  // O nome do cabe�alho que cont�m o token
        Type = SecuritySchemeType.ApiKey,        // Tipo de esquema de seguran�a (ApiKey neste caso)
        Scheme = "Bearer",                        // Nome do esquema de seguran�a (Bearer neste caso)
        BearerFormat = "token",                   // Formato esperado para o token
        In = ParameterLocation.Header,            // Onde o token ser� enviado (no cabe�alho neste caso)
        Description = "Token de Acesso",         // Descri��o do token
    });

    // Adiciona a exig�ncia de seguran�a para todas as opera��es, referenciando o esquema "Bearer"
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id ="Bearer"
                }
            },
            new string[]{}  // Nenhuma permiss�o espec�fica exigida (um array vazio)
        }
    });
});




builder.Services.AddAuthentication().AddBearerToken();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
