using NSubstitute;
using SohaLogin.Accounts;
using SohaLogin.Database;
using System;
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
            var expectedMessage = "E-mail e senha são obrigatórios para realizar o login.";
            var sohaLoginDatabase = Substitute.For<ISohaLoginDatabase>();
            var accountService = new AccountService(sohaLoginDatabase);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                accountService.Login(email, password);
            });
            Assert.Equal(expectedMessage, exception.Message);
        }

        //CA04 - O usuário deverá ser um e-mail e deverá haver uma validação para caso não seja um e-mail válido
        [Theory(DisplayName = "E-mail deve ter um formato válido.")]
        [InlineData("email1")]
        [InlineData("email1@")]
        public void Login_ShouldThrowsException_WhenEmailIsInvalid(string email)
        {
            var expectedMessage = "E-mail deve ter um formato válido. ex.: 'username@domain.com'";
            var sohaLoginDatabase = Substitute.For<ISohaLoginDatabase>();
            var accountService = new AccountService(sohaLoginDatabase);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                accountService.Login(email, "anypassword");
            });
            Assert.Equal(expectedMessage, exception.Message);
        }

        //CA05 - A senha deverá ter no mínimo 4 caracteres e no máximo 15
        [Theory(DisplayName = "A senha deverá ter no mínimo 4 caracteres e no máximo 15")]
        [InlineData("pas")]
        [InlineData("ThisPasswordHasMoreThan15chars")]
        public void Login_ShouldThrowsException_WhenPasswordIsInvalid(string password)
        {
            var expectedMessage = "A senha deverá ter no mínimo 4 caracteres e no máximo 15";
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