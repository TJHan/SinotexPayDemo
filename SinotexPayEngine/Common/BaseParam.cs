using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinotexPayEngine.Models
{
    /// <summary>
    /// 签名参数基类
    /// </summary>
    public class BaseSignParam
    {
        /// <summary>
        /// 签名 根据私钥和摘要值一起使用RSA算法生成签名signature
        /// </summary>
        public string Signature
        {
            get;
            set;
        }

        /// <summary>
        /// 编码类型  UTF-8
        /// </summary>
        public string InputCharset
        {
            get;
            set;
        }

        /// <summary>
        /// 签名算法  RSA
        /// </summary>
        public string SignAlgorithm
        {
            get;
            set;
        }

        /// <summary>
        /// 商户公钥统一索引
        /// </summary>
        public string PublicKeyIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
    }
}
