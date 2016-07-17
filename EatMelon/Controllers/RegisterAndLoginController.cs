using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EatMelon.Models;

namespace EatMelon.Controllers
{
    public class RegisterAndLoginController : Controller
    {
        private Users db = new Users();
        UserMessage MyUser = new UserMessage();

        // GET: RegisterAndLogin

        public ActionResult LoginCheck()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginCheck(string userName, string passWord)
        {
            if (ModelState.IsValid)
            {
                userName = Request.Form["userName"];
                passWord = Request.Form["passWord"];
                foreach (TB_USER myUser in db.TB_USER)
                {
                    if (myUser.NAME == userName)
                    {
                        if (myUser.PASSWORD == passWord)
                        {
                            MyUser.id = myUser.ID;
                            MyUser.name = myUser.NAME;
                            // ViewData["UserMessage"] = MyUser;

                            Session["UserMessage"] = MyUser;
                            return RedirectToAction("Index", "SearchProduct");                 // !!!!!!!!!!!!!!!!!!!!!!!!登陆成功跳转页面
                        }
                        else
                        {
                            return View();
                        }
                    }
                }
            }
            return View(MyUser);
        }

        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult register([Bind(Include = "PASSWORD,NAME,DETAILADDR,DISTRICT,CITY,PROVINCE,ZIPCODE,PHONE,CREDIT_NO")] TB_USER tB_USER)
        {

            foreach (TB_USER myUser in db.TB_USER)
            {
                if (myUser.NAME == tB_USER.NAME)
                {
                   // return RedirectToAction("Index");
                    return View();
                }
            }

            tB_USER.POINT = 0;
            db.TB_USER.Add(tB_USER);
            db.SaveChanges();
            return RedirectToAction("LoginCheck");

        }
    }
}