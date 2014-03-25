using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLyBenhVien.BusinessLogic;
using System.IO;

namespace QuanLyBenhVien
{
    public partial class frmPhongToHinhX_QuangBN : Form
    {
        public frmPhongToHinhX_QuangBN()
        {
            InitializeComponent();
        }

        HinhAnhBUS ThemHinhAnh = new HinhAnhBUS();
        //gan ma benh nhan len ten cua form
        public void AddText(TextBox frmText)
        {
            this.Text = frmText.Text;
        }

        public void LoadAnhXQTuDB(string MaHinhAnh, PictureBox pic)
        {
            if (ThemHinhAnh.KiemTraMaHinhAnh(MaHinhAnh))
            {
                byte[] img = ThemHinhAnh.RetrieveImage(MaHinhAnh);
                MemoryStream str = new MemoryStream(img);
                pic.Image = Image.FromStream(str);
            }
            else
                pic.Image = null;
        }
        private void frmPhongToHinhX_QuangBN_Load(object sender, EventArgs e)
        {
            string a = this.Text;
            picXQuang_PhongToHinhXQBN.Image = null;
            LoadAnhXQTuDB(a, picXQuang_PhongToHinhXQBN);
        }

    }
}
