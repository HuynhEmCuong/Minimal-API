using Microsoft.EntityFrameworkCore;
using endpoints;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// builder.Services.AddDbContext<MeetingDb>(m => m.UseSqlServer(""));
builder.Services.AddDbContext<MeetingDb>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "TodoAPI";
    config.Title = "TodoAPI v1";
    config.Version = "v1";

});

//Config Author with Polici  
builder.Services
    .AddAuthentication()
    .AddBearerToken();
builder.Services.AddAuthorization();

builder.Services.AddAuthorizationBuilder()
  .AddPolicy("admin_greetings", policy =>
        policy
            .RequireRole("admin")
            .RequireClaim("scope", "greetings_api"));

var app = builder.Build();


//Config handle error 
app.UseExceptionHandler(exceptionHandlerApp
    => exceptionHandlerApp.Run(async context
        => await Results.Problem()
                     .ExecuteAsync(context)));

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error-development");
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "TodoAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}
app.MapGet("/error", () =>
{
    throw new InvalidOperationException("Oops, the '/' route has thrown an exception.");
});


app.MapGet("/login", (string username) =>
    {
        var claimsPrincipal = new ClaimsPrincipal(
          new ClaimsIdentity(
         new[] {
         new Claim(ClaimTypes.Name, username) ,
         new Claim(ClaimTypes.Role, "Admin")
         },
            BearerTokenDefaults.AuthenticationScheme
          )
        );

        return Results.SignIn(claimsPrincipal);
    });

//API with  Authoriztion Policies
app.MapGet("/hello", () => "Hello world!")
.RequireAuthorization("admin_greetings");

//Define EndPoint
TodoEndPoints.Map(app.MapGroup("/todoitems"));
MeetingEndPoints.Map(app.MapGroup("/meetings"));


app.Run();

