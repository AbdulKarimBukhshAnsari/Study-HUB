using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace RemindersStudyHub
{
    public class SQLiteHelper
    {
        private string connectionString = "Data Source=" + Path.Combine(Application.StartupPath, "ToDoApp.db") + ";Version=3;";


        public void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Tasks (
                        TaskId INTEGER PRIMARY KEY AUTOINCREMENT,
                        TaskName TEXT NOT NULL,
                        DueDate TEXT NOT NULL,
                        Priority TEXT NOT NULL
                    );";
                SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void AddTask(TaskItem task)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    var command = new SQLiteCommand("INSERT INTO Tasks (TaskName, DueDate, Priority) VALUES (@name, @date, @priority);", connection);
                    command.Parameters.AddWithValue("@name", task.TaskName);
                    command.Parameters.AddWithValue("@date", task.DueDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@priority", task.Priority);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding task: " + ex.Message);
            }
        }

        public List<TaskItem> GetAllTasks()
        {
            List<TaskItem> tasks = new List<TaskItem>();
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    var command = new SQLiteCommand("SELECT * FROM Tasks ORDER BY DueDate ASC;", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new TaskItem(
                                Convert.ToInt32(reader["TaskId"]),
                                reader["TaskName"].ToString(),
                                DateTime.ParseExact(reader["DueDate"].ToString(), "yyyy-MM-dd", null),
                                reader["Priority"].ToString()
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching tasks: " + ex.Message);
            }
            return tasks;
        }

        public void RemoveTask(int taskId)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    var command = new SQLiteCommand("DELETE FROM Tasks WHERE TaskId = @id;", connection);
                    command.Parameters.AddWithValue("@id", taskId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error removing task: " + ex.Message);
            }
        }
    }
}
