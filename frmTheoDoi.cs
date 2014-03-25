using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLyBenhVien.BusinessLogic;
using QuanLyBenhVien.BusinessObject;
using System.IO;

namespace QuanLyBenhVien
{
    public partial class frmTheoDoi : Form
    {
        public frmTheoDoi()
        {
            InitializeComponent();
        }
        private TheoDoiBNBUS TheoDoiBN = new TheoDoiBNBUS();
        private XuatVienBNBUS XuatVienBN = new XuatVienBNBUS();
        private HinhAnhBUS ThemHinhAnh = new HinhAnhBUS();

        public void LoadAnhTuDB(string MaHinhAnh, PictureBox pic)
        {
            if (ThemHinhAnh.KiemTraMaHinhAnh(MaHinhAnh))
            {
                byte[] img = ThemHinhAnh.RetrieveImage(MaHinhAnh);
                MemoryStream str = new MemoryStream(img);
                pic.Image = Image.FromStream(str);
            }
            else
                pic.Image = null;
        }

        public bool KiemTraNhapLieuTheoDoi()
        {
            if (txtTheoDoi_CanNangBN.Text == "" || txtTheoDoi_HuyetApBN.Text == "" || txtTheoDoi_NhietDoBN.Text == "" ||
                txtTheoDoi_NhipThoBN.Text == "" || txtTheoDoi_VatTuSuDung.Text == "" || txtTheoDoi_XuLiChamSoc.Text == "" ||
                txtTheoDoi_DienBien.Text == "")
                return false;
            return true;
        }
        
        public void AddMaBNTheoDoi(TextBox mabenhnhan)
        {
            txtMaBenhNhan.Text = mabenhnhan.Text;
        }

        private void frmTheoDoi_Load(object sender, EventArgs e)
        {
            cboGioiTinhBN.DataSource = XuatVienBN.LayGT_MaDTCuaBenhNhan(txtMaBenhNhan.Text);
            cboGioiTinhBN.DisplayMember = "GTBN";
            cboGioiTinhBN.ValueMember = "GioiTinh";

            cboMaDoiTuongBN.DataSource = XuatVienBN.LoadMaDoiTuong();
            cboMaDoiTuongBN.DisplayMember = "MaDoiTuongNV";
            cboMaDoiTuongBN.ValueMember = "MaDoiTuong";

            LoadAnhTuDB(txtMaBenhNhan.Text, picTheoDoiBenhNhan);

            #region Thong Tin Benh Nhan

            txtHoBenhNhan.DataBindings.Clear();
            txtTenBenhNhan.DataBindings.Clear();
            txtCMND.DataBindings.Clear();
            txtNgheNghiep.DataBindings.Clear();
            txtDiaChiBN.DataBindings.Clear();
            cboGioiTinhBN.DataBindings.Clear();
            cboMaDoiTuongBN.DataBindings.Clear();
            txtLyDoNhapVien.DataBindings.Clear();
            txtMaBenhAn.DataBindings.Clear();

            txtHoBenhNhan.DataBindings.Add("text", TheoDoiBN.LayThongTinBNTheoDoi(txtMaBenhNhan.Text), "Ho");
            txtTenBenhNhan.DataBindings.Add("text", TheoDoiBN.LayThongTinBNTheoDoi(txtMaBenhNhan.Text), "Ten");
            txtCMND.DataBindings.Add("text", TheoDoiBN.LayThongTinBNTheoDoi(txtMaBenhNhan.Text), "CMND");
            txtNgheNghiep.DataBindings.Add("text", TheoDoiBN.LayThongTinBNTheoDoi(txtMaBenhNhan.Text), "NgheNghiep");
            txtDiaChiBN.DataBindings.Add("text", TheoDoiBN.LayThongTinBNTheoDoi(txtMaBenhNhan.Text), "DiaChi");
            cboGioiTinhBN.DataBindings.Add("text", TheoDoiBN.LayThongTinBNTheoDoi(txtMaBenhNhan.Text), "GioiTinh");
            cboMaDoiTuongBN.DataBindings.Add("text", TheoDoiBN.LayThongTinBNTheoDoi(txtMaBenhNhan.Text), "MaDoiTuong");
            txtLyDoNhapVien.DataBindings.Add("text", TheoDoiBN.LayThongTinBNTheoDoi(txtMaBenhNhan.Text), "ChuanDoanSauCung");
            txtMaBenhAn.DataBindings.Add("text", TheoDoiBN.LayThongTinBNTheoDoi(txtMaBenhNhan.Text), "MaBenhAn");

            #endregion

            #region  Thong tin Nhap Vien

            dtPickerNgayNhapVienBN.DataBindings.Clear();
            txtKQChuanDoanBN.DataBindings.Clear();
            txtPPDieuTriBN.DataBindings.Clear();
            txtMaPhongBenhBN.DataBindings.Clear();
            txtMaGiuongBenhBN.DataBindings.Clear();
            txtMaKhoaDieuTriBN.DataBindings.Clear();
            txtMaNhapVien.DataBindings.Clear();
            txtMaTheoDoiBN.DataBindings.Clear();

            dtPickerNgayNhapVienBN.DataBindings.Add("text", TheoDoiBN.LayThongTinBNNhapVien(txtMaBenhNhan.Text), "NgayNhapVien");
            txtKQChuanDoanBN.DataBindings.Add("text", TheoDoiBN.LayThongTinBNNhapVien(txtMaBenhNhan.Text), "KetQuaChuanDoan");
            txtPPDieuTriBN.DataBindings.Add("text", TheoDoiBN.LayThongTinBNNhapVien(txtMaBenhNhan.Text), "PhuongPhapDieuTri");
            txtMaPhongBenhBN.DataBindings.Add("text", TheoDoiBN.LayThongTinBNNhapVien(txtMaBenhNhan.Text), "MaPhongBenh");//
            txtMaGiuongBenhBN.DataBindings.Add("text", TheoDoiBN.LayThongTinBNNhapVien(txtMaBenhNhan.Text), "MaGiuongBenh");
            txtMaKhoaDieuTriBN.DataBindings.Add("text", TheoDoiBN.LayThongTinBNNhapVien(txtMaBenhNhan.Text), "MaKhoa");
            txtMaNhapVien.DataBindings.Add("text", TheoDoiBN.LayThongTinBNNhapVien(txtMaBenhNhan.Text), "MaNhapVien");
            txtMaTheoDoiBN.DataBindings.Add("text", TheoDoiBN.LayMaTheoDoiBN(txtMaBenhNhan.Text), "MaTheoDoi");

            #endregion

          
        }

