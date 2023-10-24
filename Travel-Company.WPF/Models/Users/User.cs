using System.Collections.Generic;

namespace Travel_Company.WPF.Models.Users;

public class User
{
    public int Id { get; set; }
    
    public string Username { get; set; } = null!;
    
    public string Password { get; set; } = null!;
    
    public virtual ICollection<UserObject> UsersObjects { get; set; } = new List<UserObject>();
}