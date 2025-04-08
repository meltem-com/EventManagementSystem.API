# Event Management System API

This is a layered ASP.NET Core Web API project designed to manage events and user registrations. The project follows clean architecture principles and includes various advanced features such as authentication, authorization, data protection, middleware, and action filters.

---

## 📌 Technologies Used

- ASP.NET Core Web API
- Entity Framework Core (Code First)
- SQL Server (LocalDb)
- Swagger / OpenAPI
- JWT (JSON Web Tokens)
- ASP.NET Core Identity (custom logic)
- Data Protection
- Middleware & Action Filters

---

## 🧱 Architecture

The project follows a 3-layered architecture:

1. **EventManagementSystem.API** – Presentation layer  
2. **EventManagementSystem.Business** – Business logic & services  
3. **EventManagementSystem.Data** – Data access layer (Repositories, Context, Entities)

---

## ✅ Implemented Features

- [x] 3-Layered Architecture
- [x] Entity Framework Core Code-First
- [x] CRUD operations for Users, Events, and UserEvents
- [x] Many-to-many relationship between Users and Events
- [x] JWT-based Authentication & Role-based Authorization
- [x] Custom Middleware for request logging
- [x] Time-based Action Filter (e.g., access allowed only between 09:00–21:00)
- [x] Model Validation using Data Annotations
- [x] Global Exception Handling
- [x] Dependency Injection throughout the project
- [x] Swagger UI for API testing and documentation
- [ ] Data Protection for password encryption (encryption service implemented, integration in progress)

---

## 🚀 Getting Started

1. Clone the repository  
2. Configure `appsettings.json` with your connection string and JWT settings  
3. Run the following commands:

```bash
dotnet ef database update
dotnet run
