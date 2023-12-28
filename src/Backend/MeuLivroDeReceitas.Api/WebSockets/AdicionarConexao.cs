using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Exeptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MeuLivroDeReceitas.Api.WebSockets;

[Authorize(Policy = "UsuarioLogado")]
public class AdicionarConexao : Hub
{
    private readonly Broadcaster _broadcaster;

    private readonly IRecusarConexaoUseCase _recusarConexaoUseCase;
    private readonly IAceitarConexaoUseCase _aceitarConexaoUseCase;
    private readonly IReadQRCodeUseCase _qrCodeLidoUseCase;
    private readonly IGerarQRCodeUseCase _gerarQRCodeUseCase;
    private readonly IHubContext<AdicionarConexao> _hubContext;

    public AdicionarConexao(IGerarQRCodeUseCase gerarQRCodeUseCase, 
                            IHubContext<AdicionarConexao> hubContext, 
                            IReadQRCodeUseCase qrCodeLidoUseCase, 
                            IRecusarConexaoUseCase recusarConexaoUseCase,
                            IAceitarConexaoUseCase aceitarConexaoUseCase)
    {
        _broadcaster = Broadcaster.Instance;
        _gerarQRCodeUseCase = gerarQRCodeUseCase;
        _hubContext = hubContext;
        _qrCodeLidoUseCase = qrCodeLidoUseCase;
        _recusarConexaoUseCase = recusarConexaoUseCase;
        _aceitarConexaoUseCase = aceitarConexaoUseCase;
    }
    
    public async Task GetQRCode()
    {        
        try
        {
            (var qrCode, var idUsuario) = await _gerarQRCodeUseCase.Executar();
            _broadcaster.InicializarConexao(_hubContext, idUsuario, Context.ConnectionId);
            await Clients.Caller.SendAsync("ResultadoQRCode", qrCode);
        }
        catch (MeuLivroDeReceitasException ex)
        {
            await Clients.Caller.SendAsync("Erro", ex.Message);
        }
        catch
        {
            await Clients.Caller.SendAsync("Erro", ResourceMensagensDeErro.ERRO_DESCONHECIDO);
        }
    }
    public async Task ReadQRCode(string codeConnection)
    {
        try
        {
            (var usuarioToConect, var idUsuarioQrCodeGerado) = await _qrCodeLidoUseCase.Execute(codeConnection);
            
            var connectionId = _broadcaster.GetConnectionIdUser(idUsuarioQrCodeGerado);

            _broadcaster.ResetarTempoExpiracao(connectionId);
            _broadcaster.SetConnectionIdUsuarioLeitorQrCode(idUsuarioQrCodeGerado, Context.ConnectionId);

            await Clients.Client(connectionId).SendAsync("ResultadoQRCode", usuarioToConect);
        }
        catch (MeuLivroDeReceitasException ex)
        {
            await Clients.Caller.SendAsync("Erro", ex.Message);
        }
        catch 
        {
            await Clients.Caller.SendAsync("Erro", ResourceMensagensDeErro.ERRO_DESCONHECIDO);
        }
    }
    public async Task RecusarConexao()
    {
        try
        {
            var connectionIdUsuarioQrCodeGerado = Context.ConnectionId;
            var usuarioId = await _recusarConexaoUseCase.Executar();
            var connectionIdUsuarioQueLeuQRCode = _broadcaster.Remover(connectionIdUsuarioQrCodeGerado, usuarioId);
            await Clients.Client(connectionIdUsuarioQueLeuQRCode).SendAsync("OnConexaoRecusada");
        }
        catch (MeuLivroDeReceitasException ex)
        {
            await Clients.Caller.SendAsync("Erro", ex.Message);
        }
        catch
        {
            await Clients.Caller.SendAsync("Erro", ResourceMensagensDeErro.ERRO_DESCONHECIDO);
        }
    }
    public async Task AceitarConexao(string idUsuarioParaSeConectar)
    {
        try
        {
            var usuarioId = await _aceitarConexaoUseCase.Executar(idUsuarioParaSeConectar);
            var connectionIdUsuarioQrCodeGerado = Context.ConnectionId;
            var connectionIdUsuarioQueLeuQRCode = _broadcaster.Remover(connectionIdUsuarioQrCodeGerado, usuarioId);
            await Clients.Client(connectionIdUsuarioQueLeuQRCode).SendAsync("OnConexaoAceita");
        }
        catch (MeuLivroDeReceitasException ex)
        {
            await Clients.Caller.SendAsync("Erro", ex.Message);
        }
        catch
        {
            await Clients.Caller.SendAsync("Erro", ResourceMensagensDeErro.ERRO_DESCONHECIDO);
        }
    }
}
