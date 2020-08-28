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
    public partial class frmDoctoresCitas : Form
    {
        string CadConexion;
        public frmDoctoresCitas()
        {
            InitializeComponent();
            try
            {
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
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E, "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                SqlCommand Query = new SqlCommand("SELECT cp.Cita FROM   Clinica.CitasPacientes        AS cp WHERE cp.CodDoc='"+CodDoctor+"' AND cp.FechaCita>='"+FechaCitas+"' ORDER BY cp.FechaCita ASC ", sqlcon);
                SqlDataReader Lector = Query.ExecuteReader();
                while (Lector.Read())
                {
                    cboCitas.Items.Add(Lector.GetDecimal(0)).ToString();
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

        private void btnObtenerDatosPaciente_Click(object sender, EventArgs e)
        {
            string CodDoctor, FechaCitas,Cita;
            CodDoctor = cboDoctor.Text;
            FechaCitas = dateTimePicker1.Value.ToShortDateString();
            Cita=cboCitas.Text;
            try
            {
                SqlConnection sqlcon = new SqlConnection(CadConexion);
                sqlcon.Open();
                SqlCommand Query = new SqlCommand("SELECT cp.Cita,       p.Expediente,       p.Nombre,       p.FechaNacimiento,       p.Nacionalidad,       p.EstadCivil,       p.Sexo,       cp.FechaCita FROM   Clinica.CitasPacientes        AS cp       INNER JOIN Clinica.Pacientes  AS p            ON  p.Expediente = cp.Expediente WHERE cp.CodDoc='" + CodDoctor + "' AND cp.FechaCita>='" + FechaCitas + "' AND cp.Cita ='"+Cita+"'ORDER BY cp.FechaCita ASC ", sqlcon);
                SqlDataReader Lector = Query.ExecuteReader();
                while (Lector.Read())
                {
                    txtCita.Text = Cita;
                    txtExpediente.Text = Lector.GetString(1);
                    txtNombre.Text = Lector.GetString(2);
                    txtFechaNacimiento.Text = Lector.GetDateTime(3).ToShortDateString();
                    txtNacionalidad.Text= Lector.GetString(4);
                    txtEstadoCivil.Text=Lector.GetString(5);
                    txtSexo.Text=Lector.GetString(6);
                    txtFechaCita.Text = Lector.GetDateTime(7).ToString();
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
