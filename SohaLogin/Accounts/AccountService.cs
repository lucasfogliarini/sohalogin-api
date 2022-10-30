using SohaLogin.Database;
using System.ComponentModel.DataAnnotations;

namespace SohaLogin.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly ISohaLoginDatabase _sohaLoginDatabase;

        public AccountService(ISohaLoginDatabase sohaLoginDatabase)
        {
            _sohaLoginDatabase = sohaLoginDatabase;
        }

        public void Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                throw new ValidationException("E-mail e senha são obrigatórios para realizar o login.");
        }
    }
}
