using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinotexPayEngine.Models
{
    public class EBOrder
    {
        private string _salerUserNo;
        /// <summary>
        /// 卖家易付宝会员编号，登录名，商户号，赞歌参数至少有一项不为空
        /// </summary>
        public string salerUserNo
        {
            get
            {
                return _salerUserNo == null ? string.Empty : _salerUserNo;
            }
            set
            {
                _salerUserNo = value;
            }
        }

        private string _salerMerchantNo;
        /// <summary>
        /// 卖家易付宝商户号
        /// </summary>
        public string salerMerchantNo
        {
            get
            {
                return _salerMerchantNo == null ? string.Empty : _salerMerchantNo;
            }
            set
            {
                _salerMerchantNo = value;
            }
        }

        private string _salerAlias;
        /// <summary>
        /// 卖家易付宝登录名
        /// </summary>
        public string salerAlias
        {
            get
            {
                return _salerAlias == null ? string.Empty : _salerAlias;
            }
            set
            {
                _salerAlias = value;
            }
        }
        private string _royaltyParameters;
        /// <summary>
        /// 分润账号集合
        /// </summary>
        public string royaltyParameters
        {
            get
            {
                return _royaltyParameters == null ? string.Empty : _royaltyParameters;
            }
            set
            {
                _royaltyParameters = value;
            }
        }
        /// <summary>
        /// 订单类型。 01：即时到账订单 02：担保支付订单
        /// </summary>
        public string orderType { get; set; }
        /// <summary>
        /// 商户唯一订单号
        /// </summary>
        public string outOrderNo { get; set; }
        /// <summary>
        /// 商户展示订单号
        /// </summary>
        public string merchantOrderNo { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string goodsName { get; set; }
        /// <summary>
        /// 商品类型
        /// </summary>
        public string goodsType { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// 商品展示网址
        /// </summary>
        public string showUrl { get; set; }
        /// <summary>
        /// 收货人信息。格式： 姓名|地址|邮编|电话
        /// </summary>
        public string receiveInfo { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public string orderAmount { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public string quantity { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public string orderTime { get; set; }
        /// <summary>
        /// 订单有效期
        /// </summary>
        public string payTimeout { get; set; }

        private string _tunnelData;
        /// <summary>
        /// 扩展信息
        /// </summary>
        public string tunnelData
        {
            get
            {
                return _tunnelData == null ? string.Empty : _tunnelData;
            }
            set
            {
                _tunnelData = value;
            }
        }

    }
}
