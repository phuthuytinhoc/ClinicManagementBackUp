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
    class QuanLiYTaBUS
    {
        ConnectData conn = new ConnectData();

        //Lay ID tu dong tang cho MaYTa
        public string NextIDYTa()
        {
            return Utilities.NextID(conn.GetLastID("YTA", "MaYTa"), "YT");
        }       

        //Lay gioi tinh cho combobox
        public DataTable DSGioiTinh()
        {
            string sql = "Select * from GIOITINH";
            return conn.GetDataTable(sql);
        }

        public DataTable DSTrinhDo()
        {
            string sql = "Select HocVan from HOCVAN where LoaiHV = 'TrinhDo'";
            return conn.GetDataTable(sql);
        }

        public DataTable DSMaKhoa()
        {
            string sql = "Select MaKhoa from KHOA";
            return conn.GetDataTable(sql);
        }

        //Lay ten khoa tu ma khoa cho combobox
        public DataTable LayTenKhoaTuMaKhoa(string cboText)
        {
            string sql = "Select TenKhoa from KHOA where MaKhoa ='" + cboText + "'";
            return conn.GetDataTable(sql);
        }

        //lay danh sach Y Ta trong CSDL
        public DataTable DSYTa()
        {
            string sql = "Select STT, MaYTa, YTA.MaKhoa, MaPhongKham, MaPhongBenh, TenKhoa, Ho, Ten, GioiTinh, NgaySinh, CMND, Email, SDT,"
            + "DiaChi, NgayVaoLam, ChucVu, TrinhDo from YTA, KHOA where YTA.MaKhoa = KHOA.MaKhoa";
            return conn.GetDataTable(sql);
        }

        //ham Kiem tra trung lap ma y ta ?
        public bool KiemTraTrungMaYTa(string MaYTa)
        {
            if (conn.KiemTra("YTA", "MaYTa", MaYTa))
                return true;
            return false;
        }

        //Ham Them mot y ta moi
        public bool ThemYTa(YTa yt)
        {
            string sql = string.Format(" exec sp_ThemYTa '{0}', '{1}', '{2}', '{3}', N'{4}', N'{5}', N'{6}', N'{7}', '{8}', '{9}', '{10}', N'{11}', N'{12}', N'{13}', N'{14}'  " 
            ,yt.MaYTa, yt.MaPhongKham, yt.MaPhongBenh, yt.MaKhoa, yt.Ho, yt.Ten, yt.GioiTinh, yt.NgaySinh.ToString("MM/dd/yyyy"), yt.CMND, yt.Email, yt.SDT, yt.DiaChi, 
            yt.NgayVaoLam.ToString("MM/dd/yyyy"),yt.ChucVu, yt.TrinhDo);

            if (conn.ExcuteQuery(sql))
            {
                MessageBox.Show("Thêm Y tá mới thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }
            
        //Sua doi thong tin y ta
        public bool SuaDoiYTa(YTa yt)
        {
            string sql = string.Format(" exec sp_SuaDoiYTa '{0}','{1}', '{2}', '{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', N'{9}', N'{10}', N'{11}', N'{12}', N'{13}', N'{14}'   ",            
            yt.MaYTa, yt.MaPhongKham, yt.MaPhongBenh, yt.MaKhoa, yt.Ho, yt.Ten, yt.GioiTinh, yt.NgaySinh.ToString("MM/dd/yyyy"), yt.CMND, yt.Email, yt.SDT,
            yt.DiaChi, yt.NgayVaoLam.ToString("MM/dd/yyyy"), yt.ChucVu, yt.TrinhDo);

            if (conn.ExcuteQuery(sql))
            {
                MessageBox.Show("Sửa đổi thông tin Y tá thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }

        //xoa Y ta dc chon ra khoj danh sach
        public bool XoaYTa(string MaYTa)
        {
            string sql = "Delete from YTA where MaYTa = '" + MaYTa + "'  ";              

            if (conn.ExcuteQuery(sql))
            {
                MessageBox.Show("Xóa Y tá thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }

        //Ham Lay Dah sach phong kham, load nhung phong kham chua co y ta
        public DataTable DSPhongKhamYT()
        {
            string sql = "	Select MaPhongKham from PHONGKHAM where MaPhongKham = 'No' or MaPhongKham NOT IN(Select MaPhongKham from YTA)";
            return conn.GetDataTable(sql);
        }
        //ham lay danh sach phong bẹnh, oad nhung phong kham chua co y ta
        public DataTable DSPhongBenhYT()
        {
            string sql = "Select MaPhongBenh from PHONGBENH where MaPhongBenh = 'No' or MaPhongBenh NOT IN(Select MaPhongBenh from YTA)";
            return conn.GetDataTable(sql);
        }

        //load tat ca phong kham
        public DataTable DSTatCaPhongKham()
        {
            string sql = "Select MaPhongKham from PHONGKHAM";
            return conn.GetDataTable(sql);
        }

        public DataTable DSTatCaPhongBenh()
        {
            string sql = "Select MaPhongBenh from PHONGBENH";
            return conn.GetDataTable(sql);
        }

      


        /*  public bool SuaDoiYTa(YTa yt)
        {
            string sql = string.Format("Update YTA set Ho = N'{0}', Ten = N'{1}', NgaySinh = N'{2}', GioiTinh = N'{3}',DiaChi = N'{4}', CMND = '{5}', TrinhDo = N'{6}', "
            +" ChucVu = N'{7}', MaKhoa = '{8}', Email = '{9}', SDT = '{10}', NgayVaoLam = N'{11}' Where MaYTa = '{12}'", yt.Ho, yt.Ten, yt.NgaySinh.ToString("MM/dd/yyyy"),
            yt.GioiTinh, yt.DiaChi, yt.CMND, yt.TrinhDo, yt.ChucVu, yt.MaKhoa, yt.Email, yt.SDT, yt.NgayVaoLam.ToString("MM/dd/yyyy"), yt.MaYTa);

            if (conn.ExcuteQuery(sql))
            {
                MessageBox.Show("Sửa đổi thông tin Y tá thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }
        */
        /*   public bool ThemYTa(YTa yt)
   {
       string sql = string.Format("Insert into YTA(STT, MaYTa, Ho, Ten, NgaySinh, GioiTinh, DiaChi, CMND, TrinhDo,"
       + "ChucVu, MaKhoa, Email, SDT, NgayVaoLam) values('{0}', '{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', '{7}', N'{8}',"
       +"N'{9}', '{10}', '{11}', '{12}', N'{13}')", "0",yt.MaYTa, yt.Ho, yt.Ten, yt.NgaySinh.ToString("MM/dd/yyyy"), yt.GioiTinh, 
       yt.DiaChi, yt.CMND, yt.TrinhDo, yt.ChucVu, yt.MaKhoa, yt.Email, yt.SDT, yt.NgayVaoLam.ToString("MM/dd/yyyy"));

       if (conn.ExcuteQuery(sql))
       {
           MessageBox.Show("Thêm Y tá mới thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
           return true;
       }
       else
           return false;    
   }
   */

    }
}
