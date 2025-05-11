# ✈️ ARS - Airline Reservation System

**ARS** (Airline Reservation System) is a web-based airline booking system built using **ASP.NET MVC**, allowing users to search flights, book tickets, manage schedules, and handle roles (admin/customer). Designed as part of the **PRN231 Project**.

---

## 🚀 Features

- 🔍 **Flight Search** – Search flights by destination, date, and class  
- 🧾 **Ticket Booking** – Book, cancel, and view tickets  
- 🧑‍✈️ **Role Management** – Customer & Admin roles  
- 📅 **Flight Schedule Management** – Add/update/delete flights (admin)  
- 💳 **Payment Placeholder** – Ready to integrate payment gateways  
- 📊 **Basic Reporting** – Revenue or flight occupancy (optional)

---

## 🛠️ Technologies Used

- **Backend**: ASP.NET Core Web API, C#  
- **Frontend**: Razor Views, HTML, CSS, JavaScript  
- **Database**: SQL Server  
- **ORM**: Entity Framework  
- **Libraries**: Bootstrap, jQuery, AJAX  

---

## ⚙️ Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/MNTuas/ARS-Airline-Reservation-System-PRN231-GR5.git
```

### 2. Open in Visual Studio
- Open `ARS-Airline-Reservation-System.sln` in Visual Studio 2019 or later

### 3. Restore NuGet Packages
```
Tools → NuGet Package Manager → Restore NuGet Packages
```

### 4. Configure SQL Server
Edit the file `appsettings.json` (which is excluded from Git) with your database settings.

Use `appsettings.example.json` as a reference:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ARS_DB;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "your-secret-key",
    "Issuer": "your-issuer"
  }
}
```

### 5. Run the Application
- Press `F5` or go to `Debug → Start Debugging`

---

## 🔐 Sensitive File Cleanup

If you accidentally pushed `appsettings.json`, follow these steps:

```bash
# Install filter-repo if not available
# Windows: choco install git-filter-repo
# macOS: brew install git-filter-repo

# Remove appsettings.json from history
git filter-repo --path appsettings.json --invert-paths

# Add to .gitignore
echo "appsettings.json" >> .gitignore

# Add example file
cp appsettings.json appsettings.example.json

# Commit and push forcefully
git add .gitignore appsettings.example.json
git commit -m "Remove appsettings.json and add example"
git push origin --force --all
```

---

## 📁 Project Structure

```bash
ARS-Airline-Reservation-System/
├── Controllers/        # MVC Controllers
├── Models/             # Domain & View Models
├── Views/              # Razor .cshtml pages
├── Content/            # Static files (CSS, fonts)
├── Scripts/            # JavaScript & jQuery
├── appsettings.json    # Configuration (ignored from Git)
├── .gitignore          # Git ignore file
├── ARS-Airline-Reservation-System.sln # Solution file
└── Web.config          # Web app configuration
```

## 📄 License

This project is licensed under the MIT License.

> Created with ❤️ by PRN231 Group 5
