using System.Collections.Generic;

namespace Travel_Company.WPF.Models.Users;

public class Object
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public virtual ICollection<UserObject> UsersObjects { get; set; } = new List<UserObject>();
}