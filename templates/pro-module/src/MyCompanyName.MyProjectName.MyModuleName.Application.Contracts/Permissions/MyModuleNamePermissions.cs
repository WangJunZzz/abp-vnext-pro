namespace MyCompanyName.MyProjectName.MyModuleName.Permissions
{
    public class MyModuleNamePermissions
    {
   

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(MyModuleNamePermissions));
        }
    }
}