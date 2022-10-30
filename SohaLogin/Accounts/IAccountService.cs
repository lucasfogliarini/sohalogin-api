namespace SohaLogin.Accounts
{
    public interface IAccountService
    {
        AccountOuput Login(string email, string password);
    }
}
