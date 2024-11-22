using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemindersStudyHub
{
    public partial class GradeTracker : Form
    {
        // Declare placeholder text for both TextBoxes
        private string placeholderText1 = "Obtained Marks";  // For TextBox1
        private string placeholderText2 = "Total Marks";     // For TextBox2
        private readonly string pythonPath = @"C:\Program Files\Python311\python.exe";
        private readonly string scriptPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "graph1.py");
        private readonly string scriptPath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "graph2.py");
        private readonly string scriptPath2 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "graph3.py");
        private readonly string scriptPath3 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "graph4.py");



        public GradeTracker()
        {
            
            InitializeComponent();
            comboBox1.Items.AddRange(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames
            .Where(m => !string.IsNullOrEmpty(m)) // Filter out any empty entries
            .ToArray());
            comboBox2.Items.Add("Mathematics");
            comboBox2.Items.Add("Physics");
            comboBox2.Items.Add("Chemistry");
            comboBox2.Items.Add("Biology");
            comboBox2.Items.Add("Computer Science");
            //Set the item for the 3rd combo box which is for the subject 
            comboBox3.Items.Add("Quiz");
            comboBox3.Items.Add("Finals");
            // Set placeholder text when the form loads
            textBox1.Text = placeholderText1;
            textBox1.ForeColor = Color.Black; // Set placeholder color for TextBox1

            textBox2.Text = placeholderText2;
            textBox2.ForeColor = Color.Black; // Set placeholder color for TextBox2

            // Add events for handling placeholder behavior
            textBox1.Enter += textBox_Enter;
            textBox1.Leave += textBox_Leave;
            textBox2.Enter += textBox_Enter;
            textBox2.Leave += textBox_Leave;
            if (dataGridView1.Columns["Id"] != null)
            {
                dataGridView1.Columns["Id"].Visible = false;
            }
            LoadGradeData();
        }
        private void LoadGradeData()
        {
            DataAccess dataAccess = new DataAccess();
            DataTable gradeData = dataAccess.GetAllGrades();
            if (dataGridView1.Columns["Id"] != null)
            {
                dataGridView1.Columns["Id"].Visible = false;
            }
            dataGridView1.DataSource = gradeData;
            
        }

        private void GeneratePlot1()
        {
            try
            {
                // Verify Python installation
                if (!File.Exists(pythonPath))
                {
                    MessageBox.Show("Python executable not found at specified path.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verify Python script
                if (!File.Exists(scriptPath))
                {
                    MessageBox.Show("Python script not found at specified path.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the script directory for the plot path
                string scriptDir = Path.GetDirectoryName(scriptPath);
                string plotPath = Path.Combine(scriptDir, "plot1.png");

                // Clear existing image
                pictureBox2.Image?.Dispose();
                pictureBox2.Image = null;

                // Setup and run Python process
                using (Process process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = pythonPath,
                        Arguments = $"\"{scriptPath}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WorkingDirectory = scriptDir // Set working directory to script location
                    };

                    process.Start();

                    // Capture error output
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show($"Python script error: {error}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Wait briefly for file system to catch up
                System.Threading.Thread.Sleep(500);

                // Load the generated plot
                if (File.Exists(plotPath))
                {
                    try
                    {
                        using (var stream = new FileStream(plotPath, FileMode.Open, FileAccess.Read))
                        {
                            pictureBox2.Image = Image.FromStream(stream);
                            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                            
                        }
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Unable to load the plot image. The file might be in use.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Plot file was not generated at expected path: {plotPath}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GeneratePlot2()
        {
            try
            {
                // Verify Python installation
                if (!File.Exists(pythonPath))
                {
                    MessageBox.Show("Python executable not found at specified path.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verify Python script
                if (!File.Exists(scriptPath1))
                {
                    MessageBox.Show("Python script not found at specified path.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the script directory for the plot path
                string scriptDir = Path.GetDirectoryName(scriptPath1);
                string plotPath = Path.Combine(scriptDir, "plot2.png");

                // Clear existing image
                pictureBox3.Image?.Dispose();
                pictureBox3.Image = null;

                // Setup and run Python process
                using (Process process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = pythonPath,
                        Arguments = $"\"{scriptPath1}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WorkingDirectory = scriptDir // Set working directory to script location
                    };

                    process.Start();

                    // Capture error output
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show($"Python script error: {error}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Wait briefly for file system to catch up
                System.Threading.Thread.Sleep(500);

                // Load the generated plot
                if (File.Exists(plotPath))
                {
                    try
                    {
                        using (var stream = new FileStream(plotPath, FileMode.Open, FileAccess.Read))
                        {
                            pictureBox3.Image = Image.FromStream(stream);
                            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                        }
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Unable to load the plot image. The file might be in use.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Plot file was not generated at expected path: {plotPath}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GeneratePlot3()
        {
            try
            {
                // Verify Python installation
                if (!File.Exists(pythonPath))
                {
                    MessageBox.Show("Python executable not found at specified path.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verify Python script
                if (!File.Exists(scriptPath2))
                {
                    MessageBox.Show("Python script not found at specified path.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the script directory for the plot path
                string scriptDir = Path.GetDirectoryName(scriptPath2);
                string plotPath = Path.Combine(scriptDir, "plot3.png");

                // Clear existing image
                pictureBox4.Image?.Dispose();
                pictureBox4.Image = null;

                // Setup and run Python process
                using (Process process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = pythonPath,
                        Arguments = $"\"{scriptPath2}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WorkingDirectory = scriptDir // Set working directory to script location
                    };

                    process.Start();

                    // Capture error output
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show($"Python script error: {error}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Wait briefly for file system to catch up
                System.Threading.Thread.Sleep(500);

                // Load the generated plot
                if (File.Exists(plotPath))
                {
                    try
                    {
                        using (var stream = new FileStream(plotPath, FileMode.Open, FileAccess.Read))
                        {
                            pictureBox4.Image = Image.FromStream(stream);
                            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

                        }
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Unable to load the plot image. The file might be in use.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Plot file was not generated at expected path: {plotPath}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GeneratePlot4()
        {
            try
            {
                // Verify Python installation
                if (!File.Exists(pythonPath))
                {
                    MessageBox.Show("Python executable not found at specified path.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verify Python script
                if (!File.Exists(scriptPath3))
                {
                    MessageBox.Show("Python script not found at specified path.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the script directory for the plot path
                string scriptDir = Path.GetDirectoryName(scriptPath3);
                string plotPath = Path.Combine(scriptDir, "plot4.png");

                // Clear existing image
                pictureBox5.Image?.Dispose();
                pictureBox5.Image = null;

                // Setup and run Python process
                using (Process process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = pythonPath,
                        Arguments = $"\"{scriptPath3}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WorkingDirectory = scriptDir // Set working directory to script location
                    };

                    process.Start();

                    // Capture error output
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show($"Python script error: {error}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Wait briefly for file system to catch up
                System.Threading.Thread.Sleep(500);

                // Load the generated plot
                if (File.Exists(plotPath))
                {
                    try
                    {
                        using (var stream = new FileStream(plotPath, FileMode.Open, FileAccess.Read))
                        {
                            pictureBox5.Image = Image.FromStream(stream);
                            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;

                        }
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Unable to load the plot image. The file might be in use.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Plot file was not generated at expected path: {plotPath}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
        private void textBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;  // Get the TextBox that triggered the event
            if (textBox != null)
            {
                if (textBox.Text == placeholderText1 || textBox.Text == placeholderText2)
                {
                    textBox.Text = ""; // Clear placeholder text
                    textBox.ForeColor = Color.Black; // Change text color to black
                }
            }
        }

        // Generic Leave event handler for both TextBoxes
        private void textBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;  // Get the TextBox that triggered the event
            if (textBox != null)
            {
                // Check if the text is empty and restore the placeholder text accordingly
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (textBox == textBox1)
                    {
                        textBox.Text = placeholderText1; // Restore placeholder for TextBox1
                    }
                    else if (textBox == textBox2)
                    {
                        textBox.Text = placeholderText2; // Restore placeholder for TextBox2
                    }

                    textBox.ForeColor = Color.Gray; // Set placeholder color back to gray
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string month = comboBox1.Text;
            string subject = comboBox2.Text;
            string testType = comboBox3.Text;
            string obtainedMarksText = textBox1.Text;
            string totalMarksText = textBox2.Text;

            // Input validation
            if (string.IsNullOrWhiteSpace(month) || month == "Month")
            {
                MessageBox.Show("Please select a valid month.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(subject) || subject == "Subject")
            {
                MessageBox.Show("Please select a valid subject.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(testType) || testType == "Test")
            {
                MessageBox.Show("Please select a valid test type.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(obtainedMarksText, out int obtainedMarks) || obtainedMarks < 0)
            {
                MessageBox.Show("Please enter a valid number for obtained marks.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(totalMarksText, out int totalMarks) || totalMarks <= 0)
            {
                MessageBox.Show("Please enter a valid number for total marks.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (obtainedMarks > totalMarks)
            {
                MessageBox.Show("Obtained marks cannot exceed total marks.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Calculate percentage
            double percentage = (double)obtainedMarks / totalMarks * 100;

            // Save data
            DataAccess dataAccess = new DataAccess();
            //dataAccess.EnsureTableExists();
            dataAccess.SaveGrade(month, subject, testType, percentage);
            LoadGradeData();
            comboBox1.Text = "Month";
            comboBox2.Text = "Subject";
            comboBox3.Text = "Test";
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        //The delte button Functionality 
        private void button2_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Assuming the ID column is the primary key and is stored in the first column
                int id = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                // Confirm deletion
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                                      "Confirm Deletion",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Delete the record from the database
                    DataAccess dataAccess = new DataAccess();
                    dataAccess.DeleteGrade(id);

                    // Remove the row from the DataGridView
                    dataGridView1.Rows.Remove(selectedRow);

                    MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        DialogResult result = MessageBox.Show(
        "Are you sure you want to delete all records? This action cannot be undone.",
        "Confirm Delete All",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DataAccess dataAccess = new DataAccess();
                    dataAccess.DeleteAllGrades();

                    // Refresh the DataGridView
                    dataGridView1.DataSource = null; // Clear the existing data
                    LoadGradeData(); // Reload the data

                    MessageBox.Show("All records deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting all records: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)

        {
            GeneratePlot1();
            GeneratePlot2();
            GeneratePlot3();
            GeneratePlot4();
            Thread.Sleep(10000);
            DisplayInsights();
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            pictureBox4.Visible = true;
            pictureBox5.Visible = true;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void DisplayInsights()
        {
            DataAccess dataAccess = new DataAccess();
            var insights = dataAccess.GetInsights();

            label5.Text = insights.Item1; // Top Scoring Subject 🎯
            label6.Text = insights.Item2; // Most Tested Month 📅
            label7.Text = insights.Item3; // Most Frequent Test Type 📊
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
