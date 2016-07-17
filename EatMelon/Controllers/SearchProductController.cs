using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EatMelon.Models;

namespace EatMelon.Controllers
{
    public class SearchViewModel
    {
        public List<ProductWithPicture> p;
        public UserMessage u;
    }


    public class ProductWithPicture
    {
        public TB_PRODUCT Product;
        public TB_PRO_PIC Picture;
    }

    public class UserMessage
    {
        public decimal id {  get; set; }
        public string name { get; set; }
    }

    public class SearchProductController : Controller
    {
        UserMessage userMes = new UserMessage();
        
        Pro_pics dbPic = new Pro_pics();
        Products dbPro = new Products();
        List<ProductWithPicture> Items = new List<ProductWithPicture>();

        // GET: SearchProduct
        public ActionResult Index()
        {
            return View(Items);
        }
        [HttpPost]
        public ActionResult Index(string keyString)
        {
            userMes = Session["UserMessage"] as UserMessage;

            keyString = Request.Form["keyString"];

            TempData["KeyString"] = keyString;

            SearchByName(keyString);

            return View(Items);
        }

        void SearchByName(string name)
        {

            if (dbPro.TB_PRODUCT.Count() == 0)
            {
                return;
            }
            foreach (TB_PRODUCT myProduct in dbPro.TB_PRODUCT)
            {
                if (myProduct.NAME.Contains(name))
                {
                    ProductWithPicture myViewModel = new ProductWithPicture();
                    myViewModel.Product = myProduct;
                    SearchPictures(myViewModel, myProduct.S_ID, myProduct.ID);
                    Items.Add(myViewModel);
                }
            }
        }

        void SearchPictures(ProductWithPicture myViewModel, decimal s_id, decimal p_id)
        {
            if (dbPic.TB_PRO_PIC.Count() == 0)
            {
                return;
            }
            foreach (TB_PRO_PIC myPicture in dbPic.TB_PRO_PIC)
            {
                if (myPicture.S_ID == s_id && myPicture.P_ID == p_id)
                {
                    myViewModel.Picture = myPicture;
                    return;
                }
            }
        }

    }
}