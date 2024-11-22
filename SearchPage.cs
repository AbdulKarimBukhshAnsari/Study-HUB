using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RemindersStudyHub
{
    public partial class SearchPage : Form
    {
        private Common_Components windowSetup;
        CalendarManager calendarManager = CalendarManager.Instance;
        private Label headerLabel, titleLabel, dateLabel, reminderLabel, timeLabel, glLabel, specificLabel;
        private TextBox titleTextBox;
        private ComboBox dateSelectionComboBox, hourBox, minuteBox, apmBox;
        private ComboBox reminderStatusComboBox, startHourBox, startMinuteBox, startApmBox;
        private ComboBox timeSelectionComboBox, endHourBox, endMinuteBox, endApmBox;
        private Button searchButton;
        private Panel specificPanel, rangePanel, greaterLessPanel;

        public SearchPage()
        {
            InitializeComponent();
            windowSetup = new Common_Components(this, calendarManager, "bgImage.jpg");

            windowSetup.button1.Text = "Home";
            windowSetup.button1.Click += HomeButton_Click;

            windowSetup.button2.Text = "Search";
            windowSetup.button2.Click += SearchButton_Click;

            headerLabel = new Label
            {
                Font = new Font("Castellar", 22, FontStyle.Regular),
                Location = new Point(34, 55),
                Size = new Size(345, 35),
                Text = "SEARCH REMINDERS"
            };
            windowSetup.reminderPanel.Controls.Add(headerLabel);

            titleLabel = new Label
            {
                Font = new Font("Calibri Light", 15),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Title: ",
                Location = new Point(25, 125),
                Size = new Size(50, 20)
            };
            windowSetup.reminderPanel.Controls.Add(titleLabel);

            // Title input
            titleTextBox = new TextBox
            {
                PlaceholderText = "Enter Title Keywords",
                Location = new Point(80, 127),
                Size = new Size(285, 20)
            };
            windowSetup.reminderPanel.Controls.Add(titleTextBox);

            dateLabel = new Label
            {
                Font = new Font("Calibri Light", 15),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Date Selection: ",
                Location = new Point(25, 185),
                Size = new Size(135, 20)
            };
            windowSetup.reminderPanel.Controls.Add(dateLabel);

            // Date selection combo box
            dateSelectionComboBox = new ComboBox
            {
                Location = new Point(162, 189),
                Size = new Size(203, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            dateSelectionComboBox.Items.Add("Search on Selected Date");
            dateSelectionComboBox.Items.Add("Search Generally");
            dateSelectionComboBox.SelectedIndex = 0;
            windowSetup.reminderPanel.Controls.Add(dateSelectionComboBox);

            // Reminder selection combo box
            reminderLabel = new Label
            {
                Font = new Font("Calibri Light", 15),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Reminder Status: ",
                Location = new Point(25, 245),
                Size = new Size(155, 20)
            };
            windowSetup.reminderPanel.Controls.Add(reminderLabel);

            reminderStatusComboBox = new ComboBox
            {
                Location = new Point(182, 249),
                Size = new Size(183, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            reminderStatusComboBox.Items.Add("Upcoming");
            reminderStatusComboBox.Items.Add("Passed");
            reminderStatusComboBox.Items.Add("All");
            reminderStatusComboBox.SelectedIndex = 0;
            windowSetup.reminderPanel.Controls.Add(reminderStatusComboBox);

            // Time selection combo box
            timeLabel = new Label
            {
                Font = new Font("Calibri Light", 15),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Search in Time: ",
                Location = new Point(25, 305),
                Size = new Size(155, 20)
            };
            windowSetup.reminderPanel.Controls.Add(timeLabel);

            timeSelectionComboBox = new ComboBox
            {
                Location = new Point(182, 309),
                Size = new Size(183, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            timeSelectionComboBox.Items.Add("Specific Time");
            timeSelectionComboBox.Items.Add("Before Time");
            timeSelectionComboBox.Items.Add("After Time");
            timeSelectionComboBox.Items.Add("Time Range");
            timeSelectionComboBox.SelectedIndex = 0;
            timeSelectionComboBox.SelectedIndexChanged += TimeSelectionComboBox_SelectedIndexChanged;
            windowSetup.reminderPanel.Controls.Add(timeSelectionComboBox);
        }

        private void TimeSelectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            windowSetup.reminderPanel.Controls.Remove(specificPanel);
            windowSetup.reminderPanel.Controls.Remove(rangePanel);
            windowSetup.reminderPanel.Controls.Remove(greaterLessPanel);

            string selectedOption = timeSelectionComboBox.SelectedItem.ToString();

            if (selectedOption == "Specific Time")
            {
                specificPanel = CreateSpecificTimePanel();
                windowSetup.reminderPanel.Controls.Add(specificPanel);
            }
            else if (selectedOption == "Time Range")
            {
                rangePanel = CreateTimeRangePanel();
                windowSetup.reminderPanel.Controls.Add(rangePanel);
            }
            else if (selectedOption == "Before Time" || selectedOption == "After Time")
            {
                greaterLessPanel = CreateGreaterLessTimePanel(selectedOption);
                windowSetup.reminderPanel.Controls.Add(greaterLessPanel);
            }
        }

        // Create Specific Time Panel
        private Panel CreateSpecificTimePanel()
        {
            Panel panel = new Panel{
                Size = new Size(350, 100), 
                Location = new Point(40, 370), 
            };

            Label specificTimeLabel = new Label
            {
                Text = "Specific Time:",
                Location = new Point(10, 10),
                Size = new Size(100, 20)
            };
            panel.Controls.Add(specificTimeLabel);

            hourBox = new ComboBox { Location = new Point(120, 10), Size = new Size(50, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            for (int i = 1; i < 13; i++) hourBox.Items.Add(i.ToString("D2"));
            panel.Controls.Add(hourBox);

            minuteBox = new ComboBox { Location = new Point(180, 10), Size = new Size(50, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            for (int i = 0; i < 60; i++) minuteBox.Items.Add(i.ToString("D2"));
            panel.Controls.Add(minuteBox);

            apmBox = new ComboBox { Location = new Point(240, 10), Size = new Size(60, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            apmBox.Items.Add("AM");
            apmBox.Items.Add("PM");
            panel.Controls.Add(apmBox);

            return panel;
        }

        // Create Greater/Less Time Panel
        private Panel CreateGreaterLessTimePanel(string option)
        {
            Panel panel = new Panel
            {
                Size = new Size(350, 100),
                Location = new Point(40, 370),
            };

            glLabel = new Label
            {
                Text = option + ":",
                Location = new Point(10, 10),
                Size = new Size(100, 20)
            };
            panel.Controls.Add(glLabel);

            hourBox = new ComboBox { Location = new Point(120, 10), Size = new Size(50, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            for (int i = 1; i < 13; i++) hourBox.Items.Add(i.ToString("D2"));
            panel.Controls.Add(hourBox);

            minuteBox = new ComboBox { Location = new Point(180, 10), Size = new Size(50, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            for (int i = 0; i < 60; i++) minuteBox.Items.Add(i.ToString("D2"));
            panel.Controls.Add(minuteBox);

            apmBox = new ComboBox { Location = new Point(240, 10), Size = new Size(60, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            apmBox.Items.Add("AM");
            apmBox.Items.Add("PM");
            panel.Controls.Add(apmBox);

            return panel;
        }

        // Create Time Range Panel
        private Panel CreateTimeRangePanel()
        {
            Panel panel = new Panel
            {
                Size = new Size(350, 100),
                Location = new Point(40, 370),
            };

            Label fromLabel = new Label { Text = "From:", Location = new Point(10, 10), Size = new Size(100, 20) };
            panel.Controls.Add(fromLabel);

            startHourBox = new ComboBox { Location = new Point(120, 10), Size = new Size(50, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            for (int i = 1; i < 13; i++) startHourBox.Items.Add(i.ToString("D2"));
            panel.Controls.Add(startHourBox);

            startMinuteBox = new ComboBox { Location = new Point(180, 10), Size = new Size(50, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            for (int i = 0; i < 60; i++) startMinuteBox.Items.Add(i.ToString("D2"));
            panel.Controls.Add(startMinuteBox);

            startApmBox = new ComboBox { Location = new Point(240, 10), Size = new Size(60, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            startApmBox.Items.Add("AM");
            startApmBox.Items.Add("PM");
            panel.Controls.Add(startApmBox);

            Label toLabel = new Label { Text = "To:", Location = new Point(10, 50), Size = new Size(100, 20) };
            panel.Controls.Add(toLabel);

            endHourBox = new ComboBox { Location = new Point(120, 50), Size = new Size(50, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            for (int i = 1; i < 13; i++) endHourBox.Items.Add(i.ToString("D2"));
            panel.Controls.Add(endHourBox);

            endMinuteBox = new ComboBox { Location = new Point(180, 50), Size = new Size(50, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            for (int i = 0; i < 60; i++) endMinuteBox.Items.Add(i.ToString("D2"));
            panel.Controls.Add(endMinuteBox);

            endApmBox = new ComboBox { Location = new Point(240, 50), Size = new Size(60, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            endApmBox.Items.Add("AM");
            endApmBox.Items.Add("PM");
            panel.Controls.Add(endApmBox);

            return panel;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string title = titleTextBox.Text;
            string dateSelection = dateSelectionComboBox.SelectedItem.ToString();
            string reminderStatus = reminderStatusComboBox.SelectedItem.ToString();
            string timeSelection = timeSelectionComboBox.SelectedItem.ToString();

            // Check if the required fields are filled
            if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(dateSelection) && string.IsNullOrEmpty(reminderStatus))
            {
                MessageBox.Show("Please fill in at least one of the fields: Title, Date, or Reminder Status.", "Input Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime? startTime = null;
            DateTime? endTime = null;
            DateTime selectedDate = calendarManager.selectedDate;

            if (timeSelection == "Specific Time" && hourBox != null && hourBox.SelectedItem != null &&
                minuteBox != null && minuteBox.SelectedItem != null && apmBox != null && apmBox.SelectedItem != null)
            {
                int hour = int.Parse(hourBox.SelectedItem.ToString());
                int minute = int.Parse(minuteBox.SelectedItem.ToString());
                string apm = apmBox.SelectedItem.ToString();

                if (apm == "PM" && hour != 12) hour += 12; 
                if (apm == "AM" && hour == 12) hour = 0; 

                startTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, hour, minute, 0);
            }
            else if ((timeSelection == "Before Time" || timeSelection == "After Time") && hourBox != null && hourBox.SelectedItem != null &&
                     minuteBox != null && minuteBox.SelectedItem != null && apmBox != null && apmBox.SelectedItem != null)
            {
                int hour = int.Parse(hourBox.SelectedItem.ToString());
                int minute = int.Parse(minuteBox.SelectedItem.ToString());
                string apm = apmBox.SelectedItem.ToString();

                if (apm == "PM" && hour != 12) hour += 12;
                if (apm == "AM" && hour == 12) hour = 0;

                startTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, hour, minute, 0);
            }
            else if (timeSelection == "Time Range" && startHourBox != null && startHourBox.SelectedItem != null && startMinuteBox != null && startMinuteBox.SelectedItem != null
                     && startApmBox != null && startApmBox.SelectedItem != null && endHourBox != null && endHourBox.SelectedItem != null &&
                     endMinuteBox != null && endMinuteBox.SelectedItem != null && endApmBox != null && endApmBox.SelectedItem != null)
            {
                int startHour = int.Parse(startHourBox.SelectedItem.ToString());
                int startMinute = int.Parse(startMinuteBox.SelectedItem.ToString());
                string startApm = startApmBox.SelectedItem.ToString();

                if (startApm == "PM" && startHour != 12) startHour += 12;
                if (startApm == "AM" && startHour == 12) startHour = 0;

                int endHour = int.Parse(endHourBox.SelectedItem.ToString());
                int endMinute = int.Parse(endMinuteBox.SelectedItem.ToString());
                string endApm = endApmBox.SelectedItem.ToString();

                if (endApm == "PM" && endHour != 12) endHour += 12;
                if (endApm == "AM" && endHour == 12) endHour = 0;

                startTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, startHour, startMinute, 0);
                endTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, endHour, endMinute, 0);
            }


            List<Reminder> results = calendarManager.SearchReminders(title, dateSelection == "Search on Selected Date" ? selectedDate : null, reminderStatus, timeSelection, startTime, endTime);

            if (results.Count == 0)
            {
                MessageBox.Show("No reminders found for the specified search criteria.", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string message = "";

                foreach (Reminder reminder in results)
                {
                    message += "Date: ";
                    message += reminder.Time.Date.ToString("dd-MM-yy");
                    message += "\nTitle: ";
                    message += reminder.Title.ToString();
                    message += "\nDescription: ";
                    message += reminder.Description.ToString();
                    message += "\nTime: ";
                    message += reminder.Time.ToString("hh:mm tt");
                    message += "\n\n";

                    MessageBox.Show(message, "Display Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void HomeButton_Click(object sender, EventArgs e)
        {
            // Navigate to the Home page
            var homePage = new StartPage();
            homePage.Show();
            this.Hide();
        }
    }
}
