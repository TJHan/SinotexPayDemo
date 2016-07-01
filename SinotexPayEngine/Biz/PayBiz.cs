using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SinotexPayEngine.Models;
using System.Configuration;
using Newtonsoft.Json;

namespace SinotexPayEngine.Biz
{
    /// <summary>
    /// 易付宝支付接口请求信息生成类
    /// </summary>
    public class PayBiz:BaseBiz
    {
        /// <summary>
        /// 收银台模式下单支付接口请求生成方法
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static string EBPayPlatformRequestBuilder(List<EBOrder> orderList)
        {
            //收银台模式支付接口URL
            string url = "https://paymentsandbox.suning.com/epps-ebpp/eBankGateway/paymentOrder.htm";
            PayingSignParam signParam = new PayingSignParam();
            //作为易宝接入方的易纱商户号，由易付宝提供给商户
            signParam.MerchantNo = ConfigurationManager.AppSettings["SinotexMerchantNo"];
            signParam.MerchantDomain = "";
            signParam.NotifyUrl = "http://192.168.1.220:80/PayAPI/api/payment/PaySuccessReturn";
            signParam.ReturnUrl = "http://ecottonyarn.com/";
            signParam.Version = "1.0";
            signParam.SubmitTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            signParam.Orders = GetOrdersString(orderList);
            return GenerateFormString(signParam, url);
        }

        /// <summary>
        /// 设置支付接口中订单参数的详细信息并反序列化成json字符串
        /// </summary>
        /// <param name="orderLists"></param>
        /// <returns></returns>
        private static string GetOrdersString(List<EBOrder> orderLists)
        {
            string result = string.Empty;
            foreach (var item in orderLists)
            {
                item.body = BuildSignature.Base64Encode(item.body);
                item.currency = "CNY";
                item.goodsName = BuildSignature.Base64Encode(item.goodsName);
                item.orderTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                item.orderType = "01";
                item.payTimeout = "7d";
                item.receiveInfo = BuildSignature.Base64Encode(item.receiveInfo);                
            }
            result = JsonConvert.SerializeObject(orderLists);
            return result;
        }
    }
}
