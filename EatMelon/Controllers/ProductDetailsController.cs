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
    public class SingleOrder
    {
        public TB_ORDER order;
        public TB_CONTAINS contain;

        public SingleOrder()
        {
            order = new TB_ORDER();
            contain = new TB_CONTAINS();
        }
    }

    public class Comment
    {
        public string user_name;
        public TB_COMMENT comment;

        public Comment()
        {
            comment = new TB_COMMENT();
        }
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
        public List<Comment> comment_list { get; set; }
    }

    public class ProductDetailsController : Controller
    {
        private Users dbUsr = new Users();
        private Products dbPro = new Products();
        private Stores dbSto = new Stores();
        private Orders dbOrd = new Orders();
        private Contains dbCon = new Contains();
        private Comments dbCom = new Comments();
        private ProductMessage productMes = new ProductMessage();

        // GET: ProductDetails/Details/5
        public ActionResult Details(decimal? product_id, decimal? store_id)        //此处有默认值！！！！！！！！！！！！！！
        {
            if (product_id == null && store_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string sql = "select * from TB_PRODUCT WHERE ID = " + product_id + " and S_ID = " + store_id;
            List<TB_PRODUCT> list_tb_pro = dbPro.Database.SqlQuery<TB_PRODUCT>(sql).ToList();
            if (list_tb_pro.Count() != 0)
            {
                productMes.id = list_tb_pro.First().ID;
                productMes.s_id = list_tb_pro.First().S_ID;
                productMes.name = list_tb_pro.First().NAME;
                TB_STORE store = dbSto.TB_STORE.Find(store_id);
                productMes.store_name = store.NAME;
                productMes.price = list_tb_pro.First().PRICE;
                productMes.discount = (1 - list_tb_pro.First().DISCOUNT_RATE) * 100;
                productMes.new_price = productMes.price * (100 - productMes.discount) / 100;
                productMes.description = list_tb_pro.First().DESCRIPTION;
                productMes.num = list_tb_pro.First().NUM;
                productMes.comment_list = new List<Comment>();
                foreach (TB_COMMENT mycomment in dbCom.TB_COMMENT)
                {
                    if (mycomment.S_ID == productMes.s_id && mycomment.P_ID == productMes.id)
                    {
                        Comment new_comment = new Comment();
                        new_comment.comment = mycomment;
                        new_comment.user_name = dbUsr.TB_USER.Find(mycomment.U_ID).NAME;
                        productMes.comment_list.Add(new_comment);
                    }
                }
            }
            if (string.IsNullOrEmpty(Request.Form["AddToCart"]) == false)
            {
                return  (AddToCart());
            }
            if (string.IsNullOrEmpty(Request.Form["BuyNow"]) == false)
            {
                return (BuyNow());
            }
            return View(productMes);
        }

        [HttpPost]
        public ActionResult AddToCart()
        {

            UserMessage UserMes = new UserMessage();
            UserMes = Session["UserMessage"] as UserMessage;
            //UserMes.id = 35;
            SingleOrder myOrder = new SingleOrder();

            List<TB_ORDER> ShopCart = new List<TB_ORDER>();
            //dbOrd = new Orders();
            ShopCart = dbOrd.Database.SqlQuery<TB_ORDER>(
                "SELECT * FROM TB_ORDER WHERE U_ID = " + UserMes.id + " AND S_ID = " + productMes.s_id + " AND STATE = 1").ToList();
            if (ShopCart.Count == 0)
            {
                string sql = "SELECT ORDERZIZENG.nextval FROM dual";
                decimal nextOrderID = dbOrd.Database.SqlQuery<decimal>(sql).ToList().First();

                myOrder.contain.O_ID = nextOrderID;
                myOrder.contain.P_ID = productMes.id;
                myOrder.contain.S_ID = productMes.s_id;
                myOrder.contain.NUM = Convert.ToInt16(Request.Form["points"]);

                myOrder.order.ID = nextOrderID;
                myOrder.order.S_ID = productMes.s_id;
                myOrder.order.U_ID = UserMes.id;
                myOrder.order.STATE = 1;
                myOrder.order.TIME = DateTime.Now;
                myOrder.order.TOTAL_PRICE = productMes.new_price * myOrder.contain.NUM;
                if (ModelState.IsValid)
                {
                    dbOrd.TB_ORDER.Add(myOrder.order);
                    dbOrd.SaveChanges();
                    dbCon.TB_CONTAINS.Add(myOrder.contain);
                    dbCon.SaveChanges();
                    //  UpdateProductNum(dbCon.Database.SqlQuery<TB_CONTAINS>("select * from TB_CONTAINS WHERE O_ID = " + nextOrderID).ToList());
                }
            }
            else
            {
                short ProductNum = Convert.ToInt16(Request.Form["points"]);
                myOrder.order = ShopCart.First();
                List<TB_CONTAINS> Contain = new List<TB_CONTAINS>();
                Contain = dbCon.Database.SqlQuery<TB_CONTAINS>(
                    "SELECT * FROM TB_CONTAINS WHERE O_ID = " + myOrder.order.ID + " AND P_ID = " + productMes.id).ToList();
                if (Contain.Count == 0)
                {
                    myOrder.contain.O_ID = myOrder.order.ID;
                    myOrder.contain.P_ID = productMes.id;
                    myOrder.contain.S_ID = productMes.s_id;
                    myOrder.contain.NUM = ProductNum;
                    if (ModelState.IsValid)
                    {
                        dbCon.TB_CONTAINS.Add(myOrder.contain);
                        dbCon.SaveChanges();
                    }
                }
                else
                {
                    myOrder.contain = Contain.First();
                    myOrder.contain.NUM += ProductNum;
                    if (ModelState.IsValid)
                    {
                        dbCon.Entry(myOrder.contain).State = EntityState.Modified;
                        dbCon.SaveChanges();
                    }
                }
                myOrder.order.TIME = DateTime.Now;
                myOrder.order.TOTAL_PRICE += productMes.new_price * ProductNum;
                if (ModelState.IsValid)
                {
                    dbOrd.Entry(myOrder.order).State = EntityState.Modified;
                    dbOrd.SaveChanges();
                }
            }
            return RedirectToAction("Index", "ShoppingCart");
        }



        [HttpPost]
        public ActionResult BuyNow()
        {
            UserMessage UserMes = new UserMessage();
            // UserMes = Session["UserMassage"] as UserMessage;
            UserMes.id = 35;
            SingleOrder myOrder = new SingleOrder();
            string sql = "SELECT ORDERZIZENG.nextval FROM dual";
            decimal nextOrderID = dbOrd.Database.SqlQuery<decimal>(sql).ToList().First();

            myOrder.contain.O_ID = nextOrderID;
            myOrder.contain.P_ID = productMes.id;
            myOrder.contain.S_ID = productMes.s_id;
            myOrder.contain.NUM = Convert.ToInt16(Request.Form["points"]);

            myOrder.order.ID = nextOrderID;
            myOrder.order.S_ID = productMes.s_id;
            myOrder.order.U_ID = UserMes.id;
            myOrder.order.STATE = 2;
            myOrder.order.TIME = DateTime.Now;
            myOrder.order.TOTAL_PRICE = productMes.new_price * myOrder.contain.NUM;
            if (ModelState.IsValid)
            {
                dbOrd.TB_ORDER.Add(myOrder.order);
                dbOrd.SaveChanges();
                dbCon.TB_CONTAINS.Add(myOrder.contain);
                dbCon.SaveChanges();
                UpdateProductNum(dbCon.Database.SqlQuery<TB_CONTAINS>("select * from TB_CONTAINS WHERE O_ID = " + nextOrderID).ToList());
            }
            TempData["BuyNow"] = myOrder;

            return RedirectToAction("Payment", "Pay");
        }

        [HttpPost]
        public bool UpdateProductNum(List<TB_CONTAINS> contains)
        {
            foreach (TB_CONTAINS contain in contains)
            {
                string sql = "select * from TB_PRODUCT WHERE ID = " + contain.P_ID + " and S_ID = " + contain.S_ID;
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
                dbOrd.Dispose();
                dbCon.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
