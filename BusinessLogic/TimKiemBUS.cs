using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using QuanLyBenhVien.DataAccess;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyBenhVien.BusinessLogic
{
    class TimKiemBUS
    {
        ConnectData conData = new ConnectData();
        QuanLiBacSiBUS BacSiBUS = new QuanLiBacSiBUS();

        public string connectionString;

        public SqlConnection conn;

        public TimKiemBUS()
        {
            connectionString = conData.ChuoiKetNoi();
            conn = new SqlConnection(connectionString);
        }




    }
}
