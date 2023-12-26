namespace MeuLivroDeReceitas.Application;

public interface IGerarQRCodeUseCase
{
    Task<(string qrCode, string idUsuario)> Executar();
}
