using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using QuanLyBenhVien.BusinessObject;
using System.Security.Cryptography;


namespace QuanLyBenhVien
{
    class Utilities
    {
        public static User user = new User();

        public static string NextID(string lastID, string prefixID)
        {
            if (lastID == "" && prefixID.Length == 2)
                return prefixID + "00001";
            else
            {
                if (lastID == "" && prefixID.Length == 3)
                    return prefixID + "0001";
                else
                {
                    int nextID = int.Parse(lastID.Remove(0, prefixID.Length)) + 1;
                    int lengthNumberID = lastID.Length - prefixID.Length;

                    string zeroNumber = "";
                    for (int i = 1; i <= lengthNumberID; i++)
                    {
                        if (nextID < Math.Pow(10, i))
                        {
                            for (int j = 1; j <= lengthNumberID - i; i++)
                            {
                                zeroNumber += "0";
                            }
                            return prefixID + zeroNumber + nextID.ToString();
                        }
                    }
                    return prefixID + nextID;
                }
            }

        }

        //Ham ma hoa mat khau
        public static string MaHoaMD5(string text)
        {
            MD5CryptoServiceProvider _md5Hasher = new MD5CryptoServiceProvider();
            byte[] bs = Encoding.UTF8.GetBytes(text);
            bs = _md5Hasher.ComputeHash(bs);
            StringBuilder s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2"));
            }
            return s.ToString();
        } 
    }
}
