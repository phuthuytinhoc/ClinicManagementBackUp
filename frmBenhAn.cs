using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
////using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLyBenhVien.BusinessLogic;
using System.IO;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using QuanLyBenhVien.DataAccess;


namespace QuanLyBenhVien
{
    public partial class frmBenhAn : Form
    {
        public frmBenhAn()
        {
            InitializeComponent();
            btnTroLai.Visible = false;
            expandableSplitter1.Visible = false;
        }

        private BenhAnBNBUS BenhAnBN = new BenhAnBNBUS();
        private TheoDoiBNBUS TheoDoiBN = new TheoDoiBNBUS();
        private ChuyenVienBNBUS ChuyenVienBN = new ChuyenVienBNBUS();
        private HinhAnhBUS ThemHinhAnh = new HinhAnhBUS();
        private ConnectData connectData = new ConnectData();

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

        public void AddMaBNBenhAn(TextBox text)
        {
            txtBenhNhan_MaBN.Text = text.Text;
        }

        #endregion

        #region Cac Button 

        private void btnDongBenhAn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public delegate void delAnhXQuangBenhAn(TextBox MaAnh);
        private void btnXQuang_PhongToAnh_Click(object sender, EventArgs e)
        {
            frmPhongToHinhX_QuangBN frmzoom = new frmPhongToHinhX_QuangBN();
            delAnhXQuangBenhAn delPhongtoXQuang = new delAnhXQuangBenhAn(frmzoom.AddText);
            delPhongtoXQuang(this.txtXQuang_MaPhieuXQ);
            frmzoom.ShowDialog();
        }


        #endregion

        #region Cac event lien quan

