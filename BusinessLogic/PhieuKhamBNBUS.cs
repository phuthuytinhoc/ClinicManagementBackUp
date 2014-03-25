using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using QuanLyBenhVien.DataAccess;
using QuanLyBenhVien.BusinessObject;
using System.Data;

namespace QuanLyBenhVien.BusinessLogic
{
    class PhieuKhamBNBUS
    {
        ConnectData connData = new ConnectData();



        //Lay ID ke tip cho Phieu Kham
        public string NextIDPhieuKham()
        {
            return Utilities.NextID(connData.GetLastID("PHIEUKHAM", "MaPhieuKham"), "PKB");
        }


        //Them Vao STT, MaPhieuKham, MaPhongKham, MaBenhNhan
        public void ThemPhieuKhamBN(PhieuKham pk)
        {
            string sql = string.Format("Insert into PHIEUKHAM(STT, MaPhieuKham, MaPhongKham, MaBenhNhan) values('{0}', '{1}', '{2}', '{3}' ) ",
                "0", pk.MaPhieuKham, pk.MaPhongKham, pk.MaBenhNhan);

            connData.ExcuteQuery(sql);
        }

        //Load danh sach phong benh cung ho ten bac si - Kham benh
        public DataTable DSPhongKham()
        {
            string sql = "Select MaPhongKham from PHONGKHAM where MaPhongKham <> 'No'";
            return connData.GetDataTable(sql);
        }

        //lay danh sach benh nhan theo Ma Phong Kham
        public DataTable DSBenhNhanTheoMaPhongKham(string cboText)
        {
            string sql = "Select MaBenhNhan, Ho, Ten, GioiTinh, NgaySinh, DiaChi, SDT, YeuCauKham from BENHNHANCHO where MaPhongKham = '" + cboText + "'";
            return connData.GetDataTable(sql);
        }

        //lay danh sach ten phong tu ma phong kham
        public DataTable LayTenPhongKhamTuMaPhongKham(string cboText)
        {
            string sql = "Select TenPhong from PHONGKHAM where MaPhongKham = '" + cboText + "'";
            return connData.GetDataTable(sql);
        }

        //lay danh sach ten bac si tu phong kham nhap vao
        public DataTable LayHoTenBacSiTuMaPhongKham(string cboText)
        {
            string sql = "Select DIStinct BACSI.MaBacSi,Ho, Ten from BACSI, PHONGKHAM where BACSI.MaPhongKham = '" + cboText + "'";
            return connData.GetDataTable(sql);            
        }

        //Lay Ma Phieu Kham tu ma benh nhan
        public DataTable LayMaPhieuKhamTuMaBNKhamBenh(string MaBenhNhan)
        {
            string sql = " Select MaPhieuKham from PHIEUKHAM where MaBenhNhan = '" + MaBenhNhan + "'";
            return connData.GetDataTable(sql);
        }

        //ham lay ngay kham cho benh nhan cho
        public DataTable LayNgayKhamTuMaBNKhamBenh(string MaBenhNhan)
        {
            string sql = "Select NgayKham, YeuCauKham from BENHNHANCHO where MaBenhNhan = '" + MaBenhNhan + "'";
            return connData.GetDataTable(sql);
        }

        //ham them thong tin cho phieu kham
        public bool ThemThongTinPhieuKham(PhieuKham pk)
        {
            string sql = string.Format(" Update PHIEUKHAM set  CanNang = '{0}', NhomMau = '{1}', Mach = '{2}', HuyetAp = '{3}', NhietDo = '{4}', NhipTho = '{5}', LyDoKham = N'{6}', "
            + " TinhTrangHienTai = N'{7}', BenhSu = N'{8}', NgayKham = N'{9}', ChuanDoanSoBo = N'{10}', YeuCauThem = N'{11}', ChuanDoanSauCung = N'{12}', HuongDieuTri = N'{13}', YeuCauKham = N'{14}' "
            + " where MaBenhNhan = '{15}'", pk.CanNang, pk.NhomMau, pk.Mach, pk.HuyetAp, pk.NhietDo, pk.NhipTho, pk.LyDoKham, pk.TinhTrangHienTai, pk.BenhSu, pk.NgayKham.ToString("MM/dd/yyyy"),
            pk.ChuanDoanSoDo, pk.YeuCauThem, pk.ChuanDoanSauCung, pk.HuongDieuTri,pk.YeuCauThem, pk.MaBenhNhan);

            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        //ham lay thong tin kham benh cua benh nhan
        public DataTable LayThongTinKhamBenhTuMaBenhNhan(string MaBenhNhan)
        {
            string sql = "Select CanNang, NhomMau, Mach, HuyetAp, NhietDo, NhipTho, LyDoKham, TinhTrangHienTai, BenhSu, NgayKham, ChuanDoanSoBo, YeuCauThem, ChuanDoanSauCung, HuongDieuTri"
            + " from PHIEUKHAM where MaBenhNhan = '" + MaBenhNhan + "'";

            return connData.GetDataTable(sql);
        }
        //ham xoa danh benh nhan cho ra khoi danh sach kham benh neu ko lam gi them
        public bool XoaBNChoRaKhoiDSKhamBenh(string mabenhnhan)
        {
            string sql = "Delete from BENHNHANCHO where MaBenhNhan = '" + mabenhnhan + "'";
            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }
      


    }
}
