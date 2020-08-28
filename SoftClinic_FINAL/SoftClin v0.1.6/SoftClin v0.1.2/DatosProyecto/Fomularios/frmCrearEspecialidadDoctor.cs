using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SoftClin_v0._1._2
{
    public partial class frmCrearEspecialidadDoctor : Form
    {
        static string CadConexion;
        public frmCrearEspecialidadDoctor()
        {
            InitializeComponent();
            CadConexion = DataEstatica.GetCadenaConexion();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            String CodEspe, Nombre;
            SqlConnection sqlcon = new SqlConnection(CadConexion);
            SqlTransaction sqltran;
            sqlcon.Open();
            sqltran = sqlcon.BeginTransaction();
            try
            {
                CodEspe = txtCodEspecializacion.Text;
                Nombre = txtNombre.Text;
                SqlCommand Query = new SqlCommand("INSERT INTO Clinica.DoctoresEspecializaciones       (       	CodEsp,       	Descripcion,       	FechaGrabacion,       	UsuarioGrabacion,       	FechaModificacion,       	UsuarioModificacion       )       VALUES       (       	'" + CodEspe + "',       	'" + Nombre + "',       	GETDATE(),       	CURRENT_USER,       	GETDATE(),       	CURRENT_USER       )", sqlcon);
                Query.Transaction = sqltran;
                Query.ExecuteNonQuery();
                sqltran.Commit();
                MessageBox.Show("Ejecuta Correctamente", "Ejecucion Correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E, "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sqltran.Rollback();
            }
            sqlcon.Close();
        }
    }
}
