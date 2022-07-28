namespace Lion.AbpPro.Extension.Customs.Dtos
{
    public class WrapResult<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public int Code { get; set; }

        public WrapResult()
        {
            Success = true;
            Message = "Success";
            Data = default;
            Code = 200;
        }

        public void SetSuccess(T data, string message = "Success", int code = 200)
        {
            Success = true;
            Data = data;
            Code = code;
        }

        public void SetFail(string message = "Fail", int code = 500)
        {
            Success = false;
            Message = message;
            Code = code;
        }

    }
}