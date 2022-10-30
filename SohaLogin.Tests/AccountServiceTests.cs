using NSubstitute;
using SohaLogin.Accounts;
using SohaLogin.Database;
using System.ComponentModel.DataAnnotations;

namespace SohaLogin.Tests
{
    public class AccountServiceTests
    {
        //CA03 - Uma mensagem dever� informar que o usu�rio � obrigat�rio, bem como que a senha � obrigat�ria.
        [Theory(DisplayName = "Email e Senha devem ser obrigat�rios")]
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

        //CA04 - O usu�rio dever� ser um e-mail e dever� haver uma valida��o para caso n�o seja um e-mail v�lido
        [Theory(DisplayName = "E-mail deve ter um formato v�lido.")]
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