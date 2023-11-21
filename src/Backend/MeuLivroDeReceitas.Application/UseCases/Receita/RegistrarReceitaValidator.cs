using FluentValidation;
using MeuLivroDeReceitas.Comunicacao;

namespace MeuLivroDeReceitas.Application
{
    public class RegistrarReceitaValidator : AbstractValidator<RequestRegistrarReceitaJson>
    {
        public RegistrarReceitaValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty();
            RuleFor(x => x.Categoria).NotEmpty();
            RuleFor(x => x.ModoPreparo).NotEmpty();
            RuleFor(x => x.Ingredientes).NotEmpty();

            RuleForEach(x => x.Ingredientes).ChildRules(ingrediente => 
            {
                ingrediente.RuleFor(x => x.Produto);
                ingrediente.RuleFor(x => x.Quantidade);
            });

            RuleFor(x => x.Ingredientes).Custom((ingredientes, contexto) =>
            {
                var produtosDistintos = ingredientes.Select(x => x.Produto).Distinct();
                if(produtosDistintos.Count() != ingredientes.Count())
                {
                    contexto.AddFailure(new FluentValidation.Results.ValidationFailure("Ingredientes", ""));
                }
            });
        }
    }
}
