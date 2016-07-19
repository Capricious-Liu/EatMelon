using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EatMelon.Models;

namespace EatMelon.Controllers
{
    public class StoreIndexViewModel
    {
        public List<string> types;
        public List<TB_PRODUCT> low_products;
        public List<TB_PRODUCT> high_products;
        public List<HotProductView> hot_products;
        public string name { get; set; }
        public string filename { get; set; }
        public int? store_id { get; set; }
    }
    public class DiscountViewModel
    {
        public List<TB_PRODUCT> products;
        public string dis { get; set; }
        public string name { get; set; }
        public int? store_id { get; set; }
    }
    public class AllStoreViewModel
    {
        public string store_name { get; set; }
        public List<ProductWithPicture_2_0> products;
        public int? store_id { get; set; }
        public int? rank { get; set; }
    }
    public class ProductWithPicture_2_0
    {
        public string PICTURE { get; set; }
        public TB_PRODUCT product;
    }
    public class StoreShowController : Controller
    {
        Stores db = new Stores();
        Contains db2 = new Contains();
        List<HotProductView> hot_product_models = new List<HotProductView>();
        StoreIndexViewModel IndexModel = new StoreIndexViewModel();
        class StoreMes
        {
            public int? ID { get; set; }
            public string NAME { get; set; }
            public string TYPE { get; set; }
            public string FILE_NAME { get; set; }
        }
        // GET: StoreShow
        public ActionResult Index(int? store_id)
        {


            string Sql = "SELECT ID,NAME,TYPE,FILE_NAME FROM TB_STORE NATURAL JOIN TB_STORE_TYPE NATURAL JOIN TB_DECORATION WHERE ID= " + store_id.ToString();
            List<StoreMes> storemess = db.Database.SqlQuery<StoreMes>(Sql).ToList();
            IndexModel.name = storemess.First().NAME;
            IndexModel.filename = storemess.First().FILE_NAME;
            IndexModel.types = new List<string>();
            foreach (StoreMes item in storemess)
            {
                IndexModel.types.Add(item.TYPE);
            }
            IndexModel.store_id = store_id;

            string SqlString = "SELECT * FROM TB_PRODUCT P WHERE P.S_ID = " + store_id.ToString() + "ORDER BY (SELECT SUM(NUM) FROM TB_CONTAINS GROUP BY S_ID,P_ID HAVING S_ID = P.S_ID AND P_ID = P.ID) DESC";
            List<TB_PRODUCT> hot_products = db.Database.SqlQuery<TB_PRODUCT>(SqlString).ToList();
            int min_num = Math.Min(4, hot_products.Count);
            int num = 0;
            int i = 0;
            while (num < min_num && i < hot_products.Count)
            {
                TB_PRODUCT temp_product = hot_products[i];
                string Sql2 = "SELECT COUNT(*) FROM TB_CONTAINS WHERE S_ID = " + temp_product.S_ID.ToString() + "AND P_ID = " + temp_product.ID.ToString();
                var list = db2.TB_CONTAINS.Where(a => a.P_ID == temp_product.ID && a.S_ID == temp_product.S_ID).ToList();
                if (list.Count > 0)
                {
                    HotProductView product = new HotProductView();
                    product.name = temp_product.NAME;
                    product.price = temp_product.PRICE;
                    product.s_id = (int?)temp_product.S_ID;
                    product.id = (int?)temp_product.ID;
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
                    min_num = Math.Min(min_num, 4);
                }
                i++;
            }
            IndexModel.hot_products = hot_product_models;
            IndexModel.low_products = db.Database.SqlQuery<TB_PRODUCT>("SELECT * FROM TB_PRODUCT WHERE S_ID = " + store_id.ToString() + "AND DISCOUNT_RATE < 0.5").ToList();
            IndexModel.high_products = db.Database.SqlQuery<TB_PRODUCT>("SELECT * FROM TB_PRODUCT WHERE S_ID = " + store_id.ToString() + "AND DISCOUNT_RATE < 1 AND DISCOUNT_RATE > 0.5").ToList();
            return View(IndexModel);
        }
        public ActionResult DiscountShow(int? isHigh, int? store_id, string name)
        {
            DiscountViewModel model = new DiscountViewModel();

            model.name = name;
            model.store_id = store_id;
            if (isHigh == 0)
            {
                model.dis = "五折以上商品";
                model.products = db.Database.SqlQuery<TB_PRODUCT>("SELECT * FROM TB_PRODUCT WHERE S_ID = " + store_id.ToString() + "AND DISCOUNT_RATE < 1 AND DISCOUNT_RATE > 0.5").ToList();

            }
            else if (isHigh == 1)
            {
                model.dis = "五折以下商品";
                model.products = IndexModel.low_products = db.Database.SqlQuery<TB_PRODUCT>("SELECT * FROM TB_PRODUCT WHERE S_ID = " + store_id.ToString() + "AND DISCOUNT_RATE < 0.5").ToList();
            }
            else
            {
                model.dis = "全部商品";
                model.products = IndexModel.low_products = db.Database.SqlQuery<TB_PRODUCT>("SELECT * FROM TB_PRODUCT WHERE S_ID = " + store_id.ToString()).ToList();
            }
            return View(model);
        }
        public ActionResult AllStoreShow()
        {
            List<AllStoreViewModel> models = new List<AllStoreViewModel>();
            foreach (TB_STORE store in db.TB_STORE)
            {
                AllStoreViewModel item = new AllStoreViewModel();
                item.store_name = store.NAME;
                item.rank = (int?)store.QUALITY_RATING;
                item.store_id = (int?)store.ID;
                List<ProductWithPicture_2_0> products = new List<ProductWithPicture_2_0>();
                Products p_db = new Products();
                foreach (TB_PRODUCT temp_item in p_db.TB_PRODUCT.Where(a => a.S_ID == store.ID).ToList())
                {
                    ProductWithPicture_2_0 pwp = new ProductWithPicture_2_0();
                    Pro_pics pic_db = new Pro_pics();
                    pwp.product = temp_item;

                    TB_PRO_PIC pic = pic_db.TB_PRO_PIC.Where(a => a.P_ID == temp_item.ID && a.S_ID == temp_item.S_ID).First();
                    if (pic != null)
                    {
                        pwp.PICTURE = pic.PICTURE;
                    }
                    products.Add(pwp);
                }

                item.products = new List<ProductWithPicture_2_0>();
                int i = 0;
                while (i < 5 && i < products.Count)
                {
                    item.products.Add(products[i]);
                    i++;
                }
                models.Add(item);
            }
            ViewData["StoreKeyString"] = "所有";
            return View(models);
        }
        [HttpPost]
        public ActionResult AllStoreShow(string keyString)
        {
            keyString = Request.Form["keyString"];
            List<AllStoreViewModel> models = new List<AllStoreViewModel>();
            foreach (TB_STORE store in db.TB_STORE.Where(a => a.NAME.Contains(keyString)).ToList())
            {
                AllStoreViewModel item = new AllStoreViewModel();
                item.store_name = store.NAME;
                item.rank = (int?)store.QUALITY_RATING;
                item.store_id = (int?)store.ID;
                List<ProductWithPicture_2_0> products = new List<ProductWithPicture_2_0>();
                Products p_db = new Products();
                foreach (TB_PRODUCT temp_item in p_db.TB_PRODUCT.Where(a => a.S_ID == store.ID).ToList())
                {
                    ProductWithPicture_2_0 pwp = new ProductWithPicture_2_0();
                    Pro_pics pic_db = new Pro_pics();
                    pwp.product = temp_item;

                    TB_PRO_PIC pic = pic_db.TB_PRO_PIC.Where(a => a.P_ID == temp_item.ID && a.S_ID == temp_item.S_ID).First();
                    if (pic != null)
                    {
                        pwp.PICTURE = pic.PICTURE;
                    }
                    products.Add(pwp);
                }

                item.products = new List<ProductWithPicture_2_0>();
                int i = 0;
                while (i < 5 && i < products.Count)
                {
                    item.products.Add(products[i]);
                    i++;
                }
                models.Add(item);
            }
            ViewData["StoreKeyString"] = "所有";
            return View(models);
        }
    }
}