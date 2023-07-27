using AutoMapper;
using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Exeptions;
using MeuLivroDeReceitas.Domain;

namespace MeuLivroDeReceitas.Application;

public class RegistrarUsuarioUseCase : IRegistrarUsuarioUseCase
{
    private readonly IUsuarioReadOnlyRepositorio _usuarioReadyOnlyRepositorio;
    private readonly IUsuarioWriteOnlyRepositorio _repositorio;
    private readonly IMapper _mapper;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly Encriptador _encript;
    private readonly TokenController _token;

    public RegistrarUsuarioUseCase(IUsuarioWriteOnlyRepositorio repositorio, IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho, Encriptador encript, TokenController token, IUsuarioReadOnlyRepositorio usuarioReadyOnlyRepositorio)
    {
        _repositorio = repositorio;
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _encript = encript;
        _token = token;
        _usuarioReadyOnlyRepositorio = usuarioReadyOnlyRepositorio;
    }

    public async Task<ResponseUsuarioRegistradoJson> Executar(RequestRegistrarUsuarioJson request)
    {
        await Validar(request);

        var usuario = _mapper.Map<Usuario>(request);
        usuario.Senha = _encript.Criptografar(request.Senha);

        await _repositorio.Adicionar(usuario);
        await _unidadeDeTrabalho.Commit();

        var token = _token.GerarToken(request.Email);

        return new ResponseUsuarioRegistradoJson
        {
            Token = token
        };
    }

    private async Task Validar(RequestRegistrarUsuarioJson request)
    {
        var validator = new RegistrarUsuarioValidator();
        var resultado = validator.Validate(request);

        var existeUsuarioComEmail = await _usuarioReadyOnlyRepositorio.ExisteUsuarioComEmail(request.Email);

        if (existeUsuarioComEmail)
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMensagensDeErro.EMAIL_DUPLICADO));

        if (!resultado.IsValid) 
        {
            var mensagensDeErro = resultado.Errors.Select(erro => erro.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }
    }
}
