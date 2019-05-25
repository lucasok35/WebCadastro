using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Data
{
    public class PessoaDB
    {

        private Conexao conexao;

        public bool Insert(Pessoa pessoa)
        {

            try
            {
                //Query de inclusão de dados
                string sql = string.Format("insert into TB_Pessoa values ('{0}', '{1}')",
                    pessoa.Nome, pessoa.Telefone);

                //Abrir conexão para inclusão das informações
                using (conexao = new Conexao())
                {
                    conexao.ExecutaComando(sql);
                }


                return true;
            }
            catch (Exception)
            {

                //return false;
                throw;
            }
            
        }

        public List<Pessoa> All()
        {
            using (conexao = new Conexao())
            {
                var sql = "SELECT Id, Nome, Telefone FROM TB_PESSOA";
                var retorno = conexao.ExecutaComandoRetorno(sql);
                return TransformaSQLReaderEmList(retorno);
            }
        }

        private List<Pessoa> TransformaSQLReaderEmList(SqlDataReader retorno)
        {
            var listPessoa = new List<Pessoa>();

            while (retorno.Read())
            {
                var item = new Pessoa()
                {
                    Id = int.Parse(retorno["Id"].ToString()),
                    Nome = retorno["Nome"].ToString(),
                    Telefone = retorno["Telefone"].ToString(),

                };

                listPessoa.Add(item);
            }
            return listPessoa;
        }
    }


}

