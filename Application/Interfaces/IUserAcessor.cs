namespace Application.Interfaces
{
    public interface IUserAcessor
    {
        string GetCurrentUsername();
        string GetCurrentEmail();
        string GetCurrentDisplayName();
    }
}