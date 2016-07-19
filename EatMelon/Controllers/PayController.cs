using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EatMelon.Models;

namespace EatMelon.Controllers
{
    public class PayViewModel
    {
        public string name { get; set; }
        public long? phone { get; set; }
        public int? zipcode { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string detailaddr { get; set; }
        public long? creditcode { get; set; }
        public decimal? old_price { get; set; }
        public decimal? new_price { get; set; }
        public bool check { get; set; }
        public decimal point_num { get; set; }
    }
    class PriceRate
    {
        public decimal? price { get; set; }
        public decimal? rate { get; set; }
    }
    public class PayController : Controller
    {
        Products db = new Products();
        PayViewModel ViewModel = new PayViewModel();
        Users db2 = new Users();
        TB_USER old_user;
        UserMessage MyUser;
        // GET: Pay
        public ActionResult Payment()
        {
            //UserMessage MyUser = Session["UserMessage"] as UserMessage;
            MyUser = new UserMessage();
            MyUser.id = 35;
            MyUser.name = "admin";
            //string o_id = (Session["orderID"] as decimal?).ToString();
            //Session.Remove("orderID");
            string o_id = "24";
            string SqlString = "SELECT P.PRICE,P.DISCOUNT_RATE FROM TB_PRODUCT P,TB_CONTAINS C,TB_ORDER O WHERE P.ID = C.P_ID AND P.S_ID = C.S_ID AND C.O_ID = O.ID AND O.ID =" + o_id;
            List<PriceRate> prices = db.Database.SqlQuery<PriceRate>(SqlString).ToList();
            ViewModel.old_price = 0;
            ViewModel.new_price = 0;
            foreach (PriceRate item in prices)
            {
                ViewModel.old_price += item.price;
                ViewModel.new_price += item.price * item.rate;
            }
            Users user_db = new Users();
            TB_USER user = user_db.TB_USER.Where(a => a.ID == MyUser.id).First();
            if (user != null)
            {
                ViewModel.name = MyUser.name;
                ViewModel.phone = user.PHONE;
                ViewModel.province = user.PROVINCE;
                ViewModel.zipcode = user.ZIPCODE;
                ViewModel.city = user.CITY;
                ViewModel.creditcode = user.CREDIT_NO;
                ViewModel.district = user.DISTRICT;
                ViewModel.detailaddr = user.DETAILADDR;
                ViewModel.point_num = (decimal)ViewModel.old_price - decimal.Floor((decimal)ViewModel.old_price);
                ViewModel.check = true;
            }

            return View(ViewModel);
        }
        [HttpPost]
        public ActionResult Payment(FormCollection collection)
        {
            if (collection["action"] == "cancel")
            {
                return RedirectToAction("取消");
            }
            PayViewModel ViewModel = new PayViewModel();
            ViewModel.check = true;
            MyUser = new UserMessage();
            MyUser.id = 35;
            MyUser.name = "admin";
            string NewPassword = Request.Form["password"];
            string NewDetailAddr = Request.Form["detailaddr"];
            string NewCity = Request.Form["city"];
            string NewProvince = Request.Form["province"];
            int result;
            int NewZipCode, NewCardNum;
            if (int.TryParse(Request.Form["zipcode"], out result))
            {
                NewZipCode = result;
            }
            else
            {
                NewZipCode = 0;
            }
            int result2;
            if (int.TryParse(Request.Form["creditcode"], out result2))
            {
                NewCardNum = result2;
            }
            else
            {
                NewCardNum = 0;
            }
            string NewDistrict = Request.Form["district"];
            old_user = db2.TB_USER.Where(a => a.ID == MyUser.id).FirstOrDefault();
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
            if (NewPassword != old_user.PASSWORD)
            {

            }
            db.SaveChanges();
            if (NewPassword != old_user.PASSWORD)
            {
                ViewModel.check = false;
                return View(ViewModel);
            }
            return RedirectToAction("完成成功");
        }
    }
}