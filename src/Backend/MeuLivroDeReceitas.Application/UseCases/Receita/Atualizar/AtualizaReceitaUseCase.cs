using AutoMapper;
using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Exeptions;

namespace MeuLivroDeReceitas.Application;

public class AtualizaReceitaUseCase : IAtualizaReceitaUseCase
{
    private readonly IMapper _mapper;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IReceitaUpdateOnlyRepositorio _repositorio;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public AtualizaReceitaUseCase(IUnidadeDeTrabalho unidadeDeTrabalho, IMapper mapper, IUsuarioLogado usuarioLogado, IReceitaUpdateOnlyRepositorio repositorio)
    {
        _mapper = mapper;
        _usuarioLogado = usuarioLogado;
        _repositorio = repositorio;
        _unidadeDeTrabalho = unidadeDeTrabalho;

    }
    public async Task Executar(long id, RequestReceitaJson request)
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();
        var receita = await _repositorio.RecuperaReceitasPorId(id);
        Validar(usuarioLogado, receita);
        _mapper.Map(request, receita);
        _repositorio.Update(receita);
        await _unidadeDeTrabalho.Commit();
    }

    private static void Validar(Usuario usuarioLogado, Receita receita)
    {
        if (receita == null || receita.UsuarioId != usuarioLogado.Id)
            throw new ErrosDeValidacaoException(new List<string>() { ResourceMensagensDeErro.RECEITA_NAO_ENCONTRADA });
    }
}
