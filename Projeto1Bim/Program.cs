using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Serilog.AspNetCore;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.File;
using Microsoft.OpenApi.Models;
using System.Reflection;


#region Serilog

var logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
Directory.CreateDirectory(logFolder);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(new CompactJsonFormatter(),
           Path.Combine(logFolder, ".json"),
            retainedFileCountLimit: 10,
            rollingInterval: RollingInterval.Month)
    .WriteTo.File(Path.Combine(logFolder, ".log"),
            retainedFileCountLimit: 1,
            rollingInterval: RollingInterval.Day)
    .CreateLogger();

#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Habilitar o uso do serilog.
builder.Host.UseSerilog();
#region Swagger
    //***Adicionar o Middleware do Swagger
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Gerenciamento da API...",
            Version = "v1",
            Description = $@"<h3>Projeto <b>1 Bimestre - Linguagens de Progamação I</b></h3>
                                          <p>
                                              Alguma descrição....
                                          </p>",
            Contact = new OpenApiContact
            {
                Name = "Suporte Unoeste",
                Email = string.Empty,
                Url = new Uri("https://www.unoeste.br"),
            },
        });

        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    });;
#endregion

#region IOC 

//adicionado ao IOC por requisição
builder.Services.AddScoped(typeof(Projeto1Bim.Service.CartaoService));
builder.Services.AddScoped(typeof(Projeto1Bim.Service.PagamentoService));


//adicionar ao IOC instância únicas (singleton)
builder.Services.AddSingleton<Projeto1Bim.BD>(new Projeto1Bim.BD());


#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// *** Usa o Middleware do Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    c.RoutePrefix = ""; //habilitar a página inicial da API ser a doc.
    c.DocumentTitle = "Gerenciamento de Produtos - API V1";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
