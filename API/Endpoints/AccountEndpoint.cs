using System;
using API.Common;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class AccountEndpoint
{
  public static RouteGroupBuilder MapAccountEndpoint(this WebApplication app)
  {
    var group = app.MapGroup("/api/account").WithTags("account");
    group.MapPost("/register", async (HttpContext context, UserManager<AppUser>
    UserManager, [FromForm] string fullName, [FromForm] string email, [FromForm] string password) =>
    {
      var userFromDb = await UserManager.FindByEmailAsync(email);
      if (userFromDb is not null)
      {
        return Results.BadRequest(Response<string>.Failure("User is already exist."));
      }
      var user = new AppUser
      {
        Email = email,
        FullName = fullName,
      };

      var result = await UserManager.CreateAsync(user, password);
      if (!result.Succeeded)
      {
        return Results.BadRequest(Response<string>.Failure(result.Errors.Select(x => x.Description).FirstOrDefault()!));
      }
      return Results.Ok(Response<string>.Success("", "User created sucessfully."));
    });
    return group;
  }

}
