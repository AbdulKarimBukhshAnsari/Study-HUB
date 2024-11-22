using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RemindersStudyHub
{
    
    public partial class AddPage : Form
    {
        private Common_Components windowSetup;
        private AdditReminder addReminderSetup;
        CalendarManager calendarManager = CalendarManager.Instance;

        public AddPage()
        {
            InitializeComponent();
            windowSetup = new Common_Components(this, calendarManager, "bgImage.jpg");

            // specifying save reminder button
            windowSetup.button1.Text = "Save Reminder";
            windowSetup.button1.Click += SaveButton_Click;

            // specifying cancel reminder button
            windowSetup.button2.Text = "Cancel Reminder";
            windowSetup.button2.Click += CancelButton_Click;

            addReminderSetup = new AdditReminder(windowSetup);

            addReminderSetup.headerLabel.Text = "ADD REMINDER";
        }

        // Event handler for Save button
        private void SaveButton_Click(object? sender, EventArgs e)
        {
            calendarManager.selectedDate = windowSetup.monthCalendar.SelectionStart;
            DateTime reminderTime = addReminderSetup.GetSelectedTime(calendarManager);
            if (calendarManager.selectedDate < DateTime.Today || reminderTime.TimeOfDay < DateTime.Today.TimeOfDay)
            {
                MessageBox.Show("Reminders cannot be added to a past date and time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string title = addReminderSetup.titleTextBox.Text;
            string description = addReminderSetup.descriptionTextBox.Text;

            if (!string.IsNullOrWhiteSpace(title))
            {
                

                Reminder newReminder = new Reminder(title, reminderTime, description);
                calendarManager.AddReminder(reminderTime, newReminder);

                MessageBox.Show("Reminder Added.", "Saved Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var nextPage = new ViewPage();
                nextPage.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please fill in Title.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Event handler for Cancel button
        private void CancelButton_Click(object? sender, EventArgs e)
        {
            calendarManager.selectedDate = windowSetup.monthCalendar.SelectionEnd;
            DialogResult result = MessageBox.Show("Are you sure?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(result == DialogResult.Yes)
            {
                var Startpage = new StartPage();
                Startpage.Show();
                this.Hide();
            }
        }
    }
}
