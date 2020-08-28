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
    public partial class frmmodificadoctores : Form
    {
        static string CadConexion;
        public frmmodificadoctores()
        {
            InitializeComponent();
            CadConexion = DataEstatica.GetCadenaConexion();
            SqlConnection sqlcon = new SqlConnection(CadConexion);
            sqlcon.Open();
            SqlCommand Query = new SqlCommand("SELECT p.CodDoc FROM Clinica.Doctores AS p", sqlcon);
            SqlDataReader Lector = Query.ExecuteReader();
            while (Lector.Read())
            {
                cmbcoddoctorm.Items.Add(Lector.GetString(0));
            }
            Lector.NextResult();
            Lector.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string CodDoc;
            try
            {
                CodDoc = cmbcoddoctorm.Text;
                SqlConnection sqlcon = new SqlConnection(CadConexion);
                sqlcon.Open();
                SqlCommand Query = new SqlCommand("SELECT isnull(p.Nombre,''),       isnull(p.IDDoc,''),       isnull(p.CodEsp,''),       isnull(p.Genero,''),       isnull(p.DUI,''),       isnull(pt.TipoNumero,''),       isnull(pt.Numero,''),         isnull(pf.Descripcion,''),   '')FROM   Clinica.Doctores                  AS p       LEFT JOIN Clinica.DoctoresTelefonos  AS pd            ON  p.CodDoc = pd.CodDoc       LEFT JOIN Clinica.DoctoresEspecializaciones    AS pt            ON  p.CodDoc = pt.CodDoc = '" + CodDoc + "'", sqlcon);
                SqlDataReader Lector = Query.ExecuteReader();

                if (Lector.HasRows)
                {

                    while (Lector.Read())
                    {
                        txtnombrem.Text = Lector.GetString(0);
                        txtiddocm.Text = Lector.GetString(1);
                        txtcodespm.Text = Lector.GetString(2);
                        cmbgenerom.Text = Lector.GetString(3);
                        txtduim.Text = Lector.GetString(4);
                        cmbtnumerom.Text = Lector.GetString(5);
                        txtnumerom.Text = Lector.GetString(6);
                        txtdescripcionm.Text = Lector.GetString(7);
                       
                    }
                }
                else
                {
                    MessageBox.Show("Doctor no encontrado, revisar codigo de doctor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                sqlcon.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E, "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            string coddoc, nombre, iddoc, codesp, genero, dui, tinumero, numero, descripcion;
            try
            {
                //Datos Doctor
                coddoc = cmbcoddoctorm.Text;
                nombre = txtnombrem.Text;
                iddoc = txtiddocm.Text;
                codesp = txtcodespm.Text;
                genero = cmbgenerom.Text;
                dui = txtduim.Text;


                //Datos telefonico
                tinumero = cmbtnumerom.Text;
                numero = txtnumerom.Text;
                //Datos Especializacion
                descripcion = txtdescripcionm.Text;
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
