using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SinotexPayEngine.Models;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace SinotexPayEngine.Biz
{
    public class BaseBiz
    {
        /// <summary>
        /// 收银台支付接口提交的表单字符串生成方法
        /// </summary>
        /// <param name="param"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GenerateFormString(BaseSignParam param, string url)
        {
            string result = string.Empty;
            // 签名算法
            param.SignAlgorithm = "RSA";
            // 字符类型
            param.InputCharset = "UTF-8";
            // 公钥索引
            param.PublicKeyIndex = ConfigurationManager.AppSettings["SinotexPublicKeyIndex"];

            // 排除的参数
            string[] parasmeters = new string[2] {
              "Signature",
               "SignAlgorithm"
            };
            // 私钥字符串
            string privateKey = GetEPSinotexPrivateKey();

            // 签名结果
            var strSignature = BuildSignature.PCBuildMysign(
             param, privateKey, string.Empty, param.SignAlgorithm, param.InputCharset, parasmeters);
            // 签名
            param.Signature = strSignature;

            StringBuilder formParamters = new StringBuilder();
            PropertyInfo[] propertyInfos = param.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                // 将属性的首字母大写改为小写
                string name = propertyInfo.Name.Substring(0, 1).ToLower();
                name = name + propertyInfo.Name.Substring(1, propertyInfo.Name.Length - 1);
                // 判断是否为空
                string paramValue = propertyInfo.GetValue(param, null) == null ? "" : propertyInfo.GetValue(param, null).ToString();
                formParamters.Append(BuildInputHiddenFieldInForm(name, paramValue));
            }
            // 最终提交的form表单
            String formString = BuildForm(url, formParamters.ToString());

            return formString;
        }

        /// <summary>
        /// 校验接口签名
        /// </summary>
        /// <returns></returns>
        public static string ValidationSignature(EPAPIModel returnData)
        {
            // 排除的参数
            string[] parasmeters = new string[2] {
              "Signature",
               "SignAlgorithm"
            };
            // 私钥字符串
            string privateKey = GetEPSinotexPrivateKey();
            // 签名结果
            var strSignature = BuildSignature.PCBuildMysign(
             returnData, privateKey, string.Empty, returnData.signAlgorithm, "UTF-8", parasmeters);
            if (!returnData.signature.Equals(strSignature))
            {
                return "签名校验失败";
            }
            return string.Empty;
        }

        private static string BuildInputHiddenFieldInForm(string paramName, string paramValue)
        {
            String line = "<input type=\"hidden\" name=\"\" value=\"\">";
            if (paramName == null || paramValue == null)
            {
                return "";
            }
            else
            {
                paramValue = paramValue.Replace("\"", "&quot;");
                int nameSize = "name=".Length + 1;
                int valueSize = "value=".Length + 1;
                line = line.Substring(0, line.IndexOf("name=") + nameSize) + paramName
                        + line.Substring(line.IndexOf("name") + nameSize);
                line = line.Substring(0, line.IndexOf("value=") + valueSize) + paramValue
                        + line.Substring(line.IndexOf("value=") + valueSize);
                return line;
            }
        }

        private static string BuildForm(string url, string inputFiled)
        {
            StringBuilder form = new StringBuilder();
            form.Append("<form name=\"punchout_form\" id=\"punchout_form\" method=\"post\" action=\"");
            form.Append(url);
            form.Append("\">");
            form.Append(inputFiled);
            form.Append("</form><input type=\"submit\" value=\"确认\" style=\"display:none;\">");
            //form.Append("<script>document.forms['punchout_form'].submit();</script>");
            return form.ToString();
        }

        /// <summary>
        /// 获取私钥字符串
        /// </summary>
        /// <returns></returns>
        private static string GetEPSinotexPrivateKey()
        {
            // 私钥字符串
            string privateKey;

            //私钥字符串可存入全局缓存
#if DEBUG
            privateKey = File.ReadAllText(
                Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~"), "privateKey.txt"));
#else
            privateKey = File.ReadAllText(Path.Combine("C:/", "SinotexPrivateKey.txt"));
#endif
            return privateKey;
        }

    }
}
