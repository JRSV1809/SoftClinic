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
    public partial class frmCrearCita : Form
    {
        static string CadConexion;
        public frmCrearCita()
        {

            
            CadConexion = DataEstatica.GetCadenaConexion();
            InitializeComponent();
            try
            {
                SqlConnection sqlcon = new SqlConnection(CadConexion);
                sqlcon.Open();
                SqlCommand Query = new SqlCommand("SELECT MAX(ISNULL(Cita,0))+1 FROM Clinica.CitasPacientes", sqlcon);
                SqlCommand Query2 = new SqlCommand("SELECT p.Expediente FROM Clinica.Pacientes AS p", sqlcon);
                SqlCommand Query3 = new SqlCommand("SELECT d.CodDoc FROM Clinica.Doctores AS d", sqlcon);
                SqlDataReader Lector = Query.ExecuteReader();
                while (Lector.Read())
                {
                    txtCita.Text = Lector.GetDecimal(0).ToString();
                }
                Lector.Close();
                SqlDataReader Lector2 = Query2.ExecuteReader();
                while (Lector2.Read())
                {
                    cboExpediente.Items.Add(Lector2.GetString(0));
                }
                Lector2.NextResult();
                Lector2.Close();

                SqlDataReader Lector3 = Query3.ExecuteReader();
                while (Lector3.Read())
                {
                    cboDoctor.Items.Add(Lector3.GetString(0));
                }
                Lector3.NextResult();
                Lector3.Close();
                sqlcon.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E, "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCrearCita_Click(object sender, EventArgs e)
        {
            string Cita,Expediente,CodDoc,Fecha,Hora;
            try
            {
                Cita = txtCita.Text;
                Expediente = cboExpediente.Text;
                CodDoc = cboDoctor.Text;
                Fecha = dateTimePicker1.Value.ToShortDateString();
                Hora = dateTimePicker2.Value.ToShortTimeString();
                SqlConnection sqlcon = new SqlConnection(CadConexion);
                sqlcon.Open();
                SqlCommand Query = new SqlCommand("INSERT INTO Clinica.CitasPacientes (	Cita,	Expediente,	CodDoc,	FechaCita)VALUES(	'" + Cita + "',	'" + Expediente + "',	'" + CodDoc + "',	'" + Fecha + " " + Hora + "')", sqlcon);
                Query.ExecuteNonQuery();
                sqlcon.Close();
                MessageBox.Show("Cita Creada","Cita Creada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void frmCrearCita_Load(object sender, EventArgs e)
        {
            dateTimePicker2.Value = DateTime.Now;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "hh:mm:tt";
            
        }
    }
}
