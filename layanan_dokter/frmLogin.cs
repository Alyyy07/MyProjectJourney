using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Layanan_Dokter
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            pw = panel5.Width;
        }
        int pw;
        SqlConnection Conn = new SqlConnection(@"Server=ALYYY;Database=aplikasiDokter;Integrated Security=True;");
        SqlCommand Comm = new SqlCommand();
        SqlDataAdapter dA = new SqlDataAdapter();
        DataSet dS = new DataSet();
        DataRow dRow;
        System.Windows.Forms.Timer timer= new System.Windows.Forms.Timer();

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.Opacity = .01;
            timer.Interval = 10;
            timer.Tick += IncreaseOpacity;
            timer.Start();
        }

        void IncreaseOpacity(object sender,EventArgs e)
        {
            if (this.Opacity <= 1) this.Opacity += 0.01;
            if (this.Opacity == 1) timer.Stop();
        }

        void TimeoutMessage(object sender,EventArgs e)
        {
            label6.Text = "";
            timer.Stop();
        }
        private void pnlLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtUsername.ForeColor = Color.White;
                panel5.Visible = false;
            }
            catch { }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtPassword.PasswordChar = '*';
                txtPassword.ForeColor = Color.White;
                panel7.Visible = false;
            }
            catch { }

        }

        private void txtUsername_Click(object sender, EventArgs e)
        {
            txtUsername.SelectAll();
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            txtPassword.SelectAll();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Black;

        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Lime;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtUsername.Text == "Masukkan Username")
            {
                panel5.Visible = true;
            }
            if (txtPassword.Text == "" || txtPassword.Text == "Masukkan Password")
            {
                panel7.Visible = true;
            }
            Conn.Open();
            string kode;
            Comm = new SqlCommand("select * from tbl_login where Username='" + txtUsername.Text + "'and Password= '" + txtPassword.Text + "' and status_login='admin'", Conn);
            SqlDataReader dr = Comm.ExecuteReader();
            if (dr.Read() == true)
            {
                new frmAdmin().Show();
                this.Hide();
                dr.Close();
            }
            else
            {
                try
                {
                    dr.Close();
                    dA = new SqlDataAdapter("select Kode_masuk from tbl_login where Username='" + txtUsername.Text + "'and Password= '" + txtPassword.Text + "' and status_login='dokter'", Conn);
                    dA.Fill(dS, "kodedokter");
                    DataColumn[] dc = { dS.Tables["kodedokter"].Columns["Kode_masuk"] };
                    dS.Tables["kodedokter"].PrimaryKey = dc;
                    dRow = dS.Tables["kodedokter"].Rows[0];
                    kode = Convert.ToString(dRow[0]);
                    if (dRow != null)
                    {
                        FolderDokter.loadingD dokter = new FolderDokter.loadingD(kode);
                        dokter.Show();
                        this.Hide();
                        dr.Close();
                    }
                }
                catch
                {
                    dr.Close();
                    dA = new SqlDataAdapter("select Kode_masuk from tbl_login where Username='" + txtUsername.Text + "'and Password= '" + txtPassword.Text + "' and status_login='pasien'", Conn);
                    dA.Fill(dS, "kodepasien");
                    DataColumn[] dC = { dS.Tables["kodepasien"].Columns["Kode_masuk"] };
                    dS.Tables["kodepasien"].PrimaryKey = dC;
                    try
                    {
                    dRow = dS.Tables["kodepasien"].Rows[0];
                    kode = Convert.ToString(dRow[0]);
                    if (dRow != null)
                    {
                        FolderPasien.loadingP pasien = new FolderPasien.loadingP(kode);
                        pasien.Show();
                        this.Hide();
                        dr.Close();
                    }
                    }
                    catch
                    {
                        dr.Close();
                        timer.Interval = 2000;
                        timer.Enabled = true;
                        timer.Tick += TimeoutMessage;
                        timer.Start();
                        label6.Text = "Username atau Password yang anda masukkan salah";
                        txtUsername.Text = "";
                        txtPassword.Text = "";
                    }
                }
            }
            Conn.Close();
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        private void frmLogin_DragEnter(object sender, DragEventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void frmLogin_ResizeBegin(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void frmLogin_ResizeEnd(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
