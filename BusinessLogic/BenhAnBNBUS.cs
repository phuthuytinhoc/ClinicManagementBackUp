using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using QuanLyBenhVien.DataAccess;
using System.Data;
using QuanLyBenhVien.BusinessObject;
using System.Windows.Forms;

namespace QuanLyBenhVien.BusinessLogic
{
    class BenhAnBNBUS
    {
        ConnectData connData = new ConnectData();

        public string NextIDBenhAn()
        {
            return Utilities.NextID(connData.GetLastID("BENHAN", "MaBenhAn"), "BA");
        }

        //Them STT, Ma Benh An, Ma Benh nhan vao csdl BENHAN
        public void ThemBenhAnBN(BenhAn ba)
        {
            string sql = string.Format("Insert into BENHAN(STT, MaBenhAn, MaBenhNhan) values('{0}', '{1}', '{2}')", "0", ba.MaBenhAn, ba.MaBenhNhan);
            connData.ExcuteQuery(sql);
        }

        public DataTable LayThongTinBenhNhanBenhAn(string MaBenhNhan)
        {
            string sql = "Select MaBenhAn, Ho, Ten, GioiTinh, NgaySinh, CMND, SDT, NgheNghiep, MaDoiTuong, DiaChi from BENHNHAN where MaBenhNhan = '" + MaBenhNhan + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayThongTinPhongKhamBenhAn(string MaBenhNhan)
        {
            string sql = "Select CanNang, NhomMau, Mach, HuyetAp, NhipTho, NhietDo, YeuCauKham, TinhTrangHienTai, BenhSu, ChuanDoanSoBo, ChuanDoanSauCung, HuongDieuTri"
                + " from PHIEUKHAM where MaBenhNhan = '" + MaBenhNhan + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayThongTinDonThuoc(string MaBenhNhan)
        {
            string sql = "Select MaDonThuoc, MaBacSi, NgayTao, GioTao, MaBacSi, LoiDan from DONTHUOC"
            + " where MaBenhNhan = '" + MaBenhNhan + "'";
            return connData.GetDataTable(sql);
        }
        public DataTable LayThongTinXetNghiem(string MaBenhNhan)
        {
            string sql = "Select MaXetNghiem, MaPhongXetNghiem, NgayXetNghiem, YeuCauXetNghiem, KetQua, DeNghi from XETNGHIEM"
                + " where MaBenhNhan = '" + MaBenhNhan + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayThongTinXQuang(string MaBenhNhan)
        {
            string sql = "Select MaPhieuChup, MaPhongXQ, KyThuat, KyThuatChup, NgayChup ,ChuanDoan, MoTaAnhChup, KetLuan, DeNghi  "
                + " from X_QUANG where MaBenhNhan = '" + MaBenhNhan + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayThongTinPhongBenh(string MaBenhNhan)
        {
            string sql = "Select MaPhongBenh, MaKhoa, MaGiuongBenh from NHAPVIEN where MaBenhNhan = '" + MaBenhNhan + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayLayTenPhongBenh(string maphongbenh)
        {
            string sql = "Select TenPhong from PHONGBENH where MaPhongBenh = '" + maphongbenh + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayMacBacSiPhongBenh(string MaPhongBenh)
        {
            string sql = " Select MaBacSi from BACSI where MaPhongBenh ='" + MaPhongBenh + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayMaYTaPhongBenh(string MaPhongBenh)
        {
            string sql = " Select MaYTa from YTA where MaPhongBenh ='" + MaPhongBenh + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayMaTheoDoiBN(string MaBenhNhan)
        {
            string sql = "Select  MaTheoDoi from THEODOI where MaBenhNhan = '" + MaBenhNhan + "'";
            return connData.GetDataTable(sql);
        }
        public DataTable LayThongTinTheoDoiTheoNgayTheoDoi(string NgayTheoDoi)
        {
            string sql = "Select Mach, NhietDo, CanNang, NhipTho, HuyetAp, DienBien, XuLiChamSoc, ThuocVatTu, NhanDinhTinhTrang "
                + " KeHoachChamSoc from CHITIETTHEODOI where NgayTheoDoi = '" + NgayTheoDoi + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayThongTinNhapVien(string MaBenhNhan)
        {
            string sql = "Select MaNhapVien, NgayNhapVien, KetQuaChuanDoan, PhuongPhapDieuTri from NHAPVIEN where MaBenhNhan = '" + MaBenhNhan + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayThongTinXuatVien(string MaBenhAn)
        {
            string sql = "Select MaXuatVien , NgayNhapVien, NgayXuatVien, ChuanDoan, PhuongPhapDieuTri, LoiDanBS "
                + " from XUATVIEN where MaBenhAn = '" + MaBenhAn + "'";
            return connData.GetDataTable(sql);
        }
        public DataTable LayThongTinChuyenVien(string maBenhNhan)
        {
            string sql = "Select MaBenhAn, NgayChuyenVien, GioChuyenVien, DauHieuLamSang, LyDoChuyenVien, TinhTrangKhiChuyen,"
                + " PhuongTienDiChuyen from CHUYENVIEN where MaBenhNhan = '" + maBenhNhan + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayTenDoiTuongTuMaDoiTuong(string maDoituong)
        {
            string sql = "Select TenDoiTuong from DOITUONG where MaDoiTuong = '" + maDoituong + "'";
            return connData.GetDataTable(sql);
        }
        public DataTable LayHoTenBacSiTuMaBacSi(string mabacsi)
        {
            string sql = "Select Ho, Ten from BACSI where MaBacSi = '" + mabacsi + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayHoTenYTaTuMaYTa(string mayta)
        {
            string sql = "Select Ho, Ten from YTA where MaYTa = '" + mayta + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayTenPhongXQuang(string MaPhongXQ)
        {
            string sql = "Select TenPhongLamViec from PHONGLAMVIECNV where MaPhongLamViec = '" + MaPhongXQ + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayMaHoTenNhanVienTuMaPhongXQXN(string maPhongLamviec)
        {
            string sql = "Select MaNhanVien, Ho, Ten from NHANVIEN where MaPhongLamViec = '" + maPhongLamviec + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayTenKhoaTuMaKhoa(string MaKhoa)
        {
            string sql = "Select TenKhoa from KHOA where MaKhoa = '" + MaKhoa + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayYeuCauXetNghiemTuMaBenhNhan(string Mabenhnhan)
        {
            string sql = "Select YeuCauXetNghiem from XETNGHIEM where MaBenhNhan = '" + Mabenhnhan+ "'";
            return connData.GetDataTable(sql);
        }

    }
}