        private void txtMaBenhNhan_TextChanged(object sender, EventArgs e)
        {
         
            #region Luu thong tin theo doi

            if (TheoDoiBN.KiemTraMaBenhNhanTrongTHEODOI(txtMaBenhNhan.Text))
            {
                pnlBenhNhan.Enabled = true;
                pnlChiTet.Enabled = true;
                pnlKeHoacChamSoc.Enabled = true;
                pnlNhapVien.Enabled = true;
                pnlPhongBenh.Enabled = true;
            }
            else
            {
                pnlBenhNhan.Enabled = false;
                pnlChiTet.Enabled = false;
                pnlKeHoacChamSoc.Enabled = false;
                pnlNhapVien.Enabled = false;
                pnlPhongBenh.Enabled = false;
                TheoDoi td = LayThongTinTheoDoiBN();
                TheoDoiBN.LuuThongTinTheoDoiBN(td);
            }

            #endregion

            //LayThongTinBNNhapVien();
        }

        private void cboMaDoiTuongBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenDoiTuong.DataBindings.Clear();
            txtTenDoiTuong.DataBindings.Add("text", TheoDoiBN.LayTenDoiTuong(cboMaDoiTuongBN.Text), "TenDoiTuong");
        }

        private void txtMaPhongBenhBN_TextChanged(object sender, EventArgs e)
        {
            txtTenPhongBenhBN.DataBindings.Clear();
            txtTenPhongBenhBN.DataBindings.Add("text", TheoDoiBN.LayTenPhongBenhTuMaPB(txtMaPhongBenhBN.Text), "TenPhong");

            //load ma bac si ma y ya tu ma phong benh
            txtMaBSPhongBenh.DataBindings.Clear();
            txtMaYTaPhongBenh.DataBindings.Clear();
            txtMaNguoiThucHien.DataBindings.Clear();

            txtMaBSPhongBenh.DataBindings.Add("text", TheoDoiBN.LayMaBacSiTuMaPB(txtMaPhongBenhBN.Text), "MaBacSi");
            txtMaYTaPhongBenh.DataBindings.Add("text", TheoDoiBN.LayMaYTaTuMaPB(txtMaPhongBenhBN.Text), "MaYTa");
            txtMaNguoiThucHien.DataBindings.Add("text", TheoDoiBN.LayMaYTaTuMaPB(txtMaPhongBenhBN.Text), "MaYTa");
        }
        
