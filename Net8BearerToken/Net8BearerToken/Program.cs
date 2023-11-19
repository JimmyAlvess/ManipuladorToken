using Microsoft.OpenApi.Models;
using System.Security.Cryptography.Xml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    // Configuração da documentação do Swagger para a versão "v1"  da API
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "apinet8", Version = "v1" });

    // Adiciona a definição de segurança para usar o esquema "Bearer"
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",                  // O nome do cabeçalho que contém o token
        Type = SecuritySchemeType.ApiKey,        // Tipo de esquema de segurança (ApiKey neste caso)
        Scheme = "Bearer",                        // Nome do esquema de segurança (Bearer neste caso)
        BearerFormat = "token",                   // Formato esperado para o token
        In = ParameterLocation.Header,            // Onde o token será enviado (no cabeçalho neste caso)
        Description = "Token de Acesso",         // Descrição do token
    });

    // Adiciona a exigência de segurança para todas as operações, referenciando o esquema "Bearer"
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
            new string[]{}  // Nenhuma permissão específica exigida (um array vazio)
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
