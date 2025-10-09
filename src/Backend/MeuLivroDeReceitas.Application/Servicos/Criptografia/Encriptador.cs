using System.Security.Cryptography;
using System.Text;

namespace MeuLivroDeReceitas.Application;

public class Encriptador
{
    private readonly string _chaveAdicional;

    public Encriptador(string chaveAdicional)
    {
        _chaveAdicional = chaveAdicional;
    }

    public string Criptografar(string senha)
    {
        var senhaComChave = $"{senha}{_chaveAdicional}";

        var bytes = Encoding.UTF8.GetBytes(senhaComChave);
        var sha512 = SHA512.Create();
        byte[] hashBytes = sha512.ComputeHash(bytes);
        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
    }
}
