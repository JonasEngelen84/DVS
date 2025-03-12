DVS (Service Clothing Management Software)


✨ Project Description

DVS is an application for managing service clothing, including sizes, quantities, and their assignment to employees.
The application enables efficient management of service clothing, including categorization by season and other attributes.



🔍 Features

 - Create and manage Uniforms, including sizes and quantities
 - Create and manage employees and assign service clothing
 - Support for drag & drop to easily assign clothing
 - Toggle between employee/clothing view and a detailed size overview
 - Dynamic updates of list views



🌐 Future Development

DVS is still under development.
Planned features:
 - Advanced filtering and search functions
 - Print function for employees and assigned clothing
 - User roles and permissions
 - Support for multiple locations



🔧 Technologies

 - .NET Core / WPF - Frontend application
 - Entity Framework Core - ORM for database access
 - SQL Server / SQLite - Database technology
 - MVVM Pattern - Architectural approach



📂 Project Structure

DVS/
 - DVS.WPF/              # WPF frontend of the application
 - DVS.Domain/           # Business logic & models
 - DVS.EntityFramework/  # Database access using EF Core
 - DVS.sln               # Visual Studio solution file
 - README.md             # This documentation



📁 Database Structure

The database consists of the following main tables:
 - Category
 - Season
 - Clothes
 - ClothesSize
 - Employee
 - EmployeeClothesSize (linking table for employees and clothing)



🔮 Best Practices & Architecture

The project follows the MVVM pattern (Model-View-ViewModel) to ensure a clean separation between UI and logic.



🔍 Dependencies

 - DVS.WPF depends on DVS.Domain & DVS.EntityFramework
 - DVS.EntityFramework depends on DVS.Domain
 - DVS.Domain is independent and contains the core logic



⚖ License

This project is licensed under the MIT License – See the LICENSE file for details.



🔼 Installation & Setup

Requirements:
 - .NET 8.0 or later
 - Entity Framework Core
 -  SQL Server or SQLite as the database

Creating & Migrating the Database
Run the following commands in PowerShell:

# Create migrations (if none exist)
dotnet ef migrations add InitialCreate --project DVS.EntityFramework

# Apply/create the database
dotnet ef database update --project DVS.EntityFramework

# Move the .db file from DVS/DVS.EntityFramework to DVS/DVS.WPF/bin/Debug/net8.0-windows. 
# If a .db file already exists there, replace it!



If you have any questions or issues, feel free to create an issue or contact me!
Happy Coding! 🚀
