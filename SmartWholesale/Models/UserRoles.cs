namespace SmartWholesale.Models
{
    // Inheritance: SuperAdmin inherits from UserBase
    public class SuperAdmin : UserBase
    {
        public override string Role => "SuperAdmin";
        public decimal Salary { get; set; }

        public override string GetDashboardInfo()
        {
            return base.GetDashboardInfo() + " - Full System Access.";
        }
    }

    // Inheritance: Admin (Shop Owner) inherits from UserBase
    public class Admin : UserBase
    {
        public override string Role => "Admin";
        public decimal Salary { get; set; }

        public override string GetDashboardInfo()
        {
            return base.GetDashboardInfo() + " - Shop Management Access.";
        }
    }

    // Inheritance: Manager inherits from UserBase
    public class Manager : UserBase
    {
        public override string Role => "Manager";
        public decimal Salary { get; set; }

        public override string GetDashboardInfo()
        {
            return base.GetDashboardInfo() + " - Inventory Management Access.";
        }
    }

    // Inheritance: Customer inherits from UserBase
    public class Customer : UserBase
    {
        public override string Role => "Customer";

        public override string GetDashboardInfo()
        {
            return base.GetDashboardInfo() + " - Shopping Access.";
        }
    }
}
