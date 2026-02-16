# Employee Management REST API
- Dotnet version: 10.0.101 
- EF required packages version: 10.

## Objective
Build a clean CRUD-based **ASP.NET Core Web API** using **Entity Framework Core**.

##Recent Changes:
modified code by making employee's relationship with project
Added Project Table (id,name,description, start date, end date,duration.
implemented jwt authentication.
Integrated backend with frontend.
I still need to add more features.

## Functional Requirements

### Create an ASP.NET Core Web API

### Employee Entity Design
The `Employee` entity should include the following fields:

- `EmployeeId`
- `Name`
- `Email`
- `Department`
- `DateOfJoining`

---

## API Features (CRUD Operations)

- **Add Employee**
- **Update Employee**
- **Delete Employee**
- **Get All Employees**
- **Get Employee by ID**

---

## Technical Expectations

- Entity Framework Core with **SQL Server**
- **Code-First** approach using migrations
- **DTOs** with validation
- Proper **HTTP status codes**
- **Asynchronous** methods throughout the application
- **Dependency Injection** for services
- **Swagger** enabled for API documentation
- API testing using **Postman**

---
## Tools & Technologies

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- Postman
-----
# Summary:
#Performed CRUD operations
#Code First Approach implemented using Migration.
#Understanding and use of Entity Framwork core.
#Connection to database using dbcontext.
#Domain model and DTO mapping.
