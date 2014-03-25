using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace QuanLyBenhVien.DataAccess
{
    class ConnectData
    {
        private SqlConnection conn;
        private SqlDataAdapter dataAp;
        private DataTable dataTable;

        public ConnectData()
        {
            Connect();
        }

        public string ChuoiKetNoi()
        {
          // string Chuoi = @"Data Source= .\SQLEXPRESS;AttachDbFilename=" + Application.StartupPath + @"\Database\QLBV.mdf;"
            //+" Integrated Security=True;Connect Timeout=30;User Instance=True";
         //   string Chuoi = @"Data Source= " + Environment.MachineName + ";AttachDbFilename=" + Application.StartupPath + @"\Database\QLBV.mdf;"
         //+ " Integrated Security=True;Connect Timeout=30;User Instance=True";
            string Chuoi = @"Data Source=HUNG;Initial Catalog=QLBV;Integrated Security=True";
            //string Chuoi = @"Data Source =" + Environment.MachineName + ";Initial Catalog = QLBV; User Id = ; Password =";

            return Chuoi;
        }


        public void Connect()
        {
            string strConn = ChuoiKetNoi();

            try
            {
                conn = new SqlConnection(strConn);
                conn.Open();
                conn.Close();
            }
            catch (Exception ex)
            {
               MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        //lay du lie datatable tu cau truy van query
        public DataTable GetDataTable(string sql)
        {
            dataAp = new SqlDataAdapter(sql, conn);
            dataTable = new DataTable();
            dataAp.Fill(dataTable);
            return dataTable;
        }

        //ham thuc hien cau lenh insert update va delete
        public bool ExcuteQuery(string sql)
        {
            int numRecordsEffect = 0;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                numRecordsEffect = cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (numRecordsEffect > 0)
                return true;
            return false;
        }
       
        //Lay ma cuoi cung
        public string GetLastID(string nameTable, string nameField)
        {
            string sql = "Select TOP 1 " + nameField + " From " + nameTable + " Order by " + nameField + " Desc";
            if (GetDataTable(sql).Rows.Count != 0)
                return dataTable.Rows[0][nameField].ToString();
            else
                return "";
        }

        //KiemTra co trung ma benh nhan ko
        public bool KiemTra(string nameTable, string nameField, string value)
        {
            string sql = "Select * from " + nameTable + " Where " + nameField + " = '" + value + "'";
            GetDataTable(sql);

            //dem so dong tra ve neu >0 dong trung nhau thi sai, da ton tai value
            if (dataTable.Rows.Count > 0)
                return true;
            return false;
        }


        //Ham kien tra tai khoan da co hay chua
        public bool KiemtraTonTaiTaiKhoan(string TenTaiKhoan)
        {
            string sql = "Select * from USERS where Username = '" + TenTaiKhoan + "'";
            GetDataTable(sql);

            if (dataTable.Rows.Count > 0)
                return true; //co toan tai
            return false;//ko ton tai

        }

        //Ham kiem tra ma thuoc da them vao hay chua
        public bool KiemTraTrungMaThuoc(string MaThuoc, string MaDonThuoc)
        {
            string sql = "Select MaThuoc from CHITIETDONTHUOC where MaThuoc = '" + MaThuoc + "' and MaDonThuoc = '" + MaDonThuoc + "'";
            GetDataTable(sql);

            if (dataTable.Rows.Count > 0)
                return true; //co toan tai
            return false;//ko ton tai
        }

        public void BackUpDuLieu()
        {
            string strConn = ChuoiKetNoi();
            
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            string sqlBackup = "exec KetNoi ";
            SqlCommand comm = new SqlCommand(sqlBackup, conn);
            SqlDataReader reader = comm.ExecuteReader();
            conn.Close();
            MessageBox.Show("thanh cong");
        }
        
    }
        
}
