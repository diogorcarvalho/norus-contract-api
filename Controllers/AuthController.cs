using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorusContract.Api.Models;
using NorusContract.Data.Repository;
using NorusContract.Services;

namespace NorusContract.Api.Controller
{
  [ApiController]
  public class AuthController : ControllerBase
  {
    protected readonly ICryptographyService _cryptographyService;
    protected readonly IUserRepository _userRepository;
    protected readonly ITokenService _tokenService;

    public AuthController(
      [FromServices] ICryptographyService cryptographyService,
      [FromServices] ITokenService tokenService,
      [FromServices] IUserRepository userRepository
    ) {
      _cryptographyService = cryptographyService;
      _tokenService = tokenService;
      _userRepository = userRepository;
    }

    [HttpPost, AllowAnonymous, Route("v1/userCredential")]
    public ActionResult<LoggedUserCredential> GetLoggedUserCredential([FromForm] string userName, [FromForm] string password)
    {
      var loggedUserCredential = new LoggedUserCredential();

      var hashPassword = _cryptographyService.GenerateHash(password);
      
      var user = _userRepository.Get(userName, hashPassword);

      if (user == null) return StatusCode(401, new { message = "Login ou/e senha é inválido" });

      try
      {
        var token = _tokenService.GenerateToken(user);

        user.HashPassword = "";
        
        loggedUserCredential.User = user;
        loggedUserCredential.Token = token;
      }
      catch (HttpRequestException exception)
      {
        return StatusCode(401, new { message = exception.Message });
      }

      return Ok(loggedUserCredential);
    }
  }
}