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

namespace QuanLyBenhVien
{
    public partial class frmThemBenhNhan : Form
    {        

        public frmThemBenhNhan()
        {
            InitializeComponent();
        }

        #region Khai Bao

        private ThemBenhNhanBUS ThemBNBUS = new ThemBenhNhanBUS();
        private PhongKhamBUS PhongKhamBN = new PhongKhamBUS();
        private HinhAnhBUS ThemHinhAnh = new HinhAnhBUS();
        private BenhAnBNBUS BenhAnBN = new BenhAnBNBUS();
        private PhieuKhamBNBUS PhieuKhamBN = new PhieuKhamBNBUS();
        private QuanLiBenhNhanBUS BenhNhanBUS = new QuanLiBenhNhanBUS();

        #endregion


        #region Ham lien quan

        public bool KiemTraNhapLieuBNMoi()
        {
            if (txtThemBN_HoBN.Text == "" || txtThemBN_TenBN.Text == "" || txtThemBN_CMNDBN.Text == "" ||
                txtThemBN_SDTBN.Text == "")
                return false;
            return true;
        }

        private BenhNhan LayThongTinBenhNhan()
        {
            BenhNhan bn = new BenhNhan();
            bn.MaBenhNhan = txtThemBN_MaBenhNhan.Text;
            bn.MaBenhAn = txtThemBN_MaBenhAnBN.Text;
            bn.MaDoiTuong = cboThemBN_MaDoiTuongBN.SelectedValue.ToString();
            bn.MaPhieuKham = txtThemBN_MaPhieuBN.Text;
            bn.MaPhongKham = cboThemBN_PhongKham.SelectedValue.ToString();
            bn.MaBacSi = txtThemBN_MaBacSi.Text;
            bn.Ho = txtThemBN_HoBN.Text;
            bn.Ten = txtThemBN_TenBN.Text;
            bn.GioiTinh = cboThemBN_GioiTinhBN.SelectedValue.ToString();
            bn.CMND = Convert.ToInt32(txtThemBN_CMNDBN.Text);
            bn.NgheNghiep = txtThemBN_NgheNghiepBN.Text;
            bn.NgaySinh = Convert.ToDateTime(dtPickerThemBN_NgaySinhBN.Value.ToString());
            bn.DiaChi = txtThemBN_DiaChiBN.Text;
            bn.SDT = Convert.ToInt64(txtThemBN_SDTBN.Text);
            bn.YeuCauKham = rtxtThemBN_YeuCauKhamBN.Text;
            bn.NgayKham = Convert.ToDateTime(dtPickerThemBN_NgayKhamBN.Value.ToString());
            bn.GioKham = Convert.ToDateTime(dtPicker_GioKhamBN.Value.ToString());
            return bn;
        }

        //dong thoi lay thong tin benh nhan cho
        private BenhNhanCho LayThongTinBenhNhanCho()
        {
            BenhNhanCho bnc = new BenhNhanCho();
            bnc.MaBenhNhan = txtThemBN_MaBenhNhan.Text;
            bnc.MaBenhAn = txtThemBN_MaBenhAnBN.Text;
            bnc.MaDoiTuong = cboThemBN_MaDoiTuongBN.SelectedValue.ToString();
            bnc.MaPhieuKham = txtThemBN_MaPhieuBN.Text;
            bnc.MaPhongKham = cboThemBN_PhongKham.SelectedValue.ToString();
            bnc.MaBacSi = txtThemBN_MaBacSi.Text;
            bnc.Ho = txtThemBN_HoBN.Text;
            bnc.Ten = txtThemBN_TenBN.Text;
            bnc.GioiTinh = cboThemBN_GioiTinhBN.SelectedValue.ToString();
            bnc.CMND = Convert.ToInt32(txtThemBN_CMNDBN.Text);
            bnc.NgheNghiep = txtThemBN_NgheNghiepBN.Text;
            bnc.NgaySinh = Convert.ToDateTime(dtPickerThemBN_NgaySinhBN.Value.ToString());
            bnc.DiaChi = txtThemBN_DiaChiBN.Text;
            bnc.SDT = Convert.ToInt64(txtThemBN_SDTBN.Text);
            bnc.YeuCauKham = rtxtThemBN_YeuCauKhamBN.Text;
            bnc.NgayKham = Convert.ToDateTime(dtPickerThemBN_NgayKhamBN.Value.ToString());
            bnc.GioKham = Convert.ToDateTime(dtPicker_GioKhamBN.Value.ToString());
            return bnc;
        }            

        //ham lay thong tin benh an: Ma benh an, Ma Benh nhan
        private BenhAn LayThongTinSoBoBenhAn()
        {
            BenhAn ba = new BenhAn();
            ba.MaBenhAn = txtThemBN_MaBenhAnBN.Text;
            ba.MaBenhNhan = txtThemBN_MaBenhNhan.Text;
            return ba;
        }

