using System;

namespace CadastroUsuario.Models
{
    public class Usuario
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }

        public string NomeCompleto 
        { 
            get 
            {
                return Nome + " " + Sobrenome;
            }
        }

        public string EnderecoCompleto
        {
            get
            {
                return Endereco + " " + Numero + "/" + Complemento + " " + Cidade + "/" + Estado + " - " + Cep;
            }
        }
    }
}
