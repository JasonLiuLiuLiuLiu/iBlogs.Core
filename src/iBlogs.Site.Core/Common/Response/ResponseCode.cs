using System.ComponentModel;

namespace iBlogs.Site.Core.Common.Response
{
    public enum ResponseCode
    {
        [Description("成功")]
        Success = 0,

        [Description("参数错误")]
        ParameterError = 10000,

        [Description("空参数错误")]
        NullParameterError = 10001,

        [Description("参数格式错误")]
        ParameterFormatError = 10002,

        [Description("参数校验错误")]
        ValidationParameterError = 10003,

        [Description("参数格式错误")]
        RequestParameterError = 10004,

        [Description("请求空参数错误")]
        NotFoundError = 20000,

        /// <summary>
        /// 超过数据的最低限制
        /// </summary>
        [Description("超过数据的最低限制")]
        OutOfRangeError = 20001,

        /// <summary>
        /// 数据修改限制
        /// </summary>
        [Description("数据修改限制")]
        OperationLimit = 20002,

        /// <summary>
        /// 已被使用
        /// </summary>
        [Description("已存在")]
        PresenceError = 20003,

        /// <summary>
        /// 值重复
        /// </summary>
        [Description("值重复错误")]
        ValueDuplicateError = 20004,

        [Description("权限不足")]
        Forbidden = 40000,

        [Description("系统错误")]
        SystemError = 50000,
    }
}