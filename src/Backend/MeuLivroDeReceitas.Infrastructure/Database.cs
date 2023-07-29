using Dapper;
using MySqlConnector;

namespace MeuLivroDeReceitas.Infrastructure
{
    public static class Database
    {
        //public static void CriarDatabase(string connectionStrings, string nomeDatabase)
        //{
        //   using var minhaConexao = new MySqlConnection(connectionStrings);

        //    var param = new DynamicParameters(nomeDatabase);
        //    param.Add("nome", nomeDatabase);

        //    var registros = minhaConexao.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @nome", param);

        //    if (!registros.Any())
        //        minhaConexao.Execute($"CREATE DATABASE {nomeDatabase}");
        //}
        public static void CriarDatabase(string connectionStrings, string nomeDatabase)
        {
            using var minhaConexao = new MySqlConnection(connectionStrings);

            var param = new DynamicParameters();
            param.Add("nome", nomeDatabase);

            var registros = minhaConexao.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @nome", param);

            if (!registros.Any())
            {
                // Utilizando parâmetros nomeados para evitar SQL injection
                minhaConexao.Execute("CREATE DATABASE @nome", param);
            }
        }
    }
}
