using CadastroUsuario.Dados;
using CadastroUsuario.Enums;
using CadastroUsuario.Fabricas;
using CadastroUsuario.Validadores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroUsuario
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
            PopularComboBoxDeEstados();
            MontarDataGridView();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void PopularComboBoxDeEstados()
        {
            cmbEstado.DataSource = Enum.GetNames(typeof(EnumUF));
            cmbEstado.SelectedIndex = -1;
        }

        private void ValidarCamposObrigatorios()
        {
            var camposFaltando = new List<string>();

            if (string.IsNullOrEmpty(txtNome.Text.Trim()))
            {
                camposFaltando.Add("Nome");
            }
            if (string.IsNullOrEmpty(txtSobrenome.Text.Trim()))
            {
                camposFaltando.Add("Sobrenome");
            }
            if (string.IsNullOrEmpty(txtCpf.Text.Trim()))
            {
                camposFaltando.Add("CPF");
            }
            if (!txtDataDeNascimento.MaskCompleted)
            {
                camposFaltando.Add("Data de Nascimento");
            }
            if (string.IsNullOrEmpty(txtEndereco.Text.Trim()))
            {
                camposFaltando.Add("Endereço");
            }
            if (string.IsNullOrEmpty(txtNumero.Text.Trim()))
            {
                camposFaltando.Add("Número");
            }
            if (string.IsNullOrEmpty(txtComplemento.Text.Trim()))
            {
                camposFaltando.Add("Complemento");
            }
            if (string.IsNullOrEmpty(txtCidade.Text.Trim()))
            {
                camposFaltando.Add("Cidade");
            }
            if (cmbEstado.SelectedIndex == -1)
            {
                camposFaltando.Add("Estado");
            }
            if (string.IsNullOrEmpty(txtCep.Text.Trim()))
            {
                camposFaltando.Add("CEP");
            }

            if (camposFaltando?.Count > 0)
            {
                string camposFaltandoJoined = string.Join(",", camposFaltando);

                MessageBox.Show($"Os seguintes campos estão faltando o preenchimento: {camposFaltandoJoined}", 
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ValidarCPF()
        {
            if (!string.IsNullOrEmpty(txtCpf.Text.Trim()))
            {
                var valido = ValidarDocumentos.IsCpf(txtCpf.Text);
                if (!valido)
                {
                    MessageBox.Show("CPF Inválido!", "Atenção", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtSobrenome.Text = "";
            txtCpf.Text = "";
            txtDataDeNascimento.Text = "";
            txtDataDeNascimento.Mask = "##/##/####";
            txtEndereco.Text = "";
            txtNumero.Text = "";
            txtComplemento.Text = "";
            txtCidade.Text = "";
            txtCep.Text = "";
            cmbEstado.SelectedIndex = -1;
        }

        private void MontarDataGridView()
        {
            dgvUsuarios.Columns.Add("NomeCompleto", "Nome Completo");
            dgvUsuarios.Columns.Add("CPF", "CPF");
            dgvUsuarios.Columns.Add("DataDeNascimento", "Data de Nascimento");
            dgvUsuarios.Columns.Add("EnderecoCompleto", "Endereço Completo");

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarCamposObrigatorios();
                ValidarCPF();

                var usuario = FabricaDeObjetos.FabricarUsuario(txtNome.Text, txtSobrenome.Text, txtCpf.Text,
                    txtDataDeNascimento.Text, txtEndereco.Text, txtNumero.Text, txtComplemento.Text,
                    txtCidade.Text, cmbEstado.Text, txtCep.Text);

                var dadosUsuario = new DadosUsuario();
                var inseriu = dadosUsuario.Insert(usuario);

                if (inseriu > 0)
                {
                    MessageBox.Show("Cadastro inserido com sucesso!", "Atenção", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
