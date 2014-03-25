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
    class QuanLiNhanVienBUS
    {
        ConnectData conn = new ConnectData();

        //Lay ID tu dong tang cho Nhan Vien
        public string NextIDNhanVien()
        {
            return Utilities.NextID(conn.GetLastID("NHANVIEN", "MaNhanVien"), "NV");
        }

        // lay danh sach gioi tinh
        public DataTable DSGioiTinh()
        {
            string sql = "Select * from GIOITINH";
            return conn.GetDataTable(sql);
        }

        //lay danh sach hoc vi
        public DataTable DSHocVi()
        {
            string sql = "Select HocVan from HOCVAN where LoaiHV = 'HocVi'";
            return conn.GetDataTable(sql);
        }

        //lay danh sach hoc vi 
        public DataTable DSPhongBan()
        {
            string sql = "Select MaPhongBan from PHONGBAN";
            return conn.GetDataTable(sql);
        }

        //Lay Ten Phong ban tu ma phong ban
        public DataTable LayTenPhongBanTuMaPB(string cboText)
        {
            string sql = "Select TenPhongBan from PHONGBAN where MaPhongBan = '" + cboText + "'";
            return conn.GetDataTable(sql);
        }

        //lay danh sach trinh do
        public DataTable DSTrinhDo()
        {
            string sql = "Select HocVan from HOCVAN where LoaiHV = 'TrinhDo'";
            return conn.GetDataTable(sql);
        }

        //Xuat danh sach nhan vien
        public DataTable DSNhanVien()
        {
            string sql = "Select STT, MaNhanVien, NHANVIEN.MaPhongBan, TenPhongBan, MaPhongLamViec, Ho, Ten, GioiTinh, NgaySinh,"
            + " CMND, Email, SDT, DiaChi, NgayVaoLam, ChucVu, TrinhDo, HocVi from NHANVIEN, PHONGBAN where NHANVIEN.MaPhongBan = PHONGBAN.MaPhongBan";
            return conn.GetDataTable(sql);
        }

        //Kiem Tra Trung Ma Nhan Vien
        public bool KiemTraTrungMaNhanVien(string MaNhanVien)
        {
            if (conn.KiemTra("NHANVIEN", "MaNhanVien", MaNhanVien))
                return true;
            return false;
        }


        // Them mot nhan vien moi
        public bool ThemNhanVien(NhanVien nv)
        {
            string sql = string.Format("Insert into NHANVIEN(STT, MaNhanVien, MaPhongBan, Ho, Ten, GioiTinh, NgaySinh, CMND, Email, SDT, DiaChi, NgayVaoLam,"
                +" ChucVu, TrinhDo, HocVi, MaPhongLamViec) Values('{0}', '{1}', '{2}', N'{3}', N'{4}', N'{5}', N'{6}', '{7}', '{8}', '{9}', N'{10}', N'{11}', N'{12}', N'{13}', N'{14}', '{15}')",
                "0", nv.MaNhanVien, nv.MaPhongBan, nv.Ho, nv.Ten, nv.GioiTinh, nv.NgaySinh.ToString("MM/dd/yyyy"), nv.CMND, nv.Email, nv.SDT, nv.DiaChi, nv.NgayVaoLam.ToString("MM/dd/yyyy"),
                nv.ChucVu, nv.TrinhDo, nv.HocVi, nv.MaPhongLamViec);

            if (conn.ExcuteQuery(sql))
            {
                MessageBox.Show("Thêm Nhân viên mới thành công!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }

        //Sua doi thong tin NhanVien
        public bool SuaDoiNhanVien(NhanVien nv)
        {

            string sql = string.Format("Update NHANVIEN set Ho = N'{0}', Ten = N'{1}', GioiTinh = N'{2}', NgaySinh = N'{3}', CMND = '{4}', Email = '{5}', SDT = '{6}',"
            + " DiaChi = N'{7}', NgayVaoLam = N'{8}', ChucVu = N'{9}', TrinhDo = N'{10}', HocVi = N'{11}', MaPhongBan = '{12}', MaPhongLamViec = '{13}'  where MaNhanVien = '{14}' ", nv.Ho, nv.Ten, nv.GioiTinh,
            nv.NgaySinh.ToString("MM/dd/yyyy"), nv.CMND, nv.Email, nv.SDT, nv.DiaChi, nv.NgayVaoLam.ToString("MM/dd/yyyy"), nv.ChucVu, nv.TrinhDo, nv.HocVi, nv.MaPhongBan, nv.MaPhongLamViec, nv.MaNhanVien);

            if (conn.ExcuteQuery(sql))
            {
                MessageBox.Show("Sửa đổi thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }

        //xoa nhan vien ra khoi danh sach
        public bool XoaNhanVien(string MaNhanVien)
        {
            string sql = "Delete from NHANVIEN where MaNhanVien = '" + MaNhanVien +"'";

            if (conn.ExcuteQuery(sql))
            {
                MessageBox.Show("Xóa Nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }

        //Lay ma phong lam viec cho nhan vien x quang va xet nghiem
        public DataTable LayDSMaPhongLamViechChoNV(string cboText)
        {
            string sql = "SELECT MaPhongLamViec from PHONGLAMVIECNV where MaPhongLamViec like '" + cboText + "%'";
            return conn.GetDataTable(sql);
        }

        //Lay Ten Phong lam viec tu ma phong lam viec
        public DataTable LayTenPhongLVTuMaPhongLV(string MaPhongLamViec)
        {
            string sql = "Select TenPhongLamViec from PHONGLAMVIECNV where MaPhongLamViec = '" + MaPhongLamViec + "'";
            return conn.GetDataTable(sql);
        }

    }
}
