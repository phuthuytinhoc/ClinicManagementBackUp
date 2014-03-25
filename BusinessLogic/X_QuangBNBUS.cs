using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using QuanLyBenhVien.DataAccess;
using System.Data;
using QuanLyBenhVien.BusinessObject;

namespace QuanLyBenhVien.BusinessLogic
{
    class X_QuangBNBUS
    {
        ConnectData connData = new ConnectData();

        //Next id cho ma Phieu chup
        public string NextIDXQuang()
        {
            return Utilities.NextID(connData.GetLastID("X_QUANG", "MaPhieuChup"), "XQ");
        }

        public DataTable laygioiTinhBenhNhan(string mabenhnhan)
        {
            string sql = "Select GioiTinh from BENHNHAN where MaBenhNhan = '" + mabenhnhan+ "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayMaPhongXQuang()
        {
            string sql = "Select MaPhongLamViec from PHONGLAMVIECNV where MaPhongLamViec like 'MPB-XQ%'";
            return connData.GetDataTable(sql);
        }

        //load danh sach ho ten ma ky thuat vien x quang
        public DataTable LoadMaKyThuatVienXQ()
        {
            string sql = "Select MaNhanVien, Ho, Ten from NHANVIEN where MaPhongBan = 'MPB-XQ'";
            return connData.GetDataTable(sql);
        }

        //ham lay Danh sach benh nhan cho
        public DataTable DSBenhNhanChoXQ()
        {
            string sql = "Select STT, MaBenhNhan,SDT, Ho, Ten, GioiTinh, NgaySinh, DiaChi, YeuCauKham from BENHNHANCHO";
            return connData.GetDataTable(sql);
        }

        //Ham luu thong tin X Quang benh nhan
        public bool LuuThongTinXQuangBN(X_Quang xq)
        {
            string sql = string.Format("Insert into X_QUANG (STT, MaPhieuChup, MaBenhNhan, MaNhanVien, MaPhongXQ, NgayChup, KyThuat,"
            +" KyThuatChup, MoTaAnhChup, ChuanDoan, KetLuan, DeNghi) Values('{0}', '{1}', '{2}', '{3}', '{4}', N'{5}', N'{6}',"
            +" N'{7}', N'{8}', N'{9}', N'{10}', N'{11}')", "0", xq.MaPhieuChup, xq.MaBenhNhan, xq.MaNhanVien, xq.MaPhongXQ, xq.NgayChup.ToString("MM/dd/yyyy"),
            xq.KyThuat, xq.KyThuatChup, xq.MoTaAnhChup, xq.ChuanDoan, xq.KetLuan, xq.DeNghi);
            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public bool SuaDoiThongTinXQuangBN(X_Quang xq)
        {
            string sql = string.Format("Update X_QUANG set MaPhongXQ = '{0}', MaNhanVien = '{1}', KyThuat = N'{2}', KyThuatChup = N'{3}', MoTaAnhChup = N'{4}',"
            + " ChuanDoan = N'{5}', KetLuan = N'{6}', DeNghi = N'{7}' where MaPhieuChup = '{8}'", xq.MaPhongXQ, xq.MaNhanVien, xq.KyThuat, xq.KyThuatChup,
            xq.MoTaAnhChup, xq.ChuanDoan, xq.KetLuan, xq.DeNghi, xq.MaPhieuChup);

            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        //Xoa phieu chup cua benh nhan
        public bool XoaPhieuChupXQBenhNhan(string maphieuchup)
        {
            string sql = "Delete from X_QUANG where MaPhieuChup = '" + maphieuchup + "'";
            if(connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        //Kiem tra trung ma phieu chup
        public bool KiemTraTrungMaPhieuChupXQ(string maphiechup)
        {
            if (connData.KiemTra("X_QUANG", "MaPhieuChup", maphiechup))
                return true;
            return false;
        }

        public bool KiemTraTrungMaBNXQuang(string mabenhnhan)
        {
            if (connData.KiemTra("X_QUANG", "MaBenhNhan", mabenhnhan))
                return true;
            return false;
        }

        //lay ten phong xquang tu ma phong xquang
        public DataTable LayTenPhongXQtuMaPhongXQ(string maphongxq)
        {
            string sql = "Select TenPhongLamViec from PHONGLAMVIECNV where MaPhongLamViec = '" + maphongxq + "'";
            return connData.GetDataTable(sql);
        }
        //lay ho ten nhan vien tu ma nhan vien
        public DataTable LayHoTenKTVTuMaKTV(string maktv)
        {
            string sql = "Select Ho, Ten from NHANVIEN where MaNhanVien = '" + maktv + "'";
            return connData.GetDataTable(sql);
        }
        //LayThong tin x quang cua benh nhan da co tu ma benh nhan
        public DataTable LayThongTinBNDaChupXQuang(string maphieuchup)
        {
            string sql = "Select MaNhanVien, MaPhongXQ, KyThuat, KyThuatChup, ChuanDoan, MoTaAnhChup,"
            + " ChuanDoan, KetLuan, DeNghi from X_QUANG where MaPhieuChup = '" + maphieuchup + "'";
            return connData.GetDataTable(sql);
        }

        public DataTable LayMaPhieuChupTuMaBN(string mabenhnhan)
        {
            string sql = "Select MaPhieuChup from X_QUANG where MaBenhNhan = '" + mabenhnhan + "'";
            return connData.GetDataTable(sql);
        }

        //kiem tra ma benh nhan da co trong XQuang chua, neu chua thi them ma moi neu co thi giu nguyen
        public bool KiemTraMaBNTrongXQuang(string mabenhnhan)
        {
            if (connData.KiemTra("X_QUANG", "MaBenhNhan", mabenhnhan))
                return true;
            return false;
        }
    }
}
