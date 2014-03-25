using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace QuanLyBenhVien.BusinessObject
{
    class TheoDoi
    {
        public string MaTheoDoi { get; set; }
        public string MaBenhAn { get; set; }
        public string MaBenhNhan { get; set; }
        public string MaKhoa { get; set; }
        public string MaBacSi { get; set; }
        public string MaYTa { get; set; }
        public string MaNhapVien { get; set; }
        public string MaPhongBenh { get; set; }
        public string MaGiuongBenh { get; set; }
        public string NhanDinhTinhTrang { get; set; }
        public string KeHoachChamSoc { get; set; }

        public DateTime NgayTheoDoi { get; set; }
        public int Mach { get; set; }
        public int NhietDo { get; set; }
        public int NhipTho { get; set; }
        public int HuyetAp { get; set; }
        public int CanNang { get; set; }
        public string DienBien { get; set; }
        public string XuLiChamSoc { get; set; }
        public string ThuocVatTu { get; set; }
    }
}
