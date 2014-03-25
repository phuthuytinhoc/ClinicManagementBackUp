using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using QuanLyBenhVien.DataAccess;
using QuanLyBenhVien.BusinessObject;

namespace QuanLyBenhVien.BusinessLogic
{
    class ThemTaiKhoanNguoiDungBUS
    {
        ConnectData conData = new ConnectData();


        #region  Bac Si

        //Load ten nhom tu ma nhom
        public DataTable LoadTenNhomTuMaNhom(string cboText)
        {
            string sql = "Select TenNhom from USERS_NHOMNGUOIDUNG where MaNhom = '" + cboText + "'";
            return conData.GetDataTable(sql);
        }

        //load toan bo danh sach bac si
        public DataTable LoadTatCaMaBacSi()
        {
            string sql = "Select MaBacSi from BACSI";
            return conData.GetDataTable(sql);
        }

        //load combobox ma bac si, nhung bac si nao chua co tai khoan se load len
        public DataTable LoadBacSiChuaCoTK()
        {
            string sql = "Select MaBacSi from BACSI where MaBacSi"
            + " NOT IN (Select MaBacSi from BACSI a, USERS b where a.MaBacSi = b.MaNguoiDung )";
            return conData.GetDataTable(sql);
        }

        //lay du lieu cua bac si
        public DataTable LayDuLieuBacSiTheoMaBacSi(string cboText)
        {
            string sql = "Select MaBacSi, Ho, Ten, NgaySinh, ChucVu from BACSI where MaBacSi = '" + cboText + "'";
            return conData.GetDataTable(sql);
        }

        //Load du lieu cho combobox
        public DataTable LoadDatagridDSBacSi()
        {
            string sql = "Select a.MaNhom, TenNhom, MaNguoiDung, Ho, Ten, Username, Password from USERS a, USERS_NHOMNGUOIDUNG b"
            + " where a.MaNhom = b.MaNhom AND MaNguoiDung LiKE 'BS%' ";
            return conData.GetDataTable(sql);
        }

        //load ma nhom cho combobox
        public DataTable LoadMaNhomChoBacSi()
        {
            string sql = "Select MaNhom from USERS_NHOMNGUOIDUNG a where a.MaNhom = 'MNAD' or a.MaNhom = 'MNTK' or a.MaNhom = 'MNPK' or a.MaNhom = 'MNBS'";
            return conData.GetDataTable(sql);
        }

