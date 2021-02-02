using CadastroUsuario.Models;
using System;

namespace CadastroUsuario.Fabricas
{
    public static class FabricaDeObjetos
    {
        public static Usuario FabricarUsuario(string nome, string sobrenome, string cpf, string dataNascimento, string endereco,
            string numero, string complemento, string cidade, string estado, string cep)
        {
            return new Usuario()
            {
                Cep = cep.Trim(),
                Cidade = cidade.Trim(),
                Complemento = complemento.Trim(),
                Cpf = cpf.Trim(),
                DataDeNascimento = Convert.ToDateTime(dataNascimento),
                Endereco = endereco.Trim(),
                Estado = estado.Trim(),
                Nome = nome.Trim(),
                Numero = numero.Trim(),
                Sobrenome = sobrenome.Trim()
            };
        }
    }
}
