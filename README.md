# 🚂 Train Ticket Management System

A full-featured Train Ticket Booking web application built with **ASP.NET Core 9 MVC**.

## ✨ Features

| Feature | Description |
|---------|-------------|
| 🔍 Train Search | Search trains by source & destination |
| 🎫 Ticket Booking | Book for multiple passengers with Gender |
| 📄 PDF Ticket | Download ticket as PDF (QuestPDF) |
| 🔍 PNR Status | Track booking using PNR number |
| 📜 Booking History | View & cancel your own bookings |
| ❌ Cancel Booking | Cancel confirmed bookings |
| 🔐 Authentication | Register/Login with ASP.NET Core Identity |
| 👑 Admin Panel | Dashboard with stats, Add Trains, View all bookings |

## 🛠️ Tech Stack

| Layer | Technology |
|-------|------------|
| Framework | ASP.NET Core 9 MVC |
| Database | SQL Server + EF Core |
| Auth | ASP.NET Core Identity |
| PDF | QuestPDF |
| UI | Bootstrap 5 + Bootstrap Icons |

## 🚀 Getting Started

### Prerequisites
- .NET 9 SDK
- SQL Server / LocalDB
- Visual Studio 2022

### Setup Steps

```bash
# 1. Clone the repo
git clone https://github.com/yourusername/TrainTicketManagement.git
cd TrainTicketManagement

# 2. Run migrations
dotnet ef database update

# 3. Run the project
dotnet run
```

### Default Admin Credentials
- **Email:** `admin@trainms.com`
- **Password:** `Admin@123`

## 📸 Screenshots

> Add screenshots here after running the project

## 👤 Author

**Your Name** — [GitHub Profile](https://github.com/yourusername)

---
*Built as a resume project to demonstrate ASP.NET Core MVC, EF Core, Identity, and PDF generation.*
