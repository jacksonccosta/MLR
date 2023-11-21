using AutoMapper;
using HashidsNet;
using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Domain.Entidades;

namespace MeuLivroDeReceitas.Application;

public class AutoMapperConfig : Profile
{
    private readonly IHashids _hashId;

    public AutoMapperConfig(IHashids hashId)
    {
        _hashId = hashId;

        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRegistrarUsuarioJson, Usuario>()
            .ForMember(destino => destino.Senha, config => config.Ignore());
        CreateMap<RequestRegistrarReceitaJson, Receita>();
        CreateMap<RequestRegistrarIngredienteJson, Ingrediente>();
    }

    private void EntityToResponse()
    {
        CreateMap<Receita, ResponseReceitaJson>()
            .ForMember(destino => destino.Id, config => config.MapFrom(origem => _hashId.EncodeLong(origem.Id)));

        CreateMap<Ingrediente, ResponseIngredientesJson>()
            .ForMember(destino => destino.Id, config => config.MapFrom(origem => _hashId.EncodeLong(origem.Id)));
    }
}
