namespace Travel_Company.WPF.Models.Users;

public class UserObject
{
    public int UserId { get; set; }
    
    public int ObjectId { get; set; }
    
    public bool CanCreate { get; set; }
    
    public bool CanRead { get; set; }
    
    public bool CanUpdate { get; set; }
    
    public bool CanDelete { get; set; }
    
    public virtual User User { get; set; } = null!;

    public virtual Object Object { get; set; } = null!;
}