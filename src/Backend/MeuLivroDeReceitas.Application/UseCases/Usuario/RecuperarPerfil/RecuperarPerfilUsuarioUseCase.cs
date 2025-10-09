using AutoMapper;
using MeuLivroDeReceitas.Comunicacao;

namespace MeuLivroDeReceitas.Application;

public class RecuperarPerfilUsuarioUseCase : IRecuperarPerfilUsuarioUseCase
{
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IMapper _mapper;

    public RecuperarPerfilUsuarioUseCase(IUsuarioLogado usuarioLogado, IMapper mapper)
    {
        _usuarioLogado = usuarioLogado;
        _mapper = mapper;
    }

    public async Task<ResponsePerfilUsuarioJson> Executar()
    {
        var usuario = await _usuarioLogado.RecuperarUsuario();
        return _mapper.Map<ResponsePerfilUsuarioJson>(usuario);
    }
}
