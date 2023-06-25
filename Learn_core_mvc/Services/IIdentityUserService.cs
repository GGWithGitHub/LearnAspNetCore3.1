namespace Learn_core_mvc.Services
{
    public interface IIdentityUserService
    {
        string GetUserId();
        string GetUserName();
        bool IsAuthenticated();
    }
}