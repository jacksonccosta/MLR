using FluentMigrator.Builders.Create.Table;

namespace MeuLivroDeReceitas.Infrastructure
{
    public static class VersaoBase
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax InserirColunasPadrao(ICreateTableWithColumnOrSchemaOrDescriptionSyntax tabela)
        {
           return tabela.WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("DataCriacao").AsDateTime().NotNullable()
                  .WithColumn("DataModificacao").AsDateTime().NotNullable()
                  .WithColumn("indAtivo").AsBoolean().NotNullable();
        }
    }
}
