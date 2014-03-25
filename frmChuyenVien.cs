using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLyBenhVien.BusinessLogic;
using QuanLyBenhVien.BusinessObject;
using System.IO;
using CrystalDecisions.Shared;

namespace QuanLyBenhVien
{
    public partial class frmChuyenVien : Form
    {
        public frmChuyenVien()
        {
            InitializeComponent();
        }

        private ChuyenVienBNBUS ChuyenVienBN = new ChuyenVienBNBUS();
        private HinhAnhBUS ThemHinhAnh = new HinhAnhBUS();
        private XuatVienBNBUS XuatVienBN = new XuatVienBNBUS();

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

        public void addMaBN(TextBox mabenhnhan)
        {
            txtMaBenhNhan.Text = mabenhnhan.Text;
        }

        private ChuyenVien LayThongTinChuyenVien()
        {
            ChuyenVien cv = new ChuyenVien();

            cv.MaChuyenVien = txtMaChuyenVien.Text;
            cv.MaBenhNhan = txtMaBenhNhan.Text;
            cv.MaBenhAn = txtMaBenhAn.Text;
            cv.NgayChuyenVien = Convert.ToDateTime(dtPicker_NgayChuyenVien.Value.ToString());
            cv.GioChuyenVien = Convert.ToDateTime(dtTime_GioChuyenVien.Value.ToString());
            cv.DauHieuLamSang = txtDauHieuLamSang.Text;
            cv.TinhTrangKhiChuyen = txtTinhTrangKhiCV.Text;
            cv.PhuongTienDiChuyen = txtPhuongTienDiChuyen.Text;
            cv.LyDoChuyenVien = txtLyDoChuyenVien.Text;

            return cv;
        }

        #endregion


        #region Cac button

