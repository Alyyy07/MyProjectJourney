using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace Layanan_Dokter.FolderPasien
{
    public partial class frmJadwalNewB : Form
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        void IncreaseOpacity(object sender, EventArgs e)
        {
            if (this.Opacity <= 1) this.Opacity += 0.01;
            if (this.Opacity == 1) timer.Stop();
        }
        string kodepasien, idD,idP,koderiwayat,kodedokter;
        public frmJadwalNewB(string kode)
        {
            this.kodepasien = kode;
            InitializeComponent();
        }

        SqlConnection Conn = new SqlConnection(@"Server=ALYYY;Database=aplikasiDokter;Integrated Security = True;");
        SqlCommand Comm = new SqlCommand();
        DataSet dS = new DataSet();
        SqlDataAdapter dA = new SqlDataAdapter();
        DataRow dRow;
        DataTable tblDetail;
        SqlDataReader dRead;

        void IsiSpesialis()
        {
            dA = new SqlDataAdapter("select Id_spesialis,Nama_Spesialisasi from tbl_spesialis ", Conn);
            dA.Fill(dS, "spesialis");
            cmbSpesialis.DataSource = dS.Tables["spesialis"];
            cmbSpesialis.DisplayMember = "Nama_Spesialisasi";
            cmbSpesialis.ValueMember = "Id_spesialis";
            cmbSpesialis.Text = "";
            cmbSpesialis.SelectedValue = "";
        }
        void Clear()
        {
            txtNama.Clear();
            txtKeluhan.Clear();
            pictureBox1.Image = null;
        }
        void AutoNumericRiwayat()
        {
            if (Conn.State == ConnectionState.Closed)
                Conn.Open();
            Comm.Connection = Conn;
            Comm.CommandText = "SELECT * FROM tbl_HBerobat";
            dRead = Comm.ExecuteReader();
            if (dRead.HasRows)
            {
                dRead.Close();
                Comm.CommandText = @"SELECT TOP 1 kode_riwayat
                        FROM tbl_HBerobat ORDER BY kode_riwayat DESC";
                koderiwayat = string.Format("KRB{0,3:00#}",
                    Convert.ToInt32(Comm.ExecuteScalar().ToString().Substring(3)) + 1);
            }
            else
            {
                koderiwayat = "KRB001";
            }
            Conn.Close();
        }
        private void frmJadwalNewB_Load(object sender, EventArgs e)
        {
            AutoNumericRiwayat();
            IsiSpesialis();
            tblDetail = dS.Tables.Add("jadwal");
            tblDetail.Columns.Add("kode_riwayat", Type.GetType("System.String"));
            tblDetail.Columns.Add("tanggaldibuat", Type.GetType("System.String"));
            tblDetail.Columns.Add("Id_dokter", Type.GetType("System.String"));
            tblDetail.Columns.Add("Id_pasien", Type.GetType("System.String"));
            tblDetail.Columns.Add("Keluhan", Type.GetType("System.String"));
            tblDetail.Columns.Add("status", Type.GetType("System.String"));
            tblDetail.Columns.Add("kode_riwayat1", Type.GetType("System.String"));
            tblDetail.Columns.Add("jam", Type.GetType("System.String"));
            tblDetail.Columns.Add("tanggal", Type.GetType("System.String"));

            dA = new SqlDataAdapter("Select * from  tbl_pasien where kode_masuk='"+kodepasien+"'", Conn);
            dA.Fill(dS, "pasien");
            DataColumn[] dC = { dS.Tables["pasien"].Columns["Id_pasien"] };
            dS.Tables["pasien"].PrimaryKey = dC;
            dRow = dS.Tables["pasien"].Rows[0];
            idP = Convert.ToString(dRow[0]);

            dgvData.AutoGenerateColumns = false;

            dA = new SqlDataAdapter(@"Select tbl_HBerobat.kode_riwayat,tbl_HBerobat.[tanggal dibuat],tbl_HBerobat.Id_dokter,tbl_HBerobat.Id_pasien,tbl_DBerobat.Keluhan,
                                    tbl_HBerobat.status,tbl_DBerobat.kode_riwayat,tbl_DBerobat.jam,tbl_DBerobat.tanggal
                                    from tbl_HBerobat inner join tbl_DBerobat on tbl_HBerobat.kode_riwayat = tbl_DBerobat.kode_riwayat
                                    where tbl_HBerobat.Id_pasien='" + idP + "' and status!='Selesai'", Conn);
            dA.Fill(dS, "Berobat");

            this.Opacity = .01;
            timer.Interval = 10;
            timer.Tick += IncreaseOpacity;
            timer.Start();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Apakah Anda yakin ingin Keluar ?", "Pesan", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }


        private void btnDetail_Click(object sender, EventArgs e)
        {
            new frmDetailDokter(kodedokter).Show();
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            
            dA = new SqlDataAdapter("Select * from  tbl_dokter where Spesialis='" + cmbSpesialis.Text + "'", Conn);
            dA.Fill(dS, "dokter");

            if (cmbSpesialis.Text != "")
            {
                dS.Tables["dokter"].Clear();
                txtKeluhan.ReadOnly = false;
                dtpTanggal.Enabled = true;
                dtpJam.Enabled = true;
                btnDetail.Enabled = true;
                btnBuat.Enabled = true;

                dA = new SqlDataAdapter("Select * from  tbl_dokter where Spesialis='" + cmbSpesialis.Text + "'", Conn);
                dA.Fill(dS, "dokter");
                dgvData.DataSource = dS.Tables["dokter"];
            }
            else
            {
                MessageBox.Show("Silahkan Pilih Jenis Spesialis terlebih dahulu !", "Pesan Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKeluhan.ReadOnly = true;
                dtpTanggal.Enabled = false;
                dtpJam.Enabled = false;
                btnDetail.Enabled = false;
                btnBuat.Enabled = false;
                dS.Tables["dokter"].Clear();

            }
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                kodedokter= Convert.ToString(dS.Tables["dokter"].Rows[dgvData.CurrentRow.Index].ItemArray[0]);
                txtNama.Text = Convert.ToString(dS.Tables["dokter"].Rows[dgvData.CurrentRow.Index].ItemArray[1]);
                try
                {
                    byte[] image = (byte[])dS.Tables["dokter"].Rows[dgvData.CurrentRow.Index].ItemArray[9];
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
                AutoNumericRiwayat();
            }
        }

        private void btnBuat_Click(object sender, EventArgs e)
        {
            if (txtNama.Text == "")
            {
                MessageBox.Show("Anda Belum Memilih Dokter !", "Pesan Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbSpesialis.Focus();
            }
            else
            {
                idD = Convert.ToString(dS.Tables["dokter"].Rows[dgvData.CurrentRow.Index].ItemArray[0]);
                DataColumn[] dataCol = { dS.Tables["jadwal"].Columns["kode_riwayat"] };
                dS.Tables["jadwal"].PrimaryKey = dataCol;
                dRow = dS.Tables["jadwal"].Rows.Find(koderiwayat);
                if (dRow == null)
                {
                    dRow = dS.Tables["jadwal"].NewRow();
                    dRow[0] = koderiwayat;
                    dRow[1] = DateTime.Now;
                    dRow[2] = idD;
                    dRow[3] = idP;
                    dRow[4] = txtKeluhan.Text;
                    dRow[5] = "Belum disetujui";
                    dRow[6] = koderiwayat;
                    dRow[7] = dtpJam.Value.TimeOfDay;
                    dRow[8] = dtpTanggal.Value;
                    dS.Tables["jadwal"].Rows.Add(dRow);

                    SqlCommand insCmd = new SqlCommand(@"insert into tbl_HBerobat (kode_riwayat,Id_dokter,Id_pasien,status,[tanggal dibuat])
                                                        values(@kode_riwayat,@Id_dokter,@Id_pasien,@status,@tanggaldibuat)
                                                        insert into tbl_DBerobat(kode_riwayat,jam,Keluhan,tanggal) values(@kode_riwayat,@jam,@Keluhan,@tanggal)", Conn);
                    insCmd.Parameters.Add("@kode_riwayat", SqlDbType.NVarChar, 10, "kode_riwayat");
                    insCmd.Parameters.Add("@tanggaldibuat", SqlDbType.Date, 8,"tanggaldibuat");
                    insCmd.Parameters.Add("@Id_dokter", SqlDbType.NVarChar, 10, "Id_dokter");
                    insCmd.Parameters.Add("@Id_pasien", SqlDbType.NVarChar, 10, "Id_pasien");
                    insCmd.Parameters.Add("@status", SqlDbType.NVarChar, 20, "status");
                    insCmd.Parameters.Add("@jam", SqlDbType.Time, 7, "jam");
                    insCmd.Parameters.Add("@Keluhan", SqlDbType.NVarChar, 200, "Keluhan");
                    insCmd.Parameters.Add("@tanggal", SqlDbType.Date, 20, "tanggal");
                    dA.InsertCommand = insCmd;
                    dA.Update(dS, "jadwal");
                    MessageBox.Show("Jadwal telah berhasil dibuat !", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
                else
                {
                    MessageBox.Show("Jadwal Sudah Ada !", "Pesan Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cmbSpesialis.Focus();
                }
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void frmJadwalNewB_DragEnter(object sender, DragEventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void frmJadwalNewB_ResizeBegin(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void frmJadwalNewB_ResizeEnd(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

    }
}
