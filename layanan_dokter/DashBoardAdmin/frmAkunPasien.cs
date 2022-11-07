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
    public partial class frmAkunPasien : Form
    {
        public frmAkunPasien()
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
            cmbAsuransi.ResetText();
            autoNumericLoginPasien();
            autoNumericIdPasien();
        }

        void IsiAsuransi()
        {
            dA = new SqlDataAdapter("select Id_asuransi,nama_asuransi from tbl_asuransi ", Conn);
            dA.Fill(dS, "asuransi");
            cmbAsuransi.DataSource = dS.Tables["asuransi"];
            cmbAsuransi.DisplayMember = "nama_asuransi";
            cmbAsuransi.ValueMember = "Id_asuransi";
            cmbAsuransi.Text = "";
            cmbAsuransi.SelectedValue = "";
        }
        void autoNumericLoginPasien()
        {
            if (Conn.State == ConnectionState.Closed)
                Conn.Open();
            Comm.Connection = Conn;
            Comm.CommandText = "SELECT * FROM tbl_pasien";
            dRead = Comm.ExecuteReader();
            if (dRead.HasRows)
            {
                dRead.Close();
                Comm.CommandText = @"SELECT TOP 1 kode_masuk 
                        FROM tbl_pasien ORDER BY kode_masuk DESC";
                txtId.Text = string.Format("LP{0,3:00#}",
                    Convert.ToInt32(Comm.ExecuteScalar().
                    ToString().Substring(2)) + 1);
            }
            else
            {
                txtId.Text = "LP001";
            }
            Conn.Close();
        }
        void autoNumericIdPasien()
        {
            if (Conn.State == ConnectionState.Closed)
                Conn.Open();
            Comm.Connection = Conn;
            Comm.CommandText = "SELECT * FROM tbl_pasien";
            dRead = Comm.ExecuteReader();
            if (dRead.HasRows)
            {
                dRead.Close();
                Comm.CommandText = @"SELECT TOP 1 Id_pasien 
                        FROM tbl_pasien ORDER BY Id_pasien DESC";
                Id = string.Format("PAS{0,3:00#}",
                    Convert.ToInt32(Comm.ExecuteScalar().
                    ToString().Substring(3)) + 1);
            }
            else
            {
                Id = "PAS001";
            }
            Conn.Close();
        }
        private void frmAkunPasien_Load(object sender, EventArgs e)
        {
            autoNumericIdPasien();
            tblDetail = dS.Tables.Add("akunPasien");
            tblDetail.Columns.Add("Kode_masuk", Type.GetType("System.String"));
            tblDetail.Columns.Add("Username", Type.GetType("System.String"));
            tblDetail.Columns.Add("Password", Type.GetType("System.String"));
            tblDetail.Columns.Add("status_login", Type.GetType("System.String"));
            tblDetail.Columns.Add("kode_login", Type.GetType("System.String"));
            tblDetail.Columns.Add("Id_asuransi", Type.GetType("System.String"));
            tblDetail.Columns.Add("Asuransii", Type.GetType("System.String"));
            dgvAkun.DataSource = dS.Tables["akunPasien"];

            dA = new SqlDataAdapter(@"SELECT tbl_login.Kode_masuk,tbl_login.Username,tbl_login.Password, tbl_login.status_login,tbl_pasien.kode_masuk AS kode_login,
                                      tbl_pasien.Id_asuransi, tbl_pasien.Asuransii,tbl_asuransi.Id_asuransi FROM tbl_login INNER JOIN tbl_pasien ON tbl_login.Kode_masuk = tbl_pasien.kode_masuk
                                      inner join tbl_asuransi on tbl_pasien.Id_asuransi = tbl_asuransi.Id_asuransi", Conn);
            dA.Fill(dS, "akunPasien");
            dgvAkun.AutoGenerateColumns = false;
            autoNumericLoginPasien();
            IsiAsuransi();
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
                txtId.Text = Convert.ToString(dS.Tables["akunPasien"].Rows[dgvAkun.CurrentRow.Index].ItemArray[0]);
                txtUsername.Text = Convert.ToString(dS.Tables["akunPasien"].Rows[dgvAkun.CurrentRow.Index].ItemArray[1]);
                txtPassword.Text = Convert.ToString(dS.Tables["akunPasien"].Rows[dgvAkun.CurrentRow.Index].ItemArray[2]);
                cmbAsuransi.Text = Convert.ToString(dS.Tables["akunPasien"].Rows[dgvAkun.CurrentRow.Index].ItemArray[6]);
            }
            catch
            {
                clear();
                dgvAkun.Focus();
                autoNumericLoginPasien();
                autoNumericIdPasien();
            }
        }
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtId.Text == ""||txtUsername.Text==""||txtPassword.Text==""||cmbAsuransi.Text=="")
            {
                MessageBox.Show("Masukkan Kode Masuk sebelum disimpan", "Pesan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataColumn[] dCol = { dS.Tables["akunPasien"].Columns["Kode_masuk"] };
                dS.Tables["akunPasien"].PrimaryKey = dCol;
                dRow = dS.Tables["akunPasien"].Rows.Find(txtId.Text);
                if (dRow == null)
                {
                    dRow = dS.Tables["akunPasien"].NewRow();
                    dRow[0] = txtId.Text;
                    dRow[1] = txtUsername.Text;
                    dRow[2] = txtPassword.Text;
                    dRow[3] = "pasien";
                    dRow[4] = txtId.Text;
                    dRow[5] = cmbAsuransi.SelectedValue;
                    dRow[6] = cmbAsuransi.Text;
                    dS.Tables["akunPasien"].Rows.Add(dRow);

                    SqlCommand insCmd = new SqlCommand(@"insert into tbl_login (Kode_masuk,Username,Password,status_login)
                                                        values(@Kode_masuk,@Username,@Password,@status_login)
                                                        insert into tbl_pasien(Id_pasien,kode_masuk,Id_asuransi,Asuransii) values('" + Id + "',@Kode_masuk,@Id_asuransi,@nama_asuransi)", Conn);
                    insCmd.Parameters.Add("@Kode_masuk", SqlDbType.NVarChar, 10, "Kode_masuk");
                    insCmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50, "Username");
                    insCmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50, "Password");
                    insCmd.Parameters.Add("@status_login", SqlDbType.NVarChar, 10, "status_login");
                    insCmd.Parameters.Add("@Id_asuransi", SqlDbType.NVarChar, 10, "Id_asuransi");
                    insCmd.Parameters.Add("@nama_asuransi", SqlDbType.NVarChar, 20, "Asuransii");

                    dA.InsertCommand = insCmd;
                    dA.Update(dS, "akunPasien");
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
            if (txtId.Text == "" || txtUsername.Text == "" || txtPassword.Text == "" || cmbAsuransi.Text == "") 
            {
                MessageBox.Show("Pilih terlebih dahulu data yang ingin diubah", "Pesan Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtId.Focus();
            }
            else
            {
                DataColumn[] dataCol = { dS.Tables["akunPasien"].Columns["Kode_masuk"] };
                dS.Tables["akunPasien"].PrimaryKey = dataCol;
                dRow = dS.Tables["akunPasien"].Rows.Find(txtId.Text);
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
                    dRow[3] = "pasien";
                    dRow[4] = txtId.Text;
                    dRow[5] = cmbAsuransi.SelectedValue;
                    dRow[6] = cmbAsuransi.Text;
                    dRow.EndEdit();
                    SqlCommand updtCmd = new SqlCommand(@"update tbl_login set Username=@Username, Password=@Password
                                                          where Kode_masuk=@Kode_masuk
                                                          update tbl_pasien set Id_asuransi=@Id_asuransi, Asuransii=@nama_asuransi where kode_masuk=@kode_masuk", Conn);
                    updtCmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50, "Username");
                    updtCmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50, "Password");
                    updtCmd.Parameters.Add("@Kode_masuk", SqlDbType.NVarChar, 10, "Kode_masuk");
                    updtCmd.Parameters.Add("@Id_asuransi", SqlDbType.NVarChar, 10, "Id_asuransi");
                    updtCmd.Parameters.Add("@nama_asuransi", SqlDbType.NVarChar, 20, "Asuransii");
                    dA.UpdateCommand = updtCmd;
                    dA.Update(dS, "akunPasien");
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
                DataColumn[] dataCol = { dS.Tables["akunPasien"].Columns["Kode_masuk"] };
                dS.Tables["akunPasien"].PrimaryKey = dataCol;
                dRow = dS.Tables["akunPasien"].Rows.Find(txtId.Text);
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
                                                             delete from tbl_pasien where kode_masuk=@Kode_masuk", Conn);
                        dltCmd.Parameters.Add("@Kode_masuk", SqlDbType.NVarChar, 10, "Kode_masuk");
                        dA.DeleteCommand = dltCmd;
                        dA.Update(dS, "akunPasien");
                        MessageBox.Show("Data telah berhasil dihapus", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                }
            }
        }

        private void dgvAkun_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            clear();
            txtUsername.Focus();
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
