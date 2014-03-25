using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using QuanLyBenhVien.DataAccess;
using System.Data;
using QuanLyBenhVien.BusinessObject;

namespace QuanLyBenhVien.BusinessLogic
{
    class UserDangNhapBUS
    {
        ConnectData connData = new ConnectData();

        //Lay Du lieu username , password
        public User LayThongTinUser(string Username)
        {
            string sql = "Select Username, Password, Ho, Ten, a.MaNhom, TenNhom, MaNguoiDung from USERS a, USERS_NHOMNGUOIDUNG b"
                + " Where a.MaNhom = b.MaNhom AND Username = '" + Username + "'";

            DataTable dtUser = connData.GetDataTable(sql);
            User user = new User();
            if (dtUser.Rows.Count > 0)
            {
                DataRow drUser = dtUser.Rows[0];
                user.Username = drUser["Username"].ToString();
                user.Password = drUser["Password"].ToString();
                user.Ho = drUser["Ho"].ToString();
                user.Ten = drUser["Ten"].ToString();
                user.MaNhom = drUser["MaNhom"].ToString();
                user.TenNhom = drUser["TenNhom"].ToString();
                user.MaNguoiDung = drUser["MaNguoiDung"].ToString();
            }
            else
            {
                user.Username = "";
                user.Password = "";
                user.Ho = "";
                user.Ten = "";
                user.MaNhom = "";
                user.TenNhom = "";
                user.MaNguoiDung = "";
            }
            return user;
        }

        //ham doi mat khau
        public bool DoiMatKhau(string matkhau)
        {
            matkhau = Utilities.MaHoaMD5(matkhau);
            string sql = "Update USERS set Password = '" + matkhau + "' where Username = '" + Utilities.user.Username + "'";
            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }


    }
}
