using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SinotexPayDemo.Models;
using SinotexPayEngine.Biz;
using SinotexPayEngine.Models;

namespace SinotexPayDemo.Areas.Payment.Controllers
{
    public class PayController : Controller
    {
        // GET: Payment/Pay
        public ActionResult Index()
        {
            List<OrderModel> list = new List<OrderModel>();
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                OrderModel order = new OrderModel();
                order.OrderName = string.Format(@"纱线商品-{0}", i);
                order.OrderNumber = string.Format(@"{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), random.Next(999999));
                order.Price = 9.99M;
                list.Add(order);
            }

            return View(list);
        }

        public ActionResult Details(string orderName, string orderNo, Decimal orderPrice)
        {
            OrderModel model = new OrderModel();
            model.OrderName = orderName;
            model.OrderNumber = orderNo;
            model.Price = orderPrice;
            return View(model);
        }

        /// <summary>
        /// 支付页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Pay(OrderModel model)
        {
            EBOrder order = new EBOrder();
            order.outOrderNo = model.OrderNumber;
            order.goodsName = model.OrderName;
            //订单金额需转化
            order.orderAmount = (model.Price * model.GoodsCount * 100).ToString();
            order.price = model.Price.ToString();
            order.goodsType = "011001";
            Random random = new Random();
            order.merchantOrderNo = string.Format(@"{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), random.Next(99999));
            order.quantity = model.GoodsCount.ToString();
            order.receiveInfo = "张三|青岛市李沧区|266000|13888888888";
            order.showUrl = "http://ecottonyarn.com/newedit/Default/Spot/Show/b36af8d8-58ac-4983-896b-85fd0e3bb279";
            order.body = "这件商品是纱线商品";
            //卖家易宝商户号
            order.salerMerchantNo = "70056576";
            string html = PayBiz.EBPayPlatformRequestBuilder(new List<EBOrder> { order });
            return View((object)html);
        }

        /// <summary>
        /// 支付成功后回调页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PayReturn(PayReturnOrderModel model)
        {

            return View();
        }

    }
}