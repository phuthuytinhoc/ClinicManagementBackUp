using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace QuanLyBenhVien.BusinessObject
{
    class ChuyenVien
    {
        public string MaChuyenVien { get; set; }
        public string MaBenhNhan { get; set; }
        public string MaBenhAn { get; set; }
        public DateTime NgayChuyenVien { get; set; }
        public DateTime GioChuyenVien { get; set; }
        public string DauHieuLamSang { get; set; }
        public string LyDoChuyenVien { get; set; }
        public string TinhTrangKhiChuyen { get; set; }
        public string PhuongTienDiChuyen { get; set; }
    }
}
