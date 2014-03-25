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
using QuanLyBenhVien.BusinessObject;
using CrystalDecisions.Shared;

namespace QuanLyBenhVien
{
    public partial class frmXuatVien : Form
    {
        public frmXuatVien()
        {
            InitializeComponent();
        }

        XuatVienBNBUS XuatVienBN = new XuatVienBNBUS();
        HinhAnhBUS ThemHinhAnh = new HinhAnhBUS();

        #region Cac ham lien quan

        public void LoadAnhTuDB(string MaHinhAnh, PictureBox pic)
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

        private XuatVien LayThongTinXuatVienBN()
        {
            XuatVien xv = new XuatVien();

            xv.MaBenhAn = txtMaBenhAn.Text;
            xv.MaXuatVien = txtMaXuatVien.Text;
            xv.LoiDanBS = txtLoiDanBS.Text;
            xv.NgayXuatVien = Convert.ToDateTime(dtPickerNgayRaVien.Value.ToString());

            return xv;
        }

        #endregion


        #region Cac button 

        private void btnDongPhieuXV_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuuPhieuXV_Click(object sender, EventArgs e)
        {
            btnInPhieuXV.Visible = true;
            XuatVien xv = LayThongTinXuatVienBN();

            if (XuatVienBN.KiemTraMaXuatVien(txtMaXuatVien.Text))
            {
                if (XuatVienBN.SuaDoiThongTinXuatVienBN(xv))
                    MessageBox.Show("Cập nhật thông tin phiếu xuất viện bệnh nhân thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (XuatVienBN.LuuThongTinXuatVienBN(xv))
                    MessageBox.Show("Lưu thông tin xuất viện bệnh nhân thành công!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion


        #region Cac event lien quan

        private void cboXuatVien_MaBenhNhan_TextChanged(object sender, EventArgs e)
        {
            LoadAnhTuDB(cboXuatVien_MaBenhNhan.Text, picBenhNhan);
            txtMaXuatVien.Text = XuatVienBN.NextIDXuatVien();

            //Ma doi tuong
            cboMaDoiTuongBN.DataSource = XuatVienBN.LoadMaDoiTuong();
            cboMaDoiTuongBN.DisplayMember = "MaDoiTuongNV";
            cboMaDoiTuongBN.ValueMember = "MaDoiTuong";

            #region DataBindings Textbox

            txtHoBenhNhan.DataBindings.Clear();
            txtTenBenhNhan.DataBindings.Clear();
            txtNgheNghiepBN.DataBindings.Clear();
            txtDiaChiBN.DataBindings.Clear();
            txtMaBenhAn.DataBindings.Clear();
            txtCMND.DataBindings.Clear();
            dtPicker_NgaySinhBN.DataBindings.Clear();
            txtSDTBN.DataBindings.Clear();
            dtPickerNgayVaoVien.DataBindings.Clear();
            txtPPDieuTri.DataBindings.Clear();
            txtChuanDoan.DataBindings.Clear();
            cboGioiTinhBN.DataBindings.Clear();
            txtLyDoNhapVienBN.DataBindings.Clear();

            txtHoBenhNhan.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "Ho");
            txtTenBenhNhan.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "Ten");           
            txtNgheNghiepBN.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "NgheNghiep");
            txtDiaChiBN.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "DiaChi");
            txtMaBenhAn.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "MaBenhAn");
            txtCMND.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "CMND");
            dtPicker_NgaySinhBN.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "NgaySinh");
            txtSDTBN.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "SDT");
            dtPickerNgayVaoVien.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "NgayNhapVien");
            txtPPDieuTri.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "PhuongPhapDieuTri");
            txtChuanDoan.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "KetQuaChuanDoan");
            cboGioiTinhBN.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "GioiTinh");
            txtLyDoNhapVienBN.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "ChuanDoanSauCung");

            #endregion

        }

        private void cboMaDoiTuongBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenDoiTuongBN.DataBindings.Clear();
            txtTenDoiTuongBN.DataBindings.Add("text", XuatVienBN.LayTenDoiTuong(cboMaDoiTuongBN.Text), "TenDoiTuong");
        }

        private void frmXuatVien_Load(object sender, EventArgs e)
        {
            cboXuatVien_MaBenhNhan.DataSource = XuatVienBN.LayDSBenhNhanNhapVien();
            cboXuatVien_MaBenhNhan.DisplayMember = "STT";
            cboXuatVien_MaBenhNhan.ValueMember = "MaBenhNhan";  

        }

        #endregion

        private void dtPickerNgayRaVien_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime nhapvien = dtPickerNgayVaoVien.Value;
                DateTime xuatvien = dtPickerNgayRaVien.Value;
                if ((DateTime.Parse(nhapvien.ToString()) > DateTime.Parse(xuatvien.ToString())))
                {
                    dtPickerNgayRaVien.Text = nhapvien.ToString();

                }
            }
            catch
            {
                MessageBox.Show("Nhập sai ngày xuất viện");
            }
        }

        private void btnInPhieuXV_Click(object sender, EventArgs e)
        {
            btnInPhieuXV.Visible = false;
            btnTroLai.Visible = true;
            expandableSplitter1.Expanded = true;

            PhieuXuatVien rp = new PhieuXuatVien();
            ParameterValues a = new ParameterValues();
            ParameterDiscreteValue b = new ParameterDiscreteValue();

            b.Value = txtMaXuatVien.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["MaPhieu"].ApplyCurrentValues(a);

            b.Value = txtHoBenhNhan.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HoBenhNhan"].ApplyCurrentValues(a);

            b.Value = txtTenBenhNhan.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenBenhNhan"].ApplyCurrentValues(a);

            b.Value = cboGioiTinhBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["GioiTinh"].ApplyCurrentValues(a);

            b.Value = dtPicker_NgaySinhBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgaySinh"].ApplyCurrentValues(a);

            b.Value = cboXuatVien_MaBenhNhan.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["MaBenhNhan"].ApplyCurrentValues(a);

            b.Value = txtDiaChiBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["DiaChi"].ApplyCurrentValues(a);

            b.Value = txtLyDoNhapVienBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["LyDoNhapVien"].ApplyCurrentValues(a);

            b.Value = dtPickerNgayVaoVien.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgayNhapVien"].ApplyCurrentValues(a);

            b.Value = dtPickerNgayRaVien.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgayXuatVien"].ApplyCurrentValues(a);

            b.Value = txtChuanDoan.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["ChanDoan"].ApplyCurrentValues(a);

            b.Value = txtPPDieuTri.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["PhuongPhapDieuTri"].ApplyCurrentValues(a);

            b.Value = txtLoiDanBS.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["LoiDan"].ApplyCurrentValues(a);

            crystalReportViewer1.ReportSource = rp;
        }

        private void btnTroLai_Click(object sender, EventArgs e)
        {
            btnTroLai.Visible = false;
            btnInPhieuXV.Visible = true;
            expandableSplitter1.Expanded = false;
        }

        private void cboXuatVien_MaBenhNhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAnhTuDB(cboXuatVien_MaBenhNhan.Text, picBenhNhan);
            txtMaXuatVien.Text = XuatVienBN.NextIDXuatVien();

            //Ma doi tuong
            cboMaDoiTuongBN.DataSource = XuatVienBN.LayGT_MaDTCuaBenhNhan(cboXuatVien_MaBenhNhan.Text);
            cboMaDoiTuongBN.DisplayMember = "MaDoiTuongNV";
            cboMaDoiTuongBN.ValueMember = "MaDoiTuong";

            //gioitinh
            cboGioiTinhBN.DataSource = XuatVienBN.LayGT_MaDTCuaBenhNhan(cboXuatVien_MaBenhNhan.Text);
            cboGioiTinhBN.DisplayMember = "GioiTinhBN";
            cboGioiTinhBN.ValueMember = "GioiTinh";

            #region DataBindings Textbox

            txtHoBenhNhan.DataBindings.Clear();
            txtTenBenhNhan.DataBindings.Clear();
            txtNgheNghiepBN.DataBindings.Clear();
            txtDiaChiBN.DataBindings.Clear();
            txtMaBenhAn.DataBindings.Clear();
            txtCMND.DataBindings.Clear();
            dtPicker_NgaySinhBN.DataBindings.Clear();
            txtSDTBN.DataBindings.Clear();
            dtPickerNgayVaoVien.DataBindings.Clear();
            txtPPDieuTri.DataBindings.Clear();
            txtChuanDoan.DataBindings.Clear();
            cboGioiTinhBN.DataBindings.Clear();
            txtLyDoNhapVienBN.DataBindings.Clear();

            txtHoBenhNhan.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "Ho");
            txtTenBenhNhan.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "Ten");
            txtNgheNghiepBN.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "NgheNghiep");
            txtDiaChiBN.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "DiaChi");
            txtMaBenhAn.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "MaBenhAn");
            txtCMND.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "CMND");
            dtPicker_NgaySinhBN.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "NgaySinh");
            txtSDTBN.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "SDT");
            dtPickerNgayVaoVien.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "NgayNhapVien");
            txtPPDieuTri.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "PhuongPhapDieuTri");
            txtChuanDoan.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "KetQuaChuanDoan");
            cboGioiTinhBN.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "GioiTinh");
            txtLyDoNhapVienBN.DataBindings.Add("text", XuatVienBN.LayThongTinBNXuatVien(cboXuatVien_MaBenhNhan.Text), "ChuanDoanSauCung");

            #endregion
        }

    }
}
