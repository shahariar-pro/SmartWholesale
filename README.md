# SmartWholesale Management System

## Project Overview
**SmartWholesale** is a comprehensive Wholesale Management System developed for the **CSC2210 Object Oriented Programming 2** course at **AIUB** (Spring 2025-2026). The project is supervised by **Kazi Sadia**.

The system is designed as a 3-tier desktop application using **C# Windows Forms** and **SQL Server**. It models a wholesale platform that connects Business Owners (2nd party) with Customers (1st party), with the system acting as the IT platform provider (3rd party).

## Team Members
1. **Fahim Al Shihab** - 22-46945-1
2. **Fidbi Hasan** - 22-46190-1
3. **Nahiyan Aziz** - 21-45811-3
4. **Solaiman Ali Prince** - 20-42089-1

## Key Features
*   **Role-Based Access Control**: Four distinct roles with specialized dashboards and permissions.
*   **Inventory Management**: Full CRUD operations for products, including stock tracking and low-stock alerts.
*   **Transaction & Billing**: Automated billing system with wholesale discounts (5%, 10%, 15%) based on quantity.
*   **Review & Rating System**: Customers can rate and review products/shops, which Super Admins use for evaluation.
*   **Sales Analytics**: Dashboards for Admins/Shop Owners to track earnings and sales history.
*   **Advanced Search**: Robust filtering and search capabilities for products across all user roles.

## User Roles
1.  **Super Admin**: Full system control. Manages all users and can remove shop owners based on performance/reviews.
2.  **Admin (Business Owner)**: Manages their own product catalog, views earnings, and monitors sales dashboards.
3.  **Manager**: Responsible for inventory updates, stock management, and viewing transaction logs.
4.  **Customer**: Browses products, manages a shopping cart, completes purchases, and provides feedback via reviews.

## Technical Stack
*   **Language**: C#
*   **Framework**: .NET 8.0 (Windows Forms)
*   **Database**: Microsoft SQL Server (ADO.NET)
*   **Architecture**: 3-Tier Layered Architecture (UI, BLL, DAL)
*   **OOP Principles**: Demonstrates Encapsulation, Inheritance, Polymorphism, and Abstraction.

## Database Setup
1.  Locate the `Database.sql` file in the `SmartWholesale/Database` folder.
2.  Execute the script in SQL Server Management Studio (SSMS) to create the `SmartWholesaleDB` schema.
3.  Update the connection string in `SmartWholesale/DAL/DatabaseConnection.cs` to match your local server instance.

## How to Run
1.  Clone the repository.
2.  Open `SmartWholesale.sln` in **Visual Studio 2022**.
3.  Ensure the database is set up and the connection string is updated.
4.  Build and Run the project.

## Project Structure
- `UI/`: Windows Forms for various modules and dashboards.
- `BLL/`: Business Logic Layer handling calculations and rules.
- `DAL/`: Data Access Layer for database interaction using ADO.NET.
- `Models/`: Entity classes and interfaces representing the domain model.
- `Database/`: SQL scripts for database initialization.
