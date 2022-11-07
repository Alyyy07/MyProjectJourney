using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace Layanan_Dokter.FolderPasien
{
    public partial class frmPasien : Form
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

        string kodepasien,cek;
        public frmPasien(string kode)
        {
            InitializeComponent();
            this.kodepasien = kode;
        }

        SqlConnection Conn = new SqlConnection(@"Server=ALYYY;Database=aplikasiDokter;Integrated Security = True;");
        SqlCommand Comm = new SqlCommand();
        DataSet dS = new DataSet();
        SqlDataAdapter dA = new SqlDataAdapter();
        DataRow dRow;

        private void frmPasien_Load(object sender, EventArgs e)
        {
            this.Opacity = .01;
            timer.Interval = 10;
            timer.Tick += IncreaseOpacity;
            timer.Start();


            dA = new SqlDataAdapter("select * from tbl_pasien where kode_masuk='" + kodepasien + "'", Conn);
            dA.Fill(dS, "kodepasien");
            DataColumn[] dC = { dS.Tables["kodepasien"].Columns["Id_pasien"] };
            dS.Tables["kodepasien"].PrimaryKey = dC;
            dRow = dS.Tables["kodepasien"].Rows[0];
            try
            {
                byte[] image = (byte[])dRow[12];
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
            cek = Convert.ToString(dRow[2]);
            if (cek == "")
            {
                this.pnlFormLoader.Controls.Clear();
                frmBuatDatadiri awal = new frmBuatDatadiri(kodepasien) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                awal.FormBorderStyle = FormBorderStyle.None;
                this.pnlFormLoader.Controls.Add(awal);
                awal.Show();
                btnBack.Visible = false;
                btnLogOut.Visible = true;
                btnLogOut.Dock = DockStyle.Left;
            }
            label2.Text = "Selamat Datang "+cek;

            btnBack.Visible = false;
            btnLogOut.Visible = true;
            btnLogOut.Dock = DockStyle.Left;
        }
        private void pnlFormLoader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDatadiri_Click(object sender, EventArgs e)
        {
            this.pnlFormLoader.Controls.Clear();
            frmEdit edit = new frmEdit(kodepasien) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            edit.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(edit);
            edit.Show();


            btnBack.Visible = true;
            btnLogOut.Visible = false;
            btnLogOut.Dock = DockStyle.Left;
            btnBack.Dock = DockStyle.Left;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Apakah Anda yakin ingin Log Out ?", "Pesan", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                new frmLogin().Show();
                this.Hide();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new frmPasien(kodepasien).Show();
            this.Close();
        }

        private void btnJadwal_Click(object sender, EventArgs e)
        {
            this.pnlFormLoader.Controls.Clear();
            frmJadwal jadwal = new frmJadwal(kodepasien) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            jadwal.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(jadwal);
            jadwal.Show();


            btnBack.Visible = true;
            btnLogOut.Visible = false;
            btnLogOut.Dock = DockStyle.Left;
            btnBack.Dock = DockStyle.Left;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void frmPasien_DragEnter(object sender, DragEventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void frmPasien_ResizeBegin(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void frmPasien_ResizeEnd(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void btnRiwayat_Click(object sender, EventArgs e)
        {
            this.pnlFormLoader.Controls.Clear();
            frmRiwayatPasien riwayat = new frmRiwayatPasien(kodepasien) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            riwayat.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(riwayat);
            riwayat.Show();


            btnBack.Visible = true;
            btnLogOut.Visible = false;
            btnLogOut.Dock = DockStyle.Left;
            btnBack.Dock = DockStyle.Left;
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
    }
}