        //load thong tin 
        private void frmBenhAn_Load(object sender, EventArgs e)
        {
            //load anh benh nhan, anh chup XQuang
               

            #region Thong Tin Benh Nhan

            txtBenhNhan_HoBN.DataBindings.Clear();
            txtBenhNhan_TenBN.DataBindings.Clear();
            txtBenhNhan_GioiTinhBN.DataBindings.Clear();
            txtBenhNhan_NgheNghiep.DataBindings.Clear();
            txtBenhNhan_MaDoiTuong.DataBindings.Clear();
            txtBenhNhan_SDTBN.DataBindings.Clear();
            txtBenhNhan_CMNDBN.DataBindings.Clear();
            txtBenhNhan_DiaChiBN.DataBindings.Clear();
            dtPickerBenhNhan_NgaySinhBN.DataBindings.Clear();
            txtChuyenVien_MaBenhAn.DataBindings.Clear();

            txtBenhNhan_HoBN.DataBindings.Add("text", BenhAnBN.LayThongTinBenhNhanBenhAn(txtBenhNhan_MaBN.Text), "Ho");
            txtBenhNhan_TenBN.DataBindings.Add("text", BenhAnBN.LayThongTinBenhNhanBenhAn(txtBenhNhan_MaBN.Text), "Ten");
            txtBenhNhan_GioiTinhBN.DataBindings.Add("text", BenhAnBN.LayThongTinBenhNhanBenhAn(txtBenhNhan_MaBN.Text), "GioiTinh");
            txtBenhNhan_NgheNghiep.DataBindings.Add("text", BenhAnBN.LayThongTinBenhNhanBenhAn(txtBenhNhan_MaBN.Text), "NgheNghiep");
            txtBenhNhan_MaDoiTuong.DataBindings.Add("text", BenhAnBN.LayThongTinBenhNhanBenhAn(txtBenhNhan_MaBN.Text), "MaDoiTuong");
            txtBenhNhan_SDTBN.DataBindings.Add("text", BenhAnBN.LayThongTinBenhNhanBenhAn(txtBenhNhan_MaBN.Text), "SDT");
            txtBenhNhan_CMNDBN.DataBindings.Add("text", BenhAnBN.LayThongTinBenhNhanBenhAn(txtBenhNhan_MaBN.Text), "CMND");
            txtBenhNhan_DiaChiBN.DataBindings.Add("text", BenhAnBN.LayThongTinBenhNhanBenhAn(txtBenhNhan_MaBN.Text), "DiaChi");
            dtPickerBenhNhan_NgaySinhBN.DataBindings.Add("text", BenhAnBN.LayThongTinBenhNhanBenhAn(txtBenhNhan_MaBN.Text), "NgaySinh");
            txtChuyenVien_MaBenhAn.DataBindings.Add("text", BenhAnBN.LayThongTinBenhNhanBenhAn(txtBenhNhan_MaBN.Text), "MaBenhAn");

            #endregion

            #region Thong Tin Kham Benh

            txtKBenh_CanNangBN.DataBindings.Clear();
            txtKBenh_NhomMauBN.DataBindings.Clear();
            txtKBenh_MachBN.DataBindings.Clear();
            txtKBenh_HuyetApBN.DataBindings.Clear();
            txtKBenh_NhipThoBN.DataBindings.Clear();
            txtKBenh_NhietDoBN.DataBindings.Clear();
            txtKBenh_YeuCauKham.DataBindings.Clear();
            txtKBenh_TinhTrangHienTai.DataBindings.Clear();
            txtKBenh_BenhSuBN.DataBindings.Clear();
            txtKBenh_ChuanDoanSoBo.DataBindings.Clear();
            txtKBenh_ChuanDoanSauCung.DataBindings.Clear();
            txtKBenh_HuongDieuTri.DataBindings.Clear();

            txtKBenh_CanNangBN.DataBindings.Add("text", BenhAnBN.LayThongTinPhongKhamBenhAn(txtBenhNhan_MaBN.Text), "CanNang");
            txtKBenh_NhomMauBN.DataBindings.Add("text", BenhAnBN.LayThongTinPhongKhamBenhAn(txtBenhNhan_MaBN.Text), "NhomMau");
            txtKBenh_MachBN.DataBindings.Add("text", BenhAnBN.LayThongTinPhongKhamBenhAn(txtBenhNhan_MaBN.Text), "Mach");
            txtKBenh_HuyetApBN.DataBindings.Add("text", BenhAnBN.LayThongTinPhongKhamBenhAn(txtBenhNhan_MaBN.Text), "HuyetAp");
            txtKBenh_NhipThoBN.DataBindings.Add("text", BenhAnBN.LayThongTinPhongKhamBenhAn(txtBenhNhan_MaBN.Text), "NhipTho");
            txtKBenh_NhietDoBN.DataBindings.Add("text", BenhAnBN.LayThongTinPhongKhamBenhAn(txtBenhNhan_MaBN.Text), "NhietDo");
            txtKBenh_YeuCauKham.DataBindings.Add("text", BenhAnBN.LayThongTinPhongKhamBenhAn(txtBenhNhan_MaBN.Text), "YeuCauKham");
            txtKBenh_TinhTrangHienTai.DataBindings.Add("text", BenhAnBN.LayThongTinPhongKhamBenhAn(txtBenhNhan_MaBN.Text), "TinhTrangHienTai");
            txtKBenh_BenhSuBN.DataBindings.Add("text", BenhAnBN.LayThongTinPhongKhamBenhAn(txtBenhNhan_MaBN.Text), "BenhSu");
            txtKBenh_ChuanDoanSoBo.DataBindings.Add("text", BenhAnBN.LayThongTinPhongKhamBenhAn(txtBenhNhan_MaBN.Text), "ChuanDoanSoBo");
            txtKBenh_ChuanDoanSauCung.DataBindings.Add("text", BenhAnBN.LayThongTinPhongKhamBenhAn(txtBenhNhan_MaBN.Text), "ChuanDoanSauCung");
            txtKBenh_HuongDieuTri.DataBindings.Add("text", BenhAnBN.LayThongTinPhongKhamBenhAn(txtBenhNhan_MaBN.Text), "HuongDieuTri");

            #endregion

            #region Thong tin Don Thuoc

            txtDonThuoc_MaDonThuoc.DataBindings.Clear();
            dtPickerDonThuoc_GioTao.DataBindings.Clear();
            dtPickerDonThuoc_NgayTao.DataBindings.Clear();
            txtDonThuoc_MaBS.DataBindings.Clear();
            txtDonThuoc_LoiDanBS.DataBindings.Clear();

            txtDonThuoc_MaDonThuoc.DataBindings.Add("text", BenhAnBN.LayThongTinDonThuoc(txtBenhNhan_MaBN.Text), "MaDonThuoc");
            dtPickerDonThuoc_GioTao.DataBindings.Add("text", BenhAnBN.LayThongTinDonThuoc(txtBenhNhan_MaBN.Text), "GioTao");
            dtPickerDonThuoc_NgayTao.DataBindings.Add("text", BenhAnBN.LayThongTinDonThuoc(txtBenhNhan_MaBN.Text), "NgayTao");
            txtDonThuoc_MaBS.DataBindings.Add("text", BenhAnBN.LayThongTinDonThuoc(txtBenhNhan_MaBN.Text), "MaBacSi");
            txtDonThuoc_LoiDanBS.DataBindings.Add("text", BenhAnBN.LayThongTinDonThuoc(txtBenhNhan_MaBN.Text), "LoiDan");

            #endregion

            #region Thong Tin Xet Nghiem

            txtXetNghiem_MaPhieuXN.DataBindings.Clear();
            dtPickerXetNgiem_NgayXN.DataBindings.Clear();
            txtXetNghiem_MaPhongXN.DataBindings.Clear();
            txtXetNghiem_YeuCauXN.DataBindings.Clear();
            txtXetNghiem_KetLuan.DataBindings.Clear();
            txtXetNghiem_DeNghi.DataBindings.Clear();


            txtXetNghiem_MaPhieuXN.DataBindings.Add("text", BenhAnBN.LayThongTinXetNghiem(txtBenhNhan_MaBN.Text), "MaXetNghiem");
            dtPickerXetNgiem_NgayXN.DataBindings.Add("text", BenhAnBN.LayThongTinXetNghiem(txtBenhNhan_MaBN.Text), "NgayXetNghiem");
            txtXetNghiem_MaPhongXN.DataBindings.Add("text", BenhAnBN.LayThongTinXetNghiem(txtBenhNhan_MaBN.Text), "MaPhongXetNghiem");
            txtXetNghiem_YeuCauXN.DataBindings.Add("text", BenhAnBN.LayThongTinXetNghiem(txtBenhNhan_MaBN.Text), "YeuCauXetNghiem");
            txtXetNghiem_KetLuan.DataBindings.Add("text", BenhAnBN.LayThongTinXetNghiem(txtBenhNhan_MaBN.Text), "KetQua");
            txtXetNghiem_DeNghi.DataBindings.Add("text", BenhAnBN.LayThongTinXetNghiem(txtBenhNhan_MaBN.Text), "DeNghi");

            #endregion

            #region Thong Tin X Quang

            txtXQuang_MaPhieuXQ.DataBindings.Clear();
            txtXQuang_MaPhongXQ.DataBindings.Clear();
            txtXQuang_KyThuatXQ.DataBindings.Clear();
            txtXQuang_KyThuatChupXQ.DataBindings.Clear();
            txtXQuang_ChuanDoanXQ.DataBindings.Clear();
            dtPickerXQuang_NgayChup.DataBindings.Clear();
            txtXQuang_MoTaAnh.DataBindings.Clear();
            txtXQuang_KetLuan.DataBindings.Clear();
            txtXQuang_DeNghi.DataBindings.Clear();

            txtXQuang_MaPhieuXQ.DataBindings.Add("text", BenhAnBN.LayThongTinXQuang(txtBenhNhan_MaBN.Text), "MaPhieuChup");
            txtXQuang_MaPhongXQ.DataBindings.Add("text", BenhAnBN.LayThongTinXQuang(txtBenhNhan_MaBN.Text),"MaPhongXQ");
            txtXQuang_KyThuatXQ.DataBindings.Add("text", BenhAnBN.LayThongTinXQuang(txtBenhNhan_MaBN.Text), "KyThuat");
            txtXQuang_KyThuatChupXQ.DataBindings.Add("text", BenhAnBN.LayThongTinXQuang(txtBenhNhan_MaBN.Text), "KyThuatChup");
            txtXQuang_ChuanDoanXQ.DataBindings.Add("text", BenhAnBN.LayThongTinXQuang(txtBenhNhan_MaBN.Text), "ChuanDoan");
            dtPickerXQuang_NgayChup.DataBindings.Add("text", BenhAnBN.LayThongTinXQuang(txtBenhNhan_MaBN.Text), "NgayChup");
            txtXQuang_MoTaAnh.DataBindings.Add("text", BenhAnBN.LayThongTinXQuang(txtBenhNhan_MaBN.Text), "MoTaAnhChup");
            txtXQuang_KetLuan.DataBindings.Add("text", BenhAnBN.LayThongTinXQuang(txtBenhNhan_MaBN.Text), "KetLuan");
            txtXQuang_DeNghi.DataBindings.Add("text", BenhAnBN.LayThongTinXQuang(txtBenhNhan_MaBN.Text), "DeNghi");


            #endregion

            #region Thong Tin Theo Doi
            string a = dtPickerTheoDoi_NgayTD.Value.ToString("MM/dd/yyyy");

            txtTheoDoi_MaTheoDoi.DataBindings.Clear();

            txtTheoDoi_Mach.DataBindings.Clear();
            txtTheoDoi_NhipTho.DataBindings.Clear();
            txtTheoDoi_NhietDo.DataBindings.Clear();
            txtTheoDoi_HuyetAp.DataBindings.Clear();
            txtTheoDoi_CanNang.DataBindings.Clear();
            txtTheoDoi_DienBien.DataBindings.Clear();
            txtTheoDoi_XuLiChamSoc.DataBindings.Clear();
            txtTheoDoi_ThuocVatTu.DataBindings.Clear();

            txtTheoDoi_MaTheoDoi.DataBindings.Add("Text", BenhAnBN.LayMaTheoDoiBN(txtBenhNhan_MaBN.Text), "MaTheoDoi");

            txtTheoDoi_Mach.DataBindings.Add("text", BenhAnBN.LayThongTinTheoDoiTheoNgayTheoDoi(a), "Mach");
            txtTheoDoi_NhipTho.DataBindings.Add("text", BenhAnBN.LayThongTinTheoDoiTheoNgayTheoDoi(a), "NhipTho");
            txtTheoDoi_NhietDo.DataBindings.Add("text", BenhAnBN.LayThongTinTheoDoiTheoNgayTheoDoi(a), "NhietDo");
            txtTheoDoi_HuyetAp.DataBindings.Add("text", BenhAnBN.LayThongTinTheoDoiTheoNgayTheoDoi(a), "HuyetAp");
            txtTheoDoi_CanNang.DataBindings.Add("text", BenhAnBN.LayThongTinTheoDoiTheoNgayTheoDoi(a), "CanNang");
            txtTheoDoi_DienBien.DataBindings.Add("text", BenhAnBN.LayThongTinTheoDoiTheoNgayTheoDoi(a), "DienBien");
            txtTheoDoi_XuLiChamSoc.DataBindings.Add("text", BenhAnBN.LayThongTinTheoDoiTheoNgayTheoDoi(a), "XuLiChamSoc");
            txtTheoDoi_ThuocVatTu.DataBindings.Add("text", BenhAnBN.LayThongTinTheoDoiTheoNgayTheoDoi(a), "ThuocVatTu");


            #endregion

            #region Thong Tin Phong Benh

            txtPhongBenh_MaPB.DataBindings.Clear();
            txtPhongBenh_MaGiuongBenh.DataBindings.Clear();
            txtPhongBenh_MaKhoa.DataBindings.Clear();
            txtPhongBenh_MaBS.DataBindings.Clear();
            txtPhongBenh_MaYTa.DataBindings.Clear();
            txtTheoDoi_MaNgThucHien.DataBindings.Clear();

            txtPhongBenh_MaPB.DataBindings.Add("text", BenhAnBN.LayThongTinPhongBenh(txtBenhNhan_MaBN.Text), "MaPhongBenh");
            txtPhongBenh_MaGiuongBenh.DataBindings.Add("text", BenhAnBN.LayThongTinPhongBenh(txtBenhNhan_MaBN.Text), "MaGiuongBenh");
            txtPhongBenh_MaKhoa.DataBindings.Add("text", BenhAnBN.LayThongTinPhongBenh(txtBenhNhan_MaBN.Text), "MaKhoa");

            txtPhongBenh_MaBS.DataBindings.Add("text", BenhAnBN.LayMacBacSiPhongBenh(txtPhongBenh_MaPB.Text), "MaBacSi");
            txtPhongBenh_MaYTa.DataBindings.Add("text", BenhAnBN.LayMaYTaPhongBenh(txtPhongBenh_MaPB.Text), "MaYTa");
            txtTheoDoi_MaNgThucHien.DataBindings.Add("text", BenhAnBN.LayMaYTaPhongBenh(txtPhongBenh_MaPB.Text), "MaYTa");

            #endregion

            #region Thong Tin Nhap Vien

            txtNhapVien_MaNV.DataBindings.Clear();
            dtPickerNhapVien_NgayNV.DataBindings.Clear();
            txtNhapVien_KetQuaCD.DataBindings.Clear();
            txtNhapVien_PPDieuTri.DataBindings.Clear();
            txtNhapVien_MaBS.DataBindings.Clear();
            txtPhongBenh_HoBS.DataBindings.Clear();
            txtPhongBenh_TenBS.DataBindings.Clear();



            txtNhapVien_MaNV.DataBindings.Add("text", BenhAnBN.LayThongTinNhapVien(txtBenhNhan_MaBN.Text), "MaNhapVien");
            dtPickerNhapVien_NgayNV.DataBindings.Add("text", BenhAnBN.LayThongTinNhapVien(txtBenhNhan_MaBN.Text), "NgayNhapVien");
            txtNhapVien_KetQuaCD.DataBindings.Add("text", BenhAnBN.LayThongTinNhapVien(txtBenhNhan_MaBN.Text), "KetQuaChuanDoan");
            txtNhapVien_PPDieuTri.DataBindings.Add("text", BenhAnBN.LayThongTinNhapVien(txtBenhNhan_MaBN.Text), "PhuongPhapDieuTri");
            txtNhapVien_MaBS.DataBindings.Add("text", BenhAnBN.LayMacBacSiPhongBenh(txtPhongBenh_MaPB.Text), "MaBacSi");
            txtNhapVien_HoBS.DataBindings.Add("text", BenhAnBN.LayHoTenBacSiTuMaBacSi(txtPhongBenh_MaBS.Text), "Ho");
            txtNhapVien_TenBS.DataBindings.Add("text", BenhAnBN.LayHoTenBacSiTuMaBacSi(txtPhongBenh_MaBS.Text), "Ten");

            #endregion

            #region Thong Tin Chuyen vien

            dtPickerChuyenVien_NgayCV.DataBindings.Clear();
            dtPickerChuyenVien_GioCV.DataBindings.Clear();
            txtChuyenVien_DauHieuLamSang.DataBindings.Clear();
            txtChuyenVien_YeuCauXN.DataBindings.Clear();
            txtChuyenVien_ChuanDoan.DataBindings.Clear();
            txtChuyenVien_ThuocDaDung.DataBindings.Clear();
            txtChuyenVien_LyDo.DataBindings.Clear();
            txtChuyenVien_TTKhiCV.DataBindings.Clear();
            txtChuyenVien_PhuongTienDiChuyen.DataBindings.Clear();

            dtPickerChuyenVien_NgayCV.DataBindings.Add("text", BenhAnBN.LayThongTinChuyenVien(txtBenhNhan_MaBN.Text), "NgayChuyenVien");
            dtPickerChuyenVien_GioCV.DataBindings.Add("text", BenhAnBN.LayThongTinChuyenVien(txtBenhNhan_MaBN.Text), "GioChuyenVien");
            txtChuyenVien_DauHieuLamSang.DataBindings.Add("text", BenhAnBN.LayThongTinChuyenVien(txtBenhNhan_MaBN.Text), "DauHieuLamSang");
            txtChuyenVien_YeuCauXN.DataBindings.Add("text", BenhAnBN.LayYeuCauXetNghiemTuMaBenhNhan(txtBenhNhan_MaBN.Text), "YeuCauXetNghiem");
            txtChuyenVien_ChuanDoan.DataBindings.Add("text", BenhAnBN.LayThongTinPhongKhamBenhAn(txtBenhNhan_MaBN.Text), "ChuanDoanSauCung");
            txtChuyenVien_ThuocDaDung.DataBindings.Add("text", ChuyenVienBN.DanhSachThuocBN(txtBenhNhan_MaBN.Text), "TenThuoc");
            txtChuyenVien_LyDo.DataBindings.Add("text", BenhAnBN.LayThongTinChuyenVien(txtBenhNhan_MaBN.Text), "LyDoChuyenVien");
            txtChuyenVien_TTKhiCV.DataBindings.Add("text", BenhAnBN.LayThongTinChuyenVien(txtBenhNhan_MaBN.Text), "TinhTrangKhiChuyen");
            txtChuyenVien_PhuongTienDiChuyen.DataBindings.Add("text", BenhAnBN.LayThongTinChuyenVien(txtBenhNhan_MaBN.Text), "PhuongTienDiChuyen");

            

            #endregion

            #region Thong Tin Xuat Vien

            txtXuatVien_MaXV.DataBindings.Clear();
            dtPickerXuatVien_NgayVaoVien.DataBindings.Clear();
            dtPickerXuatVien_NgayRaVien.DataBindings.Clear();
            txtXuatVien_ChuanDoan.DataBindings.Clear();
            txtXuatVien_PPDieuTri.DataBindings.Clear();
            txtXuatVien_LoiDanBS.DataBindings.Clear();

            txtXuatVien_MaXV.DataBindings.Add("text", BenhAnBN.LayThongTinXuatVien(txtChuyenVien_MaBenhAn.Text), "MaXuatVien");
            dtPickerXuatVien_NgayVaoVien.DataBindings.Add("text", BenhAnBN.LayThongTinXuatVien(txtChuyenVien_MaBenhAn.Text), "NgayNhapVien");
            dtPickerXuatVien_NgayRaVien.DataBindings.Add("text", BenhAnBN.LayThongTinXuatVien(txtChuyenVien_MaBenhAn.Text), "NgayXuatVien");
            txtXuatVien_ChuanDoan.DataBindings.Add("text", BenhAnBN.LayThongTinXuatVien(txtChuyenVien_MaBenhAn.Text), "ChuanDoan");
            txtXuatVien_PPDieuTri.DataBindings.Add("text", BenhAnBN.LayThongTinXuatVien(txtChuyenVien_MaBenhAn.Text), "PhuongPhapDieuTri");
            txtXuatVien_LoiDanBS.DataBindings.Add("text", BenhAnBN.LayThongTinXuatVien(txtChuyenVien_MaBenhAn.Text), "LoiDanBS");

            #endregion

            LoadAnhTuDB(txtXQuang_MaPhieuXQ.Text, picXQuang);
            LoadAnhTuDB(txtBenhNhan_MaBN.Text, picBenhNhan);     
        }
     
