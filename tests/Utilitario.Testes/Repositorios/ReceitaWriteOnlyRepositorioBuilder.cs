﻿using MeuLivroDeReceitas.Domain;
using Moq;

namespace Utilitario.Testes.Repositorios;

public class ReceitaWriteOnlyRepositorioBuilder
{
    private static ReceitaWriteOnlyRepositorioBuilder _instance;
    private readonly Mock<IReceitaWriteOnlyRepositorio> _repositorio;

    private ReceitaWriteOnlyRepositorioBuilder()
    {
        if (_repositorio is null)
        {
            _repositorio = new Mock<IReceitaWriteOnlyRepositorio>();
        }
    }

    public static ReceitaWriteOnlyRepositorioBuilder Instancia()
    {
        _instance = new ReceitaWriteOnlyRepositorioBuilder();
        return _instance;
    }

    public IReceitaWriteOnlyRepositorio Construir()
    {
        return _repositorio.Object;
    }
}
