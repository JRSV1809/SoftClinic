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
    public partial class frmModificarEspecialidad : Form
    {
        static string CadConexion;
        public frmModificarEspecialidad()
        {
            InitializeComponent();
            CadConexion = DataEstatica.GetCadenaConexion();
            SqlConnection sqlcon = new SqlConnection(CadConexion);
            sqlcon.Open();
            SqlCommand Query = new SqlCommand("SELECT de.CodEsp FROM Clinica.DoctoresEspecializaciones AS de", sqlcon);
            SqlDataReader Lector = Query.ExecuteReader();
            while (Lector.Read())
            {
                cboEspecialidad.Items.Add(Lector.GetString(0));
            }
            Lector.NextResult();
            Lector.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string Nombre,CodEsp;
            SqlConnection sqlcon = new SqlConnection(CadConexion);
            sqlcon.Open();
            SqlTransaction sqltran;
            sqltran = sqlcon.BeginTransaction();
            try
            {
                Nombre = txtNombre.Text;
                CodEsp=cboEspecialidad.Text;
                SqlCommand Query = new SqlCommand("UPDATE Clinica.DoctoresEspecializaciones       SET 	Descripcion = '"+Nombre+"',       	FechaModificacion = GETDATE(),       	UsuarioModificacion = CURRENT_USER       WHERE CodEsp='"+CodEsp+"'", sqlcon);
                Query.Transaction = sqltran;
                Query.ExecuteNonQuery();
                MessageBox.Show("Acualizacion Exitosa", "Acualizacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sqltran.Commit();
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sqltran.Rollback();
            }
            sqlcon.Close();
        }
    }
}