        private void txtBenhNhan_MaDoiTuong_TextChanged(object sender, EventArgs e)
        {
            txtBenhNhan_TenDoiTuong.DataBindings.Clear();
            txtBenhNhan_TenDoiTuong.DataBindings.Add("text", BenhAnBN.LayTenDoiTuongTuMaDoiTuong(txtBenhNhan_MaDoiTuong.Text), "TenDoiTuong");
        }

        private void txtDonThuoc_MaBS_TextChanged(object sender, EventArgs e)
        {
            txtDonThuoc_HoBS.DataBindings.Clear();
            txtDonThuoc_TenBS.DataBindings.Clear();

            txtDonThuoc_HoBS.DataBindings.Add("text", BenhAnBN.LayHoTenBacSiTuMaBacSi(txtDonThuoc_MaBS.Text), "Ho");
            txtDonThuoc_TenBS.DataBindings.Add("text", BenhAnBN.LayHoTenBacSiTuMaBacSi(txtDonThuoc_MaBS.Text), "Ten");
           
        }

        private void txtBenhNhan_MaBN_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtXQuang_MaPhongXQ_TextChanged(object sender, EventArgs e)
        {
            txtXQuang_TenPhongXQ.DataBindings.Clear();
            txtXQuang_MaKTV.DataBindings.Clear();
            txtXQuang_HoKTV.DataBindings.Clear();
            txtXQuang_TenKTV.DataBindings.Clear();

            txtXQuang_TenPhongXQ.DataBindings.Add("text", BenhAnBN.LayTenPhongXQuang(txtXQuang_MaPhongXQ.Text), "TenPhongLamViec");
            txtXQuang_MaKTV.DataBindings.Add("text", BenhAnBN.LayMaHoTenNhanVienTuMaPhongXQXN(txtXQuang_MaPhongXQ.Text), "MaNhanVien");
            txtXQuang_HoKTV.DataBindings.Add("text", BenhAnBN.LayMaHoTenNhanVienTuMaPhongXQXN(txtXQuang_MaPhongXQ.Text), "Ho");
            txtXQuang_TenKTV.DataBindings.Add("text", BenhAnBN.LayMaHoTenNhanVienTuMaPhongXQXN(txtXQuang_MaPhongXQ.Text), "Ten");
        }

