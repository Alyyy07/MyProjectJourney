using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Layanan_Dokter.FolderPasien
{
    public partial class frmBuatDatadiri : Form
    {
        string kodepasien;
        public frmBuatDatadiri(string kode)
        {
            InitializeComponent();
            this.kodepasien = kode;
        }

        private void Datadiri_Click(object sender, EventArgs e)
        {
          
        }

        private void btnJadwal_Click(object sender, EventArgs e)
        {

        }

        private void btnRiwayat_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.pnlFormLoader.Controls.Clear();
            frmDataDiri dataDiri = new frmDataDiri(kodepasien) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            dataDiri.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(dataDiri);
            dataDiri.Show();
        }

        private void frmBuatDatadiri_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pnlFormLoader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
