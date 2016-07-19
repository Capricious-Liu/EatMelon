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

        public List<string> managers;
    }

    

    public class CreateStoreController : Controller
    {


        private Manages db1 = new Manages();
        private Stores db2 = new Stores();
        private StoreTypes db3 = new StoreTypes();
        private Users db4=new Users();

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

        bool setManageList(decimal s_id,List<string> managers)
        {
            foreach (var manageItem in db1.TB_MANAGE)
            {
                if ((manageItem.S_ID == s_id)&&(manageItem.AUTHORITY==false))
                {
                    foreach (var userItem in db4.TB_USER)
                    {
                        if (userItem.ID == manageItem.U_ID)
                        {
                            managers.Add(userItem.NAME);
                        }
                    }
                }
            }
            return true;
        }



        // GET: CreateStore
        public ActionResult MyShop()                                  //显示我的店铺，我是店主
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
                    newitem.managers = new List<string>();
                    
                    
                    newitem.db_manage.U_ID = myMag.U_ID;
                    newitem.db_manage.S_ID = myMag.S_ID;
                    newitem.db_manage.AUTHORITY = myMag.AUTHORITY;


                    if (myMag.AUTHORITY == true)
                    {

                        setManageList(myMag.S_ID, newitem.managers);
                    }


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


        // POST: TB_STORE/Create
        [HttpPost]
        public ActionResult AddShop(string name, string type)
        {

            decimal cur_user = (Session["UserMessage"] as UserMessage).id;                                 //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                                             //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                                             //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            TB_MANAGE tb_manage=new TB_MANAGE();
            TB_STORE tb_store=new TB_STORE();
            TB_STORE_TYPE tb_store_type=new TB_STORE_TYPE();



            name = Request.Form[("NAME")];
            type = Request.Form[("TYPE")];

            if (ModelState.IsValid)
            {
                foreach (TB_STORE myStore in db2.TB_STORE)
                {
                    if (myStore.NAME == name)
                    {
                        return View();
                    }
                }
                foreach (TB_MANAGE myMag in db1.TB_MANAGE)
                {
                    if (myMag.U_ID == cur_user && myMag.AUTHORITY == true)
                    {
                        return View();
                    }
                }
                tb_store.NAME = name;
                tb_store.QUALITY_RATING = 0;
                db2.TB_STORE.Add(tb_store);
                db2.SaveChanges();
                foreach (TB_STORE myStore in db2.TB_STORE)
                {
                    if (myStore.NAME == name)
                    {
                        tb_manage.U_ID = cur_user;
                        tb_manage.S_ID = myStore.ID;
                        tb_manage.AUTHORITY = true;
                        db1.TB_MANAGE.Add(tb_manage);
                        db1.SaveChanges();
                        tb_store_type.ID = myStore.ID;
                        tb_store_type.TYPE = type;
                        db3.TB_STORE_TYPE.Add(tb_store_type);
                        db3.SaveChanges();
                        return RedirectToAction("Myshop");
                    }
                }
            }
            return View();
        }


        public ActionResult MyManageStore()                   // 显示管理的店铺
        {
            return View();
        }
    }
}
