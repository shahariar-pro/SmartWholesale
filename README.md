# Project Report: SmartWholesale Management System

## 1. Title
**SmartWholesale**  
A Wholesale Management System for enhanced inventory, billing, and customer management.

## 2. Introduction
SmartWholesale is a desktop-based wholesale management platform designed to streamline the tri-party business model between Admins, Managers, and Customers. Inspired by platforms such as Daraz, Chaldal, and Foodpanda, the system centralizes product management, billing, transaction tracking, and customer reviews into a single, cohesive application.

The platform is built using **C# Windows Forms**, **SQL Server**, and **ADO.NET**, and follows a strict three-layer architecture — **UI (Presentation Layer)**, **BLL (Business Logic Layer)**, and **DAL (Data Access Layer)** — to ensure clean separation of concerns, maintainability, and scalability. The system is designed to demonstrate all four core pillars of Object-Oriented Programming: **Encapsulation, Inheritance, Polymorphism, and Abstraction**.

## 3. Team Members & Supervision
**Supervised By:** Dr. Md. Iftekharul Mobin  
**Course:** CSC2210: OBJECT ORIENTED PROGRAMMING 2 (Spring 2025-26)  

| Name | ID |
| :--- | :--- |
| 1. DEWAN SHAHARIAR HOSSEN | 24-59069-3 |
| 2. SADIYA AFRIN OISHI | 24-59097-3 |
| 3. NOSHIN TABASSUM SHOSHE | 24-59098-3 |
| 4. MD. SIAM TASBIR | 24-59099-3 |

## 4. Case Study & User Roles
SmartWholesale supports distinct user roles, each with a defined set of responsibilities and access rights:

- **Admin (Business Owner)**: Oversees the entire platform. Can add or remove products from the inventory, track total earnings, monitor overall sales figures, and view customer reviews to assess product quality and customer satisfaction.
- **Manager**: Assists with day-to-day inventory operations. Can add or remove products, create discounts on specific items to boost sales, and view customer reviews. Acts as the bridge between administrative oversight and the storefront.
- **Customer**: Browses the catalog, adds products to the cart, completes purchases (generating bills and transactions), and leaves reviews for items bought.

## 5. Key Features
- **Role-Based Access Control**: Secure login with redirection to Admin, Manager, and Customer dashboards.
- **Inventory Management (CRUD)**: Create, Read, Update, and Delete items with stock tracking and minimum stock threshold alerts.
- **Billing & Discounts**: Automated total calculation with bulk wholesale discounts.
- **User Management**: Control over all user accounts and roles across all three layers.
- **Reviews & Ratings**: Feedback system for customers to evaluate products and shops.

## 6. Tools and Technologies
- **IDE**: Visual Studio 2026
- **Language**: C#
- **Database**: Microsoft SQL Server (ADO.NET)
- **Architecture**: 3-Tier Layered Architecture (UI, BLL, DAL)
- **Framework**: .NET 8.0

## 7. Normalization & Finalization
The database design follows strict normalization principles (up to 3NF) for core modules:
- **I. User Management**: UNF, 1NF, 2NF, and 3NF ensuring no transitive dependencies.
- **II. Inventory (Items)**: Properly normalized with Foreign Key (FK) constraints on `OwnerId`.
- **III. Billing & Transactions**: Normalized through Junction Tables for `BillItems` to handle many-to-many relationships.

### Final Tables:
1. `Users`: [UId (PK), UName, UPassword, UPhoneNo, Email, UAddress, Role, JoiningDate, Salary]
2. `Items`: [IId (PK), IType, IBrand, IModelNo, IPrice, IStockStatus, IMinimumStock, OwnerId (FK)]
3. `Bills`: [BillId (PK), BillDate, TotalAmount, CustomerId (FK)]
4. `BillItems`: [BillItemId (PK), BillId (FK), IId (FK), Quantity, UnitPrice]
5. `Transactions`: [TId (PK), BillId (FK), UId (FK), TotalAmount, TransactionDate]
6. `Reviews`: [ReviewId (PK), UId (FK), IId (FK), Rating, Comment, ReviewDate]

## 8. Project Workflow & Implementation

### A. User Interface (Windows Forms)
- **Login/Signup**: Entry points for all users with role-specific redirection.
- **Dashboards**: Specialized forms for each role.
- **Modules**: Dedicated forms for Product Management, Shopping Cart, and Sales Analytics.

### B. Database Operations (ADO.NET)
- **Create**: Securely insert new users, items, and transactions.
- **Read**: Fetch and display records in `DataGridView` using `SqlDataReader`.
- **Update**: Modify existing records and update stock levels dynamically.
- **Delete**: Remove records using primary keys.

### C. C# Code Implementation (Samples)

#### User Registration (Insert)
```csharp
public bool AddUser(UserBase user)
{
    using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
    {
        string query = "INSERT INTO Users (UName, UPassword, UPhoneNo, Email, UAddress, Role, Salary) VALUES (@Name, @Pass, @Phone, @Email, @Addr, @Role, @Salary)";
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@Name", user.UName);
        cmd.Parameters.AddWithValue("@Pass", user.UPassword);
        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@Role", user.Role);
        // ... (other parameters)
        conn.Open();
        return cmd.ExecuteNonQuery() > 0;
    }
}
```

#### Item Update
```csharp
public bool UpdateItem(Item item)
{
    using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
    {
        string query = "UPDATE Items SET IType=@Type, IBrand=@Brand, IModelNo=@Model, IPrice=@Price, IStockStatus=@Stock WHERE IId=@Id";
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@Id", item.IId);
        cmd.Parameters.AddWithValue("@Price", item.IPrice);
        cmd.Parameters.AddWithValue("@Stock", item.IStockStatus);
        // ... (other parameters)
        conn.Open();
        return cmd.ExecuteNonQuery() > 0;
    }
}
```

## 9. How to Run the Project
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/shahariar-pro/SmartWholesale.git
   ```
2. **Set Up the Database**: 
   - Open the `Database.sql` file in the `SmartWholesale/Database` folder.
   - Execute the script in SQL Server Management Studio (SSMS) to create the schema.
   - Update the connection string in `DAL/DatabaseConnection.cs`:
     ```csharp
     private string _connectionString = "Server=YOUR_SERVER;Database=SmartWholesaleDB;Trusted_Connection=True;";
     ```
3. **Build & Run**: Open `SmartWholesale.sln` in **Visual Studio 2026** and run the application.

## 10. Conclusion
SmartWholesale successfully demonstrates a production-grade, desktop-based wholesale management system built with C# Windows Forms, SQL Server, and ADO.NET. By adopting a tri-party business model encompassing Admins, Managers, and Customers, the platform mirrors the operational complexity of real-world wholesale and retail platforms.

The system's adherence to a strict three-layer architecture ensures a clean separation of concerns, making the codebase maintainable, scalable, and testable. The implementation of all four OOP pillars — **Encapsulation, Inheritance, Polymorphism, and Abstraction** — further demonstrates sound software engineering practice.
