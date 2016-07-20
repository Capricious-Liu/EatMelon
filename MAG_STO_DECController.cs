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
    public class MAG_STO_DECController : Controller
    {

        Entities db = new Entities();

        // GET: MAG_STO_DEC
        public ActionResult Index(decimal? cur_did = 11)
        {
            TB_DECORATION tb_dec = new TB_DECORATION();
            foreach (TB_DECORATION my_dec in db.TB_DECORATION)
            {
                if (my_dec.ID == cur_did)
                {
                    tb_dec = my_dec;
                    break;
                }
            }
            Session["CUR_DID"] = cur_did;
            return View(tb_dec);
        }
        [HttpPost]
        public ActionResult Index(decimal? cur_did, TB_DECORATION tb_dec)
        {
            cur_did = Session["CUR_DID"] as decimal?;
            Session.Remove("CUR_DID");
            foreach (TB_DECORATION my_dec in db.TB_DECORATION)
            {
                if (my_dec.ID == cur_did)
                {
                    db.TB_DECORATION.Remove(my_dec);
                    db.SaveChanges();
                    break;
                }
            }

            tb_dec.ID = Convert.ToDecimal(cur_did);
            tb_dec.FILE_NAME = Request.Form["DEC_FINAME"];
            db.TB_DECORATION.Add(tb_dec);
            db.SaveChanges();
            return RedirectToAction("Index", new { did = cur_did });
        }
    }
}
