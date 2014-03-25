using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using QuanLyBenhVien.DataAccess;
using System.Data;
using QuanLyBenhVien.BusinessObject;

namespace QuanLyBenhVien.BusinessLogic
{
    class XetNghiemBenhNhanBUS
    {
        ConnectData connData = new ConnectData();

        //Next id cho ma xet nghiem
        public string NextIDXetNghiem()
        {
            return Utilities.NextID(connData.GetLastID("XETNGHIEM", "MaXetNghiem"), "XN");
        }

        //ham lay Danh sach benh nhan cho
        public DataTable DSBenhNhanChoXN()
        {
            string sql = "Select STT, MaBenhNhan,SDT, Ho, Ten, GioiTinh, NgaySinh, DiaChi from BENHNHANCHO";
            return connData.GetDataTable(sql);
        }
        //load danh sach ho ten ma ky thuat vien xet nghiem
        public DataTable LoadMaKyThuatVien()
        {
            string sql = "Select MaNhanVien, Ho, Ten from NHANVIEN where MaPhongBan = 'MPB-XN'";
            return connData.GetDataTable(sql);
        }

        //Load ten phong xet ngiem tu ma phong
        public DataTable LayTenPhongXNTuMaPhongXN(string MaPhongXN)
        {
            string sql = "Select TenPhongLamViec from PHONGLAMVIECNV where MaPhongLamViec = '" + MaPhongXN + "'";
            return connData.GetDataTable(sql);
        }

        //lay ho ten ktv tu ma ktv
        public DataTable LayHoTenKTVTuMaKTV(string MaKTV)
        {
            string sql = "Select Ho, Ten from NHANVIEN where MaNhanVien = '" + MaKTV + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayMaPhongXetNgiem()
        {
            string sql = "Select MaPhongLamViec from PHONGLAMVIECNV where MaPhongLamViec like 'MPB-XN%'";
            return connData.GetDataTable(sql);
        }

        //ham luu thong tin xet ngiem
        public bool LuuThongTinXetNgiem(XetNghiem xn)
        {
            string sql = string.Format("Insert into XETNGHIEM(STT, MaXetNghiem, MaBenhNhan, MaPhongXetNghiem, NgayXetNghiem,"
                + " YeuCauXetNghiem, KetQua, DeNghi) values ('{0}', '{1}', '{2}', '{3}', N'{4}', N'{5}', N'{6}', N'{7}')",
                "0", xn.MaXetNghiem, xn.MaBenhNhan ,xn.MaPhongXN, xn.NgayXN.ToString("MM/dd/yyyy"), xn.YeuCauXetNghiem, xn.KetQuaXetNghiem, xn.DeNghi);
            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        //ham sua doi thong tin xet ngiem
        public bool SuaDoiThongTinXetNgiem(XetNghiem xn)
        {
            string sql = string.Format("Update XETNGHIEM set MaPhongXetNghiem = '{0}', NgayXetNghiem = N'{1}', YeuCauXetNghiem = N'{2}', KetQua = N'{3}', DeNghi = N'{4}'"
                +"where MaXetNghiem = '{5}'", xn.MaPhongXN, xn.NgayXN.ToString("MM/dd/yyyy"), xn.YeuCauXetNghiem, xn.KetQuaXetNghiem, xn.DeNghi, xn.MaXetNghiem);

            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        //Ham Delete phieu xet nghiem
        public bool XoaPhieuXetNghiemBN(string MaPhieuXn)
        {
            string sql = "Delete from XETNGHIEM where MaXetNghiem = '" + MaPhieuXn + "'";
            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        //ham kiem tra co trung ma xet nghiem ko
        public bool KiemTraTrungMaXetNghiem(string maxetnghiem)
        {
            if (connData.KiemTra("XETNGHIEM", "MaXetNghiem", maxetnghiem))
                return true;
            return false;
        }

        //Kiem tra ma benh nhan da co trong XETNGHIEM hay chua
        public bool KiemTraTrungMaBenhNhanXN(string mabenhnhan)
        {
            if (connData.KiemTra("XETNGHIEM", "MaBenhNhan", mabenhnhan))
                return true;
            return false;
        }

        //Lay thong tin xet nghiem cho benh nhan
        public DataTable LayThongTinXetNghiemBN(string MaXetNghiem)
        {
            string sql = "Select * from XETNGHIEM where MaXetNghiem = '" + MaXetNghiem + "'";
            return connData.GetDataTable(sql);
        }

        //Lay ma xet nghiem tu ma benh nhan
        public DataTable LayMaXetNghiemTuMaBenhNhan(string MaBenhNhan)
        {
            string sql = "Select MaXetNghiem from XETNGHIEM where MaBenhNhan = '" +MaBenhNhan+ "'";
            return connData.GetDataTable(sql);
        }

        //lay ma ktv tu ma phong xet nghiem
        public DataTable LayMaKtvTuMaPhongXN(string maphongxn)
        {
            string sql = "Select MaNhanVien from NHANVIEN where MaPhongLamViec = '" + maphongxn + "'";
            return connData.GetDataTable(sql);
        }


    }
}
