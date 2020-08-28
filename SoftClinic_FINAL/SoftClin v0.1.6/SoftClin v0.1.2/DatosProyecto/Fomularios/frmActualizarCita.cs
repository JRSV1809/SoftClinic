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
    public partial class frmActualizarCita : Form
    {
        static string CadConexion;
        public frmActualizarCita()
        {
            InitializeComponent();
            try
            {
                CadConexion = DataEstatica.GetCadenaConexion();
                SqlConnection sqlcon = new SqlConnection(CadConexion);
                sqlcon.Open();
                SqlCommand Query = new SqlCommand("SELECT Cita FROM Clinica.CitasPacientes", sqlcon);
                SqlDataReader Lector = Query.ExecuteReader();
                while (Lector.Read())
                {
                    cboCita.Items.Add(Lector.GetDecimal(0).ToString());
                }
                Lector.Close();
                sqlcon.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E, "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Cita;
            try
            {
                Cita = cboCita.Text;
                SqlConnection sqlcon = new SqlConnection(CadConexion);
                sqlcon.Open();
                SqlCommand Query = new SqlCommand("SELECT cp.Expediente, cp.CodDoc FROM Clinica.CitasPacientes AS cp WHERE cp.Cita='" + Cita + "'", sqlcon);
                SqlDataReader Lector = Query.ExecuteReader();
                if (Lector.HasRows)
                {
                    while (Lector.Read())
                    {
                        txtExpediente.Text = Lector.GetString(0);
                        txtDoctor.Text = Lector.GetString(1);
                    }
                }
                sqlcon.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E, "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnElimiar_Click(object sender, EventArgs e)
        {
            string Cita;
            try
            {
                Cita = cboCita.Text;
                SqlConnection sqlcon = new SqlConnection(CadConexion);
                sqlcon.Open();
                SqlCommand Query = new SqlCommand("DELETE FROM Clinica.CitasPacientes WHERE Cita='" + Cita + "'", sqlcon);
                Query.ExecuteNonQuery();
                sqlcon.Close();
                MessageBox.Show("Eliminado con Exito","Registro Borrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E, "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

    }
}
