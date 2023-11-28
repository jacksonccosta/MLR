using AutoMapper;
using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Domain;
using System.Security.AccessControl;

namespace MeuLivroDeReceitas.Application;

public class DashboardUseCase : IDashboardUseCase
{
    private readonly IMapper _mapper;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IReceitaReadOnlyRepositorio _repositorio;

    public DashboardUseCase(IMapper mapper, IUsuarioLogado usuarioLogado, IReceitaReadOnlyRepositorio repositorio)
    {
        _mapper = mapper;
        _usuarioLogado = usuarioLogado;
        _repositorio = repositorio;
    }

    public async Task<ResponseDashboardJson> Executar(RequestDashboardJson request)
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();
        var receitas = await _repositorio.RecuperaReceitasUsuario(usuarioLogado.Id);
        receitas = (List<Receita>)Filtrar(request, receitas);

        return new ResponseDashboardJson
        {
            Receitas = _mapper.Map<List<ResponseReceitaDashboardJson>>(receitas)
        };

    }

    private static IList<Receita> Filtrar(RequestDashboardJson request, IList<Receita> receitas)
    {
        var receitasFiltradas = receitas;

        if (request.Categoria.HasValue)
            receitasFiltradas = receitas.Where(r => r.Categoria == (Domain.TipoCategoria)request.Categoria.Value).ToList();
        if (!string.IsNullOrWhiteSpace(request.TituloOuIngrediente)) 
            receitasFiltradas = receitas.Where(r => r.Titulo.Contains(request.TituloOuIngrediente) || r.Ingredientes.Any(ingrediente => ingrediente.Produto.Contains(request.TituloOuIngrediente))).ToList();

        return receitasFiltradas;
    }
}
