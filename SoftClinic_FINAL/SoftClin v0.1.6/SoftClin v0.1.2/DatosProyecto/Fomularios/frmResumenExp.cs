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
    public partial class frmResumenExp : Form
    {
        private string CadConexion;
        public frmResumenExp()
        {
            InitializeComponent();
            CadConexion=DataEstatica.GetCadenaConexion();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string TextoBuscar,SP;
            try
            {
                TextoBuscar = txtNomExp.Text;
                SP = "Clinica.ServObtneResumenesExpediente" + "'" + TextoBuscar + "'";
                SqlConnection sqlcon = new SqlConnection(CadConexion);
                SqlDataAdapter Adaptador = new SqlDataAdapter(SP, sqlcon);
                DataTable Tabla = new DataTable();
                Adaptador.Fill(Tabla);
                dgwExpe.DataSource = Tabla;
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bntLimpiar_Click(object sender, EventArgs e)
        {
            txtNomExp.Text = "";
            dgwExpe.DataSource = "";
        }
        Bitmap bmp;
        private void btn_imprimir_Click(object sender, EventArgs e)
        {
            int height = dgwExpe.Height;
            dgwExpe.Height = dgwExpe.RowCount * dgwExpe.RowTemplate.Height * 2;
            bmp = new Bitmap(dgwExpe.Width, dgwExpe.Height);
            dgwExpe.DrawToBitmap(bmp, new Rectangle(0, 0, dgwExpe.Width, dgwExpe.Height));
            dgwExpe.Height = height;
            printDocument1.DefaultPageSettings.Landscape = true;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp,0,0);

        }
    }
}
