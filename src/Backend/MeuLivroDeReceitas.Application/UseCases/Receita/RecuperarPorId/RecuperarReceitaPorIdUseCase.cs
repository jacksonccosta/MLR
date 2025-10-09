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
    private readonly IConexaoReadOnlyRepositorio _conexoesRepositorio;

    public RecuperarReceitaPorIdUseCase(IMapper mapper, 
                                        IUsuarioLogado usuarioLogado, 
                                        IReceitaReadOnlyRepositorio repositorio, 
                                        IConexaoReadOnlyRepositorio conexoesRepositorio)
    {
        _mapper = mapper;
        _usuarioLogado = usuarioLogado;
        _repositorio = repositorio;
        _conexoesRepositorio = conexoesRepositorio;
    }

    public async Task<ResponseReceitaJson> Executar(long id)
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();
        var receita = await _repositorio.RecuperaReceitasPorId(id);

       await Validar(usuarioLogado, receita);

        return _mapper.Map<ResponseReceitaJson>(receita);
    }

    private async Task Validar(Usuario usuarioLogado, Receita receita)
    {
        var usuariosConectados = await _conexoesRepositorio.RecuperarDoUsuario(usuarioLogado.Id);

        if (receita is null || (receita.UsuarioId != usuarioLogado.Id && !usuariosConectados.Any(u => u.Id == receita.UsuarioId)))
            throw new ErrosDeValidacaoException(new List<string>() { ResourceMensagensDeErro.RECEITA_NAO_ENCONTRADA});
    }
}
