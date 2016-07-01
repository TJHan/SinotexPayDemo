using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinotexPayEngine.Models
{
    /// <summary>
    /// 收银台模式订单支付回调传递参数模型
    /// </summary>
    [Serializable]
    public class PaySuccessModel : EPAPIModel
    {
        /// <summary>
        /// 响应码
        /// </summary>
        [Required]
        public string responseCode { get; set; }

        /// <summary>
        /// 支付信息描述
        /// </summary>
        public string payDescription { get; set; }

        /// <summary>
        /// 订单数据集
        /// </summary>
        [Required]
        public List<Orders> orders { get; set; }
    }
    [Serializable]
    public class Orders
    {
        /// <summary>
        /// 商户唯一订单号
        /// </summary>
        public string outOrderNo { get; set; }
        /// <summary>
        /// 商户展示订单号
        /// </summary>
        public string merchantOrderNo { get; set; }
        /// <summary>
        /// 易付宝订单号
        /// </summary>
        public string orderId { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public string orderTime { get; set; }
        /// <summary>
        /// 支付完成时间
        /// </summary>
        public string payTime { get; set; }
        /// <summary>
        /// 买方易付宝用户ID
        /// </summary>
        public string buyerUserNo { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public long orderAmount { get; set; }
        /// <summary>
        /// 扩展信息
        /// </summary>
        public string tunnelData { get; set; }
        /// <summary>
        /// 支付方式信息
        /// </summary>
        public List<PayDetails> payDetails { get; set; }
    }
    [Serializable]
    public class PayDetails
    {
        /// <summary>
        /// 支付类型编码
        /// </summary>
        public string payTypeCode { get; set; }
        /// <summary>
        /// 支付方式金额
        /// </summary>
        public long payAmount { get; set; }
        /// <summary>
        /// 入款渠道编码
        /// </summary>
        public string rcsCode { get; set; }
        /// <summary>
        /// 资金供应商
        /// </summary>
        public string payProvider { get; set; }
    }
}
