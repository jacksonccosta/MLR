using Microsoft.Extensions.Configuration;

namespace MeuLivroDeReceitas.Domain
{
    public static class RepositorioExtension
    {
        public static string GetNomeDatabase(this IConfiguration configurationManager)
        {
            var database = configurationManager.GetConnectionString("NomeDataBase");
            return database;
        }

        public static string GetConexao(this IConfiguration configurationManager)
        {
            var conn = configurationManager.GetConnectionString("Conexao");
            return conn;
        }

        public static string GetConexaoCompleta(this IConfiguration configurationManager)
        {
            var nomeDatabase = configurationManager.GetNomeDatabase();
            var conexao = configurationManager.GetConexao();

            return $"{conexao}Database={nomeDatabase}";
        }
    }
}
