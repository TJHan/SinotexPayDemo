using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SinotexPayEngine.Models;
using SinotexPayEngine.Biz;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace SinotexPayDemo.Filters
{
    /// <summary>
    /// 接口调用校验特性
    /// </summary>
    public class EBAPIAuthAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            string requestContent = actionContext.Request.Content.ReadAsStringAsync().Result;
            PaySuccessModel model = new PaySuccessModel();
            model = JsonConvert.DeserializeObject<PaySuccessModel>(requestContent);
            string result = BaseBiz.ValidationSignature(model);
            if (!string.IsNullOrEmpty(result))
            {
                var media = new JsonMediaTypeFormatter();

                ObjectContent content = new ObjectContent(typeof(JsonResult<string>), new JsonResult<string>
                {
                    ErrorCode = "0001",
                    Message = result,
                    Data = string.Empty
                }, media);
                actionContext.Response = new HttpResponseMessage() { Content = content };                
            }
        }
    }
}