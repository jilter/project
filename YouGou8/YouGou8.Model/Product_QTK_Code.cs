using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouGou8.Model
{

    public enum Product_QTK_Code
    {
        /// <summary>
        /// 系统繁忙，此时请开发者稍候再试
        /// </summary>
        SysBusy = -1,
        /// <summary>
        /// 请求成功
        /// </summary>
        Success = 10000,
        /// <summary>
        /// 必传参数未传递或值为空
        /// </summary>
        ParamReq = 10001,
        /// <summary>
        /// 参数格式错误或不在规定范围内
        /// </summary>
        ParamErr = 10002,
        /// <summary>
        /// 没有权限
        /// </summary>
        NoPromise = 20001,
        /// <summary>
        /// 超出使用限制
        /// </summary>
        Limit = 20002,
        /// <summary>
        /// API版本号错误
        /// </summary>
        APIVerErr = 30001,
        /// <summary>
        /// 该版本不存在
        /// </summary>
        APIVerNoExist = 30002
    }
}
