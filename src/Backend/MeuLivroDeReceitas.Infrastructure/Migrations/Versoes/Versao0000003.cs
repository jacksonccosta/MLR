using FluentMigrator;

namespace MeuLivroDeReceitas.Infrastructure.Migrations.Versoes;

[Migration((long)NumVersoes.AlterarTabelaReceitas, "Adicionar coluna tempo de preparo em receitas")]
public class Versao0000003 : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        Alter.Table("receitas").AddColumn("TempoPreparo").AsInt32().NotNullable().WithDefaultValue(0);
    }
}
