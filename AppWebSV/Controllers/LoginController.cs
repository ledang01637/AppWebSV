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
    public class LoginController : Controller
    {
        // GET: Login
        //[HttpPost]
        public ActionResult Login()
        { 
            return View();    
        }
        public ActionResult DsSV()
        {
            String query = "Select * From SinhVien";
            DataTable dt = ConnectMySQL.Instance.dtExcuteQuery(query);
            List<ClSinhVien> clSinhViens = new List<ClSinhVien>();
            var dsSv = new ClSinhVien();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    dsSv.MaSV = row.Field<String>("MaSV");
                    dsSv.HoTen = row.Field<String>("TenSV");
                    dsSv.Diem = row.Field<float>("Diem");
                }
                clSinhViens.Add(dsSv);
            }
            return View(clSinhViens);
        }
        public ActionResult LuuLogin(string Name, string Pass)
        {
            string User = Name;
            string Pw = Pass;
            string User1;
            string Pw1;
            String query = "Select * From dangnhap";
            DataTable dt = ConnectMySQL.Instance.dtExcuteQuery(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    User1 = row.Field<String>("TaiKhoan");
                    Pw1 = row.Field<String>("MatKhau");
                    if (User1.Equals(User) && Pw1.Equals(Pw))
                    {
                        return RedirectToAction("DsSV");
                    }
                }
            }
            return View("Loi");
        }
        public ActionResult Them()
        {
            return View();
        }
        public ActionResult LuuThem(string Ma, string Ten, float Diem)
        {
            String MaSV = Ma;
            String TenSV = Ten;
            float DiemSV = Diem;
            String query = "Insert into SinhVien Values('" + MaSV + "','" + TenSV + "'," + DiemSV + ")";
            int count = ConnectMySQL.Instance.excuteNonQuery(query);
            if (count > 0)
            {
                return RedirectToAction("DsSV");
            }
            return View("Loi");
        }
        public ActionResult Xoa()
        {
            return View();
        }
        public ActionResult LuuXoa(string Ma)
        {
            String MaSV = Ma;
            String query = "Delete From SinhVien Where MaSV = '" + MaSV + "'";
            int count = ConnectMySQL.Instance.excuteNonQuery(query);
            if (count > 0)
            {
                return RedirectToAction("DsSV");
            }
            return View("Loi");
        }
        public ActionResult Sua()
        {
            return View();
        }
        public ActionResult LuuSua(string Ma, string Ten, float Diem)
        {
            String MaSV = Ma;
            String TenSV = Ten;
            float DiemSV = Diem;
            String query = "Update SinhVien Set tenSV = N'" + TenSV + "', Diem = " + DiemSV + " Where MaSV = '" + MaSV + "'";
            int count = ConnectMySQL.Instance.excuteNonQuery(query);
            if (count > 0)
            {
                return RedirectToAction("DsSV");
            }
            return View("Loi");
        }
    }
}