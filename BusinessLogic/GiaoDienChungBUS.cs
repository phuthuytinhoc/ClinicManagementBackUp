using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLyBenhVien.DataAccess; // tang nay chi giao tiep voi DataAccess
using QuanLyBenhVien.BusinessLogic;
using System.IO;
using System.Drawing;// tang nay chi giao tiep voi BusinessLogic

namespace QuanLyBenhVien.BusinessLogic
{
    class GiaoDienChungBUS
    {

        ConnectData connData = new ConnectData();
        HinhAnhBUS myPic = new HinhAnhBUS();


        public DataTable LayDanhSachBenhNhan()
        {
            string sql = "Select BENHNHAN.STT, BENHNHAN.MaBenhNhan, MaBenhAn, TenDoiTuong, Ho, Ten, GioiTinh, CMND, NgheNghiep, NgaySinh, DiaChi, SDT, YeuCauKham "
            + "from BENHNHAN, DOITUONG where BENHNHAN.MaDoiTuong = DOITUONG.MaDoiTuong";

            return connData.GetDataTable(sql);
        }

        public DataTable LayDanhSachBenhNhanCho()
        {
            string sql = "Select a.STT, a.MaBenhNhan, a.MaBenhAn, b.TenDoiTuong, a.Ho, a.Ten, a.GioiTinh, a.CMND, a.NgheNghiep, a.NgaySinh,a.DiaChi, a.SDT, a.YeuCauKham"
            + " from BENHNHANCHO a, DOITUONG b, BENHNHAN c where a.MaDoiTuong = b.MaDoiTuong and a.MaBenhNhan = c.MaBenhNhan";

            return connData.GetDataTable(sql);
        }
        public string DuongDan = Application.StartupPath;
        //ham bacukup du lieu 
        public void BackupDuLieu()
        {
            string sql = "BACKUP DATABASE QLBV TO DISK = '" + DuongDan + @"\Database\QLBV.Bak' WITH FORMAT, MEDIANAME = 'Z_SQLServerBackups', NAME = 'Full Backup of AdventureWorks' ";
            connData.ExcuteQuery(sql);
        }


        //ham restore du lieu
        public void RestoreDuLieu()
        {
            string sql = " restore database QLSV from disk ='" + DuongDan + @"\Database\QLBV.Bak' ";
            connData.ExcuteQuery(sql);
        }

        //Ham lay ma phong kham tu ma benh nhan de CHuyen BenhNhan
        public DataTable LayMaPhongKhamDeChuyenBenhNhan(string MaBenhNhan)
        {
            string sql = "Select MaPhongKham from BENHNHAN where MaBenhNhan = '" + MaBenhNhan + "'";
            return connData.GetDataTable(sql);
        }

        public bool KiemTraMaPhongKhamCuaBacSi(string MaPhongKham)
        {
            if(connData.KiemTra("BACSI", "MaPhongKham", MaPhongKham))
                return true;
            return false;
        }

 

        public bool KiemTraPhongKhamCuaBacSi(string MaPhongKham, string MaBacSi)
        {
            string sql = "Select MaPhongKham, MaBacSi from BACSI where MaPhongKham = '" + MaPhongKham + "' AND MaBacSi = '" + MaBacSi + "'";
            connData.GetDataTable(sql);

            DataTable dt = new DataTable();
            if (dt.Rows.Count == 1)
                return true;
            return false;

        }



    }
}
