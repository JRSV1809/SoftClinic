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
    public partial class frmingresodoctores : Form
    {
        string CadConexion, Coddoctor;
        public frmingresodoctores()
        {
            InitializeComponent();
            CadConexion = DataEstatica.GetCadenaConexion();
         
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            string coddoc, nombre, iddoc, codesp, genero, dui, tinumero, numero, descripcion;
            try
            {
                //Datos Doctor
                coddoc = txtcoddoctor.Text;
                nombre = txtnombre.Text;
                iddoc = txtiddoc.Text;
                codesp = txtcodesp.Text;
                genero = cmbgenero.Text;
                dui = txtdui.Text;
                //Datos telefonico
                tinumero = cmbtnumero.Text;
                numero = txtnumero.Text;
                //Datos Especializacion
                descripcion = txtdescripcion.Text;
                //Conexion SQL
                SqlConnection sqlcon = new SqlConnection(CadConexion);
                sqlcon.Open();
                //Query Insert en Clinica.Doctores
                SqlCommand Query = new SqlCommand("INSERT INTO Clinica.Doctores(CodDoc,Nombre,IDDoc,CodEsp,	Genero,	DUI, FechaGrabacion,	UsuarioGrabacion,	FechaModificacion,	UsuarioModificacion)VALUES('" + coddoc + "','" + nombre + "','" + iddoc + "','" + codesp + "','"  + "','" + genero + "','" + dui + "','" + "',GETDATE(),CURRENT_USER,GETDATE(),CURRENT_USER)", sqlcon);
                //Query Insert en Clinica.DoctoresTelefonos
                SqlCommand Query2 = new SqlCommand("INSERT INTO Clinica.DoctoresTelefonos(	CodDoc,	TipoNumero,	Numero,	FechaGrabacion,	UsuarioGrabacion,	FechaModificacion,	UsuarioModificacion)VALUES(	'" + coddoc + "',	'" + tinumero + "',	'" + numero + "',	GETDATE(),	CURRENT_USER,	GETDATE(),	CURRENT_USER)", sqlcon);
                //Query Insert en Clinica.DoctoresEspecializaciones
                SqlCommand Query3 = new SqlCommand("INSERT INTO Clinica.DoctoresEspecializaciones(CodEsp,	Descripcion, FechaGrabacion,	UsuarioGrabacion,	FechaModificacion,	UsuarioModificacion)VALUES(	'" + codesp + "',	'" + descripcion + "',	'" + "',	GETDATE(),	CURRENT_USER,GETDATE(),	CURRENT_USER)", sqlcon);
                Query.ExecuteNonQuery();
                Query2.ExecuteNonQuery();
                Query3.ExecuteNonQuery();
                sqlcon.Close();
                MessageBox.Show("Datos Guardados Existosamente");
                this.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
