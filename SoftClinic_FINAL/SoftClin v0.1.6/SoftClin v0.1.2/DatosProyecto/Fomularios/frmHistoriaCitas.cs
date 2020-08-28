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
    public partial class frmHistoriaCitas : Form
    {
        string CadConexion;
        public frmHistoriaCitas()
        {
            InitializeComponent();
            CadConexion = DataEstatica.GetCadenaConexion();
        }

        private void bntLimpiar_Click(object sender, EventArgs e)
        {
            txtNomExp.Text = "";
            dgwHistoria.DataSource = "";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string Expediente, SP;
            try
            {
                Expediente = txtNomExp.Text;
                SP = "Clinica.ServObtneResumenesHistoriaPaciente" + "'" + Expediente + "'";
                SqlConnection sqlcon = new SqlConnection(CadConexion);
                SqlDataAdapter Adaptador = new SqlDataAdapter(SP, sqlcon);
                DataTable Tabla = new DataTable();
                Adaptador.Fill(Tabla);
                dgwHistoria.DataSource = Tabla;
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: " + E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        Bitmap bmp;
      

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
 int height = dgwHistoria.Height;
            dgwHistoria.Height = dgwHistoria.RowCount * dgwHistoria.RowTemplate.Height * 2;
            bmp = new Bitmap(dgwHistoria.Width, dgwHistoria.Height);
            dgwHistoria.DrawToBitmap(bmp, new Rectangle(0, 0, dgwHistoria.Width, dgwHistoria.Height));
            dgwHistoria.Height = height;
            printDocument1.DefaultPageSettings.Landscape = true;
            printPreviewDialog1.ShowDialog();
        
        }
    }
}
