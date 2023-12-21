﻿using Microsoft.AspNetCore.SignalR;
using System.Timers;

namespace MeuLivroDeReceitas.Api.WebSockets;

public class Conexao
{
    private readonly IHubContext<AdicionarConexao> _hubContext;
    private readonly string UsuarioQueCriouQRCodeConnectionId;
    private Action<string> _callBackTempoExpirado;

    public Conexao(IHubContext<AdicionarConexao> hubContext, string usuarioQueCriouQRCodeConnectionId)
    {
        _hubContext = hubContext;
        UsuarioQueCriouQRCodeConnectionId = usuarioQueCriouQRCodeConnectionId;

    }

    public short tempoRestanteSegundos { get; set; }
    public System.Timers.Timer _timer { get; set; }

    public void IniciarContagemTempo(Action<string> callBackTempoExpirado)
    {
        _callBackTempoExpirado = callBackTempoExpirado;
        tempoRestanteSegundos = 60;
        _timer = new System.Timers.Timer(1000)
        {
            Enabled = false
        };
        _timer.Elapsed += ElapsedTimer;
        _timer.Enabled = true;
    }
    public void StopTimer()
    {
        _timer?.Stop();
        _timer?.Dispose();
        _timer = null;
    }

    private async void ElapsedTimer(object sender, ElapsedEventArgs e)
    {
        if (tempoRestanteSegundos >= 0)
            await _hubContext.Clients.Client(UsuarioQueCriouQRCodeConnectionId).SendAsync("SetTempoRestante", tempoRestanteSegundos--);
        else
        {
            StopTimer();
            _callBackTempoExpirado(UsuarioQueCriouQRCodeConnectionId);
        }
    }
}
