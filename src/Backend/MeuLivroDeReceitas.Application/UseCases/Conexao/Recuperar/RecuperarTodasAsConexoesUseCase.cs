using AutoMapper;
using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Domain;

namespace MeuLivroDeReceitas.Application;

public class RecuperarTodasAsConexoesUseCase : IRecuperarTodasAsConexoesUseCase
{
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IConexaoReadOnlyRepositorio _repositorio;
    private readonly IMapper _mapper;
    private readonly IReceitaReadOnlyRepositorio _repositorioReceitas;

    public RecuperarTodasAsConexoesUseCase(IUsuarioLogado usuarioLogado, 
                                           IConexaoReadOnlyRepositorio repositorio, 
                                           IMapper mapper, 
                                           IReceitaReadOnlyRepositorio repositorioReceitas)
    {
        _usuarioLogado = usuarioLogado;
        _repositorio = repositorio;
        _mapper = mapper;
        _repositorioReceitas = repositorioReceitas;

    }
    public async Task<IList<ResponseUsuarioConectadoJson>> Executar()
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();
        var conexoes = await _repositorio.RecuperarDoUsuario(usuarioLogado.Id);

        var tasks = conexoes.Select(async usuario =>
        {
            var quantidadeReceitas = await _repositorioReceitas.QuantidadeDeReceitas(usuario.Id);

            var usuarioJson = _mapper.Map<ResponseUsuarioConectadoJson>(usuario);
            usuarioJson.QuantidadeReceitas = quantidadeReceitas;

            return usuarioJson;
        });

        return await Task.WhenAll(tasks);
    }
}
