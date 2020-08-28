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
    public partial class frmResumenEspecialidadesDoctores : Form
    {
        string CadConexion;
        public frmResumenEspecialidadesDoctores()
        {
            InitializeComponent();
            CadConexion = DataEstatica.GetCadenaConexion();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtEspe.Text = "";
            dgwEspe.DataSource = "";
        }

        private void btnObtener_Click(object sender, EventArgs e)
        {
            string Especialidad,SP;
            Especialidad = txtEspe.Text;
            SP = "Clinica.ServObtneResumenesEEspecialidadesDoctores" + "'" + Especialidad + "'";
            SqlConnection sqlcon = new SqlConnection(CadConexion);
            SqlDataAdapter Adaptador = new SqlDataAdapter(SP, sqlcon);
            DataTable Tabla = new DataTable();
            Adaptador.Fill(Tabla);
            dgwEspe.DataSource = Tabla;
        }
        Bitmap bmp;

        private void btn_imprimir_Click(object sender, EventArgs e)
        {
            int height = dgwEspe.Height;
            dgwEspe.Height = dgwEspe.RowCount * dgwEspe.RowTemplate.Height * 2;
            bmp = new Bitmap(dgwEspe.Width, dgwEspe.Height);
            dgwEspe.DrawToBitmap(bmp, new Rectangle(0, 0, dgwEspe.Width, dgwEspe.Height));
            dgwEspe.Height = height;
            printDocument1.DefaultPageSettings.Landscape = true;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }
    }
}
