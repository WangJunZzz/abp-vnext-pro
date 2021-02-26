using System;
using System.Collections.Generic;
using System.Text;

namespace Zzz.DTOs.Public
{
    public class ApiResult
    {
        public int Code { get; set; }

        public string Msg { get; set; } = "Success";

        public object Data { get; set; } = default;

        private ApiResult()
        {

        }

        public ApiResult(ApiCodeEnum code)
        {
            Code = (int)code;
        }

        public ApiResult(ApiCodeEnum code, string msg)
        {
            Code = (int)code;
            Msg = msg;
        }

        public ApiResult(ApiCodeEnum code, string msg, object data = default)
        {
            Code = (int)code;
            Msg = msg;
            Data = data;
        }

        public static ApiResult Ok()
        {
            return new ApiResult(ApiCodeEnum.成功);
        }

        public static ApiResult Ok(string msg)
        {
            return new ApiResult(ApiCodeEnum.成功, msg);
        }

        public static ApiResult Ok(object data)
        {
            return new ApiResult(ApiCodeEnum.成功, "Success", data);
        }


        public static ApiResult Ok(string msg, object datas)
        {
            return new ApiResult(ApiCodeEnum.成功, msg, datas);
        }



        public static ApiResult Error()
        {
            return new ApiResult(ApiCodeEnum.失败, "Failed");
        }

        public static ApiResult Error(string msg= "Failed")
        {
            return new ApiResult(ApiCodeEnum.失败, msg);
        }

        public static ApiResult Error(object data)
        {
            return new ApiResult(ApiCodeEnum.失败, "Failed", data);
        }
    }
}
