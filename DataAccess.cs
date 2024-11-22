using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace RemindersStudyHub
{
    public class DataAccess
    {
        private string connectionString;

        public DataAccess()
        {
            // Connection string for SQLite database
            connectionString = "Data Source=GradeTracker.db;Version=3;";
        }
        public void EnsureTableExists()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Grades (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Month TEXT NOT NULL,
                    Subject TEXT NOT NULL,
                    TestType TEXT NOT NULL,
                    Percentage REAL NOT NULL
                );";

                    using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error ensuring table exists: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SaveGrade(string month, string subject, string testType, double percentage)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Grades (Month, Subject, TestType, Percentage) " +
                                   "VALUES (@Month, @Subject, @TestType, @Percentage)";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Month", month);
                        command.Parameters.AddWithValue("@Subject", subject);
                        command.Parameters.AddWithValue("@TestType", testType);
                        command.Parameters.AddWithValue("@Percentage", percentage);

                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Grade data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving grade data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DataTable GetAllGrades()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Id, Month, Subject, TestType, Percentage FROM Grades";

                    using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, connection))
                    {
                        dataAdapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving grade data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
        public void DeleteGrade(int id)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Grades WHERE Id = @Id";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting grade: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void DeleteAllGrades()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Grades"; // SQL to delete all rows in the Grades table
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting all grades: {ex.Message}");
            }
        }

        public (string, string, string) GetInsights()
        {
            string topSubject = string.Empty;
            string frequentMonth = string.Empty;
            string frequentTestType = string.Empty;

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Top Scoring Subject 🎯
                    string topSubjectQuery = "SELECT Subject, MAX(Percentage) AS MaxPercentage FROM Grades GROUP BY Subject ORDER BY MaxPercentage DESC LIMIT 1";
                    using (SQLiteCommand cmd = new SQLiteCommand(topSubjectQuery, connection))
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            topSubject = $"{reader["Subject"]}: {reader["MaxPercentage"]}% 🎯";
                        }
                    }

                    // Most Tested Month 📅
                    string monthQuery = "SELECT Month, COUNT(*) AS TestCount FROM Grades GROUP BY Month ORDER BY TestCount DESC LIMIT 1";
                    using (SQLiteCommand cmd = new SQLiteCommand(monthQuery, connection))
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            frequentMonth = $"{reader["Month"]} has {reader["TestCount"]} tests 📅";
                        }
                    }

                    // Most Frequent Test Type 📊
                    string testTypeQuery = "SELECT TestType, COUNT(*) AS TestCount FROM Grades GROUP BY TestType ORDER BY TestCount DESC LIMIT 1";
                    using (SQLiteCommand cmd = new SQLiteCommand(testTypeQuery, connection))
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            frequentTestType = $"{reader["TestType"]} is popular 📊";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving insights: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return (topSubject, frequentMonth, frequentTestType);
        }



    }
}