        private TheoDoi LayThongTinTheoDoiBN()
        {
            TheoDoi td = new TheoDoi();

            td.MaTheoDoi = txtMaTheoDoiBN.Text;
            td.MaBenhAn = txtMaBenhAn.Text;
            td.MaBenhNhan = txtMaBenhNhan.Text;
            td.MaKhoa = txtMaKhoaDieuTriBN.Text;
            td.MaBacSi = txtMaBSPhongBenh.Text;
            td.MaYTa = txtMaYTaPhongBenh.Text;
            td.MaGiuongBenh = txtMaGiuongBenhBN.Text;
            td.MaNhapVien = txtMaNhapVien.Text;
            td.MaGiuongBenh = txtMaGiuongBenhBN.Text;
            td.MaPhongBenh = txtMaGiuongBenhBN.Text;


            return td;
        }

        private TheoDoi LayThongTinChiTietTheoDoiBN()
        {
            TheoDoi td = new TheoDoi();

            td.MaTheoDoi = txtMaTheoDoiBN.Text;
            td.NhanDinhTinhTrang = txtNhanDinhTT.Text;
            td.XuLiChamSoc = txtTheoDoi_XuLiChamSoc.Text;
            td.NgayTheoDoi = Convert.ToDateTime(dtPickerNgayTheoDoi.Value.ToString());
            td.Mach = Convert.ToInt32(txtTheoDoi_MachBN.Text);
            td.NhietDo = Convert.ToInt32(txtTheoDoi_NhietDoBN.Text);
            td.NhipTho = Convert.ToInt32(txtTheoDoi_NhipThoBN.Text);
            td.HuyetAp = Convert.ToInt32(txtTheoDoi_HuyetApBN.Text);
            td.CanNang = Convert.ToInt32(txtTheoDoi_CanNangBN.Text);
            td.DienBien = txtTheoDoi_DienBien.Text;       
            td.KeHoachChamSoc = txtKeHoachMucTieu.Text;
            td.ThuocVatTu = txtTheoDoi_VatTuSuDung.Text;
        

            return td;          
        }

        private void txtMaBSPhongBenh_TextChanged(object sender, EventArgs e)
        {
            txtHoBSPhongBenh.DataBindings.Clear();
            txtTenBSPhongBenh.DataBindings.Clear();

            txtHoBSPhongBenh.DataBindings.Add("text", TheoDoiBN.LayHoTenBacSiTuMS(txtMaBSPhongBenh.Text), "Ho");
            txtTenBSPhongBenh.DataBindings.Add("text", TheoDoiBN.LayHoTenBacSiTuMS(txtMaBSPhongBenh.Text), "Ten");
        }

        private void txtMaYTaPhongBenh_TextChanged(object sender, EventArgs e)
        {
            txtHoYTPhongBenh.DataBindings.Clear();
            txtTenYTPhongBenh.DataBindings.Clear();
            txtHoNguoiThucHien.DataBindings.Clear();
            txtTenNguoiThucHien.DataBindings.Clear();

            txtHoYTPhongBenh.DataBindings.Add("text", TheoDoiBN.LayHoTenYTaTuYT(txtMaYTaPhongBenh.Text), "Ho");
            txtTenYTPhongBenh.DataBindings.Add("text", TheoDoiBN.LayHoTenYTaTuYT(txtMaYTaPhongBenh.Text), "Ten");
            txtHoNguoiThucHien.DataBindings.Add("text", TheoDoiBN.LayHoTenYTaTuYT(txtMaYTaPhongBenh.Text), "Ho");
            txtTenNguoiThucHien.DataBindings.Add("text", TheoDoiBN.LayHoTenYTaTuYT(txtMaYTaPhongBenh.Text), "Ten");
        }

        private void txtMaKhoaDieuTriBN_TextChanged(object sender, EventArgs e)
        {
            txtTenKhoaDieuTriBN.DataBindings.Clear();
            txtTenKhoaDieuTriBN.DataBindings.Add("text", TheoDoiBN.LayTenKhoaTuMaKhoa(txtMaKhoaDieuTriBN.Text), "TenKhoa");
        }

