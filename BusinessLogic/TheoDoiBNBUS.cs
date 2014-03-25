using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using QuanLyBenhVien.DataAccess;
using System.Data;
using QuanLyBenhVien.BusinessObject;

namespace QuanLyBenhVien.BusinessLogic
{
    class TheoDoiBNBUS
    {
        ConnectData connData = new ConnectData();

        public string NextIDTheoDoi()
        {
            return Utilities.NextID(connData.GetLastID("THEODOI", "MaTheoDoi"), "TD");
        }

        public DataTable LayThongTinBNTheoDoi(string mabenhnhan)
        {
            string sql = "Select  Ho, Ten, BENHNHAN.MaBenhAn, CMND, NgheNghiep, DiaChi, MaDoiTuong, NgaySinh, GioiTinh, ChuanDoanSauCung"
                 + " from BENHNHAN, PHIEUKHAM where BENHNHAN.MaBenhNhan = PHIEUKHAM.MaBenhNhan and BENHNHAN.MaBenhNhan = '" + mabenhnhan + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayThongTinBNNhapVien(string Mabenhnhan)
        {
            string sql = "Select MaNhapVien, MaKhoa, MaPhongBenh, MaGiuongBenh, NgayNhapVien, KetQuaChuanDoan,"
            + " PhuongPhapDieuTri from NHAPVIEN where MaBenhNhan = '" + Mabenhnhan + "'";
            return connData.GetDataTable(sql);
        }
        public DataTable LayTenDoiTuong(string madoituong)
        {
            string sql = "Select TenDoiTuong from DOITUONG where MaDoiTuong = '" + madoituong + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayTenPhongBenhTuMaPB(string maphongbenh)
        {
            string sql = "Select TenPhong from PHONGBENH where MaPhongBenh = '" + maphongbenh + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayHoTenBacSiTuMS(string MaBacSi)
        {
            string sql = "Select Ho,Ten from BACSI where MaBacSi = '" + MaBacSi + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayHoTenYTaTuYT(string mayta)
        {
            string sql = "Select Ho,Ten from YTA where MaYTa = '" + mayta + "'";
            return connData.GetDataTable(sql);
        }
        
        //LayTen khoa tu ma khoa
        public DataTable LayTenKhoaTuMaKhoa(string MaKhoa)
        {
            string sql = "Select TenKhoa from KHOA where MaKhoa = '" + MaKhoa + "'";
            return connData.GetDataTable(sql);
        }

        //Lay MaBacSi tu MaPhongBenh
        public DataTable LayMaBacSiTuMaPB(string maphongbenh)
        {
            string sql = "Select MaBacSi from BACSI where MaPhongBenh = '" + maphongbenh + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayMaYTaTuMaPB(string maphongbenh)
        {
            string sql = "Select MayTa from YTA Where MaPhongBenh = '" + maphongbenh + "'";
            return connData.GetDataTable(sql);
        }

        public bool KiemTraMaBenhNhanTrongTHEODOI(string mabenhnhan)
        {
            if (connData.KiemTra("THEODOI", "MaBenhNhan", mabenhnhan))
                return true;
            return false;
        }

        public bool KiemTraNgayTheoDoiTrongCTTD(string ngaytheodoi)
        {
            if (connData.KiemTra("CHITIETTHEODOI", "NgayTheoDoi", ngaytheodoi))
                return true;
            return false;
        }


        //tao moi ma theo doi va luu thong tin theo doi benh nha
        public bool LuuThongTinTheoDoiBN(TheoDoi td)
        {
            string sql = string.Format("exec sp_ThemThongTinTheoDoiBN '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' ",
                td.MaTheoDoi, td.MaBenhAn, td.MaBenhNhan, td.MaKhoa, td.MaBacSi, td.MaYTa, td.MaNhapVien, td.MaPhongBenh, td.MaGiuongBenh);

            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }
        //them chi tiet theo doi cho benh nha
        public bool ThemThongTinChiTietTheoDoi(TheoDoi td)
        {
            string sql = string.Format("exec sp_ThemThongTinCHITIETTHEODOI '{0}', N'{1}', '{2}', '{3}', '{4}', '{5}', '{6}', N'{7}', N'{8}', N'{9}', N'{10}', N'{11}' ",
                td.MaTheoDoi, td.NgayTheoDoi.ToString("MM/dd/yyyy"), td.Mach, td.NhietDo, td.NhipTho, td.HuyetAp, td.CanNang, td.DienBien, td.XuLiChamSoc, td.ThuocVatTu,
                td.NhanDinhTinhTrang, td.KeHoachChamSoc);

            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public bool SuaDoiThongTInCHiTietTheoDoi(TheoDoi td)
        {
            string sql = string.Format(" exec sp_SuaDoiCHITIETTHEODOI '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', N'{6}', N'{7}', N'{8}', N'{9}', N'{10}' ",
                td.NgayTheoDoi.ToString("MM/dd/yyyy"), td.Mach, td.NhietDo, td.NhipTho, td.HuyetAp, td.CanNang, td.DienBien, td.XuLiChamSoc, td.ThuocVatTu,
                td.NhanDinhTinhTrang, td.KeHoachChamSoc);

            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public DataTable ChiTietDonThuocTuNgayTheoDoi(string ngaytheodoi)
        {
            string sql = "Select Mach, NhietDo, NhipTho, HuyetAp, CanNang, DienBien, XuLiChamSoc, ThuocVatTu, NhanDinhTinhTrang, KeHoachChamSoc"
                + " from CHITIETTHEODOI where NgayTheoDoi = '" + ngaytheodoi + "'";

            return connData.GetDataTable(sql);
        }

        public DataTable LayMaTheoDoiBN(string mabenhnhan)
        {
            string sql = "Select MaTheoDoi from THEODOI where MaBenhNhan ='" + mabenhnhan + "'";
            return connData.GetDataTable(sql);
        }

        public bool XoaTheoDoiBN(string MaTheoDoi)
        {
            string sql = "Delete from CHITIETTHEODOI where MaTheoDoi = '" + MaTheoDoi + "'"
                       + " Delete from THEODOI where MaTheoDoi = '" + MaTheoDoi + "'";
            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

    }
}
