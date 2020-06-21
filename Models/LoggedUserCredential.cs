using NorusContract.Domain.Entities;

namespace NorusContract.Api.Models
{
  public class LoggedUserCredential
  {
    public User User { get; set; }
    public string Token { get; set; }
  }
}