        private void btnTheoDoi_Luu_Click(object sender, EventArgs e)
        {
            if (KiemTraNhapLieuTheoDoi())
            {
                #region ham luu

                TheoDoi td = LayThongTinChiTietTheoDoiBN();

                string a = dtPickerNgayTheoDoi.Value.ToString("MM/dd/yyyy");
                string b = dtPickerNgayTheoDoi.Value.ToString("dd/MM/yyyy");

                if (TheoDoiBN.KiemTraNgayTheoDoiTrongCTTD(a))
                {
                    if (TheoDoiBN.SuaDoiThongTInCHiTietTheoDoi(td))
                        MessageBox.Show("Cập nhật thông tin ngày theo dõi: {" + b + "} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Cập nhật thông tin ngày theo dõi: {" + b + "} thất bại!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (TheoDoiBN.ThemThongTinChiTietTheoDoi(td))
                        MessageBox.Show("Thêm thông tin ngày theo dõi: {" + b + "} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Thêm thông tin ngày theo dõi: {" + b + "} thất bại!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //sua doi hooac them vao trong chi tiet theo doi
                                

                #endregion
            }
            else
                MessageBox.Show("Chưa nhập đủ thông tin theo dõi bệnh nhân!","Lỗi ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnTheoDoi_ThemTT_Click(object sender, EventArgs e)
        {
            pnlBenhNhan.Enabled = true;
            pnlChiTet.Enabled = true;
            pnlKeHoacChamSoc.Enabled = true;
            pnlNhapVien.Enabled = true;
            pnlPhongBenh.Enabled = true;
            //txtMaTheoDoiBN.Text = TheoDoiBN.NextIDTheoDoi();
        }

        #region event cua textbox

        private void txtTheoDoi_MachBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtTheoDoi_NhipThoBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtTheoDoi_CanNangBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtTheoDoi_NhietDoBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtTheoDoi_HuyetApBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        #endregion

        private void dtPickerNgayTheoDoi_ValueChanged(object sender, EventArgs e)
        {
            string a = dtPickerNgayTheoDoi.Value.ToString("MM/dd/yyyy");

            if (TheoDoiBN.KiemTraNgayTheoDoiTrongCTTD(a))
            {
                #region databindings

                txtNhanDinhTT.DataBindings.Clear();
                txtKeHoachMucTieu.DataBindings.Clear();
                txtTheoDoi_MachBN.DataBindings.Clear();
                txtTheoDoi_NhietDoBN.DataBindings.Clear();
                txtTheoDoi_NhipThoBN.DataBindings.Clear();
                txtTheoDoi_HuyetApBN.DataBindings.Clear();
                txtTheoDoi_CanNangBN.DataBindings.Clear();
                txtTheoDoi_DienBien.DataBindings.Clear();
                txtTheoDoi_XuLiChamSoc.DataBindings.Clear();
                txtTheoDoi_VatTuSuDung.DataBindings.Clear();

                txtNhanDinhTT.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "NhanDinhTinhTrang");
                txtKeHoachMucTieu.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "KeHoachChamSoc");
                txtTheoDoi_MachBN.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "Mach");
                txtTheoDoi_NhietDoBN.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "NhietDo");
                txtTheoDoi_NhipThoBN.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "NhipTho");
                txtTheoDoi_HuyetApBN.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "HuyetAp");
                txtTheoDoi_CanNangBN.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "CanNang");
                txtTheoDoi_DienBien.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "DienBien");
                txtTheoDoi_XuLiChamSoc.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "XuLiChamSoc");
                txtTheoDoi_VatTuSuDung.DataBindings.Add("text", TheoDoiBN.ChiTietDonThuocTuNgayTheoDoi(a), "ThuocVatTu");

                #endregion
            }
            else
            {
                txtNhanDinhTT.Text = "";
                txtKeHoachMucTieu.Text = "";
                txtTheoDoi_MachBN.Text = "";
                txtTheoDoi_NhietDoBN.Text = "";
                txtTheoDoi_NhipThoBN.Text = "";
                txtTheoDoi_HuyetApBN.Text = "";
                txtTheoDoi_CanNangBN.Text = "";
                txtTheoDoi_DienBien.Text = "";
                txtTheoDoi_XuLiChamSoc.Text = "";
                txtTheoDoi_VatTuSuDung.Text = "";
            }
        }

        private void btnDongTheoDoi_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("LƯU thông tin theo dõi bệnh nhân chọn Yes, \n XÓA thông tin theo dõi bệnh nhân chọn No","Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
           

            if (result == DialogResult.Yes)
            {
                if (TheoDoiBN.KiemTraMaBenhNhanTrongTHEODOI(txtMaBenhNhan.Text))
                {
                    MessageBox.Show("Đã lưu thông tin theo dõi bệnh nhân!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                    MessageBox.Show("Bệnh nhân này chưa có mã theo dõi để lưu, hãy tạo mới mã theo dõi!","Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (result == DialogResult.No)
                {
                    DialogResult result1 = MessageBox.Show("Xóa bệnh nhân toàn bộ thông tin theo dõi bệnh nhân: {" + txtMaBenhNhan.Text + "} ?", "Thông báo",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result1 == DialogResult.Yes)
                    {
                        TheoDoiBN.XoaTheoDoiBN(txtMaTheoDoiBN.Text);
                        this.Close();
                    }
                }
            }
        }
    }
}
