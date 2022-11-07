using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Layanan_Dokter.FolderPasien
{
    public partial class frmRiwayatPasien : Form
    {
        string kodepasien,id;
        public frmRiwayatPasien(string kode)
        {
            this.kodepasien = kode;
            InitializeComponent();
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
            dtpTglB.Text = "";
            dtpTglTemuB.Text = "";
            txtAsuransiB.Clear();
            txtNamaDokterB.Clear();
            txtSpesialisB.Clear();
        }
        void Clear()
        {
            txtNamaK.Clear();
            txtAsuransiK.Clear();
            txtNamaDokterK.Clear();
            txtSpesialisK.Clear();
            txtTujuan.Clear();
            txtKet.Clear();
        }
        SqlConnection Conn = new SqlConnection(@"Server=ALYYY;Database=aplikasiDokter;Integrated Security = True;");
        SqlDataAdapter dA = new SqlDataAdapter();
        DataSet dS = new DataSet();
        DataRow dRow;
        private void frmRiwayatPasien_Load(object sender, EventArgs e)
        {
            dA = new SqlDataAdapter(@"select * from tbl_pasien where kode_masuk='" + kodepasien+ "'", Conn);
            dA.Fill(dS, "pasien");
            DataColumn[] dc = { dS.Tables["pasien"].Columns["Id_pasien"] };
            dS.Tables["pasien"].PrimaryKey = dc;
            dRow = dS.Tables["pasien"].Rows[0];
            id = Convert.ToString(dRow[0]);

            dA = new SqlDataAdapter(@"Select tbl_HBerobat.kode_riwayat,tbl_HBerobat.[tanggal dibuat],tbl_HBerobat.Id_dokter,tbl_dokter.nama_dokter,tbl_dokter.Spesialis,tbl_HBerobat.Id_pasien,tbl_pasien.Nama_pasien,tbl_pasien.Asuransii,tbl_HBerobat.status,
                                    tbl_DBerobat.kode_riwayat,tbl_DBerobat.Diagnosa,tbl_DBerobat.Gejala,tbl_DBerobat.Keluhan,tbl_DBerobat.Id_obat,tbl_DBerobat.Treatment,tbl_DBerobat.jam,
                                    tbl_DBerobat.nama_obat,tbl_DBerobat.tanggal from tbl_HBerobat inner join tbl_DBerobat on tbl_HBerobat.kode_riwayat = tbl_DBerobat.kode_riwayat inner join tbl_dokter
                                    on tbl_HBerobat.Id_dokter = tbl_dokter.Id_dokter inner join tbl_pasien on tbl_HBerobat.Id_pasien = tbl_pasien.Id_pasien where tbl_HBerobat.Id_pasien='"+id+"' and status='Selesai'", Conn);
            dA.Fill(dS, "riwayatBerobat");
            dgvBerobat.DataSource = dS.Tables["riwayatBerobat"];

            dA = new SqlDataAdapter(@"select tbl_HKunjungan.kode_riwayat,tbl_HKunjungan.tanggal_dibuat,tbl_HKunjungan.Id_dokter,tbl_dokter.nama_dokter,tbl_dokter.Spesialis,tbl_HKunjungan.Id_pasien,tbl_pasien.Nama_pasien,tbl_pasien.Asuransii,tbl_HKunjungan.status,
                                    tbl_DKunjungan.Kode_riwayat,tbl_DKunjungan.Jam,tbl_DKunjungan.Keterangan,tbl_DKunjungan.Tujuan,tbl_DKunjungan.tanggal from tbl_HKunjungan
                                    inner join tbl_DKunjungan on tbl_HKunjungan.kode_riwayat=tbl_DKunjungan.Kode_riwayat inner join tbl_dokter
                                    on tbl_HKunjungan.Id_dokter = tbl_dokter.Id_dokter inner join tbl_pasien on tbl_HKunjungan.Id_pasien = tbl_pasien.Id_pasien where tbl_HKunjungan.Id_pasien='"+id+"'and status='Disetujui'", Conn);
            dA.Fill(dS, "riwayatKunjungan");
            dgvKunjungan.DataSource = dS.Tables["riwayatKunjungan"];

            btnKunjungan.BackColor = Color.FromArgb(133, 244, 255);
            btnBerobat.BackColor = Color.FromArgb(60, 140, 170);
            pnlKunjungan.Visible = true;
            pnlBerobat.Visible = false;
        }
        private void dgvBerobat_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
            dtpTglB.Text = Convert.ToString(dS.Tables["riwayatBerobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[1]);
            dtpJamB.Text = Convert.ToString(dS.Tables["riwayatBerobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[15]);
            txtNamaDokterB.Text = Convert.ToString(dS.Tables["riwayatBerobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[3]);
            txtSpesialisB.Text = Convert.ToString(dS.Tables["riwayatBerobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[4]);
            txtNamaB.Text = Convert.ToString(dS.Tables["riwayatBerobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[6]);
            txtAsuransiB.Text = Convert.ToString(dS.Tables["riwayatBerobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[7]);
            txtDiagnosa.Text = Convert.ToString(dS.Tables["riwayatBerobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[10]);
            txtGejala.Text = Convert.ToString(dS.Tables["riwayatBerobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[11]);
            txtKeluhan.Text = Convert.ToString(dS.Tables["riwayatBerobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[12]);
            txtTreatment.Text = Convert.ToString(dS.Tables["riwayatBerobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[14]);
            txtObat.Text = Convert.ToString(dS.Tables["riwayatBerobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[16]);
            dtpTglTemuB.Text = Convert.ToString(dS.Tables["riwayatBerobat"].Rows[dgvBerobat.CurrentRow.Index].ItemArray[17]);
            }
            catch
            {
                ClearB();
                dgvBerobat.Focus();
            }
        }
        private void dgvKunjungan_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                dtpTglK.Text = Convert.ToString(dS.Tables["riwayatKunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[1]);
                dtpJamK.Text = Convert.ToString(dS.Tables["riwayatKunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[10]);
                txtNamaDokterK.Text = Convert.ToString(dS.Tables["riwayatKunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[3]);
                txtSpesialisK.Text = Convert.ToString(dS.Tables["riwayatKunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[4]);
                txtNamaK.Text = Convert.ToString(dS.Tables["riwayatKunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[6]);
                txtAsuransiK.Text = Convert.ToString(dS.Tables["riwayatKunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[7]);
                txtKet.Text = Convert.ToString(dS.Tables["riwayatKunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[11]);
                txtTujuan.Text = Convert.ToString(dS.Tables["riwayatKunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[12]);
                dtpTglTemuK.Text = Convert.ToString(dS.Tables["riwayatKunjungan"].Rows[dgvKunjungan.CurrentRow.Index].ItemArray[13]);
            }
            catch
            {
                Clear();
                dgvKunjungan.Focus();
            }
        }
        private void btnKunjungan_Click(object sender, EventArgs e)
        {
            btnKunjungan.BackColor = Color.FromArgb(133, 244, 255);
            btnBerobat.BackColor = Color.FromArgb(60, 140, 170);
            pnlKunjungan.Visible = true;
            pnlBerobat.Visible = false;
            dgvKunjungan.Visible = true;
            pnlKunjungan.Location = pnlBerobat.Location;
            dgvBerobat.Visible = false;
        }

        private void btnBerobat_Click(object sender, EventArgs e)
        {
            btnKunjungan.BackColor = Color.FromArgb(60, 140, 170);
            btnBerobat.BackColor = Color.FromArgb(133, 244, 255);
            pnlKunjungan.Visible = false;
            pnlBerobat.Visible = true;
            dgvBerobat.Visible = true;
            pnlBerobat.Location = pnlKunjungan.Location;
            dgvKunjungan.Visible = false;
        }

        private void pnlKunjungan_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtKet_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
