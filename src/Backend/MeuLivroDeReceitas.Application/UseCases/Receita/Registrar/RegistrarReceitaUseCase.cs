using AutoMapper;
using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Exeptions;

namespace MeuLivroDeReceitas.Application;

public class RegistrarReceitaUseCase : IRegistrarReceitaUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IReceitaWriteOnlyRepositorio _repositorio;

    public RegistrarReceitaUseCase(IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho, IUsuarioLogado usuarioLogado, IReceitaWriteOnlyRepositorio repositorio)
    {
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _usuarioLogado = usuarioLogado;
        _repositorio = repositorio;
    }

    public async Task<ResponseReceitaJson> Executar(RequestRegistrarReceitaJson request)
    {
        Validar(request);
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();
        var receita = _mapper.Map<Receita>(request);
        receita.UsuarioId = usuarioLogado.Id;

        await _repositorio.Registrar(receita);
        await _unidadeDeTrabalho.Commit();

        return _mapper.Map<ResponseReceitaJson>(receita);
    }

    private void Validar(RequestRegistrarReceitaJson request)
    {
        var validador = new RegistrarReceitaValidator();
        var result = validador.Validate(request);

        if (!result.IsValid)
        {
            var mensagensDeErro = result.Errors.Select(m => m.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }
    }
}


