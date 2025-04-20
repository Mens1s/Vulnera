
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Vulnera.NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IConfiguration _config;
        private static List<String> _logs = new List<string>();
        public UserController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("register")]
        public IActionResult Register(string username, string password, string email)
        {
            var connStr = _config.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connStr);
            conn.Open();

            string query = $"(INSERT INTO Users Username, Password, Email) VALUES ('{username}','{password}','{email}')";

            using var cmd = new SqlCommand(query, conn);
            int affectedRows = cmd.ExecuteNonQuery();

            return Ok($"Kullanici eklendi. Etkilenen satir sayisi: {affectedRows}");
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            var connStr = _config.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connStr);
            conn.Open();

            string query = $"SELECT * FROM Users WHERE Username = '{username}' AND Password = '{password}'";

            using var cmd = new SqlCommand(query, conn);
            var count = cmd.ExecuteScalar();

            _logs.Add($"New login attempt! {username}. Stress : {CpuStress()}");



            if(count != null)
            {
                return Ok("Giris basarili");
            }
            else
            {
                return Unauthorized("Giris basarisiz");
            }

        }

        [HttpGet("search")]
        public IActionResult Search(string query)
        {
            var connStr = _config.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connStr);
            conn.Open();

            string sql = $"SELECT Username, Email FROM Users WHERE Username LIKE '%{query}%'";
            // ' OR '1'='1
            using var cmd = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            var results = new List<object>();
            while (reader.Read())
            {
                results.Add(new
                {
                    Username = reader["Username"].ToString(),
                    Email = reader["Email"].ToString()
                });
            }

            return Ok(results);
        }

        private String CpuStress([FromQuery] int seconds = 10)
        {
            var end = DateTime.Now.AddSeconds(seconds);

            Task.Run(() =>
            {
                while (DateTime.Now < end)
                {
                    for (int i = 0; i < Environment.ProcessorCount; i++)
                    {
                        Task.Run(() =>
                        {
                            while (DateTime.Now < end)
                            {
                                // Boş CPU yakan iş: asal sayı kontrolü
                                for (long x = 0; x < 10_000; x++)
                                {
                                    IsPrime(x);
                                }
                            }
                        });
                    }
                }
            });

            return $"CPU stress test started for {seconds} seconds.";
        }

        private bool IsPrime(long number)
        {
            if (number <= 1) return false;
            if (number <= 3) return true;
            if (number % 2 == 0 || number % 3 == 0) return false;

            for (long i = 5; i * i <= number; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0) return false;
            }
            return true;
        }
    }
}
/*
CREATE DATABASE VulneraNet;
GO

USE VulneraNet;
GO

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(100),
    Password NVARCHAR(100),
    Email NVARCHAR(255)
);
GO

INSERT INTO Users (Username, Password, Email)
VALUES 
('admin', 'admin123', 'admin@vulneranet.com'),
('ahmet', '1234', 'ahmet@mail.com');
GO
*/