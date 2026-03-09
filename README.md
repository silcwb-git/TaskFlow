# TaskFlow 🚀

Full-stack task management application built with **Angular 18+** and **.NET 8**.

## 📋 Features

- ✅ User Authentication with JWT
- ✅ Task Management (CRUD)
- ✅ Priority Levels (Low, Medium, High, Critical)
- ✅ Due Dates
- ✅ Responsive Design
- ✅ Unit Tests
- ✅ Clean Architecture

## 🛠️ Tech Stack

### Backend
- **.NET 8**
- **Entity Framework Core**
- **SQL Server**
- **JWT Authentication**
- **Clean Architecture**
- **xUnit & Moq** (Testing)


### Frontend
- **Angular 18+**
- **TypeScript**
- **Standalone Components**
- **Jasmine/Karma Tests**
- **Responsive CSS**
- **JWT Interceptors**

## 📦 Prerequisites

- Node.js 20+
- .NET 8 SDK
- Docker & Docker Compose
- Git

## 🚀 Getting Started

### Backend Setup
```bash
cd src/TaskFlow.Backend

# Start SQL Server
docker compose up -d
sleep 30

# Restore dependencies
dotnet restore

# Build
dotnet build

# Create and apply migrations
dotnet ef migrations add InitialCreate --project TaskFlow.Infrastructure --startup-project TaskFlow.API
dotnet ef database update --project TaskFlow.Infrastructure --startup-project TaskFlow.API

# Run
dotnet run --project TaskFlow.API/TaskFlow.API.csproj

### Backend Setup
```bash
cd src/TaskFlow.Frontend
```

## Install dependencies
npm install

## Run development server
ng serve

Frontend will be available at: http://localhost:4200

## 🧪 Testing
## Backend Tests
```bash
cd src/TaskFlow.Backend
dotnet test
```

## Frontend Tests
```bash
cd src/TaskFlow.Frontend
ng test --watch=falsecd src/TaskFlow.Frontend
ng test --watch=false
```

## 📚 API Documentation

Access Swagger UI at: https://localhost:5001/swaggerAuthentication 

## Endpoints
POST /api/auth/register - Register new user
POST /api/auth/login - Login user

## Task Endpoints
GET /api/tasks - Get all tasks
GET /api/tasks/{id} - Get task by ID
POST /api/tasks - Create new task
PUT /api/tasks/{id} - Update task
DELETE /api/tasks/{id} - Delete task

## 📝 Example Usage

## Register
```bash
POST /api/auth/register
{
  "email": "user@example.com",
  "password": "Password123!"
}
```

## Create Task
```bash
POST /api/tasks
Authorization: Bearer {token}
{
  "title": "My Task",
  "description": "Task description",
  "priority": "High",
  "dueDate": "2026-03-15T10:00:00"
}
```

## 📂 Project Structure

### Backend (.NET 8)

| Layer | Component | Files |
|-------|-----------|-------|
| **Domain** | Entities | `User.cs`, `Task.cs` |
| **Domain** | Enums | `TaskStatus.cs`, `TaskPriority.cs` |
| **Application** | DTOs | `LoginDto.cs`, `RegisterDto.cs`, `TaskDto.cs` |
| **Application** | Services | Service implementations |
| **Infrastructure** | Data | `TaskFlowDbContext.cs` |
| **Infrastructure** | Repositories | `GenericRepository.cs` |
| **Infrastructure** | Security | `JwtTokenService.cs` |
| **API** | Controllers | `AuthController.cs`, `TasksController.cs` |
| **API** | Configuration | `Program.cs`, `appsettings.json` |
| **Tests** | Auth Tests | `AuthServiceTests.cs` (7 tests ✅) |
| **Tests** | Auth Tests | `UserEntityTests.cs` (5 tests ✅) |
| **Tests** | Task Tests | `TaskEntityTests.cs` (5 tests ✅) |
| **Tests** | Repository Tests | `GenericRepositoryTests.cs` (2 tests ✅) |
| **DevOps** | Docker | `docker-compose.yml` |
| **DevOps** | Solution | `TaskFlow.sln` |

### Frontend (Angular 18+)

| Layer | Component | Files |
|-------|-----------|-------|
| **Core** | Guards | `auth.guard.ts` |
| **Core** | Interceptors | `jwt.interceptor.ts` |
| **Core** | Services | Service implementations |
| **Features** | Auth Components | `login.component.ts`, `register.component.ts` |
| **Features** | Auth Services | `auth.service.ts` |
| **Features** | Auth Tests | `auth.service.spec.ts` (6 tests ✅) |
| **Features** | Task Components | `task-list.component.ts` |
| **Features** | Task Services | `task.service.ts` |
| **Features** | Task Tests | `task.service.spec.ts` (6 tests ✅) |
| **Shared** | Components | Common shared components |
| **Root** | App Component | `app.component.ts` |
| **Root** | App Tests | `app.component.spec.ts` (3 tests ✅) |
| **Root** | Configuration | `app.config.ts`, `app.routes.ts` |
| **Root** | Entry Point | `main.ts`, `index.html`, `styles.css` |
| **Config** | Angular Config | `angular.json`, `tsconfig.json` |
| **Config** | Package Config | `package.json` |
| **Config** | Test Config | `karma.conf.js` |

### Project Root

| Item | Description |
|------|-------------|
| `.gitignore` | Git ignore rules |
| `README.md` | Project documentation |
| `.git/` | Git repository |

---

## 🧪 Test Details

### Backend (xUnit) - 19 Tests ✅

| Test File | Tests | Status |
|-----------|-------|--------|
| `AuthServiceTests.cs` | 7 | ✅ Passing |
| `UserEntityTests.cs` | 5 | ✅ Passing |
| `TaskEntityTests.cs` | 5 | ✅ Passing |
| `GenericRepositoryTests.cs` | 2 | ✅ Passing |
| **Total Backend** | **19** | **✅ Passing** |

### Frontend (Jasmine) - 15 Tests ✅

| Test File | Tests | Status |
|-----------|-------|--------|
| `auth.service.spec.ts` | 6 | ✅ Passing |
| `task.service.spec.ts` | 6 | ✅ Passing |
| `app.component.spec.ts` | 3 | ✅ Passing |
| **Total Frontend** | **15** | **✅ Passing** |

### Overall Test Summary

| Layer | Framework | Total Tests | Status |
|-------|-----------|-------------|--------|
| Backend | xUnit | 19 | ✅ Passing |
| Frontend | Jasmine | 15 | ✅ Passing |
| **Grand Total** | - | **34** | **✅ Passing** |

---

## 🏗️ Architecture Overview

### Backend (.NET 8)

| Layer | Component | Status |
|-------|-----------|--------|
| Domain | Entities, Enums | ✅ Implemented |
| Application | DTOs, Services | ✅ Implemented |
| Infrastructure | Data, Repositories, Security | ✅ Implemented |
| API | Controllers, Program.cs | ✅ Implemented |
| Testing | xUnit (19 tests) | ✅ Passing |
| DevOps | Docker (SQL Server) | ✅ Configured |

### Frontend (Angular 18+)

| Layer | Component | Status |
|-------|-----------|--------|
| Core | Guards, Interceptors, Services | ✅ Implemented |
| Features | Auth, Tasks | ✅ Implemented |
| Shared | Common Components | ✅ Implemented |
| Testing | Jasmine (15 tests) | ✅ Passing |
| Architecture | Standalone Components | ✅ Implemented |

### DevOps & Version Control

| Item | Description | Status |
|------|-------------|--------|
| Repository | Git Repository | ✅ Configured |
| Ignore Rules | .gitignore | ✅ Configured |
| Documentation | README.md | ✅ Complete |
| Commits | Professional Commits | ✅ Implemented |

---

## 🤝 Contributing
1. Fork the repository
2. Create a feature branch (git checkout -b feature/amazing-feature)
3. Commit your changes (git commit -m 'feat: add amazing feature')
4. Push to the branch (git push origin feature/amazing-feature)
5. Open a Pull Request

## 📄 License
This project is licensed under the MIT License.

## 👩‍💻 Author
Created with ❤️ by Silvanna Siqueira

## 📞 Support
For support, email silvana.siqueira@gmail.com or open an issue on GitHub.EOF
git add README.md git commit -m "docs: add comprehensive README with setup instructions"
---




