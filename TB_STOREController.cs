using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test2.Models;

namespace test2.Controllers
{
    public class thisuserstore
    {
        public decimal deci_user;
        public List<thismanagestore> c_man_st;
    }

    public class thismanagestore
    {
        public TB_MANAGE db_manage;
        public TB_STORE db_store;
    }

    public class TB_STOREController : Controller
    {

        // GET: TB_STORE
        private Entities db = new Entities();
        private thisuserstore en_user_st = new thisuserstore();

        bool searchstore(thismanagestore curitem, decimal cur_storeid)
        {
            foreach (TB_STORE myStore in db.TB_STORE)
            {
                if (myStore.ID == cur_storeid)
                {
                    curitem.db_store = new TB_STORE();
                    curitem.db_store.ID = myStore.ID;
                    curitem.db_store.NAME = myStore.NAME;
                    curitem.db_store.QUALITY_RATING = myStore.QUALITY_RATING;
                    return true;
                }
            }
            return false;
        }

        public ActionResult Index(decimal cur_userid = 25)
        {
            ViewData["uid"] = cur_userid;
            en_user_st.c_man_st = new List<thismanagestore>();
            en_user_st.deci_user = cur_userid;
            foreach (TB_MANAGE myMag in db.TB_MANAGE)
            {
                if (myMag.U_ID == cur_userid)
                {
                    thismanagestore newitem = new thismanagestore();
                    newitem.db_manage = new TB_MANAGE();
                    newitem.db_manage.U_ID = myMag.U_ID;
                    newitem.db_manage.S_ID = myMag.S_ID;
                    newitem.db_manage.AUTHORITY = myMag.AUTHORITY;
                    searchstore(newitem, myMag.S_ID);
                    en_user_st.c_man_st.Add(newitem);
                }
            }
            return View(en_user_st.c_man_st);
        }
        
        // GET: TB_STORE/Create
        public ActionResult Create(decimal cur_user)
        {
            return View();
        }

        // POST: TB_STORE/Create
        [HttpPost]
        public ActionResult Create(decimal cur_user, string name, string type, TB_MANAGE tb_manage, TB_STORE tb_store, TB_STORE_TYPE tb_store_type)
        {
            name = Request.Form[("NAME")];
            type = Request.Form[("TYPE")];
            if (ModelState.IsValid)
            {
                foreach (TB_STORE myStore in db.TB_STORE)
                {
                    if (myStore.NAME == name)
                    {
                        return RedirectToAction("Index");
                    }
                }
                foreach (TB_MANAGE myMag in db.TB_MANAGE)
                {
                    if (myMag.U_ID == cur_user && myMag.AUTHORITY == true)
                    {
                        return RedirectToAction("Index");
                    }
                }
                tb_store.NAME = name;
                tb_store.QUALITY_RATING = 0;
                db.TB_STORE.Add(tb_store);
                db.SaveChanges();
                foreach (TB_STORE myStore in db.TB_STORE)
                {
                    if (myStore.NAME == name)
                    {
                        tb_manage.U_ID = cur_user;
                        tb_manage.S_ID = myStore.ID;
                        tb_manage.AUTHORITY = true;
                        db.TB_MANAGE.Add(tb_manage);
                        db.SaveChanges();
                        tb_store_type.ID = myStore.ID;
                        tb_store_type.TYPE = type;
                        db.TB_STORE_TYPE.Add(tb_store_type);
                        db.SaveChanges();
                        return RedirectToAction("Index", new { id = en_user_st.deci_user });
                    }
                }
            }

            return View();
        }
    }
}
