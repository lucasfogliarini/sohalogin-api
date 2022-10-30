# Executando a aplicação

1. Instale [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/downloads/) com o .NET 6.0 runtime.
2. Configure o projeto SohaLoginApi como Startup
   - Clique com o botão direito encima do projeto, depois clique em "Set as Startup Project"
3. Execute em modo debug (F5) usando o profile "SohaLoginApi"
    - A aplicação irá executar no endereço `https://localhost:7040/`

# Executando os testes

1. Abra a View "Test Explorer"
   - Menu Principal > View > Test Explorer
2. Execute todos os testes
   - Clique com o botão direito em "SohaLogin.Tests"
   - Clique em "Run"

# Tecnologias

- [Microsoft.AspNetCore.Authentication.JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer)
   - Middleware ASP.NET Core que permite que um aplicativo receba um token de portador OpenID Connect.
- [Microsoft.EntityFrameworkCore.InMemory/](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.InMemory/)
    - Provedor de banco de dados permite que o Entity Framework Core seja usado com um banco de dados em memória.
- [ProblemDetails Middleware](https://www.nuget.org/packages/Hellang.Middleware.ProblemDetails/)
   - ProblemDetails é um middleware ASP.NET Core que adota o padrão [rfc7807](https://www.rfc-editor.org/rfc/rfc7807) e que pode ser usado para gerar resultados detalhados para as exceções que ocorrem em sua aplicação. Ele lida com exceções em seu pipeline de middleware e as converte em ProblemDetails.

- [xUnit](https://xunit.net/)
  - xUnit.net é uma ferramenta de testes unitários.
- [NSubstitute](https://nsubstitute.github.io/)
  - Usado para mockar os dados para os testes unitários