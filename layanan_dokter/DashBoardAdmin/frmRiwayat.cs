using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Layanan_Dokter
{
    public partial class frmRiwayat : Form
    {
        public frmRiwayat()
        {
            InitializeComponent();
            btnKunjungan.BackColor = Color.FromArgb(46, 51, 73);

        }

        SqlConnection Conn = new SqlConnection(@"Server=ALYYY;Database=aplikasiDokter;Integrated Security = True;");
        SqlDataAdapter dA = new SqlDataAdapter();
        DataSet dS = new DataSet();
        private void frmRiwayat_Load(object sender, EventArgs e)
        {

            dA = new SqlDataAdapter(@"Select tbl_HBerobat.kode_riwayat,tbl_HBerobat.[tanggal dibuat],tbl_HBerobat.Id_dokter,tbl_dokter.nama_dokter,tbl_dokter.Spesialis,tbl_HBerobat.Id_pasien,tbl_pasien.Nama_pasien,tbl_pasien.Asuransii,tbl_HBerobat.status,
                                    tbl_DBerobat.kode_riwayat,tbl_DBerobat.Diagnosa,tbl_DBerobat.Gejala,tbl_DBerobat.Keluhan,tbl_DBerobat.Id_obat,tbl_DBerobat.Treatment,tbl_DBerobat.jam,
                                    tbl_DBerobat.nama_obat,tbl_DBerobat.tanggal from tbl_HBerobat inner join tbl_DBerobat on tbl_HBerobat.kode_riwayat = tbl_DBerobat.kode_riwayat inner join tbl_dokter
                                    on tbl_HBerobat.Id_dokter = tbl_dokter.Id_dokter inner join tbl_pasien on tbl_HBerobat.Id_pasien = tbl_pasien.Id_pasien where status='Selesai'", Conn);
            dA.Fill(dS, "riwayatBerobat");
            dgvBerobat.DataSource = dS.Tables["riwayatBerobat"];

            dA = new SqlDataAdapter(@"select tbl_HKunjungan.kode_riwayat,tbl_HKunjungan.tanggal_dibuat,tbl_HKunjungan.Id_dokter,tbl_dokter.nama_dokter,tbl_dokter.Spesialis,tbl_HKunjungan.Id_pasien,tbl_pasien.Nama_pasien,tbl_pasien.Asuransii,tbl_HKunjungan.status,
                                    tbl_DKunjungan.Kode_riwayat,tbl_DKunjungan.Jam,tbl_DKunjungan.Keterangan,tbl_DKunjungan.Tujuan,tbl_DKunjungan.tanggal from tbl_HKunjungan
                                    inner join tbl_DKunjungan on tbl_HKunjungan.kode_riwayat=tbl_DKunjungan.Kode_riwayat inner join tbl_dokter
                                    on tbl_HKunjungan.Id_dokter = tbl_dokter.Id_dokter inner join tbl_pasien on tbl_HKunjungan.Id_pasien = tbl_pasien.Id_pasien where status='Disetujui'", Conn);
            dA.Fill(dS, "riwayatKunjungan");
            dgvKunjungan.DataSource = dS.Tables["riwayatKunjungan"];
            
            dgvKunjungan.Visible = true;
            dgvBerobat.Visible = false;
        }

        private void pnlBerobat_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btnKunjungan_Click(object sender, EventArgs e)
        {
            dgvKunjungan.Visible = true;
            dgvBerobat.Visible = false;
            btnKunjungan.BackColor = Color.FromArgb(46, 51, 73);
            btnBerobat.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnBerobat_Click(object sender, EventArgs e)
        {
            dgvBerobat.Visible = true;
            dgvKunjungan.Visible = false;
            btnBerobat.BackColor = Color.FromArgb(46, 51, 73);
            btnKunjungan.BackColor = Color.FromArgb(24, 30, 54);
        }
    }
}
