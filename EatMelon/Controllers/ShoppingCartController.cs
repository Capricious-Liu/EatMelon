using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EatMelon.Models;

namespace EatMelon.Controllers
{
    public class OrderItemViewModel
    {
        public int ID { get; set; }
        public int S_ID { get; set; }
        public int U_ID { get; set; }
        public int P_ID { get; set; }
        public DateTime? TIME { get; set; }
        public decimal? STATE { get; set; }
        public decimal? TOTAL_PRICE { get; set; }
        public decimal NUM { get; set; }
        public string NAME { get; set; }
        public string PICTURE { get; set; }
        public decimal? PRICE { get; set; }
        public decimal? DISCOUNT_RATE { get; set; }
        public string DESCRIPTION { get; set; }
        public string STORE_NAME { get; set; }

    }
    public class ShoppingCartController : Controller
    {
        decimal UserId = -1;
        Orders db = new Orders();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            UserId = (Session["UserMessage"] as UserMessage).id;
            if (UserId == -1)
            {
                return RedirectToAction("LoginCheck", "TB_USER");
            }
            var sqlString =
                "select O.ID,O.S_ID, O.U_ID, C.P_ID, O.TIME, O.STATE, O.TOTAL_PRICE, C.NUM, P.NAME, PIC.PICTURE,P.PRICE, P.DISCOUNT_RATE, P.DESCRIPTION, S.NAME " +
                "from TB_ORDER O, TB_CONTAINS C, TB_PRODUCT P, TB_PRO_PIC PIC,TB_STORE S " +
                "where O.ID = C.O_ID " +
                "and S.ID = P.S_ID " +
                "and C.S_ID = P.S_ID " +
                "and C.P_ID = P.ID " +
                "and PIC.P_ID = C.P_ID " +
                "and PIC.S_ID = C.S_ID " +
                "and O.STATE = 1 " +
                "and O.U_ID = " + UserId.ToString();

            var orderView = db.Database.SqlQuery<OrderItemViewModel>(sqlString).ToList();

            return View(orderView);
        }

        public ActionResult Delete(decimal? O_ID)
        {
            string sql = "delete from TB_ORDER where ID=" + O_ID.ToString();

            db.Database.ExecuteSqlCommand(sql);

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}