
namespace Layanan_Dokter
{
    partial class frmRiwayat
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
            this.btnKunjungan = new System.Windows.Forms.Button();
            this.btnBerobat = new System.Windows.Forms.Button();
            this.pnlLoader = new System.Windows.Forms.Panel();
            this.dgvKunjungan = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvBerobat = new System.Windows.Forms.DataGridView();
            this.Column28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlLoader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKunjungan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBerobat)).BeginInit();
            this.SuspendLayout();
            // 
            // btnKunjungan
            // 
            this.btnKunjungan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnKunjungan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.btnKunjungan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKunjungan.FlatAppearance.BorderSize = 0;
            this.btnKunjungan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKunjungan.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKunjungan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnKunjungan.Location = new System.Drawing.Point(639, 28);
            this.btnKunjungan.Margin = new System.Windows.Forms.Padding(0);
            this.btnKunjungan.Name = "btnKunjungan";
            this.btnKunjungan.Size = new System.Drawing.Size(161, 51);
            this.btnKunjungan.TabIndex = 0;
            this.btnKunjungan.Text = "Kunjungan";
            this.btnKunjungan.UseVisualStyleBackColor = false;
            this.btnKunjungan.Click += new System.EventHandler(this.btnKunjungan_Click);
            // 
            // btnBerobat
            // 
            this.btnBerobat.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBerobat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.btnBerobat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBerobat.FlatAppearance.BorderSize = 0;
            this.btnBerobat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBerobat.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBerobat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnBerobat.Location = new System.Drawing.Point(800, 28);
            this.btnBerobat.Margin = new System.Windows.Forms.Padding(0);
            this.btnBerobat.Name = "btnBerobat";
            this.btnBerobat.Size = new System.Drawing.Size(172, 51);
            this.btnBerobat.TabIndex = 0;
            this.btnBerobat.Text = "Berobat";
            this.btnBerobat.UseVisualStyleBackColor = false;
            this.btnBerobat.Click += new System.EventHandler(this.btnBerobat_Click);
            // 
            // pnlLoader
            // 
            this.pnlLoader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(90)))));
            this.pnlLoader.Controls.Add(this.btnBerobat);
            this.pnlLoader.Controls.Add(this.btnKunjungan);
            this.pnlLoader.Controls.Add(this.dgvKunjungan);
            this.pnlLoader.Controls.Add(this.dgvBerobat);
            this.pnlLoader.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLoader.Location = new System.Drawing.Point(0, 67);
            this.pnlLoader.Name = "pnlLoader";
            this.pnlLoader.Size = new System.Drawing.Size(972, 540);
            this.pnlLoader.TabIndex = 39;
            this.pnlLoader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBerobat_Paint);
            // 
            // dgvKunjungan
            // 
            this.dgvKunjungan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKunjungan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column31,
            this.Column20,
            this.Column3,
            this.Column5,
            this.Column6,
            this.Column8,
            this.Column4,
            this.Column10,
            this.Column11,
            this.Column9,
            this.Column7,
            this.Column2,
            this.Column29});
            this.dgvKunjungan.Location = new System.Drawing.Point(53, 79);
            this.dgvKunjungan.Margin = new System.Windows.Forms.Padding(0);
            this.dgvKunjungan.Name = "dgvKunjungan";
            this.dgvKunjungan.RowHeadersWidth = 51;
            this.dgvKunjungan.RowTemplate.Height = 24;
            this.dgvKunjungan.Size = new System.Drawing.Size(898, 325);
            this.dgvKunjungan.TabIndex = 43;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "tanggal_dibuat";
            this.Column1.HeaderText = "Tanggal ";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 125;
            // 
            // Column31
            // 
            this.Column31.DataPropertyName = "Id_pasien";
            this.Column31.HeaderText = "Id pasien";
            this.Column31.MinimumWidth = 6;
            this.Column31.Name = "Column31";
            this.Column31.Visible = false;
            this.Column31.Width = 125;
            // 
            // Column20
            // 
            this.Column20.DataPropertyName = "tanggal";
            this.Column20.HeaderText = "Tanggal temu";
            this.Column20.MinimumWidth = 6;
            this.Column20.Name = "Column20";
            this.Column20.Width = 125;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Jam";
            this.Column3.HeaderText = "Jam";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 125;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Id_dokter";
            this.Column5.HeaderText = "Id dokter";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.Visible = false;
            this.Column5.Width = 125;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "nama_dokter";
            this.Column6.HeaderText = "Nama Dokter";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.Width = 125;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "Spesialis";
            this.Column8.HeaderText = "Spesialis";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.Width = 125;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Nama_pasien";
            this.Column4.HeaderText = "Nama Pasien";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.Width = 125;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "Asuransii";
            this.Column10.HeaderText = "Asuransi";
            this.Column10.MinimumWidth = 6;
            this.Column10.Name = "Column10";
            this.Column10.Width = 125;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "Tujuan";
            this.Column11.HeaderText = "Tujuan";
            this.Column11.MinimumWidth = 6;
            this.Column11.Name = "Column11";
            this.Column11.Width = 125;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "Keterangan";
            this.Column9.HeaderText = "Keterangan";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.Width = 125;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "kode_riwayat";
            this.Column7.HeaderText = "Kode riwayat";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.Width = 125;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Kode_riwayat1";
            this.Column2.HeaderText = "Kode riwayat";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Visible = false;
            this.Column2.Width = 125;
            // 
            // Column29
            // 
            this.Column29.DataPropertyName = "status";
            this.Column29.HeaderText = "Status";
            this.Column29.MinimumWidth = 6;
            this.Column29.Name = "Column29";
            this.Column29.Width = 125;
            // 
            // dgvBerobat
            // 
            this.dgvBerobat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBerobat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column28,
            this.Column30,
            this.Column12,
            this.Column16,
            this.Column13,
            this.Column18,
            this.Column19,
            this.Column21,
            this.Column14,
            this.Column15,
            this.Column17,
            this.Column22,
            this.Column23,
            this.Column24,
            this.Column25,
            this.Column26,
            this.Column27});
            this.dgvBerobat.Location = new System.Drawing.Point(53, 79);
            this.dgvBerobat.Margin = new System.Windows.Forms.Padding(0);
            this.dgvBerobat.Name = "dgvBerobat";
            this.dgvBerobat.RowHeadersWidth = 51;
            this.dgvBerobat.RowTemplate.Height = 24;
            this.dgvBerobat.Size = new System.Drawing.Size(898, 325);
            this.dgvBerobat.TabIndex = 42;
            // 
            // Column28
            // 
            this.Column28.DataPropertyName = "kode_riwayat";
            this.Column28.HeaderText = "Kode Riwayat";
            this.Column28.MinimumWidth = 6;
            this.Column28.Name = "Column28";
            this.Column28.Width = 125;
            // 
            // Column30
            // 
            this.Column30.DataPropertyName = "kode_riwayat1";
            this.Column30.HeaderText = "kode riwayat";
            this.Column30.MinimumWidth = 6;
            this.Column30.Name = "Column30";
            this.Column30.Visible = false;
            this.Column30.Width = 125;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "tanggal dibuat";
            this.Column12.HeaderText = "Tanggal ";
            this.Column12.MinimumWidth = 6;
            this.Column12.Name = "Column12";
            this.Column12.Width = 125;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "tanggal";
            this.Column16.HeaderText = "Tanggal Temu";
            this.Column16.MinimumWidth = 6;
            this.Column16.Name = "Column16";
            this.Column16.Width = 125;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "jam";
            this.Column13.HeaderText = "Jam";
            this.Column13.MinimumWidth = 6;
            this.Column13.Name = "Column13";
            this.Column13.Width = 125;
            // 
            // Column18
            // 
            this.Column18.DataPropertyName = "Id_dokter";
            this.Column18.HeaderText = "Id dokter";
            this.Column18.MinimumWidth = 6;
            this.Column18.Name = "Column18";
            this.Column18.Visible = false;
            this.Column18.Width = 125;
            // 
            // Column19
            // 
            this.Column19.DataPropertyName = "nama_dokter";
            this.Column19.HeaderText = "Nama Dokter";
            this.Column19.MinimumWidth = 6;
            this.Column19.Name = "Column19";
            this.Column19.Width = 125;
            // 
            // Column21
            // 
            this.Column21.DataPropertyName = "Spesialis";
            this.Column21.HeaderText = "Spesialis";
            this.Column21.MinimumWidth = 6;
            this.Column21.Name = "Column21";
            this.Column21.Width = 125;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "Id_pasien";
            this.Column14.HeaderText = "Id_pasien";
            this.Column14.MinimumWidth = 6;
            this.Column14.Name = "Column14";
            this.Column14.Visible = false;
            this.Column14.Width = 125;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "Nama_pasien";
            this.Column15.HeaderText = "Nama Pasien";
            this.Column15.MinimumWidth = 6;
            this.Column15.Name = "Column15";
            this.Column15.Width = 125;
            // 
            // Column17
            // 
            this.Column17.DataPropertyName = "Asuransii";
            this.Column17.HeaderText = "Asuransi";
            this.Column17.MinimumWidth = 6;
            this.Column17.Name = "Column17";
            this.Column17.Width = 125;
            // 
            // Column22
            // 
            this.Column22.DataPropertyName = "Keluhan";
            this.Column22.HeaderText = "Keluhan";
            this.Column22.MinimumWidth = 6;
            this.Column22.Name = "Column22";
            this.Column22.Width = 125;
            // 
            // Column23
            // 
            this.Column23.DataPropertyName = "Gejala";
            this.Column23.HeaderText = "Gejala";
            this.Column23.MinimumWidth = 6;
            this.Column23.Name = "Column23";
            this.Column23.Width = 125;
            // 
            // Column24
            // 
            this.Column24.DataPropertyName = "Diagnosa";
            this.Column24.HeaderText = "Diagnosa";
            this.Column24.MinimumWidth = 6;
            this.Column24.Name = "Column24";
            this.Column24.Width = 125;
            // 
            // Column25
            // 
            this.Column25.DataPropertyName = "Treatment";
            this.Column25.HeaderText = "Treatment";
            this.Column25.MinimumWidth = 6;
            this.Column25.Name = "Column25";
            this.Column25.Width = 125;
            // 
            // Column26
            // 
            this.Column26.DataPropertyName = "Id_obat";
            this.Column26.HeaderText = "Id obat";
            this.Column26.MinimumWidth = 6;
            this.Column26.Name = "Column26";
            this.Column26.Visible = false;
            this.Column26.Width = 125;
            // 
            // Column27
            // 
            this.Column27.DataPropertyName = "nama_obat";
            this.Column27.HeaderText = "Nama Obat";
            this.Column27.MinimumWidth = 6;
            this.Column27.Name = "Column27";
            this.Column27.Width = 125;
            // 
            // frmRiwayat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(90)))));
            this.ClientSize = new System.Drawing.Size(972, 607);
            this.Controls.Add(this.pnlLoader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmRiwayat";
            this.Text = "frmRiwayat";
            this.Load += new System.EventHandler(this.frmRiwayat_Load);
            this.pnlLoader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKunjungan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBerobat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnKunjungan;
        private System.Windows.Forms.Button btnBerobat;
        private System.Windows.Forms.Panel pnlLoader;
        private System.Windows.Forms.DataGridView dgvBerobat;
        private System.Windows.Forms.DataGridView dgvKunjungan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column31;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column29;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column28;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column30;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column22;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column23;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column24;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column25;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column26;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column27;
    }
}