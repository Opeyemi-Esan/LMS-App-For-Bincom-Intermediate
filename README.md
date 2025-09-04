# 📚 Library Management System API

A **.NET 8 Web API** for managing a digital library. The API handles **user registration/login, book catalog, borrowing history, and JWT-based authentication**. It is documented with **Swagger** for easy testing.

---

## 🚀 Features

* 📖 Manage books (add, update, delete, fetch)
* 👥 Manage library users
* 📌 Borrow and return books with history tracking
* 🔑 Secure JWT authentication & authorization
* 📜 Swagger UI documentation with request/response examples
* 📝 Centralized logging with Serilog

---

## ⚙️ Prerequisites

Before running the project, make sure you have installed:

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or Azure SQL Database)
* [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

---

## 🛠️ Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/Opeyemi-Esan/LMS-App-For-Bincom-Intermediate.git
cd LMS-App-For-Bincom-Intermediate
```

### 2. Configure Database Connection

Open **`appsettings.json`** and update the connection string:

```json
"ConnectionStrings": {
  "LibraryDbConnection": "Server=ENOP-PC\\SQLEXPRESS;Database=BincomLMSAppDb;TrustServerCertificate=True;Trusted_Connection=True"
}
```

### 3. Configure JWT Settings

In **`appsettings.json`**, set up your JWT keys:

```json
"Jwt": {
  "Key": "ThisIsMySuperSecretKeyOpeyemiJosephEsan123456789",
  "Issuer": "BincomDev",
  "Audience": "BincomLMSApp",
  "DurationInMinutes": 60
}
```

> 🔑 Use a long, random key for security.

### 4. Apply Database Migrations

Run the following command to create/update the database:

```bash
dotnet ef database update
```

### 5. Build the Project

```bash
dotnet build
```

### 6. Run the API

```bash
dotnet run
```

The API will start at:
👉 `https://localhost:7147`

---

## 📜 API Documentation (Swagger)

Once running, open your browser at:
👉 https://localhost:7147/swagger

From here, you can:

* Explore all endpoints
* Authenticate with JWT (`Authorize` button)
* Try requests with sample payloads

---

## 🧪 Example API Request

### Register a User

**POST** `/api/libraryuser/register`

**Request Body:**

```json
{
  "firstName": "Ojo",
  "lastName": "Tunde",
  "email": "ojotunde@example.com",
  "password": "SecurePass123!"
  "confirmpassword": "SecurePass123!"
}
```


---

## 🛡️ Authentication

All protected endpoints require a **JWT token** in the `Authorization` header:

```
Authorization: Bearer {your_token_here}
```

Tokens are obtained by calling the **login endpoint**.

---

## 📝 Logging

Logs are written to:

```
/logs/log.txt
```

Rotated **daily** via Serilog.

---

## 🤝 Contributing

Pull requests are welcome! Please follow standard Git branching and PR process.

---

## 📄 License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).
