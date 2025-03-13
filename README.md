DVS (Dienstkleidungs-Verwaltungs-Software/Uniform management software)


📢 Foreword

DVS is my first self-developed "professional" application, and it is still in the development phase.
Since this is a learning project for me, there are certainly areas that could be improved or optimized.
I welcome any feedback, tips, or suggestions—whether it’s about best practices, performance improvements,
or new feature ideas. Your input will help me refine and enhance the application over time.

Thanks for checking out DVS, and I appreciate your support! 🚀


✨ Project Description

DVS is an application for the efficient management of uniforms and their assignment to employees.
DVS is a comprehensive application designed for the efficient management of uniforms,
including their sizes, quantities, and assignment to employees. It streamlines the organization
of workwear by allowing classification into categories and seasons.
With an intuitive user interface and dynamic list updates, DVS simplifies the process of tracking
and distributing uniforms, ensuring that employees always have the right clothing available.


🔍 Features

 - Create and manage Uniforms, including sizes and quantities
 - Create and manage employees and assign uniforms
 - Support for drag & drop to easily assign uniforms
 - Toggle between employee/uniforms view and a detailed size overview
 - Dynamic updates of list views


🌐 Future Development

 - Advanced filtering and search functions
 - Print function for employees and assigned clothing
 - Implement clothes-images
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
