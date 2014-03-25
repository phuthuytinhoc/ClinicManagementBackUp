using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using QuanLyBenhVien.BusinessObject;
using QuanLyBenhVien.DataAccess;
using System.Windows.Forms;


namespace QuanLyBenhVien.BusinessLogic
{
    class HinhAnhBUS
    {
        ConnectData connData = new ConnectData();

        public SqlConnection conn;

        public HinhAnhBUS()
        {
            string connectionString = connData.ChuoiKetNoi();
            conn = new SqlConnection(connectionString);
        }

        public void StorePicture(string filename, string MaHinhAnh)
        {
            string connectionString = connData.ChuoiKetNoi();
            byte[] imageData = null;

            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                imageData = new Byte[fs.Length];
                fs.Read(imageData, 0, (int)fs.Length);
            }
             using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("InsertImage", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@filename", filename);
                    cmd.Parameters["@filename"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@MaHinhAnh", MaHinhAnh);
                    cmd.Parameters["@MaHinhAnh"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("@blobdata", SqlDbType.Image);
                    cmd.Parameters["@blobdata"].Direction = ParameterDirection.Input;

                    cmd.Parameters["@blobdata"].Value = imageData;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

            /* 
              string sql = string.Format("Insert into tblImages(MaHinhAnh, filename, blobdata) values ('{0}', N'{1}', '{2}') ",
               MaHinhAnh, filename, imageData);
              connData.ExcuteQuery(sql);
           *     */
        }


        public byte[] RetrieveImage(string MaHinhAnh)
        {
            byte[] imageData = null;
            conn.Open();

            SqlCommand cmd = new SqlCommand("Select blobdata from tblImages where MaHinhAnh = '" + MaHinhAnh + "'", conn);

            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
            {
                reader.Read();
                long bytesize = reader.GetBytes(0, 0, null, 0, 0);
                imageData = new byte[bytesize];
                long bytesread = 0;
                int curpos = 0;
                int chunkSize = 1;

                while (bytesread < bytesize)
                {
                    bytesread += reader.GetBytes(0, curpos, imageData, curpos, chunkSize);
                    curpos += chunkSize;
                }
            }
            conn.Close();

            return imageData;
        }

        public void LuuMaAnhBenhNhan(HinhAnh ha)
        {
            string sql = string.Format("Insert into tblImages(MaHinhAnh) values('{0}')", ha.MaHinhAnh);

            connData.ExcuteQuery(sql);
        }


        public bool KiemTraMaHinhAnh(string MaHinhAnh)
        {
            if (connData.KiemTra("tblImages", "MaHinhAnh", MaHinhAnh))
                return true;
            return false;
        }


        //ham xoa hinh anh 
        public void XoaHinhAnh(string MaHinhAnh)
        {
            string sql = "Delete from tblImages where MaHinhAnh ='" + MaHinhAnh + "'";
            connData.ExcuteQuery(sql);
        }

    }
}

