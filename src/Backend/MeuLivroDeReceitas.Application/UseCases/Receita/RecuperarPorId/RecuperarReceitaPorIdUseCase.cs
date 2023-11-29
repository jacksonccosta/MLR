using AutoMapper;
using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Exeptions;

namespace MeuLivroDeReceitas.Application;

public class RecuperarReceitaPorIdUseCase : IRecuperarReceitaPorIdUseCase
{
    private readonly IMapper _mapper;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IReceitaReadOnlyRepositorio _repositorio;

    public RecuperarReceitaPorIdUseCase(IMapper mapper, IUsuarioLogado usuarioLogado, IReceitaReadOnlyRepositorio repositorio)
    {
        _mapper = mapper;
        _usuarioLogado = usuarioLogado;
        _repositorio = repositorio;
    }

    public async Task<ResponseReceitaJson> Executar(long id)
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();
        var receita = await _repositorio.RecuperaReceitasPorId(id);

        Validar(usuarioLogado, receita);

        return _mapper.Map<ResponseReceitaJson>(receita);
    }

    private static void Validar(Usuario usuarioLogado, Receita receita)
    {
        if (receita == null || receita.UsuarioId != usuarioLogado.Id)
            throw new ErrosDeValidacaoException(new List<string>() { ResourceMensagensDeErro.RECEITA_NAO_ENCONTRADA});
    }
}
