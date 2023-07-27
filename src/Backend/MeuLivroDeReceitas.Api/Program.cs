using MeuLivroDeReceitas.Api;
using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(option => option.LowercaseUrls = true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositorio(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMvc(option => option.Filters.Add(typeof(FiltroDasExceptions)));

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(config => { 
                                                                                        config.AddProfile(new AutoMapperConfig());
                                                                                    }).CreateMapper());

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

public partial class Program { }