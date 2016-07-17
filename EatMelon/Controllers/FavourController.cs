using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EatMelon.Models;
using EatMelon.Controllers;

namespace EatMelon.Controllers
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public int S_ID { get; set; }
        public string NAME { get; set; }
        public int PRICE { get; set; }
        public float DISCOUNT_RATE { get; set; }
        public string DESCRIPTION { get; set; }
        public string PICTURE { get; set; }
    }
    public class FavourController : Controller
    {
        decimal UserId = -1;
        Users db = new Users();
        Pro_pics db2=new Pro_pics();

        // GET: Favour
        public ActionResult FavourShow()
        {
            UserId = (Session["UserMessage"] as UserMessage).id;
            //UserId = 35;
            
            if (UserId == -1)
            {
                return RedirectToAction("LoginCheck", "TB_USER");//返回登录界面
            }
            string SqlString = "select P.ID,P.S_ID,P.NAME,P.PRICE,P.DISCOUNT_RATE,P.DESCRIPTION from TB_PRODUCT  P,TB_FAVORS  F where F.P_ID = P.ID and F.S_ID = P.S_ID and F.U_ID =" + UserId.ToString();

            List<ProductViewModel> ProductView = db.Database.SqlQuery<ProductViewModel>(SqlString).ToList();

            foreach (var productItem in ProductView)
            {
                foreach (var picItem in db2.TB_PRO_PIC)
                {
                    if (productItem.ID == picItem.P_ID)
                    {
                        productItem.PICTURE = picItem.PICTURE;
                    }
                }
            }

            return View(ProductView);
        }
    }
}