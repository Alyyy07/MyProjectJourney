using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Layanan_Dokter.FolderPasien
{
    public partial class frmDetailDokter : Form
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
        string kodedokter,id;
        public frmDetailDokter(string kode)
        {
            this.kodedokter = kode;
            InitializeComponent();
        }
        SqlConnection Conn = new SqlConnection(@"Server=ALYYY;Database=aplikasiDokter;Integrated Security = True;");
        SqlCommand Comm = new SqlCommand();
        DataSet dS = new DataSet();
        SqlDataAdapter dA = new SqlDataAdapter();
        DataRow dRow;

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDetailDokter_Load(object sender, EventArgs e)
        {
            dA = new SqlDataAdapter(@"select * from tbl_dokter where Id_dokter='" + kodedokter + "'", Conn);
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
            txtJenisKelamin.Text = Convert.ToString(dRow[11]);
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

        private void frmDetailDokter_DragEnter(object sender, DragEventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void frmDetailDokter_ResizeBegin(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void frmDetailDokter_ResizeEnd(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

    }
}
