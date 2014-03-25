using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLyBenhVien.BusinessLogic;
using QuanLyBenhVien.DataAccess;
using QuanLyBenhVien.BusinessObject;

namespace QuanLyBenhVien
{
    public partial class frmThemTaiKhoan : Form
    {
        public frmThemTaiKhoan()
        {
            InitializeComponent();
        }

        ThemTaiKhoanNguoiDungBUS ThemTaiKhoan = new ThemTaiKhoanNguoiDungBUS();
        ConnectData conData = new ConnectData();


        #region Them tai khoan cho BACSI

        #region Cac ham lien quan them tai khoan bac si

        //ham kich hoat cac textbox
        public void KichHoatSuaTaiKhoanBacSi(bool KiemTra)
        {
            pnlThongTinBacSi.Enabled = KiemTra;
        }

        //Load datagridview danh sach tai khoan bac si
        public void LoadDatagridBacSi()
        {
            dgViewDSBacSi.DataSource = ThemTaiKhoan.LoadDatagridDSBacSi();
        }
        
        //ham load thong tin de nhap lieu bac si
        public void NhapLieuBacSi()
        {
            //load comboxbox cho bac si
            cboMaBacSi.DataSource = ThemTaiKhoan.LoadTatCaMaBacSi();
            cboMaBacSi.DisplayMember = "DSTatCaMaBacSi";
            cboMaBacSi.ValueMember = "MaBacSi";


            //load combobox ma Nhom
            cboMaNhomBS.DataSource = ThemTaiKhoan.LoadMaNhomChoBacSi();
            cboMaNhomBS.DisplayMember = "STT";
            cboMaNhomBS.ValueMember = "MaNhom";
        }

        //Ham kiem tra nhap lieu truoc khi luu
        public bool KiemTraNhapLieuBacSi()
        {
            if (txtHoBacSi.Text == "" || txtMatKhauBS.Text == "" || txtTaiKhoanBS.Text == "" ||
                txtTenBacSi.Text == "" || txtTenNhomBS.Text == "")
                return false;
            else
                return true;
        }

        //Ham lay thong tin tai khoan bac si
        private ThemTaiKhoanNguoiDung LayThongTinBacSi()
        {
            ThemTaiKhoanNguoiDung ttk = new ThemTaiKhoanNguoiDung();

            ttk.MaBacSi = cboMaBacSi.SelectedValue.ToString();           
            ttk.HoBacSi = txtHoBacSi.Text;
            ttk.TenBacSi = txtTenBacSi.Text;
            ttk.MaNhomBS = cboMaNhomBS.SelectedValue.ToString();
            ttk.TenNhomBS = txtTenNhomBS.Text;
            ttk.UsernameBS = txtTaiKhoanBS.Text;
            ttk.PasswordBS = txtMatKhauBS.Text;

            return ttk;
        }

        //Load thong tin ng duung tu datagridview
        public void LoadThongTinBacSi(int dong)
        {
            txtHoBacSi.Text = dgViewDSBacSi.Rows[dong].Cells["HoBS"].Value.ToString();
            txtTenBacSi.Text = dgViewDSBacSi.Rows[dong].Cells["TenBS"].Value.ToString();
            txtTaiKhoanBS.Text = dgViewDSBacSi.Rows[dong].Cells["UsernameBS"].Value.ToString();
            txtMatKhauBS.Text = dgViewDSBacSi.Rows[dong].Cells["PasswordBS"].Value.ToString();
            cboMaBacSi.Text = dgViewDSBacSi.Rows[dong].Cells["MaNguoiDungBS"].Value.ToString();
            txtTenNhomBS.Text = dgViewDSBacSi.Rows[dong].Cells["TenNhomBS"].Value.ToString();
            cboMaNhomBS.Text = dgViewDSBacSi.Rows[dong].Cells["MaNhomBS"].Value.ToString();
        }

        #endregion


        #region Cac Button them tai khoan bac si


