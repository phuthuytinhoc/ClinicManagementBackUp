using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace QuanLyBenhVien.BusinessObject
{
    class NhanVien
    {
        public int STT { get; set; }
        public string MaNhanVien { get; set; }
        public string MaPhongBan { get; set; }
        public string TenPhongBan { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public int CMND { get; set; }
        public string Email { get; set; }
        public long SDT { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public string ChucVu { get; set; }
        public string TrinhDo { get; set; }
        public string HocVi { get; set; }
        public string MaPhongLamViec { get; set; }
    }
}
