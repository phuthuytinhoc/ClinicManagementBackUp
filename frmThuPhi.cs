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
    public partial class frmThuPhi : Form
    {
        public frmThuPhi()
        {
            InitializeComponent();
        }

        private ThuPhiBNBUS ThuPhiBN = new ThuPhiBNBUS();
        private HinhAnhBUS ThemHinhAnh = new HinhAnhBUS();

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

        #region Cac ham lien quan

        public void AddTextThuPhi(TextBox mabenhnhan)
        {
            txtMaBenhNhan.Text = mabenhnhan.Text;
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

        public bool KiemTraNhapLieuThuPhi()
        {
            if (txtMaPhieuThu.Text == "" || txtTenDichVu.Text == "" || txtDonGia.Text == "")
                return false;
            return true;
        }

        private ThuPhi LayThongTinThuPhi()
        {
            ThuPhi tp = new ThuPhi();

            tp.MaHoaDon = txtMaPhieuThu.Text;
            tp.MaBenhAn = txtMaBenhAn.Text;
            tp.MaDichVu = cboTenDichVu.SelectedValue.ToString();
            tp.MaNhanVien = txtMaNhanVien.Text;
            tp.Ngaylap = Convert.ToDateTime(dtPickerNgayThu.Value.ToString());
            tp.ThanhTien = Convert.ToInt64(txtDonGia.Text);

            return tp;
        }

        #endregion

        #region Cac Button

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuuThongTin_Click(object sender, EventArgs e)
        {
            if (KiemTraNhapLieuThuPhi())
            {
                ThuPhi tp = LayThongTinThuPhi();
                if (ThuPhiBN.KiemTraTrungMaHoaDon(txtMaPhieuThu.Text))
                {
                    if(ThuPhiBN.SuaDoiThongTinHoaDon(tp))
                        MessageBox.Show("Sửa đổi thông tin dịch vụ bệnh nhân :{" + txtMaBenhNhan.Text + "} thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Sửa đổi thông tin dịch vụ bệnh nhân :{" + txtMaBenhNhan.Text + "} thất bại!", "Thông báo",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (ThuPhiBN.LuuThongTinHoaDon(tp))
                        MessageBox.Show("Lưu thông tin dịch vụ bệnh nhân :{" + txtMaBenhNhan.Text + "} thành công!","Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                   /* else
                        MessageBox.Show("Lưu thông tin dịch vụ bệnh nhân :{" + txtMaBenhNhan.Text + "} thất bại!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);   */
                }
            }
            else
                MessageBox.Show("Chưa nhập đủ thông tin dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);            
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            cboTenDichVu.Text = "";
            txtTenDichVu.Text = "";
            txtDonGia.Text = "";
            dtPickerNgayThu.Text = "1/1/2011";
            dtPicker_GioThu.Text = "12:00:00";
            txtMaPhieuThu.Text = "";
            txtMaPhieuThu.Text = ThuPhiBN.NextIDThuPhi();
        }

        #endregion

        #region Cac event lien quan

        private void frmThuPhi_Load(object sender, EventArgs e)
        {
            cboTenDichVu.DataSource = ThuPhiBN.LoadMaDichVu();
            cboTenDichVu.DisplayMember = "MaDV";
            cboTenDichVu.ValueMember = "MaDichVu";
            txtMaPhieuThu.Text = ThuPhiBN.NextIDThuPhi();
            txtMaNhanVien.Text = Utilities.user.MaNguoiDung;
            txtHoNhanVien.Text = Utilities.user.Ho;
            txtTenNhanVien.Text = Utilities.user.Ten;
        }

        private void txtMaBenhNhan_TextChanged(object sender, EventArgs e)
        {
            txtHoBenhNhan.DataBindings.Clear();
            txtTenBenhNhan.DataBindings.Clear();
            txtGioiTinh.DataBindings.Clear();
            txtNgheNghiep.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            dtPicker_NgaySinh.DataBindings.Clear();
            txtSDT.DataBindings.Clear();
            txtCMND.DataBindings.Clear();
            txtMaDoiTuong.DataBindings.Clear();
            txtMaBenhAn.DataBindings.Clear();

            txtHoBenhNhan.DataBindings.Add("text", ThuPhiBN.LayThongTinBenhNhan(txtMaBenhNhan.Text), "Ho");
            txtTenBenhNhan.DataBindings.Add("text", ThuPhiBN.LayThongTinBenhNhan(txtMaBenhNhan.Text), "Ten");
            txtGioiTinh.DataBindings.Add("text", ThuPhiBN.LayThongTinBenhNhan(txtMaBenhNhan.Text), "GioiTinh");
            txtNgheNghiep.DataBindings.Add("text", ThuPhiBN.LayThongTinBenhNhan(txtMaBenhNhan.Text), "NgheNghiep");
            txtDiaChi.DataBindings.Add("text", ThuPhiBN.LayThongTinBenhNhan(txtMaBenhNhan.Text), "DiaChi");
            dtPicker_NgaySinh.DataBindings.Add("text", ThuPhiBN.LayThongTinBenhNhan(txtMaBenhNhan.Text), "NgaySinh");
            txtSDT.DataBindings.Add("text", ThuPhiBN.LayThongTinBenhNhan(txtMaBenhNhan.Text), "SDT");
            txtCMND.DataBindings.Add("text", ThuPhiBN.LayThongTinBenhNhan(txtMaBenhNhan.Text), "CMND");
            txtMaDoiTuong.DataBindings.Add("text", ThuPhiBN.LayThongTinBenhNhan(txtMaBenhNhan.Text), "MaDoiTuong");
            txtMaBenhAn.DataBindings.Add("text", ThuPhiBN.LayThongTinBenhNhan(txtMaBenhNhan.Text), "MaBenhAn");

            LoadAnhTuDB(txtMaBenhNhan.Text, picBenhNhan);
        }

        private void txtMaDoiTuong_TextChanged(object sender, EventArgs e)
        {
            txtTenDoiTuong.DataBindings.Clear();
            txtTenDoiTuong.DataBindings.Add("text", ThuPhiBN.LayTenDoiTuong(txtMaDoiTuong.Text), "TenDoiTuong");
        }

        private void cboTenDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenDichVu.DataBindings.Clear();
            txtDonGia.DataBindings.Clear();

            txtTenDichVu.DataBindings.Add("text", ThuPhiBN.LayTenDichVuDonGia(cboTenDichVu.Text), "TenDichVu");
            txtDonGia.DataBindings.Add("text", ThuPhiBN.LayTenDichVuDonGia(cboTenDichVu.Text), "DonGia");

        }



        #endregion

        private void dtPickerNgayThu_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime kiemtra = dtPickerNgayThu.Value;
            if ((DateTime.Parse(now.ToString()) > DateTime.Parse(kiemtra.ToString())))
            {
                dtPickerNgayThu.Text = "";

            }
        }

        private void btnInPhieu_Click_1(object sender, EventArgs e)
        {
            expandableSplitter1.Expanded = true;
            btnInPhieu.Visible = false;
            btnTroLai.Visible = true;

            PhieuThu rp = new PhieuThu();
            ParameterValues a = new ParameterValues();
            ParameterDiscreteValue b = new ParameterDiscreteValue();
            b.Value = txtHoBenhNhan.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HoBenhNhan"].ApplyCurrentValues(a);

            b.Value = txtTenBenhNhan.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenBenhNhan"].ApplyCurrentValues(a);

            b.Value = dtPicker_NgaySinh.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgaySinh"].ApplyCurrentValues(a);

            b.Value = txtNgheNghiep.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["NgheNghiep"].ApplyCurrentValues(a);

            b.Value = txtDiaChi.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["DiaChi"].ApplyCurrentValues(a);

            b.Value = txtTenDichVu.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenDichVu"].ApplyCurrentValues(a);

            b.Value = txtDonGia.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["DonGia"].ApplyCurrentValues(a);

            b.Value = txtMaPhieuThu.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["MaPhieuThu"].ApplyCurrentValues(a);

            b.Value = txtHoNhanVien.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["HoNhanVien"].ApplyCurrentValues(a);

            b.Value = txtTenNhanVien.Text;
            a.Add(b);
            rp.DataDefinition.ParameterFields["TenNhanVien"].ApplyCurrentValues(a);

            string tongtien = txtDonGia.Text.ToString();
            long tien = long.Parse(tongtien);
            So_chu(tien);

            b.Value = So_chu(tien).ToString();
            a.Add(b);
            rp.DataDefinition.ParameterFields["TongTien"].ApplyCurrentValues(a);

            crystalReportViewer1.ReportSource = rp;
        }

        private void btnTroLai_Click_1(object sender, EventArgs e)
        {
            expandableSplitter1.Expanded = false;
            btnInPhieu.Visible = true;
            btnTroLai.Visible = false;
        }


    }
}
