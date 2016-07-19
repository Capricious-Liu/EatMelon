using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EatMelon.Models;

namespace EatMelon.Controllers
{
    public class OrderViewModel
    {
        public int ID { get; set; }
        public int S_ID { get; set; }
        public int U_ID { get; set; }
        public int P_ID { get; set; }
        public DateTime? TIME { get; set; }
        public bool? STATE { get; set; }
        public decimal? TOTAL_PRICE { get; set; }
        public decimal NUM { get; set; }
        public string NAME { get; set; }
    }


    public class AccountIndexController : Controller
    {
        // GET: AccountIndex

        decimal UserId = -1;
        Users db = new Users();
        Pro_pics db2 = new Pro_pics();

        public ActionResult Index()
        {
            UserId = (Session["UserMessage"] as UserMessage).id;

            if (UserId == -1)
            {
                return RedirectToAction("LoginCheck", "TB_USER");
            }

            string SqlString =
                  "select P.ID,P.S_ID,P.NAME,P.PRICE,P.DISCOUNT_RATE,P.DESCRIPTION from TB_PRODUCT  P,TB_FAVORS  F where F.P_ID = P.ID and F.S_ID = P.S_ID and F.U_ID =" + UserId.ToString();
            var productView = db.Database.SqlQuery<ProductViewModel>(SqlString).ToList();

            foreach (var procuctItem in productView)
            {
                foreach (var picItem in db2.TB_PRO_PIC)
                {
                    if (procuctItem.ID == picItem.P_ID)
                    {
                        procuctItem.PICTURE = picItem.PICTURE;
                    }
                }
            }

            SqlString =
                " select O.ID, O.S_ID, O.U_ID, P.ID, O.TIME, O.STATE,O.TOTAL_PRICE,C.NUM,P.NAME  from TB_ORDER O, TB_CONTAINS C, TB_PRODUCT P where O.ID = C.O_ID  and C.S_ID = P.S_ID  and C.P_ID = P.ID and O.U_ID =" +
                UserId.ToString();
            var orderView = db.Database.SqlQuery<OrderViewModel>(SqlString).ToList();




            var tempTuple =
                 new Tuple<List<ProductViewModel>, List<OrderViewModel>>(productView, orderView);

            return View(tempTuple);
        }
    }
}