using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using QuanLyBenhVien.BusinessLogic;
using QuanLyBenhVien.BusinessObject;
using System.Data.SqlClient;
using System.IO;
using QuanLyBenhVien.DataAccess;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;



namespace QuanLyBenhVien
{
    public partial class frmGiaoDienChung : Office2007RibbonForm
    {
        #region Khai Bao

        private GiaoDienChungBUS gdchungBUS = new GiaoDienChungBUS();
        private QuanLiBacSiBUS QuanLiBSBUS = new QuanLiBacSiBUS();
        private ThemBenhNhanBUS ThemBNBUS = new ThemBenhNhanBUS();
        private PhongKhamBUS PhongKham = new PhongKhamBUS();
        private PhieuKhamBNBUS PhieuKhamBUS = new PhieuKhamBNBUS();
        private QuanLiYTaBUS QuanLiYTBUS = new QuanLiYTaBUS();
        private QuanLiNhanVienBUS QuanLiNVBUS = new QuanLiNhanVienBUS();
        private QuanLiBenhNhanBUS QuanLiBNBUS = new QuanLiBenhNhanBUS();
        private BenhAnBNBUS BenhAnBN = new BenhAnBNBUS();
        private DonThuocBNBUS DonThuocBUS = new DonThuocBNBUS();
        private HinhAnhBUS ThemHinhAnh = new HinhAnhBUS();
        private XetNghiemBenhNhanBUS XetNghiemBNBUS = new XetNghiemBenhNhanBUS();
        private X_QuangBNBUS XQuangBN = new X_QuangBNBUS();
        private ConnectData connectData = new ConnectData();

        public bool logged = false;
        frmDangNhap dangnhap = null;
        frmGiaoDienChung Mainform;
        frmDoiMatKhau doimatkhau = null;
        UserDangNhapBUS userBUS = new UserDangNhapBUS();


        #endregion


        public frmGiaoDienChung()
        {
            InitializeComponent();
            dtpDenNgay1.Visible = false;
            dtpDenNgay2.Visible = false;
            dtpDenNgay3.Visible = false;
            dtpDenNgay4.Visible = false;

            dtpTuNgay1.Visible = false;
            dtpTuNgay2.Visible = false;
            dtpTuNgay3.Visible = false;
            dtpTuNgay4.Visible = false;

            lblDenNgay.Visible = false;
            lblTuNgay.Visible = false;
            lblDenNgay1.Visible = false;
            lblTuNgay1.Visible = false;

            btnKhoa.Enabled = false;
            btnTongSo.Enabled = false;

            cboKhoa.Visible = false;
            cboTongSo.Visible = false;
          
           // gdchungBUS.RestoreDuLieu();
        }

        #region Hàm đọc số tiền

        private static string Chu(string gNumber)
        {
            string result = "";
            switch (gNumber)
            {
                case "0":
                    result = "không";
                    break;
                case "1":
                    result = "một";
                    break;
                case "2":
                    result = "hai";
                    break;
                case "3":
                    result = "ba";
                    break;
                case "4":
                    result = "bốn";
                    break;
                case "5":
                    result = "năm";
                    break;
                case "6":
                    result = "sáu";
                    break;
                case "7":
                    result = "bảy";
                    break;
                case "8":
                    result = "tám";
                    break;
                case "9":
                    result = "chín";
                    break;
            }
            return result;
        }

        private static string Donvi(string so)
        {
            string Kdonvi = "";

            if (so.Equals("1"))
                Kdonvi = "";
            if (so.Equals("2"))
                Kdonvi = "nghìn";
            if (so.Equals("3"))
                Kdonvi = "triệu";
            if (so.Equals("4"))
                Kdonvi = "tỷ";
            if (so.Equals("5"))
                Kdonvi = "nghìn tỷ";
            if (so.Equals("6"))
                Kdonvi = "triệu tỷ";
            if (so.Equals("7"))
                Kdonvi = "tỷ tỷ";

            return Kdonvi;
        }

        private static string Tach(string tach3)
        {
            string Ktach = "";
            if (tach3.Equals("000"))
                return "";
            if (tach3.Length == 3)
            {
                string tr = tach3.Trim().Substring(0, 1).ToString().Trim();
                string ch = tach3.Trim().Substring(1, 1).ToString().Trim();
                string dv = tach3.Trim().Substring(2, 1).ToString().Trim();
                if (tr.Equals("0") && ch.Equals("0"))
                    Ktach = " không trăm lẻ " + Chu(dv.ToString().Trim()) + " ";
                if (!tr.Equals("0") && ch.Equals("0") && dv.Equals("0"))
                    Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm ";
                if (!tr.Equals("0") && ch.Equals("0") && !dv.Equals("0"))
                    Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm lẻ " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (tr.Equals("0") && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm mười " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("0"))
                    Ktach = " không trăm mười ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("5"))
                    Ktach = " không trăm mười lăm ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim() + " ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười " + Chu(dv.Trim()).Trim() + " ";

                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười lăm ";

            }


            return Ktach;

        }

        public static string So_chu(double gNum)
        {
            if (gNum == 0)
                return "Không đồng";

            string lso_chu = "";
            string tach_mod = "";
            string tach_conlai = "";
            double Num = Math.Round(gNum, 0);
            string gN = Convert.ToString(Num);
            int m = Convert.ToInt32(gN.Length / 3);
            int mod = gN.Length - m * 3;
            string dau = "[+]";

            // Dau [+ , - ]
            if (gNum < 0)
                dau = "[-]";
            dau = "";

            // Tach hang lon nhat
            if (mod.Equals(1))
                tach_mod = "00" + Convert.ToString(Num.ToString().Trim().Substring(0, 1)).Trim();
            if (mod.Equals(2))
                tach_mod = "0" + Convert.ToString(Num.ToString().Trim().Substring(0, 2)).Trim();
            if (mod.Equals(0))
                tach_mod = "000";
            // Tach hang con lai sau mod :
            if (Num.ToString().Length > 2)
                tach_conlai = Convert.ToString(Num.ToString().Trim().Substring(mod, Num.ToString().Length - mod)).Trim();

            ///don vi hang mod 
            int im = m + 1;
            if (mod > 0)
                lso_chu = Tach(tach_mod).ToString().Trim() + " " + Donvi(im.ToString().Trim());
            /// Tach 3 trong tach_conlai

            int i = m;
            int _m = m;
            int j = 1;
            string tach3 = "";
            string tach3_ = "";

            while (i > 0)
            {
                tach3 = tach_conlai.Trim().Substring(0, 3).Trim();
                tach3_ = tach3;
                lso_chu = lso_chu.Trim() + " " + Tach(tach3.Trim()).Trim();
                m = _m + 1 - j;
                if (!tach3_.Equals("000"))
                    lso_chu = lso_chu.Trim() + " " + Donvi(m.ToString().Trim()).Trim();
                tach_conlai = tach_conlai.Trim().Substring(3, tach_conlai.Trim().Length - 3);

                i = i - 1;
                j = j + 1;
            }
            if (lso_chu.Trim().Substring(0, 1).Equals("k"))
                lso_chu = lso_chu.Trim().Substring(10, lso_chu.Trim().Length - 10).Trim();
            if (lso_chu.Trim().Substring(0, 1).Equals("l"))
                lso_chu = lso_chu.Trim().Substring(2, lso_chu.Trim().Length - 2).Trim();
            if (lso_chu.Trim().Length > 0)
                lso_chu = dau.Trim() + " " + lso_chu.Trim().Substring(0, 1).Trim().ToUpper() + lso_chu.Trim().Substring(1, lso_chu.Trim().Length - 1).Trim() + " đồng chẵn.";

            return lso_chu.ToString().Trim();

        }

        #endregion


        #region RibbonItem Le Tan


        #region Cac ham lien quan cua Le Tan


        public void LoadDanhSachBenhNhanCho()
        {
            //lay ds benh nhan cho
            dgViewBenhNhanCho.DataSource = gdchungBUS.LayDanhSachBenhNhanCho();
        }

        //load du len len datagridview danh sach benh nhan va benh nhan cho
        public void LoadDanhSachBenhNhan()
        {
            //lay danh sach benh nhan trong datagridview Thong Tin Benh Nhan
            dgViewDanhSachBenhNhan.DataSource = gdchungBUS.LayDanhSachBenhNhan();            
        }

        //Ham dung de chuyen benh nhan tu benh Le Tan sang Kham Benh de dien thong tin kham benh
        public void ChuyenBenhNhanQuaKhamBenh()
        {
            if (txtLeTan_MaBenhNhan.Text != "")
            {
                rbItemKhamBenh.Select();
                cboKBenh_PhongKham.DataBindings.Clear();

                cboKBenh_PhongKham.DataBindings.Add("text", gdchungBUS.LayMaPhongKhamDeChuyenBenhNhan(txtLeTan_MaBenhNhan.Text), "MaPhongKham");
                //txtKBenh_MaBenhNhan.Text = txtLeTan_MaBenhNhan.Text;

                //tim kiem benh nhan tren trong datagridview va show thong tin len
                int sodong = dgViewKBenh_BenhNhanCho.RowCount - 1;
                for (int i = 0; i <= sodong; i++)
                {
                    string mabenhnhan = dgViewKBenh_BenhNhanCho.Rows[i].Cells["MaBenhNhanKBenh"].Value.ToString();
                    string makiemtra = txtKBenh_MaBenhNhan.Text;

                    if (makiemtra == mabenhnhan)
                    {
                        dgViewKBenh_BenhNhanCho.Rows[i].Selected = true;
                        LoadDuLieuBNKhamBenhTuDataGridView(i);
                        LoadAnhTuDB(makiemtra, picKBenh_BenhNhan);

                        break;

                    }
                }
            }
            else
                MessageBox.Show("Chưa chọn bệnh nhân để khám bệnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //ham load thong tin benh nhan khi click trong datagridview Danh sach benh nhan
        public void LoadThongTinBNTuDSBenhNhanLeTan(int dong)
        {
            txtLeTan_MaBenhNhan.Text = dgViewDanhSachBenhNhan.Rows[dong].Cells["MaBN"].Value.ToString();
            txtLeTan_MaBenhAn.Text = dgViewDanhSachBenhNhan.Rows[dong].Cells["MaBenhAnBN"].Value.ToString();
            txtLeTan_Ho.Text = dgViewDanhSachBenhNhan.Rows[dong].Cells["HoBN"].Value.ToString();
            txtLeTan_Ten.Text = dgViewDanhSachBenhNhan.Rows[dong].Cells["TenBN"].Value.ToString();
            txtLeTan_GioiTinh.Text = dgViewDanhSachBenhNhan.Rows[dong].Cells["GioiTinhBN"].Value.ToString();
            txtLeTan_DiaChi.Text = dgViewDanhSachBenhNhan.Rows[dong].Cells["DiaChiBN"].Value.ToString();
            dtPickerLeTan_NgaySinh.Text = dgViewDanhSachBenhNhan.Rows[dong].Cells["NgaySinhBN"].Value.ToString();
            txtLeTan_SDT.Text = dgViewDanhSachBenhNhan.Rows[dong].Cells["SDTBN"].Value.ToString();
            rtxtLeTan_YeuCauKham.Text = dgViewDanhSachBenhNhan.Rows[dong].Cells["YeuCauKhamBN"].Value.ToString();
        }


        #endregion


        #region Cac event trong Le Tan

        //event ribbonitem Le tan
        private void rbItemLeTan_Click(object sender, EventArgs e)
        {
            lblLeTan_WarnTKMaBNDung.Hide();
            lblLeTan_WarnTKMaBNSai.Hide();
            LoadDanhSachBenhNhanCho();
            LoadDanhSachBenhNhan();         
        }

        //event cua datagridview danh sach benh nhan
        private void dgViewDanhSachBenhNhan_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //su kien khi click vao vao mot o cua mot dong bat ki
            //thi se load thong tin cua benh nhan thuoc dong do len
            int dong = e.RowIndex;
            try
            {
                if (dong != -1)
                {
                    LoadThongTinBNTuDSBenhNhanLeTan(dong);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }      
        }

        //event cua datagridview danh sach benh nhan cho
        private void dgViewBenhNhanCho_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            try
            {
                if (dong != -1)
                {
                    txtLeTan_MaBenhNhan.Text = dgViewBenhNhanCho.Rows[dong].Cells["MaBenhNhanBNC"].Value.ToString();
                    txtLeTan_MaBenhAn.Text = dgViewBenhNhanCho.Rows[dong].Cells["MaBenhAnBNC"].Value.ToString();
                    txtLeTan_Ho.Text = dgViewBenhNhanCho.Rows[dong].Cells["HoBNC"].Value.ToString();
                    txtLeTan_Ten.Text = dgViewBenhNhanCho.Rows[dong].Cells["TenBNC"].Value.ToString();
                    txtLeTan_GioiTinh.Text = dgViewBenhNhanCho.Rows[dong].Cells["GioiTinhBNC"].Value.ToString();
                    dtPickerLeTan_NgaySinh.Text = dgViewBenhNhanCho.Rows[dong].Cells["NgaySinhBNC"].Value.ToString();
                    txtLeTan_SDT.Text = dgViewBenhNhanCho.Rows[dong].Cells["SDTBNC"].Value.ToString();
                    rtxtLeTan_YeuCauKham.Text = dgViewBenhNhanCho.Rows[dong].Cells["YeuCauKhamBNC"].Value.ToString();
                    txtLeTan_DiaChi.Text = dgViewBenhNhanCho.Rows[dong].Cells["DiaChiBNC"].Value.ToString();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }

        //event load anh theo mabenhnhan tu DanhSachBenhNhan
        private void dgViewDanhSachBenhNhan_Click(object sender, EventArgs e)
        {

            if (ThemHinhAnh.KiemTraMaHinhAnh(txtLeTan_MaBenhNhan.Text))
                LoadAnhTuDB(txtLeTan_MaBenhNhan.Text, picLeTan_BenhNhan);
            else
                picLeTan_BenhNhan.Image = null;
        }

        //event load anh theo mabenhnhan tu DanhSachBenhNhanCho
        private void dgViewBenhNhanCho_Click(object sender, EventArgs e)
        {

            if (ThemHinhAnh.KiemTraMaHinhAnh(txtLeTan_MaBenhNhan.Text))
                LoadAnhTuDB(txtLeTan_MaBenhNhan.Text, picLeTan_BenhNhan);
            else
                picLeTan_BenhNhan.Image = null;
        } 

        #endregion


        #region Cac button cua Le Tan

        //button Them benh nhan o Le Tan
        private void btnLeTan_ThemBenhNhan_Click(object sender, EventArgs e)
        {
            frmThemBenhNhan frmThem = new frmThemBenhNhan();
            frmThem.ShowDialog();
        }

        public delegate void delMaBNThuPhi(TextBox mabenhnhan);
        //Button Thu Phi cua Le Tan
        private void btnLeTan_ThuPhi_Click(object sender, EventArgs e)
        {
            frmThuPhi frmHoaDon = new frmThuPhi();
            delMaBNThuPhi delThuPhi = new delMaBNThuPhi(frmHoaDon.AddTextThuPhi);
            delThuPhi(this.txtLeTan_MaBenhNhan);
            frmHoaDon.ShowDialog();
        }      

        //button Xuat Vien cua Le Tan
        private void btnLeTan_XuatVien_Click(object sender, EventArgs e)
        {
            frmXuatVien xuatvien = new frmXuatVien();
            xuatvien.ShowDialog();       
        }    

        //button Refresh cua Le Tan
        private void btnRefreshData_Click(object sender, EventArgs e)
        {
            LoadDanhSachBenhNhanCho();
            LoadDanhSachBenhNhan();
        }

        //button chuyen qua kham benh
        private void btnLeTan_ChuyenBN_Click(object sender, EventArgs e)
        {
           
            rbItemKhamBenh_Click(sender, e);
            ChuyenBenhNhanQuaKhamBenh();
        }

        //button tim kiem benh nhan trong danh sach cua le tan
        private void btnLeTan_TKMaBenhNhan_Click(object sender, EventArgs e)
        {
            int sodong = dgViewDanhSachBenhNhan.RowCount - 1;
            for (int i = 0; i <= sodong; i++)
            {
                string mabenhnhan = dgViewDanhSachBenhNhan.Rows[i].Cells["MaBN"].Value.ToString();
                string makiemtra = txtLeTan_TKMaBenhNhan.Text;

                if (makiemtra == mabenhnhan)
                {
                    lblLeTan_WarnTKMaBNSai.Hide();
                    lblLeTan_WarnTKMaBNDung.Show();

                    dgViewDanhSachBenhNhan.Rows[i].Selected = true;

                    LoadThongTinBNTuDSBenhNhanLeTan(i);

                    LoadAnhTuDB(makiemtra, picLeTan_BenhNhan);

                    MessageBox.Show("Tìm kiếm thành công Bệnh nhân : {" + txtLeTan_TKMaBenhNhan.Text + "} !", "Thông báo",
                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                else
                {
                    lblLeTan_WarnTKMaBNSai.Show();
                    lblLeTan_WarnTKMaBNDung.Hide();
                }
            }
        }

        public delegate void dtlBNChuyenVien(TextBox text);
        private void btnLeTan_ChuyenVien_Click(object sender, EventArgs e)
        {
            frmChuyenVien chuyenvien = new frmChuyenVien();
            delMaBNNhapVien delchuyenvien = new delMaBNNhapVien(chuyenvien.addMaBN);
            delchuyenvien(this.txtLeTan_MaBenhNhan);
            chuyenvien.ShowDialog();
        }


        #endregion
            

        #endregion
        

        #region RibbonItem Quan Li Nhan Su - Bac Si


        #region Cac Ham lien quan trong Quan Li Nhan Su - Bac Si

        //Ham Lay ma bac si de luu anh
        private HinhAnh LayMaBacSi()
        {
            HinhAnh ha = new HinhAnh();
            ha.MaBacSi = txtQLNS_MaBacSi.Text;
            return ha;
        }

        //Ham lay thong tin bac si trong event cua datagridview
        private BacSi LayThongTinBacSi()
        {
            BacSi bs = new BacSi();
            bs.MaBacSi = txtQLNS_MaBacSi.Text;
            bs.MaKhoa = cboQLNS_MaKhoaBS.SelectedValue.ToString();
            bs.MaPhongBenh = cboQLNS_PhongBenhBS.SelectedValue.ToString();
            bs.MaPhongKham = cboQLNS_PhongKhamBS.SelectedValue.ToString();
            bs.Ho = txtQLNS_HoBS.Text;
            bs.Ten = txtQLNS_TenBS.Text;
            bs.NgaySinh = Convert.ToDateTime(dtPickerQLNS_NgaySinh.Value.ToString());
            bs.GioiTinh = cboQLNS_GioiTinhBS.SelectedValue.ToString();
            bs.CMND = Convert.ToInt32(txtQLNS_CMNDBS.Text);
            bs.Email = txtQLNS_EmailBS.Text;
            bs.SDT = Convert.ToInt64(txtQLNS_SDTBS.Text);
            bs.DiaChi = txtQLNS_DiaChiBS.Text;
            bs.NgayVaoLam = Convert.ToDateTime(dtPickerQLNS_NgayVaoLamBS.Value.ToString());
            bs.MaKhoa = cboQLNS_MaKhoaBS.SelectedValue.ToString();
            bs.ChucVu = txtQLNS_ChucVuBS.Text;
            bs.TrinhDo = cboQLNS_TrinhDoBS.Text;
            bs.HocHam = cboQLNS_HocHamBS.Text;
            bs.HocVi = cboQLNS_HocViBS.Text;

            return bs;
        }

        //Load du lieu cho combobox va them moi ma benh nhan
        public void NhapLieuBacSi()
        {
            txtQLNS_MaBacSi.Text = QuanLiBSBUS.NextIDBacSi();

            //Load Combobox Gioi Tinh cho Bac Si
            cboQLNS_GioiTinhBS.DataSource = QuanLiBSBUS.DSGioiTinh();
            cboQLNS_GioiTinhBS.DisplayMember = "NameGT";
            cboQLNS_GioiTinhBS.ValueMember = "GioiTinh";
            //load combobox MakHoa cho BacSi
            cboQLNS_MaKhoaBS.DataSource = QuanLiBSBUS.DSMaKhoa();
            cboQLNS_MaKhoaBS.DisplayMember = "NameKhoa";
            cboQLNS_MaKhoaBS.ValueMember = "MaKhoa";
            //Load Combobox Trinh Do Cho Bac Si
            cboQLNS_TrinhDoBS.DataSource = QuanLiBSBUS.DSTrinhDo();
            cboQLNS_TrinhDoBS.DisplayMember = "NameTrinhDo";
            cboQLNS_TrinhDoBS.ValueMember = "HocVan";
            //Load combobox Hoc Vi cho Bac Si
            cboQLNS_HocViBS.DataSource = QuanLiBSBUS.DSHocVi();
            cboQLNS_HocViBS.DisplayMember = "NameHocVi";
            cboQLNS_HocViBS.ValueMember = "HocVan";
            //Load Combobox Hoc Ham Cho Bac Si
            cboQLNS_HocHamBS.DataSource = QuanLiBSBUS.DSHocHam();
            cboQLNS_HocHamBS.DisplayMember = "NameHocHam";
            cboQLNS_HocHamBS.ValueMember = "HocVan";  
            //load danh sach phong kham
            cboQLNS_PhongKhamBS.DataSource = QuanLiBSBUS.DSTatCaPhongKham();
            cboQLNS_PhongKhamBS.DisplayMember = "MaPK";
            cboQLNS_PhongKhamBS.ValueMember = "MaPhongKham";
            //load danh sach phong benh
            cboQLNS_PhongBenhBS.DataSource = QuanLiBSBUS.DSTatCaPhongBenh();
            cboQLNS_PhongBenhBS.DisplayMember = "MaPB";
            cboQLNS_PhongBenhBS.ValueMember = "MaPhongBenh";
        }

        //load danh sach bac si len datagridview
        public void LoadDSBacSi()
        {
            dgViewQLNS_DSBacSi.DataSource = QuanLiBSBUS.DSBacSi();
        }

        //kich hoat chuc nang sua doi cua chuong trinh
        public void KichHoatSuaDoiBacSi(bool KiemTra)
        {
            txtQLNS_MaBacSi.Enabled = KiemTra;
            txtQLNS_TenBS.Enabled = KiemTra;
            txtQLNS_HoBS.Enabled = KiemTra;
            dtPickerQLNS_NgaySinh.Enabled = KiemTra;
            cboQLNS_GioiTinhBS.Enabled = KiemTra;
            txtQLNS_DiaChiBS.Enabled = KiemTra;
            txtQLNS_CMNDBS.Enabled = KiemTra;
            cboQLNS_TrinhDoBS.Enabled = KiemTra;
            txtQLNS_ChucVuBS.Enabled = KiemTra;
            cboQLNS_MaKhoaBS.Enabled = KiemTra;
            txtQLNS_TenKhoaBS.Enabled = KiemTra;
            txtQLNS_EmailBS.Enabled = KiemTra;
            txtQLNS_SDTBS.Enabled = KiemTra;
            dtPickerQLNS_NgayVaoLamBS.Enabled = KiemTra;
            cboQLNS_HocViBS.Enabled = KiemTra;
            cboQLNS_HocHamBS.Enabled = KiemTra;
            cboQLNS_PhongKhamBS.Enabled = KiemTra;
            txtQLNS_TenPhongKhamBS.Enabled = KiemTra;
            cboQLNS_PhongBenhBS.Enabled = KiemTra;
            txtQLNS_TenPhongBenhBS.Enabled = KiemTra;

            btnQLNS_XoaAnhBS.Enabled = KiemTra;
            btnQLNS_TaiAnhBS.Enabled = KiemTra;
        }

        public void LoadDuLieuBacSiTuDaTaGridView(int dong)
        {
            txtQLNS_MaBacSi.Text = dgViewQLNS_DSBacSi.Rows[dong].Cells["MaBacSi"].Value.ToString();
            cboQLNS_MaKhoaBS.SelectedValue = dgViewQLNS_DSBacSi.Rows[dong].Cells["MaKhoaBS"].Value.ToString();
            txtQLNS_TenKhoaBS.Text = dgViewQLNS_DSBacSi.Rows[dong].Cells["TenKhoaBS"].Value.ToString();
            txtQLNS_HoBS.Text = dgViewQLNS_DSBacSi.Rows[dong].Cells["HoBS"].Value.ToString();
            txtQLNS_TenBS.Text = dgViewQLNS_DSBacSi.Rows[dong].Cells["TenBS"].Value.ToString();
            dtPickerQLNS_NgaySinh.Text = dgViewQLNS_DSBacSi.Rows[dong].Cells["NgaySinhBS"].Value.ToString();
            cboQLNS_GioiTinhBS.SelectedValue = dgViewQLNS_DSBacSi.Rows[dong].Cells["GioiTinhBS"].Value.ToString();
            txtQLNS_DiaChiBS.Text = dgViewQLNS_DSBacSi.Rows[dong].Cells["DiaChiBS"].Value.ToString();
            txtQLNS_CMNDBS.Text = dgViewQLNS_DSBacSi.Rows[dong].Cells["CMNDBS"].Value.ToString();
            cboQLNS_TrinhDoBS.SelectedValue = dgViewQLNS_DSBacSi.Rows[dong].Cells["TrinhDoBS"].Value.ToString();
            txtQLNS_ChucVuBS.Text = dgViewQLNS_DSBacSi.Rows[dong].Cells["ChucVuBS"].Value.ToString();
            txtQLNS_EmailBS.Text = dgViewQLNS_DSBacSi.Rows[dong].Cells["EmailBS"].Value.ToString();
            txtQLNS_SDTBS.Text = dgViewQLNS_DSBacSi.Rows[dong].Cells["SDTBS"].Value.ToString();
            dtPickerQLNS_NgayVaoLamBS.Text = dgViewQLNS_DSBacSi.Rows[dong].Cells["NgayVaoLamBS"].Value.ToString();
            cboQLNS_HocViBS.SelectedValue = dgViewQLNS_DSBacSi.Rows[dong].Cells["HocViBS"].Value.ToString();
            cboQLNS_HocHamBS.SelectedValue = dgViewQLNS_DSBacSi.Rows[dong].Cells["HocHamBS"].Value.ToString();
            cboQLNS_PhongKhamBS.SelectedValue = dgViewQLNS_DSBacSi.Rows[dong].Cells["MaPhongKhamBS"].Value.ToString();
            cboQLNS_PhongBenhBS.SelectedValue = dgViewQLNS_DSBacSi.Rows[dong].Cells["MaPhongBenhBS"].Value.ToString();
        }

        //kiem tra truoc khi luu bac si
        public bool KiemTraTruocKhiLuuBacSi()
        {
            if (txtQLNS_HoBS.Text == "" || txtQLNS_TenBS.Text == "" || txtQLNS_CMNDBS.Text == ""
                || txtQLNS_TenKhoaBS.Text == "" || txtQLNS_SDTBS.Text == "")
                return false;
            else
                return true;
        }

        #endregion
       

        #region Button Quan Li Nhan Su - Bac si

        //button Them moi Bac si
        private void btnQLNS_ThemMoiBS_Click(object sender, EventArgs e)
        {
            KichHoatSuaDoiBacSi(true);

            btnQLNS_ThemMoiBS.Enabled = false;
            btnQLNS_SuaBS.Enabled = false;
            btnQLNS_XoaBS.Enabled = false;
            btnQLNS_LuuBS.Enabled = true;

            NhapLieuBacSi();

            txtQLNS_HoBS.Text = "";
            txtQLNS_TenBS.Text = "";
            txtQLNS_DiaChiBS.Text = "";
            txtQLNS_CMNDBS.Text = "";
            txtQLNS_ChucVuBS.Text = "";
            cboQLNS_MaKhoaBS.Text = "";
            txtQLNS_TenKhoaBS.Text = "";
            txtQLNS_EmailBS.Text = "";
            txtQLNS_SDTBS.Text = "";
            cboQLNS_GioiTinhBS.Text = "";
            picQLNS_BacSi.Image = null;

            //Load combobox danh sach cac phong kham co san
            cboQLNS_PhongKhamBS.DataSource = QuanLiBSBUS.DSPhongKhamBS();
            cboQLNS_PhongKhamBS.DisplayMember = "MaPKCoSan";
            cboQLNS_PhongKhamBS.ValueMember = "MaPhongKham";
            //load combobox danh sach cac phong benh co san
            cboQLNS_PhongBenhBS.DataSource = QuanLiBSBUS.DSPhongBenhBS();
            cboQLNS_PhongBenhBS.DisplayMember = "MaPBCoSan";
            cboQLNS_PhongBenhBS.ValueMember = "MaPhongBenh";
            dgViewQLNS_DSBacSi.Enabled = false;
            
        }       

        //button xoa bac si khoi danh sach
        private void btnQLNS_XoaBS_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xóa bác sĩ " + txtQLNS_HoBS.Text + " " + txtQLNS_TenBS.Text + " ra khỏi danh sách?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (QuanLiBSBUS.XoaBacSi(txtQLNS_MaBacSi.Text))
                    LoadDSBacSi();
                ThemHinhAnh.XoaHinhAnh(txtQLNS_MaBacSi.Text);
                picQLNS_BacSi.Image = null;
            }
            sptabBacSi_Click(sender, e);
        }

        //button sua doi thong tin bac si
        private void btnQLNS_SuaBS_Click(object sender, EventArgs e)
        {
            KichHoatSuaDoiBacSi(true);
            btnQLNS_LuuBS.Enabled = true;
            btnQLNS_XoaBS.Enabled = true;
            btnQLNS_SuaBS.Enabled = false;
            btnQLNS_ThemMoiBS.Enabled = false;
            dgViewQLNS_DSBacSi.Enabled = true;
        }

        //button luu bac si vao danh sach
        private void btnQLNS_LuuBS_Click(object sender, EventArgs e)
        {
            if (KiemTraTruocKhiLuuBacSi())
            {
                BacSi bs = LayThongTinBacSi();

                if (QuanLiBSBUS.KiemTraTrungMaBacSi(bs.MaBacSi))
                {
                    if (QuanLiBSBUS.SuaDoiBacSi(bs))
                        LoadDSBacSi();
                }
                else
                {
                    if (QuanLiBSBUS.ThemBacSi(bs))
                        LoadDSBacSi();
                }

                KichHoatSuaDoiBacSi(false);
                btnQLNS_XoaBS.Enabled = false;
                btnQLNS_LuuBS.Enabled = false;
                btnQLNS_SuaBS.Enabled = true;
                btnQLNS_ThemMoiBS.Enabled = true;
                dgViewQLNS_DSBacSi.Enabled = true;
                sptabBacSi_Click(sender, e);
            }
            
        }

        //Button tai anh len picturebox
        private void btnQLNS_TaiAnhBS_Click(object sender, EventArgs e)
        {           
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.JPG)|*.JPG|GIF Files(*.GIF)|*.GIF";
            //luu anh va xuat anh ra
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                ThemHinhAnh.XoaHinhAnh(txtQLNS_MaBacSi.Text);
                ThemHinhAnh.StorePicture(dlg.FileName, txtQLNS_MaBacSi.Text);
                MessageBox.Show("Lưu ảnh Bác sĩ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAnhTuDB(txtQLNS_MaBacSi.Text, picQLNS_BacSi);
            }
        }

        //button tim kiem ma bac si
        private void btnQLNS_TimKiemBS_Click(object sender, EventArgs e)
        {
            int sodong = dgViewQLNS_DSBacSi.RowCount - 1;

            for (int i = 0; i <= sodong; i++)
            {
                string mabacsi = dgViewQLNS_DSBacSi.Rows[i].Cells["MaBacSi"].Value.ToString();
                string makiemtra = txtQLNS_TKMaBacSi.Text;
                if (makiemtra == mabacsi)
                {
                    lblQLNS_WarnTKMaBSSai.Hide();
                    lblQLNS_WarnTKMaBSDung.Show();

                    dgViewQLNS_DSBacSi.Rows[i].Selected = true;

                    //load thong tin bac si tim kiem dc
                    LoadDuLieuBacSiTuDaTaGridView(i);

                    LoadAnhTuDB(makiemtra, picQLNS_BacSi);

                    MessageBox.Show("Tìm kiếm thành công Bác sĩ : {" + txtQLNS_MaBacSi.Text + "} !", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;
                }
                else
                {
                    lblQLNS_WarnTKMaBSSai.Show();
                    lblQLNS_WarnTKMaBSDung.Hide();
                }
            }

        }

        //button xoa anh bac si
        private void btnQLNS_XoaAnhBS_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xóa ảnh bác sĩ : {" + txtQLNS_MaBacSi.Text + "}", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ThemHinhAnh.XoaHinhAnh(txtQLNS_MaBacSi.Text);
                picQLNS_BacSi.Image = null;
            }
        }

        //button huy cua bac si
        private void btnQLNS_HuyThaoTacBS_Click(object sender, EventArgs e)
        {
            sptabBacSi_Click(sender, e);
        }

        #endregion


        #region Cac Event trong Quan Li Nhan Su - Bac Si

        //Event load lai ribbonitem Quan Li Nhan su
        private void rbItemQuanLiNhanSu_Click(object sender, EventArgs e)
        {
            lblQLNS_WarnTKMaBSDung.Hide();
            lblQLNS_WarnTKMaBSSai.Hide();

            NhapLieuBacSi();
            KichHoatSuaDoiBacSi(false);
            btnQLNS_XoaBS.Enabled = false;
            btnQLNS_LuuBS.Enabled = false;
            btnQLNS_SuaBS.Enabled = true;
            btnQLNS_ThemMoiBS.Enabled = true;
            LoadDSBacSi();
            dgViewQLNS_DSBacSi.Enabled = true;
            
        }

        //Event load lai tab Supertab
        private void sptabBacSi_Click(object sender, EventArgs e)
        {
            lblQLNS_WarnTKMaBSSai.Hide();
            lblQLNS_WarnTKMaBSDung.Hide();

            NhapLieuBacSi();
            KichHoatSuaDoiBacSi(false);
            btnQLNS_XoaBS.Enabled = false;
            btnQLNS_LuuBS.Enabled = false;
            btnQLNS_SuaBS.Enabled = true;
            btnQLNS_ThemMoiBS.Enabled = true;
            LoadDSBacSi();
            dgViewQLNS_DSBacSi.Enabled = true;
        }

        private void cboQLNS_PhongKhamBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQLNS_TenPhongKhamBS.DataBindings.Clear();
            txtQLNS_TenPhongKhamBS.DataBindings.Add("text", QuanLiBSBUS.LayTenPKTuMaPK(cboQLNS_PhongKhamBS.Text), "TenPhong");
        }