        private void btnLuuThongTinCV_Click(object sender, EventArgs e)
        {
            ChuyenVien cv = LayThongTinChuyenVien();

            if (ChuyenVienBN.KiemTraMaChuyenVien(txtMaChuyenVien.Text))
            {
                if (ChuyenVienBN.SuaDoiThongTinChuyenVien(cv))
                    MessageBox.Show("Sửa đổi thông tin chuyển viện bệnh nhân thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Sửa đổi thông tin chuyển viện bệnh nhân thất bại!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (ChuyenVienBN.LuuThongTinChuyenVien(cv))
                    MessageBox.Show("Lưu thông tin chuyển viện bệnh nhân thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Lưu thông tin chuyển viện bệnh nhân thất bại!", "Thông báo",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDongCV_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


        #region Cac event lien quan

        private void txtMaBenhNhan_TextChanged(object sender, EventArgs e)
        {
            cboMaDoiTuong.DataSource = XuatVienBN.LoadMaDoiTuong();
            cboMaDoiTuong.DisplayMember = "MaDoiTuongNV";
            cboMaDoiTuong.ValueMember = "MaDoiTuong";

            #region Databindings TextBox

            txtHoBenhNhan.DataBindings.Clear();
            txtTenBenhNhan.DataBindings.Clear();  
            dtPicker_NgaySinhBN.DataBindings.Clear();
            txtCMND.DataBindings.Clear();
            txtSDT.DataBindings.Clear();
            txtNgheNghiep.DataBindings.Clear();
            cboMaDoiTuong.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            txtLyDoNV.DataBindings.Clear();
            txtMaBenhAn.DataBindings.Clear();
            txtChuanDoan.DataBindings.Clear();
            txtXNDaLam.DataBindings.Clear();           
            txtThuocDaDung.DataBindings.Clear();
            cboGioiTinhBN.DataBindings.Clear();
            txtMaChuyenVien.DataBindings.Clear();

            txtHoBenhNhan.DataBindings.Add("text", ChuyenVienBN.LayThongTinBNChuyenVien(txtMaBenhNhan.Text), "Ho");
            txtTenBenhNhan.DataBindings.Add("text", ChuyenVienBN.LayThongTinBNChuyenVien(txtMaBenhNhan.Text), "Ten");
            dtPicker_NgaySinhBN.DataBindings.Add("text", ChuyenVienBN.LayThongTinBNChuyenVien(txtMaBenhNhan.Text), "NgaySinh");
            txtCMND.DataBindings.Add("text", ChuyenVienBN.LayThongTinBNChuyenVien(txtMaBenhNhan.Text), "CMND");
            txtSDT.DataBindings.Add("text", ChuyenVienBN.LayThongTinBNChuyenVien(txtMaBenhNhan.Text), "SDT");
            txtNgheNghiep.DataBindings.Add("text", ChuyenVienBN.LayThongTinBNChuyenVien(txtMaBenhNhan.Text), "NgheNghiep");
            cboMaDoiTuong.DataBindings.Add("text", ChuyenVienBN.LayThongTinBNChuyenVien(txtMaBenhNhan.Text), "MaDoiTuong");
            txtDiaChi.DataBindings.Add("text", ChuyenVienBN.LayThongTinBNChuyenVien(txtMaBenhNhan.Text), "DiaChi");
            txtLyDoNV.DataBindings.Add("text", ChuyenVienBN.LayThongTinBNChuyenVien(txtMaBenhNhan.Text), "ChuanDoanSauCung");
            txtMaBenhAn.DataBindings.Add("text", ChuyenVienBN.LayThongTinBNChuyenVien(txtMaBenhNhan.Text), "MaBenhAn");
            txtChuanDoan.DataBindings.Add("text", ChuyenVienBN.LayThongTinBNChuyenVien(txtMaBenhNhan.Text), "KetQuaChuanDoan");
            txtXNDaLam.DataBindings.Add("text", ChuyenVienBN.LayXetNghiemCuaBN(txtMaBenhNhan.Text), "YeuCauXetNghiem");
            txtThuocDaDung.DataBindings.Add("text", ChuyenVienBN.DanhSachThuocBN(txtMaBenhNhan.Text), "TenThuoc");
            cboGioiTinhBN.DataBindings.Add("text", ChuyenVienBN.LayThongTinBNChuyenVien(txtMaBenhNhan.Text), "GioiTinh");
            txtMaChuyenVien.DataBindings.Add("text", ChuyenVienBN.LayMaChuyenVienBNNeuCo(txtMaBenhNhan.Text), "MaChuyenVien");

            #endregion
        }

        private void cboMaDoiTuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenDoiTuong.DataBindings.Clear();
            txtTenDoiTuong.DataBindings.Add("text", ChuyenVienBN.LayTenDoiTuong(cboMaDoiTuong.Text), "TenDoiTuong");
        }

        private void frmChuyenVien_Load(object sender, EventArgs e)
        {          
            if(txtMaChuyenVien.Text != "")
            {
                txtDauHieuLamSang.DataBindings.Clear();
                txtLyDoChuyenVien.DataBindings.Clear();
                txtTinhTrangKhiCV.DataBindings.Clear();
                txtPhuongTienDiChuyen.DataBindings.Clear();

                txtDauHieuLamSang.DataBindings.Add("text", ChuyenVienBN.LayThongTinChuyenVienBN(txtMaChuyenVien.Text), "DauHieuLamSang");
                txtLyDoChuyenVien.DataBindings.Add("text", ChuyenVienBN.LayThongTinChuyenVienBN(txtMaChuyenVien.Text), "LyDoChuyenVien");
                txtTinhTrangKhiCV.DataBindings.Add("text", ChuyenVienBN.LayThongTinChuyenVienBN(txtMaChuyenVien.Text), "TinhTrangKhiChuyen");
                txtPhuongTienDiChuyen.DataBindings.Add("text", ChuyenVienBN.LayThongTinChuyenVienBN(txtMaChuyenVien.Text), "PhuongTienDiChuyen");

            }
            else
            {
                txtMaChuyenVien.Text = ChuyenVienBN.NextIDChuyenVien();
                txtDauHieuLamSang.Text = "";
                txtLyDoChuyenVien.Text = "";
                txtTinhTrangKhiCV.Text = "";
                txtPhuongTienDiChuyen.Text = "";
            }

            LoadAnhTuDB(txtMaBenhNhan.Text, picBNChuyenVien);

            cboGioiTinhBN.DataSource = XuatVienBN.LayGT_MaDTCuaBenhNhan(txtMaBenhNhan.Text);
            cboGioiTinhBN.DisplayMember = "GTBN";
            cboGioiTinhBN.ValueMember = "GioiTinh";

          
        }

        #endregion

        private void txtMaChuyenVien_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtPicker_NgayChuyenVien_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime kiemtra = dtPicker_NgayChuyenVien.Value;
            if ((DateTime.Parse(now.ToString()) > DateTime.Parse(kiemtra.ToString())))
            {
                dtPicker_NgayChuyenVien.Text = "";

            }
        }

        private void btnInPhieuCV_Click(object sender, EventArgs e)
        {
            expandableSplitter1.Expanded = true;
            btnInPhieuCV.Visible = false;
            btnTroLai.Visible = true;

            PhieuChuyenVien rp = new PhieuChuyenVien();
            ParameterValues a = new ParameterValues();
            ParameterDiscreteValue b = new ParameterDiscreteValue();

            b.Value = txtMaChuyenVien.Text;
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

            b.Value = txtDiaChi.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["DiaChi"].ApplyCurrentValues(a);

            b.Value = txtLyDoNV.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["LyDoNhapVien"].ApplyCurrentValues(a);

            b.Value = dtPicker_NgayChuyenVien.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgayChuyen"].ApplyCurrentValues(a);

            b.Value = dtTime_GioChuyenVien.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["GioChuyen"].ApplyCurrentValues(a);

            b.Value = txtXNDaLam.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["XetNghiemDaLam"].ApplyCurrentValues(a);

            b.Value = txtChuanDoan.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["ChanDoan"].ApplyCurrentValues(a);

            b.Value = txtThuocDaDung.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["Thuoc"].ApplyCurrentValues(a);

            b.Value = txtDauHieuLamSang.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["DauHieuLamSang"].ApplyCurrentValues(a);

            b.Value = txtLyDoChuyenVien.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["LyDoChuyen"].ApplyCurrentValues(a);

            b.Value = txtTinhTrangKhiCV.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TinhTrang"].ApplyCurrentValues(a);

            b.Value = txtPhuongTienDiChuyen.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["PhuongTien"].ApplyCurrentValues(a);

            crystalReportViewer1.ReportSource = rp;
        }

        private void btnTroLai_Click(object sender, EventArgs e)
        {
            expandableSplitter1.Expanded = false;
            btnInPhieuCV.Visible = true;
            btnTroLai.Visible = false;
        }

    }
}
