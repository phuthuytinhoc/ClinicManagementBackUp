using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using QuanLyBenhVien.DataAccess;
using System.Data;
using QuanLyBenhVien.BusinessObject;

namespace QuanLyBenhVien.BusinessLogic
{
    class NhapVienBNBUS
    {
        ConnectData connData = new ConnectData();

        public string NextIDNhapVien()
        {
            return Utilities.NextID(connData.GetLastID("NHAPVIEN", "MaNhapVien"), "VV");
        }

        //Lay Ten doi tuong tu ma doi tuong
        public DataTable LayTenDoiTuong(string madoituong)
        {
            string sql = "Select TenDoiTuong from DOITUONG where MaDoiTuong = '" + madoituong + "'";
            return connData.GetDataTable(sql);
        }

        //Lay thong tin benh nhan tu ma benh nhan
        public DataTable LayThongTinBNNhapVien(string mabenhnhan)
        {
            string sql = "Select Ho, Ten, GioiTinh, CMND, NgheNghiep, MaBenhAn, NgaySinh, SDT, MaDoiTuong, DiaChi"
                + " from BENHNHAN where MaBenhNhan = '" + mabenhnhan + "'";
            return connData.GetDataTable(sql);
        }

        //lay thong tin ten khoa tu ma khoa
        public DataTable LayTenMaKhoaDieuTri(string makhoa)
        {
            string sql = "Select TenKhoa from KHOA where MaKhoa ='" + makhoa + "'";
            return connData.GetDataTable(sql);
        }

        //lay ma khoa dieu tri
        public DataTable LayMaKhoaDieuTri()
        {
            string sql = "Select MaKhoa from KHOA";
            return connData.GetDataTable(sql);
        }

        //load ma doi tuong
        public DataTable LoadMaDoiTuong()
        {
            string sql = "Select MaDoiTuong from DOITUONG";
            return connData.GetDataTable(sql);
        }

        //Lay ho ten bac si tu ma phong benh
        public DataTable LayHoTenBSPhongBenh(string maphongbenh)
        {
            string sql = "Select Ho, Ten from BACSI where MaPhongBenh = '" + maphongbenh + "'";
            return connData.GetDataTable(sql);
        }
        //Lay ho ten y ta tu ma phong benh
        public DataTable LayHoTenYTPhongBenh(string maphongbenh)
        {
            string sql = "Select Ho, Ten from YTA where MaPhongBenh = '" + maphongbenh + "'";
            return connData.GetDataTable(sql);
        }

        //lay giuong benh tu ma phong benh 
        public DataTable LayMaGiuongBenhTuMaPhongBenh(string maphongbenh)
        {
            string sql = "Select * from DanhsachPhongBenh('" + maphongbenh + "')";
            return connData.GetDataTable(sql);
        }      

        //lay ma phong benh
        public DataTable LayMaPhongBenhChoBN()
        {
            string sql = "Select MaPhongBenh from PHONGBENH where MaPhongBenh <> 'No'";
            return connData.GetDataTable(sql);
        }

        //ham luu thong tin nhap vien cua benh nhan
        public bool LuuThongTinNhapVienBN(NhapVien nvi)
        {
            string sql = string.Format("Insert into NHAPVIEN(STT, MaNhapVien, MaBenhAn, MaBenhNhan, MaKhoa, MaPhongBenh, "
            + " MaGiuongBenh, KetQuaChuanDoan, PhuongPhapDieuTri, NgayNhapVien, NgayXuatVienDK, TamUng) values ('{0}', '{1}',"
            +" '{2}', '{3}', '{4}', '{5}', '{6}', N'{7}', N'{8}', N'{9}', N'{10}', '{11}' )", "0", nvi.MaNhapVien,
            nvi.MaBenhAn, nvi.MaBenhNhan, nvi.MaKhoa, nvi.MaPhongBenh, nvi.MaGiuongBenh, nvi.KetQuaChuanDoan, nvi.PhuongPhapDieuTri,
            nvi.NgayNhapVien.ToString("MM/dd/yyyy"), nvi.NgayXuatVienDK.ToString("MM/dd/yyyy"), nvi.TamUng);
            
            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public bool SuaDoiThongTinNhapVienBN(NhapVien nvi)
        {
            string sql = string.Format("Update NHAPVIEN set MaKhoa = '{0}', MaPhongBenh = '{1}', MaGiuongBenh = '{2}', "
            + "KetQuaChuanDoan = N'{3}', PhuongPhapDieuTri = N'{4}', NgayNhapVien = N'{5}', NgayXuatVienDK = N'{6}', TamUng = '{7}' where MaNhapVien = '{8}'",
            nvi.MaKhoa, nvi.MaPhongBenh, nvi.MaGiuongBenh, nvi.KetQuaChuanDoan, nvi.PhuongPhapDieuTri, nvi.NgayNhapVien.ToString("MM/dd/yyyy"),
            nvi.NgayXuatVienDK.ToString("MM/dd/yyyy"), nvi.TamUng, nvi.MaNhapVien);

            if (connData.ExcuteQuery(sql))
                return true;
            return false;
        }

        public bool KiemTraMaNhapVien(string MaNhapVien)
        {
            if (connData.KiemTra("NHAPVIEN", "MaNhapVien", MaNhapVien))
                return true;
            return false;
        }
    }
}
