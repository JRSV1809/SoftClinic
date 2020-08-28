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
    public partial class frmNvoExpediente : Form
    {
        string CadConexion, CodPaciente;
        public frmNvoExpediente()
        {
            CadConexion = DataEstatica.GetCadenaConexion();
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string NombreCompleto, PrimerNombre, SegundoNombre, TercerNombre, PrimerApellido, SegundoApellido, TercerApellido, TipoTelefono, Telefono, Nacionalidad, EstadoCivil, Sexo, TipoDireccion, Calle, Barrio, NumeroCalles, ContactoEmergencia, NombreEmergencia, IDEmergencia, ParentescoEmergencia, TelefonoEmergencia, DireccionEmergencia, Pais, FechaNacimiento,SP;
            SqlConnection sqlcon = new SqlConnection(CadConexion);
            SqlTransaction sqlTran;
            sqlcon.Open();
            sqlTran = sqlcon.BeginTransaction();
            try
            {
                //Datos Pacientes
                CodPaciente = txtExpediente.Text;
                PrimerNombre = txtPrimerNombre.Text;
                SegundoNombre = txtSegundoNombre.Text;
                TercerNombre = txtTercerNombre.Text;
                PrimerApellido = txtPrimerApellido.Text;
                SegundoApellido = txtSegundoApellido.Text;
                TercerApellido = txtApellidoCasada.Text;
                NombreCompleto = PrimerNombre + " "+SegundoNombre +" "+ TercerNombre +" "+ PrimerApellido +" "+ SegundoApellido + " "+TercerApellido;
                FechaNacimiento = Calendario.Value.ToShortDateString();
                Nacionalidad = cboNacionalidad.Text;
                Sexo = cboSexo.Text;
                EstadoCivil = cboEstadoCivil.Text;
                //Datos telefonico
                TipoTelefono = cobTipoTelefono.Text;
                Telefono = txtTelefono.Text;
                //Datos Direccion
                TipoDireccion = cboTipoDireccion.Text;
                Calle = txtCalle.Text;
                Barrio = txtBarrio.Text;
                NumeroCalles = txtNumero.Text;
                Pais = cboPais.Text;
                txtNombreCompleto.Text = NombreCompleto.ToString();
                //Datos Emergencia
                ContactoEmergencia = CboEmergenciaContacto.Text;
                NombreEmergencia = txtNombreCompletoEmergencia.Text;
                ParentescoEmergencia = cboParentesco.Text;
                IDEmergencia = txtIDEmergencia.Text;
                TelefonoEmergencia = txtTelefonoEmergencia.Text;
                DireccionEmergencia = txtDireccionEmergencia.Text;
                SP = "Clinica.Recepcion_NuevoExpediente_AltaExpediente"+"'"+CodPaciente+ "','"+NombreCompleto+ "','"+PrimerNombre+ "','"+SegundoNombre+ "','"+TercerNombre+ "','"+PrimerApellido+ "','"+SegundoApellido+ "','"+TercerApellido+ "','"+FechaNacimiento+ "','"+Nacionalidad+ "','"+EstadoCivil+ "','"+Sexo+ "','"+TipoDireccion+ "','"+Calle+ "','"+Barrio+ "','"+NumeroCalles+ "','"+Pais+ "','"+ContactoEmergencia+ "','"+NombreEmergencia+"','"+ParentescoEmergencia+ "','"+IDEmergencia+ "','"+DireccionEmergencia+ "','"+TelefonoEmergencia+ "','"+TipoTelefono+ "','"+Telefono+ "'";
                //Conexion SQL
                SqlCommand Query = new SqlCommand(SP, sqlcon);
                Query.Transaction = sqlTran;
                Query.ExecuteNonQuery();
                sqlTran.Commit();
                //Datos Contacto emergencia
                if (ContactoEmergencia == "NO")
                {
                    txtNombreCompletoEmergencia.Visible = false;
                    cboParentesco.Visible = false;
                    txtIDEmergencia.Visible = false;
                    txtTelefonoEmergencia.Visible = false;
                    txtDireccionEmergencia.Visible = false;

                }
                MessageBox.Show("Datos Guardados Existosamente");
                this.Close();
            }
            catch (Exception E)
            {
                sqlTran.Rollback();
                MessageBox.Show("Error: " + E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

       

        private void txtNombreCompleto_MouseClick(object sender, MouseEventArgs e)
        {
            txtNombreCompleto.Text = txtPrimerNombre.Text + " " + txtSegundoNombre.Text + " " + txtTercerNombre.Text + " " + txtPrimerApellido.Text + " " + txtSegundoApellido.Text + " " + txtApellidoCasada.Text;
        }

   

   

       

    }

}


    
   
