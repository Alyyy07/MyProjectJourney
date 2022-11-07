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
    public partial class frmEdit : Form
    {
        string kodepasien, id, photopath;
        byte[] binaryphoto;
        public frmEdit(string kode)
        {
            InitializeComponent();
            this.kodepasien = kode;
        }


        SqlConnection Conn = new SqlConnection(@"Server=ALYYY;Database=aplikasiDokter;Integrated Security = True;");
        SqlCommand Comm = new SqlCommand();
        DataSet dS = new DataSet();
        SqlDataAdapter dA = new SqlDataAdapter();
        DataRow dRow;
        private void frmEdit_Load(object sender, EventArgs e)
        {
            btnSimpan.Visible = false;

            dA = new SqlDataAdapter(@"select * from tbl_pasien where kode_masuk='" + kodepasien + "'", Conn);
            dA.Fill(dS, "pasien");
            DataColumn[] dc = { dS.Tables["pasien"].Columns["Id_pasien"] };
            dS.Tables["pasien"].PrimaryKey = dc;
            dRow = dS.Tables["pasien"].Rows[0];
            id = Convert.ToString(dRow[0]);
            dRow = dS.Tables["pasien"].Rows.Find(id);
            txtNama.Text = Convert.ToString(dRow[2]);
            cmbJnsKelamin.Text = Convert.ToString(dRow[3]);
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

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Apakah Anda yakin ingin Menyimpan Data ?", "Pesan", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                if (txtNama.Text == "" || txtAlamat.Text == "" || txtNoTlp.Text == "" || txtTmptLahir.Text == "" || txtUsia.Text == "" || txtPekerjaan.Text == "" || cmbJnsKelamin.Text == "" || pictureBox1.Image == null)
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
                    MessageBox.Show("Anda telah berhasil mengisi data diri !", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtNama.ReadOnly = true;
                    txtAlamat.ReadOnly = true;
                    txtNoTlp.ReadOnly = true;
                    txtPekerjaan.ReadOnly = true;
                    txtTmptLahir.ReadOnly = true;
                    txtUsia.ReadOnly = true;
                    cmbJnsKelamin.Enabled = false;
                    dtpTglLahir.Enabled = false;
                    btnSimpan.Visible = false;
                    btnEdit.Visible = true;
                    btnUpload.Enabled = false;
                }
            }
        }

        private void dtpTglLahir_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan usia = DateTime.Now - dtpTglLahir.Value;
            txtUsia.Text = string.Format("{0}", Math.Floor(Convert.ToDouble(usia.Days) / 365));
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Apakah Anda yakin ingin Mengedit Data diri Anda ?", "Pesan", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                txtNama.ReadOnly = false;
                txtAlamat.ReadOnly = false;
                txtNoTlp.ReadOnly = false;
                txtPekerjaan.ReadOnly = false;
                txtTmptLahir.ReadOnly = false;
                txtUsia.ReadOnly = false;
                cmbJnsKelamin.Enabled = true;
                dtpTglLahir.Enabled = true;
                btnSimpan.Visible = true;
                btnEdit.Visible = false;
                btnUpload.Enabled = true;
            }
        }
    }
}
