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
    public partial class frmJadwal : Form
    {
        string kodedokter,koderiwayat,idD,idP;
        public frmJadwal(string kode)
        {
            this.kodedokter = kode;
            InitializeComponent();
        }

        SqlConnection Conn = new SqlConnection(@"Server=ALYYY;Database=aplikasiDokter;Integrated Security = True;");
        SqlDataAdapter dA = new SqlDataAdapter();
        DataSet dS = new DataSet();
        DataRow dRow;
        DataTable tblDetail;

        void IsiNamaObat()
        {
                dA = new SqlDataAdapter("select Id_Obat,Nama_Obat from tbl_obat ", Conn);
                dA.Fill(dS, "obat");
                cmbObat.DataSource = dS.Tables["obat"];
                cmbObat.DisplayMember = "Nama_Obat";
                cmbObat.ValueMember = "Id_Obat";
                cmbObat.Text = "";
                cmbObat.SelectedValue = "";
        }
        void ClearB()
        {
            txtNamaB.Clear();
            txtDiagnosa.Clear();
            txtGejala.Clear();
            txtKeluhan.Clear();
            txtObat.Clear();
            txtTreatment.Clear();
            dtpJamB.Text = "";
            dtpTanggalB.Text = "";
        }
        void Clear()
        {
            txtNama.Clear();
            txtTujuan.Clear();
            txtKet.Clear();
            pictureBox1.Image = null;
        }

        private void frmJadwal_Load(object sender, EventArgs e)
        {
            IsiNamaObat();
            tblDetail = dS.Tables.Add("jadwal");
            tblDetail.Columns.Add("kode_riwayat", Type.GetType("System.String"));
            tblDetail.Columns.Add("Treatment", Type.GetType("System.String"));
            tblDetail.Columns.Add("Diagnosa", Type.GetType("System.String"));
            tblDetail.Columns.Add("Gejala", Type.GetType("System.String"));
            tblDetail.Columns.Add("NamaObat", Type.GetType("System.String"));
            tblDetail.Columns.Add("status", Type.GetType("System.String"));

            dA = new SqlDataAdapter("Select * from  tbl_dokter where kode_masuk='" + kodedokter+ "'", Conn);
            dA.Fill(dS, "dokter");
            DataColumn[] dC = { dS.Tables["dokter"].Columns["Id_dokter"] };
            dS.Tables["dokter"].PrimaryKey = dC;
            dRow = dS.Tables["dokter"].Rows[0];
            idD = Convert.ToString(dRow[0]);


            dA = new SqlDataAdapter(@"Select tbl_HBerobat.kode_riwayat,tbl_HBerobat.[tanggal dibuat],tbl_HBerobat.Id_dokter,tbl_dokter.nama_dokter,tbl_dokter.Spesialis,tbl_HBerobat.Id_pasien,tbl_pasien.Nama_pasien,tbl_pasien.Asuransii,tbl_HBerobat.status,
                                    tbl_DBerobat.kode_riwayat,tbl_DBerobat.Diagnosa,tbl_DBerobat.Gejala,tbl_DBerobat.Keluhan,tbl_DBerobat.Id_obat,tbl_DBerobat.Treatment,tbl_DBerobat.jam,
                                    tbl_DBerobat.nama_obat,tbl_DBerobat.tanggal from tbl_HBerobat inner join tbl_DBerobat on tbl_HBerobat.kode_riwayat = tbl_DBerobat.kode_riwayat inner join tbl_dokter
                                    on tbl_HBerobat.Id_dokter = tbl_dokter.Id_dokter inner join tbl_pasien on tbl_HBerobat.Id_pasien = tbl_pasien.Id_pasien where tbl_HBerobat.Id_dokter='" +idD+ "' and status!='Selesai'", Conn);
            dA.Fill(dS, "Berobat");
            dgvBerobat.DataSource = dS.Tables["Berobat"];

            dA = new SqlDataAdapter(@"select tbl_HKunjungan.kode_riwayat,tbl_HKunjungan.tanggal_dibuat,tbl_HKunjungan.Id_dokter,tbl_dokter.nama_dokter,tbl_dokter.Spesialis,tbl_HKunjungan.Id_pasien,tbl_pasien.Nama_pasien,tbl_pasien.Asuransii,tbl_pasien.foto,tbl_HKunjungan.status,
                                    tbl_DKunjungan.Kode_riwayat,tbl_DKunjungan.Jam,tbl_DKunjungan.Keterangan,tbl_DKunjungan.Tujuan,tbl_DKunjungan.tanggal from tbl_HKunjungan
                                    inner join tbl_DKunjungan on tbl_HKunjungan.kode_riwayat=tbl_DKunjungan.Kode_riwayat inner join tbl_dokter
                                    on tbl_HKunjungan.Id_dokter = tbl_dokter.Id_dokter inner join tbl_pasien on tbl_HKunjungan.Id_pasien = tbl_pasien.Id_pasien where tbl_HKunjungan.Id_dokter='" + idD + "'and status!='Disetujui'", Conn);
            dA.Fill(dS, "Kunjungan");
            dgvKunjungan.DataSource = dS.Tables["Kunjungan"];

            btnKunjungan.BackColor = Color.FromArgb(133, 244, 255);
            btnBerobat.BackColor = Color.FromArgb(60, 140, 170);
            pnlKunjungan.Visible = true;
            pnlBerobat.Visible = false;
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void pnlBerobat_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnKunjungan_Click(object sender, EventArgs e)
        {

            btnKunjungan.BackColor = Color.FromArgb(133, 244, 255);
            btnBerobat.BackColor = Color.FromArgb(60, 140, 170);
            pnlKunjungan.Visible = true;
            pnlBerobat.Visible = false;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtNamaB.Text == "")
            {
                MessageBox.Show("Silahkan pilih dokter terlebih dahulu !", "Pesan Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dgvBerobat.Focus();
            }
            else
            {
                DataColumn[] dataCol = { dS.Tables["jadwal"].Columns["kode_riwayat"] };
                dS.Tables["jadwal"].PrimaryKey = dataCol;
                dRow = dS.Tables["jadwal"].Rows.Find(koderiwayat);
                if (dRow == null)
                {
                    dRow = dS.Tables["jadwal"].NewRow();
                    dRow[0] = koderiwayat;
                    dRow[1] = txtTreatment.Text;
                    dRow[2] = txtDiagnosa.Text;
                    dRow[3] = txtGejala.Text;
                    dRow[4] = txtObat.Text;
                    dRow[5] = "Selesai";
                    dS.Tables["jadwal"].Rows.Add(dRow);

                    SqlCommand insCmd = new SqlCommand(@"update tbl_HBerobat set status = @status where kode_riwayat=@kode_riwayat
                                                         update tbl_DBerobat set  Treatment=@Treatment,Diagnosa=@Diagnosa,Gejala=@Gejala,nama_obat=@NamaObat where kode_riwayat=@kode_riwayat", Conn);
                    insCmd.Parameters.Add("@kode_riwayat", SqlDbType.NVarChar, 10, "kode_riwayat");
                    insCmd.Parameters.Add("@status", SqlDbType.NVarChar, 20, "status");
                    insCmd.Parameters.Add("@Treatment", SqlDbType.NVarChar, 200, "Treatment");
                    insCmd.Parameters.Add("@Diagnosa", SqlDbType.NVarChar, 200, "Diagnosa");
                    insCmd.Parameters.Add("@Gejala", SqlDbType.NVarChar, 200, "Gejala");
                    insCmd.Parameters.Add("@NamaObat", SqlDbType.NVarChar, 200, "NamaObat");
                    dA.InsertCommand = insCmd;
                    dA.Update(dS, "jadwal");
                    MessageBox.Show("Jadwal telah berhasil dibuat !", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearB();

                    dS.Tables["Berobat"].Clear();
                    dA = new SqlDataAdapter(@"Select tbl_HBerobat.kode_riwayat,tbl_HBerobat.[tanggal dibuat],tbl_HBerobat.Id_dokter,tbl_dokter.nama_dokter,tbl_dokter.Spesialis,tbl_HBerobat.Id_pasien,tbl_pasien.Nama_pasien,tbl_pasien.Asuransii,tbl_HBerobat.status,
                                    tbl_DBerobat.kode_riwayat,tbl_DBerobat.Diagnosa,tbl_DBerobat.Gejala,tbl_DBerobat.Keluhan,tbl_DBerobat.Id_obat,tbl_DBerobat.Treatment,tbl_DBerobat.jam,
                                    tbl_DBerobat.nama_obat,tbl_DBerobat.tanggal from tbl_HBerobat inner join tbl_DBerobat on tbl_HBerobat.kode_riwayat = tbl_DBerobat.kode_riwayat inner join tbl_dokter
                                    on tbl_HBerobat.Id_dokter = tbl_dokter.Id_dokter inner join tbl_pasien on tbl_HBerobat.Id_pasien = tbl_pasien.Id_pasien where tbl_HBerobat.Id_dokter='" + idD + "' and status!='Selesai'", Conn);
                    dA.Fill(dS, "Berobat");
                    dgvBerobat.DataSource = dS.Tables["Berobat"];
                }
                else
                {
                    MessageBox.Show("Jadwal Tidak Ada!", "Pesan Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dgvBerobat.Focus();
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (cmbObat.Text != "") txtObat.Text += cmbObat.Text+" ";
            else MessageBox.Show("Silahkan Pilih Obat terlebiih dahulu !", "Pesan Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (txtNamaB.Text == "")
            {
                MessageBox.Show("Silahkan pilih dokter terlebih dahulu !", "Pesan Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dgvBerobat.Focus();
            }
            else
            {
                DataColumn[] dataCol = { dS.Tables["Berobat"].Columns["kode_riwayat"] };
                dS.Tables["Berobat"].PrimaryKey = dataCol;
                dRow = dS.Tables["Berobat"].Rows.Find(koderiwayat);
                if (dRow != null)
                {
                    dRow.Delete();
                    SqlCommand dltCmd = new SqlCommand(@"delete tbl_HBerobat where kode_riwayat=@kode_riwayat
                                                         delete tbl_DBerobat where kode_riwayat=@kode_riwayat",Conn);
                    dltCmd.Parameters.Add("@kode_riwayat", SqlDbType.NVarChar, 10, "kode_riwayat");
                    dA.DeleteCommand = dltCmd;
                    dA.Update(dS, "Berobat");
                    MessageBox.Show("Jadwal Berhasil Dihapus", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearB();
                }
                else
                {
                    MessageBox.Show("Jadwal Tidak Ada", "Pesan Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgvBerobat.Focus();
                }
            }
        }

        private void dgvKunjungan_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                koderiwayat = Convert.ToString(dS.Tables["Kunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[0]);
                idD = Convert.ToString(dS.Tables["Kunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[2]);
                idP = Convert.ToString(dS.Tables["Kunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[5]);
                txtNama.Text = Convert.ToString(dS.Tables["Kunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[6]);
                dtpTanggal.Text = Convert.ToString(dS.Tables["Kunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[14]);
                dtpJam.Text = Convert.ToString(dS.Tables["Kunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[11]);
                txtTujuan.Text = Convert.ToString(dS.Tables["Kunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[13]);
                txtKet.Text = Convert.ToString(dS.Tables["Kunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[12]);
                try
                {
                    byte[] image = (byte[])dS.Tables["Kunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[8];
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
            }
            catch
            {
                Clear();
                dgvKunjungan.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNama.Text == "")
            {
                MessageBox.Show("Silahkan pilih dokter terlebih dahulu !", "Pesan Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dgvBerobat.Focus();
            }
            else
            {
                DataColumn[] dataCol = { dS.Tables["Kunjungan"].Columns["kode_riwayat"] };
                dS.Tables["Kunjungan"].PrimaryKey = dataCol;
                dRow = dS.Tables["Kunjungan"].Rows.Find(koderiwayat);
                if (dRow != null)
                {
                    dRow[0] = koderiwayat;
                    dRow[9]="Disetujui";
                    SqlCommand updtCmd = new SqlCommand(@"update tbl_HKunjungan set status = @status where kode_riwayat=@kode_riwayat", Conn);
                    updtCmd.Parameters.Add("@kode_riwayat", SqlDbType.NVarChar, 10, "kode_riwayat");
                    updtCmd.Parameters.Add("@status", SqlDbType.NVarChar, 20, "status");
                    dA.UpdateCommand = updtCmd;
                    dA.Update(dS, "Kunjungan");
                    MessageBox.Show("Jadwal Berhasil Disetujui", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    dS.Tables["Kunjungan"].Clear();

                    dA = new SqlDataAdapter(@"select tbl_HKunjungan.kode_riwayat,tbl_HKunjungan.tanggal_dibuat,tbl_HKunjungan.Id_dokter,tbl_dokter.nama_dokter,tbl_dokter.Spesialis,tbl_HKunjungan.Id_pasien,tbl_pasien.Nama_pasien,tbl_pasien.Asuransii,tbl_pasien.foto,tbl_HKunjungan.status,
                                    tbl_DKunjungan.Kode_riwayat,tbl_DKunjungan.Jam,tbl_DKunjungan.Keterangan,tbl_DKunjungan.Tujuan,tbl_DKunjungan.tanggal from tbl_HKunjungan
                                    inner join tbl_DKunjungan on tbl_HKunjungan.kode_riwayat=tbl_DKunjungan.Kode_riwayat inner join tbl_dokter
                                    on tbl_HKunjungan.Id_dokter = tbl_dokter.Id_dokter inner join tbl_pasien on tbl_HKunjungan.Id_pasien = tbl_pasien.Id_pasien where tbl_HKunjungan.Id_dokter='" + idD + "'and status!='Disetujui'", Conn);
                    dA.Fill(dS, "Kunjungan");
                    dgvKunjungan.DataSource = dS.Tables["Kunjungan"];
                    dgvKunjungan.Focus();
                }
                else
                {
                    MessageBox.Show("Jadwal Tidak Ada", "Pesan Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgvBerobat.Focus();
                }
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (idP == "")
            {
                MessageBox.Show("Silahkan pilih Jadwal terlebih dahulu", "Pesan Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvKunjungan.Focus();
            }
            else
            {
            new frmDetailPasien(idP).Show();
            }
        }

        private void dgvBerobat_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
            koderiwayat = Convert.ToString(dS.Tables["Berobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[0]);
            idD = Convert.ToString(dS.Tables["Berobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[2]);
            txtNamaB.Text = Convert.ToString(dS.Tables["Berobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[6]);
            dtpTanggalB.Text = Convert.ToString(dS.Tables["Berobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[17]);
            dtpJamB.Text = Convert.ToString(dS.Tables["Berobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[15]);
            txtDiagnosa.Text = Convert.ToString(dS.Tables["Berobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[10]);
            txtGejala.Text = Convert.ToString(dS.Tables["Berobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[11]);
            txtKeluhan.Text = Convert.ToString(dS.Tables["Berobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[12]);
            txtTreatment.Text = Convert.ToString(dS.Tables["Berobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[14]);
            txtObat.Text = Convert.ToString(dS.Tables["Berobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[16]);
            }
            catch
            {
                ClearB();
                dgvBerobat.Focus();
            }



        }

        private void btnBerobat_Click(object sender, EventArgs e)
        {
            btnKunjungan.BackColor = Color.FromArgb(60, 140, 170);
            btnBerobat.BackColor =  Color.FromArgb(133, 244, 255);
            pnlKunjungan.Visible = false;
            pnlBerobat.Visible = true;
        }
    }
}
