namespace Lion.AbpPro.FileManagement.Files.Dto;

public class CreateFileInput
{
    [Required(ErrorMessage = "文件名不能为空")] public string FileName { get; set; }

    [Required(ErrorMessage = "文件地址不能为空")] public string FilePath { get; set; }
}