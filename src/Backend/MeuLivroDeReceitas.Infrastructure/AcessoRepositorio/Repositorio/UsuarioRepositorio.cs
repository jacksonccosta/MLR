﻿using MeuLivroDeReceitas.Domain;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infrastructure;

public class UsuarioRepositorio : IUsuarioWriteOnlyRepositorio, IUsuarioReadOnlyRepositorio, IUsuarioUpdateOnlyRepositorio
{
    private readonly MeuLivroDeReceitaContext _contexto;

    public UsuarioRepositorio(MeuLivroDeReceitaContext contexto)
    {
        _contexto = contexto;
    }

    public async Task Adicionar(Usuario usuario)
    {
        await _contexto.Usuarios.AddAsync(usuario);
    }

    public async Task<bool> ExisteUsuarioComEmail(string email)
    {
        return await _contexto.Usuarios.AnyAsync(u => u.Email.Equals(email));
    }

    public async Task<Usuario> Login(string email, string senha)
    {
        return await _contexto.Usuarios.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Senha.Equals(senha));
    }

    public async Task<Usuario> RecuperarPorEmail(string email)
    {
        return await _contexto.Usuarios.AsNoTracking()
           .FirstOrDefaultAsync(u => u.Email.Equals(email));
    }

    public async Task<Usuario> RecuperarPorId(long id)
    {
        return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
    }

    public void Update(Usuario usuario)
    {
        _contexto.Usuarios.Update(usuario);
    }
}
