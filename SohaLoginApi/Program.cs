using Hellang.Middleware.ProblemDetails;
using SohaLogin.Api;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;

var builder = WebApplication.CreateBuilder(args);

var app = AddServices(builder).Build();

Run(app);

static WebApplicationBuilder AddServices(WebApplicationBuilder builder)
{
    // Add services to the container.

    builder.Services.AddServices();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    Jwt.AddJwtAuthentication(builder.Services, builder.Configuration);

    builder.Services.AddProblemDetails(x =>
    {
        x.MapToStatusCode<ValidationException>(StatusCodes.Status400BadRequest);
        x.MapToStatusCode<AuthenticationException>(StatusCodes.Status401Unauthorized);
        x.IncludeExceptionDetails = (ctx, ex) =>
        {
            return true;
        };
    });

    return builder;
}

static void Run(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}