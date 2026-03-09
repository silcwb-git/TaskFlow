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

# Install dependencies
npm install

# Run development server
ng serve

Frontend will be available at: http://localhost:4200

### 🧪 Testing
# Backend Tests
```bash
cd src/TaskFlow.Backend
dotnet test
```

# Frontend Tests
```bash
cd src/TaskFlow.Frontend
ng test --watch=falsecd src/TaskFlow.Frontend
ng test --watch=false
```

## 📚 API Documentation

Access Swagger UI at: https://localhost:5001/swaggerAuthentication 

# Endpoints
POST /api/auth/register - Register new user
POST /api/auth/login - Login user

# Task Endpoints
GET /api/tasks - Get all tasks
GET /api/tasks/{id} - Get task by ID
POST /api/tasks - Create new task
PUT /api/tasks/{id} - Update task
DELETE /api/tasks/{id} - Delete task

## 📝 Example Usage

# Register
```bash
POST /api/auth/register
{
  "email": "user@example.com",
  "password": "Password123!"
}
```

# Create Task
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

TaskFlow/
├── src/
│   ├── TaskFlow.Backend/
│   │   ├── TaskFlow.Domain/
│   │   │   ├── Entities/
│   │   │   │   ├── User.cs
│   │   │   │   └── Task.cs
│   │   │   └── Enums/
│   │   │       ├── TaskStatus.cs
│   │   │       └── TaskPriority.cs
│   │   ├── TaskFlow.Application/
│   │   │   ├── DTOs/
│   │   │   │   ├── LoginDto.cs
│   │   │   │   ├── RegisterDto.cs
│   │   │   │   └── TaskDto.cs
│   │   │   └── Services/
│   │   ├── TaskFlow.Infrastructure/
│   │   │   ├── Data/
│   │   │   │   └── TaskFlowDbContext.cs
│   │   │   ├── Repositories/
│   │   │   │   └── GenericRepository.cs
│   │   │   └── Security/
│   │   │       └── JwtTokenService.cs
│   │   ├── TaskFlow.API/
│   │   │   ├── Controllers/
│   │   │   │   ├── AuthController.cs
│   │   │   │   └── TasksController.cs
│   │   │   ├── Program.cs
│   │   │   └── appsettings.json
│   │   ├── TaskFlow.Tests/
│   │   │   ├── Features/
│   │   │   │   ├── Auth/
│   │   │   │   │   ├── AuthServiceTests.cs (✅ 7 tests)
│   │   │   │   │   └── UserEntityTests.cs (✅ 5 tests)
│   │   │   │   └── Tasks/
│   │   │   │       └── TaskEntityTests.cs (✅ 5 tests)
│   │   │   ├── Infrastructure/
│   │   │   │   └── GenericRepositoryTests.cs (✅ 2 tests)
│   │   │   └── TaskFlow.Tests.csproj
│   │   ├── docker-compose.yml
│   │   └── TaskFlow.sln
│   │
│   └── TaskFlow.Frontend/
│       ├── src/
│       │   ├── app/
│       │   │   ├── core/
│       │   │   │   ├── guards/
│       │   │   │   │   └── auth.guard.ts
│       │   │   │   ├── interceptors/
│       │   │   │   │   └── jwt.interceptor.ts
│       │   │   │   └── services/
│       │   │   ├── features/
│       │   │   │   ├── auth/
│       │   │   │   │   ├── components/
│       │   │   │   │   │   ├── login.component.ts
│       │   │   │   │   │   └── register.component.ts
│       │   │   │   │   └── services/
│       │   │   │   │       ├── auth.service.ts
│       │   │   │   │       └── auth.service.spec.ts (✅ 6 tests)
│       │   │   │   └── tasks/
│       │   │   │       ├── components/
│       │   │   │       │   └── task-list.component.ts
│       │   │   │       └── services/
│       │   │   │           ├── task.service.ts
│       │   │   │           └── task.service.spec.ts (✅ 6 tests)
│       │   │   ├── shared/
│       │   │   │   └── components/
│       │   │   ├── app.component.ts
│       │   │   ├── app.component.spec.ts (✅ 3 tests)
│       │   │   ├── app.config.ts
│       │   │   └── app.routes.ts
│       │   ├── main.ts
│       │   ├── index.html
│       │   └── styles.css
│       ├── angular.json
│       ├── package.json
│       ├── tsconfig.json
│       └── karma.conf.js
│
├── .gitignore
├── README.md
└── .git/

# Backend (.NET 8)
✅ Domain Layer (Entities, Enums)
✅ Application Layer (DTOs, Services)
✅ Infrastructure Layer (Data, Repositories, Security)
✅ API Layer (Controllers, Program.cs)
✅ Tests (xUnit - 19 testes passando)
✅ Docker (SQL Server)

# Frontend (Angular 18+)
✅ Core (Guards, Interceptors, Services)
✅ Features (Auth, Tasks)
✅ Shared (Common Components)
✅ Tests (Jasmine - 15 testes passando)
✅ Standalone Components

# DevOps
✅ Git Repository
✅ .gitignore
✅ README.md
✅ Professional Commits

# 🎯 Tests Summary

| Layer | Framework | Tests | Status |
|--------|-----------|--------|--------|
| Backend | xUnit | 19 | ✅ Passing |
| Frontend | Jasmine | 15 | ✅ Passing |
| **Total** | - | **34** | **✅ Passing** |

# 🧪 Tests Details

# Backend (xUnit) - 19 testes
AuthServiceTests.cs → 7 tests ✅
UserEntityTests.cs → 5 tests ✅
TaskEntityTests.cs → 5 tests ✅
GenericRepositoryTests.cs → 2 tests ✅

# Frontend (Jasmine) - 15 tests
auth.service.spec.ts → 6 tests ✅
task.service.spec.ts → 6 tests ✅
app.component.spec.ts → 3 tests ✅

## 🤝 Contributing
1. Fork the repository
2. Create a feature branch (git checkout -b feature/amazing-feature)
3. Commit your changes (git commit -m 'feat: add amazing feature')
4. Push to the branch (git push origin feature/amazing-feature)
5. Open a Pull Request

## 📄 License
This project is licensed under the MIT License.

## 👨‍💻 Author
Created with ❤️ by Silvanna Siqueira

## 📞 SupportFor support, email silvana.siqueira@gmail.com or open an issue on GitHub.EOF
git add README.md git commit -m "docs: add comprehensive README with setup instructions"
---




