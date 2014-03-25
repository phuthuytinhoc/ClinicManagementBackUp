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
    class QuanLiBenhNhanBUS
    {
        ConnectData conn = new ConnectData();

        //Lay ID tu dong tang cho MaBenhNhan
        public string NextIDMaBenhNhan()
        {
            return Utilities.NextID(conn.GetLastID("BENHNHAN", "MaBenhNhan"), "BN");
        }

        public DataTable DSGioiTinh()
        {
            string sql = "Select * from GIOITINH";
            return conn.GetDataTable(sql);
        }

        //Lay danh sach doi tuong
        public DataTable DSDoiTuong()
        {
            string sql = "Select MaDoiTuong from DOITUONG";
            return conn.GetDataTable(sql);
        }

        //Lay ten doi tuong tu ma doi tuong
        public DataTable LayTenDoiTuongTuMaDT(string cboText)
        {
            string sql = "Select TenDoiTuong from DOITUONG where MaDoiTuong = '" + cboText + "'";
            return conn.GetDataTable(sql);
        }

        //lay danh sach benh nhan
        public DataTable LayDanhSachBenhNhan()
        {
            string sql = "	Select BENHNHAN.STT, BENHNHAN.MaBenhNhan,MaBenhAn, BENHNHAN.MaDoiTuong,  TenDoiTuong, Ho, Ten, GioiTinh, CMND, NgheNghiep, NgaySinh, "
            + " DiaChi, SDT, YeuCauKham from BENHNHAN, DOITUONG where BENHNHAN.MaDoiTuong = DOITUONG.MaDoiTuong ";
            return conn.GetDataTable(sql);
        }

        //ham Kiem tra trung lap ma benh nhan ?
        public bool KiemTraTrungMaBenhNhan(string MaBenhNhan)
        {
            if (conn.KiemTra("BENHNHAN", "MaBenhNhan", MaBenhNhan))
                return true;
            return false;
        }
   
        //ham them benh nhan moi vao CSDL 
        public bool ThemBenhNhan(BenhNhan bn)
        {
            //them thong tin vao table BENHNHAN 
            string sql = string.Format("Insert into BENHNHAN(STT, MaBenhNhan, MaDoiTuong, Ho, Ten, GioiTinh, CMND, NgheNghiep, NgaySinh,"
            + "DiaChi, SDT, YeuCauKham, MaBenhAn, NgayKham, MaPhongKham) values('{0}', '{1}', '{2}', N'{3}', N'{4}', N'{5}', '{6}', N'{7}', N'{8}', N'{9}', '{10}', N'{11}', '{12}', N'{13}', '{14}' )",
            "0", bn.MaBenhNhan, bn.MaDoiTuong, bn.Ho, bn.Ten, bn.GioiTinh, bn.CMND, bn.NgheNghiep, bn.NgaySinh.ToString("MM/dd/yyyy"), bn.DiaChi, bn.SDT, bn.YeuCauKham, bn.MaBenhAn,
            bn.NgayKham.ToString("MM/dd/yyyy"), bn.MaPhongKham);

            if (conn.ExcuteQuery(sql))
            {
                MessageBox.Show("Đã thêm bệnh nhân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }

        // them vao danh sach benh nhan cho
        //ham them benh nhan moi vao CSDL 
        public bool ThemBenhNhanCho(BenhNhanCho bnc)
        {
            //them thong tin vao table BENHNHAN 
            string sql = string.Format("Insert into BENHNHANCHO(STT, MaBenhNhan, MaDoiTuong, Ho, Ten, GioiTinh, CMND, NgheNghiep, NgaySinh,"
            + "DiaChi, SDT, YeuCauKham, MaBenhAn, NgayKham, MaPhongKham) values('{0}', '{1}', '{2}', N'{3}', N'{4}', N'{5}', '{6}', N'{7}', N'{8}', N'{9}', '{10}', N'{11}', '{12}', N'{13}', '{14}' )",
            "0", bnc.MaBenhNhan, bnc.MaDoiTuong, bnc.Ho, bnc.Ten, bnc.GioiTinh, bnc.CMND, bnc.NgheNghiep, bnc.NgaySinh.ToString("MM/dd/yyyy"), bnc.DiaChi, bnc.SDT, bnc.YeuCauKham, bnc.MaBenhAn,
            bnc.NgayKham.ToString("MM/dd/yyyy"), bnc.MaPhongKham);

            if (conn.ExcuteQuery(sql))
            {
                MessageBox.Show("Đã thêm bệnh nhân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }

        //Sua doi thong tin benh nhan
        public bool SuaDoiBenhNhan(BenhNhan bn)
        {
            string sql = string.Format("Update BENHNHAN set Ho = N'{0}', Ten = N'{1}', NgaySinh = N'{2}', GioiTinh = N'{3}', DiaChi = N'{4}',"
            +" CMND = '{5}', MaDoiTuong = '{6}', NgheNghiep = N'{7}', SDT = '{8}', YeuCauKham = '{9}', NgayKham = N'{10}', MaPhongKham = '{11}'"
            + " where MaBenhNhan = '{12}'",bn.Ho, bn.Ten, 
            bn.NgaySinh.ToString("MM/dd/yyyy"), bn.GioiTinh, bn.DiaChi, bn.CMND, bn.MaDoiTuong, bn.NgheNghiep, bn.SDT, bn.YeuCauKham, 
            bn.NgayKham.ToString("MM/dd/yyyy"), bn.MaPhongKham, bn.MaBenhNhan);

            if (conn.ExcuteQuery(sql))
            {
                MessageBox.Show("Sửa đổi thông tin Bệnh nhân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }  
        //Sua Doi Thong tin benh nhan cho
        public void SuaDoiBenhNhanCho(BenhNhanCho bnc)
        {
            string sql = string.Format("Update BENHNHANCHO set Ho = N'{0}', Ten = N'{1}', NgaySinh = N'{2}', GioiTinh = N'{3}', DiaChi = N'{4}',"
            + " CMND = '{5}', MaDoiTuong = '{6}', NgheNghiep = N'{7}', SDT = '{8}', YeuCauKham = '{9}', NgayKham = N'{10}', MaPhongKham = '{11}'"
            + " where MaBenhNhan = '{12}'", bnc.Ho, bnc.Ten, bnc.NgaySinh.ToString("MM/dd/yyyy"), bnc.GioiTinh, bnc.DiaChi, bnc.CMND, bnc.MaDoiTuong,
            bnc.NgheNghiep, bnc.SDT, bnc.YeuCauKham, bnc.NgayKham.ToString("MM/dd/yyyy"), bnc.MaPhongKham, bnc.MaBenhNhan);

            conn.ExcuteQuery(sql);            
        }  

        //xoa thong tin benh nhan
        public bool XoaBenhNhan(string MaBenhNhan)
        {
            //string sql = "Delete from BENHNHAN where MaBenhNhan = '" + MaBenhNhan + "'";

            string sql = "exec XOABENHNHAN '"+ MaBenhNhan +"'";
            if (conn.ExcuteQuery(sql))
            {
                MessageBox.Show("Xóa bệnh nhân thành công!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return true;
            }
            else
            return false;
        }


    }
}
