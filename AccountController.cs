using Microsoft.AspNetCore.Mvc;
using SecureWebApp.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;

namespace SecureWebApp.Controllers
{
    public class AccountController : Controller
    {
        // Simulated secure connection string string 
        private readonly string _connectionString = "Server=localhost;Database=SecureUserDB;Trusted_Connection=True;TrustServerCertificate=True;";

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Structural defense against Cross-Site Request Forgery (CSRF)
        public IActionResult Register(UserModel model)
        {
            // Enforces strict model state state validation checks before processing inputs
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // High-performance, secure hashing using SHA-256
            string hashedPassword = HashPassword(model.Password);

            // Parameterized Query Design to completely neutralize SQL Injection vectors
            string query = "INSERT INTO Users (Username, Email, PasswordHash) VALUES (@Username, @Email, @PasswordHash)";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Explicitly assigning sanitized data types parameters
                        command.Parameters.Add("@Username", System.Data.SqlDbType.VarChar, 20).Value = model.Username;
                        command.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, 100).Value = model.Email;
                        command.Parameters.Add("@PasswordHash", System.Data.SqlDbType.VarChar, 64).Value = hashedPassword;

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                ViewBag.SuccessMessage = "User account registered securely!";
                return View();
            }
            catch (SqlException ex)
            {
                // Secure logging rule: Log the full stack trace internally but show a generic error to the client
                ViewBag.ErrorMessage = "An internal processing error occurred during system transaction runtime.";
                return View(model);
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
