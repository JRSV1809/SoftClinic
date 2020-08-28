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
    public partial class frmHistoriaDetalleCita : Form
    {
        static string CadConexion;
        public frmHistoriaDetalleCita()
        {
            InitializeComponent();
            CadConexion = DataEstatica.GetCadenaConexion();
            SqlConnection sqlcon = new SqlConnection(CadConexion);
            sqlcon.Open();
            SqlCommand Query = new SqlCommand("SELECT d.CodDoc FROM Clinica.Doctores AS d", sqlcon);
            SqlDataReader Lector = Query.ExecuteReader();
            while (Lector.Read())
            {
                cboDoctor.Items.Add(Lector.GetString(0));
            }
            Lector.NextResult();
            Lector.Close();
            sqlcon.Close();
        }

        private void btnObtenerCitas_Click(object sender, EventArgs e)
        {
            string CodDoctor, FechaCitas;
            try
            {
                CodDoctor = cboDoctor.Text;
                FechaCitas = dateTimePicker1.Value.ToShortDateString();
                SqlConnection sqlcon = new SqlConnection(CadConexion);
                sqlcon.Open();
                SqlCommand Query = new SqlCommand("SELECT hcp.Cita FROM   Clinica.HisCitasPacientes     AS hcp WHERE hcp.CodDoc='" + CodDoctor + "' AND hcp.FechaCita>='" + FechaCitas + "' ORDER BY hcp.FechaCita ASC ", sqlcon);
                SqlDataReader Lector = Query.ExecuteReader();
                while (Lector.Read())
                {
                    cboCita.Items.Add(Lector.GetDecimal(0)).ToString();
                }
                Lector.NextResult();
                Lector.Close();
                sqlcon.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E, "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnObtenerDetalle_Click(object sender, EventArgs e)
        {
            string CodDoctor, FechaCitas, Cita;
            CodDoctor = cboDoctor.Text;
            FechaCitas = dateTimePicker1.Value.ToShortDateString();
            Cita = cboCita.Text;
            try
            {
                SqlConnection sqlcon = new SqlConnection(CadConexion);
                sqlcon.Open();
                SqlCommand Query = new SqlCommand("SELECT hcp.Cita,       hcp.Expediente,       p.Nombre,       p.FechaNacimiento,       p.Nacionalidad,       p.EstadCivil,       p.Sexo,hcp.FechaCita ,       hcp.Observacion1,       hcp.Observacion2,       hcp.Observacion3 FROM   Clinica.HisCitasPacientes     AS hcp       INNER JOIN Clinica.Pacientes  AS p            ON  hcp.Expediente = p.Expediente WHERE  hcp.Cita = '" + Cita + "'       AND hcp.CodDoc = '" + CodDoctor + "'       AND hcp.FechaCita >= '" + FechaCitas + "'", sqlcon);
                SqlDataReader Lector = Query.ExecuteReader();
                while (Lector.Read())
                {
                    txtCita.Text = Cita;
                    txtExpediente.Text = Lector.GetString(1);
                    txtNombre.Text = Lector.GetString(2);
                    txtFechaNacimiento.Text = Lector.GetDateTime(3).ToShortDateString();
                    txtNacionalidad.Text = Lector.GetString(4);
                    txtEstadoCivil.Text = Lector.GetString(5);
                    txtSexo.Text = Lector.GetString(6);
                    txtFechaCita.Text = Lector.GetDateTime(7).ToString();
                    txtDetalle.Text = Lector.GetString(8);
                    txtObservaciones.Text=Lector.GetString(9);
                    txtDiagnostico.Text = Lector.GetString(10);
                }
                Lector.Close();
                sqlcon.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E, "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
