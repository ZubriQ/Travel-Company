using System.Linq;
using Travel_Company.WPF.Models;

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
        return (_context.Users.FirstOrDefault(u => u.Username == username) is User user && user.Password == password)
            ? user
            : null;
    }
}