        private void txtXetNghiem_MaPhongXN_TextChanged(object sender, EventArgs e)
        {
            txtXetNghiem_TenPhongXN.DataBindings.Clear();
            txtXetNghiem_MaKTV.DataBindings.Clear();
            txtXetNghiem_HoKTV.DataBindings.Clear();
            txtXetNghiem_TenKTV.DataBindings.Clear();

            txtXetNghiem_TenPhongXN.DataBindings.Add("text", BenhAnBN.LayTenPhongXQuang(txtXetNghiem_MaPhongXN.Text), "TenPhongLamViec");
            txtXetNghiem_MaKTV.DataBindings.Add("text", BenhAnBN.LayMaHoTenNhanVienTuMaPhongXQXN(txtXetNghiem_MaPhongXN.Text), "MaNhanVien");
            txtXetNghiem_HoKTV.DataBindings.Add("text", BenhAnBN.LayMaHoTenNhanVienTuMaPhongXQXN(txtXetNghiem_MaPhongXN.Text), "Ho");
            txtXetNghiem_TenKTV.DataBindings.Add("text", BenhAnBN.LayMaHoTenNhanVienTuMaPhongXQXN(txtXetNghiem_MaPhongXN.Text), "Ten");
        }

        private void txtPhongBenh_MaKhoa_TextChanged(object sender, EventArgs e)
        {
            txtPhongBenh_TenKhoa.DataBindings.Clear();
            txtPhongBenh_TenKhoa.DataBindings.Add("text", BenhAnBN.LayTenKhoaTuMaKhoa(txtPhongBenh_MaKhoa.Text), "TenKhoa");
        }

