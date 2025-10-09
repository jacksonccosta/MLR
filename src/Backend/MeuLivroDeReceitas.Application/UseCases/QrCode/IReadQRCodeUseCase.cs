using MeuLivroDeReceitas.Comunicacao;

namespace MeuLivroDeReceitas.Application;

public interface IReadQRCodeUseCase
{
    Task<(ResponseUsuarioConexaoJson usuarioToConect, string idUsuarioQrCodeGerado)> Execute(string codeConnection);
}
