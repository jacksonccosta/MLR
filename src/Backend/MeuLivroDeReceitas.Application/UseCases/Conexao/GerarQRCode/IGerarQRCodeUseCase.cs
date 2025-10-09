namespace MeuLivroDeReceitas.Application;

public interface IGerarQRCodeUseCase
{
    Task<(byte[] qrCode, string idUsuario)> Executar();
}
