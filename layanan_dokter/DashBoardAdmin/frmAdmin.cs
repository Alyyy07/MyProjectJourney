using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Layanan_Dokter
{
    public partial class frmAdmin : Form
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

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int NrightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );
        public frmAdmin()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnDashboard.Height;
            pnlNav.Top = btnDashboard.Top;
            pnlNav.Left = btnDashboard.Left;

            lblTitle.Text = "Dashboard";
            this.pnlFormLoader.Controls.Clear();
            frmDashboard frmDash = new frmDashboard() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmDash.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(frmDash);
            frmDash.Show();

            btnDashboard.BackColor = Color.FromArgb(46, 51, 73);
        }
        private void frmAdmin_Load(object sender, EventArgs e)
        {
            this.Opacity = .01;
            timer.Interval = 10;
            timer.Tick += IncreaseOpacity;
            timer.Start();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnDashboard.Height;
            pnlNav.Top = btnDashboard.Top;
            pnlNav.Left = btnDashboard.Left;
            btnDashboard.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Dashboard";
            this.pnlFormLoader.Controls.Clear();
            frmDashboard frmDash = new frmDashboard() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmDash.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(frmDash);
            frmDash.Show();
        }
        private void btnAkunPasien_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnAkunPasien.Height;
            pnlNav.Top = btnAkunPasien.Top;
            pnlNav.Left = btnAkunPasien.Left;
            btnAkunPasien.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Akun Pasien";
            this.pnlFormLoader.Controls.Clear();
            frmAkunPasien frmAP = new frmAkunPasien() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmAP.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(frmAP);
            frmAP.Show();
        }
        private void btnAkunDoc_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnDataDoc.Height;
            pnlNav.Top = btnDataDoc.Top;
            pnlNav.Left = btnDataDoc.Left;
            btnDataDoc.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Akun Dokter";
            this.pnlFormLoader.Controls.Clear();
            frmAkunDokter frmAD = new frmAkunDokter() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmAD.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(frmAD);
            frmAD.Show();
        }

        private void btnDataObat_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnDataObat.Height;
            pnlNav.Top = btnDataObat.Top;
            pnlNav.Left = btnDataObat.Left;
            btnDataObat.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Data Obat";
            this.pnlFormLoader.Controls.Clear();
            frmDataObat frmDO = new frmDataObat() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmDO.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(frmDO);
            frmDO.Show();
        }
        private void btnRiwayat_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnRiwayat.Height;
            pnlNav.Top = btnRiwayat.Top;
            pnlNav.Left = btnRiwayat.Left;
            btnRiwayat.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Riwayat";
            this.pnlFormLoader.Controls.Clear();
            frmRiwayat frmRiwayat = new frmRiwayat() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmRiwayat.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(frmRiwayat);
            frmRiwayat.Show();
        }

        private void btnDashboard_Leave(object sender, EventArgs e)
        {
            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }
        private void btnAkunPasien_Leave(object sender, EventArgs e)
        {
            btnAkunPasien.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnAkunDoc_Leave(object sender, EventArgs e)
        {
            btnDataDoc.BackColor = Color.FromArgb(24, 30, 54);
        }
        private void btnDataObat_Leave(object sender, EventArgs e)
        {
            btnDataObat.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnRiwayat_Leave(object sender, EventArgs e)
        {
            btnRiwayat.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnKeluar_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Close();
        }
        private void pnlNav_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlFormLoader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void frmAdmin_DragEnter(object sender, DragEventArgs e)
        {
            this.Opacity = 0.5;

        }

        private void frmAdmin_ResizeBegin(object sender, EventArgs e)
        {
            this.Opacity = 0.5;

        }

        private void frmAdmin_ResizeEnd(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }
    }
}
