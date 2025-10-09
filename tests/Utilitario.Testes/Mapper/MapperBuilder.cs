using AutoMapper;
using HashidsNet;
using MeuLivroDeReceitas.Application;

namespace Utilitario.Testes;

public class MapperBuilder
{
    public static IMapper Instancia()
    {
        Hashids hashids = new Hashids();
        var mockMapper = new MapperConfiguration(c =>
        {
            c.AddProfile(new AutoMapperConfig(hashids));
        });

        return mockMapper.CreateMapper();
    }
}
