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
    public partial class loadingP : Form
    {
        string kodepasien;
        public loadingP(string kode)
        {
            this.kodepasien = kode;
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            progressBar1.Increment(3);
            if (progressBar1.Value >= 40)
            {
                progressBar1.Increment(6);
            }
            if (progressBar1.Value >= 150)
            {
                timer1.Enabled = false;
                new frmPasien(kodepasien).Show();
                this.Close();
            }
        }

        private void loadingP_Load(object sender, EventArgs e)
        {
            frmPasien pasien = new frmPasien(kodepasien);
            pasien.Close();
            timer1.Start();
        }
    }
}
