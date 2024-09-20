using MySql.Data.MySqlClient;

namespace Projeto1Bim
{
    public class BD
    {
        public MySqlConnection CriarConexao()
        {
            string strCon = Environment.GetEnvironmentVariable("STRING_CONEXAO");
            MySqlConnection conexao = new MySqlConnection(strCon);
            return conexao;
        }
    }
}
