using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using QuanLyBenhVien.DataAccess;
using System.Data;
using QuanLyBenhVien.BusinessObject;

namespace QuanLyBenhVien.BusinessLogic
{
    class XuatVienBNBUS
    {
        ConnectData connData = new ConnectData();

        public string NextIDXuatVien()
        {
            return Utilities.NextID(connData.GetLastID("XUATVIEN", "MaXuatVien"), "XV");
        }

        public DataTable LayTenDoiTuong(string madoituong)
        {
            string sql = "Select TenDoiTuong from DOITUONG where MaDoiTuong = '" + madoituong + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LoadMaDoiTuong()
        {
            string sql = "Select MaDoiTuong from DOITUONG";
            return connData.GetDataTable(sql);
        }

        public DataTable LayDSMaDoiTuong()
        {
            string sql = "Select MaDoiTuong from DOITUONG";
            return connData.GetDataTable(sql);
        }

        //Lay danh sach benh nhan da nhap vien
        public DataTable LayDSBenhNhanNhapVien()
        {
            string sql = " Select MaBenhNhan from NHAPVIEN  group by MaBenhNhan";
            return connData.GetDataTable(sql);
        }

        //LayThong Tin Benh nhan
        public DataTable LayThongTinBNXuatVien(string mabenhnhan)
        {
            string sql = "Select Ho, Ten, GioiTinh, NgheNghiep, DiaChi, BENHNHAN.MaBenhAn, CMND, NgaySinh, SDT, KetQuaChuanDoan,"
            + " NgayNhapVien, PhuongPhapDieuTri, KetQuaChuanDoan, MaDoiTuong, ChuanDoanSauCung from BENHNHAN, NHAPVIEN, PHIEUKHAM"
            + " where BENHNHAN.MaBenhNhan = NHAPVIEN.MaBenhNhan and PHIEUKHAM.MaBenhNhan = BENHNHAN.MaBenhNhan and BENHNHAN.MaBenhNhan = '" + mabenhnhan + "'";
            return connData.GetDataTable(sql);
        }

        public bool LuuThongTinXuatVienBN(XuatVien xv)
        {
            string sql = string.Format("Insert into XUATVIEN(STT, MaXuatVien, MaBenhAn, NgayXuatVien, LoiDanBS) values('{0}', '{1}', '{2}', N'{3}', N'{4}')",
                "0", xv.MaXuatVien, xv.MaBenhAn, xv.NgayXuatVien.ToString("MM/dd/yyyy"), xv.LoiDanBS);

            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public bool SuaDoiThongTinXuatVienBN(XuatVien xv)
        {
            string sql = string.Format("Update XUATVIEN set NgayXuatVien = N'{0}', LoiDanBS = N'{1}' where MaXuatVien = '{2}'", xv.NgayXuatVien.ToString("MM/dd/yyyy"),
                xv.LoiDanBS, xv.MaXuatVien);

            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }
        public DataTable LayGT_MaDTCuaBenhNhan(string Mabenhnhan)
        {
            string sql = "Select GioiTinh, MaDoiTuong from BENHNHAN where MaBenhNhan = '" + Mabenhnhan + "'";
            return connData.GetDataTable(sql);
        }

        public bool KiemTraMaXuatVien(string MaXuatVien)
        {
            if (connData.KiemTra("XUATVIEN", "MaXuatVien", MaXuatVien))
                return true;
            return false;
        }

    }
}
