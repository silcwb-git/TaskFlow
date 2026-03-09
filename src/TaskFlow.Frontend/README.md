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

### Frontend
- **Angular 18+**
- **TypeScript**
- **Standalone Components**
- **Jasmine/Karma Tests**
- **Responsive CSS**

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
│   │   ├── TaskFlow.Application/
│   │   ├── TaskFlow.Infrastructure/
│   │   └── TaskFlow.API/
│   └── TaskFlow.Frontend/
│       └── src/app/
│           ├── core/
│           ├── features/
│           └── shared/
└── README.md

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




