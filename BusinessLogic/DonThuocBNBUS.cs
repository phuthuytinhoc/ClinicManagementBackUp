using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using QuanLyBenhVien.DataAccess;
using System.Data;
using QuanLyBenhVien.BusinessObject;

namespace QuanLyBenhVien.BusinessLogic
{
    class DonThuocBNBUS
    {
        ConnectData connData = new ConnectData();


        //Lay ID tu dong tang cho Don Thuoc
        public string NextIDDonThuoc()
        {
            return Utilities.NextID(connData.GetLastID("DONTHUOC", "MaDonThuoc"), "DT");
        }

       //load danh sach nhom thuoc
        public DataTable DSNhomThuoc()
        {
            string sql = "Select DISTINCT NhomThuoc from THUOC";
            return connData.GetDataTable(sql);
        }


        //ham lay ho ten bac si tu ma bac si
        public DataTable LayHoTenBacSiTuMaBS(string MaBacSi)
        {
            string sql = "Select Ho, Ten from BACSI where MaBacSi = '" + MaBacSi + "'";
            return connData.GetDataTable(sql);
        }

        //Ham lay ho, ten, gioitinh, Nhom mau cua benh nhan
        public DataTable LayThongTinDonThuocBenhNhan(string MaBenhNhan)
        {
            string sql = "select Ho, Ten, GioiTinh, NgaySinh, NhomMau from BENHNHAN, PHIEUKHAM "
           + " where BENHNHAN.MaBenhNhan = PHIEUKHAM.MaBenhNhan AND PHIEUKHAM.MaBenhNhan = '" + MaBenhNhan + "'";

            return connData.GetDataTable(sql);
        }

        //ham lay ten thuoc tu nhom thuoc
        public DataTable LayTenThuocTuNhomThuoc(string cboText)
        {
            string sql = "Select TenThuoc from THUOC where NhomThuoc Like N'" + cboText + "'";
            return connData.GetDataTable(sql);
        }

        //ham lay ma thuoc tu ten thuoc
        public DataTable LayMaThuocTuTenThuoc(string TenThuoc)
        {
            string sql = "Select MaThuoc from THUOC where TenThuoc LIKE N'" + TenThuoc + "'";
            return connData.GetDataTable(sql);
        }

        //Luu thong tin chi tiet don thuoc
        public void LuuThongTinChiTietDonThuoc(DonThuoc dt)
        {
            string sql = string.Format("Insert into CHITIETDONTHUOC(STT,MaDonThuoc, MaThuoc, SoLuong, Ngay1, Ngay2, MoiLan1, MoiLan2, ThoiDiemSuDung) "
            + " values('{0}', '{1}', '{2}', '{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}')", "0", dt.MaDonThuoc, dt.MaThuoc, dt.SoLuong, dt.Ngay1, dt.Ngay2, dt.MoiLan1,
            dt.MoiLan2, dt.ThoiDiemSuDung);
            connData.ExcuteQuery(sql);
        }

        //luu thong tin don thuoc
        public bool LuuThongTinDonThuoc(DonThuoc dt)
        {
            string sql = string.Format("Insert into DONTHUOC(STT, MaDonThuoc, MaBenhNhan, MaBacSi, NgayTao, GioTao, LoiDan) values('{0}', '{1}', '{2}', '{3}', N'{4}', '{5}', N'{6}')",
                "0", dt.MaDonThuoc, dt.MaBenhNhan, dt.MaBacSi, dt.NgayTao.ToString("MM/dd/yyyy"), dt.GioTao.ToString("hh:mm:ss") ,dt.LoiDan);
            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public bool CapNhapDonThuoc(DonThuoc dt)
        {
            string sql = string.Format(" Update DONTHUOC set NgayTao = N'{0}', GioTao = '{1}', LoiDan = N'{2}', MaBacSi = '{3}' where MaDonThuoc = '{4}'",
                dt.NgayTao.ToString("MM/dd/yyyy"), dt.GioTao.ToString("hh:mm:ss"), dt.LoiDan, dt.MaBacSi, dt.MaDonThuoc);
            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public bool KiemTraTrungMaDonThuoc(string MaDonThuoc)
        {
            if (connData.KiemTra("DONTHUOC", "MaDonThuoc",MaDonThuoc))
                return true;
            return false;
        }

        public bool KiemTraMaBenhNhanDaCoTrongDonThuoc(string MaBenhNhan)
        {
            if (connData.KiemTra("DONTHUOC", "MaBenhNhan", MaBenhNhan))
                return true;
            return false;
        }
        public DataTable LayMaDonThuocCuaBenhNhan(string MaBenhNhan)
        {
           string sql = "Select MaDonThuoc from DONTHUOC where DONTHUOC.MaBenhNhan = '" + MaBenhNhan + "'";
           return connData.GetDataTable(sql);
        }

        //sua doi thong tin don thuoc
       // public bool SuaDoiThongTinDonThuoc

        //Ham xuat thong tin ra DS Thuoc
        public DataTable DSTenThuocDatagirdView(string MaBenhNhan)
        {
            string sql = "select * from DanhSachThuocDaChon('" + MaBenhNhan + "')";

            return connData.GetDataTable(sql);
        }

        //Ham kiem tra ma thuoc da them vao hay chua
        public bool KiemTraTrungMaThuoc(string MaThuoc)
        {
            if(connData.KiemTra("CHITIETDONTHUOC", "MaThuoc", MaThuoc))
                return true; //trung
            return false;//khong trung
        }
        //Xoa thuoc ra khoi danh sach thuoc
        public void XoaThuocKhoiDSThuoc(string MaThuocXoa)
        {
            string sql = "Delete from CHITIETDONTHUOC where MaThuoc = '" + MaThuocXoa + "'";
            connData.ExcuteQuery(sql);
        }

        public void TaoMoiDonThuocBN(DonThuoc dt1)
        {
            string sql = string.Format("Insert into DONTHUOC(STT, MaDonThuoc, MaBenhNhan) values('{0}', '{1}', '{2}')", "0", dt1.MaDonThuoc, dt1.MaBenhNhan);
            connData.ExcuteQuery(sql);
        }
        public void XoaDonThuocBN(string MaDonThuoc)
        {
            string sql = "Delete from DONTHUOC where MaDonThuoc = '" + MaDonThuoc + "'"
                + "Delete from CHITIETDONTHUOC where MaDonThuoc = '" + MaDonThuoc + "'";
            connData.ExcuteQuery(sql);
        }

        public bool KiemTraMaBenhNhanTrongDonThuoc(string mabenhnhan)
        {
            if (connData.KiemTra("DONTHUOC", "MaBenhNhan", mabenhnhan))
                return true;
            return false;
        }
        public DataTable LayLoiDanCuaBS(string MaDonThuoc)
        {
            string sql = " Select LoiDan from DONTHUOC where MaDonThuoc = '" + MaDonThuoc +"'";
            return connData.GetDataTable(sql);
        }

    }
}
