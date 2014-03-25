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
using QuanLyBenhVien.DataAccess;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace QuanLyBenhVien
{
    public partial class frmDonThuoc : Form
    {
        public frmDonThuoc()
        {
            InitializeComponent();
            expandableSplitter1.Visible = false;
            btnTroLai.Visible = false;
        }

        private DonThuocBNBUS DonThuocBN = new DonThuocBNBUS();
        private ConnectData connectData = new ConnectData();

        #region Cac Ham Lien quan trong Don Thuoc

        public void LayMaBenhNhan(TextBox MaBenhNhan)
        {
            txtDonThuoc_MaBenhNhan.Text = MaBenhNhan.Text;
        }

        public void LayMaBacSi(TextBox MaBacSi)
        {
            txtDonThuoc_MaBacSi.Text = MaBacSi.Text;
        }

        //Lay thong tin don thuoc luu vao DONTHUOC
        private DonThuoc LayThongTinDonThuoc()
        {
            DonThuoc dt = new DonThuoc();

            dt.MaBacSi = txtDonThuoc_MaBacSi.Text;
            dt.MaDonThuoc = txtDonThuoc_MaDonThuoc.Text;
            dt.MaBacSi = txtDonThuoc_MaBacSi.Text;
            dt.MaBenhNhan = txtDonThuoc_MaBenhNhan.Text;
            dt.MaBacSi = txtDonThuoc_MaBacSi.Text;
            dt.MaThuoc = txtDonThuoc_MaThuoc.Text;
            dt.NgayTao = Convert.ToDateTime(dtPickerDonThuoc_NgayTao.Value.ToString());
            dt.GioTao = Convert.ToDateTime(dtPicker_GioTaoDonThuoc.Value.ToString());
            dt.LoiDan = txtDonThuoc_LoiDan.Text;

            return dt;
        }

        //ham lay thong tin chi tiet don thuoc
        private DonThuoc LayThongTinChiTietThuoc()
        {
            DonThuoc dt = new DonThuoc();

            dt.MaDonThuoc = txtDonThuoc_MaDonThuoc.Text;
            dt.MaThuoc = txtDonThuoc_MaThuoc.Text;
            dt.MaBenhNhan = txtDonThuoc_MaBenhNhan.Text;
            dt.SoLuong = Convert.ToInt32(txtDonThuoc_SoLuong.Text);
            dt.Ngay1 = cboDonThuoc_UongNho.Text;
            dt.Ngay2 = Convert.ToInt32(txtDonThuoc_SoLanUongNho.Text);
            dt.MoiLan1 = Convert.ToInt32(txtDonThuoc_MoiLan.Text);
            dt.MoiLan2 = cboDonThuoc_VienGiot.Text;
            dt.ThoiDiemSuDung = txtDonThuoc_ThoiDiemSuDung.Text;

            return dt;
        }

        //ham reset them thuoc moi
        public void ThemThuocMoi()
        {       
            txtDonThuoc_SoLuong.Text = "";           
            txtDonThuoc_SoLanUongNho.Text = "";
            txtDonThuoc_ThoiDiemSuDung.Text = "";
            txtDonThuoc_MoiLan.Text = "";            

            cboDonThuoc_UongNho.Text = "";
            cboDonThuoc_VienGiot.Text = "";
            cboDonThuoc_NhomThuoc.Text = "";
            cboDonThuoc_TenThuoc.Text = "";
        }

        //Ham load du lieu len datagridview
        public void LoadDuLieuDSThuocDataGridView()
        {
            dgViewDonThuoc_DSThuoc.DataSource = DonThuocBN.DSTenThuocDatagirdView(txtDonThuoc_MaBenhNhan.Text);
        }

        //Ham kiem tra thong tin nhap lieu truoc khi luu thong tin
        public bool KiemTraNhapLieuDonThuoc()
        {
            if (txtDonThuoc_ThoiDiemSuDung.Text == "" || txtDonThuoc_SoLuong.Text == "" ||
                txtDonThuoc_SoLanUongNho.Text == "" || txtDonThuoc_MoiLan.Text == "")
                return false;
            else
                return true;
        }

        public void LoadComboboxVienGiot()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("STT");
            dt.Columns.Add("VienGiot");

            dt.Rows.Add("Viên");
            dt.Rows.Add("Giọt");

            //Load combobox loai thuoc uong
            cboDonThuoc_VienGiot.DataSource = dt;
            cboDonThuoc_VienGiot.DisplayMember = "STT";
            cboDonThuoc_VienGiot.ValueMember = "VienGiot";

        }

        public void LoadComboboxUongNho()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("STT");
            dt.Columns.Add("UongNho");

            dt.Rows.Add("Uống");
            dt.Rows.Add("Nhỏ");

            //cach su dung thuoc
            cboDonThuoc_UongNho.DataSource = dt;
            cboDonThuoc_UongNho.DisplayMember = "STT";
            cboDonThuoc_UongNho.ValueMember = "UongNho";
        }


        #endregion


        #region Cac Buttion Don Thuoc

        //Dong form
        private void btnDonThuoc_Dong_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Lưu lại thông tin đơn thuốc bệnh nhân(Yes).\n\n Xóa thông tin đơn thuốc bệnh nhân(No)","Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (DonThuocBN.KiemTraMaBenhNhanDaCoTrongDonThuoc(txtDonThuoc_MaBenhNhan.Text))
                {
                    DonThuoc dt1 = LayThongTinDonThuoc();
                    DonThuocBN.CapNhapDonThuoc(dt1);
                    this.Close();
                }
                else
                    MessageBox.Show("Bệnh nhân này chưa có đơn thuốc để lưu, hãy tạo mới đơn thuốc!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);              
            }
            else
            {
                DonThuocBN.XoaDonThuocBN(txtDonThuoc_MaDonThuoc.Text);
                this.Close();
            }
        }

        //Button them thuoc moi vao danh sach
        private void btnDonThuoc_ThemThuoc_Click(object sender, EventArgs e)
        {
            if (DonThuocBN.KiemTraTrungMaDonThuoc(txtDonThuoc_MaDonThuoc.Text))
            {
                if (KiemTraNhapLieuDonThuoc())
                {
                    if (connectData.KiemTraTrungMaThuoc(txtDonThuoc_MaThuoc.Text, txtDonThuoc_MaDonThuoc.Text) == false)
                    {
                        DonThuoc dt = LayThongTinChiTietThuoc();
                        DonThuocBN.LuuThongTinChiTietDonThuoc(dt);
                        LoadDuLieuDSThuocDataGridView();
                        ThemThuocMoi();
                        ResetSTTCacThuoc();
                    }
                    else
                        MessageBox.Show("Thuốc này đã có trong đơn thuốc, mời chọn thuốc khác", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);                   
                }
                else
                    MessageBox.Show("Chưa nhập thông tin cách dùng thuốc, thêm thuốc thất bại!","Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else                  
                ThemThuocMoi();       
        }

        //luu thong tin don thuoc
        private void btnDonThuoc_Luu_Click(object sender, EventArgs e)
        {
            DonThuoc dt = LayThongTinDonThuoc();
            if (DonThuocBN.KiemTraTrungMaDonThuoc(txtDonThuoc_MaDonThuoc.Text))
            {
                DonThuocBN.CapNhapDonThuoc(dt);
                MessageBox.Show("Cập nhật thông tin đơn thuốc bệnh nhân {" + txtDonThuoc_MaBenhNhan.Text + "} thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (DonThuocBN.LuuThongTinDonThuoc(dt))
                {
                    MessageBox.Show("Lưu thông tin đơn thuốc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Lưu thông tin đơn thuốc thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Xoa thuoc vua them vao
        private void btnDonThuoc_XoaThuocVuaThem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xóa thuốc: {" + cboDonThuoc_TenThuoc.Text + "} ra khỏi danh sách thuốc?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (txtDonThuoc_MaThuoc.Text != "")
            {
                if (result == DialogResult.Yes)
                {
                    DonThuocBN.XoaThuocKhoiDSThuoc(txtDonThuoc_MaThuoc.Text);
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDuLieuDSThuocDataGridView();
                    ResetSTTCacThuoc();
                }
            }
            else
                MessageBox.Show("Chưa chọn thuốc để xóa!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //button tao moi don thuoc
        private void btnDonThuoc_TaoMoiDonThuoc_Click(object sender, EventArgs e)
        {
            txtDonThuoc_MaDonThuoc.Text = DonThuocBN.NextIDDonThuoc();
            GPanelThongTinhDonThuoc.Enabled = true;
            DonThuoc dt1 = LayThongTinDonThuoc();
            DonThuocBN.TaoMoiDonThuocBN(dt1);
        }

        #endregion


        #region Cac event lien quan


        #region event combobox

        //load ma thuoc tu ten thuoc
        private void cboDonThuoc_TenThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDonThuoc_MaThuoc.DataBindings.Clear();

            txtDonThuoc_MaThuoc.DataBindings.Add("text", DonThuocBN.LayMaThuocTuTenThuoc(cboDonThuoc_TenThuoc.Text), "MaThuoc");
        }

        private void cboDonThuoc_NhomThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //load combobox ten thuoc tu nhom thuoc
            cboDonThuoc_TenThuoc.DataSource = DonThuocBN.LayTenThuocTuNhomThuoc(cboDonThuoc_NhomThuoc.Text);
            cboDonThuoc_TenThuoc.DisplayMember = "TenCacLoaiThuoc";
            cboDonThuoc_TenThuoc.ValueMember = "TenThuoc";
        }

        //load thong tin bac si
        private void txtDonThuoc_MaBacSi_TextChanged(object sender, EventArgs e)
        {
            txtDonThuoc_HoBacSi.DataBindings.Clear();
            txtDonThuoc_TenBacSi.DataBindings.Clear();

            txtDonThuoc_HoBacSi.DataBindings.Add("text", DonThuocBN.LayHoTenBacSiTuMaBS(txtDonThuoc_MaBacSi.Text), "Ho");
            txtDonThuoc_TenBacSi.DataBindings.Add("text", DonThuocBN.LayHoTenBacSiTuMaBS(txtDonThuoc_MaBacSi.Text), "Ten");
        }

        //load thong tin benh nhan
        private void txtDonThuoc_MaBenhNhan_TextChanged(object sender, EventArgs e)
        {
            frmGiaoDienChung frm = new frmGiaoDienChung();
            txtDonThuoc_HoBN.DataBindings.Clear();
            txtDonThuoc_TenBN.DataBindings.Clear();
            txtDonThuoc_GioiTinhBN.DataBindings.Clear();
            txtDonThuoc_NhomMauBN.DataBindings.Clear();
            dtPickerDonThuoc_NgaySinh.DataBindings.Clear();
            txtDonThuoc_MaDonThuoc.DataBindings.Clear();

            
            txtDonThuoc_HoBN.DataBindings.Add("text", DonThuocBN.LayThongTinDonThuocBenhNhan(txtDonThuoc_MaBenhNhan.Text), "Ho");
            txtDonThuoc_TenBN.DataBindings.Add("text", DonThuocBN.LayThongTinDonThuocBenhNhan(txtDonThuoc_MaBenhNhan.Text), "Ten");
            txtDonThuoc_GioiTinhBN.DataBindings.Add("text", DonThuocBN.LayThongTinDonThuocBenhNhan(txtDonThuoc_MaBenhNhan.Text), "GioiTinh");
            txtDonThuoc_NhomMauBN.DataBindings.Add("text", DonThuocBN.LayThongTinDonThuocBenhNhan(txtDonThuoc_MaBenhNhan.Text), "NhomMau");
            dtPickerDonThuoc_NgaySinh.DataBindings.Add("text", DonThuocBN.LayThongTinDonThuocBenhNhan(txtDonThuoc_MaBenhNhan.Text), "NgaySinh");
            txtDonThuoc_MaDonThuoc.DataBindings.Add("text", DonThuocBN.LayMaDonThuocCuaBenhNhan(txtDonThuoc_MaBenhNhan.Text), "MaDonThuoc");
        }

        //Form Load
        private void frmDonThuoc_Load(object sender, EventArgs e)
        {
            if (DonThuocBN.KiemTraMaBenhNhanDaCoTrongDonThuoc(txtDonThuoc_MaBenhNhan.Text))
                GPanelThongTinhDonThuoc.Enabled = true;
            else
                GPanelThongTinhDonThuoc.Enabled = false;      
   
            //load combobox Nhom Thuoc
            cboDonThuoc_NhomThuoc.DataSource = DonThuocBN.DSNhomThuoc();
            cboDonThuoc_NhomThuoc.DisplayMember = "TenNhomThuoc";
            cboDonThuoc_NhomThuoc.ValueMember = "NhomThuoc";

            //load combobox ten thuoc tu nhom thuoc
            cboDonThuoc_TenThuoc.DataSource = DonThuocBN.LayTenThuocTuNhomThuoc(cboDonThuoc_NhomThuoc.Text);
            cboDonThuoc_TenThuoc.DisplayMember = "TenCacLoaiThuoc";
            cboDonThuoc_TenThuoc.ValueMember = "TenThuoc";

            LoadComboboxUongNho();
            LoadComboboxVienGiot();

            dgViewDonThuoc_DSThuoc.DataSource = DonThuocBN.DSTenThuocDatagirdView(txtDonThuoc_MaBenhNhan.Text);
            ResetSTTCacThuoc();
        }


        #endregion


        #region Event cua textbox

        private void txtDonThuoc_SoLanUongNho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtDonThuoc_MoiLan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtDonThuoc_SoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtDonThuoc_ThoiDiemSuDung_TextChanged(object sender, EventArgs e)
        {
            if (txtDonThuoc_ThoiDiemSuDung.Text == "")
                lblDonThuoc_WarnThoiDiemSuDung.Show();
            else
                lblDonThuoc_WarnThoiDiemSuDung.Hide();
        }

        private void txtDonThuoc_SoLuong_TextChanged(object sender, EventArgs e)
        {
            if (txtDonThuoc_SoLuong.Text == "")
                lblDonThuoc_WarnSoLuong.Show();
            else
                lblDonThuoc_WarnSoLuong.Hide();
        }

        private void txtDonThuoc_SoLanUongNho_TextChanged(object sender, EventArgs e)
        {
            if (txtDonThuoc_SoLanUongNho.Text == "")
                lblDonThuoc_WarnNgay2.Show();
            else
                lblDonThuoc_WarnNgay2.Hide();
        }

        private void txtDonThuoc_MoiLan_TextChanged(object sender, EventArgs e)
        {
            if (txtDonThuoc_MoiLan.Text == "")
                lblDonThuoc_WarnMoiLan1.Show();
            else
                lblDonThuoc_WarnMoiLan1.Hide();
        }

      

        #endregion

        private void dgViewDonThuoc_DSThuoc_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            ResetSTTCacThuoc();
            if (dong != -1)
            {
                cboDonThuoc_NhomThuoc.SelectedValue = dgViewDonThuoc_DSThuoc.Rows[dong].Cells["NhomThuoc"].Value.ToString();
                cboDonThuoc_TenThuoc.SelectedValue = dgViewDonThuoc_DSThuoc.Rows[dong].Cells["TenThuoc"].Value.ToString();
                txtDonThuoc_ThoiDiemSuDung.Text = dgViewDonThuoc_DSThuoc.Rows[dong].Cells["ThoiDiemSuDung"].Value.ToString();
                txtDonThuoc_SoLuong.Text = dgViewDonThuoc_DSThuoc.Rows[dong].Cells["SoLuong"].Value.ToString();
            }
        }


        #endregion

        private void dtPickerDonThuoc_NgayTao_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime kiemtra = dtPickerDonThuoc_NgayTao.Value;
            if ((DateTime.Parse(now.ToString()) > DateTime.Parse(kiemtra.ToString())))
            {
                dtPickerDonThuoc_NgayTao.Text = "";

            }
        }


        public void ResetSTTCacThuoc()
        {
            int sodong = dgViewDonThuoc_DSThuoc.RowCount - 1;
            for (int i = 0; i <= sodong; i++)
            {
                dgViewDonThuoc_DSThuoc.Rows[i].Cells["STT"].Value = i + 1;
            }
        }

        private void txtDonThuoc_MaDonThuoc_TextChanged(object sender, EventArgs e)
        {
            txtDonThuoc_LoiDan.DataBindings.Clear();
            txtDonThuoc_LoiDan.DataBindings.Add("text", DonThuocBN.LayLoiDanCuaBS(txtDonThuoc_MaDonThuoc.Text), "LoiDan");            
        }

        private void btnDonThuoc_In_Click(object sender, EventArgs e)
        {
            btnDonThuoc_In.Visible = false;
            btnTroLai.Visible = true;
            expandableSplitter1.Expanded = true;

            SqlConnection cnn;
            string connectionString = null;
            string sql = null;

            connectionString = connectData.ChuoiKetNoi();
            cnn = new SqlConnection(connectionString);
            cnn.Open();

            string mabenhnhan = txtDonThuoc_MaBenhNhan.Text.ToString();

            sql = "EXEC CHITIETDONTHUOC_TENTHUOC1 '" + mabenhnhan + "'";

            //sql = "Select distinct c.NhomThuoc, b.MaThuoc, TenThuoc, b.SoLuong, ThoiDiemSuDung "+
            //        "from DONTHUOC as a, CHITIETDONTHUOC as b, THUOC as c, BENHNHAN as d "+ 
            //        "where (a.MaBenhNhan = '" txtDonThuoc_MaBenhNhan.Text +"') and (c.MaThuoc = b.MaThuoc) and (a.MaDonThuoc = b.MaDonThuoc)";
            SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);
            QLBVDataSet ds = new QLBVDataSet();
            dscmd.Fill(ds, "CHITIETDONTHUOC_TENTHUOC1");
            //MessageBox.Show(ds.Tables[1].Rows.Count.ToString());
            cnn.Close();

            rpDonThuoc objRpt = new rpDonThuoc();

            //objRpt.SetDataSource(ds.Tables[0]);
            objRpt.Database.Tables["CHITIETDONTHUOC_TENTHUOC1"].SetDataSource(ds);

            ParameterValues a = new ParameterValues();
            ParameterDiscreteValue b = new ParameterDiscreteValue();
            b.Value = txtDonThuoc_MaDonThuoc.Text;
            a.Add(b);
            objRpt.DataDefinition.ParameterFields["MaPhieuKham"].ApplyCurrentValues(a);

            b.Value = txtDonThuoc_HoBN.Text;
            a.Add(b);
            objRpt.DataDefinition.ParameterFields["HoBenhNhan"].ApplyCurrentValues(a);

            b.Value = txtDonThuoc_TenBN.Text;
            a.Add(b);
            objRpt.DataDefinition.ParameterFields["TenBenhNhan"].ApplyCurrentValues(a);

            b.Value = txtDonThuoc_GioiTinhBN.Text;
            a.Add(b);
            objRpt.DataDefinition.ParameterFields["GioiTinh"].ApplyCurrentValues(a);

            b.Value = dtPickerDonThuoc_NgaySinh.Text;
            a.Add(b);
            objRpt.DataDefinition.ParameterFields["NgaySinh"].ApplyCurrentValues(a);

            b.Value = txtDonThuoc_HoBacSi.Text;
            a.Add(b);
            objRpt.DataDefinition.ParameterFields["HoBacSi"].ApplyCurrentValues(a);

            b.Value = txtDonThuoc_TenBacSi.Text;
            a.Add(b);
            objRpt.DataDefinition.ParameterFields["TenBacSi"].ApplyCurrentValues(a);



            crystalReportViewer1.ReportSource = objRpt;
            crystalReportViewer1.Refresh();
        }

        private void btnTroLai_Click(object sender, EventArgs e)
        {
            expandableSplitter1.Expanded = false;
            btnTroLai.Visible = false;
            btnDonThuoc_In.Visible = true;
        }
  



    }
}
