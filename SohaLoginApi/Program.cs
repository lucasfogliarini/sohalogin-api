using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using SohaLogin.Api;
using SohaLogin.Database;
using SohaLogin.Database.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;

var builder = WebApplication.CreateBuilder(args);

var app = AddServices(builder).Build();

Seed(app);

Run(app);

static WebApplicationBuilder AddServices(WebApplicationBuilder builder)
{
    // Add services to the container.

    builder.Services.AddServices();
    builder.Services.AddDatabase();

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

    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin());

    app.UseHttpsRedirection();

    app.UseProblemDetails();

    app.UseAuthentication();//must be before UseAuthorization
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

static async void Seed(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var sohaLoginDbContext = scope.ServiceProvider.GetService<SohaLoginDbContext>();
    var account = new Account
    {
        Name = "Lucas Fogliarini",
        Email = "lucasfogliarini@gmail.com",
        Password = "pass1",
        CreatedAt = DateTime.Now
    };

    sohaLoginDbContext.Add(account);
    await sohaLoginDbContext.SaveChangesAsync();
}