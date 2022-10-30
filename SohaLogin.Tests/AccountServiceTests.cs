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
        public void Login_ShouldThrowsException_WhenEmailAndPasswordIsEmpty(string email, string password)
        {
            var sohaLoginDatabase = Substitute.For<ISohaLoginDatabase>();
            var accountService = new AccountService(sohaLoginDatabase);

            Assert.Throws<ValidationException>(() =>
            {
                accountService.Login(email, password);
            });
        }

        //CA04 - O usuário deverá ser um e-mail e deverá haver uma validação para caso não seja um e-mail válido
        [Theory(DisplayName = "E-mail deve ter um formato válido.")]
        [InlineData(null)]
        [InlineData("email1")]
        [InlineData("email1@")]
        public void Login_ShouldThrowsException_WhenEmailIsInvalid(string email)
        {
            var sohaLoginDatabase = Substitute.For<ISohaLoginDatabase>();
            var accountService = new AccountService(sohaLoginDatabase);

            Assert.Throws<ValidationException>(() =>
            {
                accountService.Login(email, "anypassword");
            });
        }
    }
}