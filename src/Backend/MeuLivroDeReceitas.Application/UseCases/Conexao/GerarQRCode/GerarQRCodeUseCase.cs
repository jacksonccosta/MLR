﻿using HashidsNet;
using MeuLivroDeReceitas.Domain;
using QRCoder;
using System.Drawing;

namespace MeuLivroDeReceitas.Application;

public class GerarQRCodeUseCase : IGerarQRCodeUseCase
{
    private readonly ICodigoWriteOnlyRepositorio _repositorio;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly IHashids _hashIds;

    public GerarQRCodeUseCase(ICodigoWriteOnlyRepositorio repositorio, IUsuarioLogado usuarioLogado, IUnidadeDeTrabalho unidadeDeTrabalho, IHashids hashids)
    {
        _repositorio = repositorio;
        _usuarioLogado = usuarioLogado;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _hashIds = hashids;
    }
    public async Task<(byte[] qrCode, string idUsuario)> Executar()
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();
        var codigo = new Codigos
        {
            Codigo = Guid.NewGuid().ToString(),
            UsuarioId = usuarioLogado.Id
        };
        await _repositorio.Registrar(codigo);
        await _unidadeDeTrabalho.Commit();

        return (GerarImagemQRCode(codigo.Codigo), _hashIds.EncodeLong(usuarioLogado.Id));
    }

    private static byte[] GerarImagemQRCode(string codigo)
    {
        var qrCodeGenerator = new QRCodeGenerator();
        var qrCodeData = qrCodeGenerator.CreateQrCode(codigo, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new QRCode(qrCodeData);
        var bitmap =  qrCode.GetGraphic(5, Color.Black, Color.Transparent, true);

        using var stream = new MemoryStream();
        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        return stream.ToArray();
    }
}
