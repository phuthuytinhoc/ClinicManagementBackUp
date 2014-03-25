using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using QuanLyBenhVien.DataAccess;
using System.Data;
using QuanLyBenhVien.BusinessObject;

namespace QuanLyBenhVien.BusinessLogic
{
    class ChuyenVienBNBUS
    {
        ConnectData connData = new ConnectData();

        public string NextIDChuyenVien()
        {
            return Utilities.NextID(connData.GetLastID("CHUYENVIEN", "MaChuyenVien"), "CV");
        }

        //lay thong tin benh nhhan
        public DataTable LayThongTinBNChuyenVien(string mabenhnhan)
        {
            string sql = "Select Ho, Ten, GioiTinh, NgheNghiep, DiaChi, BENHNHAN.MaBenhAn, CMND, NgaySinh, SDT, KetQuaChuanDoan,"
            + " NgayNhapVien, PhuongPhapDieuTri, MaDoiTuong, ChuanDoanSauCung from BENHNHAN, NHAPVIEN, PHIEUKHAM"
            + " where BENHNHAN.MaBenhNhan = NHAPVIEN.MaBenhNhan and PHIEUKHAM.MaBenhNhan = BENHNHAN.MaBenhNhan and BENHNHAN.MaBenhNhan = '" + mabenhnhan + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayTenDoiTuong(string madoituong)
        {
            string sql = "Select TenDoiTuong from DOITUONG where MaDoiTuong = '" + madoituong + "'";
            return connData.GetDataTable(sql);
        }

        //Lay cac xet nghiem da lam cua benh nhan
        public DataTable LayXetNghiemCuaBN(string MaBenhNhan)
        {
            string sql = "Select YeuCauXetNghiem from XETNGHIEM where MaBenhNhan = '" + MaBenhNhan + "'";
            return connData.GetDataTable(sql);
        }

        //lay danh sach thuoc tu ma benh nhan
        public DataTable DanhSachThuocBN(string mabenhnhan)
        {
            string sql = "Select TenThuoc from THUOC where THUOC.MaThuoc IN (Select MaThuoc from CHITIETDONTHUOC, DONTHUOC" 
		                      + " where DONTHUOC.MaDonThuoc = CHITIETDONTHUOC.MaDonThuoc and DONTHUOC.MaBenhNhan = '" + mabenhnhan +"')";
            return connData.GetDataTable(sql);												   
        }

        //Luu thong tin benh nhan chuyen vien
        public bool LuuThongTinChuyenVien(ChuyenVien cv)
        {
            string sql = string.Format("Insert into CHUYENVIEN(STT, MaChuyenVien, MaBenhNhan, MaBenhAn, NgayChuyenVien, GioChuyenVien, DauHieuLamSang, "
                + " LyDoChuyenVien, TinhTrangKhiChuyen, PhuongTienDiChuyen) values('{0}', '{1}', '{2}', '{3}', N'{4}', '{5}', N'{6}', N'{7}', N'{8}', N'{9}')",
                "0", cv.MaChuyenVien, cv.MaBenhNhan, cv.MaBenhAn, cv.NgayChuyenVien.ToString("MM/dd/yyyy"), cv.GioChuyenVien.ToString("hh:mm:ss"), 
                cv.DauHieuLamSang, cv.LyDoChuyenVien, cv.TinhTrangKhiChuyen, cv.PhuongTienDiChuyen);

            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public bool SuaDoiThongTinChuyenVien(ChuyenVien cv)
        {
            string sql = string.Format("Update CHUYENVIEN set NgayChuyenVien = N'{0}', GioChuyenVien = '{1}', DauHieuLamSang = N'{2}', LyDoChuyenVien = '{3}',"
                +" TinhTrangKhiChuyen = N'{4}', PhuongTienDiChuyen = N'{5}' where MaChuyenVien = '{6}'", cv.NgayChuyenVien.ToString("MM/dd/yyyy"), 
                cv.GioChuyenVien.ToString("hh:mm:ss"), cv.DauHieuLamSang, cv.LyDoChuyenVien, cv.TinhTrangKhiChuyen, cv.PhuongTienDiChuyen, cv.MaChuyenVien);

            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public bool KiemTraMaChuyenVien(string MaChuyenVien)
        {
            if (connData.KiemTra("CHUYENVIEN", "MaChuyenVien", MaChuyenVien))
                return true;
            return false;
        }

        public bool KiemTraMaBenhNhan(string MaBenhNhan)
        {
            if (connData.KiemTra("CHUYENVIEN", "MaBenhNhan", MaBenhNhan))
                return true;
            return false;
        }

        public DataTable LayMaChuyenVienBNNeuCo(string MaBenhNhan)
        {
            string sql = "Select MaChuyenVien from CHUYENVIEN where MaBenhNhan = '" + MaBenhNhan + "'";
            return connData.GetDataTable(sql);
        }

        //newu benh nhan da co ma chuyen vien roi thi ko can tao moi
        public DataTable LayThongTinChuyenVienBN(string MaChuyenVien)
        {
            string sql = "Select NgayChuyenVien, GioChuyenVien, DauHieuLamSang, LyDoChuyenVien,"
                + " TinhTrangKhiChuyen, PhuongTienDiChuyen from CHUYENVIEN where MaChuyenVien = '" + MaChuyenVien + "'";
            return connData.GetDataTable(sql);
        }


    }
}
