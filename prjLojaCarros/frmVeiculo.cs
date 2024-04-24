using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace prjLojaCarros
{
    public partial class frmVeiculo : Form
    {
        int registroAtual = 0;
        int totalRegistros = 0;
        DataTable dtVeiculo = new DataTable();
        DataTable dtMarca = new DataTable();
        DataTable dtTipo = new DataTable();
        String connectionString = @"Server=prometheus.mssql.somee.com;Database=prometheus;User Id=Maik_Ribeiro_SQLLogin_1;Password=4fqncedyef;";

        public frmVeiculo()
        {
            InitializeComponent();
        }

        private void frmVeiculo_Load(object sender, EventArgs e)
        {

            carregaAllValues();
            btnSalvar.Enabled = false;
            txtCodVeiculo.Enabled = false;
            txtModelo.Enabled = false;
            txtAno.Enabled = false;
            cmbTipo.Enabled = false;
            cmbMarca.Enabled = false;
        }

        public void carregaAllValues()
        {
            PreencherDadosVeiculo();
            PreencherDataTableMarca();
            PreencherDataTableTipo();
            carregaComboMarca();
            carregaComboTipo();
        }

        private void PreencherDadosVeiculo()
        {
            string sql = "SELECT cvei_ano, cvei_modelo, cmar_descricao, ctip_descricao " +
                         "FROM cad_veiculo " +
                         "INNER JOIN cad_marca ON cad_veiculo.cvei_cmar_id = cad_marca.cmar_id " +
                         "INNER JOIN cad_tipo ON cad_veiculo.cvei_ctip_id = cad_tipo.ctip_id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();

                            txtAno.Text = reader["cvei_ano"].ToString();
                            txtModelo.Text = reader["cvei_modelo"].ToString();
                            cmbMarca.Text = reader["cmar_descricao"].ToString();
                            cmbTipo.Text = reader["ctip_descricao"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Não há veículos cadastrados.");
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao carregar dados do veículo: " + ex.Message);
                    }
                }
            }
        }

        private void PreencherDataTableMarca()
        {
            string sql = "SELECT cmar_id, cmar_descricao FROM cad_marca";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dtMarca);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao preencher DataTable de marca: " + ex.Message);
                    }
                }
            }
        }

        private void PreencherDataTableTipo()
        {
            string sql = "SELECT ctip_id, ctip_descricao FROM cad_tipo";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dtTipo);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao preencher DataTable de tipo: " + ex.Message);
                    }
                }
            }
        }

        private void carregaComboMarca()
        {
            cmbMarca.DataSource = dtMarca;
            cmbMarca.DisplayMember = "cmar_descricao";
            cmbMarca.ValueMember = "cmar_id";
        }
        private void carregaComboTipo()
        {
            cmbTipo.DataSource = dtTipo;
            cmbTipo.DisplayMember = "ctip_descricao";
            cmbTipo.ValueMember = "ctip_id";
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtModelo.Text = null;
            txtAno.Text = null;
            cmbMarca.Text = null;
            cmbTipo.Text = null;

            btnSalvar.Enabled = true;
            txtModelo.Enabled = true;
            txtAno.Enabled = true;
            cmbTipo.Enabled = true;
            cmbMarca.Enabled = true;

            btnAnterior.Enabled = false;
            btnProximo.Enabled = false;
            btnPrimeiro.Enabled = false;
            btnUltimo.Enabled = false;

            btnCancelar.Visible = true;
            btnCancelar.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnAtualizar.Enabled = false;
            btnSalvar.Enabled = true;
            btnSalvar.Visible = true;
            btnNovo.Enabled = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO cad_veiculo (cvei_ano, cvei_modelo, cvei_cmar_id, cvei_ctip_id) " +
                         "VALUES (@Ano, @Modelo, @MarcaID, @TipoID)";

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Ano", txtAno.Text);
            cmd.Parameters.AddWithValue("@Modelo", txtModelo.Text);
            cmd.Parameters.AddWithValue("@MarcaID", cmbMarca.SelectedValue);
            cmd.Parameters.AddWithValue("@TipoID", cmbTipo.SelectedValue);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Veículo cadastrado com sucesso");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erro ao cadastrar veículo: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            btnNovo.Enabled = true;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            txtCodVeiculo.Enabled = false;
            txtModelo.Enabled = false;
            txtAno.Enabled = false;
            cmbMarca.Enabled = false;
            cmbTipo.Enabled = false;
            btnCancelar.Enabled = false;
            btnCancelar.Visible = false;
            btnAtualizar.Enabled = false;
            btnAtualizar.Visible = false;
            btnPrimeiro.Enabled = true;
            btnUltimo.Enabled = true;
            btnAnterior.Enabled = true;
            btnProximo.Enabled = true;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnCancelar.Visible = false;
            btnCancelar.Enabled = false;

            carregaAllValues();
            btnSalvar.Enabled = false;
            txtModelo.Enabled = false;
            txtAno.Enabled = false;
            cmbTipo.Enabled = false;
            cmbMarca.Enabled = false;

            btnAnterior.Enabled = true;
            btnProximo.Enabled = true;
            btnPrimeiro.Enabled = true;
            btnUltimo.Enabled = true;

            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnAtualizar.Visible = false;
            btnAtualizar.Enabled = false;
            btnSalvar.Enabled = false;
            btnSalvar.Visible = true;
            btnNovo.Enabled = true;
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {

        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {

        }

        private void btnProximo_Click(object sender, EventArgs e)
        {

        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {

        }
    }
}
