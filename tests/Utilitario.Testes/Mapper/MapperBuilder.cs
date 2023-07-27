using AutoMapper;
using MeuLivroDeReceitas.Application;

namespace Utilitario.Testes;

public class MapperBuilder
{
    public static IMapper Instancia()
    {
        var configuracao = new MapperConfiguration(c =>
        {
            c.AddProfile<AutoMapperConfig>();
        });

        return configuracao.CreateMapper();
    }
}
