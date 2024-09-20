using Projeto1Bim.Domain;
using MySql.Data.MySqlClient;

namespace Projeto1Bim.Service
{
    public class CartaoService
    {
        private readonly ILogger<CartaoService> _logger;
        private readonly BD _bd;

        /// <summary>
        ///•	Este endpoint recebe o número do cartão de crédito e retorna sua bandeira (VISA, MASTERCARD, ELO...) de acordo com a regra de negócio fictícia dada a seguir, que considera os primeiros 4 dígitos do número e o 8º do cartão (BIN): 
        /// o	1111-XXX-X1X-XXX: VISA
        /// o	2222-XXX-X2X-XXX: MASTERCARD
        /// o	3333-XXX-X3X-XXX: ELO
        /// <param name="cartao"></param>
        /// <returns>
        /// Cartão ou Null.
        /// </returns>
        public string ObterBandeira(string cartao)
        {

            Cartao _cartao = null;

            MySqlConnection conexao = _bd.CriarConexao();

            conexao.Open();

            MySqlCommand cmd = conexao.CreateCommand();

            cmd.CommandText = @$"select * 
                                 from Cartao 
                                 where Numero = {cartao}";

            var dr = cmd.ExecuteReader();

            _cartao = Map(dr).FirstOrDefault();
            string Bandeira = "";

            if (_cartao.Numero[8] == '1')
            {
                Bandeira = "VISA";
            }
            else if (_cartao.Numero[8] == '2')
            {
                Bandeira = "MASTERCARD";
            }
            else if (_cartao.Numero[8] == '3')
            {
                Bandeira = "ELO";
            }
            else
            {
                Bandeira = "NotFound"; 
            }

            conexao.Close();

            return Bandeira;

        }

        /// <summary>
        /// •	Este endpoint recebe o número do cartão de crédito e retorna um valor booleano indicando se o cartão é válido.
        ///     Isso deve ser feito verificando sua existência e validade do cartão na tabela "CARTAO".
        /// </summary>
        /// <param name="cartao"></param>
        /// <returns>Cartão ou Null.</returns>
        public Domain.Cartao Valido(string cartao)
        {

            Cartao _cartao = null;

            MySqlConnection conexao = _bd.CriarConexao();

            conexao.Open();

            MySqlCommand cmd = conexao.CreateCommand();

            cmd.CommandText = @$"SELECT *
                                FROM Cartao
                                WHERE Numero = '{cartao}' AND Validade >= CURDATE();";

            var dr = cmd.ExecuteReader();

            _cartao = Map(dr).FirstOrDefault();

            conexao.Close();

            return _cartao;

        }



        private List<Domain.Cartao> Map(MySqlDataReader dr)
        {

            List<Domain.Cartao> alunos = new List<Domain.Cartao>();
            while (dr.Read())
            {
                var cartao = new Cartao();
                cartao.Numero = dr["Numero"].ToString();
                cartao.Validade = Convert.ToDateTime(dr["Validade"]);

                alunos.Add(cartao);
            }

            return alunos;

        }
    }
}
