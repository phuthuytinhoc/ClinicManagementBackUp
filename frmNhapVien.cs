using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLyBenhVien.BusinessLogic;
using System.IO;
using QuanLyBenhVien.BusinessObject;
using CrystalDecisions.Shared;

namespace QuanLyBenhVien
{
    public partial class frmNhapVien : Form
    {
        public frmNhapVien()
        {
            InitializeComponent();
        }

        private NhapVienBNBUS NhapVienBN = new NhapVienBNBUS();
        private HinhAnhBUS ThemHinhAnh = new HinhAnhBUS();


        #region Cac Ham lien quan

        public void AddMaBN(TextBox MaBenhNhan)
        {
            txtNhapVien_MaBN.Text = MaBenhNhan.Text;
        }

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

        //ham lay thong tin nhap vien
        private NhapVien LayThongTinNhapVien()
        {
            NhapVien nvi = new NhapVien();

            nvi.MaBenhNhan = txtNhapVien_MaBN.Text;
            nvi.MaBenhAn = txtNhapVien_MaBenhAn.Text;
            nvi.MaNhapVien = txtNhapVien_MaNhapVien.Text;
            nvi.KetQuaChuanDoan = txtNhapVien_KQChuanDoan.Text;
            nvi.MaKhoa = cboNhapVien_KhoaDieuTri.SelectedValue.ToString();
            nvi.PhuongPhapDieuTri = txtNhapVien_PPDieuTri.Text;
            nvi.MaPhongBenh = cboNhapVien_PhongBenh.SelectedValue.ToString();
            nvi.MaGiuongBenh = cboNhapVien_GiuongBenh.SelectedValue.ToString();
            nvi.NgayNhapVien = Convert.ToDateTime(dtPickerNhapVien_NgayNhapVien.Value.ToString());
            nvi.NgayXuatVienDK = Convert.ToDateTime(dtPickerNgayXuatVienDuKien.Value.ToString());
            nvi.TamUng = txtNhapVien_TamUng.Text;

            return nvi;
        }

        public bool KiemTraThongTinhNhapVienBN()
        {
            if (txtNhapVien_KQChuanDoan.Text == "" || txtNhapVien_PPDieuTri.Text == "")
                return false;
            return true;
        }

        #endregion


        #region Cac button

        private void btnNhapVien_Dong_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Hãy chắc chắn rằng bạn đã lưu thông tin phiếu nhập viện !", "LƯU Ý", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }

