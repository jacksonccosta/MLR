using AutoMapper;
using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Domain;

namespace MeuLivroDeReceitas.Application;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<RequestRegistrarUsuarioJson, Usuario>()
            .ForMember(destino => destino.Senha, config => config.Ignore());
    }
}
