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
    public partial class frmResumenDoctores : Form
    {
        string CadConexion;
        public frmResumenDoctores()
        {
            InitializeComponent();
            CadConexion = DataEstatica.GetCadenaConexion();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombreCod.Text = "";
            dgwDoc.DataSource = "";
        }

        private void btnObtenerDatos_Click(object sender, EventArgs e)
        {
            string SP, CodDoctor;
            try
            {
                CodDoctor = txtNombreCod.Text;
                SP = "Clinica.ServObtneResumenesDoctores" + "'" + CodDoctor + "'";
                SqlConnection sqlcon = new SqlConnection(CadConexion);
                SqlDataAdapter Adaptador = new SqlDataAdapter(SP, sqlcon);
                DataTable Tabla = new DataTable();
                Adaptador.Fill(Tabla);
                dgwDoc.DataSource = Tabla;
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        Bitmap bmp;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp,0,0);
        }

        private void btn_imprimir_Click(object sender, EventArgs e)
        {
            int height = dgwDoc.Height;
            dgwDoc.Height = dgwDoc.RowCount * dgwDoc.RowTemplate.Height * 2;
            bmp = new Bitmap(dgwDoc.Width, dgwDoc.Height);
            dgwDoc.DrawToBitmap(bmp, new Rectangle(0, 0, dgwDoc.Width, dgwDoc.Height));
            dgwDoc.Height = height;
            printDocument1.DefaultPageSettings.Landscape = true;
            printPreviewDialog1.ShowDialog();
        }
    }
}
