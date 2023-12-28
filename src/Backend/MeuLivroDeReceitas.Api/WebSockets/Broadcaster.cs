﻿using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Exeptions;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace MeuLivroDeReceitas.Api.WebSockets;

public class Broadcaster
{
    private readonly static Lazy<Broadcaster> _instance = new(() => new Broadcaster());
    public static Broadcaster Instance {  get { return _instance.Value; } }
    private ConcurrentDictionary<string, object> _dictionary { get; set; }
    
    public Broadcaster() 
    {
        _dictionary = new ConcurrentDictionary<string, object>();
    }

    public void InicializarConexao(IHubContext<AdicionarConexao> hubContext, string idUsuarioQrCodeGerado, string connectionId)
    {
        var conexao = new Conexao(hubContext, connectionId);

        _dictionary.TryAdd(connectionId, conexao);
        _dictionary.TryAdd(idUsuarioQrCodeGerado, connectionId);

        conexao.IniciarContagemTempo(CallBackTempoExpirado);
    }
    private void CallBackTempoExpirado(string connectionId)
    {
        _dictionary.TryRemove(connectionId, out _);
    }
    public string GetConnectionIdUser(string usuarioId)
    {
        if (!_dictionary.TryGetValue(usuarioId, out var connectionId))
            throw new MeuLivroDeReceitasException("");

        return connectionId.ToString();
    }
    public void ResetarTempoExpiracao(string connectionId)
    {
        _dictionary.TryGetValue(connectionId, out var objConexao);
        var conexao = objConexao as Conexao;
        conexao.ResetarContagemTempo();
    }
    public void SetConnectionIdUsuarioLeitorQrCode(string idUsuarioQueGerouQRCode, string connectionIdUsuarioLeitorQrCode)
    {
        var connectionIdUsuarioQueLeuQrCode = GetConnectionIdUser(idUsuarioQueGerouQRCode);
        _dictionary.TryGetValue(connectionIdUsuarioQueLeuQrCode, out var objConexao);
        var conexao = objConexao as Conexao;

        conexao.SetConnectionIdUsuarioLeitorQrCode(connectionIdUsuarioLeitorQrCode);
    }
    public string Remover(string connectionId, string usuarioId)
    {
        _dictionary.TryGetValue(connectionId, out var objConexao);
        var conexao = objConexao as Conexao;
        conexao.StopTimer();
        _dictionary.TryRemove(connectionId, out _);
        _dictionary.TryRemove(usuarioId, out _);

        return conexao.UsuarioLeitorQrCode();
    }
}
