using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Layanan_Dokter.FolderPasien
{
    public partial class frmDataDiri : Form
    {
        string kode,id,photopath;
        byte[] binaryphoto;
        public frmDataDiri(string kode)
        {
            InitializeComponent();
            this.kode = kode;
        }


        SqlConnection Conn = new SqlConnection(@"Server=ALYYY;Database=aplikasiDokter;Integrated Security = True;");
        SqlCommand Comm = new SqlCommand();
        DataSet dS = new DataSet();
        SqlDataAdapter dA = new SqlDataAdapter();
        DataRow dRow;

        private void frmDataDiri_Load(object sender, EventArgs e)
        { 
            dA = new SqlDataAdapter(@"select * from tbl_pasien where kode_masuk='"+kode+"'", Conn);
            dA.Fill(dS, "pasien");
            DataColumn[] dc = { dS.Tables["pasien"].Columns["Id_pasien"] };
            dS.Tables["pasien"].PrimaryKey = dc;
            dRow = dS.Tables["pasien"].Rows[0];
            id = Convert.ToString(dRow[0]);
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
            dRow = dS.Tables["pasien"].Rows.Find(id);
            txtAsuransi.Text = Convert.ToString(dRow[10]);
            new frmBuatDatadiri(kode).Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNama.Text == "" || txtAlamat.Text == "" || txtNoTlp.Text == "" || txtTmptLahir.Text == "" || txtUsia.Text == "" || txtPekerjaan.Text == "" || cmbJnsKelamin.Text == "")
            {
                MessageBox.Show("Masukkan Data diri Anda dengan benar", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                dRow.BeginEdit();
                dRow[2] = txtNama.Text;
                dRow[3] = cmbJnsKelamin.Text;
                dRow[4] = dtpTglLahir.Value;
                dRow[5] = txtTmptLahir.Text;
                dRow[6] = txtUsia.Text;
                dRow[7] = txtNoTlp.Text;
                dRow[8] = txtAlamat.Text;
                dRow[11] = txtPekerjaan.Text;
                dRow[12] = binaryphoto;
                dRow.EndEdit();
            SqlCommandBuilder cmd = new SqlCommandBuilder(dA);
                dA.Update(dS, "pasien");
                frmPasien pasien = new frmPasien(kode);
                pasien.Show();
                this.Close();

            }
        }

        private void dtpTglLahir_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan usia = DateTime.Now - dtpTglLahir.Value;
            txtUsia.Text = string.Format("{0}", Math.Floor(Convert.ToDouble(usia.Days) / 365));
        }

        private void frmDataDiri_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void frmDataDiri_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void pnlFrmLoader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "jpg|*.jpg|Jpeg|*.Jpeg|png|*.png|Gif|*.Gif|Bitmaps|*.Bitmaps";
            file.Title = "Silahkan pilih foto yang ingin di upload";
            if (file.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(file.OpenFile());
                photopath = file.FileName;
                FileStream fs = new FileStream(photopath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                binaryphoto = br.ReadBytes((int)fs.Length);
                fs.Close();
            }
        }
    }
}
