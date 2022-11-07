using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Layanan_Dokter.FolderDokter
{
    public partial class frmDetailPasien : Form
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        void IncreaseOpacity(object sender, EventArgs e)
        {
            if (this.Opacity <= 1) this.Opacity += 0.01;
            if (this.Opacity == 1) timer.Stop();
        }
        string kodepasien,id;
        public frmDetailPasien(string kode)
        {
            this.kodepasien = kode;
            InitializeComponent();
        }
        SqlConnection Conn = new SqlConnection(@"Server=ALYYY;Database=aplikasiDokter;Integrated Security = True;");
        SqlCommand Comm = new SqlCommand();
        DataSet dS = new DataSet();
        SqlDataAdapter dA = new SqlDataAdapter();
        DataRow dRow;
        private void frmDetailPasien_Load(object sender, EventArgs e)
        {
            this.Opacity = .01;
            timer.Interval = 10;
            timer.Tick += IncreaseOpacity;
            timer.Start();

            dA = new SqlDataAdapter(@"select * from tbl_pasien where Id_pasien='" + kodepasien + "'", Conn);
            dA.Fill(dS, "pasien");
            DataColumn[] dc = { dS.Tables["pasien"].Columns["Id_pasien"] };
            dS.Tables["pasien"].PrimaryKey = dc;
            dRow = dS.Tables["pasien"].Rows[0];
            id = Convert.ToString(dRow[0]);
            dRow = dS.Tables["pasien"].Rows.Find(id);
            txtNama.Text = Convert.ToString(dRow[2]);
            txtJenisKelamin.Text = Convert.ToString(dRow[3]);
            dtpTglLahir.Value = Convert.ToDateTime(dRow[4]);
            txtTmptLahir.Text = Convert.ToString(dRow[5]);
            txtUsia.Text = Convert.ToString(dRow[6]);
            txtNoTlp.Text = Convert.ToString(dRow[7]);
            txtAlamat.Text = Convert.ToString(dRow[8]);
            txtAsuransi.Text = Convert.ToString(dRow[10]);
            txtPekerjaan.Text = Convert.ToString(dRow[11]);
            try
            {
                byte[] image = (byte[])dRow[12];
                if (image == null)
                {
                    pictureBox1.BackgroundImage = Layanan_Dokter.Properties.Resources.usericon;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(image);
                    pictureBox1.Image = Image.FromStream(ms);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch
            {
                pictureBox1.Image = Layanan_Dokter.Properties.Resources.usericon;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Apakah Anda yakin ingin Keluar ?", "Pesan", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void frmDetailPasien_DragEnter(object sender, DragEventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void frmDetailPasien_ResizeBegin(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void frmDetailPasien_ResizeEnd(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void txtNama_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
