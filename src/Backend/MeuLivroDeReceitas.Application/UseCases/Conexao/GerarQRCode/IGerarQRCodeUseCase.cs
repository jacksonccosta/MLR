namespace MeuLivroDeReceitas.Application;

public interface IGerarQRCodeUseCase
{
    Task<string> Executar();
}
