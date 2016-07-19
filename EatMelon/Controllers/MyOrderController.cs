using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EatMelon.Models;

namespace EatMelon.Controllers
{
    public class OrderAndContains
    {
        public TB_ORDER Order;
        public List<TB_CONTAINS> Products;
        public string Date;

        public OrderAndContains()
        {
            Order = new TB_ORDER();
            Products = new List<TB_CONTAINS>();
        }
    }

    public class UserAndOrder
    {
        public TB_USER User;
        public List<OrderAndContains> Order;

        public UserAndOrder()
        {
            User = new TB_USER();
            Order = new List<OrderAndContains>();
        }
    }

    public class OrderMessage
    {
        public int index { get; set; }
        public string order_id { get; set; }
        public string store_name { get; set; }
        public string product_name { get; set; }
        public string img_src { get; set; }
        public decimal total_price { get; set; }
        public string state { get; set; }
        public string state_color { get; set; }
        public string time { get; set; }
        public List<string> page_btn { get; set; }
    }

    public class MyOrderController : Controller
    {
        private Orders dbOrd = new Orders();
        private Contains dbCon = new Contains();
        private Stores dbSto = new Stores();
        private Products dbPro = new Products();
        private UserMessage userMes = new UserMessage();
        private UserAndOrder UserOrder = null;

        // GET: MyOrder
        public ActionResult Index(decimal cur_page = 1)
        {
            userMes = Session["UserMessage"] as UserMessage;
            //userMes.id = 35;
            if (UserOrder == null)
            {
                UserOrder = new UserAndOrder();
                foreach (TB_ORDER myorder in dbOrd.TB_ORDER)
                {
                    if (myorder.U_ID == userMes.id)
                    {
                        OrderAndContains new_order = new OrderAndContains();
                        new_order.Order = myorder;
                        new_order.Products = (from a in dbCon.TB_CONTAINS where a.O_ID == myorder.ID select a).ToList();
                        new_order.Date = myorder.TIME.ToString();
                        if (new_order.Order.STATE >= 2)
                        {
                            UserOrder.Order.Add(new_order);
                        }
                    }
                }
            }

            List<OrderMessage> this_page = new List<OrderMessage>();
            int allPtr = (int)(cur_page - 1) * 5 + 1;
            int thisPtr = 0;
            foreach (OrderAndContains myorder in UserOrder.Order)
            {
                if (allPtr <= UserOrder.Order.Count && thisPtr < 5)
                {
                    this_page.Add(new OrderMessage());
                    //序号
                    this_page[thisPtr].index = thisPtr + 1;
                    //订单号
                    this_page[thisPtr].order_id = "9000" + myorder.Date.Substring(2, 2) +
                        ("000" + myorder.Order.ID.ToString()).Remove(0, myorder.Order.ID.ToString().Length - 1);
                    //商铺号
                    this_page[thisPtr].store_name = dbSto.Database.SqlQuery<string>(
                        "SELECT NAME FROM TB_STORE WHERE ID = " + myorder.Order.S_ID).ToList().FirstOrDefault();
                    //商品名
                    this_page[thisPtr].product_name = dbPro.Database.SqlQuery<string>(
                        "SELECT NAME FROM TB_PRODUCT WHERE ID = " + myorder.Products[0].P_ID).ToList().FirstOrDefault();
                    int number = myorder.Products.Count;
                    if (number > 1)
                    {
                        this_page[thisPtr].product_name += "等";
                    }
                    //商品图

                    //总价
                    this_page[thisPtr].total_price = (decimal)myorder.Order.TOTAL_PRICE;
                    //状态
                    if (myorder.Order.STATE == 2)
                    {
                        this_page[thisPtr].state = "未支付";
                        this_page[thisPtr].state_color = "";
                    }
                    else if (myorder.Order.STATE == 3)
                    {
                        this_page[thisPtr].state = "已付款";
                        this_page[thisPtr].state_color = "";
                    }
                    else if (myorder.Order.STATE == 4)
                    {
                        this_page[thisPtr].state = "已发货";
                        this_page[thisPtr].state_color = "";
                    }
                    else if (myorder.Order.STATE == 5)
                    {
                        this_page[thisPtr].state = "待评论";
                        this_page[thisPtr].state_color = "danger";
                    }
                    else if (myorder.Order.STATE == 6)
                    {
                        this_page[thisPtr].state = "已完成";
                        this_page[thisPtr].state_color = "success";
                    }
                    //创建时间
                    this_page[thisPtr].time = myorder.Date;
                    //循环变量
                    allPtr++;
                    thisPtr++;
                }
            }
            if (this_page.Count != 0)
            {
                this_page.FirstOrDefault().page_btn = new List<string>();
                for (int i = 0; i < 5; i++)
                {
                    if (i == cur_page - 1)
                    {
                        this_page.FirstOrDefault().page_btn.Add("active");
                    }
                    else
                    {
                        this_page.FirstOrDefault().page_btn.Add("");
                    }
                }
                return View(this_page);
            }
            else
            {
                return RedirectToAction("Index", new { cur_page = 1 });
            }
        }
    }
}