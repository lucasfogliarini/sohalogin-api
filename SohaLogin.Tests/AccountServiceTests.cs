using NSubstitute;
using SohaLogin.Accounts;
using SohaLogin.Database;
using System.ComponentModel.DataAnnotations;

namespace SohaLogin.Tests
{
    public class AccountServiceTests
    {
        //CA03 - Uma mensagem deverá informar que o usuário é obrigatório, bem como que a senha é obrigatória.
        [Theory(DisplayName = "Email e Senha devem ser obrigatórios")]
        [InlineData(null, null)]
        [InlineData("email1", null)]
        [InlineData("", "password1")]
        public void Login_(string email, string password)
        {
            var sohaLoginDatabase = Substitute.For<ISohaLoginDatabase>();
            var accountService = new AccountService(sohaLoginDatabase);

            Assert.Throws<ValidationException>(() =>
            {
                accountService.Login(email, password);
            });
        }
    }
}