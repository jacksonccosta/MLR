﻿namespace MeuLivroDeReceitas.Domain;

public class Codigos : EntidadeBase
{
    public string Codigo { get; set; }
    public long UsuarioId { get; set; }
}
