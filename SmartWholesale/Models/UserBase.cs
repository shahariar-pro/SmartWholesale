using System;

namespace SmartWholesale.Models
{
    /// <summary>
    /// Base class for all users (Demonstrates Abstraction and Encapsulation)
    /// </summary>
    public abstract class UserBase : IUser
    {
        // Encapsulation: Private fields with Public properties
        private int _uid;
        private string _uname;
        private string _upassword;
        private string _uphoneNo;
        private string _email;
        private string _uaddress;
        private DateTime _joiningDate;

        public int UId { get => _uid; set => _uid = value; }
        public string UName { get => _uname; set => _uname = value; }
        public string UPassword { get => _upassword; set => _upassword = value; }
        public string UPhoneNo { get => _uphoneNo; set => _uphoneNo = value; }
        public string Email { get => _email; set => _email = value; }
        public string UAddress { get => _uaddress; set => _uaddress = value; }
        public DateTime JoiningDate { get => _joiningDate; set => _joiningDate = value; }

        // Abstract property to be implemented by subclasses
        public abstract string Role { get; }

        // Polymorphism: Virtual method that can be overridden
        public virtual string GetDashboardInfo()
        {
            return $"Welcome, {UName}! Role: {Role}";
        }

        public override string ToString()
        {
            return $"{UName} ({Role})";
        }
    }
}
