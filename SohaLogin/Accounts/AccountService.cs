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
            ValidateLogin(email, password);
        }

        private void ValidateLogin(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                throw new ValidationException("E-mail e senha são obrigatórios para realizar o login.");

            ValidateEmail(email);
            ValidatePassword(password);
        }
        private void ValidateEmail(string email)
        {
            var mailValidator = new EmailAddressAttribute();
            if(!mailValidator.IsValid(email))
                throw new ValidationException("E-mail deve ter um formato válido. ex.: 'username@domain.com'");
        }
        private void ValidatePassword(string password)
        {
            if (password == null || password.Length < 4 || password.Length > 15)
                throw new ValidationException("A senha deverá ter no mínimo 4 caracteres e no máximo 15");
        }
    }
}
