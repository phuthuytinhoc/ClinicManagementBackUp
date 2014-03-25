using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using QuanLyBenhVien.DataAccess;

namespace QuanLyBenhVien.BusinessLogic
{
    class PhongKhamBUS
    {
        ConnectData conData = new ConnectData();

        //Lay danh sach phong kham
        public DataTable DSPhongKham()
        {
            string sql = "Select MaPhongKham from PHONGKHAM where MaPhongKham <> 'No' ";
            return conData.GetDataTable(sql);
        }
        public DataTable LayTenPhongTuMaPhong(string cboText)
        {
            string sql = "Select TenPhong from PHONGKHAM where MaPhongKham = '" + cboText + "'";
            return conData.GetDataTable(sql);
        }

        public DataTable LayNgayKhamMaPhongKham(string MaBenhNhan)
        {
            string sql = "select MaPhongKham, NgayKham from BENHNHAN where MaBenhNhan = '" + MaBenhNhan + "'";
            return conData.GetDataTable(sql);
        }
       
    }
}
