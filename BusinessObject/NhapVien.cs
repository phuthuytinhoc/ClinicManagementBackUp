using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace QuanLyBenhVien.BusinessObject
{
    class NhapVien
    {
        public string MaNhapVien { get; set; }
        public string MaBenhAn { get; set; }
        public string MaBenhNhan { get; set; }
        public string MaKhoa { get; set; }
        public string MaPhongBenh { get; set; }
        public string MaGiuongBenh { get; set; }
        public string KetQuaChuanDoan { get; set; }
        public string PhuongPhapDieuTri { get; set; }
        public DateTime NgayNhapVien { get; set; }
        public DateTime NgayXuatVienDK { get; set; }
        public string TamUng { get; set; }
    }
}
