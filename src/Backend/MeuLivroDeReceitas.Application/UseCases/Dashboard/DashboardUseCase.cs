using AutoMapper;
using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Domain.Extension;
using System.Linq;

namespace MeuLivroDeReceitas.Application;

public class DashboardUseCase : IDashboardUseCase
{
    private readonly IMapper _mapper;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IReceitaReadOnlyRepositorio _repositorio;
    private readonly IConexaoReadOnlyRepositorio _conexoesRepositorio;

    public DashboardUseCase(IMapper mapper, 
                            IUsuarioLogado usuarioLogado, 
                            IReceitaReadOnlyRepositorio repositorio, 
                            IConexaoReadOnlyRepositorio conexoesRepositorio)
    {
        _mapper = mapper;
        _usuarioLogado = usuarioLogado;
        _repositorio = repositorio;
        _conexoesRepositorio = conexoesRepositorio;
    }

    public async Task<ResponseDashboardJson> Executar(RequestDashboardJson request)
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();
        var receitas = await _repositorio.RecuperaReceitasUsuario(usuarioLogado.Id);
        receitas = Filtrar(request, receitas);

        //ADICIONA RECEITAS DE USUÁRIOS CONECTADOS
        var receitaUsuariosConectados = await GetReceitasUsuariosConectados(request, usuarioLogado);
        // ADICIONA A LISTA DAS MINHAS RECEITAS AS RECEITAS DOS USUÁRIOS CONECTADOS
        receitas = receitas.Concat(receitaUsuariosConectados).ToList();

        return new ResponseDashboardJson
        {
            Receitas = _mapper.Map<List<ResponseReceitaDashboardJson>>(receitas)
        };
    }

    private async Task<IList<Receita>> GetReceitasUsuariosConectados(RequestDashboardJson request, Usuario usuarioLogado)
    {
        var conexoes = await _conexoesRepositorio.RecuperarDoUsuario(usuarioLogado.Id);

        var usuariosConectados = conexoes.Select(c => c.Id).ToList();
        var receitasUsuariosConectados = await _repositorio.RecuperaReceitasTodosUsuarios(usuariosConectados);

        return receitasUsuariosConectados = Filtrar(request, receitasUsuariosConectados);
    }

    private static IList<Domain.Receita> Filtrar(RequestDashboardJson request, IList<Receita> receitas)
    {
        if(receitas is null)
            return new List<Domain.Receita>();

        var receitasFiltradas = receitas;

        if (request.Categoria.HasValue)
            receitasFiltradas = receitas.Where(r => r.Categoria == (Domain.TipoCategoria)request.Categoria.Value).ToList();
        if (!string.IsNullOrWhiteSpace(request.TituloOuIngrediente)) 
            receitasFiltradas = receitas.Where(r => r.Titulo.CompararSemConsiderarAcentoUpperCase(request.TituloOuIngrediente) || r.Ingredientes.Any(ingrediente => ingrediente.Produto.CompararSemConsiderarAcentoUpperCase(request.TituloOuIngrediente))).ToList();

        return receitasFiltradas.OrderBy(c => c.Titulo).ToList();
    }
}
