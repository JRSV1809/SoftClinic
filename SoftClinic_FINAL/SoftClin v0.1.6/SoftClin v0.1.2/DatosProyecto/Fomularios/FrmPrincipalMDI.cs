using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SoftClin_v0._1._2
{
    public partial class FrmPrincipalMDI : Form
    {
        private int childFormNumber = 0;
        static string CadConexion;

        public FrmPrincipalMDI()
        {
            try
            {
                String TieneRecepcion, TieneDoctor, TieneAdministracion, TieneIT;
                CadConexion = DataEstatica.GetCadenaConexion();
                //Validaciones de menus
                SqlConnection sqlcon = new SqlConnection(CadConexion);
                /*Menu Recepcion*/
                SqlCommand sqlcmd = new SqlCommand("clinica.servmenuRecepcion", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                /*Menu Doctores*/
                SqlCommand sqlcmd2 = new SqlCommand("clinica.servmenuDoctores", sqlcon);
                sqlcmd2.CommandType = CommandType.StoredProcedure;
                /*Menu Administracion*/
                SqlCommand sqlcmd3 = new SqlCommand("clinica.servmenuAdministracion", sqlcon);
                sqlcmd3.CommandType = CommandType.StoredProcedure;
                /*Menu IT*/
                SqlCommand sqlcmd4 = new SqlCommand("clinica.servmenuIT", sqlcon);
                sqlcmd4.CommandType = CommandType.StoredProcedure;
                sqlcon.Open();
                TieneRecepcion = sqlcmd.ExecuteScalar().ToString();
                TieneDoctor = sqlcmd2.ExecuteScalar().ToString();
                TieneAdministracion = sqlcmd3.ExecuteScalar().ToString();
                TieneIT = sqlcmd4.ExecuteScalar().ToString();
                sqlcon.Close();

                InitializeComponent();

                if (TieneRecepcion == "S")
                {
                    recepcionToolStripMenuItem.Visible = true;
                }
                else { recepcionToolStripMenuItem.Visible = false; }
                if (TieneDoctor == "S")
                {
                    doctoresToolStripMenuItem.Visible = true;
                }
                else { doctoresToolStripMenuItem.Visible = false; }
                if (TieneAdministracion == "S")
                {
                    administracionToolStripMenuItem.Visible = true;
                }
                else { administracionToolStripMenuItem.Visible = false; }

            }
            catch (Exception E)
            {
                MessageBox.Show("Error al cargar formulario" + E, "Error Formulario Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void pruebaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string CadConexion;

            CadConexion = DataEstatica.GetCadenaConexion();

            MessageBox.Show("Cadena Conexion: " + CadConexion);
        }

        private void crearNuevoExpedienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNvoExpediente fmne = new frmNvoExpediente();
            fmne.MdiParent = this;
            fmne.Show();
        }

        private void modificarExpedienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmActuExp fmae = new frmActuExp();
            fmae.MdiParent = this;
            fmae.Show();
        }

        private void verExpedientesResumenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmResumenExp fmre = new frmResumenExp();
            fmre.MdiParent = this;
            fmre.Show();
        }

        private void resumenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmResumenDoctores fmrd = new frmResumenDoctores();
            fmrd.MdiParent = this;
            fmrd.Show();
        }

        private void crearCitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCrearCita fmcc = new frmCrearCita();
            fmcc.MdiParent = this;
            fmcc.Show();
        }

        private void resumenCitasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmResumenCitas fmrc = new frmResumenCitas();
            fmrc.MdiParent = this;
            fmrc.Show();
        }

        private void eliminarCitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmActualizarCita fmac = new frmActualizarCita();
            fmac.MdiParent = this;
            fmac.Show();
        }

        private void verCitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDoctoresCitas fmdc = new frmDoctoresCitas();
            fmdc.MdiParent = this;
            fmdc.Show();
        }

        private void atenderCitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAtenderCita fmac = new frmAtenderCita();
            fmac.MdiParent = this;
            fmac.Show();
        }

        private void historiaCitasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHistoriaCitas fmhc = new frmHistoriaCitas();
            fmhc.MdiParent = this;
            fmhc.Show();
        }

        private void historiaCitasDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHistoriaDetalleCita fmhdc = new frmHistoriaDetalleCita();
            fmhdc.MdiParent = this;
            fmhdc.Show();
        }

        private void crearEspecialidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCrearEspecialidadDoctor fmed = new frmCrearEspecialidadDoctor();
            fmed.MdiParent = this;
            fmed.Show();
        }

        private void resumenEspecialidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmResumenEspecialidadesDoctores fmed = new frmResumenEspecialidadesDoctores();
            fmed.MdiParent = this;
            fmed.Show();
        }

        private void modificarEspecialidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmModificarEspecialidad fmme = new frmModificarEspecialidad();
            fmme.MdiParent = this;
            fmme.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmPrincipalMDI_Load(object sender, EventArgs e)
        {

        }

        private void crearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmingresodoctores fmid = new frmingresodoctores();
            fmid.MdiParent = this;
            fmid.Show();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmodificadoctores fmmd = new frmmodificadoctores();
            fmmd.MdiParent = this;
            fmmd.Show();
        }
    }
}
