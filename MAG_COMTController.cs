using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using test3.Models;

namespace test2.Controllers
{
    public class ADD_COMTController : Controller
    {
        Entities db = new Entities();

        // GET: ADD_COMT
        public ActionResult Index(decimal cur_uid = 29, decimal cur_oid = 3)
        {
            ViewData["uid"] = cur_uid;
            ViewData["oid"] = cur_oid;
            string sql = "select U_ID, S_ID, P_ID, DESCRIPTION, O_ID, ID, RANK from TB_CONTAINS natural left outer join TB_COMMENT WHERE O_ID = " + cur_oid;
            List<TB_COMMENT> list_tb_comt = db.Database.SqlQuery<TB_COMMENT>(sql).ToList();
            foreach (TB_COMMENT tb_comt in list_tb_comt)
            {
                if (tb_comt.U_ID == null)
                {
                    tb_comt.ID = 0;
                    tb_comt.U_ID = cur_uid;
                }
            }
            Session["list_tb_comt"] = list_tb_comt;
            return View(Session["list_tb_comt"] as List<TB_COMMENT>);
        }
        // POST: ADD_COMT/Index
        [HttpPost]
        public ActionResult Index(decimal? cur_uid, decimal? cur_oid, decimal? cur_sid, TB_COMMENT tb_comt)
        {
            List<TB_COMMENT> list_tb_comt = Session["list_tb_comt"] as List<TB_COMMENT>;
            int i = 0;
            foreach (TB_COMMENT my_comt in list_tb_comt)
            {
                my_comt.RANK = Convert.ToByte(Request.Form["rank" + i.ToString()]);
                my_comt.DESCRIPTION = Request.Form["description" + i.ToString()];
                i = i + 1;
            }

            foreach (TB_COMMENT my_comt in list_tb_comt)
            {
                cur_uid = my_comt.U_ID;
                cur_oid = my_comt.O_ID;
                cur_sid = my_comt.S_ID;
                break;
            }

            decimal? comt_cnt = 0;
            foreach (TB_COMMENT my_comt in list_tb_comt)
            {
                if (my_comt.S_ID == cur_sid)
                {
                    comt_cnt = comt_cnt + 1;
                }
            }

            TB_STORE tb_store = db.TB_STORE.Find(cur_sid);
            List<TB_COMMENT> list_tb_comt_t1 = (from a in db.TB_COMMENT where a.O_ID == cur_oid select a).ToList();

            foreach (TB_COMMENT my_comt in list_tb_comt)
            {
                if (my_comt.DESCRIPTION == null)
                {
                    if (my_comt.ID != 0)
                    {
                        foreach (TB_COMMENT my_comt_t1 in list_tb_comt_t1)
                        {
                            if (my_comt_t1.ID == my_comt.ID)
                            {
                                decimal? cur_rank = 0;
                                cur_rank = my_comt_t1.RANK;
                                db.TB_COMMENT.Remove(my_comt_t1);
                                db.SaveChanges();
                                list_tb_comt_t1.Remove(my_comt_t1);
                                tb_store.QUALITY_RATING = (comt_cnt * tb_store.QUALITY_RATING - cur_rank) / (comt_cnt - 1);
                                db.Entry(tb_store).State = EntityState.Modified;
                                db.SaveChanges();
                                comt_cnt = comt_cnt - 1;
                            }
                        }
                    }
                }
                else
                {
                    if (my_comt.ID == 0)
                    {
                        db.TB_COMMENT.Add(my_comt);
                        db.SaveChanges();
                        tb_store.QUALITY_RATING = (comt_cnt * tb_store.QUALITY_RATING + my_comt.RANK) / (comt_cnt + 1);
                        db.SaveChanges();
                        comt_cnt = comt_cnt + 1;
                    }
                    else
                    {
                        foreach (TB_COMMENT my_comt_t2 in list_tb_comt_t1)
                        {
                            if (my_comt_t2.ID == my_comt.ID)
                            {
                                decimal? cur_rank = 0;
                                cur_rank = my_comt_t2.RANK;
                                my_comt_t2.RANK = my_comt.RANK;
                                my_comt_t2.DESCRIPTION = my_comt.DESCRIPTION;
                                db.Entry(my_comt_t2).State = EntityState.Modified;
                                db.SaveChanges();
                                tb_store.QUALITY_RATING = (comt_cnt * tb_store.QUALITY_RATING - cur_rank + my_comt.RANK) / comt_cnt;
                                db.Entry(tb_store).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
            Session.Remove("list_tb_comt");
            return RedirectToAction("Index", new { uid = cur_uid, oid = cur_uid });
        }
    }
}