        private void txtPhongBenh_MaBS_TextChanged(object sender, EventArgs e)
        {
            txtPhongBenh_HoBS.DataBindings.Clear();
            txtPhongBenh_TenBS.DataBindings.Clear();

            txtPhongBenh_HoBS.DataBindings.Add("text", BenhAnBN.LayHoTenBacSiTuMaBacSi(txtPhongBenh_MaBS.Text), "Ho");
            txtPhongBenh_TenBS.DataBindings.Add("text", BenhAnBN.LayHoTenBacSiTuMaBacSi(txtPhongBenh_MaBS.Text), "Ten");
        }

        private void txtPhongBenh_MaYTa_TextChanged(object sender, EventArgs e)
        {
            txtPhongBenh_HoYT.DataBindings.Clear();
            txtPhongBenh_TenYTa.DataBindings.Clear();

            txtPhongBenh_HoYT.DataBindings.Add("text", BenhAnBN.LayHoTenYTaTuMaYTa(txtPhongBenh_MaYTa.Text), "Ho");
            txtPhongBenh_TenYTa.DataBindings.Add("text", BenhAnBN.LayHoTenYTaTuMaYTa(txtPhongBenh_MaYTa.Text), "Ten");
        }

        private void dtPickerTheoDoi_NgayTD_ValueChanged(object sender, EventArgs e)
        {
             string a = dtPickerTheoDoi_NgayTD.Value.ToString("MM/dd/yyyy");

            if (TheoDoiBN.KiemTraNgayTheoDoiTrongCTTD(a))
            {
                #region databindings

                txtTheoDoi_NhanDinhTT.DataBindings.Clear();
                txtTheoDoi_KeHoachChamSoc.DataBindings.Clear();
                txtTheoDoi_Mach.DataBindings.Clear();
                txtTheoDoi_NhietDo.DataBindings.Clear();
                txtTheoDoi_NhipTho.DataBindings.Clear();
                txtTheoDoi_HuyetAp.DataBindings.Clear();
                txtTheoDoi_CanNang.DataBindings.Clear();
                txtTheoDoi_DienBien.DataBindings.Clear();
                txtTheoDoi_XuLiChamSoc.DataBindings.Clear();
                txtTheoDoi_ThuocVatTu.DataBindings.Clear();

                txtTheoDoi_NhanDinhTT.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "NhanDinhTinhTrang");
                txtTheoDoi_KeHoachChamSoc.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "KeHoachChamSoc");
                txtTheoDoi_Mach.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "Mach");
                txtTheoDoi_NhietDo.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "NhietDo");
                txtTheoDoi_NhipTho.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "NhipTho");
                txtTheoDoi_HuyetAp.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "HuyetAp");
                txtTheoDoi_CanNang.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "CanNang");
                txtTheoDoi_DienBien.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "DienBien");
                txtTheoDoi_XuLiChamSoc.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "XuLiChamSoc");
                txtTheoDoi_ThuocVatTu.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "ThuocVatTu");

                #endregion
            }
            else
            {
                txtTheoDoi_NhanDinhTT.Text = "";
                txtTheoDoi_KeHoachChamSoc.Text = "";
                txtTheoDoi_Mach.Text = "";
                txtTheoDoi_NhietDo.Text = "";
                txtTheoDoi_NhipTho.Text = "";
                txtTheoDoi_HuyetAp.Text = "";
                txtTheoDoi_CanNang.Text = "";
                txtTheoDoi_DienBien.Text = "";
                txtTheoDoi_XuLiChamSoc.Text = "";
                txtTheoDoi_ThuocVatTu.Text = "";
            }        
        }

        private void txtPhongBenh_MaPB_TextChanged(object sender, EventArgs e)
        {
            txtPhongBenh_TenPB.DataBindings.Clear();
            txtPhongBenh_TenPB.DataBindings.Add("text", BenhAnBN.LayLayTenPhongBenh(txtPhongBenh_MaPB.Text), "TenPhong");            
        }

        private void txtTheoDoi_MaNgThucHien_TextChanged(object sender, EventArgs e)
        {
            txtTheoDoi_HoNgThucHien.DataBindings.Clear();
            txtTheoDoi_TenNgThucHien.DataBindings.Clear();

            txtTheoDoi_HoNgThucHien.DataBindings.Add("text", BenhAnBN.LayHoTenYTaTuMaYTa(txtPhongBenh_MaYTa.Text), "Ho");
            txtTheoDoi_TenNgThucHien.DataBindings.Add("text", BenhAnBN.LayHoTenYTaTuMaYTa(txtPhongBenh_MaYTa.Text), "Ten");
        }

        #endregion

        private void btnInBenhAn_Click(object sender, EventArgs e)
        {
            btnTroLai.Visible = true;
            btnInBenhAn.Visible = false;
            expandableSplitter1.Expanded = true;
            groupPanel4.Visible = false;

            //SqlConnection cnn;
            //string connectionString = null;
            //string sql = null;
            
            //connectionString = connectData.ChuoiKetNoi();
            //cnn = new SqlConnection(connectionString);
            //cnn.Open();

            //sql = "EXEC CHITIETDONTHUOC_TENTHUOC1 '" + txtDonThuoc_MaDonThuoc.Text.ToString() + "'";

            //SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);
            //QLBVDataSet ds = new QLBVDataSet();
            //dscmd.Fill(ds, "CHITIETDONTHUOC_TENTHUOC1");
            ////MessageBox.Show(ds.Tables[1].Rows.Count.ToString());
            //cnn.Close();

            //rpBenhAn rp = new rpBenhAn();
            //rp.Database.Tables["CHITIETDONTHUOC_TENTHUOC1"].SetDataSource(ds);
            
            //crystalReportViewer1.ReportSource = rp;
            //crystalReportViewer1.Refresh();  

            rpBenhAn rp = new rpBenhAn();
            ParameterValues a = new ParameterValues();
            ParameterDiscreteValue b = new ParameterDiscreteValue();

            b.Value = txtBenhNhan_HoBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HoBenhNhan"].ApplyCurrentValues(a);

            b.Value = txtBenhNhan_TenBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenBenhNhan"].ApplyCurrentValues(a);

            b.Value = txtBenhNhan_GioiTinhBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["GioiTinh"].ApplyCurrentValues(a);

            b.Value = dtPickerBenhNhan_NgaySinhBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgaySinh"].ApplyCurrentValues(a);

            b.Value = txtBenhNhan_NgheNghiep.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgheNghiep"].ApplyCurrentValues(a);

            b.Value = txtBenhNhan_TenDoiTuong.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["DoiTuong"].ApplyCurrentValues(a);

            b.Value = txtBenhNhan_DiaChiBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["DiaChi"].ApplyCurrentValues(a);

            b.Value = txtKBenh_CanNangBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["CanNang"].ApplyCurrentValues(a);

            b.Value = txtKBenh_NhomMauBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NhomMau"].ApplyCurrentValues(a);

            b.Value = txtKBenh_NhietDoBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NhietDo"].ApplyCurrentValues(a);

            b.Value = txtKBenh_MachBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["Mach"].ApplyCurrentValues(a);

            b.Value = txtKBenh_HuyetApBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HuyetAp"].ApplyCurrentValues(a);

            b.Value = txtKBenh_NhipThoBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NhipTho"].ApplyCurrentValues(a);

            b.Value = txtKBenh_YeuCauKham.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["YeuCauKham"].ApplyCurrentValues(a);

            b.Value = txtKBenh_TinhTrangHienTai.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TinhTrangHienTai"].ApplyCurrentValues(a);

            b.Value = txtKBenh_BenhSuBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["BenhSu"].ApplyCurrentValues(a);

            b.Value = txtKBenh_ChuanDoanSoBo.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["ChanDoanSoBo"].ApplyCurrentValues(a);


            b.Value = txtKBenh_ChuanDoanSauCung.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["ChanDoanSauCung"].ApplyCurrentValues(a);

            b.Value = txtKBenh_HuongDieuTri.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HuongDieuTri"].ApplyCurrentValues(a);

            b.Value = txtDonThuoc_MaDonThuoc.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["MaDonThuoc"].ApplyCurrentValues(a);

            b.Value = dtPickerDonThuoc_NgayTao.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgayTao"].ApplyCurrentValues(a);

            b.Value = dtPickerDonThuoc_GioTao.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["GioTao"].ApplyCurrentValues(a);

            b.Value = txtDonThuoc_HoBS.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HoBacSiDonThuoc"].ApplyCurrentValues(a);

            b.Value = txtDonThuoc_TenBS.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenBacSiDonThuoc"].ApplyCurrentValues(a);

            if (txtXetNghiem_MaPhongXN.Text.ToString() != "")
            {
                b.Value = txtXetNghiem_TenPhongXN.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["PhongXN"].ApplyCurrentValues(a);

                b.Value = txtXetNghiem_MaPhieuXN.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["PhieuXetNghiem"].ApplyCurrentValues(a);

                b.Value = txtXetNghiem_YeuCauXN.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["YeuCauXetNghiem"].ApplyCurrentValues(a);

                b.Value = txtXetNghiem_KetLuan.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["KetLuan"].ApplyCurrentValues(a);

                b.Value = txtXetNghiem_DeNghi.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["DeNghi"].ApplyCurrentValues(a);
            }
            else
            {
                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["PhongXN"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["PhieuXetNghiem"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["YeuCauXetNghiem"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["KetLuan"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["DeNghi"].ApplyCurrentValues(a);
            }

            if (txtXQuang_MaPhieuXQ.Text.ToString() != "")
            {
                b.Value = txtXQuang_TenPhongXQ.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["TenPhongXquang"].ApplyCurrentValues(a);

                b.Value = txtXQuang_MaPhieuXQ.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["MaPhieuChup"].ApplyCurrentValues(a);

                b.Value = txtXQuang_MoTaAnh.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["MoTaHinhAnh"].ApplyCurrentValues(a);

                b.Value = txtXQuang_KetLuan.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["KetLuanXquang"].ApplyCurrentValues(a);

                b.Value = txtXQuang_DeNghi.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["DeNghiXquang"].ApplyCurrentValues(a);
            }
            else
            {
                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["TenPhongXquang"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["MaPhieuChup"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["MoTaHinhAnh"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["KetLuanXquang"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["DeNghiXquang"].ApplyCurrentValues(a);
            }

            if (txtNhapVien_MaNV.Text.ToString() != "")
            {
                b.Value = dtPickerNhapVien_NgayNV.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["NgayNhapVien"].ApplyCurrentValues(a);

                b.Value = txtNhapVien_KetQuaCD.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["KetQuaChanDoan"].ApplyCurrentValues(a);

                b.Value = txtNhapVien_PPDieuTri.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["PPDieuTri"].ApplyCurrentValues(a);
            }
            else
            {
                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["NgayNhapVien"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["KetQuaChanDoan"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["PPDieuTri"].ApplyCurrentValues(a);
            }

            if (txtTheoDoi_MaTheoDoi.Text.ToString() != "")
            {
                b.Value = txtPhongBenh_TenPB.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["PhongBenh"].ApplyCurrentValues(a);

                b.Value = txtPhongBenh_MaGiuongBenh.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["GiuongBenh"].ApplyCurrentValues(a);

                b.Value = txtPhongBenh_HoBS.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["HoBSPhuTrach"].ApplyCurrentValues(a);

                b.Value = txtPhongBenh_TenBS.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["TenBSPhuTrach"].ApplyCurrentValues(a);

                b.Value = txtPhongBenh_HoYT.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["HoYTPhuTrach"].ApplyCurrentValues(a);

                b.Value = txtPhongBenh_HoYT.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["TenYTPhuTrach"].ApplyCurrentValues(a);

                b.Value = txtTheoDoi_NhanDinhTT.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["NhanDinhTinhTrang"].ApplyCurrentValues(a);

                b.Value = txtTheoDoi_KeHoachChamSoc.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["KeHoach"].ApplyCurrentValues(a);
            }
            else
            {
                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["PhongBenh"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["GiuongBenh"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["HoBSPhuTrach"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["TenBSPhuTrach"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["HoYTPhuTrach"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["TenYTPhuTrach"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["NhanDinhTinhTrang"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["KeHoach"].ApplyCurrentValues(a);
            }

            if (txtXuatVien_MaXV.Text.ToString() != "")
            {
                b.Value = dtPickerXuatVien_NgayRaVien.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["NgayXuatVien"].ApplyCurrentValues(a);

                b.Value = txtXuatVien_ChuanDoan.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["ChanDoan"].ApplyCurrentValues(a);

                b.Value = txtXuatVien_PPDieuTri.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["PPDieuTri1"].ApplyCurrentValues(a);
            }
            else
            {
                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["NgayXuatVien"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["ChanDoan"].ApplyCurrentValues(a);

                b.Value = "";
                a.Add(b);
                rp.DataDefinition.ParameterFields["PPDieuTri1"].ApplyCurrentValues(a);
            }


            crystalReportViewer1.ReportSource = rp;
        }

        private void btnTroLai_Click(object sender, EventArgs e)
        {
            btnInBenhAn.Visible = true;
            btnTroLai.Visible = false;
            expandableSplitter1.Expanded = false;
            groupPanel4.Visible = true;
        }


    }
}
