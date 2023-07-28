using Bogus;
using MeuLivroDeReceitas.Domain;

namespace Utilitario.Testes;

public class UsuarioBuilder
{
    public static (Usuario usuario, string senha) Contruir()
    {
        string senha = string.Empty;

        var usuario =  new Faker<Usuario>()
                .RuleFor(u => u.Id, _ => 1)
                .RuleFor(u => u.Nome, f => f.Person.FullName)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Senha, f =>
                {
                    senha = f.Internet.Password();

                    return EncriptadorDeSenhaBuilder.Instancia().Criptografar(senha);
                })
                .RuleFor(u => u.Telefone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"));
       
        return (usuario, senha);
    }
}
