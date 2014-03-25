using System;
using System.Collections.Generic;
using System.Text;
using QuanLyBenhVien.DataAccess;
using System.Data;

namespace QuanLyBenhVien.BusinessLogic
{
    class BNNhapVien
    {
        ConnectData connect = new ConnectData();

        public DataTable selectBNNV()
        {
            string sql = "Select MaBenhNhan from NHAPVIEN";
            return connect.GetDataTable(sql);
        }

    }
}
