using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SinotexPayEngine.Models
{
    public class BuildSignature
    { /// <summary>
        /// 生成签名结果
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="data">数据</param>
        /// <param name="privateKeyPath">私钥路径</param>
        /// <param name="keyPassword">私钥加密密码</param>
        /// <param name="signType">签名算法</param>
        /// <param name="inputCharset">编码类型</param>
        /// <param name="except">排除的参数</param>
        /// <returns>生成签名结果字符串</returns>
        public static string PCBuildMysign<T>(T data, string privateKeyPath, string keyPassword, string signType, string inputCharset, params string[] except)
        {
            PropertyInfo[] exceptPropertyInfo = new PropertyInfo[except.Length];
            for (int i = 0; i < except.Length; i++)
            {
                if (data.GetType().GetProperties().Where(d => d.Name.ToLower() == except[i].ToLower()) != null)
                {
                    exceptPropertyInfo[i] = data.GetType().GetProperties().Where(p => p.Name.ToLower() == except[i].ToLower()).ToArray()[0];
                }
            }
            StringBuilder strBuilder = new StringBuilder();
            // 过滤掉不签名参数，并按参数名从a-z排序
            PropertyInfo[] properties = data.GetType().GetProperties().Except(exceptPropertyInfo).Where(p => 1 == 1).OrderBy(p => p.Name).ToArray();
            // 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
            foreach (PropertyInfo pro in properties)
            {
                // 将属性的首字母大写改为小写
                string name = pro.Name.Substring(0, 1).ToLower();
                name = name + pro.Name.Substring(1, pro.Name.Length - 1);

                var value = pro.GetValue(data, null);
                // 为空判断
                if (value == null)
                {
                    value = "";
                }

                //检索value值是否是实体集合，若是实体集合则需要序列化成json字符串
                string valueStr = value.ToString();
                if (value is System.Collections.IList)
                {
                    valueStr = JsonConvert.SerializeObject(value);
                }
                strBuilder.AppendFormat("{0}={1}&", name, valueStr);

            }
            // 待签名字符串，去掉最后一位&字符
            string unsignedStr = string.Format("{0}", strBuilder.ToString().Substring(0, strBuilder.Length - 1));

            // 签名结果，把待签名字符串进行MD5签名然后进行字符大写转换最后进行RSA签名
            string mysign = "";
            mysign = MD5Sign(unsignedStr, inputCharset);
            // 摘要大写
            mysign = mysign.ToUpper();
            switch (signType.ToUpper())
            {
                case "RSA":
                    mysign = RSAPKCS12Sign(mysign, privateKeyPath, keyPassword, inputCharset);
                    break;
                default:
                    mysign = "";
                    break;
            }

            return mysign;
        }

        /// <summary>
        /// 签名字符串
        /// </summary>
        /// <param name="unsignStr">需要签名的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <returns>签名结果</returns>
        public static string MD5Sign(string unsignStr, string inputCharset)
        {
            StringBuilder sb = new StringBuilder(32);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(inputCharset).GetBytes(unsignStr));
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="content">需要签名的内容</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <returns></returns>
        public static string RSAPKCS12Sign(string unsignStr, string privateKeyParh, string keyPassword, string inputCharset)
        {
            RSACryptoServiceProvider crypt = new RSACryptoServiceProvider();
            crypt.FromXmlString(privateKeyParh);
            SHA1Managed sha1 = new SHA1Managed();
            Encoding code = Encoding.GetEncoding(inputCharset);
            byte[] data = code.GetBytes(unsignStr);
            byte[] hash = sha1.ComputeHash(data);
            byte[] signData = crypt.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
            return Convert.ToBase64String(signData);
        }

        /// <summary>
        /// base 64编码
        /// </summary>
        /// <param name="data">待编码数据</param>
        /// <returns></returns>
        public static string Base64Encode(string data)
        {
            byte[] encodeBase64Byte = new byte[data.Length];
            encodeBase64Byte = System.Text.Encoding.UTF8.GetBytes(data);
            string encodedData = Convert.ToBase64String(encodeBase64Byte);
            return encodedData;
        }
    }
}
