using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;

namespace RemindersStudyHub
{
    internal class Database
    {
        private string connectionString;

        public Database(string dbFilePath)
        {
            connectionString = $"Data Source={dbFilePath};Version=3;";
        }
        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
        public void CreateNotesTable()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // SQL query to create the 'notes' table
                string query = @"
                CREATE TABLE IF NOT EXISTS Note (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT UNIQUE NOT NULL,
                Content TEXT NOT NULL
                );";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void SaveNote(string name, string content)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT OR REPLACE INTO Note (Name, Content) VALUES (@Name, @Content)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Content", content);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string GetNoteContentByName(string name)
        {
            string content = string.Empty;
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Content FROM Note WHERE Name = @Name";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        content = result.ToString();
                    }
                }
            }
            return content;
        }

        public void CreateUserTable()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                
                string query = @"
                CREATE TABLE IF NOT EXISTS User (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT UNIQUE NOT NULL,
                Password TEXT NOT NULL
                );";

                using (var command1 = new SQLiteCommand(query, connection))
                {
                    command1.ExecuteNonQuery();
                }
            }
        }

        public void SaveUser(string name, string password)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT OR IGNORE INTO User (Name, Password) VALUES (@Name, @Password)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool ValidateUser(string name, string password)
        {
            bool isValidUser = false;

            // Make sure to properly handle the database connection and query
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                // Query to check if the user exists with the provided username and password
                string query = "SELECT COUNT(*) FROM User WHERE Name = @Name AND Password = @Password";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    // Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Password", password);

                    // Execute the query and get the result
                    var result = cmd.ExecuteScalar();
                    Console.WriteLine($"Query: {query}, Name: {name}, Password: {password}");
                    Console.WriteLine($"Result: {result}");

                    // Check if a match was found
                    if (result != null && Convert.ToInt32(result) > 0)
                    {
                        isValidUser = true; // User exists
                    }
                }
            }

            return isValidUser; // Return whether the user is valid
        }


    }
}
