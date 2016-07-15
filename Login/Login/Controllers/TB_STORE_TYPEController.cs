using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Login.Models;

namespace Login.Controllers
{
    public class ThisStoreType
    {
        public decimal tb_store;
        public TB_STORE store;
        public List<TB_STORE_TYPE> type;
    }

    public class TB_STORE_TYPEController : Controller
    {
        private Model3 db = new Model3();
        private ThisStoreType StoreType = new ThisStoreType();

        // GET: TB_STORE_TYPE
        public ActionResult Index(decimal storeId = 10)
        {
            ViewData["sid"] = storeId;
            StoreType.tb_store = storeId;
            StoreType.store = new TB_STORE();
            foreach(TB_STORE mystore in db.TB_STORE)
            {
                if(mystore.ID == storeId)
                {
                    StoreType.store.ID = mystore.ID;
                    StoreType.store.NAME = mystore.NAME;
                    StoreType.store.QUALITY_RATING = mystore.QUALITY_RATING;
                }
            }

            StoreType.type = new List<TB_STORE_TYPE>();
            foreach(TB_STORE_TYPE mystoretype in db.TB_STORE_TYPE)
            {
                if(mystoretype.ID == storeId)
                {
                    TB_STORE_TYPE newtype = new TB_STORE_TYPE();
                    newtype.ID = mystoretype.ID;
                    newtype.TYPE = mystoretype.TYPE;
                    newtype.TB_STORE = mystoretype.TB_STORE;
                    StoreType.type.Add(newtype);
                }
            }
            return View(StoreType.type);
        }

        public ActionResult Create(decimal storeid)
        {
            return View();
        }

    // POST: TB_STORE_TYPE/Create
    // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
    // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
    [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(decimal storeid, string type, TB_STORE_TYPE tb_store_type)
        {
            type = Request.Form[("TYPE")];
            if (ModelState.IsValid)
            {
                foreach(TB_STORE_TYPE mystoretype in db.TB_STORE_TYPE)
                {
                    if(mystoretype.ID == storeid)
                    {
                        if(mystoretype.TYPE == type)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                tb_store_type.ID = storeid;
                tb_store_type.TYPE = type;
                db.TB_STORE_TYPE.Add(tb_store_type);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = StoreType.tb_store });
            }
            return View();
        }

        // GET: TB_STORE_TYPE/Delete/5
        public ActionResult Delete(decimal storeid, string storeType, TB_STORE_TYPE tb_store_type)
        {
            if (ModelState.IsValid)
            {
                int typeNum = 0;
                foreach (TB_STORE_TYPE mystoretype in db.TB_STORE_TYPE)
                {
                    if(mystoretype.ID == storeid)
                    {
                        typeNum++;
                    }
                }
                if (typeNum > 1)
                {
                    foreach (TB_STORE_TYPE mystoretype in db.TB_STORE_TYPE)
                    {
                        if (mystoretype.ID == storeid)
                        {
                            if (mystoretype.TYPE == storeType)
                            {
                                db.TB_STORE_TYPE.Remove(mystoretype);
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
            }
                return View();
        }



    }
}
