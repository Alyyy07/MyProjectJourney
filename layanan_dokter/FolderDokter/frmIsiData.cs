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

namespace Layanan_Dokter.FolderDokter
{
    public partial class frmIsiData : Form
    {
        string kodedokter,id,photopath;
        byte[] binaryphoto;
        public frmIsiData(string kode)
        {
            this.kodedokter = kode;
            InitializeComponent();
        }

        SqlConnection Conn = new SqlConnection(@"Server=ALYYY;Database=aplikasiDokter;Integrated Security = True;");
        SqlCommand Comm = new SqlCommand();
        DataSet dS = new DataSet();
        SqlDataAdapter dA = new SqlDataAdapter();
        DataRow dRow;

        private void dtpJamMasuk_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpJamPulang_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNama.Text == "" || cmbJnsKelamin.Text == "" || dtpJamMasuk.Text == "" || dtpJamPulang.Text == "" || checkedListBox1.CheckedItems == null)
            {
                MessageBox.Show("Masukkan Data diri Anda dengan benar", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                dRow.BeginEdit();
                dRow[1] = txtNama.Text;
                dRow[4] = txtAlamat.Text;
                dRow[5] = txtNoTelp.Text;
                dRow[6] = dtpJamMasuk.Value.TimeOfDay;
                dRow[7] = textBox1.Text;
                dRow[9] = binaryphoto;
                dRow[10] = dtpJamPulang.Value.TimeOfDay;
                dRow[11] = cmbJnsKelamin.Text;
                dRow.EndEdit();
                SqlCommandBuilder cmd = new SqlCommandBuilder(dA);
                    dA.Update(dS, "dokter");
                MessageBox.Show("Anda telah berhasil mengisi data diri !", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                new frmDokter(kodedokter).Show();
                this.Close();

            }
        }

        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
                for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
                {
                    textBox1.Text += " " + (checkedListBox1.CheckedItems[i]);
                }
        }

        private void pnlFormLoader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmIsiData_Load(object sender, EventArgs e)
        {
            dA = new SqlDataAdapter(@"select * from tbl_dokter where kode_masuk='" +kodedokter+ "'", Conn);
            dA.Fill(dS, "dokter");
            DataColumn[] dc = { dS.Tables["dokter"].Columns["Id_dokter"] };
            dS.Tables["dokter"].PrimaryKey = dc;
            dRow = dS.Tables["dokter"].Rows[0];
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
            dRow = dS.Tables["dokter"].Rows.Find(id);
            txtSpesialis.Text = Convert.ToString(dRow[3]);
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
