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
    class QuanLiBacSiBUS
    {
        ConnectData conn = new ConnectData();

        //Lay ID tu dong tang cho MaBacSi
        public string NextIDBacSi()
        {
            return Utilities.NextID(conn.GetLastID("BACSI", "MaBacSi"), "BS");
        }

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

        public DataTable DSHocVi()
        {
            string sql = "Select HocVan from HOCVAN where LoaiHV = 'HocVi'";
            return conn.GetDataTable(sql);
        }

        public DataTable DSHocHam()
        {
            string sql = "Select HocVan from HOCVAN where LoaiHV = 'HocHam'";
            return conn.GetDataTable(sql);
        }

        public DataTable DSBacSi()
        {
            string sql = "Select STT, MaBacSi, BACSI.MaKhoa, MaPhongKham, MaPhongBenh, TenKhoa, Ho, Ten, GioiTinh, NgaySinh, CMND, Email, SDT, DiaChi, NgayVaoLam, ChucVu, TrinhDo, HocHam, HocVi from BACSI, KHOA "
            + "where BACSI.MaKhoa=KHOA.MaKhoa";
            return conn.GetDataTable(sql);
        } 

        public DataTable DSMaKhoa()
        {
            string sql = "Select MaKhoa from KHOA";
            return conn.GetDataTable(sql);
        }


        public DataTable LayTenKhoaTuMaKhoa(string cboText)
        {
            string sql = "Select TenKhoa from KHOA where MaKhoa ='" + cboText + "'";
            return conn.GetDataTable(sql);
        }
        //Ham them bac si moi vao danh sach
        public bool ThemBacSi(BacSi bs)
        {
            string sql = string.Format(" exec sp_ThemBacSi '{0}','{1}', '{2}', '{3}', N'{4}', N'{5}', N'{6}', N'{7}', '{8}', '{9}', '{10}', N'{11}', N'{12}', N'{13}', N'{14}', N'{15}', N'{16}'   "
               , bs.MaBacSi, bs.MaPhongKham, bs.MaPhongBenh, bs.MaKhoa, bs.Ho, bs.Ten, bs.GioiTinh, bs.NgaySinh.ToString("MM/dd/yyyy"),
                bs.CMND, bs.Email, bs.SDT, bs.DiaChi, bs.NgayVaoLam.ToString("MM/dd/yyyy"), bs.ChucVu, bs.TrinhDo, bs.HocHam, bs.HocVi, bs.MaBacSi, bs.MaPhongKham);

            if (conn.ExcuteQuery(sql))
            {
                MessageBox.Show("Thêm Bác sĩ thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }


        //lay ten phong kham tu ma phong kham
        public DataTable LayTenPKTuMaPK(string cboText)
        {
            string sql = "Select TenPhong from PHONGKHAM where MaPhongKham = '" + cboText + "'";
            return conn.GetDataTable(sql);
        }

        //Ham Lay Dah sach phong kham, load nhung phong kham chua co bac si
        public DataTable DSPhongKhamBS()
        {
            string sql = "	Select MaPhongKham from PHONGKHAM where MaPhongKham = 'No' or MaPhongKham NOT IN(Select MaPhongKham from BACSI)";
            return conn.GetDataTable(sql);
        }

        //ham lay danh sach phong benh, load nhung phong benh chua co bac si
        public DataTable DSPhongBenhBS()
        {
            string sql = "	Select MaPhongBenh from PHONGBENH where MaPhongBenh = 'No' or MaPhongBenh NOT IN(Select MaPhongBenh from BACSI)";
            return conn.GetDataTable(sql);
        }

        //load tat ca phong kham
        public DataTable DSTatCaPhongKham()
        {
            string sql = "Select MaPhongKham from PHONGKHAM";
            return conn.GetDataTable(sql);
        }

        //load tat ca phong benh
        public DataTable DSTatCaPhongBenh()
        {
            string sql = "Select MaPhongBenh from PHONGBENH";
            return conn.GetDataTable(sql);
        }

        public DataTable LayTenPBTuMaPB(string cboText)
        {
            string sql = "Select TenPhong from PHONGBENH where MaPhongBenh = '" + cboText + "'";
            return conn.GetDataTable(sql);
        }

        //Kiem Tra có trùng lặp MaBacSi
        public bool KiemTraTrungMaBacSi(string MaBacSi)
        {
            if (conn.KiemTra("BACSI", "MaBacSi", MaBacSi))
                return true;
            return false;
        }

        //Sua doi thong tin bac si 
  /*    public bool SuaDoiBacSi(BacSi bs)
        {
            string sql = string.Format("Update PHONGKHAM set MaBacSi = NULL where MaBacSi = '{0}'  Update PHONGKHAM set MaBacSi = '{1}' where MaPhongKham = '{2}' "
            + " Update PHONGBENH set MaBacSi = NULL  where MaBacSi = '{3}'  Update PHONGBENH set MaBacSi = '{4}' where MaPhongKham = '{5}' "
            + "  exec sp_SuaDoiBacSi '{6}','{7} ', '{8}', '{9}', N'{10}', N'{11}', N'{12}', N'{13}', '{14}', '{15}', '{16}', N'{17}', N'{18}', N'{19}', N'{20}', N'{18}', N'{21}' ",
            bs.MaBacSi, bs.MaBacSi, bs.MaPhongKham, bs.MaBacSi, bs.MaBacSi, bs.MaPhongBenh, bs.MaBacSi,bs.MaPhongKham, bs.MaPhongBenh, bs.MaKhoa, bs.Ho, bs.Ten, bs.GioiTinh, bs.NgaySinh.ToString("MM/dd/yyyy"), bs.CMND,
            bs.Email, bs.SDT, bs.DiaChi, bs.NgayVaoLam.ToString("MM/dd/yyyy"), bs.ChucVu, bs.TrinhDo, bs.HocHam, bs.HocVi);

            if (conn.ExcuteQuery(sql))
            {
                MessageBox.Show("Sửa đổi thông tin bác sĩ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        } 
   * */

        //Xoa Bac Si ra khoi danh sach
        public bool XoaBacSi(string MaBacSi)
        {
            string sql = "Delete from BACSI where MaBacSi = '" + MaBacSi + "'  ";              
           
            if (conn.ExcuteQuery(sql))
            {
                MessageBox.Show("Xóa thành công bác sĩ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;         
        }       

        /*      public bool ThemBacSi(BacSi bs)
      {
          string sql = string.Format("Insert into BACSI(STT, MaBacSi, MaKhoa,  Ho, Ten, GioiTinh, NgaySinh, CMND, Email, SDT, DiaChi, NgayVaoLam, ChucVu, TrinhDo,"
              + "HocHam, HocVi) values('{0}','{1}', '{2}', N'{3}', N'{4}', N'{5}', N'{6}', '{7}', '{8}', '{9}', N'{10}', N'{11}', N'{12}', N'{13}', N'{14}', N'{15}')",
              "0",bs.MaBacSi, bs.MaKhoa, bs.Ho, bs.Ten, bs.GioiTinh, bs.NgaySinh.ToString("MM/dd/yyyy"), bs.CMND, bs.Email, bs.SDT, bs.DiaChi, bs.NgayVaoLam.ToString("MM/dd/yyyy"),
           bs.ChucVu, bs.TrinhDo, bs.HocHam, bs.HocVi);

          if (conn.ExcuteQuery(sql))
          {
              MessageBox.Show("Thêm Bác sĩ thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
              return true;
          }                
          else
              return false;
      }
      */
          public bool SuaDoiBacSi(BacSi bs)
      {
          string sql = string.Format("Update BACSI set Ho = N'{0}', Ten = N'{1}', NgaySinh = N'{2}', GioiTinh = N'{3}', CMND ='{4}',"
          + "Email = '{5}', SDT = '{6}', DiaChi = N'{7}', NgayVaoLam = N'{8}', MaKhoa = N'{9}', ChucVu = N'{10}', "
          + "TrinhDo = N'{11}', HocHam = N'{12}', HocVi = N'{13}', MaPhongKham = '{14}', MaPhongBenh = '{15}'  where MaBacSi = '{16}'", bs.Ho, bs.Ten, bs.NgaySinh.ToString("MM/dd/yyyy"), bs.GioiTinh, bs.CMND,
          bs.Email, bs.SDT, bs.DiaChi, bs.NgayVaoLam.ToString("MM/dd/yyyy"), bs.MaKhoa, bs.ChucVu, bs.TrinhDo, bs.HocHam, bs.HocVi,bs.MaPhongKham, bs.MaPhongBenh, bs.MaBacSi);

          if(conn.ExcuteQuery(sql))
          {
              MessageBox.Show("Sửa đổi thông tin bác sĩ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
              return true;
          }
          else
              return false;
      }    
  
    }
}
