# Order Management System (.NET 8)

## Overview

This project is a monolithic Order Management API built using **.NET 8**, **Entity Framework Core**, and **JWT-based authentication**. It provides features for managing customers, products, orders, and order status tracking, along with discount calculations based on customer segments.

## Architecture

The project follows a **Clean Architecture** pattern with the following layers:
- **API Layer** (`OrderManagement.Api`): Contains controllers and startup logic.
- **Application Layer** (`OrderManagement.Application`): Contains business logic, commands, and queries (MediatR).
- **Domain Layer** (`OrderManagement.Domain`): Contains Domain Entities
- **Infrastructure Layer** (`OrderManagement.Infrastructure`): Handles database access, service implementations, and shared utilities.
- **Test Layer** (`OrderManagement.Test`): Contains unit and integration tests.

## Key Features

- JWT authentication and authorization
- Discount logic based on customer segment
- Order status tracking with historical records
- Configurable seed data (toggle via `appsettings.json`)
- Swagger UI with JWT support
- xUnit-based unit tests

## Approach

1. **Authentication**: Secured endpoints using JWT with `Bearer` tokens.
2. **Separation of Concerns**: Business logic is abstracted using MediatR commands and services.
3. **Seeding**: Seed data logic is placed in a `DataSeeder` service and conditionally executed at startup based on `SeedData:Enabled` flag.
4. **Tests**:
   - **Unit tests** for business logic like discount calculation.
   - **Integration tests** using `WebApplicationFactory` to spin up the full app in-memory.
5. **Performance Optimization**: Used `AsNoTracking()` in read-only queries to reduce EF Core tracking overhead.

## Assumptions

- All orders are created for existing customers.
- Each order contains at least one item.
- Discounts are applied based only on customer segment and total spent.
- The seed data is primarily for development/testing and is not required in production.
- Authentication token must be passed as `Authorization: Bearer {token}` in Swagger or API calls.

## Running the App

1. Create a database (OrderDb) in a local MSSQL server
2. Run EF Core migrations:
   PM> Add-migration 'comment'
   PM> Update-database
3. Change SeedData "Enabled": true if you prefer to use seeddata for testing purposes.
4. Run the app dotnet run --project OrderManagement.Api
5. Open Swagger: https://localhost:{port}/swagger
