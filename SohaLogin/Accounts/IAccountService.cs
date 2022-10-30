namespace SohaLogin.Accounts
{
    public interface IAccountService
    {
        void Login(string email, string password);
    }
}
