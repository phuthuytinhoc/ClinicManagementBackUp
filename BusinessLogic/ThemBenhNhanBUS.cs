using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using QuanLyBenhVien.BusinessLogic;
using QuanLyBenhVien.DataAccess;
using QuanLyBenhVien.BusinessObject;


namespace QuanLyBenhVien.BusinessLogic
{
    class ThemBenhNhanBUS
    {


        ConnectData connData = new ConnectData();
      
        public DataTable DSGioiTinh()
        {
            string sql = "Select * from GIOITINH";
                return connData.GetDataTable(sql);
        }

        public string NextIDBenhNhan()
        {
            return Utilities.NextID(connData.GetLastID("BENHNHAN", "MaBenhNhan"), "BN");
        }

        //lay danh sach ma doi tuong 
        public DataTable DSDoiTuong()
        {
            string sql = "Select MaDoiTuong from DOITUONG";
            return connData.GetDataTable(sql);
        }

        //Lay ten doi tuong tu ma doi tuong
        public DataTable LayTenDoiTuongTuMaDT(string cboText)
        {
            string sql = "Select TenDoiTuong from DOITUONG where MaDoiTuong = '" + cboText + "'";
            return connData.GetDataTable(sql);
        }



        //ham them benh nhan moi vao CSDL 
        public bool ThemBenhNhan(BenhNhan bn)
        {
            //them thong tin vao table BENHNHAN 
            string sql = string.Format("Insert into BENHNHAN(STT, MaBenhNhan, MaDoiTuong, MaPhongKham, MaBacSi, Ho, Ten, GioiTinh, CMND, NgheNghiep, NgaySinh,"
            + "DiaChi, SDT, YeuCauKham, GioKham, NgayKham) values('{0}', '{1}', '{2}', '{3}', '{4}', N'{5}', N'{6}', N'{7}', '{8}', N'{9}', N'{10}', N'{11}', '{12}', N'{13}', '{14}', N'{15}')",
            "0", bn.MaBenhNhan,bn.MaDoiTuong, bn.MaPhongKham, bn.MaBacSi, bn.Ho, bn.Ten, bn.GioiTinh, bn.CMND, bn.NgheNghiep, bn.NgaySinh.ToString("MM/dd/yyyy"),
            bn.DiaChi, bn.SDT, bn.YeuCauKham, bn.GioKham.ToString("hh:mm:ss"), bn.NgayKham.ToString("MM/dd/yyyy"));

            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        //Ham Them Benh Nhan Moi vao danh sach benh nhan cho
        public bool ThemBenhNhanCho(BenhNhanCho bnc)
        {
            string sql = string.Format("Insert into BENHNHANCHO(STT, MaBenhNhan,  MaDoiTuong,  MaPhongKham, MaBacSi,  Ho, Ten, GioiTinh, CMND, NgheNghiep, NgaySinh,"
          + "DiaChi, SDT, YeuCauKham, GioKham, NgayKham) values('{0}', '{1}', '{2}', '{3}', '{4}', N'{5}', N'{6}', N'{7}', '{8}', N'{9}', N'{10}', N'{11}', '{12}', N'{13}', '{14}', N'{15}')",
          "0", bnc.MaBenhNhan, bnc.MaDoiTuong, bnc.MaPhongKham, bnc.MaBacSi, bnc.Ho, bnc.Ten, bnc.GioiTinh, bnc.CMND, bnc.NgheNghiep, bnc.NgaySinh.ToString("MM/dd/yyyy"),
          bnc.DiaChi, bnc.SDT, bnc.YeuCauKham, bnc.GioKham.ToString("hh:mm:ss"), bnc.NgayKham.ToString("MM/dd/yyyy"));

            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }
     


        //Kiem tra trung ma benh nhan
        public bool KiemTraTrungMaBenhNhan(string MaBenhNhan)
        {
            if (connData.KiemTra("BENHNHAN", "MaBenhNhan", MaBenhNhan))
                return true;
            return false;
        }

        //ham lay ma bac si tu ma phong kham
        public DataTable LayMaBSTuMaPhongKham(string cboText)
        {
            string sql = "Select MaBacSi, Ho, Ten from BACSI where MaPhongKham = '" + cboText + "'";
            return connData.GetDataTable(sql);
        }


    }
}
