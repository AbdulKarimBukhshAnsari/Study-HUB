using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RemindersStudyHub
{

    public partial class ViewPage : Form
    {
        private Common_Components windowSetup;
        private AdditReminder editReminderSetup;
        CalendarManager calendarManager = CalendarManager.Instance;
        private Label headerLabel, colour1, colour2;
        private Button deleteButton, viewButton, cancelButton, newButton;
        public DataGridView displayBox;

        public ViewPage(List<Reminder> reminders)
        {
            InitializeComponent();
            windowSetup = new Common_Components(this, calendarManager, "bgImage.jpg");

            windowSetup.monthCalendar.DateChanged += MonthCalendar_DateChanged;

            // Button setups
            windowSetup.button1.Text = "Add Reminder";
            windowSetup.button1.Click += AddReminderButton_Click;
            windowSetup.button2.Text = "Search Reminder";
            windowSetup.button2.Click += SearchReminderButton_Click;

            // Initialize header label
            headerLabel = new Label
            {
                Font = new Font("Castellar", 20, FontStyle.Regular),
                Text = "SEARCH RESULTS",
                Location = new Point(100, 55),
                Size = new Size(250, 35)
            };
            windowSetup.reminderPanel.Controls.Add(headerLabel);

            // Initialize displayBox
            displayBox = new DataGridView
            {
                Size = new Size(340, 375),
                ScrollBars = ScrollBars.Vertical,
                Location = new Point(35, 122),
                BackgroundColor = Color.White,
                ReadOnly = true,
                AllowUserToResizeRows = false,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                AllowUserToResizeColumns = false,
            };

            // Add custom columns
            displayBox.Columns.Add("Date", "Date");
            displayBox.Columns.Add("Title", "Title");
            displayBox.Columns.Add("Time", "Time");
            displayBox.DefaultCellStyle.Font = new Font("Calibri Light", 12);
            displayBox.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri Light", 12);
            displayBox.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            displayBox.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            displayBox.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            displayBox.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            ShowReminders(reminders);
        }

        public ViewPage()
        {
            InitializeComponent();
            windowSetup = new Common_Components(this, calendarManager, "bgImage.jpg");

            windowSetup.monthCalendar.DateChanged += MonthCalendar_DateChanged;

            // specifying add reminder button
            windowSetup.button1.Text = "Add Reminder";
            windowSetup.button1.Click += AddReminderButton_Click;

            // specifying search reminder button
            windowSetup.button2.Text = "Search Reminder";
            windowSetup.button2.Click += SearchReminderButton_Click;

            // initializing header label
            headerLabel = new Label
            {
                Font = new Font("Castellar", 25, FontStyle.Regular),
                Text = "Reminders",
                Location = new Point(100, 55),
                Size = new Size(250, 35)
            };
            windowSetup.reminderPanel.Controls.Add(headerLabel);

            // initializing colour key
            colour1 = new Label
            {
                BackColor = Color.LightGray,
                Location = new Point(130, 100),
                Size = new Size(80, 15),
                Text = "PREVIOUS",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Calibri Light", 10)
            };

            colour2 = new Label
            {
                BackColor = Color.LightSkyBlue,
                Location = new Point(220, 100),
                Size = new Size(80, 15),
                Text = "UPCOMING",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Calibri Light", 10)
            };

            windowSetup.reminderPanel.Controls.Add(colour1);
            windowSetup.reminderPanel.Controls.Add(colour2);

            // initializing displayBox
            displayBox = new DataGridView
            {
                Size = new Size(340, 375),
                ScrollBars = ScrollBars.Vertical,
                Location = new Point(35, 122),
                BackgroundColor = Color.White,
                ReadOnly = true,
                AllowUserToResizeRows = false,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                AllowUserToResizeColumns = false,
            };

            DataGridViewTextBoxColumn serialColumn = new DataGridViewTextBoxColumn
            {
                Name = "SerialNumber",
                HeaderText = "S.No.",
                ReadOnly = true,
                Resizable = DataGridViewTriState.False
            };

            displayBox.Columns.Insert(0, serialColumn);
            displayBox.Columns.Add("Title", "Title");
            displayBox.Columns.Add("Time", "Time");
            displayBox.DefaultCellStyle.Font = new Font("Calibri Light", 12);
            displayBox.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri Light", 12);
            displayBox.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            displayBox.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            displayBox.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            displayBox.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            displayBox.RowPostPaint += (sender, e) =>
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(),
                    displayBox.Font, Brushes.Black, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 5);
            };

            windowSetup.reminderPanel.Controls.Add(displayBox);

            viewButton = new Button
            {
                Text = "Edit",
                Size = new Size(70, 30),
                Location = new Point(120, 507),
                BackColor = Color.AliceBlue,
            };
            viewButton.Click += EditButton_Click;

            deleteButton = new Button
            {
                Text = "Delete",
                Size = new Size(70, 30),
                Location = new Point(210, 507),
                BackColor = Color.CornflowerBlue,
            };
            deleteButton.Click += DeleteButton_Click;

            windowSetup.reminderPanel.Controls.Add(viewButton);
            windowSetup.reminderPanel.Controls.Add(deleteButton);

            ShowReminders();
        }


        private void EditButton_Click(object? sender, EventArgs e)
        {
            if (displayBox.CurrentRow != null)
            {
                displayBox.Visible = false;
                viewButton.Visible = false;
                deleteButton.Visible = false;

                DataGridViewRow selectedRow = displayBox.CurrentRow;
                string title = selectedRow.Cells["Title"].Value.ToString();
                string timeString = selectedRow.Cells["Time"].Value.ToString();
                DateTime time = DateTime.ParseExact(timeString, "hh:mm tt", null);

                Reminder? reminder = calendarManager.GetReminderDetails(calendarManager.selectedDate, time, title);


                if (reminder != null)
                {
                    editReminderSetup = new AdditReminder(windowSetup) { };
                    editReminderSetup.headerLabel.Text = "Edit Reminder";
                    editReminderSetup.headerLabel.Size = new Size(340, 35);
                    editReminderSetup.titleTextBox.Text = reminder.Title;
                    editReminderSetup.descriptionTextBox.Text = reminder.Description;
                    editReminderSetup.hourBox.SelectedItem = reminder.Time.ToString("hh");
                    editReminderSetup.minuteBox.SelectedItem = reminder.Time.ToString("mm");
                    editReminderSetup.apmBox.SelectedItem = reminder.Time.ToString("tt");

                    windowSetup.button1.Text = "Save Changes";
                    windowSetup.button1.Click -= AddReminderButton_Click;
                    windowSetup.button1.Click += SaveChanges_Click;
                    windowSetup.button2.Text = "Cancel";
                    windowSetup.button2.Location = new Point(143, 90);
                    windowSetup.button2.Size = new Size(90, 50);
                    windowSetup.button2.Click -= SearchReminderButton_Click;
                    windowSetup.button2.Click += CancelEdit_Click;

                    newButton = new Button
                    {
                        Text = "Delete",
                        Size = new Size(90, 50),
                        Location = new Point(25, 90)
                    };
                    newButton.Click += NewButton_Click;
                    windowSetup.buttonPanel.Controls.Add(newButton);
                }
                else
                {
                    MessageBox.Show("Reminder details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    displayBox.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Please select a reminder to view.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ReturnDisplay()
        {
            editReminderSetup.Erase();
            displayBox.Visible = true;
            viewButton.Visible = true;
            deleteButton.Visible = true;
            windowSetup.button1.Text = "Add Reminder";
            windowSetup.button1.Click -= SaveChanges_Click;
            windowSetup.button1.Click += AddReminderButton_Click;
            windowSetup.button2.Text = "Search Reminder";
            windowSetup.button2.Click -= CancelEdit_Click;
            windowSetup.button2.Click += SearchReminderButton_Click;
            windowSetup.buttonPanel.Controls.Remove(newButton);
            windowSetup.button2.Location = new Point(63, 90);
            windowSetup.button2.Size = new Size(150, 50);
            ShowReminders();
        }
        private void SaveChanges_Click(object? sender, EventArgs e)
        {
            string title = editReminderSetup.titleTextBox.Text;
            string description = editReminderSetup.descriptionTextBox.Text;

            if (!string.IsNullOrWhiteSpace(title))
            {
                string timeString = displayBox.CurrentRow.Cells["Time"].Value.ToString();
                DateTime oldTime = DateTime.ParseExact(timeString, "hh:mm tt", null);
                string oldTitle = displayBox.CurrentRow.Cells["Title"].Value.ToString();
                DateTime reminderTime = editReminderSetup.GetSelectedTime(calendarManager);
                Reminder newReminder = new Reminder(title, reminderTime, description);
                DateTime date = calendarManager.selectedDate;

                calendarManager.EditReminder(date, oldTime, oldTitle, newReminder);

                MessageBox.Show("Changes Have Been Saved.", "Saved Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReturnDisplay();
            }
            else
            {
                MessageBox.Show("Please fill in Title.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void CancelEdit_Click(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit? Any changes made will not be saved.", "Cancel Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                ReturnDisplay();
            }
        }

        private void DeletionGeneral(DataGridViewRow selectedRow)
        {
            string title = selectedRow.Cells["Title"].Value.ToString();
            string timeString = selectedRow.Cells["Time"].Value.ToString();
            DateTime time = DateTime.ParseExact(timeString, "hh:mm tt", null);

            Reminder? reminder = calendarManager.GetReminderDetails(calendarManager.selectedDate, time, title);

            DateTime date = calendarManager.selectedDate;
            DateTime dateTime = date.Date.Add(time.TimeOfDay);
            string scheduled = dateTime.ToString("MMMM dd, yyyy h:mm tt");

            calendarManager.DeleteReminder(dateTime, reminder);

            MessageBox.Show($"The reminder {title} scheduled on {scheduled} has been deleted successfully.", "Successfully Deleted", MessageBoxButtons.OK);

            displayBox.Rows.Clear();
            ShowReminders();
        }

        private void NewButton_Click(object? sender, EventArgs e)
        {
            if (displayBox.CurrentRow != null)
            {
                calendarManager.selectedDate = windowSetup.monthCalendar.SelectionEnd;
                DialogResult result = MessageBox.Show("Are you sure you want to delete this reminder?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    DeletionGeneral(displayBox.CurrentRow);
                    ReturnDisplay();
                }
            }
        }

        private void DeleteButton_Click(object? sender, EventArgs e)
        {
            if (displayBox.CurrentRow != null)
            {
                calendarManager.selectedDate = windowSetup.monthCalendar.SelectionEnd;
                DialogResult result = MessageBox.Show("Are you sure you want to delete this reminder?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DeletionGeneral(displayBox.CurrentRow);
                }

            }
        }

        private void MonthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            ShowReminders();
        }

        private void ShowReminders()
        {
            calendarManager.selectedDate = windowSetup.monthCalendar.SelectionStart;

            var reminders = calendarManager.GetRemindersForDate(calendarManager.selectedDate);

            if (reminders.Count > 0)
            {
                displayBox.Rows.Clear();

                foreach (var reminder in reminders)
                {
                    string time = reminder.Time.ToString("hh:mm tt");
                    displayBox.Rows.Add(null, reminder.Title, time);
                }

                displayBox.CellFormatting += DisplayBox_CellFormatting;

                displayBox.RowPostPaint += (sender, e) =>
                {
                    e.Graphics.DrawString((e.RowIndex + 1).ToString(),
                        displayBox.Font, Brushes.Black, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 5);
                };
            }
            else
            {
                var startPage = new StartPage();
                startPage.Show();
                this.Hide();
            }
        }

        private void ShowReminders(List<Reminder> reminders)
        {
            displayBox.Rows.Clear();

            if (reminders.Count == 0)
            {
                MessageBox.Show("No reminders available.", "No Reminders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var reminder in reminders)
            {
                int rowIndex = displayBox.Rows.Add(); 

                DataGridViewRow row = displayBox.Rows[rowIndex];

                row.Cells["Date"].Value = reminder.Time.Date.ToString("dd-MM-yy"); 
                row.Cells["Title"].Value = reminder.Title;     
                row.Cells["Time"].Value = reminder.Time.ToString("hh:mm tt"); 

                if (reminder.Time < DateTime.Now) 
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                }
                else 
                {
                    row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }
            }
        }


        private void DisplayBox_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)  
            {
                // Get the reminder time from the cell
                DateTime reminderTime;
                if (DateTime.TryParse(e.Value?.ToString(), out reminderTime))
                {
                    if (reminderTime <= DateTime.Now) 
                    {
                        displayBox.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray; 
                    }
                    else 
                    {
                        displayBox.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightSkyBlue; 
                    }
                }
            }
        }

        // Add Reminder Button Click
        private void AddReminderButton_Click(object? sender, EventArgs e)
        {
            calendarManager.selectedDate = windowSetup.monthCalendar.SelectionStart;
            var addNewPageForm = new AddPage();
            addNewPageForm.Show();
            this.Hide();
        }


        // Search Reminder Button Click
        private void SearchReminderButton_Click(object? sender, EventArgs e)
        {
            calendarManager.selectedDate = windowSetup.monthCalendar.SelectionStart;
            var searchReminderForm = new SearchPage();
            searchReminderForm.Show();
            this.Hide();
        }
    }
}
