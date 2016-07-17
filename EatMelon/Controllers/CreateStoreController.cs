using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EatMelon.Models;

namespace EatMelon.Controllers
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

    public class CreateStoreController : Controller
    {


        private Manages db1 = new Manages();
        private Stores db2 = new Stores();
        private thisuserstore en_user_st = new thisuserstore();



        bool searchstore(thismanagestore curitem, decimal cur_storeid)
        {
            foreach (TB_STORE myStore in db2.TB_STORE)
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





        // GET: CreateStore
        public ActionResult MyShop()
        {
             //ViewData["uid"] = cur_userid;
             decimal cur_userid = (Session["UserMessage"] as UserMessage).id;
             en_user_st.c_man_st = new List<thismanagestore>();
             en_user_st.deci_user = cur_userid;
             foreach (TB_MANAGE myMag in db1.TB_MANAGE)
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

        public ActionResult AddShop()
        {
            return View();
        }
    }
}