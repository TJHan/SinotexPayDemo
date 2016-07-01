using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinotexPayEngine.Models
{
    /// <summary>
    /// 支付完成回调页面时传递的数据模型
    /// </summary>
    public class PayReturnOrderModel
    {
        public string responseCode { get; set; }
        public string signAlgorithm { get; set; }
        public string signature { get; set; }
        public string keyIndex { get; set; }
        public string merchantOrderNos { get; set; }
    }
}
