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
    public partial class frmDataObat : Form
    {
        public frmDataObat()
        {
            InitializeComponent();
        }
        SqlConnection Conn = new SqlConnection(@"Server=ALYYY;Database=aplikasiDokter;Integrated Security = True;");
        SqlCommand Comm = new SqlCommand();
        DataSet dS = new DataSet();
        SqlDataAdapter dA = new SqlDataAdapter();
        DataRow dRow;
        SqlDataReader dRead;

        void clear()
        {
            txtId.Clear();
            txtNamaObat.Clear();
            dtpKadaluarsa.Text = "";
            autoNumeric();
        }

        void autoNumeric()
        {
            if (Conn.State == ConnectionState.Closed)
                Conn.Open();
            Comm.Connection = Conn;
            Comm.CommandText = "SELECT * FROM tbl_obat";
            dRead = Comm.ExecuteReader();
            if (dRead.HasRows)
            {
                dRead.Close();
                Comm.CommandText = @"SELECT TOP 1 Id_obat 
                        FROM tbl_obat ORDER BY Id_obat DESC";
                txtId.Text = string.Format("IO-{0,3:00#}",
                    Convert.ToInt32(Comm.ExecuteScalar().
                    ToString().Substring(3)) + 1);
            }
            else
            {
                txtId.Text = "IO-001";
            }
            Conn.Close();
        }
        private void frmDataObat_Load(object sender, EventArgs e)
        {
            dA = new SqlDataAdapter("SELECT * FROM tbl_obat", Conn);
            dA.Fill(dS, "obat");
            dgvObat.DataSource = dS.Tables["obat"];
            autoNumeric();
        }
        private void dgvObat_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtId.Text = Convert.ToString(dS.Tables["obat"].Rows[dgvObat.CurrentRow.Index].ItemArray[0]);
                txtNamaObat.Text = Convert.ToString(dS.Tables["obat"].Rows[dgvObat.CurrentRow.Index].ItemArray[1]);
                dtpKadaluarsa.Value = Convert.ToDateTime(dS.Tables["obat"].Rows[dgvObat.CurrentRow.Index].ItemArray[2]);
            }
            catch
            {
                clear();
                txtNamaObat.Focus();
            }
        }
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtId.Text == ""||txtNamaObat.Text=="")
            {
                MessageBox.Show("Masukkan Data yang akan disimpan !", "Pesan Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNamaObat.Focus();
            }
            else
            {
                DataColumn[] dC = { dS.Tables["obat"].Columns["Id_obat"] };
                dS.Tables["obat"].PrimaryKey = dC;
                dRow = dS.Tables["obat"].Rows.Find(txtId.Text);
                if (dRow == null)
                {
                    dRow = dS.Tables["obat"].NewRow();
                    dRow[0] = txtId.Text;
                    dRow[1] = txtNamaObat.Text;
                    dRow[2] = dtpKadaluarsa.Value;
                    dS.Tables["obat"].Rows.Add(dRow);
                    SqlCommandBuilder commBuild = new SqlCommandBuilder(dA);
                    dA.Update(dS, "obat");
                    MessageBox.Show("Data telah berhasil disimpan", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    clear();
                }
                else
                {
                    MessageBox.Show("Data telah terdaftar", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNamaObat.Focus();
                }

            }
        }

        private void btnSimpan_MouseEnter(object sender, EventArgs e)
        {
            btnSimpan.ForeColor = Color.Black;
        }

        private void btnSimpan_MouseLeave(object sender, EventArgs e)
        {
            btnSimpan.ForeColor = Color.Lime;
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (txtId.Text == ""||txtNamaObat.Text=="")
            {
                MessageBox.Show("Masukkan Data yang ingin Diubah !", "Pesan Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtId.Focus();
            }
            else
            {
                DataColumn[] dC = { dS.Tables["obat"].Columns["Id_obat"] };
                dS.Tables["obat"].PrimaryKey = dC;
                dRow = dS.Tables["obat"].Rows.Find(txtId.Text);
                if (dRow == null)
                {
                    MessageBox.Show("Data belum terdaftar !", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtId.Focus();
                }
                else
                {
                    dRow.BeginEdit();
                    dRow[0] = txtId.Text;
                    dRow[1] = txtNamaObat.Text;
                    dRow[2] = dtpKadaluarsa.Value;
                    dRow.EndEdit();
                    SqlCommandBuilder commBuild = new SqlCommandBuilder(dA);
                    dA.Update(dS, "obat");
                    MessageBox.Show("Data telah berhasil disimpan", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    clear();
                }

            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Pilih Data yang ingin Dihapus !", "Pesan Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtId.Focus();
            }
            else
            {
                DataColumn[] dC = { dS.Tables["obat"].Columns["Id_obat"] };
                dS.Tables["obat"].PrimaryKey = dC;
                dRow = dS.Tables["obat"].Rows.Find(txtId.Text);
                if (dRow == null)
                {
                    MessageBox.Show("Data belum terdaftar !", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtId.Focus();
                }
                else
                {
                    DialogResult result;
                    result = MessageBox.Show("Apakah Anda yakin ingin menghapus Data ini ?", "Pesan", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.OK)
                    {
                    dRow.Delete();
                    SqlCommandBuilder commBuild = new SqlCommandBuilder(dA);
                    dA.Update(dS, "obat");
                    MessageBox.Show("Data telah berhasil disimpan", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    clear();
                    }
                }

            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            clear();
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

    }
}
