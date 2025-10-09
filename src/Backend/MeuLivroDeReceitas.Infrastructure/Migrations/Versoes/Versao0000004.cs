using FluentMigrator;

namespace MeuLivroDeReceitas.Infrastructure.Migrations.Versoes;

[Migration((long)NumVersoes.CriarTabelasAssociacaoUsuario, "Tabela para associação de usuários")]
public class Versao0000004 : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        CriarTabelaCodigos();
        CriarTabelaConexao();
    }

    private void CriarTabelaCodigos()
    {
        var tabela = VersaoBase.InserirColunasPadrao(Create.Table("Codigos"));

        tabela
            .WithColumn("Codigo").AsString(2000).NotNullable()
            .WithColumn("UsuarioId").AsInt64().NotNullable().ForeignKey("FK_Codigo_Usuario_Id", "Usuarios", "Id");
    }
    private void CriarTabelaConexao()
    {
        var tabela = VersaoBase.InserirColunasPadrao(Create.Table("Conexoes"));

        tabela
            .WithColumn("UsuarioId").AsInt64().NotNullable().ForeignKey("FK_Conexao_Usuario_Id", "Usuarios", "Id")
            .WithColumn("ConexaoUsuarioId").AsInt64().NotNullable().ForeignKey("FK_Conexao_ConexaoUsuario_Id", "Usuarios", "Id");
    }
}