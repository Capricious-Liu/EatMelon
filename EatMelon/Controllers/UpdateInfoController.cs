using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EatMelon.Models;

namespace EatMelon.Controllers
{
    public class UpdateInfoController : Controller
    {
        Users db = new Users();
        TB_USER old_user;
        UserMessage userMes;

        // GET: UpdateInfo
        public ActionResult Update()
        {
            userMes = new UserMessage();
            userMes = Session["UserMessage"] as UserMessage;
            //userMes.id = 24;
            //userMes.name = "liuzhili";
            old_user = db.TB_USER.Where(a => a.ID == userMes.id).FirstOrDefault();
            ViewData["check_password"] = true;
            return View(old_user);
        }
        [HttpPost]
        public ActionResult Update(string NewPassword, int? NewCardNum, string NewDetailAddr, string NewCity, string NewProvince, int? NewZipCode, string NewDistrict,int? NewPhone)
        {
            //userMes = Session["UserMessage"] as UserMessage;
            userMes = new UserMessage();
            //userMes.id = 24;
            //userMes.name = "liuzhili";
            userMes = Session["UserMessage"] as UserMessage;

            NewPassword = Request.Form["first_password"];
            NewDetailAddr = Request.Form["detail_address"];
            NewCity = Request.Form["city"];
            NewProvince = Request.Form["province"];
            int result;
            if (int.TryParse(Request.Form["zipcode"], out result))
            {
                NewZipCode = result;
            }
            else
            {
                NewZipCode = 0;
            }
            int result2;
            if (int.TryParse(Request.Form["credit_no"], out result2))
            {
                NewCardNum = result2;
            }
            else
            {
                NewCardNum = 0;
            }
            int result3;
            if (int.TryParse(Request.Form["phone"], out result3))
            {
                NewPhone = result3;
            }
            else
            {
                NewPhone = 0;
            }
            NewDistrict = Request.Form["district"];
            //string SqlString = "select * from TB_USER where ID = " + userMes.id.ToString();
            //TB_USER old_user = db.Database.SqlQuery<TB_USER>(SqlString).First();
            old_user = db.TB_USER.Where(a => a.ID == userMes.id).FirstOrDefault();
            if (Request.Form["first_password"] != Request.Form["second_password"])
            {
                ViewData["check_password"] = false;
                return View(old_user);
            }
            ViewData["check_password"] = true;
            if (NewPassword != "" && NewPassword != old_user.PASSWORD)
            {
                old_user.PASSWORD = NewPassword;
            }

            if (NewCardNum != 0 && NewCardNum != old_user.CREDIT_NO)
            {
                old_user.CREDIT_NO = NewCardNum;
            }

            if (NewDetailAddr != "" && NewDetailAddr != old_user.DETAILADDR)
            {
                old_user.DETAILADDR = NewDetailAddr;
            }

            if (NewCity != "" && NewCity != old_user.CITY)
            {
                old_user.CITY = NewCity;
            }

            if (NewProvince != "" && NewProvince != old_user.PROVINCE)
            {
                old_user.PROVINCE = NewProvince;
            }

            if (NewZipCode != 0 && NewZipCode != old_user.ZIPCODE)
            {
                old_user.ZIPCODE = NewZipCode;
            }

            if (NewDistrict != "" && NewDistrict != old_user.DISTRICT)
            {
                old_user.DISTRICT = NewDistrict;
            }
            if (NewPhone != 0 && NewPhone != old_user.PHONE)
            {
                old_user.PHONE = NewPhone;
            }

            db.SaveChanges();

            return View(old_user);
        }
    }
}