        //Ham Lay Thong Tin phieu kham: MaPhieuKham, MaPhongKham, MaBenhNhan
        private PhieuKham LayThongTinSoBoPhieuKham()
        {
            PhieuKham pk = new PhieuKham();
            pk.MaPhieuKham = txtThemBN_MaPhieuBN.Text;
            pk.MaBenhNhan = txtThemBN_MaBenhNhan.Text;
            pk.MaPhongKham = cboThemBN_PhongKham.SelectedValue.ToString();

            return pk;
        }

        #endregion


        #region Cac Button trong Them benh nhan 

        //Button them benh nhan moi
        private void btnThemBenhNhan_Dong_Click(object sender, EventArgs e)
        {

            DialogResult result;
            result = MessageBox.Show("Hãy chắc chắn đã lưu thông tin Bệnh nhân !", "LƯU Ý", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                frmGiaoDienChung frm = new frmGiaoDienChung();
                this.Close();
            }
        }

        //button Them anh vao cho benh nhan
        private void btnThemBN_TaiAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.JPG)|*.JPG|GIF Files(*.GIF)|*.GIF";
            //luu anh vao va xuat anh ra
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                ThemHinhAnh.XoaHinhAnh(txtThemBN_MaBenhNhan.Text);
                ThemHinhAnh.StorePicture(dlg.FileName, txtThemBN_MaBenhNhan.Text);
                MessageBox.Show("Lưu ảnh bệnh nhân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmGiaoDienChung frm = new frmGiaoDienChung();
                frm.LoadAnhTuDB(txtThemBN_MaBenhNhan.Text, picThemBN_AnhBN);
            }
        }

        //button xoa anh benh nhan
        private void btnThemBN_XoaAnhBN_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xóa bệnh nhân : {" + txtThemBN_MaBenhNhan.Text + "}", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ThemHinhAnh.XoaHinhAnh(txtThemBN_MaBenhNhan.Text);
                picThemBN_AnhBN.Image = null;
            }
        }

        // Button nhap lai thong tin benh nhan
        private void btnThemBN_NhapMoi_Click(object sender, EventArgs e)
        {
            txtThemBN_MaBenhNhan.Text = ThemBNBUS.NextIDBenhNhan();
            txtThemBN_MaBenhAnBN.Text = BenhAnBN.NextIDBenhAn();
            txtThemBN_MaPhieuBN.Text = PhieuKhamBN.NextIDPhieuKham();

            txtThemBN_HoBN.Text = "";
            txtThemBN_TenBN.Text = "";
            txtThemBN_CMNDBN.Text = "";
            txtThemBN_SDTBN.Text = "";
            txtThemBN_NgheNghiepBN.Text = "";
            txtThemBN_DiaChiBN.Text = "";
            dtPicker_GioKhamBN.Text = "";
            txtThemBN_HoBSPhuTrach.Text = "";
            txtThemBN_TenBSPhuTrach.Text = "";
            txtThemBN_TenPhong.Text = "";
            rtxtThemBN_YeuCauKhamBN.Text = "";
          
        }

        //Button Luu Thong tin benh nha
        private void btnThemBN_Luu_Click(object sender, EventArgs e)
        {
            if (txtThemBN_MaBacSi.Text != "")
            {
                if (KiemTraNhapLieuBNMoi())
                {
                    BenhNhan bn = LayThongTinBenhNhan();
                    BenhNhanCho bnc = LayThongTinBenhNhanCho();
                    PhieuKham pk = LayThongTinSoBoPhieuKham();
                    BenhAn ba = LayThongTinSoBoBenhAn();
                    if (ThemBNBUS.KiemTraTrungMaBenhNhan(txtThemBN_MaBenhNhan.Text))
                    {
                        if (BenhNhanBUS.SuaDoiBenhNhan(bn))
                        {
                            BenhNhanBUS.SuaDoiBenhNhanCho(bnc);
                        }
                    }
                    else
                    {
                        ThemBNBUS.ThemBenhNhan(bn);
                        ThemBNBUS.ThemBenhNhanCho(bnc);
                        BenhAnBN.ThemBenhAnBN(ba);
                        PhieuKhamBN.ThemPhieuKhamBN(pk);
                        MessageBox.Show("Đã thêm bệnh nhân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show("Chưa nhập đủ thông tin bệnh nhân để lưu", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Phòng khám này chưa có bác sĩ phụ trách, hãy chọn lại phòng khám khác!", "Thông báo",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);            
        }

        #endregion


        #region Event cua form

        //load form
        private void frmThemBenhNhan_Load(object sender, EventArgs e)
        {
            txtThemBN_MaBenhNhan.Text = ThemBNBUS.NextIDBenhNhan();
            txtThemBN_MaBenhAnBN.Text = BenhAnBN.NextIDBenhAn();
            txtThemBN_MaPhieuBN.Text = PhieuKhamBN.NextIDPhieuKham();

            //Load Combobox Gioi Tinh
            cboThemBN_GioiTinhBN.DataSource = ThemBNBUS.DSGioiTinh();
            cboThemBN_GioiTinhBN.DisplayMember = "NameGT";
            cboThemBN_GioiTinhBN.ValueMember = "GioiTinh";

            //Load Combobox PhongKham
            cboThemBN_PhongKham.DataSource = PhongKhamBN.DSPhongKham();
            cboThemBN_PhongKham.DisplayMember = "NamePK";
            cboThemBN_PhongKham.ValueMember = "MaPhongKham";
            //Load combobox Doi Tuong
            cboThemBN_MaDoiTuongBN.DataSource = ThemBNBUS.DSDoiTuong();
            cboThemBN_MaDoiTuongBN.DisplayMember = "NameDT";
            cboThemBN_MaDoiTuongBN.ValueMember = "MaDoiTuong";
        }

        //event xuat ra ten bac si cua phong kham
        private void cboThemBN_PhongKham_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtThemBN_TenPhong.Text = "";
            txtThemBN_MaBacSi.Text = "";
            txtThemBN_HoBSPhuTrach.Text = "";
            txtThemBN_TenBSPhuTrach.Text = "";

            txtThemBN_TenPhong.DataBindings.Clear();
            txtThemBN_MaBacSi.DataBindings.Clear();
            txtThemBN_HoBSPhuTrach.DataBindings.Clear();
            txtThemBN_TenBSPhuTrach.DataBindings.Clear();

            txtThemBN_TenPhong.DataBindings.Add("text", PhongKhamBN.LayTenPhongTuMaPhong(cboThemBN_PhongKham.Text), "TenPhong");
            txtThemBN_MaBacSi.DataBindings.Add("text", ThemBNBUS.LayMaBSTuMaPhongKham(cboThemBN_PhongKham.Text), "MaBacSi");
            txtThemBN_HoBSPhuTrach.DataBindings.Add("text", ThemBNBUS.LayMaBSTuMaPhongKham(cboThemBN_PhongKham.Text), "Ho");
            txtThemBN_TenBSPhuTrach.DataBindings.Add("text", ThemBNBUS.LayMaBSTuMaPhongKham(cboThemBN_PhongKham.Text), "Ten");
        }

        //event tu dong dien ten doi tuong khi click combobox Ma Doi Tuong
        private void cboThemBN_MaDoiTuongBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtThemBN_TenDoiTuongBN.DataBindings.Clear();

            txtThemBN_TenDoiTuongBN.DataBindings.Add("text", ThemBNBUS.LayTenDoiTuongTuMaDT(cboThemBN_MaDoiTuongBN.Text), "TenDoiTuong");
        }

        //su kien cua textbox
        private void txtThemBN_CMNDBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtThemBN_SDTBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtThemBN_HoBN_TextChanged(object sender, EventArgs e)
        {
            if (txtThemBN_HoBN.Text == "")
                lblThemBN_WarnHoBN.Show();
            else
                lblThemBN_WarnHoBN.Hide();
        }

        private void txtThemBN_TenBN_TextChanged(object sender, EventArgs e)
        {
            if (txtThemBN_TenBN.Text == "")
                lblThemBN_WarnTenBN.Show();
            else
                lblThemBN_WarnTenBN.Hide();
        }

        private void txtThemBN_CMNDBN_TextChanged(object sender, EventArgs e)
        {
            if (txtThemBN_CMNDBN.Text == "")
                lblThemBN_WarnCMND.Show();
            else
                lblThemBN_WarnCMND.Hide();
        }

        private void txtThemBN_SDTBN_TextChanged(object sender, EventArgs e)
        {
            if (txtThemBN_SDTBN.Text == "")
                lblThemBN_WarnSDT.Show();
            else
                lblThemBN_WarnSDT.Hide();
        }


        #endregion 

        private void dtPickerThemBN_NgaySinhBN_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime kiemtra = dtPickerThemBN_NgaySinhBN.Value;
            if ((DateTime.Parse(now.ToString()) < DateTime.Parse(kiemtra.ToString())))
            {
                dtPickerThemBN_NgaySinhBN.Text = "";

            }
        }

        private void dtPickerThemBN_NgayKhamBN_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime kiemtra = dtPickerThemBN_NgayKhamBN.Value;
            if ((DateTime.Parse(now.ToString()) > DateTime.Parse(kiemtra.ToString())))
            {
                dtPickerThemBN_NgayKhamBN.Text = "";

            }
        }


       

        

      
      
        

      
   
    }
}
