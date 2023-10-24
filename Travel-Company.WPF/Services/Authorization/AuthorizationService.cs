using System.Linq;
using Travel_Company.WPF.Models;
using Travel_Company.WPF.Models.Users;

namespace Travel_Company.WPF.Services.Authorization;

public class AuthorizationService : IAuthorizationService
{
    private readonly TravelCompanyDbContext _context;

    public AuthorizationService(TravelCompanyDbContext context)
    {
        _context = context;
    }
    
    public User? LogIn(string username, string password)
    {
        if (_context.Users.FirstOrDefault(u => u.Username == username) is User user && user.Password == password)
        {
            return user;
        }

        return null!;
    }
}