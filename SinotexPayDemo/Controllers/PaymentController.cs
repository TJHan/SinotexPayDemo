using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SinotexPayDemo.Filters;
using SinotexPayEngine.Models;

namespace SinotexPayDemo.Controllers
{
    [RoutePrefix("api/Payment")]
    public class PaymentController : ApiController
    {
        /// <summary>
        /// 支付成功系统通知接口
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]        
        [Route("PayReturn")]
        [EBAPIAuth]
        public string SuccessNotification(PaySuccessModel model)
        {
            if (model != null)
            { }
            return "true";
        }
    }
}
