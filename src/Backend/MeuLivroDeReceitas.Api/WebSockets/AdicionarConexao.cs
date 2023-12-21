﻿using MeuLivroDeReceitas.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MeuLivroDeReceitas.Api.WebSockets;

[Authorize(Policy = "UsuarioLogado")]
public class AdicionarConexao : Hub
{
    private readonly Broadcaster _broadcaster;
    private readonly IGerarQRCodeUseCase _gerarQRCodeUseCase;
    private readonly IHubContext<AdicionarConexao> _hubContext;

    public AdicionarConexao(IGerarQRCodeUseCase gerarQRCodeUseCase, IHubContext<AdicionarConexao> hubContext)
    {
        _broadcaster = Broadcaster.Instance;
        _gerarQRCodeUseCase = gerarQRCodeUseCase;
        _hubContext = hubContext;
    }
    public async Task GetQRCode()
    {
        var qrCode = await _gerarQRCodeUseCase.Executar();
        _broadcaster.InicializarConexao(_hubContext, Context.ConnectionId);
        await Clients.Caller.SendAsync("ResultadoQRCode", qrCode);
    }
}
