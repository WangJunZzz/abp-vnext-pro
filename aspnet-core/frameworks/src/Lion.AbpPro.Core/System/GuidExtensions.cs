namespace System;

public static class GuidExtensions
{
    public static bool IsNullOrEmpty(this Guid? value)
    {
        return value == null || value.Value == Guid.Empty;
    }
    
    public static bool IsNullOrEmpty(this Guid value)
    {
        return value == Guid.Empty;
    }
}