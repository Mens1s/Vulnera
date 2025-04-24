# 💥 Vulnera.NET – Security Playground

Vulnera.NET is a deliberately insecure ASP.NET 6.0 Web API + Bootstrap-based playground built to demonstrate:
- ✅ View YouTube DEMO: https://youtu.be/vsUeiiuE0kg
- ✅ SQL Injection vulnerabilities
- ✅ Brute Force attack simulation
- ✅ Real-time CPU/RAM usage tracking (Server Stress)
- ✅ Visual frontend for learning and demo purposes

---

## ⚙️ Technologies

- ASP.NET Core 9.0
- Bootstrap 5 (UI)
- Chart.js (Live charts)
- JavaScript (Frontend logic)
- SQL Server / LocalDB

---

## 📁 Features

### 🔐 Login / Register
> Endpoints are intentionally SQL injection vulnerable

- `/api/users/register`  
- `/api/users/login`  
- `SQLi test` with payloads like `' OR '1'='1`

---

### 🔍 User Search
> Vulnerable search feature using `LIKE '%query%'` + optional SQLi payloads

---

### 🚨 Brute Force Simulation
> Run dictionary-based brute force attack from the UI  
> Passwords like `123`, `admin123`, `password` are tested client-side

---

### 📊 System Stats + Realtime Graphs
> Backend exposes CPU & memory usage  
> Frontend shows usage via `Chart.js` and updates every 3 seconds



1. GET /api/systemstats/stats

---

### 🔥 CPU Stress Endpoint
> Designed to spike CPU for testing charts and system load impact

1. GET /api/stress/cpu-stress?seconds=10
---

## 🚀 Run Instructions

1. Clone repo  
2. Restore packages  
3. Run SQL script (Users table)  
4. Update `appsettings.json` connection string  
5. Run the app  
6. Open `index.html` in browser

---

## 📷 Screenshots

![login](screenshots/login.png)  
![brute](screenshots/brute.png)  
![chart](screenshots/systemstats.png)

---

## ❗ Disclaimer

This project is **intentionally vulnerable** and for **educational/demonstration** purposes only.  
Do **not deploy** to production or expose to public networks.

---

## 🧠 Author

Built by Ahmet Yiğit – for security demos, backend performance education, and fun 😎
