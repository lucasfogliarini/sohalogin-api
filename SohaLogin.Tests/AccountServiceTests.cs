using NSubstitute;
using SohaLogin.Accounts;
using SohaLogin.Database;
using System;
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
            var expectedMessage = "E-mail e senha s�o obrigat�rios para realizar o login.";
            var sohaLoginDatabase = Substitute.For<ISohaLoginDatabase>();
            var accountService = new AccountService(sohaLoginDatabase);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                accountService.Login(email, password);
            });
            Assert.Equal(expectedMessage, exception.Message);
        }

        //CA04 - O usu�rio dever� ser um e-mail e dever� haver uma valida��o para caso n�o seja um e-mail v�lido
        [Theory(DisplayName = "E-mail deve ter um formato v�lido.")]
        [InlineData("email1")]
        [InlineData("email1@")]
        public void Login_ShouldThrowsException_WhenEmailIsInvalid(string email)
        {
            var expectedMessage = "E-mail deve ter um formato v�lido. ex.: 'username@domain.com'";
            var sohaLoginDatabase = Substitute.For<ISohaLoginDatabase>();
            var accountService = new AccountService(sohaLoginDatabase);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                accountService.Login(email, "anypassword");
            });
            Assert.Equal(expectedMessage, exception.Message);
        }

        //CA05 - A senha dever� ter no m�nimo 4 caracteres e no m�ximo 15
        [Theory(DisplayName = "A senha dever� ter no m�nimo 4 caracteres e no m�ximo 15")]
        [InlineData("pas")]
        [InlineData("ThisPasswordHasMoreThan15chars")]
        public void Login_ShouldThrowsException_WhenPasswordIsInvalid(string password)
        {
            var expectedMessage = "A senha dever� ter no m�nimo 4 caracteres e no m�ximo 15";
            var sohaLoginDatabase = Substitute.For<ISohaLoginDatabase>();
            var accountService = new AccountService(sohaLoginDatabase);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                accountService.Login("anyemail@domain.com", password);
            });
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}