using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Layanan_Dokter.FolderDokter
{
    public partial class loadingD : Form
    {
        string kodedokter;
        public loadingD(string kode)
        {
            this.kodedokter = kode;
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
                new frmDokter(kodedokter).Show();
                this.Close();
            }
        }

        private void loadingD_Load(object sender, EventArgs e)
        {
            frmDokter dokter = new frmDokter(kodedokter);
            dokter.Close();
            timer1.Start();
        }
    }
}
