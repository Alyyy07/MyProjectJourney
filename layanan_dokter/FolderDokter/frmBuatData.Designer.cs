
namespace Layanan_Dokter.FolderDokter
{
    partial class frmBuatData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlFrmLoader = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFrmLoader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFrmLoader
            // 
            this.pnlFrmLoader.Controls.Add(this.button1);
            this.pnlFrmLoader.Controls.Add(this.label1);
            this.pnlFrmLoader.Controls.Add(this.lblTitle);
            this.pnlFrmLoader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFrmLoader.Location = new System.Drawing.Point(0, 0);
            this.pnlFrmLoader.Name = "pnlFrmLoader";
            this.pnlFrmLoader.Size = new System.Drawing.Size(626, 505);
            this.pnlFrmLoader.TabIndex = 0;
            this.pnlFrmLoader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlFrmLoader_Paint);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(232)))), ((int)(((byte)(229)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(177, 285);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 46);
            this.button1.TabIndex = 21;
            this.button1.Text = "Buat Data Diri";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(194)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(60, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(554, 27);
            this.label1.TabIndex = 19;
            this.label1.Text = "Silahkan Klik tombol dibawah untuk mengisi data diri";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(114, 148);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(394, 31);
            this.lblTitle.TabIndex = 20;
            this.lblTitle.Text = "Anda Belum Mengisi Data Diri..";
            // 
            // frmBuatData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(194)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(626, 505);
            this.Controls.Add(this.pnlFrmLoader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBuatData";
            this.Text = "frmBuatData";
            this.Load += new System.EventHandler(this.frmBuatData_Load);
            this.pnlFrmLoader.ResumeLayout(false);
            this.pnlFrmLoader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFrmLoader;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitle;
    }
}