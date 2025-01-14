using Shouldly;
using Volo.Abp.Authorization.Permissions;
using Xunit;

namespace Lion.AbpPro.BasicManagement;

public class Permission_Tests:BasicManagementApplicationTestBase
{
    
    [Fact]
    public void GetPolicySingleTest()
    {
        var grant = new MultiplePermissionGrantResult();
        
        grant.Result.Add("AbpIdentity.AuditLog", PermissionGrantResult.Granted);
        var result=  GetPolicy("AbpIdentity.AuditLog", grant);
        result.FirstOrDefault(e=>e=="AbpIdentity").ShouldBe("AbpIdentity");
    }
    
    
    [Fact]
    public void GetPolicyTest()
    {
        var grant = new MultiplePermissionGrantResult();
        grant.Result.Add("AbpIdentity.Roles", PermissionGrantResult.Granted);
        grant.Result.Add("AbpIdentity.Roles.Create", PermissionGrantResult.Granted);
        grant.Result.Add("AbpIdentity.Roles.Update", PermissionGrantResult.Undefined);
        
        grant.Result.Add("AbpIdentity.Users", PermissionGrantResult.Undefined);
        grant.Result.Add("AbpIdentity.Users.Create", PermissionGrantResult.Granted);
        grant.Result.Add("AbpIdentity.Users.Update", PermissionGrantResult.Granted);
        
        
        grant.Result.Add("AbpCode.CodeManagement.Project", PermissionGrantResult.Granted);
        grant.Result.Add("AbpCode.CodeManagement.Project.Create", PermissionGrantResult.Undefined);
        var result1=  GetPolicy("AbpIdentity.Roles.Update", grant);
     
        result1.FirstOrDefault(e=>e=="AbpIdentity").ShouldBe("AbpIdentity");
        result1.FirstOrDefault(e=>e=="AbpIdentity.Roles").ShouldBe("AbpIdentity.Roles");
        result1.FirstOrDefault(e=>e=="AbpIdentity.Roles.Update").ShouldBe(null);
        
        var result2=  GetPolicy("AbpIdentity.Users.Update", grant);
        result2.FirstOrDefault(e=>e=="AbpIdentity").ShouldBe(null);
        result2.FirstOrDefault(e=>e=="AbpIdentity.Users").ShouldBe(null);
        
        var result3=  GetPolicy("AbpCode.CodeManagement.Project", grant);
        result3.FirstOrDefault(e=>e=="AbpCode.CodeManagement").ShouldBe(null);
        result3.FirstOrDefault(e=>e=="AbpCode.CodeManagement.Project").ShouldBe(null);
    }
    
    
    /// <summary>
    /// 获取权限
    /// </summary>
    /// <remarks>比如设置了角色有权限AbpIdentity.Roles.Update,但是没有AbpIdentity.Roles权限，那么这个时候AbpIdentity.Roles应该是false</remarks>
    private List<string> GetPolicy(string policy, MultiplePermissionGrantResult permissions)
    {
        // AbpIdentity.Roles.Create
        // AbpIdentity 按照.分割，第一级是分组，剩下的才是权限
        var result = new List<string>();
        var split = policy.Split('.', StringSplitOptions.RemoveEmptyEntries);
        if (split.Length <= 0) return result;
        // 1. 获取当前policy组名
        var groupName = split.First();
        
        //2. 判断组下面的权限是菜单权限还是按钮权限
        // AbpIdentity.Roles 页面权限
        // AbpIdentity.Roles.Create 按钮权限
        // AbpIdentity.AuditLog 页面权限

        // 这个情况是菜单权限
        if (split.Length == 2)
        {
            var currentPolicyValue = permissions.Result.FirstOrDefault(e => e.Key == policy);

            if (currentPolicyValue.Value != PermissionGrantResult.Granted) return result;
            {
                result.Add(groupName);
            }
        }
        else
        {
            var currentPolicy = string.Empty;
            for (int i = 0; i < split.Length - 1; i++)
            {
                if (i == 0)
                {
                    currentPolicy += split[i];
                }
                else
                {
                    currentPolicy += "." + split[i];
                }
            }

            if (!currentPolicy.IsNullOrWhiteSpace())
            {
                var currentPolicyValue = permissions.Result.FirstOrDefault(e => e.Key == currentPolicy);
                if (currentPolicyValue.Value == PermissionGrantResult.Granted)
                {
                    result.Add(currentPolicy);
                    // 获取上级code
                    var parent = currentPolicy.Split('.', StringSplitOptions.RemoveEmptyEntries);
                    if (parent.Length > 1)
                    {
                        result.Add(parent.First());
                       
                    }
                }

                result.AddRange(GetPolicy(currentPolicy, permissions));
            }
        }
        

        return result.Distinct().ToList();
    }
}