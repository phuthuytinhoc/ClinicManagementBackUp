namespace QuanLyBenhVien
{
    partial class frmDoiMatKhau
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDoiMatKhau));
            this.btnDoiMatKhau = new DevComponents.DotNetBar.ButtonX();
            this.btnThoatDoiMatKhau = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtMatKhauCu = new System.Windows.Forms.TextBox();
            this.txtMatKhauMoi = new System.Windows.Forms.TextBox();
            this.txtNhapLaiMatKhau = new System.Windows.Forms.TextBox();
            this.lblWarn_ThayDoiMatKhau = new DevComponents.DotNetBar.LabelX();
            this.lblWarn_OkMatKhauCu = new DevComponents.DotNetBar.LabelX();
            this.lblWarn_OkMatKhauMoi = new DevComponents.DotNetBar.LabelX();
            this.lblWarn_OkNhapLai = new DevComponents.DotNetBar.LabelX();
            this.lblWarn_NULLMatKhauCu = new DevComponents.DotNetBar.LabelX();
            this.lblWarn_NULLMatKhauMoi = new DevComponents.DotNetBar.LabelX();
            this.lblWarn_NULLNhapLai = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // btnDoiMatKhau
            // 
            this.btnDoiMatKhau.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDoiMatKhau.BackColor = System.Drawing.Color.Transparent;
            this.btnDoiMatKhau.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.btnDoiMatKhau.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDoiMatKhau.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnDoiMatKhau.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoiMatKhau.Image = ((System.Drawing.Image)(resources.GetObject("btnDoiMatKhau.Image")));
            this.btnDoiMatKhau.ImageFixedSize = new System.Drawing.Size(40, 40);
            this.btnDoiMatKhau.Location = new System.Drawing.Point(200, 261);
            this.btnDoiMatKhau.Name = "btnDoiMatKhau";
            this.btnDoiMatKhau.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(8);
            this.btnDoiMatKhau.Size = new System.Drawing.Size(122, 50);
            this.btnDoiMatKhau.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDoiMatKhau.TabIndex = 0;
            this.btnDoiMatKhau.Text = "Đổi mật khẩu";
            // 
            // btnThoatDoiMatKhau
            // 
            this.btnThoatDoiMatKhau.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnThoatDoiMatKhau.BackColor = System.Drawing.Color.Transparent;
            this.btnThoatDoiMatKhau.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.btnThoatDoiMatKhau.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThoatDoiMatKhau.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnThoatDoiMatKhau.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoatDoiMatKhau.Image = ((System.Drawing.Image)(resources.GetObject("btnThoatDoiMatKhau.Image")));
            this.btnThoatDoiMatKhau.ImageFixedSize = new System.Drawing.Size(40, 40);
            this.btnThoatDoiMatKhau.Location = new System.Drawing.Point(385, 261);
            this.btnThoatDoiMatKhau.Name = "btnThoatDoiMatKhau";
            this.btnThoatDoiMatKhau.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(8);
            this.btnThoatDoiMatKhau.Size = new System.Drawing.Size(98, 50);
            this.btnThoatDoiMatKhau.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnThoatDoiMatKhau.TabIndex = 2;
            this.btnThoatDoiMatKhau.Text = "Thoát";
            this.btnThoatDoiMatKhau.Click += new System.EventHandler(this.btnThoatDoiMatKhau_Click);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelX1.Location = new System.Drawing.Point(95, 112);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(88, 23);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "Mật khẩu cũ";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelX2.Location = new System.Drawing.Point(83, 157);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(100, 23);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "Mật khẩu mới";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelX3.Location = new System.Drawing.Point(47, 196);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(145, 32);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "Nhập lại mật khẩu";
            // 
            // txtMatKhauCu
            // 
            this.txtMatKhauCu.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtMatKhauCu.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatKhauCu.Location = new System.Drawing.Point(200, 108);
            this.txtMatKhauCu.Name = "txtMatKhauCu";
            this.txtMatKhauCu.PasswordChar = '●';
            this.txtMatKhauCu.Size = new System.Drawing.Size(283, 30);
            this.txtMatKhauCu.TabIndex = 6;
            this.txtMatKhauCu.TextChanged += new System.EventHandler(this.txtMatKhauCu_TextChanged);
            // 
            // txtMatKhauMoi
            // 
            this.txtMatKhauMoi.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtMatKhauMoi.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatKhauMoi.Location = new System.Drawing.Point(200, 153);
            this.txtMatKhauMoi.Name = "txtMatKhauMoi";
            this.txtMatKhauMoi.PasswordChar = '●';
            this.txtMatKhauMoi.Size = new System.Drawing.Size(283, 30);
            this.txtMatKhauMoi.TabIndex = 7;
            this.txtMatKhauMoi.TextChanged += new System.EventHandler(this.txtMatKhauMoi_TextChanged);
            // 
            // txtNhapLaiMatKhau
            // 
            this.txtNhapLaiMatKhau.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtNhapLaiMatKhau.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhapLaiMatKhau.Location = new System.Drawing.Point(200, 196);
            this.txtNhapLaiMatKhau.Name = "txtNhapLaiMatKhau";
            this.txtNhapLaiMatKhau.PasswordChar = '●';
            this.txtNhapLaiMatKhau.Size = new System.Drawing.Size(283, 30);
            this.txtNhapLaiMatKhau.TabIndex = 8;
            this.txtNhapLaiMatKhau.TextChanged += new System.EventHandler(this.txtNhapLaiMatKhau_TextChanged);
            // 
            // lblWarn_ThayDoiMatKhau
            // 
            this.lblWarn_ThayDoiMatKhau.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblWarn_ThayDoiMatKhau.BackgroundStyle.Class = "";
            this.lblWarn_ThayDoiMatKhau.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblWarn_ThayDoiMatKhau.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarn_ThayDoiMatKhau.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblWarn_ThayDoiMatKhau.Location = new System.Drawing.Point(200, 232);
            this.lblWarn_ThayDoiMatKhau.Name = "lblWarn_ThayDoiMatKhau";
            this.lblWarn_ThayDoiMatKhau.Size = new System.Drawing.Size(371, 23);
            this.lblWarn_ThayDoiMatKhau.TabIndex = 9;
            this.lblWarn_ThayDoiMatKhau.Text = "* Mật khẩu chưa được thay đổi *";
            // 
            // lblWarn_OkMatKhauCu
            // 
            this.lblWarn_OkMatKhauCu.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblWarn_OkMatKhauCu.BackgroundStyle.Class = "";
            this.lblWarn_OkMatKhauCu.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblWarn_OkMatKhauCu.Image = global::QuanLyBenhVien.Properties.Resources.symbol_check1;
            this.lblWarn_OkMatKhauCu.Location = new System.Drawing.Point(498, 106);
            this.lblWarn_OkMatKhauCu.Name = "lblWarn_OkMatKhauCu";
            this.lblWarn_OkMatKhauCu.Size = new System.Drawing.Size(40, 39);
            this.lblWarn_OkMatKhauCu.TabIndex = 10;
            // 
            // lblWarn_OkMatKhauMoi
            // 
            this.lblWarn_OkMatKhauMoi.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblWarn_OkMatKhauMoi.BackgroundStyle.Class = "";
            this.lblWarn_OkMatKhauMoi.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblWarn_OkMatKhauMoi.Image = global::QuanLyBenhVien.Properties.Resources.symbol_check1;
            this.lblWarn_OkMatKhauMoi.Location = new System.Drawing.Point(498, 151);
            this.lblWarn_OkMatKhauMoi.Name = "lblWarn_OkMatKhauMoi";
            this.lblWarn_OkMatKhauMoi.Size = new System.Drawing.Size(40, 39);
            this.lblWarn_OkMatKhauMoi.TabIndex = 11;
            // 
            // lblWarn_OkNhapLai
            // 
            this.lblWarn_OkNhapLai.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblWarn_OkNhapLai.BackgroundStyle.Class = "";
            this.lblWarn_OkNhapLai.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblWarn_OkNhapLai.Image = global::QuanLyBenhVien.Properties.Resources.symbol_check1;
            this.lblWarn_OkNhapLai.Location = new System.Drawing.Point(498, 194);
            this.lblWarn_OkNhapLai.Name = "lblWarn_OkNhapLai";
            this.lblWarn_OkNhapLai.Size = new System.Drawing.Size(40, 39);
            this.lblWarn_OkNhapLai.TabIndex = 12;
            // 
            // lblWarn_NULLMatKhauCu
            // 
            this.lblWarn_NULLMatKhauCu.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblWarn_NULLMatKhauCu.BackgroundStyle.Class = "";
            this.lblWarn_NULLMatKhauCu.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblWarn_NULLMatKhauCu.Image = global::QuanLyBenhVien.Properties.Resources.symbol_delete;
            this.lblWarn_NULLMatKhauCu.Location = new System.Drawing.Point(498, 106);
            this.lblWarn_NULLMatKhauCu.Name = "lblWarn_NULLMatKhauCu";
            this.lblWarn_NULLMatKhauCu.Size = new System.Drawing.Size(40, 39);
            this.lblWarn_NULLMatKhauCu.TabIndex = 13;
            // 
            // lblWarn_NULLMatKhauMoi
            // 
            this.lblWarn_NULLMatKhauMoi.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblWarn_NULLMatKhauMoi.BackgroundStyle.Class = "";
            this.lblWarn_NULLMatKhauMoi.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblWarn_NULLMatKhauMoi.Image = global::QuanLyBenhVien.Properties.Resources.symbol_delete;
            this.lblWarn_NULLMatKhauMoi.Location = new System.Drawing.Point(498, 151);
            this.lblWarn_NULLMatKhauMoi.Name = "lblWarn_NULLMatKhauMoi";
            this.lblWarn_NULLMatKhauMoi.Size = new System.Drawing.Size(40, 39);
            this.lblWarn_NULLMatKhauMoi.TabIndex = 14;
            // 
            // lblWarn_NULLNhapLai
            // 
            this.lblWarn_NULLNhapLai.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblWarn_NULLNhapLai.BackgroundStyle.Class = "";
            this.lblWarn_NULLNhapLai.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblWarn_NULLNhapLai.Image = global::QuanLyBenhVien.Properties.Resources.symbol_delete;
            this.lblWarn_NULLNhapLai.Location = new System.Drawing.Point(498, 195);
            this.lblWarn_NULLNhapLai.Name = "lblWarn_NULLNhapLai";
            this.lblWarn_NULLNhapLai.Size = new System.Drawing.Size(40, 39);
            this.lblWarn_NULLNhapLai.TabIndex = 15;
            // 
            // frmDoiMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(583, 327);
            this.Controls.Add(this.lblWarn_NULLNhapLai);
            this.Controls.Add(this.lblWarn_NULLMatKhauMoi);
            this.Controls.Add(this.lblWarn_NULLMatKhauCu);
            this.Controls.Add(this.lblWarn_OkNhapLai);
            this.Controls.Add(this.lblWarn_OkMatKhauMoi);
            this.Controls.Add(this.lblWarn_OkMatKhauCu);
            this.Controls.Add(this.lblWarn_ThayDoiMatKhau);
            this.Controls.Add(this.txtNhapLaiMatKhau);
            this.Controls.Add(this.txtMatKhauMoi);
            this.Controls.Add(this.txtMatKhauCu);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnThoatDoiMatKhau);
            this.Controls.Add(this.btnDoiMatKhau);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDoiMatKhau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đổi mật khẩu";
            this.Load += new System.EventHandler(this.frmDoiMatKhau_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        public DevComponents.DotNetBar.ButtonX btnDoiMatKhau;
        public DevComponents.DotNetBar.ButtonX btnThoatDoiMatKhau;
        public System.Windows.Forms.TextBox txtMatKhauCu;
        public System.Windows.Forms.TextBox txtMatKhauMoi;
        public System.Windows.Forms.TextBox txtNhapLaiMatKhau;
        public DevComponents.DotNetBar.LabelX lblWarn_ThayDoiMatKhau;
        private DevComponents.DotNetBar.LabelX lblWarn_OkMatKhauCu;
        private DevComponents.DotNetBar.LabelX lblWarn_OkMatKhauMoi;
        private DevComponents.DotNetBar.LabelX lblWarn_OkNhapLai;
        private DevComponents.DotNetBar.LabelX lblWarn_NULLMatKhauCu;
        private DevComponents.DotNetBar.LabelX lblWarn_NULLMatKhauMoi;
        private DevComponents.DotNetBar.LabelX lblWarn_NULLNhapLai;
    }
}