        private void cboQLNS_PhongBenhBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQLNS_TenPhongBenhBS.DataBindings.Clear();
            txtQLNS_TenPhongBenhBS.DataBindings.Add("text", QuanLiBSBUS.LayTenPBTuMaPB(cboQLNS_PhongBenhBS.Text), "TenPhong");
        }

        //event combobox
        private void cboQLNS_MaKhoaBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQLNS_TenKhoaBS.DataBindings.Clear();
            txtQLNS_TenKhoaBS.DataBindings.Add("text", QuanLiBSBUS.LayTenKhoaTuMaKhoa(cboQLNS_MaKhoaBS.Text), "TenKhoa");
        }

        //event datagridview
        private void dgViewDSBacSi_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            try
            {
                if (dong != -1)
                {
                    LoadDuLieuBacSiTuDaTaGridView(dong);                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        //event load anh tu dong cho bac si
        private void dgViewQLNS_DSBacSi_Click(object sender, EventArgs e)
        {
            if (ThemHinhAnh.KiemTraMaHinhAnh(txtQLNS_MaBacSi.Text))
                LoadAnhTuDB(txtQLNS_MaBacSi.Text, picQLNS_BacSi);
            else
                picQLNS_BacSi.Image = null;

        }

        //ngay vao lam bac si
        private void dtPickerQLNS_NgayVaoLamBS_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime kiemtra = dtPickerQLNS_NgayVaoLamBS.Value;
            if ((DateTime.Parse(now.ToString()) < DateTime.Parse(kiemtra.ToString())))
            {
                dtPickerQLNS_NgayVaoLamBS.Text = "";

            }
        }

        //ngay sinh bac si
        private void dtPickerQLNS_NgaySinh_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime kiemtra = dtPickerQLNS_NgaySinh.Value;
            if ((DateTime.Parse(now.ToString()) < DateTime.Parse(kiemtra.ToString())))
            {
                dtPickerQLNS_NgaySinh.Text = "";

            }
        }

        //hoc dai hoc moi co them dat hoc ham, hoc vi
        private void cboQLNS_TrinhDoBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboQLNS_TrinhDoBS.Text == "Đại học")
            {

                cboQLNS_HocHamBS.Enabled = true;
                cboQLNS_HocViBS.Enabled = true;
                cboQLNS_HocHamBS.SelectedValue = "";
                cboQLNS_HocViBS.SelectedValue = "";
            }
            else
            {
                cboQLNS_HocViBS.SelectedValue = "";
                cboQLNS_HocHamBS.SelectedValue = "";
                cboQLNS_HocHamBS.Enabled = false;
                cboQLNS_HocViBS.Enabled = false;
            }
        }

        //tien si moi co the dat danh hieu hoc vi 
        private void cboQLNS_HocViBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboQLNS_HocViBS.Text == "Tiến sĩ")
            {
                cboQLNS_HocHamBS.SelectedValue = "";
                cboQLNS_HocHamBS.Enabled = true;
            }
            else
            {
                cboQLNS_HocHamBS.SelectedValue = "";
                cboQLNS_HocHamBS.Enabled = false;
            }
        }

        #region Event cua textbox


        //Event textbox
        private void txtQLNS_NgaySinhBS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }    

        private void txtQLNS_CMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtQLNS_SDTBS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtQLNS_HoBS_TextChanged(object sender, EventArgs e)
        {
            if (txtQLNS_HoBS.Text == "")
                lblQLNS_WarnHoBS.Show();
            else
                lblQLNS_WarnHoBS.Hide();
        }

        private void txtQLNS_TenBS_TextChanged(object sender, EventArgs e)
        {
            if (txtQLNS_TenBS.Text == "")
                lblQLNS_WarnTenBS.Show();
            else
                lblQLNS_WarnTenBS.Hide();
        }

        private void txtQLNS_CMNDBS_TextChanged(object sender, EventArgs e)
        {
            if (txtQLNS_CMNDBS.Text == "")
                lblQLNS_WarnCMNDBS.Show();
            else
                lblQLNS_WarnCMNDBS.Hide();
        }

        private void txtQLNS_SDTBS_TextChanged(object sender, EventArgs e)
        {
            if (txtQLNS_SDTBS.Text == "")
                lblQLNS_WarnSDTBS.Show();
            else
                lblQLNS_WarnSDTBS.Hide();
        }

        private void txtQLNS_TenKhoaBS_TextChanged(object sender, EventArgs e)
        {
            if (txtQLNS_TenKhoaBS.Text == "")
                lblQLNS_WarnTenKhoaBS.Show();
            else
                lblQLNS_WarnTenKhoaBS.Hide();
        }

        #endregion      

        #endregion


        #endregion      


        #region RibbonItem Quan Li Nhan Su - Y Ta


        #region Cac Ham Lien Quan - Y Ta

        public void NhapLieuYTa()
        {
            txtQLNS_MaYTa.Text = QuanLiYTBUS.NextIDYTa();

            //load combobox Gioi Tinh cho Y Ta
            cboQLNS_GioiTinhYT.DataSource = QuanLiYTBUS.DSGioiTinh();
            cboQLNS_GioiTinhYT.DisplayMember = "NameGTYT";
            cboQLNS_GioiTinhYT.ValueMember = "GioiTinh";

            //load combobox Trinh Do cho Y Ta
            cboQLNS_TrinhDoYT.DataSource = QuanLiYTBUS.DSTrinhDo();
            cboQLNS_TrinhDoYT.DisplayMember = "NameTrinhDoYT";
            cboQLNS_TrinhDoYT.ValueMember = "HocVan";

            //load combobox MaKhoa
            cboQLNS_MaKhoaYT.DataSource = QuanLiYTBUS.DSMaKhoa();
            cboQLNS_MaKhoaYT.DisplayMember = "NameMaKhoaYT";
            cboQLNS_MaKhoaYT.ValueMember = "MaKhoa";
            //Load combobox PhongKham
            cboQLNS_PhongKhamYT.DataSource = QuanLiYTBUS.DSTatCaPhongKham();
            cboQLNS_PhongKhamYT.DisplayMember = "MaPKYT";
            cboQLNS_PhongKhamYT.ValueMember = "MaPhongKham";
            //load danh sach phong benh 
            cboQLNS_PhongBenhYT.DataSource = QuanLiYTBUS.DSPhongBenhYT();
            cboQLNS_PhongBenhYT.DisplayMember = "MaPBYT";
            cboQLNS_PhongBenhYT.ValueMember = "MaPhongBenh";
        }

        //lay thong tin Y ta
        private YTa LayThongTinYTa()
        {
            YTa yt = new YTa();

            yt.MaYTa = txtQLNS_MaYTa.Text;
            yt.Ho = txtQLNS_HoYT.Text;
            yt.Ten = txtQLNS_TenYT.Text;
            yt.NgaySinh = Convert.ToDateTime(dtPicker_NgaySinhYT.Value.ToString());
            yt.GioiTinh = cboQLNS_GioiTinhYT.SelectedValue.ToString();
            yt.DiaChi = txtQLNS_DiaChiYT.Text;
            yt.CMND = Convert.ToInt32(txtQLNS_CMNDYT.Text);
            yt.TrinhDo = cboQLNS_TrinhDoYT.SelectedValue.ToString();
            yt.ChucVu = txtQLNS_ChucVuYT.Text;
            yt.MaKhoa = cboQLNS_MaKhoaYT.SelectedValue.ToString();
            yt.Email = txtQLNS_EmailYT.Text;
            yt.SDT = Convert.ToInt64(txtQLNS_SDTYT.Text);
            yt.NgayVaoLam = Convert.ToDateTime(dtPickerNgayVaoLamYT.Value.ToString());
            yt.MaPhongBenh = cboQLNS_PhongBenhYT.SelectedValue.ToString();
            yt.MaPhongKham = cboQLNS_PhongKhamYT.SelectedValue.ToString();

            return yt;
        }

        //ham kick hoat khi sua doi hay them moi y ta
        public void KichHoatSuaDoiYTa(bool KiemTra)
        {
            txtQLNS_MaYTa.Enabled = KiemTra;
            txtQLNS_HoYT.Enabled = KiemTra;
            txtQLNS_TenYT.Enabled = KiemTra;
            dtPicker_NgaySinhYT.Enabled = KiemTra;
            cboQLNS_GioiTinhYT.Enabled = KiemTra;
            txtQLNS_DiaChiYT.Enabled = KiemTra;
            txtQLNS_CMNDYT.Enabled = KiemTra;
            cboQLNS_TrinhDoYT.Enabled = KiemTra;
            cboQLNS_MaKhoaYT.Enabled = KiemTra;
            txtQLNS_TenKhoaYT.Enabled = KiemTra;
            txtQLNS_EmailYT.Enabled = KiemTra;
            txtQLNS_SDTYT.Enabled = KiemTra;
            dtPickerNgayVaoLamYT.Enabled = KiemTra;
            cboQLNS_PhongKhamYT.Enabled = KiemTra;
            txtQLNS_TenPhongKhamYT.Enabled = KiemTra;
            cboQLNS_PhongBenhYT.Enabled = KiemTra;
            txtQLNS_TenPhongBenhYT.Enabled = KiemTra;
            txtQLNS_ChucVuYT.Enabled = KiemTra;
            btnQLNS_XoaAnhYT.Enabled = KiemTra;
            btnQLNS_TaiAnhYTa.Enabled = KiemTra;
        }

        //load danh sach y ta len datagridview
        public void LoadDSYTa()
        {
            dgViewQLNS_DanhSachYT.DataSource = QuanLiYTBUS.DSYTa();
        }      

        //Ham Load du lieu tu datagridview cua y ta len form
        public void LoadDuLieuYTaTuDataGridView(int dong)
        {
            txtQLNS_MaYTa.Text = dgViewQLNS_DanhSachYT.Rows[dong].Cells["MaYTa"].Value.ToString();
            txtQLNS_HoYT.Text = dgViewQLNS_DanhSachYT.Rows[dong].Cells["HoYT"].Value.ToString();
            txtQLNS_TenYT.Text = dgViewQLNS_DanhSachYT.Rows[dong].Cells["TenYT"].Value.ToString();
            dtPicker_NgaySinhYT.Text = dgViewQLNS_DanhSachYT.Rows[dong].Cells["NgaySinhYT"].Value.ToString();
            cboQLNS_GioiTinhYT.Text = dgViewQLNS_DanhSachYT.Rows[dong].Cells["GioiTinhYT"].Value.ToString();
            txtQLNS_DiaChiYT.Text = dgViewQLNS_DanhSachYT.Rows[dong].Cells["DiaChiYT"].Value.ToString();
            txtQLNS_CMNDYT.Text = dgViewQLNS_DanhSachYT.Rows[dong].Cells["CMNDYT"].Value.ToString();
            cboQLNS_TrinhDoYT.Text = dgViewQLNS_DanhSachYT.Rows[dong].Cells["TrinhDoYT"].Value.ToString();
            txtQLNS_ChucVuYT.Text = dgViewQLNS_DanhSachYT.Rows[dong].Cells["ChucVuYT"].Value.ToString();
            cboQLNS_MaKhoaYT.Text = dgViewQLNS_DanhSachYT.Rows[dong].Cells["MaKhoaYT"].Value.ToString();
            txtQLNS_EmailYT.Text = dgViewQLNS_DanhSachYT.Rows[dong].Cells["EmailYT"].Value.ToString();
            txtQLNS_SDTYT.Text = dgViewQLNS_DanhSachYT.Rows[dong].Cells["SDTYT"].Value.ToString();
            dtPickerNgayVaoLamYT.Text = dgViewQLNS_DanhSachYT.Rows[dong].Cells["NgayVaoLamYT"].Value.ToString();
            cboQLNS_PhongKhamYT.Text = dgViewQLNS_DanhSachYT.Rows[dong].Cells["MaPhongKhamYT"].Value.ToString();
            cboQLNS_PhongBenhYT.Text = dgViewQLNS_DanhSachYT.Rows[dong].Cells["MaPhongBenhYT"].Value.ToString();
        }

        //Ham kiem tra thong tin y ta truoc khi luu
        public bool KiemTraDuLieuYTaTruocKhiLuu()
        {
            if (txtQLNS_HoYT.Text == "" || txtQLNS_TenYT.Text == "" || txtQLNS_SDTYT.Text == ""
                || txtQLNS_CMNDYT.Text == "" || txtQLNS_TenKhoaYT.Text == "")
                return false;
            else
                return true;
        }

        #endregion


        #region Cac Button trong Y Ta
       

        //button Them Y Ta moi
        private void btnQLNS_ThemYTa_Click(object sender, EventArgs e)
        {
            KichHoatSuaDoiYTa(true);

            btnQLNS_LuuYTa.Enabled = true;
            btnQLNS_ThemYTa.Enabled = false;
            btnQLNS_XoaYTa.Enabled = false;
            btnQLNS_SuaDoiYTa.Enabled = false;

            NhapLieuYTa();

            txtQLNS_HoYT.Text = "";
            txtQLNS_TenYT.Text = "";
            txtQLNS_DiaChiYT.Text = "";
            txtQLNS_CMNDYT.Text = "";
            cboQLNS_TrinhDoYT.Text = "";
            txtQLNS_ChucVuYT.Text = "";
            cboQLNS_MaKhoaYT.Text = "";
            txtQLNS_TenKhoaYT.Text = "";
            txtQLNS_EmailYT.Text = "";
            txtQLNS_SDTYT.Text = "";
            cboQLNS_GioiTinhYT.Text = "";

            //Load Danh sach phong kham co san cho y ta
            cboQLNS_PhongKhamYT.DataSource = QuanLiYTBUS.DSPhongKhamYT();
            cboQLNS_PhongKhamYT.DisplayMember = "MaPKYT";
            cboQLNS_PhongKhamYT.ValueMember = "MaPhongKham";
            //load phong benh
            cboQLNS_PhongBenhYT.DataSource = QuanLiYTBUS.DSPhongBenhYT();
            cboQLNS_PhongBenhYT.DisplayMember = "MaPhongBenhYT";
            cboQLNS_PhongBenhYT.ValueMember = "MaPhongBenh";

            dgViewQLNS_DanhSachYT.Enabled = false;

        }

        //Button sua doi thong tin cua y ta
        private void btnQLNS_SuaDoiYTa_Click(object sender, EventArgs e)
        {
            KichHoatSuaDoiYTa(true);
            btnQLNS_ThemYTa.Enabled = false;
            btnQLNS_SuaDoiYTa.Enabled = false;
            btnQLNS_LuuYTa.Enabled = true;
            btnQLNS_XoaYTa.Enabled = true;
            dgViewQLNS_DanhSachYT.Enabled = true;
        }

        //Button xoa y ta trong danh sach
        private void btnQLNS_XoaYTa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xóa y tá " + txtQLNS_HoYT.Text + " " + txtQLNS_TenYT.Text + " ra khỏi danh sách?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (QuanLiYTBUS.XoaYTa(txtQLNS_MaYTa.Text))
                    LoadDSYTa();
            }
            sptabYTa_Click(sender, e);
        }

        //Button Luu thong tin cua Y Ta
        private void btnQLNS_LuuYTa_Click(object sender, EventArgs e)
        {
            if (KiemTraDuLieuYTaTruocKhiLuu())
            {
                YTa yt = LayThongTinYTa();
                if (QuanLiYTBUS.KiemTraTrungMaYTa(yt.MaYTa))
                {
                    if (QuanLiYTBUS.SuaDoiYTa(yt))
                        LoadDSYTa();
                }
                else
                {
                    if (QuanLiYTBUS.ThemYTa(yt))
                        LoadDSYTa();
                }

                KichHoatSuaDoiYTa(false);
                btnQLNS_LuuYTa.Enabled = false;
                btnQLNS_XoaYTa.Enabled = false;
                btnQLNS_SuaDoiYTa.Enabled = true;
                btnQLNS_ThemYTa.Enabled = true;                
                dgViewQLNS_DanhSachYT.Enabled = true;
            }           
        }

        //button tai anh Y ta
        private void btnQLNS_TaiAnhYTa_Click(object sender, EventArgs e)
        {
          
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.JPG)|*.JPG|GIF Files(*.GIF)|*.GIF";
            //luu anh va xuat ra
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                ThemHinhAnh.XoaHinhAnh(txtQLNS_MaYTa.Text);
                ThemHinhAnh.StorePicture(dlg.FileName, txtQLNS_MaYTa.Text);
                MessageBox.Show("Lưu ảnh Y tá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAnhTuDB(txtQLNS_MaYTa.Text, picQLNS_YTa);
            }
        }

        //Button tim kiem ma y ta 
        private void btnQLNS_TKMaYTa_Click(object sender, EventArgs e)
        {
            int sodong = dgViewQLNS_DanhSachYT.RowCount - 1;

            for (int i = 0; i <= sodong; i++)
            {
                string mayta = dgViewQLNS_DanhSachYT.Rows[i].Cells["MaYTa"].Value.ToString();
                string makiemtra = txtQLNS_TKMaYTa.Text;
                if (makiemtra == mayta)
                {
                    lblQLNS_WarnTKYTaDung.Show();
                    lblQLNS_WarnTKYTaSai.Hide();

                    dgViewQLNS_DanhSachYT.Rows[i].Selected = true;
                    LoadDuLieuYTaTuDataGridView(i);
                    LoadAnhTuDB(makiemtra, picQLNS_YTa);

                    MessageBox.Show("Tìm kiếm thành công Y tá : {" + txtQLNS_TKMaYTa.Text + "} !", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;
                }
                else
                {
                    lblQLNS_WarnTKYTaDung.Hide();
                    lblQLNS_WarnTKYTaSai.Show();
                }
            }
        }

        //button xoa anh y ta 
        private void btnQLNS_XoaAnhYT_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xóa ảnh y tá : {" + txtQLNS_MaYTa.Text + "}", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ThemHinhAnh.XoaHinhAnh(txtQLNS_MaYTa.Text);
                picQLNS_YTa.Image = null;
            }
        }

        //button huy cua y ta
        private void btnQLNS_HuyThaoTacYT_Click(object sender, EventArgs e)
        {
            sptabYTa_Click(sender, e);
        }


        #endregion


        #region Cac Event cua form Y Ta


        //Load thong tin khi click vao Supertab Y Ta
        private void sptabYTa_Click(object sender, EventArgs e)
        {
            lblQLNS_WarnTKYTaDung.Hide();
            lblQLNS_WarnTKYTaSai.Hide();

            NhapLieuYTa();
            KichHoatSuaDoiYTa(false);

            btnQLNS_LuuYTa.Enabled = false;
            btnQLNS_XoaYTa.Enabled = false;
            btnQLNS_SuaDoiYTa.Enabled = true;
            btnQLNS_ThemYTa.Enabled = true;   

            LoadDSYTa();
            dgViewQLNS_DanhSachYT.Enabled = true;
        }

        //su kien tu dong dien vao textbox ten khoa khi chon MaKhoa

        //SuKien load thong tin khi click vao datagridview
        private void dgViewQLNS_DanhSachYT_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            try
            {
                if (dong != -1)
                {
                    LoadDuLieuYTaTuDataGridView(dong);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //event load anh tu dong cho Y ta khi click vao datagridview
        private void dgViewQLNS_DanhSachYT_Click(object sender, EventArgs e)
        {
            if (ThemHinhAnh.KiemTraMaHinhAnh(txtQLNS_MaYTa.Text))
                LoadAnhTuDB(txtQLNS_MaYTa.Text, picQLNS_YTa);
            else
                picQLNS_YTa.Image = null;
        }       

        private void cboQLNS_PhongKhamYT_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQLNS_TenPhongKhamYT.DataBindings.Clear();
            txtQLNS_TenPhongKhamYT.DataBindings.Add("text", QuanLiBSBUS.LayTenPKTuMaPK(cboQLNS_PhongKhamYT.Text), "TenPhong");
        }

        private void cboQLNS_PhongBenhYT_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQLNS_TenPhongBenhYT.DataBindings.Clear();
            txtQLNS_TenPhongBenhYT.DataBindings.Add("text", QuanLiBSBUS.LayTenPBTuMaPB(cboQLNS_PhongBenhYT.Text), "TenPhong");
        }

        //ngay sinh y ta
        private void dtPicker_NgaySinhYT_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime kiemtra = dtPicker_NgaySinhYT.Value;
            if ((DateTime.Parse(now.ToString()) < DateTime.Parse(kiemtra.ToString())))
            {
                dtPicker_NgaySinhYT.Text = "";

            }
        }

        //ngay vao lam y ta
        private void dtPickerNgayVaoLamYT_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime kiemtra = dtPickerNgayVaoLamYT.Value;
            if ((DateTime.Parse(now.ToString()) < DateTime.Parse(kiemtra.ToString())))
            {
                dtPickerNgayVaoLamYT.Text = "";

            }
        }

        private void cboQLNS_MaKhoaYT_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQLNS_TenKhoaYT.DataBindings.Clear();
            txtQLNS_TenKhoaYT.DataBindings.Add("text", QuanLiYTBUS.LayTenKhoaTuMaKhoa(cboQLNS_MaKhoaYT.Text), "TenKhoa");

        }

        #region Cac event cua textbox

        //event cua textbox
        private void txtQLNS_CMNDYT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtQLNS_SDTYT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }


        private void txtQLNS_HoYT_TextChanged(object sender, EventArgs e)
        {
            if (txtQLNS_HoYT.Text == "")
                lblQLNS_WarnHoYT.Show();
            else
                lblQLNS_WarnHoYT.Hide();
        }

        private void txtQLNS_TenYT_TextChanged(object sender, EventArgs e)
        {
            if (txtQLNS_TenYT.Text == "")
                lblQLNS_WarnTenYT.Show();
            else
                lblQLNS_WarnTenYT.Hide();
        }

        private void txtQLNS_CMNDYT_TextChanged(object sender, EventArgs e)
        {
            if (txtQLNS_CMNDYT.Text == "")
                lblQLNS_WarnCMNDYT.Show();
            else
                lblQLNS_WarnCMNDYT.Hide();
        }

        private void txtQLNS_TenKhoaYT_TextChanged(object sender, EventArgs e)
        {
            if (txtQLNS_TenKhoaYT.Text == "")
                lblQLNS_WarnTenKhoaYT.Show();
            else
                lblQLNS_WarnTenKhoaYT.Hide();
        }

        private void txtQLNS_SDTYT_TextChanged(object sender, EventArgs e)
        {
            if (txtQLNS_SDTYT.Text == "")
                lblQLNS_WarnSDTYT.Show();
            else
                lblQLNS_WarnSDTYT.Hide();
        }



        #endregion

        #endregion


        #endregion


        #region RibbonItem Quan Li Nhan Su - Nhan Vien


        #region Cac Ham Lien Quan - Nhan Vien

        //ham load thong tin cho Quan li nhan vien
        public void NhapLieuNhanVien()
        {
            txtQLNS_MaNhanVien.Text = QuanLiNVBUS.NextIDNhanVien();

            //Load combobox gioi tinh
            cboQLNS_GioiTinhNV.DataSource = QuanLiNVBUS.DSGioiTinh();
            cboQLNS_GioiTinhNV.DisplayMember = "NameGTNV";
            cboQLNS_GioiTinhNV.ValueMember = "GioiTinh";

            //Load combobox trinh do
            cboQLNS_TrinhDoNV.DataSource = QuanLiNVBUS.DSTrinhDo();
            cboQLNS_TrinhDoNV.DisplayMember = "NameTrinhDoNV";
            cboQLNS_TrinhDoNV.ValueMember = "HocVan";

            //load combobox MaPhongBan
            cboQLNS_MaPhongBanNV.DataSource = QuanLiNVBUS.DSPhongBan();
            cboQLNS_MaPhongBanNV.DisplayMember = "NameMaPhongBan";
            cboQLNS_MaPhongBanNV.ValueMember = "MaPhongBan";

            //load combobox Hoc vi cho nhan vien
            cboQLNS_HocViNV.DataSource = QuanLiNVBUS.DSHocVi();
            cboQLNS_HocViNV.DisplayMember = "NameHocViNV";
            cboQLNS_HocViNV.ValueMember = "HocVan";

        }

        //Lay thong tin cua nhan vien
        private NhanVien LayThongTinNhanVien()
        {
            NhanVien nv = new NhanVien();

            nv.MaNhanVien = txtQLNS_MaNhanVien.Text;
            nv.Ho = txtQLNS_HoNV.Text;
            nv.Ten = txtQLNS_TenNV.Text;
            nv.NgaySinh = Convert.ToDateTime(dtPickerQLNS_NgaySinhNV.Value.ToString());
            nv.GioiTinh = cboQLNS_GioiTinhNV.SelectedValue.ToString();
            nv.DiaChi = txtQLNS_DiaChiNV.Text;
            nv.CMND = Convert.ToInt32(txtQLNS_CMNDNV.Text);
            nv.TrinhDo = cboQLNS_TrinhDoNV.SelectedValue.ToString();
            nv.ChucVu = txtQLNS_ChucVuNV.Text;
            nv.MaPhongBan = cboQLNS_MaPhongBanNV.SelectedValue.ToString();
            nv.Email = txtQLNS_EmailNV.Text;
            nv.SDT = Convert.ToInt64(txtQLNS_SDTNV.Text);
            nv.NgayVaoLam = Convert.ToDateTime(dtPickerQLNS_NgayVaoLamNV.Value.ToString());
            nv.HocVi = cboQLNS_HocViNV.SelectedValue.ToString();
            nv.MaPhongLamViec = cboQLNS_MaPhongCongTac.SelectedValue.ToString();

            return nv;

        }

        //Kich hoat sua doi thong tin nhan vien
        public void KichHoatSuaDoiNhanVien(bool KiemTra)
        {
            txtQLNS_MaNhanVien.Enabled = KiemTra;
            txtQLNS_HoNV.Enabled = KiemTra;
            txtQLNS_TenNV.Enabled = KiemTra;
            dtPickerQLNS_NgaySinhNV.Enabled = KiemTra;
            cboQLNS_GioiTinhNV.Enabled = KiemTra;
            txtQLNS_DiaChiNV.Enabled = KiemTra;
            txtQLNS_CMNDNV.Enabled = KiemTra;
            cboQLNS_TrinhDoNV.Enabled = KiemTra;
            txtQLNS_ChucVuNV.Enabled = KiemTra;
            cboQLNS_MaPhongBanNV.Enabled = KiemTra;
            txtQLNS_TenPhongBanNV.Enabled = KiemTra;
            txtQLNS_EmailNV.Enabled = KiemTra;
            txtQLNS_SDTNV.Enabled = KiemTra;
            dtPickerQLNS_NgayVaoLamNV.Enabled = KiemTra;
            cboQLNS_HocViNV.Enabled = KiemTra;
            cboQLNS_MaPhongCongTac.Enabled = KiemTra;
            txtQLNS_TenPhongCongTac.Enabled = KiemTra;

            btnQLNS_XoaAnhNV.Enabled = KiemTra;
            btnQLNS_TaiAnhNV.Enabled = KiemTra;           
        }

        //Load danh sach nhan vien
        public void LoadDSNhanVien()
        {
            dgViewQLNS_DSNhanVien.DataSource = QuanLiNVBUS.DSNhanVien();
        }        

        //Load du lieu tu datagridview nhan vien len form
        public void LoadDuLieuNhanVienTuDataGridView(int dong)
        {
            txtQLNS_MaNhanVien.Text = dgViewQLNS_DSNhanVien.Rows[dong].Cells["MaNhanVien"].Value.ToString();
            txtQLNS_HoNV.Text = dgViewQLNS_DSNhanVien.Rows[dong].Cells["HoNV"].Value.ToString();
            txtQLNS_TenNV.Text = dgViewQLNS_DSNhanVien.Rows[dong].Cells["TenNV"].Value.ToString();
            dtPickerQLNS_NgaySinhNV.Text = dgViewQLNS_DSNhanVien.Rows[dong].Cells["NgaySinhNV"].Value.ToString();
            cboQLNS_GioiTinhNV.Text = dgViewQLNS_DSNhanVien.Rows[dong].Cells["GioiTinhNV"].Value.ToString();
            txtQLNS_DiaChiNV.Text = dgViewQLNS_DSNhanVien.Rows[dong].Cells["DiaChiNV"].Value.ToString();
            txtQLNS_CMNDNV.Text = dgViewQLNS_DSNhanVien.Rows[dong].Cells["CMNDNV"].Value.ToString();
            cboQLNS_TrinhDoNV.Text = dgViewQLNS_DSNhanVien.Rows[dong].Cells["TrinhDoNV"].Value.ToString();
            txtQLNS_ChucVuNV.Text = dgViewQLNS_DSNhanVien.Rows[dong].Cells["ChucVuNV"].Value.ToString();
            cboQLNS_MaPhongBanNV.Text = dgViewQLNS_DSNhanVien.Rows[dong].Cells["MaPhongBan"].Value.ToString();
            txtQLNS_EmailNV.Text = dgViewQLNS_DSNhanVien.Rows[dong].Cells["EmailNV"].Value.ToString();
            txtQLNS_SDTNV.Text = dgViewQLNS_DSNhanVien.Rows[dong].Cells["SDTNV"].Value.ToString();
            dtPickerQLNS_NgayVaoLamNV.Text = dgViewQLNS_DSNhanVien.Rows[dong].Cells["NgayVaoLamNV"].Value.ToString();
            cboQLNS_HocViNV.Text = dgViewQLNS_DSNhanVien.Rows[dong].Cells["HocViNV"].Value.ToString();
            cboQLNS_MaPhongCongTac.Text = dgViewQLNS_DSNhanVien.Rows[dong].Cells["MaPhongLamViecNV"].Value.ToString();
        }

        //Ham kiem tra thong tin nv nhap du chua 
        public bool KiemTraNhapThongTinNhanVien()
        {
            if (txtQLNS_HoNV.Text == "" || txtQLNS_TenNV.Text == "" || txtQLNS_SDTNV.Text == ""
                || txtQLNS_CMNDNV.Text == "" || txtQLNS_TenPhongBanNV.Text == "" || txtQLNS_TenPhongCongTac.Text == "")
                return false;
            else
                return true; 
        }


        #endregion


        #region Cac button quan li nhan su - Nhan vien


        //button them moi mot nhan vien
        private void btnQLNS_ThemMoiNV_Click(object sender, EventArgs e)
        {
            KichHoatSuaDoiNhanVien(true);

            NhapLieuNhanVien();

            btnQLNS_ThemMoiNV.Enabled = false;
            btnQLNS_XoaNV.Enabled = false;
            btnQLNS_SuaDoiNV.Enabled = false;
            btnQLNS_LuuNV.Enabled = true;

            txtQLNS_HoNV.Text = "";
            txtQLNS_TenNV.Text = "";
            cboQLNS_GioiTinhNV.Text = "";
            txtQLNS_DiaChiNV.Text = "";
            txtQLNS_CMNDNV.Text = "";
            cboQLNS_TrinhDoNV.Text = "";
            txtQLNS_ChucVuNV.Text = "";
            txtQLNS_TenPhongBanNV.Text = "";
            txtQLNS_EmailNV.Text = "";
            txtQLNS_SDTNV.Text = "";
            cboQLNS_HocViNV.Text = "";

            dgViewQLNS_DSNhanVien.Enabled = false;

        }

        //Button Xoa nhan vien khoi danh sach
        private void btnQLNS_XoaNV_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xóa Nhân Viên " + txtQLNS_HoNV.Text + " " + txtQLNS_TenNV.Text + "ra khỏi danh sách?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (QuanLiNVBUS.XoaNhanVien(txtQLNS_MaNhanVien.Text))
                    LoadDSNhanVien();
            }
        }

        //Button Sua Doi THong Tin Benh Nhan
        private void btnQLNS_SuaDoiNV_Click(object sender, EventArgs e)
        {
            KichHoatSuaDoiNhanVien(true);
            btnQLNS_XoaNV.Enabled = true;
            btnQLNS_LuuNV.Enabled = true;
            btnQLNS_ThemMoiNV.Enabled = false;
            btnQLNS_SuaDoiNV.Enabled = false;
            dgViewQLNS_DSNhanVien.Enabled = true;
        }

        //Button Luu Thong tin nhan vien 
        private void btnQLNS_LuuNV_Click(object sender, EventArgs e)
        {
            if (KiemTraNhapThongTinNhanVien())
            {
                NhanVien nv = LayThongTinNhanVien();

                if (QuanLiNVBUS.KiemTraTrungMaNhanVien(nv.MaNhanVien))
                {
                    if (cboQLNS_MaPhongBanNV.Text != "MPB-XN" && cboQLNS_MaPhongBanNV.Text != "MPB-XQ")
                        nv.MaPhongLamViec = "";
                    if (QuanLiNVBUS.SuaDoiNhanVien(nv))
                        LoadDSNhanVien();                
                }
                else
                {
                    if (cboQLNS_MaPhongBanNV.Text != "MPB-XN" && cboQLNS_MaPhongBanNV.Text != "MPB-XQ")
                        nv.MaPhongLamViec = "";
                    if (QuanLiNVBUS.ThemNhanVien(nv))
                        LoadDSNhanVien();
                }

                KichHoatSuaDoiNhanVien(false);

                btnQLNS_LuuNV.Enabled = false;
                btnQLNS_SuaDoiNV.Enabled = true;
                btnQLNS_ThemMoiNV.Enabled = true;
                btnQLNS_XoaNV.Enabled = true;
                dgViewBenhNhanCho.Enabled = true;              
            }            
        }

        //Button tai anh Nhan Vien
        private void btnQLNS_TaiAnhNV_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.JPG)|*.JPG|GIF Files(*.GIF)|*.GIF";
            //luu anh va xuat ra
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                ThemHinhAnh.XoaHinhAnh(txtQLNS_MaNhanVien.Text);
                ThemHinhAnh.StorePicture(dlg.FileName, txtQLNS_MaNhanVien.Text);
                MessageBox.Show("Lưu ảnh Nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAnhTuDB(txtQLNS_MaNhanVien.Text, picQLNS_NhanVien);
            }
        }

        //button tim kiem ma nhan vien
        private void btnQLNS_TKMaNhanVien_Click(object sender, EventArgs e)
        {
            int sodong = dgViewQLNS_DSNhanVien.RowCount - 1;
            for (int i = 0; i <= sodong; i++)
            {
                string manhanvien = dgViewQLNS_DSNhanVien.Rows[i].Cells["MaNhanVien"].Value.ToString();
                string makiemtra = txtQLNS_TKMaNhanVien.Text;
                if (makiemtra == manhanvien)
                {
                    lblQLNS_WarnTKMaNVDung.Show();
                    lblQLNS_WarnTKMaNVSai.Hide();

                    dgViewQLNS_DSNhanVien.Rows[i].Selected = true;
                    LoadDuLieuNhanVienTuDataGridView(i);
                    LoadAnhTuDB(makiemtra, picQLNS_NhanVien);

                    MessageBox.Show("Tìm kiếm thành công Nhân viên : {" + txtQLNS_TKMaNhanVien.Text + "} !", "Thông báo",
                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;
                }
                else
                {
                    lblQLNS_WarnTKMaNVDung.Hide();
                    lblQLNS_WarnTKMaNVSai.Show();
                }
            }
        }

        //button xoa anh nhan vien
        private void btnQLNS_XoaAnhNV_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xóa ảnh nhân viên : {" + txtQLNS_MaNhanVien.Text + "}", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ThemHinhAnh.XoaHinhAnh(txtQLNS_MaNhanVien.Text);
                picQLNS_NhanVien.Image = null;
            }
        }

        //button huy cua nhan vien
        private void btnQLNS_HuyThaoTacNV_Click(object sender, EventArgs e)
        {
            sptabNhanVien_Click(sender, e);
        }

        #endregion


        #region Cac Event trong QLNS - Nhan Vien


        //Supertab load thong tin Quan Li Nhan Vien
        private void sptabNhanVien_Click(object sender, EventArgs e)
        {
            lblQLNS_WarnTKMaNVDung.Hide();
            lblQLNS_WarnTKMaNVSai.Hide();

            btnQLNS_XoaNV.Enabled = false;
            btnQLNS_LuuNV.Enabled = false;
            btnQLNS_ThemMoiNV.Enabled = true;
            btnQLNS_SuaDoiNV.Enabled = true;

            NhapLieuNhanVien();
            KichHoatSuaDoiNhanVien(false);
            LoadDSNhanVien();
            dgViewQLNS_DSNhanVien.Enabled = true;
        }

        //event khi click ma phong ban thi tu dong dien ten phong ban
        private void cboQLNS_MaPhongBanNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQLNS_TenPhongBanNV.DataBindings.Clear();
            txtQLNS_TenPhongBanNV.DataBindings.Add("text", QuanLiNVBUS.LayTenPhongBanTuMaPB(cboQLNS_MaPhongBanNV.Text), "TenPhongBan");

            if (cboQLNS_MaPhongBanNV.Text == "MPB-XN" || cboQLNS_MaPhongBanNV.Text == "MPB-XQ")
            {
                cboQLNS_MaPhongCongTac.Visible = true;
                txtQLNS_TenPhongCongTac.Visible = true;
                //load combobbox danh sach ma phong lam viec xet ngiem va x quang
                cboQLNS_MaPhongCongTac.DataSource = QuanLiNVBUS.LayDSMaPhongLamViechChoNV(cboQLNS_MaPhongBanNV.Text);
                cboQLNS_MaPhongCongTac.DisplayMember = "DSMaPhongLamViec";
                cboQLNS_MaPhongCongTac.ValueMember = "MaPhongLamViec";
            }
            else
            {
                cboQLNS_MaPhongCongTac.Visible = false;
                txtQLNS_TenPhongCongTac.Visible = false;
            }

        } 


        //cua nhan vien
        private void cboQLNS_MaPhongCongTac_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQLNS_TenPhongCongTac.DataBindings.Clear();
            txtQLNS_TenPhongCongTac.DataBindings.Add("text", QuanLiNVBUS.LayTenPhongLVTuMaPhongLV(cboQLNS_MaPhongCongTac.Text), "TenPhongLamViec");
        }

        //load anh nhan vien tu dong khi click vao datagridview
        private void dgViewQLNS_DSNhanVien_Click(object sender, EventArgs e)
        {
            if (ThemHinhAnh.KiemTraMaHinhAnh(txtQLNS_MaNhanVien.Text))
                LoadAnhTuDB(txtQLNS_MaNhanVien.Text, picQLNS_NhanVien);
            else
                picQLNS_NhanVien.Image = null;
        }

        //event datagridView danh sach nhan vien
        private void dgViewQLNS_DSNhanVien_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            try
            {
                if (dong != -1)
                {
                    LoadDuLieuNhanVienTuDataGridView(dong);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //ngay sinh nhan vien
        private void dtPickerQLNS_NgaySinhNV_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime kiemtra = dtPickerQLNS_NgaySinhNV.Value;
            if ((DateTime.Parse(now.ToString()) < DateTime.Parse(kiemtra.ToString())))
            {
                dtPickerQLNS_NgaySinhNV.Text = "";

            }
        }

        //ngay vao lam nhan vien
        private void dtPickerQLNS_NgayVaoLamNV_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime kiemtra = dtPickerQLNS_NgayVaoLamNV.Value;
            if ((DateTime.Parse(now.ToString()) < DateTime.Parse(kiemtra.ToString())))
            {
                dtPickerQLNS_NgayVaoLamNV.Text = "";

            }
        }

        private void cboQLNS_TrinhDoNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboQLNS_TrinhDoNV.Text == "Đại học")
            {

                cboQLNS_HocViNV.Enabled = true;
                cboQLNS_HocViNV.SelectedValue = "";
            }
            else
            {
                cboQLNS_HocViNV.SelectedValue = "";
                cboQLNS_HocViNV.Enabled = false;
            }
        }


        #region Event cua textbox


        //event cua textbox
        private void txtQLNS_CMNDNV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtQLNS_SDTNV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtQLNS_HoNV_TextChanged(object sender, EventArgs e)
        {
            if (txtQLNS_HoNV.Text == "")
                lblQLNS_WarnHoNV.Show();
            else
                lblQLNS_WarnHoNV.Hide();
        }

        private void txtQLNS_TenNV_TextChanged(object sender, EventArgs e)
        {
            if (txtQLNS_TenNV.Text == "")
                lblQLNS_WarnTenNV.Show();
            else
                lblQLNS_WarnTenNV.Hide();
        }

        private void txtQLNS_CMNDNV_TextChanged(object sender, EventArgs e)
        {
            if (txtQLNS_CMNDNV.Text == "")
                lblQLNS_WarnCMNDNV.Show();
            else
                lblQLNS_WarnCMNDNV.Hide();
        }

        private void txtQLNS_TenPhongBanNV_TextChanged(object sender, EventArgs e)
        {
            if (txtQLNS_TenPhongBanNV.Text == "")
                lblQLNS_WarnTenPhongBanNV.Show();
            else
                lblQLNS_WarnTenPhongBanNV.Hide();
        }

        private void txtQLNS_SDTNV_TextChanged(object sender, EventArgs e)
        {
            if (txtQLNS_SDTNV.Text == "")
                lblQLNS_WarnSDTNV.Show();
            else
                lblQLNS_WarnSDTNV.Hide();
        }

        private void txtQLNS_TenPhongCongTac_TextChanged(object sender, EventArgs e)
        {
            if (txtQLNS_TenPhongCongTac.Text == "")
                lblQLNS_WarnTenPhongCongTac.Show();
            else
                lblQLNS_WarnTenPhongCongTac.Hide();
        }


        #endregion

        #endregion



        #endregion


        #region RibbonItem Quan Li Benh Nhan


        #region Cac ham lien quan - Benh Nhan


        public void ResetTextBoxQLBenhNhan()
        {
            txtQLBN_MaBenhNhan.Text = "";
            txtQLBN_MaBenhAnBN.Text = "";
            txtQLBN_HoBN.Text = "";
            txtQLBN_TenBN.Text = "";
            cboQLBN_GioiTinhBN.Text = "";
            txtQLBN_DiaChiBN.Text = "";
            txtQLBN_CMND.Text = "";
            cboQLBN_MaDoiTuongBN.Text = "";
            txtQLBN_TenDoiTuongBN.Text = "";
            txtQLBN_NgheNghiepBN.Text = "";
            txtQLBN_SDTBN.Text = "";
            txtQLBN_YeuCauKhamBN.Text = "";
            picQLBN_BenhNhan.Image = null;
        }

        //Ham lay thong tin benh nhan
        private BenhNhan LayThongTinBenhNhan()
        {
            BenhNhan bn = new BenhNhan();

            bn.MaBenhNhan = txtQLBN_MaBenhNhan.Text;
            bn.Ho = txtQLBN_HoBN.Text;
            bn.Ten = txtQLBN_TenBN.Text;
            bn.NgaySinh = Convert.ToDateTime(dtPickerQLBN_NgaySinhBN.Value.ToString());
            bn.GioiTinh = cboQLBN_GioiTinhBN.SelectedValue.ToString();
            bn.DiaChi = txtQLBN_DiaChiBN.Text;        
            bn.CMND = Convert.ToInt32(txtQLBN_CMND.Text);          
            bn.MaBenhAn = txtQLBN_MaBenhAnBN.Text;
            bn.MaDoiTuong = cboQLBN_MaDoiTuongBN.SelectedValue.ToString();
            bn.NgheNghiep = txtQLBN_NgheNghiepBN.Text;
            bn.SDT = Convert.ToInt64(txtQLBN_SDTBN.Text);
            bn.YeuCauKham = txtQLBN_YeuCauKhamBN.Text;
            bn.NgayKham = Convert.ToDateTime(dtPickerQLBN_NgayKhamBN.Value.ToString());
            bn.MaPhongKham = cboQLBN_PhongKhamBN.SelectedValue.ToString();

            return bn;
        }

        private BenhNhanCho LayThongTinBenhNhanCho()
        {
            BenhNhanCho bnc = new BenhNhanCho();

            bnc.MaBenhNhan = txtQLBN_MaBenhNhan.Text;
            bnc.Ho = txtQLBN_HoBN.Text;
            bnc.Ten = txtQLBN_TenBN.Text;
            bnc.NgaySinh = Convert.ToDateTime(dtPickerQLBN_NgaySinhBN.Value.ToString());
            bnc.GioiTinh = cboQLBN_GioiTinhBN.SelectedValue.ToString();
            bnc.DiaChi = txtQLBN_DiaChiBN.Text;
            bnc.CMND = Convert.ToInt32(txtQLBN_CMND.Text);
            bnc.MaBenhAn = txtQLBN_MaBenhAnBN.Text;
            bnc.MaDoiTuong = cboQLBN_MaDoiTuongBN.SelectedValue.ToString();
            bnc.NgheNghiep = txtQLBN_NgheNghiepBN.Text;
            bnc.SDT = Convert.ToInt64(txtQLBN_SDTBN.Text);
            bnc.YeuCauKham = txtQLBN_YeuCauKhamBN.Text;
            bnc.NgayKham = Convert.ToDateTime(dtPickerQLBN_NgayKhamBN.Value.ToString());
            bnc.MaPhongKham = cboQLBN_PhongKhamBN.SelectedValue.ToString();

            return bnc;
        }

        //Load Thong tin cua Quan li benh nhan
        public void NhapLieuBenhNhan()
        {
            txtQLBN_MaBenhNhan.Text = QuanLiBNBUS.NextIDMaBenhNhan();
            txtQLBN_MaBenhAnBN.Text = BenhAnBN.NextIDBenhAn();

            //Load combobox GioiTinh
            cboQLBN_GioiTinhBN.DataSource = QuanLiBNBUS.DSGioiTinh();
            cboQLBN_GioiTinhBN.DisplayMember = "NameGTBN";
            cboQLBN_GioiTinhBN.ValueMember = "GioiTinh";

            //Load combobox Doi Tuong
            cboQLBN_MaDoiTuongBN.DataSource = QuanLiBNBUS.DSDoiTuong();
            cboQLBN_MaDoiTuongBN.DisplayMember = "NameDoiTuongBN";
            cboQLBN_MaDoiTuongBN.ValueMember = "MaDoiTuong";

            //load danh sach ma phong kham
            cboQLBN_PhongKhamBN.DataSource = PhongKham.DSPhongKham();
            cboQLBN_PhongKhamBN.DisplayMember = "NameMaPhongKham";
            cboQLBN_PhongKhamBN.ValueMember = "MaPhongKham";
        }       

        //Load danh sach benh nhan
        public void LoadDSBenhNhan()
        {
            dgViewQLBN_DSBenhNhan.DataSource = QuanLiBNBUS.LayDanhSachBenhNhan();
        }

        //Kich hoat cac control trong Quan li benh nhan
        public void KichHoatSuaDoiBenhNhan(bool KiemTra)
        {
            txtQLBN_MaBenhNhan.Enabled = KiemTra;
            txtQLBN_MaBenhAnBN.Enabled = KiemTra;
            txtQLBN_HoBN.Enabled = KiemTra;
            txtQLBN_TenBN.Enabled = KiemTra;
            dtPickerQLBN_NgaySinhBN.Enabled = KiemTra;
            cboQLBN_GioiTinhBN.Enabled = KiemTra;
            txtQLBN_DiaChiBN.Enabled = KiemTra;
            txtQLBN_CMND.Enabled = KiemTra;
            cboQLBN_MaDoiTuongBN.Enabled = KiemTra;
            txtQLBN_TenDoiTuongBN.Enabled = KiemTra;
            txtQLBN_NgheNghiepBN.Enabled = KiemTra;
            txtQLBN_SDTBN.Enabled = KiemTra;
            txtQLBN_YeuCauKhamBN.Enabled = KiemTra;
            cboQLBN_PhongKhamBN.Enabled = KiemTra;
            dtPickerQLBN_NgayKhamBN.Enabled = KiemTra;

            btnQLBN_XoaAnhBN.Enabled = KiemTra;
            btnQLBN_TaiAnhBN.Enabled = KiemTra;           
        }

        //ham lay thong tin benh an: Ma benh an, Ma Benh nhan
        private BenhAn LayThongTinSoBoBenhAn()
        {
            BenhAn ba = new BenhAn();
            ba.MaBenhAn = txtQLBN_MaBenhAnBN.Text;
            ba.MaBenhNhan = txtQLBN_MaBenhNhan.Text;
            return ba;
        }

        //Load du lieu tu datagridview benh nhan
        public void LoadDuLieuBenhNhanTuDataGridView(int dong)
        {
            txtQLBN_MaBenhNhan.Text = dgViewQLBN_DSBenhNhan.Rows[dong].Cells["MaBenhNhanQLBN"].Value.ToString();
            txtQLBN_MaBenhAnBN.Text = dgViewQLBN_DSBenhNhan.Rows[dong].Cells["MaBenhAnQLBN"].Value.ToString();
            txtQLBN_HoBN.Text = dgViewQLBN_DSBenhNhan.Rows[dong].Cells["HoQLBN"].Value.ToString();
            txtQLBN_TenBN.Text = dgViewQLBN_DSBenhNhan.Rows[dong].Cells["TenQLBN"].Value.ToString();
            dtPickerQLBN_NgaySinhBN.Text = dgViewQLBN_DSBenhNhan.Rows[dong].Cells["NgaySinhQLBN"].Value.ToString();
            cboQLBN_GioiTinhBN.Text = dgViewQLBN_DSBenhNhan.Rows[dong].Cells["GioiTinhQLBN"].Value.ToString();
            txtQLBN_DiaChiBN.Text = dgViewQLBN_DSBenhNhan.Rows[dong].Cells["DiaChiQLBN"].Value.ToString();
            txtQLBN_CMND.Text = dgViewQLBN_DSBenhNhan.Rows[dong].Cells["CMNDQLBN"].Value.ToString();
            cboQLBN_MaDoiTuongBN.Text = dgViewQLBN_DSBenhNhan.Rows[dong].Cells["MaDoiTuongQLBN"].Value.ToString();
            txtQLBN_NgheNghiepBN.Text = dgViewQLBN_DSBenhNhan.Rows[dong].Cells["NgheNghiepQLBN"].Value.ToString();
            txtQLBN_SDTBN.Text = dgViewQLBN_DSBenhNhan.Rows[dong].Cells["SDTQLBN"].Value.ToString();
            txtQLBN_YeuCauKhamBN.Text = dgViewQLBN_DSBenhNhan.Rows[dong].Cells["YeuCauKhamQLBN"].Value.ToString();            
        }

        //Ham kiem tra truoc khi luu benh nhan trong Quan Li Benh Nhan
        public bool KiemTraTruocKhiLuuQLBN()
        {
            if (txtQLBN_YeuCauKhamBN.Text == "" || txtQLBN_TenBN.Text == "" || txtQLBN_CMND.Text == ""
                || txtQLBN_HoBN.Text == "" || txtQLBN_SDTBN.Text == "")
                return false;
            else
                return true;
        }

        #endregion


        #region Cac button - Benh Nhan

        public delegate void delMaBNBenhAn(TextBox mabenhnhan);
        //button Thong tin benh an cua benh nhan
        private void btnQLBN_BenhAnBN_Click(object sender, EventArgs e)
        {
            frmBenhAn benhan = new frmBenhAn();
            delMaBNBenhAn delBenhAn = new delMaBNBenhAn(benhan.AddMaBNBenhAn);
            delBenhAn(this.txtQLBN_MaBenhNhan);
            benhan.ShowDialog();
        }

        public delegate void delMaBNTheoDoi(TextBox mabenhnhan);
        //button thong tin theo doi cua benh nhan
        private void btnQLBN_TheoDoiBN_Click(object sender, EventArgs e)
        {
            frmTheoDoi theodoi = new frmTheoDoi();
            delMaBNTheoDoi delTheoDoi = new delMaBNTheoDoi(theodoi.AddMaBNTheoDoi);
            delTheoDoi(this.txtQLBN_MaBenhNhan);
            theodoi.ShowDialog();
        }

        //Button Luu thong tin benh nha
        private void btnQLBN_LuuBN_Click(object sender, EventArgs e)
        {            
            if (KiemTraTruocKhiLuuQLBN())
            {
                BenhNhan bn = LayThongTinBenhNhan();
                BenhNhanCho bnc = LayThongTinBenhNhanCho();
                BenhAn ba = LayThongTinSoBoBenhAn();
                if (QuanLiBNBUS.KiemTraTrungMaBenhNhan(bn.MaBenhNhan))
                {
                    if (QuanLiBNBUS.SuaDoiBenhNhan(bn))
                    {
                        QuanLiBNBUS.SuaDoiBenhNhanCho(bnc);
                        LoadDSBenhNhan();
                        rbItemQuanLiBenhNhan_Click(sender, e);
                       
                    }                                            
                }
                else
                {
                    if (QuanLiBNBUS.ThemBenhNhan(bn))
                    {
                        QuanLiBNBUS.ThemBenhNhanCho(bnc);
                        BenhAnBN.ThemBenhAnBN(ba);
                        LoadDSBenhNhan();
                        rbItemQuanLiBenhNhan_Click(sender, e);
                    }
                }
                KichHoatSuaDoiBenhNhan(false);
                btnQLBN_ThemMoiBN.Enabled = true;
                btnQLBN_SuaDoiBN.Enabled = true;
                btnQLBN_XoaBN.Enabled = true;
                btnQLBN_LuuBN.Enabled = false;
                dgViewQLBN_DSBenhNhan.Enabled = true;                
            }           
        }

        //Button Them moi benh nhan
        private void btnQLBN_ThemMoiBN_Click(object sender, EventArgs e)
        {
            btnQLBN_ThemMoiBN.Enabled = false;
            btnQLBN_XoaBN.Enabled = false;
            btnQLBN_SuaDoiBN.Enabled = false;
            btnQLBN_LuuBN.Enabled = true;
            KichHoatSuaDoiBenhNhan(true);

            NhapLieuBenhNhan();
            ResetTextBoxQLBenhNhan();
            dgViewQLBN_DSBenhNhan.Enabled = false;
        }

        //Button Xoa benh nhan
        private void btnQLBN_XoaBN_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xóa bệnh nhân " + txtQLBN_HoBN.Text + " " + txtQLBN_TenBN.Text + " ra khỏi danh sách?","Thông báo",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (QuanLiBNBUS.XoaBenhNhan(txtQLBN_MaBenhNhan.Text))
                    rbItemQuanLiBenhNhan.Select();
                rbItemQuanLiBenhNhan_Click(sender, e);
            }
        }

        //Button Sua doi benh nhan
        private void btnQLBN_SuaDoiBN_Click(object sender, EventArgs e)
        {
            btnQLBN_SuaDoiBN.Enabled = false;
            btnQLBN_ThemMoiBN.Enabled = false;
            btnQLBN_XoaBN.Enabled = true;
            btnQLBN_LuuBN.Enabled = true;

            KichHoatSuaDoiBenhNhan(true);           
            dgViewQLBN_DSBenhNhan.Enabled = true;
        }

        //button tai anh benh nhan cho Quan Li Benh Nhan
        private void btnQLBN_TaiAnhBN_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.JPG)|*.JPG|GIF Files(*.GIF)|*.GIF";
            //luu anh vao va xuat anh ra
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                ThemHinhAnh.XoaHinhAnh(txtQLBN_MaBenhNhan.Text);
                ThemHinhAnh.StorePicture(dlg.FileName, txtQLBN_MaBenhNhan.Text);
                MessageBox.Show("Lưu ảnh bệnh nhân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAnhTuDB(txtQLBN_MaBenhNhan.Text, picQLBN_BenhNhan);
            }
        }

        //button xoa anh benh nhan
        private void btnQLBN_XoaAnhBN_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xóa ảnh bệnh nhân : {" + txtQLBN_MaBenhNhan.Text + "}", "Thông Báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ThemHinhAnh.XoaHinhAnh(txtQLBN_MaBenhNhan.Text);
                picQLBN_BenhNhan.Image = null;
            }

        }

        //button tim kiem ma benh nhan
        private void btnQLBN_TKMaBenhNhan_Click(object sender, EventArgs e)
        {
            int sodong = dgViewQLBN_DSBenhNhan.RowCount - 1;
            for (int i = 0; i <= sodong; i++)
            {
                string mabenhnhan = dgViewQLBN_DSBenhNhan.Rows[i].Cells["MaBenhNhanQLBN"].Value.ToString();
                string makiemtra = txtQLBN_TKMaBenhNhan.Text;

                if (makiemtra == mabenhnhan)
                {
                    lblQLBN_WarnTKMaBNSai.Hide();
                    lblQLBN_TKMaBNDung.Show();

                    dgViewQLBN_DSBenhNhan.Rows[i].Selected = true;

                    LoadDuLieuBenhNhanTuDataGridView(i);

                    LoadAnhTuDB(makiemtra, picQLBN_BenhNhan);

                    MessageBox.Show("Tìm kiếm thành công Bệnh nhân : {" + txtQLBN_TKMaBenhNhan.Text + "} !", "Thông báo",
                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;
                }
                else
                {
                    lblQLBN_WarnTKMaBNSai.Show();
                    lblQLBN_TKMaBNDung.Hide();
                }
                   
            }
        }

        //button Huy cac thao tac dang su dung
        private void btnQLBN_HuyThaoTac_Click(object sender, EventArgs e)
        {
            rbItemQuanLiBenhNhan_Click(sender, e);
        }

        #endregion


        #region Cac event cua Quan li - Benh Nhan

        //LayTenPhongKham tu ma phong kham
        private void cboQLBN_PhongKhamBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQLBN_TenPhongKhamBN.DataBindings.Clear();
            txtQLBN_TenPhongKhamBN.DataBindings.Add("text", PhongKham.LayTenPhongTuMaPhong(cboQLBN_PhongKhamBN.Text), "TenPhong");
        }

        //event khi click vao supertab Quan Li BenhNhan
        private void rbItemQuanLiBenhNhan_Click(object sender, EventArgs e)
        {
            if (dgViewQLBN_DSBenhNhan.RowCount == 0)
                ResetTextBoxQLBenhNhan();

            lblQLBN_TKMaBNDung.Hide();
            lblQLBN_WarnTKMaBNSai.Hide();
            NhapLieuBenhNhan();
            KichHoatSuaDoiBenhNhan(false); 
         
            btnQLBN_XoaBN.Enabled = false;
            btnQLBN_LuuBN.Enabled = false;
            btnQLBN_ThemMoiBN.Enabled = true;
            btnQLBN_SuaDoiBN.Enabled = true;
 
            LoadDSBenhNhan();
            dgViewQLBN_DSBenhNhan.Enabled = true;
        }       
      
        //Event tu dong dien ten doi tuong khi click vao Ma Doi tuong
        private void cboQLBN_MaDoiTuongBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQLBN_TenDoiTuongBN.DataBindings.Clear();
            txtQLBN_TenDoiTuongBN.DataBindings.Add("text", QuanLiBNBUS.LayTenDoiTuongTuMaDT(cboQLBN_MaDoiTuongBN.Text), "TenDoiTuong");
        }

        //event cua datagridview
        private void dgViewQLBN_DSBenhNhan_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;

            try
            {
                if (dong != -1)
                {
                    LoadDuLieuBenhNhanTuDataGridView(dong);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //event load anh tu dong cho BenhNhan khi click vao datagridview
        private void dgViewQLBN_DSBenhNhan_Click(object sender, EventArgs e)
        {

        }

        private void dtPickerQLBN_NgaySinhBN_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime kiemtra = dtPickerQLBN_NgaySinhBN.Value;
            if ((DateTime.Parse(now.ToString()) < DateTime.Parse(kiemtra.ToString())))
            {
                dtPickerQLBN_NgaySinhBN.Text = "";

            }
        }

        private void dtPickerQLBN_NgayKhamBN_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime kiemtra = dtPickerQLBN_NgayKhamBN.Value;
            if ((DateTime.Parse(now.ToString()) > DateTime.Parse(kiemtra.ToString())))
            {
                dtPickerQLBN_NgayKhamBN.Text = "";

            }
        }

        #region event cua textbox

        //Load ngay kham va ma phong kham cho benh nhan khi ma benh nhan thay doi
        private void txtQLBN_MaBenhNhan_TextChanged(object sender, EventArgs e)
        {
            cboQLBN_PhongKhamBN.DataBindings.Clear();
            dtPickerQLBN_NgayKhamBN.DataBindings.Clear();

            cboQLBN_PhongKhamBN.DataBindings.Add("text", PhongKham.LayNgayKhamMaPhongKham(txtQLBN_MaBenhNhan.Text), "MaPhongKham");
            dtPickerQLBN_NgayKhamBN.DataBindings.Add("text", PhongKham.LayNgayKhamMaPhongKham(txtQLBN_MaBenhNhan.Text), "NgayKham");
        }

        //dau nhac nho cho SDT bn
        private void txtQLBN_SDTBN_TextChanged(object sender, EventArgs e)
        {
            if (txtQLBN_SDTBN.Text == "")
                lblQLBN_WarnSDT.Show();
            else
                lblQLBN_WarnSDT.Hide();
        } 

        //dau nhac nho cho TenBN
        private void txtQLBN_TenBN_TextChanged(object sender, EventArgs e)
        {
            if (txtQLBN_TenBN.Text == "")
                lblQLBN_WarnTenBN.Show();
            else
                lblQLBN_WarnTenBN.Hide();
        }      

        //event nhac nho nguoi nhap CMND cho BN
        private void txtQLBN_CMND_TextChanged(object sender, EventArgs e)
        {
            if (txtQLBN_CMND.Text == "")
                lblQLBN_WarnCMND.Show();
            else
                lblQLBN_WarnCMND.Hide();
        }

        private void txtQLBN_HoBN_TextChanged(object sender, EventArgs e)
        {
            if (txtQLBN_HoBN.Text == "")
                lblQLBN_WarnHoBN.Show();
            else
                lblQLBN_WarnHoBN.Hide();
        }

        private void txtQLBN_YeuCauKhamBN_TextChanged_1(object sender, EventArgs e)
        {
            if (txtQLBN_YeuCauKhamBN.Text == "")
                lblQLBN_WarnYeuCauKham.Show();
            else
                lblQLBN_WarnYeuCauKham.Hide();
        }

        //event cua textbox ko cho dien ky tu vao textbox
        private void txtQLBN_SDTBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        //event cua textbox ko cho dien ky tu vao textbox
        private void txtQLBN_CMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        } 

        #endregion

        #endregion


        #endregion    


        #region RibbonItem Kham Benh


        #region Cac ham lien quan trong kham benh

        //ham lay thong tin kham benh
        private PhieuKham LayThongTinKhamBenh()
        {
            PhieuKham pk = new PhieuKham();

            pk.MaBenhNhan = txtKBenh_MaBenhNhan.Text;
            pk.CanNang =  Convert.ToInt32(txtKBenh_CanNang.Text);
            pk.NhomMau = cboKBenh_NhomMauBN.Text;
            pk.NhietDo = Convert.ToInt32(txtKBenh_NhietDo.Text);
            pk.Mach = Convert.ToInt32(txtKBenh_Mach.Text);
            pk.HuyetAp = Convert.ToInt32(txtKBenh_HuyetAp.Text);
            pk.NhipTho = Convert.ToInt32(txtKBenh_NhipTho.Text);
            pk.LyDoKham = txtKBenh_LyDoKham.Text;
            pk.TinhTrangHienTai = txtKBenh_TTHienTai.Text;
            pk.BenhSu = txtKBenh_BenhSu.Text;
            pk.ChuanDoanSoDo = txtKBenh_ChuanDoanSoBo.Text;
            pk.YeuCauThem = txtKBenh_YeuCauThem.Text;
            pk.ChuanDoanSauCung = txtKBenh_ChuanDoanSauCung.Text;
            pk.HuongDieuTri = txtKBenh_HuongDieuTri.Text;
            pk.NgayKham = Convert.ToDateTime(dtPickerKBenh_NgayKham.Value.ToString());

            return pk;
        }

        //load cac thong tin cua KhamBenh
        public void NhapLieuKhamBenh()
        {
            //Load combobox danh sach phong kham
            cboKBenh_PhongKham.DataSource = PhieuKhamBUS.DSPhongKham();
            cboKBenh_PhongKham.DisplayMember = "NamePhongKhamBenh";
            cboKBenh_PhongKham.ValueMember = "MaPhongKham";

            DataTable dt = new DataTable();
            
            dt.Columns.Add("STT");
            dt.Columns.Add("NhomMau");

            dt.Rows.Add(" ");
            dt.Rows.Add("O");
            dt.Rows.Add("A");
            dt.Rows.Add("B");
            dt.Rows.Add("AB");
            //Load combobox nhom mau
            cboKBenh_NhomMauBN.DataSource = dt;
            cboKBenh_NhomMauBN.DisplayMember = "STT";
            cboKBenh_NhomMauBN.ValueMember = "NhomMau";            
        }

        //Lay danh sach benh nhan theo phong kham
        public void LoadDSBenhNhanTheoPhongKham()
        {
            if (cboKBenh_PhongKham.Text != "")
                dgViewKBenh_BenhNhanCho.DataSource = PhieuKhamBUS.DSBenhNhanTheoMaPhongKham(cboKBenh_PhongKham.Text);            
        }

        //Load Du lieu benh nhan theo ma khoa tu datagridview
        public void LoadDuLieuBNKhamBenhTuDataGridView(int dong)
        {
            //load du lieu cho phan thong tin benh nhan
            txtKBenh_MaBenhNhan.Text = dgViewKBenh_BenhNhanCho.Rows[dong].Cells["MaBenhNhanKBenh"].Value.ToString();
            txtKBenh_HoBN.Text = dgViewKBenh_BenhNhanCho.Rows[dong].Cells["HoKBenh"].Value.ToString();
            txtKBenh_TenBN.Text = dgViewKBenh_BenhNhanCho.Rows[dong].Cells["TenKBenh"].Value.ToString();
            txtKBenh_GioiTinhBN.Text = dgViewKBenh_BenhNhanCho.Rows[dong].Cells["GioiTinhKBenh"].Value.ToString();
            dtPickerKBenh_NgaySinh.Text = dgViewKBenh_BenhNhanCho.Rows[dong].Cells["NgaySinhKBenh"].Value.ToString();
            txtKBenh_DiaChiBN.Text = dgViewKBenh_BenhNhanCho.Rows[dong].Cells["DiaChiKBenh"].Value.ToString();
            txtKBenh_SDTBN.Text = dgViewKBenh_BenhNhanCho.Rows[dong].Cells["SDTKBenh"].Value.ToString();
            txtKBenh_YeuCauKhamBN.Text = dgViewKBenh_BenhNhanCho.Rows[dong].Cells["YeuCauKhamKBenh"].Value.ToString();
        
            //Load thong tin kham benh cua benh nhan da luu dua vao ma benh nhan
            LoadDuLieuKhamBenhChoBenhNhan();
        }

        public void LoadDuLieuKhamBenhChoBenhNhan()
        {
            txtKBenh_CanNang.DataBindings.Clear();
            txtKBenh_Mach.DataBindings.Clear();
            cboKBenh_NhomMauBN.DataBindings.Clear();
            txtKBenh_HuyetAp.DataBindings.Clear();
            txtKBenh_NhietDo.DataBindings.Clear();
            txtKBenh_NhipTho.DataBindings.Clear();
            txtKBenh_LyDoKham.DataBindings.Clear();
            txtKBenh_TTHienTai.DataBindings.Clear();
            txtKBenh_BenhSu.DataBindings.Clear();
            txtKBenh_ChuanDoanSoBo.DataBindings.Clear();
            txtKBenh_YeuCauThem.DataBindings.Clear();
            txtKBenh_ChuanDoanSauCung.DataBindings.Clear();
            txtKBenh_HuongDieuTri.DataBindings.Clear();

            txtKBenh_CanNang.DataBindings.Add("text", PhieuKhamBUS.LayThongTinKhamBenhTuMaBenhNhan(txtKBenh_MaBenhNhan.Text), "CanNang");
            txtKBenh_Mach.DataBindings.Add("text", PhieuKhamBUS.LayThongTinKhamBenhTuMaBenhNhan(txtKBenh_MaBenhNhan.Text), "Mach");
            cboKBenh_NhomMauBN.DataBindings.Add("text", PhieuKhamBUS.LayThongTinKhamBenhTuMaBenhNhan(txtKBenh_MaBenhNhan.Text), "NhomMau");
            txtKBenh_HuyetAp.DataBindings.Add("text", PhieuKhamBUS.LayThongTinKhamBenhTuMaBenhNhan(txtKBenh_MaBenhNhan.Text), "HuyetAp");
            txtKBenh_NhietDo.DataBindings.Add("text", PhieuKhamBUS.LayThongTinKhamBenhTuMaBenhNhan(txtKBenh_MaBenhNhan.Text), "NhietDo");
            txtKBenh_NhipTho.DataBindings.Add("text", PhieuKhamBUS.LayThongTinKhamBenhTuMaBenhNhan(txtKBenh_MaBenhNhan.Text), "NhipTho");
            txtKBenh_LyDoKham.DataBindings.Add("text", PhieuKhamBUS.LayThongTinKhamBenhTuMaBenhNhan(txtKBenh_MaBenhNhan.Text), "LyDoKham");
            txtKBenh_TTHienTai.DataBindings.Add("text", PhieuKhamBUS.LayThongTinKhamBenhTuMaBenhNhan(txtKBenh_MaBenhNhan.Text), "TinhTrangHienTai");
            txtKBenh_BenhSu.DataBindings.Add("text", PhieuKhamBUS.LayThongTinKhamBenhTuMaBenhNhan(txtKBenh_MaBenhNhan.Text), "BenhSu");
            txtKBenh_ChuanDoanSoBo.DataBindings.Add("text", PhieuKhamBUS.LayThongTinKhamBenhTuMaBenhNhan(txtKBenh_MaBenhNhan.Text), "ChuanDoanSoBo");
            txtKBenh_YeuCauThem.DataBindings.Add("text", PhieuKhamBUS.LayThongTinKhamBenhTuMaBenhNhan(txtKBenh_MaBenhNhan.Text), "YeuCauThem");
            txtKBenh_ChuanDoanSauCung.DataBindings.Add("text", PhieuKhamBUS.LayThongTinKhamBenhTuMaBenhNhan(txtKBenh_MaBenhNhan.Text), "ChuanDoanSauCung");
            txtKBenh_HuongDieuTri.DataBindings.Add("text", PhieuKhamBUS.LayThongTinKhamBenhTuMaBenhNhan(txtKBenh_MaBenhNhan.Text), "HuongDieuTri");
        }

        public void ResetTextBoxKhamBenh()
        {
            txtKBenh_MaBenhNhan.Text = "";
            txtKBenh_HoBN.Text = "";
            txtKBenh_TenBN.Text = "";
            txtKBenh_GioiTinhBN.Text = "";
            dtPickerKBenh_NgaySinh.Text = "";
            txtKBenh_DiaChiBN.Text = "";
            txtKBenh_SDTBN.Text = "";
            txtKBenh_YeuCauKhamBN.Text = "";

            LoadAnhTuDB(txtKBenh_MaBenhNhan.Text, picKBenh_BenhNhan);
        }

        public bool KiemTraTruocKhiLuuKhamBenh()
        {          
            if (txtKBenh_CanNang.Text == "" || txtKBenh_Mach.Text == "" || txtKBenh_HuyetAp.Text == "" || txtKBenh_NhietDo.Text == "" || txtKBenh_NhipTho.Text == "" ||
                txtKBenh_LyDoKham.Text == "" || txtKBenh_TTHienTai.Text == "" || txtKBenh_BenhSu.Text == "" || txtKBenh_ChuanDoanSoBo.Text == "" ||
                txtKBenh_YeuCauThem.Text == "" || txtKBenh_ChuanDoanSauCung.Text == "" || txtKBenh_HuongDieuTri.Text == "" )    
                return false;
            return true;
        }

        //hàm đánh số thứ tự cho datagridview kham benh
        public void ResetSTTCacBenhNhan()
        {
            int sodong = dgViewKBenh_BenhNhanCho.RowCount - 1;
            for (int i = 0; i <= sodong; i++)
            {
                dgViewKBenh_BenhNhanCho.Rows[i].Cells["STTKBenh"].Value = i + 1;
            }
        }
        

        #endregion


        #region Cac button


        //delegate toi frmNhapVien
        public delegate void delMaBNNhapVien(TextBox mabenhnhan);
        frmNhapVien frmnv = new frmNhapVien(); 
        private void btnKBenh_NhapVien_Click(object sender, EventArgs e)
        {
            if (KiemTraTruocKhiLuuKhamBenh())
            {
                frmNhapVien frmNhapVien = new frmNhapVien();
                delMaBNNhapVien del1 = new delMaBNNhapVien(frmNhapVien.AddMaBN);
                del1(this.txtKBenh_MaBenhNhan);
                frmNhapVien.ShowDialog();
            }
            else
                MessageBox.Show("Bệnh nhân này chưa khám bệnh!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);              
        }

        //event khi click vao ribbon Kham benh
        private void rbItemKhamBenh_Click(object sender, EventArgs e)
        {
            NhapLieuKhamBenh();
            LoadDSBenhNhanTheoPhongKham();
            ResetSTTCacBenhNhan();
        }

        //Delete ma benh nhan cho don thuoc
        public delegate void delMaBenhNhan(TextBox mabenhnhan);
        frmDonThuoc frmcon = new frmDonThuoc();
        //Button cap thuoc
        private void btnKhBenh_CapThuoc_Click(object sender, EventArgs e)
        {
            if (txtKBenh_MaBenhNhan.Text != "" && txtKBenh_HoBN.Text != "" && txtKBenh_TenBN.Text != "")
            {
                frmDonThuoc frmThuoc = new frmDonThuoc();
                delMaBenhNhan del = new delMaBenhNhan(frmThuoc.LayMaBenhNhan);
                del(this.txtKBenh_MaBenhNhan);
                delMaBenhNhan del1 = new delMaBenhNhan(frmThuoc.LayMaBacSi);
                del1(this.txtKBenh_MaBS);
                frmThuoc.ShowDialog();
            }
            else
                MessageBox.Show("Chưa chọn bệnh nhân, bác sĩ để cấp thuốc!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);    
        }

        //button luu thong tin kham benh cua benh nhan
        private void btnKBenh_Luu_Click(object sender, EventArgs e)
        {
            if (KiemTraTruocKhiLuuKhamBenh() == false)
            {
                MessageBox.Show("Chưa nhập đầy đủ thông tin khám bệnh của bệnh nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                PhieuKham pk = LayThongTinKhamBenh();
                if (PhieuKhamBUS.ThemThongTinPhieuKham(pk))
                    MessageBox.Show("Lưu thông tin khám bệnh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Lưu thông tin khám bệnh Thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }         
        }


        #endregion                


        #region Cac Event khac trong Kham Benh

        //su kien click vao datagridview thi load du lieu len
        private void dgViewKBenh_BenhNhanCho_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            try
            {
                if (dong != -1)
                {
                    LoadDuLieuBNKhamBenhTuDataGridView(dong);
                    LoadAnhTuDB(txtKBenh_MaBenhNhan.Text, picKBenh_BenhNhan);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Load thong tin ten phong kham, bac si cua phong kham
        private void cboKBenh_PhongKham_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetTextBoxKhamBenh();

            LoadDSBenhNhanTheoPhongKham();

            txtKBenh_TenPhongKham.DataBindings.Clear();
            txtKBenh_MaBS.DataBindings.Clear();
            txtKBenh_HoBacSi.DataBindings.Clear();
            txtKBenh_TenBacSi.DataBindings.Clear();
            dtPickerKBenh_NgayKham.DataBindings.Clear();

            txtKBenh_TenPhongKham.DataBindings.Add("text", PhieuKhamBUS.LayTenPhongKhamTuMaPhongKham(cboKBenh_PhongKham.Text), "TenPhong");
            txtKBenh_MaBS.DataBindings.Add("text", PhieuKhamBUS.LayHoTenBacSiTuMaPhongKham(cboKBenh_PhongKham.Text), "MaBacSi");
            txtKBenh_HoBacSi.DataBindings.Add("text", PhieuKhamBUS.LayHoTenBacSiTuMaPhongKham(cboKBenh_PhongKham.Text), "Ho");
            txtKBenh_TenBacSi.DataBindings.Add("text", PhieuKhamBUS.LayHoTenBacSiTuMaPhongKham(cboKBenh_PhongKham.Text), "Ten");
            dtPickerKBenh_NgayKham.DataBindings.Add("text", PhieuKhamBUS.LayNgayKhamTuMaBNKhamBenh(txtKBenh_MaBenhNhan.Text), "NgayKham");
            ResetSTTCacBenhNhan();
        }

        private void dtPickerKBenh_NgayKham_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime kiemtra = dtPickerKBenh_NgayKham.Value;
            if ((DateTime.Parse(now.ToString()) > DateTime.Parse(kiemtra.ToString())))
            {
                dtPickerKBenh_NgayKham.Text = "";

            }
        }

        #region Event cua textbox

        private void txtKBenh_CanNang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtKBenh_Mach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtKBenh_HuyetAp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtKBenh_NhipTho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtKBenh_NhietDo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtKBenh_MaBenhNhan_TextChanged(object sender, EventArgs e)
        {
            txtKBenh_MaPhieuKham.DataBindings.Clear();
            txtKBenh_YeuCauKhamBN.DataBindings.Clear();

            txtKBenh_MaPhieuKham.DataBindings.Add("text", PhieuKhamBUS.LayMaPhieuKhamTuMaBNKhamBenh(txtKBenh_MaBenhNhan.Text), "MaPhieuKham");
            txtKBenh_YeuCauKhamBN.DataBindings.Add("text", PhieuKhamBUS.LayNgayKhamTuMaBNKhamBenh(txtKBenh_MaBenhNhan.Text), "YeuCauKham");

            if (txtKBenh_MaBenhNhan.Text == "")
            {
                btnKBenh_Luu.Enabled = false;
                btnKhBenh_CapThuoc.Enabled = false;
                btnKBenh_XoaPhieuKhamBNC.Enabled = false;
            }
            else
            {
                btnKBenh_Luu.Enabled = true;
                btnKhBenh_CapThuoc.Enabled = true;
                btnKBenh_XoaPhieuKhamBNC.Enabled = true;
            }
        }

        #endregion    


        #endregion

       
        #endregion


        #region RibbonItem Xet Nghiem

        #region Cac ham lien quan trong xet nghiem

        //Lay yeu cau kham
        public string LayThongTinCheckBox()
        {
            string a = "";
            string b = "";
            string c = "";
            string d = "";
            string e = "";

            if (ckbYeuCauXN_ADN.Checked)
                a = ckbYeuCauXN_ADN.Text + " ";
            if (ckbYeuCauXN_HuyetHoc.Checked)
                b = ckbYeuCauXN_HuyetHoc.Text + " ";
            if (ckbYeuCauXN_MienDich.Checked)
                c = ckbYeuCauXN_MienDich.Text + " ";
            if (ckbYeuCauXN_SinhHoa.Checked)
                d = ckbYeuCauXN_SinhHoa.Text + " ";
            if (ckbYeuCauXN_ViSinh.Checked)
                e = ckbYeuCauXN_ViSinh.Text + " ";

            return (a + b + c + d + e);
        }

        //ham lay thong tin xet nghiem
        private XetNghiem LayThongTinXetNghiemBN()
        {
            XetNghiem xn = new XetNghiem();

            xn.YeuCauXetNghiem = LayThongTinCheckBox();
            xn.MaPhongXN = cboXetNghiem_MaPhongXN.SelectedValue.ToString();
            xn.TenPhong = txtXetNghiem_TenPhongXN.Text;
            xn.MaKTV = cboXetNghiem_MaKTV.SelectedValue.ToString();
            xn.HoKTV = txtXetNghiem_HoKTV.Text;
            xn.TenKTV = txtXetNghiem_TenKTV.Text;
            xn.MaXetNghiem = txtXetNghiem_MaXetNghiem.Text;
            xn.NgayXN = Convert.ToDateTime(dtPickerXetNghiem_NgayXetNghiem.Value.ToString());
            xn.KetQuaXetNghiem = txtXetNghiem_KetQuaXN.Text;
            xn.DeNghi = txtXetNghiem_DeNghiXN.Text;
            xn.MaBenhNhan = txtXetNghiem_MaBenhNhan.Text;

            return xn;
        }

        //ham load tong tin cho form xet nghiem
        public void NhapLieuXetNghiemBN()
        {
            //Load ma phong xet nghiem
            cboXetNghiem_MaPhongXN.DataSource = XetNghiemBNBUS.LayMaPhongXetNgiem();
            cboXetNghiem_MaPhongXN.DisplayMember = "DSMaPhongXN";
            cboXetNghiem_MaPhongXN.ValueMember = "MaPhongLamViec";

            //load combobox danh sach ma ktv
            cboXetNghiem_MaKTV.DataSource = XetNghiemBNBUS.LoadMaKyThuatVien();
            cboXetNghiem_MaKTV.DisplayMember = "DSMaKTV";
            cboXetNghiem_MaKTV.ValueMember = "MaNhanVien";
        }

        public void LoadDSBenhNhanChoXN()
        {
            dgViewXetNghiem_DSBenhNhanCho.DataSource = XetNghiemBNBUS.DSBenhNhanChoXN();
        }

        //load du lieu len datagridview
        public void LoadDuLieuBenhNhanXetNgiemTuDS(int dong)
        {
            txtXetNghiem_MaBenhNhan.Text = dgViewXetNghiem_DSBenhNhanCho.Rows[dong].Cells["MaBNXN"].Value.ToString();
            txtXetNghiem_HoBN.Text = dgViewXetNghiem_DSBenhNhanCho.Rows[dong].Cells["HoBNXN"].Value.ToString();
            txtXetNghiem_TenBN.Text = dgViewXetNghiem_DSBenhNhanCho.Rows[dong].Cells["TenBNXN"].Value.ToString();
            cboXetNghiem_GioiTinhBN.Text = dgViewXetNghiem_DSBenhNhanCho.Rows[dong].Cells["GioiTinhBNXN"].Value.ToString();
            dtPickerXetNghiem_NgaySinhBN.Text = dgViewXetNghiem_DSBenhNhanCho.Rows[dong].Cells["NgaySinhBNXN"].Value.ToString();
            txtXetNghiem_DiaChiBN.Text = dgViewXetNghiem_DSBenhNhanCho.Rows[dong].Cells["DiaChiBNXN"].Value.ToString();
            txtXetNghiem_SDTBN.Text = dgViewXetNghiem_DSBenhNhanCho.Rows[dong].Cells["SDTBNXN"].Value.ToString();
        }

        //Kiem tra truoc khi luu thong tin xet nghiem
        public bool KiemTraNhapLieuXetNghiem()
        {
            if(txtXetNghiem_MaBenhNhan.Text == "" || txtXetNghiem_TenKTV.Text == "" || txtXetNghiem_HoKTV.Text == "" || 
                txtXetNghiem_TenPhongXN.Text == "" || txtXetNghiem_KetQuaXN.Text == "" || txtXetNghiem_DeNghiXN.Text == "" ||
                txtXetNghiem_MaXetNghiem.Text == "")
                return false;
            return true;
        }

        #endregion


        #region Cac button trong Xet Nghiem

        //button luu thong tin xet nghiem benh nhan
        private void btnXetNghiem_Luu_Click(object sender, EventArgs e)
        {
            if (KiemTraNhapLieuXetNghiem())
            {
                XetNghiem xn = LayThongTinXetNghiemBN();

                if (XetNghiemBNBUS.KiemTraTrungMaXetNghiem(txtXetNghiem_MaXetNghiem.Text))
                {
                    if (XetNghiemBNBUS.SuaDoiThongTinXetNgiem(xn))
                        MessageBox.Show("Sửa đổi thông tin xét nghiệm cho bệnh nhân thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (XetNghiemBNBUS.LuuThongTinXetNgiem(xn))
                        MessageBox.Show("Lưu thông tin xét nghiệm thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                MessageBox.Show("Chưa nhập đầy đủ thông tin xét nghiệm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //button xoa thong tin xet nghiem
        private void btnXetNghiem_XoaBN_Click(object sender, EventArgs e)
        {
            if (txtXetNghiem_MaXetNghiem.Text != "")
            {
                DialogResult result = MessageBox.Show("Xóa phiếu xét nghiệm bệnh nhân " + txtXetNghiem_HoBN.Text + " " + txtXetNghiem_TenBN.Text +
                    " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (XetNghiemBNBUS.XoaPhieuXetNghiemBN(txtXetNghiem_MaXetNghiem.Text))
                        MessageBox.Show("Xóa phiếu xét nghiệm bệnh nhân thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Chưa có thông tin phiêu xét nghiệm, xóa thất bại!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //button them phieu xet nghiem cho benh nhan
        private void btnXetNghiem_ThemPhieuXN_Click(object sender, EventArgs e)
        {
            if(XetNghiemBNBUS.KiemTraTrungMaBenhNhanXN(txtXetNghiem_MaBenhNhan.Text) == false)
                txtXetNghiem_MaXetNghiem.Text = XetNghiemBNBUS.NextIDXetNghiem();
        }

        #endregion


        #region Cac event lien quan trong Xet Nghiem

        private void txtXetNghiem_MaXetNghiem_TextChanged(object sender, EventArgs e)
        {
            cboXetNghiem_MaPhongXN.DataBindings.Clear();
            dtPickerXetNghiem_NgayXetNghiem.DataBindings.Clear();
            txtXetNghiem_KetQuaXN.DataBindings.Clear();
            txtXetNghiem_DeNghiXN.DataBindings.Clear();

            cboXetNghiem_MaPhongXN.DataBindings.Add("text", XetNghiemBNBUS.LayThongTinXetNghiemBN(txtXetNghiem_MaXetNghiem.Text), "MaPhongXetNghiem");
            dtPickerXetNghiem_NgayXetNghiem.DataBindings.Add("text", XetNghiemBNBUS.LayThongTinXetNghiemBN(txtXetNghiem_MaXetNghiem.Text), "NgayXetNghiem");
            txtXetNghiem_KetQuaXN.DataBindings.Add("text", XetNghiemBNBUS.LayThongTinXetNghiemBN(txtXetNghiem_MaXetNghiem.Text), "KetQua");
            txtXetNghiem_DeNghiXN.DataBindings.Add("text", XetNghiemBNBUS.LayThongTinXetNghiemBN(txtXetNghiem_MaXetNghiem.Text), "DeNghi");
        }

        private void cboXetNghiem_MaPhongXN_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtXetNghiem_TenPhongXN.DataBindings.Clear();
            txtXetNghiem_TenPhongXN.DataBindings.Add("text", XetNghiemBNBUS.LayTenPhongXNTuMaPhongXN(cboXetNghiem_MaPhongXN.Text), "TenPhongLamViec");
        }

        private void cboXetNghiem_MaKTV_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtXetNghiem_HoKTV.DataBindings.Clear();
            txtXetNghiem_TenKTV.DataBindings.Clear();
            txtXetNghiem_HoKTV.DataBindings.Add("text", XetNghiemBNBUS.LayHoTenKTVTuMaKTV(cboXetNghiem_MaKTV.Text), "Ho");
            txtXetNghiem_TenKTV.DataBindings.Add("text", XetNghiemBNBUS.LayHoTenKTVTuMaKTV(cboXetNghiem_MaKTV.Text), "Ten");
        }

        private void dgViewXetNghiem_DSBenhNhanCho_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            try
            {
                if (dong != -1)
                {
                    LoadDuLieuBenhNhanXetNgiemTuDS(dong);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //load anh benh nhan khi click vao datagridview
        private void dgViewXetNghiem_DSBenhNhanCho_Click(object sender, EventArgs e)
        {
            if (ThemHinhAnh.KiemTraMaHinhAnh(txtXetNghiem_MaBenhNhan.Text))
                LoadAnhTuDB(txtXetNghiem_MaBenhNhan.Text, picXetNghiem_BenhNhan);
            else
                picXetNghiem_BenhNhan.Image = null;
            if (XetNghiemBNBUS.KiemTraTrungMaBenhNhanXN(txtXetNghiem_MaBenhNhan.Text))
            {
                txtXetNghiem_MaXetNghiem.DataBindings.Clear();
                cboXetNghiem_MaKTV.DataBindings.Clear();

                txtXetNghiem_MaXetNghiem.DataBindings.Add("text", XetNghiemBNBUS.LayMaXetNghiemTuMaBenhNhan(txtXetNghiem_MaBenhNhan.Text), "MaXetNghiem");
                cboXetNghiem_MaKTV.DataBindings.Add("text", XetNghiemBNBUS.LayMaKtvTuMaPhongXN(cboXetNghiem_MaPhongXN.Text), "MaNhanVien");
            }
            else
            {
                txtXetNghiem_MaXetNghiem.Text = "";
                txtXetNghiem_KetQuaXN.Text = "";
                txtXetNghiem_DeNghiXN.Text = "";
            }
        }

        private void rbItemXetNghiem_Click(object sender, EventArgs e)
        {
            NhapLieuXetNghiemBN();
            LoadDSBenhNhanChoXN();
        }

        //ngay xet nghiem
        private void dtPickerXetNghiem_NgayXetNghiem_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime kiemtra = dtPickerXetNghiem_NgayXetNghiem.Value;
            if ((DateTime.Parse(now.ToString()) > DateTime.Parse(kiemtra.ToString())))
            {
                dtPickerXetNghiem_NgayXetNghiem.Text = "";
            }
        }

        #endregion

        #endregion


        #region Form Main cua chuong trinh

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            ConnectData conn = new ConnectData();
            conn.BackUpDuLieu();
        }

        #region Cac ham chung cua chuong trinh

        //Vo hieu hoa tat ca cac menu
        public void LoadMainForm(bool KiemTra, string MaNhomUsers = "MNAD")
        {
            btnItem_DangNhapHeThong.Enabled = !KiemTra;
            btnItem_DangXuatHeThong.Enabled = KiemTra;
            btnItem_DoiMatKhau.Enabled = KiemTra;
            rbItemLeTan.Visible = KiemTra;
            rbItemKhamBenh.Visible = KiemTra;
            rbItemX_Quang.Visible = KiemTra;
            rbItemXetNghiem.Visible = KiemTra;
            rbItemQuanLiNhanSu.Visible = KiemTra;
            rbItemQuanLiBenhNhan.Visible = KiemTra;
            rbItemGiupDo.Visible = KiemTra;
            rbItemThongKeBaoCao.Visible = KiemTra;

            //load chuc nang theo ma nhom cua nguoi dang nhap
            switch (MaNhomUsers)
            {
                case "MNAD": UsersAdmin(); break;
                case "MNBS": UsersBacSi(); break;
                case "MNLT": UsersLeTan(); break;               
                case "MNNV": UsersNhanVien(); break;
                case "MNPK": UsersPhoKhoa(); break;
                case "MNPP": UsersPhoPhong(); break;
                case "MNTK": UsersTruongKhoa(); break;
                case "MNTP": UsersTruongPhong(); break;                
            }
        }

        #region Phan Quyen Users
        //quyen cua admin
        public void UsersAdmin()
        {
            sptabBacSi.Visible = true;
            sptabNhanVien.Visible = true;
            sptabYTa.Visible = true;
            pnlQLBN_Button.Enabled = true;
            pnlQLNS_BacSi.Enabled = true;
            pnlQLNS_NhanVien.Enabled = true;
            pnlQLNS_YTa.Enabled = true;
            btnItem_ThemTaiKhoan.Enabled = true;
        }
        //quyen cua le tan
        public void UsersLeTan()
        {
            rbItemKhamBenh.Visible = false;
            rbItemX_Quang.Visible = false;
            rbItemXetNghiem.Visible = false;
            rbItemQuanLiNhanSu.Visible = false;
            rbItemQuanLiBenhNhan.Visible = false;
            rbItemQuanLiNhanSu.Visible = false;
            rbItemThongKeBaoCao.Visible = false;
        }
        //quyen cua bac si
        public void UsersBacSi()
        {
            pnlQLBN_Button.Enabled = false;
            rbItemLeTan.Visible = false;
            rbItemXetNghiem.Visible = false;
            rbItemX_Quang.Visible = false;
            rbItemThongKeBaoCao.Visible = false;
            rbItemQuanLiNhanSu.Visible = false;
            rbItemThongKeBaoCao.Visible = false;
        }
        //quyen cua truong khoa, PhoKhoa
        public void UsersNhanVien()
        {
            rbItemLeTan.Visible = false;
            rbItemKhamBenh.Visible = false;
            rbItemQuanLiBenhNhan.Visible = false;
            rbItemQuanLiNhanSu.Visible = false;
        }
        //quyen cua Truong Khoa
        public void UsersTruongKhoa()
        {
            rbItemLeTan.Visible = false;
            rbItemXetNghiem.Visible = false;
            rbItemX_Quang.Visible = false;
            rbItemQuanLiBenhNhan.Visible = true;

            rbItemQuanLiNhanSu.Visible = true;
            sptabYTa.Visible = true;
            sptabBacSi.Visible = true;
            sptabNhanVien.Visible = false;

            pnlQLBN_Button.Enabled = true;
            pnlQLNS_BacSi.Enabled = true;
            pnlQLNS_YTa.Enabled = true;
            
        }
        //quyen cua pho khoa
        public void UsersPhoKhoa()
        {
            pnlQLBN_Button.Enabled = false;
            pnlQLNS_BacSi.Enabled = false;
            pnlQLNS_YTa.Enabled = false;
            sptabNhanVien.Visible = false;
            sptabYTa.Visible = true; ;
            sptabBacSi.Visible = true;
            rbItemLeTan.Visible = false;
            rbItemThongKeBaoCao.Visible = false;
            rbItemX_Quang.Visible = false;
            rbItemXetNghiem.Visible = false;
        }
        //Quyen cua truong phong
        public void UsersTruongPhong()
        {
            rbItemKhamBenh.Visible = false;
            rbItemLeTan.Visible = false;
            rbItemQuanLiBenhNhan.Visible = false;
            sptabYTa.Visible = false;
            sptabBacSi.Visible = false;
            sptabNhanVien.Visible = true;
            pnlQLNS_NhanVien.Enabled = true;
        }
        //Quyen cua pho phong
        public void UsersPhoPhong()
        {
            rbItemLeTan.Visible = false;
            rbItemKhamBenh.Visible = false;
            rbItemQuanLiBenhNhan.Visible = false;
            sptabYTa.Visible = false;
            sptabBacSi.Visible = false;
            sptabNhanVien.Visible = true;
            pnlQLNS_NhanVien.Enabled = false;
        }

        #endregion

        //ham Load anh len tu database
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


        #endregion


        #region Event cua Form Main

        private void frmGiaoDienChung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void frmGiaoDienChung_Load(object sender, EventArgs e)
        {
            if (logged == false)
            {
                LoadMainForm(false);
                btnItem_ThemTaiKhoan.Enabled = false;
            }
            else
                LoadMainForm(true);
        }

        private void frmGiaoDienChung_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Thoát khỏi chương trình?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //gdchungBUS.BackupDuLieu();
                e.Cancel = false;
            }
            else
                e.Cancel = true;
        }

        private void btnThoatHeThong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


        #region ButtonItem cua Form Main

        //button doi mat khau 
        private void btnItem_DoiMatKhau_Click(object sender, EventArgs e)
        {           
        Cont:
            if (doimatkhau == null || doimatkhau.IsDisposed)
                doimatkhau = new frmDoiMatKhau();
        if (doimatkhau.ShowDialog() == DialogResult.OK)
        {
            string matkhaucu = doimatkhau.txtMatKhauCu.Text;
            string matkhaumoi = doimatkhau.txtMatKhauMoi.Text;
            string nhaplaimatkhau = doimatkhau.txtNhapLaiMatKhau.Text;

            if (matkhaucu.Equals("") || matkhaumoi.Equals("") || nhaplaimatkhau.Equals(""))
            {
                MessageBox.Show("Chưa nhập thông tin đổi mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto Cont;
            }
            //mat khau nhap lai khong trung voi mat khau moi
            if (matkhaumoi != nhaplaimatkhau)
            {
                doimatkhau.lblWarn_ThayDoiMatKhau.Text = "Mật khẩu nhập lại không trùng với mật khẩu mới!";
                goto Cont;
            }
            //mat cu khong dung
            if (Utilities.MaHoaMD5(matkhaucu) != Utilities.user.Password)
            {
                doimatkhau.lblWarn_ThayDoiMatKhau.Text = "Mật khẩu cũ không đúng!";
                goto Cont;
            }
            //sau khi nhap dung tat ca thong tin
            if (userBUS.DoiMatKhau(matkhaumoi))
            {
                MessageBox.Show("Đổi mật khẩu thành công, đăng nhập lại để kiểm tra!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnDangXuatHeThong_Click(sender, e);
            }       
        }

        }

        //Dang xuat khoi chuong trinh
        private void btnDangXuatHeThong_Click(object sender, EventArgs e)
        {
            rbItem_TrangChu.Select();
            User user = new User();
            Utilities.user = user;
            LoadMainForm(false);
            btnItem_ThemTaiKhoan.Enabled = false;
            this.Text = "Chưa đăng nhập hệ thống!";
            btnItem_DangNhapHeThong_Click(sender, e);
        }

        //Dang nhap vao he thong hoac dang xuat de dang nhap lai
        private void btnItem_DangNhapHeThong_Click(object sender, EventArgs e)
        {
            rbItem_TrangChu.Select();
            Mainform = new frmGiaoDienChung();
            this.Text = "Chưa đăng nhập hệ thống!";
            int numsErrorPass = 0;          
        //Vi tri se chạy continue
        Cont:
           
            if (dangnhap == null || dangnhap.IsDisposed)
                dangnhap = new frmDangNhap();

            //neu nhan vao nut dang nhap o frmDangNhap
            if (dangnhap.ShowDialog() == DialogResult.OK)
            {
          
                string username = dangnhap.txtTaiKhoan.Text;
                string password = dangnhap.txtMatKhau.Text;
                if (username.Equals(""))
                {
                    dangnhap.lblWarn_LoiTraVe.Text = "Tài khoản không hợp lệ!";
               
                    goto Cont;
                }
                if (password.Equals(""))
                {
                    dangnhap.lblWarn_LoiTraVe.Text = "Mật khẩu không hợp lệ!";
                  
                    goto Cont;
                }
                Utilities.user = userBUS.LayThongTinUser(username);

                //user không tồn tại
                if (Utilities.user.Username.Equals(""))
                {
                    dangnhap.lblWarn_LoiTraVe.Text = "Tài khoản không tồn tai !";
                    dangnhap.txtTaiKhoan.Focus();
                    goto Cont;
                }

                //mật khẩu sai
                if (Utilities.user.Password != Utilities.MaHoaMD5(password))
                {
                    dangnhap.lblWarn_LoiTraVe.Text = "Mật khẩu nhập vào sai!";
                    dangnhap.txtMatKhau.Focus();
                    numsErrorPass++;
                    if (numsErrorPass == 3)
                        this.Close();
                    goto Cont;
                }

                // neu dang nhap thanh cong thi se enable lai cac tab theo ma nhom cua ng dang nhap
                LoadMainForm(true, Utilities.user.MaNhom);
                this.Text = "Quản lí hoạt động bệnh viện - " + Utilities.user.TenNhom + " - Mã người dùng: " + Utilities.user.MaNguoiDung + 
                    " - " + Utilities.user.Ho + " " + Utilities.user.Ten;
            }
        }

        //Button them tai khoan
        private void btnItem_ThemTaiKhoan_Click(object sender, EventArgs e)
        {
            frmThemTaiKhoan frm = new frmThemTaiKhoan();
            frm.ShowDialog();
        }

        #endregion


        #endregion


        #region RibbonItem Chup X-Quang


        #region Cac ham lien quang trong Chup X Quang

        private X_Quang LayThongTinXQuangBN()
        {
            X_Quang xq = new X_Quang();

            xq.MaBenhNhan = txtXQuang_MaBN.Text;
            xq.MaPhieuChup = txtXQuang_MaPhieuXQBN.Text;
            xq.MaPhongXQ = cboXQuang_MaPhongXQ.SelectedValue.ToString();
            xq.KyThuat = txtXQuang_KyThuatXQ.Text;
            xq.KyThuatChup = txtXQuang_KyThuatChupXQ.Text;
            xq.MaNhanVien = cboXQuang_MaKTVXQ.SelectedValue.ToString();
            xq.ChuanDoan = txtXQuang_ChuanDoanBN.Text;
            xq.NgayChup = Convert.ToDateTime(dtPickerXQuang_NgayChupXQ.Value.ToString());
            xq.MoTaAnhChup = txtXQuang_ChiTietHinhAnh.Text;
            xq.KetLuan = txtXQuang_KetLuan.Text;
            xq.DeNghi = txtXQuang_DeNghi.Text;

            return xq;
        }

        public void NhapLieuXQuangBN()
        {
            //load combobox ma phong xquang
            cboXQuang_MaPhongXQ.DataSource = XQuangBN.LayMaPhongXQuang();
            cboXQuang_MaPhongXQ.DisplayMember = "DSMaPhongXQ";
            cboXQuang_MaPhongXQ.ValueMember = "MaPhongLamViec";
            //load combobox danh sach ktv
            cboXQuang_MaKTVXQ.DataSource = XQuangBN.LoadMaKyThuatVienXQ();
            cboXQuang_MaKTVXQ.DisplayMember = "DSMaKTV";
            cboXQuang_MaKTVXQ.ValueMember = "MaNhanVien";
        }

        public void LoadDSBenhNhanChoXQuang()
        {
            dgViewXQuang_DSBenhNhanCho.DataSource = XQuangBN.DSBenhNhanChoXQ();
        }

        public void LoadDuLieuBNXQuangLenDatagridview(int dong)
        {
            txtXQuang_MaBN.Text = dgViewXQuang_DSBenhNhanCho.Rows[dong].Cells["MaBNXQ"].Value.ToString();
            txtXQuang_HoBN.Text = dgViewXQuang_DSBenhNhanCho.Rows[dong].Cells["HoBNXQ"].Value.ToString();
            txtXQuang_TenBN.Text = dgViewXQuang_DSBenhNhanCho.Rows[dong].Cells["TenBNXQ"].Value.ToString();
            cboXQuang_GioiTinhBN.Text = dgViewXQuang_DSBenhNhanCho.Rows[dong].Cells["GioiTinhBNXQ"].Value.ToString();
            dtPickerXQuang_NgaySinhBN.Text = dgViewXQuang_DSBenhNhanCho.Rows[dong].Cells["NgaySinhBNXQ"].Value.ToString();
            txtXQuang_SDTBN.Text = dgViewXQuang_DSBenhNhanCho.Rows[dong].Cells["SDTBNXQ"].Value.ToString();
            txtXQuang_DiaChiBN.Text = dgViewXQuang_DSBenhNhanCho.Rows[dong].Cells["DiaChiBNXQ"].Value.ToString();
            txtXQuang_YeuCauKham.Text = dgViewXQuang_DSBenhNhanCho.Rows[dong].Cells["YeuCauKhamBNXQ"].Value.ToString();

            txtXQuang_GioiTinhBN.Text = dgViewXQuang_DSBenhNhanCho.Rows[dong].Cells["GioiTinhBNXQ"].Value.ToString();
        }

        public bool KiemTraNhapLieuXQuangBN()
        {
            if (txtXQuang_KyThuatChupXQ.Text == "" || txtXQuang_KyThuatXQ.Text == "" || txtXQuang_KetLuan.Text == "" ||
                txtXQuang_DeNghi.Text == "" || txtXQuang_ChuanDoanBN.Text == "" || txtXQuang_ChiTietHinhAnh.Text == "" ||
                txtXQuang_MaPhieuXQBN.Text == "" || txtXQuang_TenKTVXQ.Text == "" || txtXQuang_TenPhongXQ.Text == "")
                return false;
            return true;
        }

        #endregion


        #region Cac button cua X-Quang

        //button In phieu xet nghiem
        private void btnLeTan_InPhieuXQuang_Click(object sender, EventArgs e)
        {

        }

        //button them phieu chup xquang
        private void btnXQuang_ThemPhieuXQ_Click(object sender, EventArgs e)
        {
            if(XQuangBN.KiemTraMaBNTrongXQuang(txtXQuang_MaBN.Text) == false)
                txtXQuang_MaPhieuXQBN.Text = XQuangBN.NextIDXQuang();
        }

        //button xoa phieu chup xquang
        private void btnXQuang_XoaPhieuXQBN_Click(object sender, EventArgs e)
        {
            if (txtXQuang_MaPhieuXQBN.Text != "")
            {
                DialogResult result = MessageBox.Show("Xóa phiếu chụp X-Quang bệnh nhân " + txtXQuang_HoBN.Text + " " + txtXQuang_TenBN.Text +
                    " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (XQuangBN.XoaPhieuChupXQBenhNhan(txtXQuang_MaPhieuXQBN.Text))
                    {
                        ThemHinhAnh.XoaHinhAnh(txtXQuang_MaPhieuXQBN.Text);
                        MessageBox.Show("Xóa phiếu chụp X-Quang bệnh nhân thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rbItemX_Quang_Click(sender, e);
                    }
                }
            }
            else
            {
                MessageBox.Show("Chưa có thông tin phiêu chụp X-Quang bệnh nhân, xóa thất bại!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //button tai anh chup xquang cho phieu chup xquang
        private void btnXQuang_TaiAnhXQuang_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.JPG)|*.JPG|GIF Files(*.GIF)|*.GIF";
            //luu anh va xuat anh ra
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                ThemHinhAnh.XoaHinhAnh(txtXQuang_MaPhieuXQBN.Text);
                ThemHinhAnh.StorePicture(dlg.FileName, txtXQuang_MaPhieuXQBN.Text);
                MessageBox.Show("Lưu ảnh chụp X-Quang thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAnhTuDB(txtXQuang_MaPhieuXQBN.Text, picXQuang_AnhChupXQ);
            }
        }

        //xoa anh chup x quang da luu
        private void btnXQuang_XoaAnhXQ_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xóa ảnh chụp X-Quang bệnh nhân : {" + txtXQuang_MaBN.Text + "}", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (picXQuang_AnhChupXQ.Image != null)
            {
                if (result == DialogResult.Yes)
                {
                    ThemHinhAnh.XoaHinhAnh(txtXQuang_MaPhieuXQBN.Text);
                    picXQuang_AnhChupXQ.Image = null;
                }               
            }
            else
                MessageBox.Show("Bệnh nhân này chưa có hình chụp X-Quang để xóa!", "Lỗi ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);           
        }

        //button luu thong tin chup xquang
        private void btnXQuang_LuuBN_Click(object sender, EventArgs e)
        {
            if (KiemTraNhapLieuXQuangBN())
            {
                X_Quang xq = LayThongTinXQuangBN();

                if (XQuangBN.KiemTraTrungMaPhieuChupXQ(txtXQuang_MaPhieuXQBN.Text))
                {
                    if (XQuangBN.SuaDoiThongTinXQuangBN(xq))
                        MessageBox.Show("Sửa đổi thông tin phiếu chup X-Quang cho bệnh nhân thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (XQuangBN.LuuThongTinXQuangBN(xq))
                        MessageBox.Show("Lưu thông tin phiếu chụp X-Quang thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                MessageBox.Show("Chưa nhập đầy đủ thông tin phiếu X-Quang!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //button xoa benh nhan cho ra khoi danh sach cho
        private void btnKBenh_XoaPhieuKhamBNC_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xóa bệnh nhân : {" + txtKBenh_MaBenhNhan.Text + "} ra khỏi danh sách chờ?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (PhieuKhamBUS.XoaBNChoRaKhoiDSKhamBenh(txtKBenh_MaBenhNhan.Text))
                {
                    MessageBox.Show("Xóa bệnh nhân chờ thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDSBenhNhanTheoPhongKham();
                }
                else
                    MessageBox.Show("Xóa bệnh nhân chờ thất bại!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public delegate void delMaBNXQuang(TextBox text);
        frmPhongToHinhX_QuangBN frmXQ = new frmPhongToHinhX_QuangBN();
        //button phong to anh chup xQuang
        private void btnXQuang_PhongToAnhXQ_Click(object sender, EventArgs e)
        {
            if (txtXQuang_MaPhieuXQBN.Text != "")
            {
                if (KiemTraNhapLieuXQuangBN())
                {
                    frmPhongToHinhX_QuangBN frm = new frmPhongToHinhX_QuangBN();
                    delMaBNXQuang del = new delMaBNXQuang(frm.AddText);
                    del(this.txtXQuang_MaPhieuXQBN);
                    frm.ShowDialog();
                }
                else
                    MessageBox.Show("Bệnh nhân này chưa có Mã phiếu chụp!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Chưa chọn phiếu chụp X-Quang của bệnh nhân!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        #endregion

   
        #region Cac event lien quan trong X-Quang

        private void rbItemX_Quang_Click(object sender, EventArgs e)
        {
            NhapLieuXQuangBN();
            LoadDSBenhNhanChoXQuang();
        }

        //Xuat ten phong xquang tu ma phong
        private void cboXQuang_MaPhongXQ_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtXQuang_TenPhongXQ.DataBindings.Clear();
            txtXQuang_TenPhongXQ.DataBindings.Add("text", XQuangBN.LayTenPhongXQtuMaPhongXQ(cboXQuang_MaPhongXQ.Text), "TenPhongLamViec");
        }

        //lay ho ten ktv tu ma nhan ktv
        private void cboXQuang_MaKTVXQ_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtXQuang_HoKTVXQ.DataBindings.Clear();
            txtXQuang_TenKTVXQ.DataBindings.Clear();

            txtXQuang_HoKTVXQ.DataBindings.Add("text", XQuangBN.LayHoTenKTVTuMaKTV(cboXQuang_MaKTVXQ.Text), "Ho");
            txtXQuang_TenKTVXQ.DataBindings.Add("text", XQuangBN.LayHoTenKTVTuMaKTV(cboXQuang_MaKTVXQ.Text), "Ten");
        }

        private void dgViewXQuang_DSBenhNhanCho_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            try
            {
                if (dong != -1)
                {
                    LoadDuLieuBNXQuangLenDatagridview(dong);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgViewXQuang_DSBenhNhanCho_Click(object sender, EventArgs e)
        {
            if (ThemHinhAnh.KiemTraMaHinhAnh(txtXQuang_MaBN.Text))
                LoadAnhTuDB(txtXQuang_MaBN.Text, picXQuang_BenhNhan);
            else
                picXQuang_BenhNhan.Image = null;
        }      

        private void txtXQuang_MaPhieuXQBN_TextChanged(object sender, EventArgs e)
        {
            if (txtXQuang_MaPhieuXQBN.Text != "")
            {
                cboXQuang_MaPhongXQ.DataBindings.Clear();
                txtXQuang_KyThuatXQ.DataBindings.Clear();
                txtXQuang_KyThuatChupXQ.DataBindings.Clear();
                cboXQuang_MaKTVXQ.DataBindings.Clear();
                txtXQuang_ChiTietHinhAnh.DataBindings.Clear();
                txtXQuang_KetLuan.DataBindings.Clear();
                txtXQuang_DeNghi.DataBindings.Clear();
                txtXQuang_ChuanDoanBN.DataBindings.Clear();

                txtXQuang_KyThuatXQ.DataBindings.Add("text", XQuangBN.LayThongTinBNDaChupXQuang(txtXQuang_MaPhieuXQBN.Text), "KyThuat");
                txtXQuang_KyThuatChupXQ.DataBindings.Add("text", XQuangBN.LayThongTinBNDaChupXQuang(txtXQuang_MaPhieuXQBN.Text), "KyThuatChup");
                cboXQuang_MaKTVXQ.DataBindings.Add("text", XQuangBN.LayThongTinBNDaChupXQuang(txtXQuang_MaPhieuXQBN.Text), "MaNhanVien");
                cboXQuang_MaPhongXQ.DataBindings.Add("text", XQuangBN.LayThongTinBNDaChupXQuang(txtXQuang_MaPhieuXQBN.Text), "MaPhongXQ");
                txtXQuang_ChiTietHinhAnh.DataBindings.Add("text", XQuangBN.LayThongTinBNDaChupXQuang(txtXQuang_MaPhieuXQBN.Text), "MoTaAnhChup");
                txtXQuang_KetLuan.DataBindings.Add("text", XQuangBN.LayThongTinBNDaChupXQuang(txtXQuang_MaPhieuXQBN.Text), "KetLuan");
                txtXQuang_DeNghi.DataBindings.Add("text", XQuangBN.LayThongTinBNDaChupXQuang(txtXQuang_MaPhieuXQBN.Text), "DeNghi");
                txtXQuang_ChuanDoanBN.DataBindings.Add("text", XQuangBN.LayThongTinBNDaChupXQuang(txtXQuang_MaPhieuXQBN.Text), "ChuanDoan");
            }
            else
            {
                cboXQuang_MaPhongXQ.Text = "";
                txtXQuang_TenPhongXQ.Text = "";
                txtXQuang_KyThuatXQ.Text = "";
                txtXQuang_KyThuatChupXQ.Text = "";
                cboXQuang_MaKTVXQ.Text = "";
                txtXQuang_ChiTietHinhAnh.Text = "";
                txtXQuang_KetLuan.Text = "";
                txtXQuang_DeNghi.Text = "";
                txtXQuang_ChuanDoanBN.Text = "";
            }
        }

        private void txtXQuang_MaBN_TextChanged(object sender, EventArgs e)
        {
            if (XQuangBN.KiemTraTrungMaBNXQuang(txtXQuang_MaBN.Text))
            {
                txtXQuang_MaPhieuXQBN.DataBindings.Clear();
                txtXQuang_MaPhieuXQBN.DataBindings.Add("text", XQuangBN.LayMaPhieuChupTuMaBN(txtXQuang_MaBN.Text), "MaPhieuChup");           
            }
            else
            {
                txtXQuang_MaPhieuXQBN.Text = "";
                cboXQuang_MaPhongXQ.Text = "";
                txtXQuang_TenPhongXQ.Text = "";
                txtXQuang_KyThuatXQ.Text = "";
                txtXQuang_KyThuatChupXQ.Text = "";
                cboXQuang_MaKTVXQ.Text = "";
                txtXQuang_ChiTietHinhAnh.Text = "";
                txtXQuang_KetLuan.Text = "";
                txtXQuang_DeNghi.Text = "";
                txtXQuang_ChuanDoanBN.Text = "";
            }
            if (ThemHinhAnh.KiemTraMaHinhAnh(txtXQuang_MaPhieuXQBN.Text))
                LoadAnhTuDB(txtXQuang_MaPhieuXQBN.Text, picXQuang_AnhChupXQ);
            else
                picXQuang_AnhChupXQ.Image = null;
        }

        //ngay chup x quang
        private void dtPickerXQuang_NgayChupXQ_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime kiemtra = dtPickerXQuang_NgayChupXQ.Value;
            if ((DateTime.Parse(now.ToString()) > DateTime.Parse(kiemtra.ToString())))
            {
                dtPickerXQuang_NgayChupXQ.Text = "";
            }
        }
        
        #endregion      

        #endregion


        #region Report - Thong Ke Bao Cao   

  
        #region Report Ket qua Kham

        private void btnKBenh_In_Click(object sender, EventArgs e)
        {
            expandableSplitter1.Expanded = true;
            btnKBenh_TroLai.Visible = true;
            btnKBenh_In.Visible = false;
            groupPanel5.Visible = false;

            KetQuaKham rp = new KetQuaKham();
            ParameterValues a = new ParameterValues();
            ParameterDiscreteValue b = new ParameterDiscreteValue();
            b.Value = txtKBenh_HoBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HoBenhNhan"].ApplyCurrentValues(a);

            b.Value = txtKBenh_TenBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenBenhNhan"].ApplyCurrentValues(a);

            b.Value = txtKBenh_MaPhieuKham.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["MaPhieuKham"].ApplyCurrentValues(a);

            b.Value = txtKBenh_GioiTinhBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["GioiTinh"].ApplyCurrentValues(a);

            b.Value = dtPickerKBenh_NgaySinh.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgaySinh"].ApplyCurrentValues(a);

            b.Value = txtKBenh_DiaChiBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["DiaChi"].ApplyCurrentValues(a);

            b.Value = txtKBenh_LyDoKham.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["LyDoKham"].ApplyCurrentValues(a);

            b.Value = txtKBenh_TTHienTai.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TinhTrangHienTai"].ApplyCurrentValues(a);

            b.Value = txtKBenh_BenhSu.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["BenhSu"].ApplyCurrentValues(a);

            b.Value = dtPickerKBenh_NgayKham.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgayKham"].ApplyCurrentValues(a);

            b.Value = cboKBenh_PhongKham.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["PhongKham"].ApplyCurrentValues(a);

            b.Value = txtKBenh_HoBacSi.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HoBacSi"].ApplyCurrentValues(a);

            b.Value = txtKBenh_TenBacSi.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenBacSi"].ApplyCurrentValues(a);

            b.Value = txtKBenh_ChuanDoanSoBo.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["ChanDoanSoBo"].ApplyCurrentValues(a);

            b.Value = txtKBenh_YeuCauThem.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["YeuCauThem"].ApplyCurrentValues(a);

            b.Value = txtKBenh_ChuanDoanSauCung.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["ChanDoanSauCung"].ApplyCurrentValues(a);

            b.Value = txtKBenh_HuongDieuTri.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HuongDieuTri"].ApplyCurrentValues(a);

            b.Value = txtKBenh_Mach.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["Mach"].ApplyCurrentValues(a);

            b.Value = cboKBenh_NhomMauBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NhomMau"].ApplyCurrentValues(a);

            b.Value = txtKBenh_HuyetAp.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HuyetAp"].ApplyCurrentValues(a);

            b.Value = txtKBenh_NhietDo.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NhietDo"].ApplyCurrentValues(a);

            b.Value = txtKBenh_NhipTho.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NhipTho"].ApplyCurrentValues(a);

            b.Value = txtKBenh_CanNang.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["CanNang"].ApplyCurrentValues(a);

            crystalReportViewer1.ReportSource = rp;
        }

        private void btnKBenh_TroLai_Click(object sender, EventArgs e)
        {
            expandableSplitter1.Expanded = false;
            btnKBenh_In.Visible = true;
            btnKBenh_TroLai.Visible = false;
            groupPanel5.Visible = true;
        }

        #endregion


        #region Report Ket Qua Xet nghiem

        private void btnXetNghiem_In_Click(object sender, EventArgs e)
        {
            expandableSplitter2.Expanded = true;
            btnXetNghiem_TroLai.Visible = true;
            btnXetNghiem_In.Visible = false;
            groupPanel9.Visible = false;

            KetQuaXetNghiem rp = new KetQuaXetNghiem();
            ParameterValues a = new ParameterValues();
            ParameterDiscreteValue b = new ParameterDiscreteValue();

            b.Value = txtXetNghiem_MaXetNghiem.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["MaXetNghiem"].ApplyCurrentValues(a);

            b.Value = txtXetNghiem_HoBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HoBenhNhan"].ApplyCurrentValues(a);

            b.Value = txtXetNghiem_TenBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenBenhNhan"].ApplyCurrentValues(a);

            b.Value = cboXetNghiem_GioiTinhBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["GioiTinh"].ApplyCurrentValues(a);

            b.Value = dtPickerXetNghiem_NgaySinhBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgaySinh"].ApplyCurrentValues(a);

            b.Value = txtXetNghiem_DiaChiBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["DiaChi"].ApplyCurrentValues(a);

            if (ckbYeuCauXN_ADN.Checked == true)
            {
                b.Value = ckbYeuCauXN_ADN.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["YeuCauXetNghiem"].ApplyCurrentValues(a);
            }

            if (ckbYeuCauXN_HuyetHoc.Checked == true)
            {
                b.Value = ckbYeuCauXN_HuyetHoc.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["YeuCauXetNghiem1"].ApplyCurrentValues(a);
            }

            if (ckbYeuCauXN_MienDich.Checked == true)
            { 
                b.Value = ckbYeuCauXN_MienDich.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["YeuCauXetNghiem2"].ApplyCurrentValues(a);
            }

            if (ckbYeuCauXN_SinhHoa.Checked == true)
            {
                b.Value = ckbYeuCauXN_SinhHoa.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["YeuCauXetNghiem3"].ApplyCurrentValues(a);
            }

            if (ckbYeuCauXN_ViSinh.Checked == true)
            {
                b.Value = ckbYeuCauXN_ViSinh.Text;
                a.Add(b);
                rp.DataDefinition.ParameterFields["YeuCauXetNghiem4"].ApplyCurrentValues(a);
            }

            b.Value = txtXetNghiem_TenPhongXN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenPhongXetNghiem"].ApplyCurrentValues(a);

            b.Value = txtXetNghiem_HoKTV.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HoNhanVien"].ApplyCurrentValues(a);

            b.Value = txtXetNghiem_TenKTV.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenNhanVien"].ApplyCurrentValues(a);

            b.Value = dtPickerXetNghiem_NgayXetNghiem.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgayXetNghiem"].ApplyCurrentValues(a);

            b.Value = txtXetNghiem_KetQuaXN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["KetQuaXetNghiem"].ApplyCurrentValues(a);

            b.Value = txtXetNghiem_DeNghiXN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["DeNghi"].ApplyCurrentValues(a);

            crystalReportViewer2.ReportSource = rp;
        }

        private void btnXetNghiem_TroLai_Click(object sender, EventArgs e)
        {
            expandableSplitter2.Expanded = false;
            btnXetNghiem_TroLai.Visible = false;
            btnXetNghiem_In.Visible = true;
            groupPanel9.Visible = true;
        }

        #endregion


        #region Report Ket Qua X-Quang

        private void btnXQuang_InPhieu_Click(object sender, EventArgs e)
        {
            expandableSplitter3.Expanded = true;
            btnXQuang_InPhieu.Visible = false;
            btnXQuang_TroLai.Visible = true;
            groupPanel20.Visible = false;

            KetQuaXQuang rp = new KetQuaXQuang();
            ParameterValues a = new ParameterValues();
            ParameterDiscreteValue b = new ParameterDiscreteValue();

            b.Value = txtXQuang_MaPhieuXQBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["MaPhieuChup"].ApplyCurrentValues(a);

            b.Value = txtXQuang_HoBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HoBenhNhan"].ApplyCurrentValues(a);

            b.Value = txtXQuang_TenBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenBenhNhan"].ApplyCurrentValues(a);

            b.Value = txtXQuang_GioiTinhBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["GioiTinh"].ApplyCurrentValues(a);

            b.Value = dtPickerXQuang_NgaySinhBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgaySinh"].ApplyCurrentValues(a);

            b.Value = txtXQuang_DiaChiBN.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["DiaChi"].ApplyCurrentValues(a);

            b.Value = txtXQuang_TenPhongXQ.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenPhongXQ"].ApplyCurrentValues(a);

            b.Value = txtXQuang_HoKTVXQ.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HoNhanVien"].ApplyCurrentValues(a);

            b.Value = txtXQuang_TenKTVXQ.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenNhanVien"].ApplyCurrentValues(a);

            b.Value = dtPickerXQuang_NgayChupXQ.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgayChup"].ApplyCurrentValues(a);

            b.Value = txtXQuang_KyThuatXQ.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["KyThuat"].ApplyCurrentValues(a);

            b.Value = txtXQuang_KyThuatChupXQ.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["KyThuatChup"].ApplyCurrentValues(a);

            b.Value = txtXQuang_KetLuan.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["KetLuan"].ApplyCurrentValues(a);

            b.Value = txtXQuang_DeNghi.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["DeNghi"].ApplyCurrentValues(a);

            b.Value = txtXQuang_ChiTietHinhAnh.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["MoTa"].ApplyCurrentValues(a);

            crystalReportViewer3.ReportSource = rp;

        }

        private void btnXQuang_TroLai_Click(object sender, EventArgs e)
        {
            expandableSplitter3.Expanded = false;
            btnXQuang_InPhieu.Visible = true;
            btnXQuang_TroLai.Visible = false;
            groupPanel20.Visible = true;
        }

        #endregion


        #region In report thống kê báo cáo

        //thong ke bao cao - button In
        private void btnIn_Click(object sender, EventArgs e)
        {
            #region Code ban dau
            ////rpBaoCaoKhamBenh rp = new rpBaoCaoKhamBenh();
            //dsBaoCaoKhamBenh dataset = new dsBaoCaoKhamBenh();
            ////Instantiate variables
            //ReportDocument reportDocument = new ReportDocument();
            //ParameterField paramField = new ParameterField();
            //ParameterFields paramFields = new ParameterFields();
            //ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();

            ////Set instances for input parameter 1 -  @vDepartment
            //paramField.Name = "TUNGAY";
            ////Below variable can be set to any data present in SalseData table, Department column
            //paramDiscreteValue.Value = dtpTuNgay1.Value.ToString();
            //paramField.CurrentValues.Add(paramDiscreteValue);
            ////Add the paramField to paramFields
            //paramFields.Add(paramField);

            ////Set instances for input parameter 2 -  @iSalseYear
            ////*Remember to reconstruct the paramDiscreteValue and paramField objects
            //paramField = new ParameterField();
            //paramField.Name = "DENNGAY";
            //paramDiscreteValue = new ParameterDiscreteValue();
            //paramDiscreteValue.Value = dtpDenNgay1.Value.ToString();
            //paramField.CurrentValues.Add(paramDiscreteValue);

            ////Add the paramField to paramFields
            //paramFields.Add(paramField);


            //crystalReportViewer4.ParameterFieldInfo = paramFields;

            //reportDocument.Load(@"..\..\..\QuanLyBenhVien\rpBaoCaoKhamBenh.rpt");
            ////reportDocument.Load(@"D:\Hoc tap\Lap trinh CSDL\Quan ly benh vien\Do an Quan Ly Benh Vien\Code\QuanLyBenhVien\QuanLyBenhVien\rpBaoCaoKhamBenh.rpt");

            ////set the database loggon information.
            ////**Note that the third parameter is the DSN name and not the Database or System name
            ////reportDocument.SetDatabaseLogon("", "", "", "");
            //reportDocument.SetDataSource((DataTable)dataset.BAOCAO_BENHNHAN);
            

            ////Load the report by setting the report source

            //crystalReportViewer4.ReportSource = reportDocument;           
            

            ////  ConnectionInfo CInfo = new ConnectionInfo();
            ////  CInfo.ServerName = "XUBI-LAP";  // This Should be the same name What we have Configured under Oledb(ADO)
            ////  CInfo.UserID = "xubi";
            ////  CInfo.Password = "123456";
            ////  TableLogOnInfo tableInfo = new TableLogOnInfo(); // providing table Details  use the Seperate object of TableLogOnInfo Class for each table we use in the Report
            ////  tableInfo.ConnectionInfo = CInfo;
            ////  TableLogOnInfos tablelog = new TableLogOnInfos();
            ////  tablelog.Add(tableInfo);
            ////  crystalReportViewer1.ReportSource = @"D:\Hoc tap\Lap trinh CSDL\Quan ly benh vien\Do an Quan Ly Benh Vien\Code\QuanLyBenhVien\QuanLyBenhVien\rpBaoCaoKhamBenh.rpt"; //<"Path of the report">;
            ////  crystalReportViewer1.LogOnInfo = tablelog;
            ////// Using Above Coding to Skip/Elliminate user Id,passaword i.e.Connection Datails
            //// // string cname = ConfigurationManager.AppSettings.Get("CName");
            //// // string addr = ConfigurationManager.AppSettings.Get("Addr");
            ////  //string Website = ConfigurationManager.AppSettings.Get("WebSite");
            ////  ReportDocument obj = new ReportDocument();
            ////  obj.Load(@"D:\Hoc tap\Lap trinh CSDL\Quan ly benh vien\Do an Quan Ly Benh Vien\Code\QuanLyBenhVien\QuanLyBenhVien\rpBaoCaoKhamBenh.rpt");
            ////  crystalReportViewer1.ReportSource = obj;
            ////  //obj.SetParameterValue("CompanyName", cname);
            ////  //obj.SetParameterValue("Address", addr);
            //// // obj.SetParameterValue("Company Website", Website);
            ////  obj.SetParameterValue("TUNGAY",dtpTuNgay.Value.ToString());
            ////  obj.SetParameterValue("DENNGAY",dtpDenNgay.Value.ToString()); 

            #endregion

            // In danh sách bệnh nhân khám bệnh
            if (dtpTuNgay1.Visible == true)
            {
                SqlConnection cnn;
                string connectionString = null;
                string sql = null;

                string tungay;
                tungay = dtpTuNgay1.Value.ToString("MM/dd/yyyy");
                string denngay;
                denngay = dtpDenNgay1.Value.ToString("MM/dd/yyyy");

                connectionString = connectData.ChuoiKetNoi();
                //connectionString = "Data Source=XUBI-LAP;Initial Catalog=QLBV;Integrated Security=True";
                cnn = new SqlConnection(connectionString);
                cnn.Open();
                sql = "SELECT PHIEUKHAM.STT, BENHNHAN.MaBenhNhan, BENHNHAN.Ho, BENHNHAN.Ten, BENHNHAN.GioiTinh, " +
                                "BENHNHAN.NgheNghiep, BENHNHAN.NgaySinh, BENHNHAN.DiaChi, " +
                                "PHIEUKHAM.MaPhieuKham, PHIEUKHAM.NgayKham," +
                                "PHONGKHAM.TenPhong " +
                       "FROM BENHNHAN, PHIEUKHAM, PHONGKHAM " +
                       "WHERE (BENHNHAN.MaBenhNhan = PHIEUKHAM.MaBenhNhan) AND (BENHNHAN.MaPhongKham = PHONGKHAM.MaPhongKham) AND " +
                                "(PHIEUKHAM.NgayKham >= " + "'" + tungay + "'" + ") AND (PHIEUKHAM.NgayKham <= " + "'" + denngay + "'" + ")" + 
                        "ORDER BY PHIEUKHAM.STT";
                SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);
                QLBVDataSet ds = new QLBVDataSet();
                dscmd.Fill(ds, "BAOCAO_BENHNHAN");
                //MessageBox.Show(ds.Tables[1].Rows.Count.ToString());
                cnn.Close();



                rpBaoCaoKhamBenh objRpt = new rpBaoCaoKhamBenh();

                //objRpt.SetDataSource(ds.Tables[0]);
                objRpt.Database.Tables["BAOCAO_BENHNHAN"].SetDataSource(ds);
                //objRpt.SetDataSource((DataTable)ds.BAOCAO_BENHNHAN);
                
                ParameterValues a = new ParameterValues();
                ParameterDiscreteValue b = new ParameterDiscreteValue();
                b.Value = dtpTuNgay1.Text;
                a.Add(b);
                objRpt.DataDefinition.ParameterFields["TUNGAY"].ApplyCurrentValues(a);

                b.Value = dtpDenNgay1.Text;
                a.Add(b);
                objRpt.DataDefinition.ParameterFields["DENNGAY"].ApplyCurrentValues(a);

                crystalReportViewer4.ReportSource = objRpt;
                crystalReportViewer4.Refresh();                
            }

            // In danh sách bệnh nhân xét nghiệm
            if (dtpTuNgay2.Visible == true)
            {
                SqlConnection cnn;
                string connectionString = null;
                string sql = null;

                string tungay;
                tungay = dtpTuNgay2.Value.ToString("MM/dd/yyyy");
                string denngay;
                denngay = dtpDenNgay2.Value.ToString("MM/dd/yyyy");

                connectionString = connectData.ChuoiKetNoi();
                //connectionString = "Data Source=XUBI-LAP;Initial Catalog=QLBV;Integrated Security=True";
                cnn = new SqlConnection(connectionString);
                cnn.Open();
                sql = "SELECT XETNGHIEM.STT, BENHNHAN.MaBenhNhan, BENHNHAN.Ho, BENHNHAN.Ten, BENHNHAN.GioiTinh," +
                                "BENHNHAN.NgheNghiep, BENHNHAN.NgaySinh, BENHNHAN.DiaChi," +
                                "XETNGHIEM.MaXetNghiem, XETNGHIEM.NgayXetNghiem, " +
                                "PHONGLAMVIECNV.TenPhongLamViec " +
                    "FROM BENHNHAN, XETNGHIEM, PHONGLAMVIECNV " +
                    "WHERE (BENHNHAN.MaBenhNhan = XETNGHIEM.MaBenhNhan) AND ( XETNGHIEM.MaPhongXetNghiem = PHONGLAMVIECNV.MaPhongLamViec) AND " +
                            "(XETNGHIEM.NgayXetNghiem >= " + "'" + tungay + "'" + ") AND (XETNGHIEM.NgayXetNghiem <= " + "'" + denngay + "'" + ")" +
                    " ORDER BY XETNGHIEM.STT";
                SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);
                QLBVDataSet ds = new QLBVDataSet();
                dscmd.Fill(ds, "BAOCAO_BENHNHAN_XETNGHIEM");
                //MessageBox.Show(ds.Tables[1].Rows.Count.ToString());
                cnn.Close();



                rpBaoCaoXetNghiem objRpt = new rpBaoCaoXetNghiem();

                //objRpt.SetDataSource(ds.Tables[0]);
                objRpt.Database.Tables["BAOCAO_BENHNHAN_XETNGHIEM"].SetDataSource(ds);
                //objRpt.SetDataSource((DataTable)ds.BAOCAO_BENHNHAN);
                ParameterValues a = new ParameterValues();
                ParameterDiscreteValue b = new ParameterDiscreteValue();
                b.Value = dtpTuNgay2.Text;
                a.Add(b);
                objRpt.DataDefinition.ParameterFields["TUNGAY"].ApplyCurrentValues(a);

                b.Value = dtpDenNgay2.Text;
                a.Add(b);
                objRpt.DataDefinition.ParameterFields["DENNGAY"].ApplyCurrentValues(a);

                crystalReportViewer4.ReportSource = objRpt;
                crystalReportViewer4.Refresh();
            }

            // IN DANH SÁCH BỆNH NHÂN CHỤP X-QUANG
            if (dtpTuNgay3.Visible == true)
            {
                SqlConnection cnn;
                string connectionString = null;
                string sql = null;

                string tungay;
                tungay = dtpTuNgay3.Value.ToString("MM/dd/yyyy");
                string denngay;
                denngay = dtpDenNgay3.Value.ToString("MM/dd/yyyy");

                connectionString = connectData.ChuoiKetNoi();
                //connectionString = "Data Source=XUBI-LAP;Initial Catalog=QLBV;Integrated Security=True";
                cnn = new SqlConnection(connectionString);
                cnn.Open();
                sql = "SELECT X_QUANG.STT, BENHNHAN.MaBenhNhan, BENHNHAN.Ho, BENHNHAN.Ten, BENHNHAN.GioiTinh, "+
			                    "BENHNHAN.NgheNghiep, BENHNHAN.NgaySinh, BENHNHAN.DiaChi, "+
			                    "X_QUANG.MaPhongXQ, X_QUANG.NgayChup, "+
			                    "PHONGLAMVIECNV.TenPhongLamViec "+
	                    "FROM BENHNHAN, X_QUANG, PHONGLAMVIECNV "+
	                    "WHERE (BENHNHAN.MaBenhNhan = X_QUANG.MaBenhNhan) AND (X_QUANG.MaPhongXQ = PHONGLAMVIECNV.MaPhongLamViec) AND "+
			                    "(X_QUANG.NgayChup >= " + "'" + tungay + "'" + ") AND (X_QUANG.NgayChup <= " + "'" + denngay + "'" + ")" +
	                    "ORDER BY X_QUANG.STT";
                SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);
                QLBVDataSet ds = new QLBVDataSet();
                dscmd.Fill(ds, "BAOCAO_BENHNHAN_XQUANG");
                //MessageBox.Show(ds.Tables[1].Rows.Count.ToString());
                cnn.Close();



                rpBaoCaoXQuang objRpt = new rpBaoCaoXQuang();

                //objRpt.SetDataSource(ds.Tables[0]);
                objRpt.Database.Tables["BAOCAO_BENHNHAN_XQUANG"].SetDataSource(ds);
                //objRpt.SetDataSource((DataTable)ds.BAOCAO_BENHNHAN);
                ParameterValues a = new ParameterValues();
                ParameterDiscreteValue b = new ParameterDiscreteValue();
                b.Value = dtpTuNgay3.Text;
                a.Add(b);
                objRpt.DataDefinition.ParameterFields["TUNGAY"].ApplyCurrentValues(a);

                b.Value = dtpDenNgay3.Text;
                a.Add(b);
                objRpt.DataDefinition.ParameterFields["DENNGAY"].ApplyCurrentValues(a);

                crystalReportViewer4.ReportSource = objRpt;
                crystalReportViewer4.Refresh();
            }

            // in báo cáo doanh thu
            if (dtpDenNgay4.Visible == true)
            {
                SqlConnection cnn;
                string connectionString = null;
                string sql = null;

                string tungay;
                tungay = dtpTuNgay4.Value.ToString("MM/dd/yyyy");
                string denngay;
                denngay = dtpDenNgay4.Value.ToString("MM/dd/yyyy");

                
                connectionString = connectData.ChuoiKetNoi();
               // connectionString = "Data Source=XUBI-LAP;Initial Catalog=QLBV;Integrated Security=True";
                cnn = new SqlConnection(connectionString);
                cnn.Open();
                /*
                sql = "SELECT HOADON.STT, HOADON.MaHoaDon, HOADON.NgayLap, HOADON.ThanhTien, HOADON.MaDichVu," +
			                    "DICHVU.TenDichVu, DICHVU.DonGia "+
	                   "FROM HOADON, DICHVU "+
	                   "WHERE (HOADON.MaDichVu = DICHVU.MaDichVu) AND "+
                                "(HOADON.NgayLap >= " + "'" + tungay + "'" + ") AND (HOADON.NgayLap <= " + "'" + denngay + "'" + ")" +
	                    "ORDER BY HOADON.STT";
                 */
                 sql = "EXEC BAOCAO_DOANHTHU '" + tungay + "','" + denngay + "'";
                SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);
                QLBVDataSet ds = new QLBVDataSet();
                dscmd.Fill(ds, "BAOCAO_DOANHTHU");
                //MessageBox.Show(ds.Tables[1].Rows.Count.ToString());
                cnn.Close();



                rpBaoCaoDoanhThu objRpt = new rpBaoCaoDoanhThu();

                
                //objRpt.SetDataSource(ds.Tables[0]);
                //objRpt.Database.Tables["BAOCAO_DOANHTHU"].SetDataSource(ds);
                objRpt.SetDataSource((DataTable)ds.BAOCAO_DOANHTHU);
                try
                {
                    string tongtien = ds.Tables["BAOCAO_DOANHTHU"].Rows[0].ItemArray[0].ToString();

                    double tien = double.Parse(tongtien);


                    ParameterValues a = new ParameterValues();
                    ParameterDiscreteValue b = new ParameterDiscreteValue();
                    b.Value = dtpTuNgay4.Text;
                    a.Add(b);
                    objRpt.DataDefinition.ParameterFields["TUNGAY"].ApplyCurrentValues(a);

                    b.Value = dtpDenNgay4.Text;
                    a.Add(b);
                    objRpt.DataDefinition.ParameterFields["DENNGAY"].ApplyCurrentValues(a);

                    b.Value = So_chu(tien).ToString();
                    a.Add(b);
                    objRpt.DataDefinition.ParameterFields["TongTien1"].ApplyCurrentValues(a);

                    crystalReportViewer4.ReportSource = objRpt;
                    crystalReportViewer4.Refresh();
                }
                catch 
                {
                    MessageBox.Show( "Không có hóa đơn thuộc khoảng thời gian đã chọn!");
                }
            }

            // in danh sach bac si
            if (btnTongSo.Enabled == true && btnKhoa.Enabled == false)
            {
                SqlConnection cnn;
                string connectionString = null;
                string sql = null;

                connectionString = connectData.ChuoiKetNoi();
                // connectionString = "Data Source=XUBI-LAP;Initial Catalog=QLBV;Integrated Security=True";
                cnn = new SqlConnection(connectionString);
                cnn.Open();

                sql = "EXEC BAOCAO_DANHSACHBACSI ";
                SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);
                QLBVDataSet ds = new QLBVDataSet();
                dscmd.Fill(ds, "BAOCAO_DANHSACHBACSI");
                //MessageBox.Show(ds.Tables[1].Rows.Count.ToString());
                cnn.Close();

                rpDanhSachBacSi objRpt = new rpDanhSachBacSi();

                //objRpt.SetDataSource(ds.Tables[0]);
                objRpt.Database.Tables["BAOCAO_DANHSACHBACSI"].SetDataSource(ds);
                //objRpt.SetDataSource((DataTable)ds.BAOCAO_BENHNHAN);
               

                crystalReportViewer4.ReportSource = objRpt;
                crystalReportViewer4.Refresh();
            }

            // IN DANH SACH BAC SI THEO KHOA
            if (btnTongSo.Enabled == false && btnKhoa.Enabled == true)
            {
                SqlConnection cnn;
                string connectionString = null;
                string sql = null;

                connectionString = connectData.ChuoiKetNoi();
                // connectionString = "Data Source=XUBI-LAP;Initial Catalog=QLBV;Integrated Security=True";
                cnn = new SqlConnection(connectionString);
                cnn.Open();

                sql = "EXEC BAOCAO_DANHSACHBACSI ";
                SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);
                QLBVDataSet ds = new QLBVDataSet();
                dscmd.Fill(ds, "BAOCAO_DANHSACHBACSI");
                //MessageBox.Show(ds.Tables[1].Rows.Count.ToString());
                cnn.Close();

                rpDanhSachBacSi1 objRpt = new rpDanhSachBacSi1();

                //objRpt.SetDataSource(ds.Tables[0]);
                objRpt.Database.Tables["BAOCAO_DANHSACHBACSI"].SetDataSource(ds);
                //objRpt.SetDataSource((DataTable)ds.BAOCAO_BENHNHAN);


                crystalReportViewer4.ReportSource = objRpt;
                crystalReportViewer4.Refresh();
            }

 
        }

        #endregion


        #region Cac button 

        private void btnKham_Click(object sender, EventArgs e)
        {
            dtpDenNgay1.Visible = true;
            dtpDenNgay2.Visible = false;
            dtpDenNgay3.Visible = false;
            dtpDenNgay4.Visible = false;

            dtpTuNgay1.Visible = true;
            dtpTuNgay2.Visible = false;
            dtpTuNgay3.Visible = false;
            dtpTuNgay4.Visible = false;

            lblDenNgay.Visible = true;
            lblTuNgay.Visible = true;
            lblDenNgay1.Visible = false;
            lblTuNgay1.Visible = false;

            btnKhoa.Enabled = false;
            btnTongSo.Enabled = false;

            cboKhoa.Enabled = false;
            cboTongSo.Enabled = false;
        }

        private void btnXetNghiem_Click(object sender, EventArgs e)
        {
            dtpDenNgay1.Visible = false;
            dtpDenNgay2.Visible = true;
            dtpDenNgay3.Visible = false;
            dtpDenNgay4.Visible = false;

            dtpTuNgay1.Visible = false;
            dtpTuNgay2.Visible = true;
            dtpTuNgay3.Visible = false;
            dtpTuNgay4.Visible = false;

            lblDenNgay.Visible = true;
            lblTuNgay.Visible = true;
            lblDenNgay1.Visible = false;
            lblTuNgay1.Visible = false;

            btnKhoa.Enabled = false;
            btnTongSo.Enabled = false;

            cboKhoa.Enabled = false;
            cboTongSo.Enabled = false;
        }

        private void btnXquang_Click(object sender, EventArgs e)
        {
            dtpDenNgay1.Visible = false;
            dtpDenNgay2.Visible = false;
            dtpDenNgay3.Visible = true;
            dtpDenNgay4.Visible = false;

            dtpTuNgay1.Visible = false;
            dtpTuNgay2.Visible = false;
            dtpTuNgay3.Visible = true;
            dtpTuNgay4.Visible = false;

            lblDenNgay.Visible = true;
            lblTuNgay.Visible = true;
            lblDenNgay1.Visible = false;
            lblTuNgay1.Visible = false;

            btnKhoa.Enabled = false;
            btnTongSo.Enabled = false;

            cboKhoa.Enabled = false;
            cboTongSo.Enabled = false;
        }

        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            dtpDenNgay1.Visible = false;
            dtpDenNgay2.Visible = false;
            dtpDenNgay3.Visible = false;
            dtpDenNgay4.Visible = true;

            dtpTuNgay1.Visible = false;
            dtpTuNgay2.Visible = false;
            dtpTuNgay3.Visible = false;
            dtpTuNgay4.Visible = true;

            lblDenNgay.Visible = false;
            lblTuNgay.Visible = false;
            lblDenNgay1.Visible = true;
            lblTuNgay1.Visible = true;

            btnKhoa.Enabled = false;
            btnTongSo.Enabled = false;

            cboKhoa.Enabled = false;
            cboTongSo.Enabled = false;
        }

        private void btnSoLuong_Click_1(object sender, EventArgs e)
        {
            dtpDenNgay1.Visible = false;
            dtpDenNgay2.Visible = false;
            dtpDenNgay3.Visible = false;
            dtpDenNgay4.Visible = false;

            dtpTuNgay1.Visible = false;
            dtpTuNgay2.Visible = false;
            dtpTuNgay3.Visible = false;
            dtpTuNgay4.Visible = false;

            btnKhoa.Enabled = true;
            btnTongSo.Enabled = false;

            lblDenNgay.Visible = false;
            lblTuNgay.Visible = false;
            lblDenNgay1.Visible = false;
            lblTuNgay1.Visible = false;

            cboKhoa.Visible = false;
            cboTongSo.Visible = false;
            btnTongSo_Click(sender, e);
        }

        #endregion

        private void btnTongSo_Click(object sender, EventArgs e)
        {
            btnKhoa.Enabled = false;
            btnTongSo.Enabled = true;
        }
    

        private void btnKhoa_Click(object sender, EventArgs e)
        {
            btnTongSo.Enabled = false;
            btnKhoa.Enabled = true;
        }
        

        private void btnTheoKhoa_Click(object sender, EventArgs e)
        {
            dtpDenNgay1.Visible = false;
            dtpDenNgay2.Visible = false;
            dtpDenNgay3.Visible = false;
            dtpDenNgay4.Visible = false;

            dtpTuNgay1.Visible = false;
            dtpTuNgay2.Visible = false;
            dtpTuNgay3.Visible = false;
            dtpTuNgay4.Visible = false;

            btnKhoa.Enabled = true;
            btnTongSo.Enabled = false;

            lblDenNgay.Visible = false;
            lblTuNgay.Visible = false;
            lblDenNgay1.Visible = false;
            lblTuNgay1.Visible = false;

            cboKhoa.Visible = false;
            cboTongSo.Visible = false;
            btnKhoa_Click(sender, e);
        }

        #endregion

        private void label144_Click(object sender, EventArgs e)
        {

        }

        private void rbDSNhapVien_Click(object sender, EventArgs e)
        {
            LoadDsBenhNhanNhapVien();
        }

        private void groupPanel23_Click(object sender, EventArgs e)
        {

        }

        BNNhapVien bnnv = new BNNhapVien();

        public void LoadDsBenhNhanNhapVien()
        {
            dtGridView.DataSource = bnnv.selectBNNV();
        }
    }
}

