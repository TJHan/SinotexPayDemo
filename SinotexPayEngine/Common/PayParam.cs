using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinotexPayEngine.Models
{
    /// <summary>
    /// 支付签名参数
    /// </summary>
    [Serializable]
    public class PayingSignParam : BaseSignParam
    {
        /// <summary>
        /// 商户号
        /// </summary>
        public string MerchantNo { get; set; }

        /// <summary>
        /// 商户域名
        /// </summary>
        public string MerchantDomain { get; set; }
        
        /// <summary>
        /// 易付宝服务器主动通知商户网站里指定的页面 URL 路径
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 易付宝处理完请求后，当前页面自动跳转到商户网站里指定页面的 http 路径
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 请求报文提交时间  取当前
        /// </summary>
        public string SubmitTime { get; set; }

        /// <summary>
        /// 订单数据集
        /// </summary>
        public string Orders { get; set; }
    }
}