        private void btnNhapVien_LuuBN_Click(object sender, EventArgs e)
        {
            
            NhapVien nvi = LayThongTinNhapVien();

            if (KiemTraThongTinhNhapVienBN())
            {
                if (NhapVienBN.KiemTraMaNhapVien(txtNhapVien_MaNhapVien.Text))
                {
                    if (NhapVienBN.SuaDoiThongTinNhapVienBN(nvi))
                        MessageBox.Show("Cập nhật thông tin bệnh nhân nhập viện thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Cập nhật thông tin bệnh nhân nhập viện thất bại!", "Thông báo",
                             MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (NhapVienBN.LuuThongTinNhapVienBN(nvi))
                    {
                        btnNhapVien_InBN.Visible = true;
                        MessageBox.Show("Lưu thông tin bệnh nhân nhập viện thành công!", "Thông báo",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Lưu thông tin bệnh nhân nhập viện thất bại!", "Thông báo",
                             MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Chưa nhập dầy đủ thông tin nhập viện của bệnh nhân!","Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
           
        }

        #endregion


        #region Cac event lien quan

        private void frmNhapVien_Load(object sender, EventArgs e)
        {
            txtNhapVien_MaNhapVien.Text = NhapVienBN.NextIDNhapVien();

            LoadAnhTuDB(txtNhapVien_MaBN.Text, picNhapVienBN);

            #region Load combobox
            //Ma doi tuong
            cboNhapVien_MaDoiTuong.DataSource = NhapVienBN.LoadMaDoiTuong();
            cboNhapVien_MaDoiTuong.DisplayMember = "MaDoiTuongNV";
            cboNhapVien_MaDoiTuong.ValueMember = "MaDoiTuong";
            //makhoa
            cboNhapVien_KhoaDieuTri.DataSource = NhapVienBN.LayMaKhoaDieuTri();
            cboNhapVien_KhoaDieuTri.DisplayMember = "MaKhoaNV";
            cboNhapVien_KhoaDieuTri.ValueMember = "MaKhoa";
            //load phong benh
            cboNhapVien_PhongBenh.DataSource = NhapVienBN.LayMaPhongBenhChoBN();
            cboNhapVien_PhongBenh.DisplayMember = "DSPhongBenh";
            cboNhapVien_PhongBenh.ValueMember = "MaPhongBenh";

            #endregion

            #region load thong tin benh nhan

            txtNhapVien_HoBN.DataBindings.Clear();
            txtNhapVien_TenBN.DataBindings.Clear();
            txtNhapVien_GioiTinh.DataBindings.Clear();
            txtNhapVien_CMND.DataBindings.Clear();
            txtNhapVien_NgheNghiep.DataBindings.Clear();
            txtNhapVien_MaBenhAn.DataBindings.Clear();
            dtPickerNhapVien_NgaySinhBN.DataBindings.Clear();
            txtNhapVien_SDTBN.DataBindings.Clear();
            cboNhapVien_MaDoiTuong.DataBindings.Clear();
            txtNhapVien_DiaChiBN.DataBindings.Clear();


            txtNhapVien_HoBN.DataBindings.Add("text", NhapVienBN.LayThongTinBNNhapVien(txtNhapVien_MaBN.Text), "Ho");
            txtNhapVien_TenBN.DataBindings.Add("text", NhapVienBN.LayThongTinBNNhapVien(txtNhapVien_MaBN.Text), "Ten");
            txtNhapVien_GioiTinh.DataBindings.Add("text", NhapVienBN.LayThongTinBNNhapVien(txtNhapVien_MaBN.Text), "GioiTinh");
            txtNhapVien_CMND.DataBindings.Add("text", NhapVienBN.LayThongTinBNNhapVien(txtNhapVien_MaBN.Text), "CMND");
            txtNhapVien_NgheNghiep.DataBindings.Add("text", NhapVienBN.LayThongTinBNNhapVien(txtNhapVien_MaBN.Text), "NgheNghiep");
            txtNhapVien_MaBenhAn.DataBindings.Add("text", NhapVienBN.LayThongTinBNNhapVien(txtNhapVien_MaBN.Text), "MaBenhAn");
            dtPickerNhapVien_NgaySinhBN.DataBindings.Add("text", NhapVienBN.LayThongTinBNNhapVien(txtNhapVien_MaBN.Text), "NgaySinh");
            txtNhapVien_SDTBN.DataBindings.Add("text", NhapVienBN.LayThongTinBNNhapVien(txtNhapVien_MaBN.Text), "SDT");
            cboNhapVien_MaDoiTuong.DataBindings.Add("text", NhapVienBN.LayThongTinBNNhapVien(txtNhapVien_MaBN.Text), "MaDoiTuong");
            txtNhapVien_DiaChiBN.DataBindings.Add("text", NhapVienBN.LayThongTinBNNhapVien(txtNhapVien_MaBN.Text), "DiaChi");

            #endregion

        }

        private void cboNhapVien_MaDoiTuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNhapVien_TenDoiTuong.DataBindings.Clear();
            txtNhapVien_TenDoiTuong.DataBindings.Add("text",NhapVienBN.LayTenDoiTuong(cboNhapVien_MaDoiTuong.Text), "TenDoiTuong");
        }

        private void cboNhapVien_KhoaDieuTri_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNhapVien_TenKhoaDieuTri.DataBindings.Clear();
            txtNhapVien_TenKhoaDieuTri.DataBindings.Add("text", NhapVienBN.LayTenMaKhoaDieuTri(cboNhapVien_KhoaDieuTri.Text), "TenKhoa");
        }

        private void cboNhapVien_PhongBenh_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNhapVien_TenYT.Text = "";
            txtNhapVien_TenBS.Text = "";
            txtNhapVien_HoBS.Text = "";
            txtNhapVien_HoYT.Text = "";

            txtNhapVien_HoBS.DataBindings.Clear();
            txtNhapVien_TenBS.DataBindings.Clear();
            txtNhapVien_HoYT.DataBindings.Clear();
            txtNhapVien_TenYT.DataBindings.Clear();

            txtNhapVien_HoBS.DataBindings.Add("text", NhapVienBN.LayHoTenBSPhongBenh(cboNhapVien_PhongBenh.Text), "Ho");
            txtNhapVien_TenBS.DataBindings.Add("text", NhapVienBN.LayHoTenBSPhongBenh(cboNhapVien_PhongBenh.Text), "Ten");
            txtNhapVien_HoYT.DataBindings.Add("text", NhapVienBN.LayHoTenYTPhongBenh(cboNhapVien_PhongBenh.Text), "Ho");
            txtNhapVien_TenYT.DataBindings.Add("text", NhapVienBN.LayHoTenYTPhongBenh(cboNhapVien_PhongBenh.Text), "Ten");

            cboNhapVien_GiuongBenh.DataSource = NhapVienBN.LayMaGiuongBenhTuMaPhongBenh(cboNhapVien_PhongBenh.Text);
            cboNhapVien_GiuongBenh.DisplayMember = "DSMaGiuongBenh";
            cboNhapVien_GiuongBenh.ValueMember = "MaGiuongBenh";

        }

        private void txtNhapVien_CMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtNhapVien_SDTBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtNhapVien_TamUng_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        #endregion

        private void dtPickerNgayXuatVienDuKien_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime nhapvien = dtPickerNhapVien_NgayNhapVien.Value;
                DateTime xuatvien = dtPickerNgayXuatVienDuKien.Value;
                if ((DateTime.Parse(nhapvien.ToString()) > DateTime.Parse(xuatvien.ToString())))
                {
                    dtPickerNgayXuatVienDuKien.Text = nhapvien.ToString();

                }
            }
            catch
            {
                MessageBox.Show("Nhập sai ngày xuất viện");
            }
        }

