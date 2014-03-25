namespace QuanLyBenhVien
{
    partial class frmPhongToHinhX_QuangBN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPhongToHinhX_QuangBN));
            this.picXQuang_PhongToHinhXQBN = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picXQuang_PhongToHinhXQBN)).BeginInit();
            this.SuspendLayout();
            // 
            // picXQuang_PhongToHinhXQBN
            // 
            this.picXQuang_PhongToHinhXQBN.BackColor = System.Drawing.Color.Transparent;
            this.picXQuang_PhongToHinhXQBN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picXQuang_PhongToHinhXQBN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picXQuang_PhongToHinhXQBN.Image = global::QuanLyBenhVien.Properties.Resources.noImage;
            this.picXQuang_PhongToHinhXQBN.Location = new System.Drawing.Point(0, 0);
            this.picXQuang_PhongToHinhXQBN.Name = "picXQuang_PhongToHinhXQBN";
            this.picXQuang_PhongToHinhXQBN.Size = new System.Drawing.Size(615, 482);
            this.picXQuang_PhongToHinhXQBN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picXQuang_PhongToHinhXQBN.TabIndex = 0;
            this.picXQuang_PhongToHinhXQBN.TabStop = false;
            // 
            // frmPhongToHinhX_QuangBN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::QuanLyBenhVien.Properties.Resources.noImage;
            this.ClientSize = new System.Drawing.Size(615, 482);
            this.Controls.Add(this.picXQuang_PhongToHinhXQBN);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPhongToHinhX_QuangBN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ảnh X-Quang bệnh nhân";
            this.Load += new System.EventHandler(this.frmPhongToHinhX_QuangBN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picXQuang_PhongToHinhXQBN)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picXQuang_PhongToHinhXQBN;
    }
}