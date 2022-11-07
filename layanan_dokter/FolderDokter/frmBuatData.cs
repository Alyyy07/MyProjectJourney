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
    public partial class frmBuatData : Form
    {
        string kodedokter;
        public frmBuatData(string kode)
        {
            this.kodedokter = kode;
            InitializeComponent();
        }

        private void frmBuatData_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.pnlFrmLoader.Controls.Clear();
            frmIsiData  dataDiri = new frmIsiData(kodedokter) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            dataDiri.FormBorderStyle = FormBorderStyle.None;
            this.pnlFrmLoader.Controls.Add(dataDiri);
            dataDiri.Show();
        }

        private void pnlFrmLoader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
