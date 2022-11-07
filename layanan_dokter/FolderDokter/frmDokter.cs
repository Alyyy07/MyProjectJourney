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
    public partial class frmDokter : Form
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

        SqlConnection Conn = new SqlConnection(@"Server=ALYYY;Database=aplikasiDokter;Integrated Security = True;");
        SqlCommand Comm = new SqlCommand();
        DataSet dS = new DataSet();
        SqlDataAdapter dA = new SqlDataAdapter();
        DataRow dRow;

        string kodedokter,cek;
        public frmDokter(string kode)
        {
            InitializeComponent();
            this.kodedokter = kode;
        }

        private void frmDokter_Load(object sender, EventArgs e)
        {
            this.Opacity = .01;
            timer.Interval = 10;
            timer.Tick += IncreaseOpacity;
            timer.Start();

            dA = new SqlDataAdapter("select * from tbl_dokter where kode_masuk='" + kodedokter + "'", Conn);
            dA.Fill(dS, "kodedokter");
            DataColumn[] dC = { dS.Tables["kodedokter"].Columns["Id_dokter"] };
            dS.Tables["kodedokter"].PrimaryKey = dC;
            dRow = dS.Tables["kodedokter"].Rows[0];
            try
            {
                byte[] image = (byte[])dRow[9];
                if (image == null)
                {
                    pictureBox1.Image = Layanan_Dokter.Properties.Resources.usericon;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                }
                else
                {
                    MemoryStream ms = new MemoryStream(image);
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            catch
            {
                pictureBox1.Image = Layanan_Dokter.Properties.Resources.usericon;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            cek = Convert.ToString(dRow[1]);
            if (cek == "")
            {
                this.pnlFormLoader.Controls.Clear();
                frmBuatData awal = new frmBuatData(kodedokter) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                awal.FormBorderStyle = FormBorderStyle.None;
                this.pnlFormLoader.Controls.Add(awal);
                awal.Show();

                btnBack.Visible = false;
                btnLogOut.Visible = true;
                btnLogOut.Dock = DockStyle.Left;
            }
            label2.Text += " "+ cek;

            btnBack.Visible = false;
            btnLogOut.Visible = true;
            btnLogOut.Dock = DockStyle.Left;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Apakah Anda yakin ingin Keluar ?", "Pesan", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Apakah Anda yakin ingin Log Out ?", "Pesan", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                new frmLogin().Show();
                this.Close();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new frmDokter(kodedokter).Show();
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void frmDokter_DragEnter(object sender, DragEventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void frmDokter_ResizeBegin(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pnlFormLoader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDatadiri_Click(object sender, EventArgs e)
        {
            this.pnlFormLoader.Controls.Clear();
            frmEdit edit = new frmEdit(kodedokter) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            edit.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(edit);
            edit.Show();

            btnBack.Visible = true;
            btnLogOut.Visible = false;
            btnLogOut.Dock = DockStyle.Left;
        }

        private void btnJadwal_Click(object sender, EventArgs e)
        {
            this.pnlFormLoader.Controls.Clear();
            frmJadwal jadwal = new frmJadwal(kodedokter) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            jadwal.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(jadwal);
            jadwal.Show();

            btnBack.Visible = true;
            btnLogOut.Visible = false;
            btnLogOut.Dock = DockStyle.Left;
        }

        private void btnRiwayat_Click(object sender, EventArgs e)
        {
            this.pnlFormLoader.Controls.Clear();
            frmRiwayat riwayat = new frmRiwayat(kodedokter) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            riwayat.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(riwayat);
            riwayat.Show();

            btnBack.Visible = true;
            btnLogOut.Visible = false;
            btnLogOut.Dock = DockStyle.Left;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmDokter_ResizeEnd(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }
    }
}
