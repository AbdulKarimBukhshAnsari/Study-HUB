using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemindersStudyHub
{
    public partial class ToDo : Form
    {
        private SQLiteHelper dbHelper = new SQLiteHelper();
        private CustomQueue taskQueue = new CustomQueue();
        public ToDo()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ToDo_Load(object sender, EventArgs e)
        {
            textBox1.PlaceholderText = "Enter your Task here....";
            dbHelper.InitializeDatabase();
            var tasks = dbHelper.GetAllTasks();
            foreach (var task in tasks)
            {
                taskQueue.Enqueue(task);
                DisplayTask(task);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string taskName = textBox1.Text;
            DateTime dueDate = dateTimePicker1.Value;
            string priority = comboBox1.SelectedItem?.ToString() ?? "Normal";

            if (!string.IsNullOrEmpty(taskName))
            {
                TaskItem task = new TaskItem(taskName, dueDate, priority);
                dbHelper.AddTask(task); // Save task to the database

                DisplayTask(task); // Display the task in the UI
            }

            textBox1.Clear();
        }
        private void DisplayTask(TaskItem task)
        {
            Panel taskPanel = new Panel
            {
                Size = new Size(645, 150),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(225, 230, 234)
            };

            Label nameLabel = new Label
            {
                Text = "Task: " + task.TaskName,
                Location = new Point(10, 10),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Size = new Size(320, 45)
            };

            Label dateLabel = new Label
            {
                Text = "Due Date: " + task.DueDate.ToShortDateString(),
                Location = new Point(10, 70),
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                Size = new Size(420, 30),
                ForeColor = Color.Blue
            };

            Label priorityLabel = new Label
            {
                Text = "Priority: " + task.Priority,
                Location = new Point(10, 110),
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                Size = new Size(300, 30),
                ForeColor = Color.Red
            };

            Button deleteButton = new Button
            {
                Text = "Delete",
                BackColor = Color.Black,
                ForeColor = Color.White,
                Size = new Size(125, 50),
                Location = new Point(500, 40),
                Font = new Font("Segoe UI", 15, FontStyle.Regular, GraphicsUnit.Point, 0)
            };

            deleteButton.Click += (s, args) =>
            {
                flowLayoutPanel1.Controls.Remove(taskPanel);
                dbHelper.RemoveTask(task.TaskId); // Remove task from database
            };

            taskPanel.Controls.Add(nameLabel);
            taskPanel.Controls.Add(dateLabel);
            taskPanel.Controls.Add(priorityLabel);
            taskPanel.Controls.Add(deleteButton);

            flowLayoutPanel1.Controls.Add(taskPanel);
        }

    }
}
