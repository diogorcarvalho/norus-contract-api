using NorusContract.Date;
using NorusContract.Domain.Entities;
using System.Linq;

namespace NorusContract.Data.Repository
{
  public interface IUserRepository
  {
    User Get(string userName, string hashPassword);
  }

  public class UserRepository : IUserRepository
  {
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
      _context = context;
    }

    public User Get(string userName, string hashPassword)
    {
      return (from user in _context.Users
        where user.UserName.Equals(userName) && user.HashPassword.Equals(hashPassword)
        select user).FirstOrDefault();
    }
  }
}