        public bool ThemTaiKhoanBacSi(ThemTaiKhoanNguoiDung TTK)
        {
            TTK.PasswordBS = Utilities.MaHoaMD5(TTK.PasswordBS);
            string sql = string.Format("Insert into USERS (STT, MaNhom, Username, Password, Ho, Ten, MaNguoiDung)"
            + "values('{0}', '{1}', '{2}', '{3}', N'{4}', N'{5}', '{6}')","0", TTK.MaNhomBS, TTK.UsernameBS,
            TTK.PasswordBS, TTK.HoBacSi, TTK.TenBacSi, TTK.MaBacSi);

            if (conData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public bool SuaDoiTaiKhoanBacSi(ThemTaiKhoanNguoiDung ttk)
        {
            ttk.PasswordBS = Utilities.MaHoaMD5(ttk.PasswordBS);
            string sql = string.Format("Update USERS set Username = '{0}', Password = '{1}', Ho = N'{2}', Ten = N'{3}',"
            + " MaNhom = '{4}' where MaNguoiDung = '{5}'", ttk.UsernameBS, ttk.PasswordBS, ttk.HoBacSi, ttk.TenBacSi, 
            ttk.MaNhomBS, ttk.MaBacSi);
            if (conData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public bool KiemTraTrungMaNguoiDung(string MaNguoiDung)
        {
            if (conData.KiemTra("USERS", "MaNguoiDung", MaNguoiDung))
                return true;
            return false;
        }
    
        //Xoa nguoi dung ra khoi danh sach
        public bool XoaTaiKhoanBacSi(string MaBacSi)
        {
            string sql = "Delete from USERS where Username = '" + MaBacSi + "'";
            if (conData.ExcuteQuery(sql))
                return true;
            return false;
        }

        #endregion 


        #region Y Ta

        //Load du lieu cho combobox
        public DataTable LoadDatagridDSYTa()
        {
            string sql = "Select a.MaNhom, TenNhom, MaNguoiDung, Ho, Ten, Username, Password from USERS a, USERS_NHOMNGUOIDUNG b"
            + " where a.MaNhom = b.MaNhom AND MaNguoiDung LiKE 'YT%' ";
            return conData.GetDataTable(sql);
        }

        public DataTable LoadTatCaMaYTa()
        {
            string sql = "Select MaYTa from YTA";
            return conData.GetDataTable(sql);
        }

        public DataTable LoadMaNhomChoYTa()
        {
            string sql = "Select MaNhom from USERS_NHOMNGUOIDUNG a where a.MaNhom = 'MNYT'";
            return conData.GetDataTable(sql);
        }

        public bool ThemTaiKhoanYTa(ThemTaiKhoanNguoiDung TTK)
        {
            TTK.PasswordYT = Utilities.MaHoaMD5(TTK.PasswordYT);
            string sql = string.Format("Insert into USERS (STT, MaNhom, Username, Password, Ho, Ten, MaNguoiDung)"
            + "values('{0}', '{1}', '{2}', '{3}', N'{4}', N'{5}', '{6}')", "0", TTK.MaNhomYT, TTK.UsernameYT,
            TTK.PasswordYT, TTK.HoYTa, TTK.TenYTa, TTK.MaYTa);

            if (conData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public bool SuaDoiTaiKhoanYTa(ThemTaiKhoanNguoiDung ttk)
        {
            ttk.PasswordYT = Utilities.MaHoaMD5(ttk.PasswordYT);
            string sql = string.Format("Update USERS set Username = '{0}', Password = '{1}', Ho = N'{2}', Ten = N'{3}',"
            + " MaNhom = '{4}' where MaNguoiDung = '{5}'", ttk.UsernameYT, ttk.PasswordYT, ttk.HoYTa, ttk.TenYTa,
            ttk.MaNhomYT, ttk.MaYTa);
            if (conData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public bool XoaTaiKhoanYTa(string MaYTa)
        {
            string sql = "Delete from USERS where Username = '" + MaYTa + "'";
            if (conData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public DataTable LoadYTaChuaCoTK()
        {
            string sql = "Select MaYTa from YTA where MaYTa"
            + " NOT IN (Select MaYTa from YTA a, USERS b where a.MaYTa = b.MaNguoiDung )";
            return conData.GetDataTable(sql);
        }

        public DataTable LayDuLieuBacSiTheoMaYTa(string cboText)
        {
            string sql = "Select MaYTa , Ho, Ten, NgaySinh, ChucVu from YTA where MaYTa = '" + cboText + "'";
            return conData.GetDataTable(sql);
        }

        #endregion


        #region Nhan Vien

        //Load du lieu cho combobox
        public DataTable LoadDatagridDSNhanVien()
        {
            string sql = "Select a.MaNhom, TenNhom, MaNguoiDung, Ho, Ten, Username, Password from USERS a, USERS_NHOMNGUOIDUNG b"
            + " where a.MaNhom = b.MaNhom AND MaNguoiDung LiKE 'NV%' ";
            return conData.GetDataTable(sql);
        }

        public DataTable LoadTatCaMaNhanVien()
        {
            string sql = "Select MaNhanVien from NHANVIEN";
            return conData.GetDataTable(sql);
        }

        public DataTable LoadMaNhomChoNhanVien()
        {
            string sql = "Select MaNhom from USERS_NHOMNGUOIDUNG a where a.MaNhom = 'MNNV' or a.MaNhom = 'MNTP' or a.MaNhom = 'MNPP'";
            return conData.GetDataTable(sql);
        }

        public bool ThemTaiKhoanNhanVien(ThemTaiKhoanNguoiDung TTK)
        {
            TTK.PasswordNV = Utilities.MaHoaMD5(TTK.PasswordNV);
            string sql = string.Format("Insert into USERS (STT, MaNhom, Username, Password, Ho, Ten, MaNguoiDung)"
            + "values('{0}', '{1}', '{2}', '{3}', N'{4}', N'{5}', '{6}')", "0", TTK.MaNhomNV, TTK.UsernameNV,
            TTK.PasswordNV, TTK.HoNhanVien, TTK.TenNhanVien, TTK.MaNhanVien);

            if (conData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public bool SuaDoiTaiKhoanNhanVien(ThemTaiKhoanNguoiDung ttk)
        {
            ttk.PasswordNV = Utilities.MaHoaMD5(ttk.PasswordNV);
            string sql = string.Format("Update USERS set Username = '{0}', Password = '{1}', Ho = N'{2}', Ten = N'{3}',"
            + " MaNhom = '{4}' where MaNguoiDung = '{5}'", ttk.UsernameNV, ttk.PasswordNV, ttk.HoNhanVien, ttk.TenNhanVien,
            ttk.MaNhomNV, ttk.MaNhanVien);
            if (conData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public bool XoaTaiKhoanNhanVien(string MaNhanVien)
        {
            string sql = "Delete from USERS where Username = '" + MaNhanVien + "'";
            if (conData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public DataTable LoadNhanVienChuaCoTK()
        {
            string sql = "Select MaNhanVien from NHANVIEN where MaNhanVien"
            + " NOT IN (Select MaNhanVien from NHANVIEN a, USERS b where a.MaNhanVien = b.MaNguoiDung )";
            return conData.GetDataTable(sql);
        }

        public DataTable LayDuLieuBacSiTheoMaNhanVien(string cboText)
        {
            string sql = "Select MaNhanVien , Ho, Ten, NgaySinh, ChucVu from NHANVIEN where MaNhanVien = '" + cboText + "'";
            return conData.GetDataTable(sql);
        }

        #endregion

    }
}
