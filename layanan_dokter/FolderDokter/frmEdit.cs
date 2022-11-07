using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Layanan_Dokter.FolderDokter
{
    public partial class frmEdit : Form
    {

        string kodedokter, id, photopath;
        byte[] binaryphoto;
        public frmEdit(string kode)
        {
            InitializeComponent();
            this.kodedokter = kode;
        }

        SqlConnection Conn = new SqlConnection(@"Server=ALYYY;Database=aplikasiDokter;Integrated Security = True;");
        SqlCommand Comm = new SqlCommand();
        DataSet dS = new DataSet();
        SqlDataAdapter dA = new SqlDataAdapter();
        DataRow dRow;
        private void frmEdit_Load(object sender, EventArgs e)
        {
            btnSimpan.Visible = false;

            dA = new SqlDataAdapter(@"select * from tbl_dokter where kode_masuk='" + kodedokter + "'", Conn);
            dA.Fill(dS, "dokter");
            DataColumn[] dc = { dS.Tables["dokter"].Columns["Id_dokter"] };
            dS.Tables["dokter"].PrimaryKey = dc;
            dRow = dS.Tables["dokter"].Rows[0];
            id = Convert.ToString(dRow[0]);
            txtNama.Text = Convert.ToString(dRow[1]);
            txtSpesialis.Text = Convert.ToString(dRow[3]);
            txtAlamat.Text = Convert.ToString(dRow[4]);
            txtNoTelp.Text = Convert.ToString(dRow[5]);
            dtpJamMasuk.Value = Convert.ToDateTime(dRow[6]);
            textBox1.Text = Convert.ToString(dRow[7]);
            cmbJnsKelamin.Text = Convert.ToString(dRow[11]);
            string[] hari = textBox1.Text.Split(' ');
            try
            {
                byte[] image = (byte[])dRow[9];
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
            for (int a = 0; a < hari.Length; a++)
            {
                if (checkBox1.Text == hari[a])
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Text == hari[a])
                {
                    checkBox2.Checked = true;
                }
                if (checkBox3.Text == hari[a])
                {
                    checkBox3.Checked = true;
                }
                if (checkBox4.Text == hari[a])
                {
                    checkBox4.Checked = true;
                }
                if (checkBox5.Text == hari[a])
                {
                    checkBox5.Checked = true;
                }
                if (checkBox6.Text == hari[a])
                {
                    checkBox6.Checked = true;
                }
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Apakah Anda yakin ingin Menyimpan Data ?", "Pesan", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
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
                    MessageBox.Show("Anda telah berhasil mengubah data diri !", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNama.ReadOnly = true;
                    txtAlamat.ReadOnly = true;
                    txtNoTelp.ReadOnly = true;
                    txtSpesialis.ReadOnly = true;
                    cmbJnsKelamin.Enabled = false;
                    btnUpload.Enabled = false;
                    dtpJamMasuk.Enabled = false;
                    dtpJamPulang.Enabled = false;
                    btnEdit.Visible = true;
                    checkedListBox1.Visible = false;
                    btnSimpan.Visible = true;
                    checkBox1.Visible = true;
                    checkBox2.Visible = true;
                    checkBox3.Visible = true;
                    checkBox4.Visible = true;
                    checkBox5.Visible = true;
                    checkBox6.Visible = true;

                    string[] hari = textBox1.Text.Split(' ');
                    for (int a = 0; a < hari.Length; a++)
                    {
                        if (checkBox1.Text == hari[a])
                        {
                            checkBox1.Checked = true;
                        }
                        if (checkBox2.Text == hari[a])
                        {
                            checkBox2.Checked = true;
                        }
                        if (checkBox3.Text == hari[a])
                        {
                            checkBox3.Checked = true;
                        }
                        if (checkBox4.Text == hari[a])
                        {
                            checkBox4.Checked = true;
                        }
                        if (checkBox5.Text == hari[a])
                        {
                            checkBox5.Checked = true;
                        }
                        if (checkBox6.Text == hari[a])
                        {
                            checkBox6.Checked = true;
                        }
                    }
                }
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
                txtNoTelp.ReadOnly = false;
                txtSpesialis.ReadOnly = false;
                cmbJnsKelamin.Enabled = true;
                btnUpload.Enabled = true;
                dtpJamMasuk.Enabled = true;
                dtpJamPulang.Enabled = true;
                checkedListBox1.Visible = true;
                checkBox1.Visible = false;
                checkBox2.Visible = false;
                checkBox3.Visible = false;
                checkBox4.Visible = false;
                checkBox5.Visible = false;
                checkBox6.Visible = false;
                btnSimpan.Visible = true;
                btnEdit.Visible = false;

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
