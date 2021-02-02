using CadastroUsuario.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroUsuario.Dados
{
    public class DadosUsuario
    {
        private static SqlConnection objSqlCeConnection = null;

        public DadosUsuario()
        {
            string connString2 = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Banco.mdf;Integrated Security=True";
            objSqlCeConnection = new SqlConnection(connString2);
        }

        public void Open()
        {
            try
            {
                if (objSqlCeConnection.State == ConnectionState.Closed)
                {
                    objSqlCeConnection.Open();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Dispose()
        {
            try
            {
                if (objSqlCeConnection.State != ConnectionState.Closed)
                {
                    objSqlCeConnection.Close();
                    objSqlCeConnection.Dispose();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int Insert(Usuario usuario)
        {

            string sql = @"insert into Usuario(Nome, Sobrenome, Cpf, DataDeNascimento, 
            Cep, Endereco, Numero, Complemento, Cidade, Estado) values
            (@nome, @sobrenome, @cpf, @data, @cep, @endereco, @numero, @complemento, 
            @cidade, @estado)";

            Open();
            SqlCommand dCmd = new SqlCommand(sql, objSqlCeConnection);
            dCmd.CommandType = CommandType.Text;
            try
            {
                dCmd.Parameters.AddWithValue("@nome", usuario.Nome);
                dCmd.Parameters.AddWithValue("@sobrenome", usuario.Sobrenome);
                dCmd.Parameters.AddWithValue("@cpf", usuario.Cpf);
                dCmd.Parameters.AddWithValue("@data", usuario.DataDeNascimento);
                dCmd.Parameters.AddWithValue("@cep", usuario.Cep);
                dCmd.Parameters.AddWithValue("@endereco", usuario.Endereco);
                dCmd.Parameters.AddWithValue("@numero", usuario.Numero);
                dCmd.Parameters.AddWithValue("@complemento", usuario.Complemento);
                dCmd.Parameters.AddWithValue("@cidade", usuario.Cidade);
                dCmd.Parameters.AddWithValue("@estado", usuario.Estado);

                var retorno = dCmd.ExecuteNonQuery();
                return retorno;
            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                dCmd.Dispose();
                Dispose();
            }
        }
    }
}
