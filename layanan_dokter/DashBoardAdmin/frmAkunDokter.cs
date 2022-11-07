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
    public partial class frmAkunDokter : Form
    {
        public frmAkunDokter()
        {
            InitializeComponent();
        }

        SqlConnection Conn = new SqlConnection(@"Server=ALYYY;Database=aplikasiDokter;Integrated Security = True;");
        SqlCommand Comm = new SqlCommand();
        DataSet dS = new DataSet();
        SqlDataAdapter dA = new SqlDataAdapter();
        DataRow dRow;
        SqlDataReader dRead;
        DataTable tblDetail;
        string Id;

        void clear()
        {
            txtId.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            cmbSpesialis.ResetText();
            autoNumericLoginDokter();
            autoNumericIdDokter();
        }

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

        void autoNumericLoginDokter()
        {
            if (Conn.State == ConnectionState.Closed)
                Conn.Open();
            Comm.Connection = Conn;
            Comm.CommandText = "SELECT * FROM tbl_dokter";
            dRead = Comm.ExecuteReader();
            if (dRead.HasRows)
            {
                dRead.Close();
                Comm.CommandText = @"SELECT TOP 1 kode_masuk 
                        FROM tbl_dokter ORDER BY kode_masuk DESC";
                txtId.Text = string.Format("LD{0,3:00#}",
                    Convert.ToInt32(Comm.ExecuteScalar().
                    ToString().Substring(2)) + 1);
            }
            else
            {
                txtId.Text = "LD001";
            }
            Conn.Close();
        }
        void autoNumericIdDokter()
        {
            if (Conn.State == ConnectionState.Closed)
                Conn.Open();
            Comm.Connection = Conn;
            Comm.CommandText = "SELECT * FROM tbl_dokter";
            dRead = Comm.ExecuteReader();
            if (dRead.HasRows)
            {
                dRead.Close();
                Comm.CommandText = @"SELECT TOP 1 Id_dokter 
                        FROM tbl_dokter ORDER BY Id_dokter DESC";
                Id = string.Format("DOK{0,3:00#}",
                    Convert.ToInt32(Comm.ExecuteScalar().
                    ToString().Substring(3)) + 1);
            }
            else
            {
                Id = "DOK001";
            }
            Conn.Close();
        }
        private void frmAkunDokter_Load(object sender, EventArgs e)
        {
            autoNumericIdDokter();
            tblDetail = dS.Tables.Add("akun");
            tblDetail.Columns.Add("Kode_masuk", Type.GetType("System.String"));
            tblDetail.Columns.Add("Username", Type.GetType("System.String"));
            tblDetail.Columns.Add("Password", Type.GetType("System.String"));
            tblDetail.Columns.Add("status_login", Type.GetType("System.String"));
            tblDetail.Columns.Add("kode_login", Type.GetType("System.String"));
            tblDetail.Columns.Add("Id_spesialis", Type.GetType("System.String"));
            tblDetail.Columns.Add("Spesialis", Type.GetType("System.String"));
            dgvAkun.DataSource = dS.Tables["akunDokter"];

            dA = new SqlDataAdapter(@"SELECT tbl_login.Kode_masuk,tbl_login.Username,tbl_login.Password, tbl_login.status_login,tbl_dokter.kode_masuk AS kode_login,
                                      tbl_dokter.Id_spesialis,tbl_dokter.Spesialis
                                      FROM tbl_login INNER JOIN tbl_dokter ON tbl_login.Kode_masuk = tbl_dokter.kode_masuk", Conn);
            dA.Fill(dS, "akunDokter");
            dgvAkun.AutoGenerateColumns = true;
            dgvAkun.DataSource = dS.Tables["akunDokter"];
            autoNumericLoginDokter();
            IsiSpesialis();
        }
        private void btnSimpan_MouseEnter(object sender, EventArgs e)
        {
            btnSimpan.ForeColor = Color.Black;
        }

        private void btnSimpan_MouseLeave(object sender, EventArgs e)
        {
            btnSimpan.ForeColor = Color.Lime;
        }
        private void btnBatal_MouseEnter(object sender, EventArgs e)
        {
            btnBatal.ForeColor = Color.Black;
        }

        private void btnBatal_MouseLeave(object sender, EventArgs e)
        {
            btnBatal.ForeColor = Color.Lime;
        }

        private void btnUbah_MouseEnter(object sender, EventArgs e)
        {
            btnUbah.ForeColor = Color.Black;
        }

        private void btnUbah_MouseLeave(object sender, EventArgs e)
        {
            btnUbah.ForeColor = Color.Lime;

        }

        private void btnHapus_MouseEnter(object sender, EventArgs e)
        {
            btnHapus.ForeColor = Color.Black;

        }

        private void btnHapus_MouseLeave(object sender, EventArgs e)
        {
            btnHapus.ForeColor = Color.Lime;

        }

        private void dgvAkun_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtId.Text = Convert.ToString(dS.Tables["akunDokter"].Rows[dgvAkun.CurrentRow.Index].ItemArray[0]);
                txtUsername.Text = Convert.ToString(dS.Tables["akunDokter"].Rows[dgvAkun.CurrentRow.Index].ItemArray[1]);
                txtPassword.Text = Convert.ToString(dS.Tables["akunDokter"].Rows[dgvAkun.CurrentRow.Index].ItemArray[2]);
                cmbSpesialis.Text = Convert.ToString(dS.Tables["akunDokter"].Rows[dgvAkun.CurrentRow.Index].ItemArray[6]);
            }
            catch
            {
                clear();
                dgvAkun.Focus();
                autoNumericLoginDokter();
                autoNumericIdDokter();
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtUsername.Text=="" || txtPassword.Text=="" || cmbSpesialis.Text == "")
            {
                MessageBox.Show("Masukkan Data dengan benar !", "Pesan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataColumn[] dCol = { dS.Tables["akunDokter"].Columns["Kode_masuk"] };
                dS.Tables["akunDokter"].PrimaryKey = dCol;
                dRow = dS.Tables["akunDokter"].Rows.Find(txtId.Text);
                if (dRow == null)
                {
                    dRow = dS.Tables["akunDokter"].NewRow();
                    dRow[0] = txtId.Text;
                    dRow[1] = txtUsername.Text;
                    dRow[2] = txtPassword.Text;
                    dRow[3] = "dokter";
                    dRow[4] = txtId.Text;
                    dRow[5] = cmbSpesialis.SelectedValue;
                    dRow[6] = cmbSpesialis.Text;
                    dS.Tables["akunDokter"].Rows.Add(dRow);

                    SqlCommand insCmd = new SqlCommand(@"insert into tbl_login (Kode_masuk,Username,Password,status_login)
                                                        values(@Kode_masuk,@Username,@Password,@status_login)
                                                        insert into tbl_dokter(Id_dokter,kode_masuk,Id_spesialis,Spesialis) values('"+Id+"',@Kode_masuk,@Id_spesialis,@Spesialis)",Conn);
                    insCmd.Parameters.Add("@Kode_masuk", SqlDbType.NVarChar, 10, "Kode_masuk");
                    insCmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50, "Username");
                    insCmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50, "Password");
                    insCmd.Parameters.Add("@status_login", SqlDbType.NVarChar, 10, "status_login");
                    insCmd.Parameters.Add("@Id_spesialis", SqlDbType.NVarChar, 10, "Id_spesialis");
                    insCmd.Parameters.Add("@Spesialis", SqlDbType.NVarChar, 50, "Spesialis");

                    dA.InsertCommand = insCmd;
                    dA.Update(dS, "akunDokter");
                    MessageBox.Show("Data telah berhasil disimpan", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }
                else
                {
                    MessageBox.Show("Data sudah ada", "Pesan Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dgvAkun.Focus();
                }
            }
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtUsername.Text=="" || txtPassword.Text==""|| cmbSpesialis.Text=="")
            {
                MessageBox.Show("Pilih terlebih dahulu data yang ingin diubah", "Pesan Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtId.Focus();
            }
            else
            {
                DataColumn[] dataCol = { dS.Tables["akunDokter"].Columns["Kode_masuk"] };
                dS.Tables["akunDokter"].PrimaryKey = dataCol;
                dRow = dS.Tables["akunDokter"].Rows.Find(txtId.Text);
                if (dRow == null)
                {
                    MessageBox.Show("Data yang ingin anda ubah belum terdaftar di Database", "Pesan Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtId.Focus();
                }
                else
                {
                    dRow.BeginEdit();
                    dRow[0] = txtId.Text;
                    dRow[1] = txtUsername.Text;
                    dRow[2] = txtPassword.Text;
                    dRow[3] = "dokter";
                    dRow[4] = txtId.Text;
                    dRow[5] = cmbSpesialis.SelectedValue;
                    dRow[6] = cmbSpesialis.Text;
                    dRow.EndEdit();
                    SqlCommand updtCmd = new SqlCommand(@"update tbl_login set Username=@Username, Password=@Password
                                                          where Kode_masuk=@Kode_masuk
                                                        update tbl_dokter set Id_spesialis = @Id_spesialis, Spesialis = @Spesialis where kode_masuk = @kode_masuk", Conn);
                    updtCmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50, "Username");
                    updtCmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50, "Password");
                    updtCmd.Parameters.Add("@Kode_masuk", SqlDbType.NVarChar, 10, "Kode_masuk");
                    updtCmd.Parameters.Add("@Id_spesialis", SqlDbType.NVarChar, 10, "Id_spesialis");
                    updtCmd.Parameters.Add("@Spesialis", SqlDbType.NVarChar,50, "Spesialis");
                    dA.UpdateCommand = updtCmd;
                    dA.Update(dS, "akunDokter");
                    MessageBox.Show("Data telah berhasil diubah", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Pilih terlebih dahulu data yang ingin dihapus", "Pesan Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtId.Focus();
            }
            else
            {
                DataColumn[] dataCol = { dS.Tables["akunDokter"].Columns["Kode_masuk"] };
                dS.Tables["akunDokter"].PrimaryKey = dataCol;
                dRow = dS.Tables["akunDokter"].Rows.Find(txtId.Text);
                if (dRow == null)
                {
                    MessageBox.Show("Data yang ingin anda hapus belum terdaftar di Database", "Pesan Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtId.Focus();
                }
                else
                {
                    DialogResult result;
                    result = MessageBox.Show("Apakah Anda yakin ingin menghapus Data ini ?", "Pesan", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.OK)
                    {
                        dRow.Delete();
                        SqlCommand dltCmd = new SqlCommand(@"delete from tbl_login where Kode_masuk=@Kode_masuk
                                                             delete from tbl_dokter where kode_masuk=@Kode_masuk", Conn);
                        dltCmd.Parameters.Add("@Kode_masuk", SqlDbType.NVarChar, 10, "Kode_masuk");
                        dA.DeleteCommand = dltCmd;
                        dA.Update(dS, "akunDokter");
                        MessageBox.Show("Data telah berhasil dihapus", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                }
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkShowPass.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }
    }
}
