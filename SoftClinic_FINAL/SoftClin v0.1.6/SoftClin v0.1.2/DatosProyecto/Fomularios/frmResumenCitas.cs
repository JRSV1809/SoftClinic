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
    public partial class frmResumenCitas : Form
    {
        string CadConexion;
        public frmResumenCitas()
        {
            InitializeComponent();
            CadConexion = DataEstatica.GetCadenaConexion();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombreCod.Text = "";
            dgwResCitas.DataSource = "";
        }

        private void btnObtenerDatos_Click(object sender, EventArgs e)
        {
            string CodDoc, Fecha,SP;
            
            try
            {
                CodDoc = txtNombreCod.Text;
                Fecha = dtpFecha.Value.ToShortDateString();
                SP = "Clinica.ServObtneResumenesEEspecialidadesDoctoresResumenCitas" + "'" + CodDoc + "','" + Fecha + "'";
                SqlConnection sqlcon = new SqlConnection(CadConexion);
                SqlDataAdapter Adaptador = new SqlDataAdapter(SP, sqlcon);
                DataTable Tabla = new DataTable();
                Adaptador.Fill(Tabla);
                dgwResCitas.DataSource = Tabla;
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.dgwResCitas.Width, this.dgwResCitas.Height);
            dgwResCitas.DrawToBitmap(bm, new Rectangle(0, 0, this.dgwResCitas.Width, this.dgwResCitas.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }

    
        private void btn_imprimir_Click_1(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.Landscape = true;
            printPreviewDialog1.ShowDialog();
            
        }
       
    }
}
