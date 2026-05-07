namespace SmartWholesale.Models
{
    /// <summary>
    /// Interface for User operations (Demonstrates Abstraction)
    /// </summary>
    public interface IUser
    {
        int UId { get; set; }
        string UName { get; set; }
        string Role { get; }
        string GetDashboardInfo();
    }
}
