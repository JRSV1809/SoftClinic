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
    public partial class frmActuExp : Form
    {
        static string CadConexion;
        public frmActuExp()
        {
            InitializeComponent();
            CadConexion = DataEstatica.GetCadenaConexion();
            SqlConnection sqlcon = new SqlConnection(CadConexion);
            sqlcon.Open();
            SqlCommand Query = new SqlCommand("SELECT p.Expediente FROM Clinica.Pacientes AS p", sqlcon);
            SqlDataReader Lector = Query.ExecuteReader();
            while (Lector.Read())
            {
                cboExpediente.Items.Add(Lector.GetString(0));
            }
            Lector.NextResult();
            Lector.Close();
        }

        private void btnObtenerDatos_Click(object sender, EventArgs e)
        {
            string CodExpediente;
            try
            {
                CodExpediente = cboExpediente.Text;
                SqlConnection sqlcon = new SqlConnection(CadConexion);
                sqlcon.Open();
                SqlCommand Query = new SqlCommand("SELECT isnull(p.Nombre,''),       isnull(p.PrimerNombre,''),       isnull(p.SegundoNombre,''),       isnull(p.TercerNombre,''),       isnull(p.PrimerApellido,''),       isnull(p.SegundoApellido,''),       isnull(p.ApellidoCasada,''),       isnull(p.FechaNacimiento,''),       isnull(p.Nacionalidad,''),       isnull(p.EstadCivil,''),       isnull(p.Sexo,''),       isnull(pd.TipoDireccion,''),       isnull(pd.Calle,''),       isnull(pd.Barrio,''),       isnull(pd.Numero,''),       isnull(pt.TipoNumero,''),       isnull(pt.Numero,''),       isnull(pf.Nombre,''),       isnull(pf.Parentesco,''),       isnull(pf.DUI,''),       isnull(pf.Direccion,''),       isnull(pf.Telefono,''), isnull(pd.Pais,'')FROM   Clinica.Pacientes                  AS p       LEFT JOIN Clinica.PacientesDirecciones  AS pd            ON  p.Expediente = pd.Expediente       LEFT JOIN Clinica.PacientesTelefonos    AS pt            ON  p.Expediente = pt.Expediente       LEFT JOIN clinica.PacientesFamiliares   AS pf            ON  p.Expediente = pf.Expediente WHERE  p.Expediente = '" + CodExpediente + "'", sqlcon);
                SqlDataReader Lector = Query.ExecuteReader();

                if (Lector.HasRows)
                {
                    
                    while (Lector.Read())
                    {
                        txtNombreCompleto.Text = Lector.GetString(0);
                        txtPrimerNombre.Text = Lector.GetString(1);
                        txtSegundoNombre.Text = Lector.GetString(2);
                        txtTercerNombre.Text = Lector.GetString(3);
                        txtPrimerApellido.Text = Lector.GetString(4);
                        txtSegundoApellido.Text = Lector.GetString(5);
                        txtApellidoCasada.Text = Lector.GetString(6);
                        Calendario.Value = Lector.GetDateTime(7);
                        cboNacionalidad.Text = Lector.GetString(8);
                        cboEstadoCivil.Text = Lector.GetString(9);
                        cboSexo.Text = Lector.GetString(10);
                        cboTipoDireccion.Text = Lector.GetString(11);
                        txtCalle.Text = Lector.GetString(12);
                        txtBarrio.Text = Lector.GetString(13);
                        txtNumero.Text = Lector.GetString(14);
                        cobTipoTelefono.Text = Lector.GetString(15);
                        txtTelefono.Text = Lector.GetString(16);
                        txtNombreCompletoEmergencia.Text = Lector.GetString(17);
                        cboParentesco.Text = Lector.GetString(18);
                        txtIDEmergencia.Text = Lector.GetString(19);
                        txtDireccionEmergencia.Text = Lector.GetString(20);
                        txtTelefonoEmergencia.Text = Lector.GetString(21);
                        cboPais.Text = Lector.GetString(22);
                    }
                }
                else
                {
                    MessageBox.Show("Expediente no encontrado, revisar numero de expediente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                sqlcon.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E, "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBorrarExpediente_Click(object sender, EventArgs e)
        {
            string CodExpediente,SP;
            SqlConnection sqlcon = new SqlConnection(CadConexion);
            SqlTransaction sqlTran;
            sqlcon.Open();
            sqlTran = sqlcon.BeginTransaction();
            try
            {
                CodExpediente = cboExpediente.Text;
                SP = "Clinica.Recepcion_BorrarExpediente" + "'" + CodExpediente + "'";
                //Conexion SQL
                SqlCommand Query = new SqlCommand(SP, sqlcon);
                Query.Transaction = sqlTran;
                Query.ExecuteNonQuery();
                sqlTran.Commit();
                MessageBox.Show("Datos Borrados Existosamente");
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E.Message, "Error en Borraado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sqlTran.Rollback();
            }
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string CodExpe, NombreCompleto, PrimerNombre, SegundoNombre, TercerNombre, PrimerApellido, SegundoApellido, TercerApellido, TipoTelefono, Telefono, Nacionalidad, EstadoCivil, Sexo, TipoDireccion, Calle, Barrio, NumeroCalles, ContactoEmergencia, NombreEmergencia, IDEmergencia, ParentescoEmergencia, TelefonoEmergencia, DireccionEmergencia, Pais,FechaNacimiento,SP;
            SqlConnection sqlcon = new SqlConnection(CadConexion);
            SqlTransaction sqlTran;
            sqlcon.Open();
            sqlTran = sqlcon.BeginTransaction();
            try
            {
                CodExpe = cboExpediente.Text;
                PrimerNombre = txtPrimerNombre.Text;
                SegundoNombre = txtSegundoNombre.Text;
                TercerNombre = txtTercerNombre.Text;
                PrimerApellido = txtPrimerApellido.Text;
                SegundoApellido = txtSegundoApellido.Text;
                TercerApellido = txtApellidoCasada.Text;
                FechaNacimiento = Calendario.Value.ToShortDateString();
                Nacionalidad = cboNacionalidad.Text;
                Sexo = cboSexo.Text;
                EstadoCivil = cboEstadoCivil.Text;
                NombreCompleto = PrimerNombre + " " + SegundoNombre + " " + TercerNombre + " " + PrimerApellido + " " + SegundoApellido + " " + TercerApellido;
                //Datos telefonico
                TipoTelefono = cobTipoTelefono.Text;
                Telefono = txtTelefono.Text;
                //Datos Direccion
                TipoDireccion = cboTipoDireccion.Text;
                Calle = txtCalle.Text;
                Barrio = txtBarrio.Text;
                NumeroCalles = txtNumero.Text;
                Pais = cboPais.Text;

                ContactoEmergencia = CboEmergenciaContacto.Text;
                NombreEmergencia = txtNombreCompletoEmergencia.Text;
                ParentescoEmergencia = cboParentesco.Text;
                IDEmergencia = txtIDEmergencia.Text;
                TelefonoEmergencia = txtTelefonoEmergencia.Text;
                DireccionEmergencia = txtDireccionEmergencia.Text;
                SP = "Clinica.Recepcion_ActualizarExpediente" + "'" + CodExpe + "','" + NombreCompleto + "','" + PrimerNombre + "','" + SegundoNombre + "','" + TercerNombre + "','" + PrimerApellido + "','" + SegundoApellido + "','" + TercerApellido + "','" + FechaNacimiento + "','" + Nacionalidad + "','" + EstadoCivil + "','" + Sexo + "','" + TipoDireccion + "','" + Calle + "','" + Barrio + "','" + NumeroCalles + "','" + Pais + "','" + ContactoEmergencia + "','" + NombreEmergencia + "','" + ParentescoEmergencia + "','" + IDEmergencia + "','" + DireccionEmergencia + "','" + TelefonoEmergencia + "','" + TipoTelefono + "','" + Telefono + "'";
                SqlCommand Query = new SqlCommand(SP, sqlcon);
                Query.Transaction = sqlTran;
                Query.ExecuteNonQuery();
                sqlTran.Commit();
                MessageBox.Show("Datos Actualizados Existosamente");
                this.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sqlTran.Rollback();
            }
            sqlcon.Close();
        }

        private void CboEmergenciaContacto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ContactoEmergencia;
            ContactoEmergencia = CboEmergenciaContacto.Text;
            

            if (ContactoEmergencia == "SI")
            {
                txtNombreCompletoEmergencia.Visible = true;
                cboParentesco.Visible = true;
                txtIDEmergencia.Visible = true;
                txtTelefonoEmergencia.Visible = true;
                txtDireccionEmergencia.Visible = true;
                label20.Visible = true;
                label21.Visible = true;
                label22.Visible = true;
                label23.Visible = true;
                label24.Visible = true;
               
            }
            else
            {
                txtNombreCompletoEmergencia.Visible = false;
                cboParentesco.Visible = false;
                txtIDEmergencia.Visible = false;
                txtTelefonoEmergencia.Visible = false;
                txtDireccionEmergencia.Visible = false;
                label20.Visible = false;
                label21.Visible = false;
                label22.Visible = false;
                label23.Visible = false;
                label24.Visible = false;
               

            }
        }

    }
}
