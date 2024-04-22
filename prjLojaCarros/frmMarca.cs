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
        String connectionString = @"Server=prometheus.mssql.somee.com ;Database=prometheus;User Id=Maik_Ribeiro_SQLLogin_1;Password=4fqncedyef;";
        public frmMarca()
        {
            InitializeComponent();
        }

        private void frmMarca_Load(object sender, EventArgs e)
        {
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
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtCodMarca.Text = reader[0].ToString();
                    txtMarca.Text = reader[1].ToString();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erro ao listar Marcas: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
            txtCodMarca.Enabled = false;
            txtMarca.Enabled = true;
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
        }
    }
}
