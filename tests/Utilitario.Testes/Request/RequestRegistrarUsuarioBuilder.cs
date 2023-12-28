using Bogus;
using MeuLivroDeReceitas.Comunicacao;

namespace Utilitario.Testes;

public class RequestRegistrarUsuarioBuilder
{
    public static RequestRegistrarUsuarioJson Construir(int tamanhoSenha = 10)
    {
        return new Faker<RequestRegistrarUsuarioJson>()
                .RuleFor(u => u.Nome, f => f.Person.FullName)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Senha, f => f.Internet.Password(tamanhoSenha))
                .RuleFor(u => u.Telefone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"));
    }
}
