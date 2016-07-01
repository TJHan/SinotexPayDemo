using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace SinotexPayEngine.Models
{
    /// <summary>
    /// 接口回执数据信息类
    /// </summary>
    /// <typeparam name="T">回执数据的具体数据类型</typeparam>
    public class JsonResult<T>
    {
        [DataMember(Name = "errorCode")]
        public string ErrorCode { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "data")]
        public T Data { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
