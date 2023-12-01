using HashidsNet;
using MeuLivroDeReceitas.Api;
using MeuLivroDeReceitas.Api.Filtros;
using MeuLivroDeReceitas.Api.Middleware;
using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(option => option.LowercaseUrls = true);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.OperationFilter<HashidsOperationFilter>();
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Meu livro de receitas API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMvc(option => option.Filters.Add(typeof(FiltroDasExceptions)));

//builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(config =>
{
    config.AddProfile(new AutoMapperConfig(provider.GetService<IHashids>()));
}).CreateMapper());

builder.Services.AddScoped<UsuarioAutenticadoAttribute>();

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

AtualizarBaseDeDados();

app.Run();

void AtualizarBaseDeDados()
{
    using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    using var context = serviceScope.ServiceProvider.GetService<MeuLivroDeReceitaContext>();

    bool? dataBaseInMemory = context?.Database?.ProviderName?.Equals("Microsoft.EntityFrameworkCore.InMemory");

    if (!dataBaseInMemory.HasValue || !dataBaseInMemory.Value)
    {
        var database = builder.Configuration.GetNomeDatabase();
        var connectionString = builder.Configuration.GetConexao();

        Database.CriarDatabase(connectionString, database);

        app.MigrationBancoDeDados();
    }    
}

app.UseMiddleware<CultureMiddleware>();

#pragma warning disable CA1050, S3903, S1118
public partial class Program { }
#pragma warning restore CA1050, S3903, S1118