namespace Lion.AbpPro.FileManagement.Files;

public static class SizeHelper
{
    public static string BeautifySize(long size)
    {
        if (size == 0 || size == 1)
        {
            return $"{size} Byte";
        }

        if (size >= FileManagementConsts.Terabyte)
        {
            var fixedSize = ((float)size / (float)FileManagementConsts.Terabyte);
            return $"{FormatSize(fixedSize)} TB";
        }

        if (size >= FileManagementConsts.Gigabyte)
        {
            var fixedSize = ((float)size / (float)FileManagementConsts.Gigabyte);
            return $"{FormatSize(fixedSize)} GB";
        }

        if (size >= FileManagementConsts.Megabyte)
        {
            var fixedSize = ((float)size / (float)FileManagementConsts.Megabyte);
            return $"{FormatSize(fixedSize)} MB";
        }

        if (size >= FileManagementConsts.Kilobyte)
        {
            var fixedSize = ((float)size / (float)FileManagementConsts.Kilobyte);
            return $"{FormatSize(fixedSize)} KB";
        }

        return $"{size} B";
    }

    public static string FormatSize(float size)
    {
        var s = $"{size:0.00}";

        return s.EndsWith("00") ? ((int)size).ToString() : s;
    }
}