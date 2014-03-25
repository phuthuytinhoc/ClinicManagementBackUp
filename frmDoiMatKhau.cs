using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyBenhVien
{
    public partial class frmDoiMatKhau : Form
    {
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnThoatDoiMatKhau_Click(object sender, EventArgs e)
        {

        }

        private void txtMatKhauCu_TextChanged(object sender, EventArgs e)
        {
            if (txtMatKhauCu.Text == "")
                lblWarn_NULLMatKhauCu.Show();
            else
                lblWarn_NULLMatKhauCu.Hide();
        }

        private void txtMatKhauMoi_TextChanged(object sender, EventArgs e)
        {
            if (txtMatKhauMoi.Text == "")
                lblWarn_NULLMatKhauMoi.Show();
            else
                lblWarn_NULLMatKhauMoi.Hide();
        }

        private void txtNhapLaiMatKhau_TextChanged(object sender, EventArgs e)
        {
            if (txtNhapLaiMatKhau.Text == "")
                lblWarn_NULLNhapLai.Show();
            else
                lblWarn_NULLNhapLai.Hide();
        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            lblWarn_OkMatKhauCu.Hide();
            lblWarn_OkMatKhauMoi.Hide();
            lblWarn_OkNhapLai.Hide();
        }
    }
}
