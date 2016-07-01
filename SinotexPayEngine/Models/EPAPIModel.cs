using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinotexPayEngine.Models
{

    /// <summary>
    /// 易付宝回调传输数据模型基类
    /// </summary>
    [Serializable]
    public abstract class EPAPIModel
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// 签名方式
        /// </summary>
        public string signAlgorithm { get; set; }
        /// <summary>
        /// 易付宝密钥索引号
        /// </summary>
        public string keyIndex { get; set; }
    }
}
