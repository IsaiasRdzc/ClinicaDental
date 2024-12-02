# Dental Clinic System Project

## General

aqui va la descripsion de nuestro proyecto.

## Prerequisites

- **.NET 8.0 SDK**: [Download .NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

- **.NET Aspire Workload**: Install and update as follows:

  ```bash
  dotnet workload update
  dotnet workload install aspire
  ```

- **Node.js LTS**: [Download Node.js](https://nodejs.org/)

- **Angular CLI**: Install globally using npm:

  ```bash
  npm install -g @angular/cli
  ```

- **OCI-Compliant Container Runtime**: Install one of the following, for example:
  - [Docker Desktop](https://www.docker.com/products/docker-desktop)

## Projects

- [AppHost](src/ClinicaDental/ClinicaDental.AppHost): Core service that manages the lifecycle of application services and components, acting as the main entry point.

- [ServiceDefaults](src/ClinicaDental/ClinicaDental.ServiceDefault): A shared library containing common configuration settings used across the application.

- [WebApp](src\ClinicaDental\ClinicaDental.WebApp): The user-facing web application providing the graphical interface for end-users.

- [ApiService](src\ClinicaDental\ClinicaDental.ApiService): The backend service housing the core business logic and API endpoints for communication between client and server.

  - [Database](src\ClinicaDental\ClinicaDental.ApiService\DataBase): Responsible for database schema definition, connectivity, and entity mapping.

- [Tests](src\ClinicaDental\ClinicaDental.Tests): Contains unit, integration, and functional tests to verify system functionality.