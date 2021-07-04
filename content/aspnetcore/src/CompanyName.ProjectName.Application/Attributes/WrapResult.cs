namespace CompanyNameProjectName.Attributes
{
    public class WrapResult<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
        public WrapResult()
        {
            Success = true;
            Message = "Success";
            Data = default;
        }

        public void SetSucess(T data, string message = "Success")
        {
            Success = true;
            Data = data;
        }

        public void SetFail(string message = "Fail")
        {
            Success = false;
            Message = message;
        }

    }

}
