using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SohaLogin.Accounts;

namespace SohaLogin.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly Jwt _jwt;

        public TokenController(IAccountService accountService, IOptions<Jwt> jwt)
        {
            _accountService = accountService;
            _jwt = jwt.Value;
        }

        [HttpPost]
        public IActionResult Token(AccountInput accountInput)
        {
            var accountOut = _accountService.Login(accountInput.Email, accountInput.Password);
            var tokenDescriptor = _jwt.CreateTokenDescriptor(accountOut.Email, accountOut.Name);

            var authentication = new 
            {
                Email = accountOut.Email,
                JwToken = _jwt.GenerateToken(tokenDescriptor),
                ExpiresAt = tokenDescriptor.Expires.GetValueOrDefault(),
            };

            return Ok(authentication);
        }
    }
}