        //Button tim kiem ma bac si
        private void btnTimKiemMaBS_Click(object sender, EventArgs e)
        {
            int sodong = dgViewDSBacSi.RowCount - 1;
            for (int i = 0; i <= sodong; i++)
            {
                string mabenhnhan = dgViewDSBacSi.Rows[i].Cells["MaNguoiDungBS"].Value.ToString();
                string makiemtra = txtTimKiemMaBS.Text;

                if (makiemtra == mabenhnhan)
                {
                    lblWarnMaBS_Sai.Hide();
                    lblWarnMaBS_Dung.Show();

                    dgViewDSBacSi.Rows[i].Selected = true;

                    LoadThongTinBacSi(i);

                    MessageBox.Show("Tìm kiếm thành công bác sĩ : {" + txtTimKiemMaBS.Text + "} !", "Thông báo",
                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                else
                {
                    lblWarnMaBS_Sai.Show();
                    lblWarnMaBS_Dung.Hide();
                }
            }
        }

        //button xoa tai khoan bac si
        private void btnXoaBacSi_Click(object sender, EventArgs e)
        {
            if (ThemTaiKhoan.XoaTaiKhoanBacSi(txtTaiKhoanBS.Text))
            {
                MessageBox.Show("Xóa tài khoản bác sĩ thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                spTabThemBacSi_Click(sender, e);
            }
            else
                MessageBox.Show("Xóa tài khoản bác sĩ thất bại!", "Thông báo",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnThemBacSi_Click(object sender, EventArgs e)
        {
            cboMaBacSi.DataSource = ThemTaiKhoan.LoadBacSiChuaCoTK();
            cboMaBacSi.DisplayMember = "DSMaBacSi";
            cboMaBacSi.ValueMember = "MaBacSi";

            cboMaBacSi.Text = "";
            cboMaNhomBS.Text = "";
            txtMatKhauBS.Text = "";
            txtTaiKhoanBS.Text = "";
            txtTenNhomBS.Text = "";

            btnThemBacSi.Enabled = false;
            btnXoaBacSi.Enabled = false;
            btnSuaBacSi.Enabled = false;
            btnLuuBacSi.Enabled = true;

            KichHoatSuaTaiKhoanBacSi(true);
            dgViewDSBacSi.Enabled = true;
        }

        //button Huy them tai khoan
        private void btnHuyBacSi_Click(object sender, EventArgs e)
        {
            spTabThemBacSi_Click(sender, e);
        }

        //button luu thong tin tai khoan bac si
        private void btnLuuBacSi_Click(object sender, EventArgs e)
        {
            if (KiemTraNhapLieuBacSi())
            {
                ThemTaiKhoanNguoiDung ttk = LayThongTinBacSi();
                if (ThemTaiKhoan.KiemTraTrungMaNguoiDung(cboMaBacSi.Text))
                {
                    if (ThemTaiKhoan.SuaDoiTaiKhoanBacSi(ttk))
                    {
                        MessageBox.Show("Sửa đổi thông tin tài khoản bác sĩ thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        spTabThemBacSi_Click(sender, e);
                    }
                }
                else
                {
                    if (conData.KiemtraTonTaiTaiKhoan(txtTaiKhoanBS.Text))
                    {
                        MessageBox.Show("Tài khoản đã tồn tại, mời nhập lại tài khoản khác!", "Thông báo!",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTaiKhoanBS.Focus();
                        txtTaiKhoanBS.Text = "";
                    }
                    else
                    {                        
                        if (ThemTaiKhoan.ThemTaiKhoanBacSi(ttk))
                        {
                            LoadDatagridBacSi();
                            MessageBox.Show("Thêm tài khoản mới cho bác sĩ thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            spTabThemBacSi_Click(sender, e);
                        }                        
                    }                    
                }
            }
            else
                MessageBox.Show("Chưa nhập đủ thông tin tài khoản bác sĩ!", "Thông báo!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //sua doi thong tin tai khoan bac si
        private void btnSuaBacSi_Click(object sender, EventArgs e)
        {
            btnSuaBacSi.Enabled = false;
            btnThemBacSi.Enabled = false;
            btnXoaBacSi.Enabled = true;
            btnLuuBacSi.Enabled = true;

            KichHoatSuaTaiKhoanBacSi(true);
            dgViewDSBacSi.Enabled = true;
        }

        #endregion


        #region Cac Event lien quan


        private void cboMaBacSi_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHoBacSi.DataBindings.Clear();
            txtTenBacSi.DataBindings.Clear();
            txtChucVuBS.DataBindings.Clear();

            txtHoBacSi.DataBindings.Add("text", ThemTaiKhoan.LayDuLieuBacSiTheoMaBacSi(cboMaBacSi.Text), "Ho");
            txtTenBacSi.DataBindings.Add("text", ThemTaiKhoan.LayDuLieuBacSiTheoMaBacSi(cboMaBacSi.Text), "Ten");
            txtChucVuBS.DataBindings.Add("text", ThemTaiKhoan.LayDuLieuBacSiTheoMaBacSi(cboMaBacSi.Text), "ChucVu");
        }
        
        private void cboMaNhomBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenNhomBS.DataBindings.Clear();
            txtTenNhomBS.DataBindings.Add("text", ThemTaiKhoan.LoadTenNhomTuMaNhom(cboMaNhomBS.Text), "TenNhom");
        }

        private void dgViewDSBacSi_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            if (dong != -1)
            {
                LoadThongTinBacSi(dong);
            }
        }

        private void spTabThemBacSi_Click(object sender, EventArgs e)
        {         
            lblWarnMaBS_Dung.Hide();
            lblWarnMaBS_Sai.Hide();

            NhapLieuBacSi();
            KichHoatSuaTaiKhoanBacSi(false);

            btnXoaBacSi.Enabled = false;
            btnLuuBacSi.Enabled = false;
            btnThemBacSi.Enabled = true;
            btnSuaBacSi.Enabled = true;

            LoadDatagridBacSi();
            dgViewDSBacSi.Enabled = true;
        }

        private void frmThemTaiKhoan_Load(object sender, EventArgs e)
        {
            spTabThemBacSi_Click(sender, e);
        }

        #endregion


        #endregion


        #region Them tai Khoan cho YTA


        #region Cac ham lien quan trong Y Ta

        public void KichHoatSuaTaiKhoanYTa(bool KiemTra)
        {
            pnlThongTinYTa.Enabled = KiemTra;
        }

        public void LoadDatagridYTa()
        {
            dgViewDSYTa.DataSource = ThemTaiKhoan.LoadDatagridDSYTa();
        }

        public void NhapLieuYTa()
        {
            //load comboxbox cho bac si
            cboMaYTa.DataSource = ThemTaiKhoan.LoadTatCaMaYTa();
            cboMaYTa.DisplayMember = "DSTatCaMaYTa";
            cboMaYTa.ValueMember = "MaYTa";


            //load combobox ma Nhom
            cboMaNhomYT.DataSource = ThemTaiKhoan.LoadMaNhomChoYTa();
            cboMaNhomYT.DisplayMember = "STT";
            cboMaNhomYT.ValueMember = "MaNhom";
        }

        public bool KiemTraNhapLieuYTa()
        {
            if (txtHoYTa.Text == "" || txtMatKhauYT.Text == "" || txtTaiKhoanYT.Text == "" ||
                txtTenYTa.Text == "" || txtTenNhomYT.Text == "")
                return false;
            else
                return true;
        }

        private ThemTaiKhoanNguoiDung LayThongTinYTa()
        {
            ThemTaiKhoanNguoiDung ttk1 = new ThemTaiKhoanNguoiDung();

            ttk1.MaYTa = cboMaYTa.SelectedValue.ToString();
            ttk1.HoYTa = txtHoYTa.Text;
            ttk1.TenYTa = txtTenYTa.Text;
            ttk1.MaNhomYT = cboMaNhomYT.SelectedValue.ToString();
            ttk1.TenNhomYT = txtTenNhomYT.Text;
            ttk1.UsernameYT = txtTaiKhoanYT.Text;
            ttk1.PasswordYT = txtMatKhauYT.Text;      

            return ttk1;
        }

        public void LoadThongTinYTa(int dong)
        {
            txtHoYTa.Text = dgViewDSYTa.Rows[dong].Cells["HoYT"].Value.ToString();
            txtTenYTa.Text = dgViewDSYTa.Rows[dong].Cells["TenYT"].Value.ToString();
            txtTaiKhoanYT.Text = dgViewDSYTa.Rows[dong].Cells["UsernameYT"].Value.ToString();
            txtMatKhauYT.Text = dgViewDSYTa.Rows[dong].Cells["PasswordYT"].Value.ToString();
            cboMaYTa.Text = dgViewDSYTa.Rows[dong].Cells["MaNguoiDungYT"].Value.ToString();
            txtTenNhomYT.Text = dgViewDSYTa.Rows[dong].Cells["TenNhomYT"].Value.ToString();
            cboMaNhomYT.Text = dgViewDSYTa.Rows[dong].Cells["MaNhomYT"].Value.ToString();
        }

        #endregion


        #region Cac button trong y ta

        //button tim kiem ma y ta
        private void btnTKMaYTa_Click(object sender, EventArgs e)
        {
            int sodong = dgViewDSYTa.RowCount - 1;
            for (int i = 0; i <= sodong; i++)
            {
                string mabenhnhan = dgViewDSYTa.Rows[i].Cells["MaNguoiDungYT"].Value.ToString();
                string makiemtra = txtTKMaYTa.Text;

                if (makiemtra == mabenhnhan)
                {
                    lblWarn_MaYTaSai.Hide();
                    lblWarn_MaYTaDung.Show();

                    dgViewDSYTa.Rows[i].Selected = true;

                    LoadThongTinYTa(i);

                    MessageBox.Show("Tìm kiếm thành công Y tá : {" + txtTKMaYTa.Text + "} !", "Thông báo",
                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                else
                {
                    lblWarn_MaYTaSai.Show();
                    lblWarn_MaYTaDung.Hide();
                }
            }
        }

        //button them tai khoan moi cho y ta
        private void btnThemTaikhoanYT_Click(object sender, EventArgs e)
        {
            cboMaYTa.DataSource = ThemTaiKhoan.LoadYTaChuaCoTK();
            cboMaYTa.DisplayMember = "DSMaYTa";
            cboMaYTa.ValueMember = "MaYTa";

            cboMaYTa.Text = "";
            cboMaNhomYT.Text = "";
            txtMatKhauYT.Text = "";
            txtTaiKhoanYT.Text = "";
            txtTenNhomYT.Text = "";

            btnThemTaikhoanYT.Enabled = false;
            btnXoaTaiKhoanYT.Enabled = false;
            btnSuaDoiTKYTa.Enabled = false;
            btnLuuTKYTa.Enabled = true;

            KichHoatSuaTaiKhoanYTa(true);
            dgViewDSYTa.Enabled = true;
        }

        private void btnSuaDoiTKYTa_Click(object sender, EventArgs e)
        {
            btnSuaDoiTKYTa.Enabled = false;
            btnThemTaikhoanYT.Enabled = false;
            btnXoaTaiKhoanYT.Enabled = true;
            btnLuuTKYTa.Enabled = true;

            KichHoatSuaTaiKhoanYTa(true);
            dgViewDSYTa.Enabled = true;
        }

        private void btnXoaTaiKhoanYT_Click(object sender, EventArgs e)
        {
            if (ThemTaiKhoan.XoaTaiKhoanYTa(txtTaiKhoanYT.Text))
            {
                MessageBox.Show("Xóa tài khoản Y tá thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                spTabYTa_Click(sender, e);
            }
            else
                MessageBox.Show("Xóa tài khoản Y tá thất bại!", "Thông báo",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnLuuTKYTa_Click(object sender, EventArgs e)
        {
            if (KiemTraNhapLieuYTa())
            {
                ThemTaiKhoanNguoiDung ttk1 = LayThongTinYTa();
                if (ThemTaiKhoan.KiemTraTrungMaNguoiDung(cboMaYTa.Text))
                {
                    if (ThemTaiKhoan.SuaDoiTaiKhoanYTa(ttk1))
                    {
                        MessageBox.Show("Sửa đổi thông tin tài khoản Y tá thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        spTabYTa_Click(sender, e);
                    }
                }
                else
                {
                    if (conData.KiemtraTonTaiTaiKhoan(txtTaiKhoanYT.Text))
                    {
                        MessageBox.Show("Tài khoản đã tồn tại, mời nhập lại tài khoản khác!", "Thông báo!",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTaiKhoanYT.Focus();
                        txtTaiKhoanYT.Text = "";
                    }
                    else
                    {
                        if (ThemTaiKhoan.ThemTaiKhoanYTa(ttk1))
                        {
                            LoadDatagridYTa();
                            MessageBox.Show("Thêm tài khoản mới cho Y tá thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            spTabYTa_Click(sender, e);
                        }
                    }
                }
            }
            else
                MessageBox.Show("Chưa nhập đủ thông tin tài khoản Y tá!", "Thông báo!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnHuyYT_Click(object sender, EventArgs e)
        {
            spTabYTa_Click(sender, e);
        }

        #endregion


        #region Cac event lien quan trong y ta

        //event load thong tin 
        private void spTabYTa_Click(object sender, EventArgs e)
        {
            lblWarn_MaYTaDung.Hide();
            lblWarn_MaYTaSai.Hide();

            NhapLieuYTa();
            KichHoatSuaTaiKhoanYTa(false);

            btnXoaTaiKhoanYT.Enabled = false;
            btnLuuTKYTa.Enabled = false;
            btnThemTaikhoanYT.Enabled = true;
            btnSuaDoiTKYTa.Enabled = true;

            LoadDatagridYTa();
            dgViewDSYTa.Enabled = true;
        }

        private void cboMaYTa_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHoYTa.DataBindings.Clear();
            txtTenYTa.DataBindings.Clear();
            txtChucVuYT.DataBindings.Clear();

            txtHoYTa.DataBindings.Add("text", ThemTaiKhoan.LayDuLieuBacSiTheoMaYTa(cboMaYTa.Text), "Ho");
            txtTenYTa.DataBindings.Add("text", ThemTaiKhoan.LayDuLieuBacSiTheoMaYTa(cboMaYTa.Text), "Ten");
            txtChucVuYT.DataBindings.Add("text", ThemTaiKhoan.LayDuLieuBacSiTheoMaYTa(cboMaYTa.Text), "ChucVu");
        }

        private void cboMaNhomYT_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenNhomYT.DataBindings.Clear();
            txtTenNhomYT.DataBindings.Add("text", ThemTaiKhoan.LoadTenNhomTuMaNhom(cboMaNhomYT.Text), "TenNhom");
        }

        private void dgViewDSYTa_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            if (dong != -1)
            {
                LoadThongTinYTa(dong);
            }
        }


        #endregion

       
        #endregion


        #region Them tai khoan cho nhan vien


        #region Cac ham lien quan them tai khoan nhan vien

        //ham kich hoat cac textbox
        public void KichHoatSuaTaiKhoanNhanVien(bool KiemTra)
        {
            pnlThongTinNhanVien.Enabled = KiemTra;
        }

        //Load datagridview danh sach tai khoan bac si
        public void LoadDatagridNhanVien()
        {
            dgViewDSNhanVien.DataSource = ThemTaiKhoan.LoadDatagridDSNhanVien();
        }

        //ham load thong tin de nhap lieu bac si
        public void NhapLieuNhanVien()
        {
            //load comboxbox cho bac si
            cboMaNhanVien.DataSource = ThemTaiKhoan.LoadTatCaMaNhanVien();
            cboMaNhanVien.DisplayMember = "DSTatCaMaNhanVien";
            cboMaNhanVien.ValueMember = "MaNhanVien";


            //load combobox ma Nhom
            cboMaNhomNV.DataSource = ThemTaiKhoan.LoadMaNhomChoNhanVien();
            cboMaNhomNV.DisplayMember = "STT";
            cboMaNhomNV.ValueMember = "MaNhom";
        }

        //Ham kiem tra nhap lieu truoc khi luu
        public bool KiemTraNhapLieuNhanVien()
        {
            if (txtHoNhanVien.Text == "" || txtMatKhauNV.Text == "" || txtTaiKhoanNV.Text == "" ||
                txtTenNhanVien.Text == "" || txtTenNhomNV.Text == "")
                return false;
            else
                return true;
        }

        //Ham lay thong tin tai khoan bac si
        private ThemTaiKhoanNguoiDung LayThongTinNhanVien()
        {
            ThemTaiKhoanNguoiDung ttk2 = new ThemTaiKhoanNguoiDung();

            ttk2.MaNhanVien = cboMaNhanVien.SelectedValue.ToString();
            ttk2.HoNhanVien = txtHoNhanVien.Text;
            ttk2.TenNhanVien = txtTenNhanVien.Text;
            ttk2.MaNhomNV = cboMaNhomNV.SelectedValue.ToString();
            ttk2.TenNhomNV = txtTenNhomNV.Text;
            ttk2.UsernameNV = txtTaiKhoanNV.Text;
            ttk2.PasswordNV = txtMatKhauNV.Text;

            return ttk2;
        }

        //Load thong tin ng duung tu datagridview
        public void LoadThongTinNhanVien(int dong)
        {
            txtHoNhanVien.Text = dgViewDSNhanVien.Rows[dong].Cells["HoNV"].Value.ToString();
            txtTenNhanVien.Text = dgViewDSNhanVien.Rows[dong].Cells["TenNV"].Value.ToString();
            txtTaiKhoanNV.Text = dgViewDSNhanVien.Rows[dong].Cells["UsernameNV"].Value.ToString();
            txtMatKhauNV.Text = dgViewDSNhanVien.Rows[dong].Cells["PasswordNV"].Value.ToString();
            cboMaNhanVien.Text = dgViewDSNhanVien.Rows[dong].Cells["MaNguoiDungNV"].Value.ToString();
            txtTenNhomNV.Text = dgViewDSNhanVien.Rows[dong].Cells["TenNhomNV"].Value.ToString();
            cboMaNhomNV.Text = dgViewDSNhanVien.Rows[dong].Cells["MaNhomNV"].Value.ToString();
        }

        #endregion


        #region Cac button them nhan vien

        //tim kiem ma nhan vien
        private void btnTKMaNhanVien_Click(object sender, EventArgs e)
        {
            int sodong = dgViewDSNhanVien.RowCount - 1;
            for (int i = 0; i <= sodong; i++)
            {
                string mabenhnhan = dgViewDSNhanVien.Rows[i].Cells["MaNguoiDungNV"].Value.ToString();
                string makiemtra = txtTKMaNhanVien.Text;

                if (makiemtra == mabenhnhan)
                {
                    lblWarnMaNVSai.Hide();
                    lblWarnMaNVDung.Show();

                    dgViewDSNhanVien.Rows[i].Selected = true;

                    LoadThongTinNhanVien(i);

                    MessageBox.Show("Tìm kiếm thành công nhân viên : {" + txtTKMaNhanVien.Text + "} !", "Thông báo",
                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                else
                {
                    lblWarnMaNVSai.Show();
                    lblWarnMaNVDung.Hide();
                }
            }
        }

        //button them nhan vien
        private void btnThemNhanVien_Click(object sender, EventArgs e)
        {
            cboMaNhanVien.DataSource = ThemTaiKhoan.LoadNhanVienChuaCoTK();
            cboMaNhanVien.DisplayMember = "DSMaNhanVien";
            cboMaNhanVien.ValueMember = "MaNhanVien";

            cboMaNhanVien.Text = "";
            cboMaNhomNV.Text = "";
            txtMatKhauNV.Text = "";
            txtTaiKhoanNV.Text = "";
            txtTenNhomNV.Text = "";

            btnThemNhanVien.Enabled = false;
            btnXoaNhanVien.Enabled = false;
            btnSuaDoiNV.Enabled = false;
            btnLuuNV.Enabled = true;

            KichHoatSuaTaiKhoanNhanVien(true);
            dgViewDSNhanVien.Enabled = true;
        }

        //button xoa nhan vien
        private void btnXoaNhanVien_Click(object sender, EventArgs e)
        {
            if (ThemTaiKhoan.XoaTaiKhoanNhanVien(txtTaiKhoanNV.Text))
            {
                MessageBox.Show("Xóa tài khoản nhân viên thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                spTabNhanVien_Click(sender, e);
            }
            else
                MessageBox.Show("Xóa tài khoản nhân viên thất bại!", "Thông báo",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSuaDoiNV_Click(object sender, EventArgs e)
        {
            btnSuaDoiNV.Enabled = false;
            btnThemNhanVien.Enabled = false;
            btnXoaNhanVien.Enabled = true;
            btnLuuNV.Enabled = true;

            KichHoatSuaTaiKhoanNhanVien(true);
            dgViewDSNhanVien.Enabled = true;
        }

        //luu tai khoan moi
        private void btnLuuNV_Click(object sender, EventArgs e)
        {
            if (KiemTraNhapLieuNhanVien())
            {
                ThemTaiKhoanNguoiDung ttk3 = LayThongTinNhanVien();
                if (ThemTaiKhoan.KiemTraTrungMaNguoiDung(cboMaNhanVien.Text))
                {
                    if (ThemTaiKhoan.SuaDoiTaiKhoanNhanVien(ttk3))
                    {
                        MessageBox.Show("Sửa đổi thông tin tài khoản nhân viên thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        spTabNhanVien_Click(sender, e);
                    }
                }
                else
                {
                    if (conData.KiemtraTonTaiTaiKhoan(txtTaiKhoanNV.Text))
                    {
                        MessageBox.Show("Tài khoản đã tồn tại, mời nhập lại tài khoản khác!", "Thông báo!",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTaiKhoanNV.Focus();
                        txtTaiKhoanNV.Text = "";
                    }
                    else
                    {
                        if (ThemTaiKhoan.ThemTaiKhoanNhanVien(ttk3))
                        {
                            LoadDatagridNhanVien();
                            MessageBox.Show("Thêm tài khoản mới cho nhân viên thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            spTabNhanVien_Click(sender, e);
                        }
                    }
                }
            }
            else
                MessageBox.Show("Chưa nhập đủ thông tin tài khoản nhân viên!", "Thông báo!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //button huy sua soi
        private void btnHuyNV_Click(object sender, EventArgs e)
        {
            spTabNhanVien_Click(sender, e);
        }

        #endregion


        #region cac event lien quan

        private void cboMaNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHoNhanVien.DataBindings.Clear();
            txtTenNhanVien.DataBindings.Clear();
            txtChucVuNV.DataBindings.Clear();

            txtHoNhanVien.DataBindings.Add("text", ThemTaiKhoan.LayDuLieuBacSiTheoMaNhanVien(cboMaNhanVien.Text), "Ho");
            txtTenNhanVien.DataBindings.Add("text", ThemTaiKhoan.LayDuLieuBacSiTheoMaNhanVien(cboMaNhanVien.Text), "Ten");
            txtChucVuNV.DataBindings.Add("text", ThemTaiKhoan.LayDuLieuBacSiTheoMaNhanVien(cboMaNhanVien.Text), "ChucVu");
        }

        private void cboMaNhomNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenNhomNV.DataBindings.Clear();
            txtTenNhomNV.DataBindings.Add("text", ThemTaiKhoan.LoadTenNhomTuMaNhom(cboMaNhomNV.Text), "TenNhom");
        }

        private void dgViewDSNhanVien_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            if (dong != -1)
            {
                LoadThongTinNhanVien(dong);
            }
        }

        private void spTabNhanVien_Click(object sender, EventArgs e)
        {
            lblWarnMaNVDung.Hide();
            lblWarnMaNVSai.Hide();

            NhapLieuNhanVien();
            KichHoatSuaTaiKhoanNhanVien(false);

            btnXoaNhanVien.Enabled = false;
            btnLuuNV.Enabled = false;
            btnThemNhanVien.Enabled = true;
            btnSuaDoiNV.Enabled = true;

            LoadDatagridNhanVien();
            dgViewDSYTa.Enabled = true;
        }

        #endregion



        #endregion

  

    }
}
