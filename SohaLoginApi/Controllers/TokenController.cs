using Divagando.Accounts;
using Divagando.Database;
using Divagando.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Divagando.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly Jwt _jwt;

        public TokenController(IAccountService accountService, IOptions<Jwt> jwt)
        {
            _divagandoDatabase = divagandoDatabase;
            _accountService = accountService;
            _jwt = jwt.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Token(AuthenticationInput authenticationInput)
        {
            await _accountService.CreateOrUpdateAsync(authenticationInput);
            var authentication = await CreateAuthenticationAsync(authenticationInput);
            return Ok(authentication);
        }

        private async Task<Authentication> CreateAuthenticationAsync(AuthenticationInput authenticationInput)
        {
            var tokenDescriptor = _jwt.CreateTokenDescriptor(authenticationInput.Email, authenticationInput.Name);

            var authentication = new Authentication
            {
                Email = authenticationInput.Email,
                JwToken = _jwt.GenerateToken(tokenDescriptor),
                CreatedAt = DateTime.Now,
                ExpiresAt = tokenDescriptor.Expires.GetValueOrDefault(),
            };
            _divagandoDatabase.Add(authentication);
            await _divagandoDatabase.CommitAsync();
            return authentication;
        }
    }
}
