using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EatMelon.Models;

namespace EatMelon.Controllers
{
    public class HotProductView
    {
        public string picture { get; set; }
        public string name { get; set; }
        public decimal? price { get; set; }
        public int? id { get; set; }
        public int? s_id { get; set; }
    }

    public class HomeController : Controller
    {
        Contains db = new Contains();
        List<HotProductView> hot_product_models = new List<HotProductView>();

        public ActionResult Index()
        {
            string Sql = "SELECT * FROM TB_PRODUCT P ORDER BY (SELECT SUM(NUM) FROM TB_CONTAINS GROUP BY S_ID,P_ID HAVING S_ID = P.S_ID AND P_ID = P.ID) DESC";
            List<TB_PRODUCT> hot_products = db.Database.SqlQuery<TB_PRODUCT>(Sql).ToList();
            int min_num = Math.Min(8, hot_products.Count);
            int num = 0;
            int i = 0;
            while (num < min_num && i < hot_products.Count)
            {
                TB_PRODUCT temp_product = hot_products[i];
                string Sql2 = "SELECT COUNT(*) FROM TB_CONTAINS WHERE S_ID = " + temp_product.S_ID.ToString() + "AND P_ID = " + temp_product.ID.ToString();
                var list = db.TB_CONTAINS.Where(a => a.P_ID == temp_product.ID && a.S_ID == temp_product.S_ID).ToList();
                if (list.Count > 0)
                {
                    HotProductView product = new HotProductView();
                    product.name = temp_product.NAME;
                    product.price = temp_product.PRICE;
                    product.id = (int?)temp_product.ID;
                    product.s_id = (int?)temp_product.S_ID;
                    Pro_pics pics = new Pro_pics();
                    TB_PRO_PIC pic = pics.TB_PRO_PIC.Where(a => a.P_ID == temp_product.ID && a.S_ID == temp_product.S_ID).FirstOrDefault();
                    if (pic != null)
                    {
                        product.picture = pic.PICTURE;
                    }
                    hot_product_models.Add(product);
                    num--;
                }
                else
                {
                    min_num--;
                    min_num = Math.Min(min_num, 8);
                }
                i++;
            }
            return View(hot_product_models);
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            string product_name = collection["product_name"];
            TempData["product_name"] = product_name;
            return RedirectToAction("");
        }


    }
}