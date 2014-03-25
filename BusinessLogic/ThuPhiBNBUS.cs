using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using QuanLyBenhVien.DataAccess;
using System.Data;
using QuanLyBenhVien.BusinessObject;

namespace QuanLyBenhVien.BusinessLogic
{
    class ThuPhiBNBUS
    {
        ConnectData connData = new ConnectData();

        public string NextIDThuPhi()
        {
            return Utilities.NextID(connData.GetLastID("HOADON", "MaHoaDon"), "HD");
        }

        public DataTable LayTenDoiTuong(string MaDoiTuong)
        {
            string sql = "Select TenDoiTuong from DOITUONG where MaDoiTuong = '" + MaDoiTuong + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayThongTinBenhNhan(string mabenhnhan)
        {
            string sql = "Select Ho, Ten, GioiTinh, NgaySinh, CMND, SDT, NgheNghiep, DiaChi, MaDoiTuong, MaBenhAn "
            + " from BENHNHAN where MaBenhNhan = '" + mabenhnhan+ "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LoadMaDichVu()
        {
            string sql = "Select MaDichVu from DICHVU";
            return connData.GetDataTable(sql);
        }

        public DataTable LayTenDichVuDonGia(string madichvu)
        {
            string sql = "Select TenDichVu, DonGia from DICHVU where MaDichVu = '" + madichvu + "'";
            return connData.GetDataTable(sql);
        }

        public bool LuuThongTinHoaDon(ThuPhi tp)
        {
            string sql = string.Format("Insert Into HOADON(STT, MaHoaDon, MaNhanVien, MaBenhAn, NgayLap, ThanhTien, MaDichVu)"
                + " Values('{0}', '{1}', '{2}', '{3}', N'{4}', '{5}', '{6}')", "0", tp.MaHoaDon, tp.MaNhanVien, tp.MaBenhAn,
                tp.Ngaylap.ToString("MM/dd/yyyy"), tp.ThanhTien, tp.MaDichVu);

            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public bool SuaDoiThongTinHoaDon(ThuPhi tp)
        {
            string sql = string.Format("Update HOADON set MaDichVu = '{0}', ThanhTien = '{1}', NgayLap = '{2}' " 
                + " where MaHoaDon = '{3}'", tp.MaDichVu, tp.ThanhTien, tp.Ngaylap.ToString("MM/dd/yyyy"), tp.MaHoaDon);

            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public bool KiemTraTrungMaHoaDon(string MaHoaDon)
        {
            if (connData.KiemTra("HOADON", "MaHoaDon", MaHoaDon))
                return true;
            return false;
        }
    }
}