        private void dtPickerNhapVien_NgayNhapVien_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime nhapvien = dtPickerNhapVien_NgayNhapVien.Value;
                DateTime xuatvien = dtPickerNgayXuatVienDuKien.Value;
                if ((DateTime.Parse(xuatvien.ToString()) < DateTime.Parse(nhapvien.ToString())))
                {
                    dtPickerNhapVien_NgayNhapVien.Text = DateTime.Now.ToString();

                }
            }
            catch(Exception)
            {
                MessageBox.Show("Nhập sai ngày nhập viện");
            }
        }

        private void btnNhapVien_InBN_Click(object sender, EventArgs e)
        {
            btnNhapVien_InBN.Visible = false;
            btnTroLai.Visible = true;
            expandableSplitter1.Expanded = true;

            PhieuNhapVien rp = new PhieuNhapVien();
            ParameterValues a = new ParameterValues();
            ParameterDiscreteValue b = new ParameterDiscreteValue();

            b.Value = txtNhapVien_MaBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["MaBenhNhan"].ApplyCurrentValues(a);

            b.Value = txtNhapVien_HoBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HoBenhNhan"].ApplyCurrentValues(a);

            b.Value = dtPickerNhapVien_NgaySinhBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgaySinh"].ApplyCurrentValues(a);

            b.Value = txtNhapVien_TenBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenBenhNhan"].ApplyCurrentValues(a);

            b.Value = txtNhapVien_GioiTinh.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["GioiTinh"].ApplyCurrentValues(a);

            b.Value = txtNhapVien_DiaChiBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["DiaChi"].ApplyCurrentValues(a);

            b.Value = txtNhapVien_NgheNghiep.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgheNghiep"].ApplyCurrentValues(a);

            b.Value = txtNhapVien_MaBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["MaBenhNhan"].ApplyCurrentValues(a);

            b.Value = txtNhapVien_MaNhapVien.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["MaPhieu"].ApplyCurrentValues(a);

            b.Value = dtPickerNhapVien_NgayNhapVien.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgayNhapVien"].ApplyCurrentValues(a);

            b.Value = dtPickerNgayXuatVienDuKien.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgayXuatVien"].ApplyCurrentValues(a);

            b.Value = cboNhapVien_KhoaDieuTri.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["MaKhoa"].ApplyCurrentValues(a);

            b.Value = txtNhapVien_TenKhoaDieuTri.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenKhoa"].ApplyCurrentValues(a);

            b.Value = txtNhapVien_PPDieuTri.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["PhuongPhapDieuTri"].ApplyCurrentValues(a);

            b.Value = txtNhapVien_KQChuanDoan.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["KetQuaChanDoan"].ApplyCurrentValues(a);

            b.Value = cboNhapVien_PhongBenh.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["MaPhongBenh"].ApplyCurrentValues(a);

            b.Value = cboNhapVien_GiuongBenh.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["GiuongBenh"].ApplyCurrentValues(a);

            b.Value = txtNhapVien_HoBS.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HoBacSi"].ApplyCurrentValues(a);

            b.Value = txtNhapVien_TenBS.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenBacSi"].ApplyCurrentValues(a);

            b.Value = txtNhapVien_HoYT.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HoYTa"].ApplyCurrentValues(a);

            b.Value = txtNhapVien_TenYT.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenYTa"].ApplyCurrentValues(a);

            crystalReportViewer1.ReportSource = rp;
        }

        private void btnTroLai_Click(object sender, EventArgs e)
        {
            btnNhapVien_InBN.Visible = true;
            btnTroLai.Visible = false;
            expandableSplitter1.Expanded = false;
        }

        private void groupPanel2_Click(object sender, EventArgs e)
        {

        }


    }
}
