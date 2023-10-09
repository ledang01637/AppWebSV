using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppWebSV.MySQL;
using MySql.Data.MySqlClient;
using System.Data;
using AppWebSV.SinhVien;

namespace AppWebSV.Controllers
{
    public class DsSVController : Controller
    {
        // GET: DsSV
        public ActionResult DsSV()
        {
            String query = "Select * From SinhVien";
            DataTable dt = ConnectMySQL.Instance.dtExcuteQuery(query);
            List<ClSinhVien> clSinhViens = new List<ClSinhVien>();
            var dsSv = new ClSinhVien();
            if (dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    dsSv.MaSV = row.Field<String>("MaSV");
                    dsSv.HoTen = row.Field<String>("TenSV");
                    dsSv.Diem = row.Field<float>("Diem");
                }
                clSinhViens.Add(dsSv);
            }
            return View(clSinhViens);
        }
        public ActionResult Them(String Ma, String Ten,float Diem)
        {
            String MaSV = Ma;
            String TenSV = Ten;
            float DiemSV = Diem;
            String query = "Insert into SinhVien Values('" + MaSV + "','" + TenSV + "'," + DiemSV + ")";
            int count = ConnectMySQL.Instance.excuteNonQuery(query);
            return View();
        }
    }
}