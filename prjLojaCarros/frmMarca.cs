using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjLojaCarros
{
    public partial class frmMarca : Form
    {
        int registroAtual = 0;
        int totalRegistros = 0;
        DataTable dtMarca = new DataTable();
        String connectionString = @"Server=prometheus.mssql.somee.com ;Database=prometheus;User Id=Maik_Ribeiro_SQLLogin_1;Password=4fqncedyef;";
        public frmMarca()
        {
            InitializeComponent();
        }
        private void navegar()
        {
            txtCodMarca.Text = dtMarca.Rows[registroAtual][0].ToString();
            txtMarca.Text = dtMarca.Rows[registroAtual][1].ToString();
        }

        private void carregar()
        {
            dtMarca = new DataTable();
            string sql = "SELECT * FROM cad_marca";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtMarca.Load(reader);
                    totalRegistros = dtMarca.Rows.Count;
                    registroAtual = 0;
                    navegar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void frmMarca_Load(object sender, EventArgs e)
        {
            carregar();
            btnSalvar.Enabled = false;
            txtCodMarca.Enabled = false;
            txtMarca.Enabled = false;
            string sql = "SELECT * FROM cad_marca";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtMarca.Load(reader);
                    totalRegistros = dtMarca.Rows.Count;
                    registroAtual = 0;
                    navegar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = true;
            txtCodMarca.Enabled = false;
            txtMarca.Enabled = true;
            txtMarca.Text = "";
            btnAnterior.Enabled = false;
            btnProximo.Enabled = false;
            btnPrimeiro.Enabled = false;
            btnUltimo.Enabled = false;
            btnCancelar.Enabled = true;
            btnCancelar.Visible = true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO cad_marca (cmar_descricao) " +
                         "VALUES " +
                         "('" + txtMarca.Text + "')";
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Marca cadastrada com sucesso");
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erro ao cadastrar marca: " + ex);

            }
            finally
            {
                conn.Close();
            }

            btnNovo.Enabled = true;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            txtCodMarca.Enabled = false;
            txtMarca.Enabled = false;
            btnCancelar.Enabled = false;
            btnCancelar.Visible = false;
            btnAtualizar.Enabled = false;
            btnAtualizar.Visible = false;
            btnPrimeiro.Enabled = true;
            btnUltimo.Enabled = true;
            btnAnterior.Enabled = true;
            btnProximo.Enabled = true;
            carregar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = false;
            txtCodMarca.Enabled = false;
            txtMarca.Enabled = true;
            btnAtualizar.Enabled = true;
            btnAtualizar.Visible = true;
            btnCancelar.Enabled = true;
            btnCancelar.Visible = true;
            btnAnterior.Enabled = false;
            btnProximo.Enabled = false;
            btnPrimeiro.Enabled = false;
            btnUltimo.Enabled = false;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE cad_marca SET cmar_descricao = ('" + txtMarca.Text + "') WHERE cmar_id = " + txtCodMarca.Text;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Marca atualizada com sucesso");
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erro ao atualizar marca: " + ex);

            }
            finally
            {
                conn.Close();
            }

            btnNovo.Enabled = true;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            txtCodMarca.Enabled = false;
            txtMarca.Enabled = false;
            btnCancelar.Enabled = false;
            btnCancelar.Visible = false;
            btnAtualizar.Enabled = false;
            btnAtualizar.Visible = false;
            btnPrimeiro.Enabled = true;
            btnUltimo.Enabled = true;
            btnAnterior.Enabled = true;
            btnProximo.Enabled = true;
            carregar();
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (registroAtual < totalRegistros - 1)
            {
                registroAtual++;
                navegar();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (registroAtual > 0)
            {
                registroAtual--;
                navegar();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (registroAtual < totalRegistros - 1)
            {
                registroAtual = totalRegistros - 1;
                navegar();
            }
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (registroAtual > 0)
            {
                registroAtual = 0;
                navegar();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DialogResult confirma = MessageBox.Show("Deseja excluir essa marca?", "Excluir Marca?", MessageBoxButtons.YesNo);
            if (confirma == DialogResult.Yes)
            {
                string sql = "DELETE FROM cad_marca WHERE cmar_id = " + txtCodMarca.Text;
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Marca deletada com sucesso");
                    }

                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Erro ao deletar marca: " + ex);

                }
                finally
                {
                    conn.Close();
                }
                carregar();
            }
            else
            {
                carregar();
            }


            btnNovo.Enabled = true;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            txtCodMarca.Enabled = false;
            txtMarca.Enabled = false;
            btnAtualizar.Enabled = false;
            btnAtualizar.Visible = false;
            carregar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = true;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            txtCodMarca.Enabled = false;
            txtMarca.Enabled = false;
            btnAtualizar.Enabled = false;
            btnAtualizar.Visible = false;
            btnCancelar.Enabled = false;
            btnCancelar.Visible = false;
            btnAnterior.Enabled = true;
            btnProximo.Enabled = true;
            btnPrimeiro.Enabled = true;
            btnUltimo.Enabled = true;
        }
    }
}
