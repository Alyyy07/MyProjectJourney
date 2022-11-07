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
    public partial class frmJadwal : Form
    {
        string kodepasien,id;
        public frmJadwal(string kode)
        {
            this.kodepasien = kode;
            InitializeComponent();
        }
        SqlConnection Conn = new SqlConnection(@"Server=ALYYY;Database=aplikasiDokter;Integrated Security = True;");
        SqlDataAdapter dA = new SqlDataAdapter();
        DataSet dS = new DataSet();
        DataRow dRow;
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmJadwal_Load(object sender, EventArgs e)
        {
            dA = new SqlDataAdapter("select * from tbl_pasien where kode_masuk='" + kodepasien + "'", Conn);
            dA.Fill(dS, "pasien");
            DataColumn[] dc = { dS.Tables["pasien"].Columns["Id_pasien"] };
            dS.Tables["pasien"].PrimaryKey = dc;
            dRow = dS.Tables["pasien"].Rows[0];
            id = Convert.ToString(dRow[0]);

            dA = new SqlDataAdapter(@"Select tbl_HBerobat.kode_riwayat,tbl_HBerobat.[tanggal dibuat],tbl_HBerobat.Id_dokter,tbl_dokter.nama_dokter,tbl_dokter.Spesialis,tbl_HBerobat.Id_pasien,tbl_pasien.Nama_pasien,tbl_pasien.Asuransii,tbl_HBerobat.status,
                                    tbl_DBerobat.kode_riwayat,tbl_DBerobat.Diagnosa,tbl_DBerobat.Gejala,tbl_DBerobat.Keluhan,tbl_DBerobat.Id_obat,tbl_DBerobat.Treatment,tbl_DBerobat.jam,
                                    tbl_DBerobat.nama_obat,tbl_DBerobat.tanggal from tbl_HBerobat inner join tbl_DBerobat on tbl_HBerobat.kode_riwayat = tbl_DBerobat.kode_riwayat inner join tbl_dokter
                                    on tbl_HBerobat.Id_dokter = tbl_dokter.Id_dokter inner join tbl_pasien on tbl_HBerobat.Id_pasien = tbl_pasien.Id_pasien where tbl_HBerobat.Id_pasien='" + id + "' and status!='Selesai'", Conn);
            dA.Fill(dS, "riwayatBerobat");
            dgvBerobat.DataSource = dS.Tables["riwayatBerobat"];

            dA = new SqlDataAdapter(@"select tbl_HKunjungan.kode_riwayat,tbl_HKunjungan.tanggal_dibuat,tbl_HKunjungan.Id_dokter,tbl_dokter.nama_dokter,tbl_dokter.Spesialis,tbl_HKunjungan.Id_pasien,tbl_pasien.Nama_pasien,tbl_pasien.Asuransii,tbl_HKunjungan.status,
                                    tbl_DKunjungan.Kode_riwayat,tbl_DKunjungan.Jam,tbl_DKunjungan.Keterangan,tbl_DKunjungan.Tujuan,tbl_DKunjungan.tanggal from tbl_HKunjungan
                                    inner join tbl_DKunjungan on tbl_HKunjungan.kode_riwayat=tbl_DKunjungan.Kode_riwayat inner join tbl_dokter
                                    on tbl_HKunjungan.Id_dokter = tbl_dokter.Id_dokter inner join tbl_pasien on tbl_HKunjungan.Id_pasien = tbl_pasien.Id_pasien where tbl_HKunjungan.Id_pasien='" + id + "'and status!='Disetujui" +
                                    "'", Conn);
            dA.Fill(dS, "riwayatKunjungan");
            dgvKunjungan.DataSource = dS.Tables["riwayatKunjungan"];

            btnKunjungan.BackColor = Color.FromArgb(179, 232, 229);
            btnBerobat.BackColor = Color.FromArgb(60, 140, 170);
            dgvKunjungan.Visible = true;
            dgvBerobat.Visible = false;
            btnJdwlKN.Visible = true;
            btnJdwlBN.Visible = false;
        }

        private void btnKunjungan_Click(object sender, EventArgs e)
        {
            dS.Tables["riwayatKunjungan"].Clear();

            dA = new SqlDataAdapter(@"select tbl_HKunjungan.kode_riwayat,tbl_HKunjungan.tanggal_dibuat,tbl_HKunjungan.Id_dokter,tbl_dokter.nama_dokter,tbl_dokter.Spesialis,tbl_HKunjungan.Id_pasien,tbl_pasien.Nama_pasien,tbl_pasien.Asuransii,tbl_HKunjungan.status,
                                    tbl_DKunjungan.Kode_riwayat,tbl_DKunjungan.Jam,tbl_DKunjungan.Keterangan,tbl_DKunjungan.Tujuan,tbl_DKunjungan.tanggal from tbl_HKunjungan
                                    inner join tbl_DKunjungan on tbl_HKunjungan.kode_riwayat=tbl_DKunjungan.Kode_riwayat inner join tbl_dokter
                                    on tbl_HKunjungan.Id_dokter = tbl_dokter.Id_dokter inner join tbl_pasien on tbl_HKunjungan.Id_pasien = tbl_pasien.Id_pasien where tbl_HKunjungan.Id_pasien='" + id + "'and status!='Disetujui'", Conn);
            dA.Fill(dS, "riwayatKunjungan");
            dgvKunjungan.DataSource = dS.Tables["riwayatKunjungan"];

            btnKunjungan.BackColor = Color.FromArgb(179, 232, 229);
            btnBerobat.BackColor = Color.FromArgb(60, 140, 170);
            dgvKunjungan.Visible = true;
            dgvBerobat.Visible = false;
            btnJdwlKN.Visible = true;
            btnJdwlBN.Visible = false;
        }

        private void btnBerobat_Click(object sender, EventArgs e)
        {
            dS.Tables["riwayatBerobat"].Clear();
            dA = new SqlDataAdapter(@"Select tbl_HBerobat.kode_riwayat,tbl_HBerobat.[tanggal dibuat],tbl_HBerobat.Id_dokter,tbl_dokter.nama_dokter,tbl_dokter.Spesialis,tbl_HBerobat.Id_pasien,tbl_pasien.Nama_pasien,tbl_pasien.Asuransii,tbl_HBerobat.status,
                                    tbl_DBerobat.kode_riwayat,tbl_DBerobat.Diagnosa,tbl_DBerobat.Gejala,tbl_DBerobat.Keluhan,tbl_DBerobat.Id_obat,tbl_DBerobat.Treatment,tbl_DBerobat.jam,
                                    tbl_DBerobat.nama_obat,tbl_DBerobat.tanggal from tbl_HBerobat inner join tbl_DBerobat on tbl_HBerobat.kode_riwayat = tbl_DBerobat.kode_riwayat inner join tbl_dokter
                                    on tbl_HBerobat.Id_dokter = tbl_dokter.Id_dokter inner join tbl_pasien on tbl_HBerobat.Id_pasien = tbl_pasien.Id_pasien where tbl_HBerobat.Id_pasien='" + id + "' and status!='Disetujui'", Conn);
            dA.Fill(dS, "riwayatBerobat");
            dgvBerobat.DataSource = dS.Tables["riwayatBerobat"];
            btnKunjungan.BackColor = Color.FromArgb(60, 140, 170);
            btnBerobat.BackColor = Color.FromArgb(179, 232, 229);
            dgvKunjungan.Visible = false;
            dgvBerobat.Visible = true;
            btnJdwlKN.Visible = false;
            btnJdwlBN.Visible = true;
        }

        private void btnJdwlKN_Click(object sender, EventArgs e)
        {
            new frmJadwalNewK(kodepasien).Show();
        }

        private void btnJdwlBN_Click(object sender, EventArgs e)
        {
            new frmJadwalNewB(kodepasien).Show();
        }

        private void dgvKunjungan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
