using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using test2.Models;

namespace test2.Controllers
{
    public class thispicpro
    {
        public TB_PRODUCT db_pro;
        public List<TB_PRO_PIC> db_pic;
    }

    public class ADD_PROController : Controller
    {
        private Entities db = new Entities();
        List<thispicpro> list_tb_picpro;

        void searchpic(thispicpro curitem, decimal cur_sid, decimal cur_pid)
        {
            curitem.db_pic = new List<TB_PRO_PIC>();
            foreach (TB_PRO_PIC myPic in db.TB_PRO_PIC)
            {
                if (myPic.S_ID == cur_sid && myPic.P_ID == cur_pid)
                {
                    TB_PRO_PIC picnewitem = new TB_PRO_PIC();
                    picnewitem.S_ID = myPic.S_ID;
                    picnewitem.P_ID = myPic.P_ID;
                    picnewitem.PICTURE = myPic.PICTURE;
                    curitem.db_pic.Add(picnewitem);
                }
            }
        }

        // GET: ADD_PRO
        public ActionResult Index(decimal cur_storeid = 11)
        {
            ViewData["sid"] = cur_storeid;
            list_tb_picpro = new List<thispicpro>();
            foreach (TB_PRODUCT myPro in db.TB_PRODUCT)
            {
                if (myPro.S_ID == cur_storeid)
                {
                    thispicpro newitem = new thispicpro();
                    newitem.db_pro = new TB_PRODUCT();
                    newitem.db_pro.S_ID = myPro.S_ID;
                    newitem.db_pro.ID = myPro.ID;
                    newitem.db_pro.NAME = myPro.NAME;
                    newitem.db_pro.NUM = myPro.NUM;
                    newitem.db_pro.PRICE = myPro.PRICE;
                    newitem.db_pro.TYPE = myPro.TYPE;
                    newitem.db_pro.DESCRIPTION = myPro.DESCRIPTION;
                    newitem.db_pro.DISCOUNT_RATE = myPro.DISCOUNT_RATE;
                    searchpic(newitem, myPro.S_ID, myPro.ID);
                    list_tb_picpro.Add(newitem);
                }
            }
            return View(list_tb_picpro);
        }

        // GET: ADD_PRO/Create
        public ActionResult Create(decimal cur_store)
        {
            return View();
        }

        // POST: ADD_PRO/Create
        [HttpPost]
        public ActionResult Create(decimal cur_store, string picture, [Bind(Include = "ID,NAME,NUM,PRICE,TYPE,DESCRIPTION,DISCOUNT_RATE")] TB_PRODUCT tb_pro, TB_PRO_PIC tb_pic)
        {
            tb_pro.S_ID = cur_store;
            picture = Request.Form[("PICTURE")];
            if (ModelState.IsValid)
            {
                foreach (TB_PRODUCT myPro in db.TB_PRODUCT)
                {
                    if (myPro.S_ID == tb_pro.S_ID && myPro.ID == tb_pro.ID)
                    {
                        return RedirectToAction("Index");
                    }
                }
                db.TB_PRODUCT.Add(tb_pro);
                db.SaveChanges();
                tb_pic.S_ID = tb_pro.S_ID;
                tb_pic.P_ID = tb_pro.ID;
                tb_pic.PICTURE = picture;
                db.TB_PRO_PIC.Add(tb_pic);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = cur_store });
            }
            return View();
        }

        // GET: ADD_PRO/Create
        public ActionResult Edit(decimal cur_sid, decimal cur_pid)
        {
            TB_PRODUCT tb_pro = (from li_pro in db.TB_PRODUCT where (li_pro.S_ID == cur_sid && li_pro.ID == cur_pid) select li_pro).FirstOrDefault();
            if (tb_pro == null)
            {
                HttpNotFound();
            }
            return View(tb_pro);
        }

        // POST: ADD_PRO/Create
        [HttpPost]
        public ActionResult Edit(decimal cur_sid, decimal cur_pid, [Bind(Include = "S_ID,ID,NAME,NUM,PRICE,TYPE,DESCRIPTION,DISCOUNT_RATE")] TB_PRODUCT tb_pro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_pro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = cur_sid });
            }
            return View(tb_pro);
        }
    }
}
