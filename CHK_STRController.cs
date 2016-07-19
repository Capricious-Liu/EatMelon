using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using test5.Models;

namespace test5.Controllers
{
    public class List_chk_sitem
    {
        public TB_CHECK_STORE tb_chk_sitem;
        public TB_STORE tb_sitem;
        public List<List_chk_proitem> tb_chk_prolist;
    }

    public class List_chk_proitem
    {
        public TB_CHECK_PRODUCT tb_chk_proitem;
        public TB_PRODUCT tb_proitem;
        public List<TB_PRO_PIC> tb_piclist;
    }

    public class CHK_STRController : Controller
    {

        Entities db = new Entities();

        // GET: CHK_STR
        public ActionResult Index(decimal? pagenum = 1, decimal? cur_aid = 1)
        {
            ViewData["CURPAGE"] = pagenum as decimal?;
            ViewData["CUR_AID"] = cur_aid as decimal?;
            Session["CUR_AID"] = cur_aid;
            decimal? pagesize = 1;
            decimal? pagecnt = 0;
            decimal? pagestart = (pagenum - 1) * pagesize;
            decimal? pageend = pagenum * pagesize;
            List<List_chk_sitem> this_chk_slist = new List<List_chk_sitem>();
            foreach (TB_CHECK_STORE my_chk_store in db.TB_CHECK_STORE)
            {
                if (pagecnt < pagestart)
                {
                    pagecnt = pagecnt + 1;
                    continue;
                }
                if (pagecnt >= pageend)
                {
                    continue;
                }
                pagecnt = pagecnt + 1;
                List_chk_sitem new_chk_sitem = new List_chk_sitem();
                //new_chk_sitem.tb_chk_sitem = my_chk_store;
                new_chk_sitem.tb_chk_sitem = new TB_CHECK_STORE();
                new_chk_sitem.tb_chk_sitem.A_ID = my_chk_store.A_ID;
                new_chk_sitem.tb_chk_sitem.S_ID = my_chk_store.S_ID;
                new_chk_sitem.tb_chk_sitem.STATE = my_chk_store.STATE;
                new_chk_sitem.tb_chk_sitem.TIME = my_chk_store.TIME;
                //new_chk_sitem.tb_sitem = db.TB_STORE.Find(new_chk_sitem.tb_chk_sitem.S_ID);
                new_chk_sitem.tb_sitem = new TB_STORE();
                TB_STORE new_tb_sitem = db.TB_STORE.Find(new_chk_sitem.tb_chk_sitem.S_ID);
                new_chk_sitem.tb_sitem.ID = new_tb_sitem.ID;
                new_chk_sitem.tb_sitem.NAME = new_tb_sitem.NAME;
                new_chk_sitem.tb_sitem.QUALITY_RATING = new_tb_sitem.QUALITY_RATING;
                new_chk_sitem.tb_chk_prolist = new List<List_chk_proitem>();
                foreach (TB_PRODUCT my_pro in db.TB_PRODUCT)
                {
                    if (my_pro.S_ID == new_chk_sitem.tb_sitem.ID)
                    {
                        List_chk_proitem new_chk_proitem = new List_chk_proitem();
                        foreach (TB_CHECK_PRODUCT my_chk_pro in db.TB_CHECK_PRODUCT)
                        {
                            if (my_chk_pro.P_ID == my_pro.ID && my_chk_pro.S_ID == my_pro.S_ID)
                            {
                                //new_chk_proitem.tb_chk_proitem = my_chk_pro;
                                new_chk_proitem.tb_chk_proitem = new TB_CHECK_PRODUCT();
                                new_chk_proitem.tb_chk_proitem.A_ID = my_chk_pro.A_ID;
                                new_chk_proitem.tb_chk_proitem.S_ID = my_chk_pro.S_ID;
                                new_chk_proitem.tb_chk_proitem.P_ID = my_chk_pro.P_ID;
                                new_chk_proitem.tb_chk_proitem.STATE = my_chk_pro.STATE;
                                new_chk_proitem.tb_chk_proitem.TIME = my_chk_pro.TIME;
                                break;
                            }
                        }
                        new_chk_proitem.tb_proitem = my_pro;
                        new_chk_proitem.tb_piclist = new List<TB_PRO_PIC>();
                        foreach (TB_PRO_PIC my_propic in db.TB_PRO_PIC)
                        {
                            if (my_pro.ID == my_propic.P_ID && my_pro.S_ID == my_propic.S_ID)
                            {
                                new_chk_proitem.tb_piclist.Add(my_propic);
                            }
                        }
                        new_chk_sitem.tb_chk_prolist.Add(new_chk_proitem);
                    }
                }
                this_chk_slist.Add(new_chk_sitem);
            }
            ViewData["NEXTPAGE"] = 0;
            int k = 0;
            k = k + 1;
            if ((pagecnt + 1) > db.TB_CHECK_STORE.Count())
            {
                ViewData["NEXTPAGE"] = 1;
            }
            Session["this_chk_slist"] = this_chk_slist;
            return View(Session["this_chk_slist"] as List<List_chk_sitem>);
        }
        [HttpPost]
        public ActionResult Index(decimal? pagenum, decimal? cur_aid, TB_CHECK_STORE tb_check_store, TB_CHECK_PRODUCT tb_check_pro)
        {
            int i = 0;
            cur_aid = Session["CUR_AID"] as decimal?;
            List<List_chk_sitem> itemlist = Session["this_chk_slist"] as List<List_chk_sitem>;
            foreach (List_chk_sitem listitem in itemlist)
            {
                listitem.tb_chk_sitem.STATE = Request.Form["state" + i.ToString()];
                listitem.tb_chk_sitem.A_ID = Convert.ToDecimal(cur_aid);
                db.Entry(listitem.tb_chk_sitem).State = EntityState.Modified;
                db.SaveChanges();
                int j = 0;
                foreach (List_chk_proitem listitem1 in listitem.tb_chk_prolist)
                {
                    listitem1.tb_chk_proitem.STATE = Request.Form["state" + i.ToString() + j.ToString()];
                    listitem1.tb_chk_proitem.A_ID = Convert.ToDecimal(cur_aid);
                    db.Entry(listitem1.tb_chk_proitem).State = EntityState.Modified;
                    db.SaveChanges();
                    j = j + 1;
                }
                i = i + 1;
            }
            Session.Remove("this_chk_list");
            Session.Remove("CUR_AID");
            return RedirectToAction("Index", new { page = (ViewData["CURPAGE"] as decimal?), aid = (ViewData["CUR_AID"] as decimal?) });
        }
    }
}
