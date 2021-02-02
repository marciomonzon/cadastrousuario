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
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                dCmd.Dispose();
                Dispose();
            }
        }

        public List<Usuario> ObterUsuarios()
        {
            try
            {
                SqlCommand command = new SqlCommand(@"Select Nome, Sobrenome, Cpf, DataDeNascimento, 
                Cep, Endereco, Numero, Complemento, Cidade, Estado from Usuario", objSqlCeConnection);

                Open();

                var usuarios = new List<Usuario>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuarios.Add(new Usuario()
                        {
                            Nome = reader["Nome"].ToString(),
                            Sobrenome = reader["Sobrenome"].ToString(),
                            Cpf = reader["Cpf"].ToString(),
                            Endereco = reader["Endereco"].ToString(),
                            Numero = reader["Numero"].ToString(),
                            Complemento = reader["Complemento"].ToString(),
                            DataDeNascimento = Convert.ToDateTime(reader["DataDeNascimento"]),
                            Cep = reader["Cep"].ToString(),
                            Cidade = reader["Cidade"].ToString(),
                            Estado = reader["Estado"].ToString()
                        });
                    }
                }

                return usuarios;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public bool VerificarSeExisteUsuarioPorCpf(string cpf)
        {
            try
            {
                SqlCommand command = new SqlCommand(@"Select Cpf from Usuario where Cpf = @Cpf", objSqlCeConnection);
                command.Parameters.AddWithValue("@Cpf", cpf);
                Open();

                var existe = false;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        existe = true;
                    }
                }

                return existe;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Dispose();
            }
        }
    }
}
