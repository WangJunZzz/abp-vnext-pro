namespace Lion.AbpPro.FileManagement.Security;

[Dependency(ReplaceServices = true)]
public class FakeCurrentPrincipalAccessor : ThreadCurrentPrincipalAccessor
{
    private ClaimsPrincipal _principal;

    protected override ClaimsPrincipal GetClaimsPrincipal()
    {
        return GetPrincipal();
    }

    private ClaimsPrincipal GetPrincipal()
    {
        if (_principal == null)
        {
            lock (this)
            {
                if (_principal == null)
                {
                    _principal = new ClaimsPrincipal(
                        new ClaimsIdentity(
                            new List<Claim>
                            {
                                new(AbpClaimTypes.UserId, "2e701e62-0953-4dd3-910b-dc6cc93ccb0d"),
                                new(AbpClaimTypes.UserName, "admin"),
                                new(AbpClaimTypes.Email, "admin@abp.io")
                            }
                        )
                    );
                }
            }
        }

        return _principal;
    }
}