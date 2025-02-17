namespace Lion.AbpPro.Cli.Dto;

public class ErrorResponse
{
    public Error error { get; set; }
}

public class Error
{
    public object code { get; set; }
    public string message { get; set; }
    public object details { get; set; }
    public object data { get; set; }
    public object validationErrors { get; set; }
}