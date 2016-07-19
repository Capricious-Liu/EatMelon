using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EatMelon.Models;

namespace EatMelon.Controllers
{
    public class SingleOrder {
        public TB_ORDER order;
        public TB_CONTAINS contain;
    }

    public class ProductMessage
    {
        public decimal id { get; set; }
        public decimal s_id { get; set; }
        public string name { get; set; }
        public string store_name { get; set; }
        public decimal? price { get; set; }
        public decimal? discount { get; set; }
        public decimal? new_price { get; set; }
        public string description { get; set; } 
        public decimal? num { get; set; }
    }

    public class ProductDetailsController : Controller
    {
        private Products dbPro = new Products();
        private Stores dbSto = new Stores();
        private Orders dbOrd = new Orders();
        private Contains dbCon = new Contains();
        private ProductMessage productMes = new ProductMessage();

        // GET: ProductDetails/Details/5
        public ActionResult Details(decimal? product_id = 34, decimal? store_id = 15)
        {
            if (product_id == null && store_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string sql = "select * from TB_PRODUCT WHERE ID = " + product_id + " and S_ID = " + store_id;
            List<TB_PRODUCT> list_tb_pro = dbPro.Database.SqlQuery<TB_PRODUCT>(sql).ToList();
            if (list_tb_pro.Count() != 0)
            {
                productMes.id = list_tb_pro[0].ID;
                productMes.s_id = list_tb_pro[0].S_ID;
                productMes.name = list_tb_pro[0].NAME;
                productMes.price = list_tb_pro[0].PRICE;
                productMes.discount = (1 - list_tb_pro[0].DISCOUNT_RATE) * 100;
                productMes.new_price = productMes.price * (100 - productMes.discount) / 100;
                productMes.description = list_tb_pro[0].DESCRIPTION;
                productMes.num = list_tb_pro[0].NUM;
                TB_STORE store = dbSto.TB_STORE.Find(store_id);
                productMes.store_name = store.NAME;
            }
            return View(productMes);
        }
        
        [HttpPost]
        public ActionResult button()
        {
            UserMessage UserMes = new UserMessage();
            UserMes = Session["UserMassage"] as UserMessage;

            if (string.IsNullOrEmpty(Request.Form["加入到购物车"]) == false)
            {
                // 购物车
            }
            if (string.IsNullOrEmpty(Request.Form["现在购买"]) == false)
            {
                SingleOrder myOrder = new SingleOrder();
                myOrder.contain.P_ID = productMes.id;
                myOrder.contain.S_ID = productMes.s_id;
                myOrder.contain.NUM = Convert.ToInt32(Request.Form["points"]);
                myOrder.order.S_ID = productMes.s_id;
                myOrder.order.U_ID = UserMes.id;
                myOrder.order.STATE = 2;
                myOrder.order.TIME = DateTime.Now;
                myOrder.order.TOTAL_PRICE = productMes.new_price * myOrder.contain.NUM;
                if(ModelState.IsValid)
                {
                    dbOrd.TB_ORDER.Add(myOrder.order);
                    dbOrd.SaveChanges();
                    //{U_ID, TIME}作为候选码，找到O_ID
                    string sql = "select max(O_ID) from TB_ORDER WHERE U_ID = " + myOrder.order.U_ID + " and TIME = " + myOrder.order.TIME;
                    List<TB_ORDER> lastOrder = dbPro.Database.SqlQuery<TB_ORDER>(sql).ToList();
                    myOrder.contain.O_ID = lastOrder[0].ID;
                    dbCon.TB_CONTAINS.Add(myOrder.contain);
                    dbCon.SaveChanges();
                    UpdateProductNum(dbCon.Database.SqlQuery<TB_CONTAINS>("select * from TB_CONTAINS WHERE O_ID = " + lastOrder[0].ID).ToList());
                }
                TempData["BuyNow"] = myOrder;
            }
            return RedirectToAction("Delete");
        }

        [HttpPost]
        public bool UpdateProductNum(List<TB_CONTAINS> contains)
        {
            foreach (TB_CONTAINS contain in contains)
            {
                string sql = "select * from TB_PRODUCT WHERE ID = " + contain.P_ID +" and S_ID = " + contain.S_ID;
                List<TB_PRODUCT> product = dbPro.Database.SqlQuery<TB_PRODUCT>(sql).ToList();
                product[0].NUM -= contain.NUM;
                if (ModelState.IsValid)
                {
                    dbPro.Entry(product[0]).State = EntityState.Modified;
                    dbPro.SaveChanges();
                }
            }
            return true;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbPro.Dispose();
                dbSto.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
