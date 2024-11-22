using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemindersStudyHub
{
    public partial class StartPage : Form
    {
        CalendarManager calendarManager = CalendarManager.Instance;
        private Button addReminderButton, searchReminderButton;
        private Common_Components windowSetup;

        public StartPage()
        {
            InitializeComponent();
            windowSetup = new Common_Components(this, calendarManager, "bgImage_Start.jpg");

            windowSetup.monthCalendar.DateChanged += MonthCalendar_DateChanged;

            // specifying add reminder button
            windowSetup.button1.Text = "Add Reminder";
            windowSetup.button1.Click += AddReminderButton_Click;

            // specifying search reminder button
            windowSetup.button2.Text = "Search Reminder";
            windowSetup.button2.Click += SearchReminderButton_Click;
        }


        //date changing
        private void MonthCalendar_DateChanged(object? sender, DateRangeEventArgs e)
        {
            calendarManager.selectedDate = windowSetup.monthCalendar.SelectionStart;

            if (calendarManager.selectedDate < DateTime.Today.Date)
            {
                windowSetup.button1.Click += InvalidDate_Click;
                windowSetup.button1.BackColor = Color.LightGray;
                windowSetup.button1.ForeColor = Color.AntiqueWhite;
                windowSetup.button1.Click -= AddReminderButton_Click; 
            }
            else
            {
                if (calendarManager.GetTotalReminders() != 0)
                {
                    var nextForm = new ViewPage();
                    nextForm.Show();
                    this.Hide();
                }
                else
                {
                    windowSetup.button1.Click += AddReminderButton_Click;
                    windowSetup.button1.Click -= InvalidDate_Click;
                    windowSetup.button1.BackColor = SystemColors.Control;
                    windowSetup.button1.ForeColor = SystemColors.ControlText; 
                }
            }
        }

        private void InvalidDate_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Reminders cannot be added to a past date and time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            var searchReminderForm = new ViewPage();
            searchReminderForm.Show();
            this.Hide();
        }
    }

    public class Common_Components
    {
        public MonthCalendar monthCalendar;
        public Panel reminderPanel, imagePanel, buttonPanel, borderPanel;
        public Button button1, button2;
        public Common_Components(Form form, CalendarManager calendarManager, string img)
        {
            form.BackColor = Color.White;
            form.Height = 600;
            form.Width = 700;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;

            // initalizing image panel
            imagePanel = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                Bounds = new Rectangle(-5, 0, 274, 184),
            };

            // image in bin/Debug/whatever folder
            Bitmap ogImage = new Bitmap("reminderImage.jpg");
            Bitmap resizedImage = new Bitmap(ogImage, imagePanel.ClientSize);
            imagePanel.BackgroundImage = resizedImage;
            imagePanel.BackgroundImageLayout = ImageLayout.Center;
            form.Controls.Add(imagePanel);

            // initializing border Panel
            borderPanel = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                Bounds = new Rectangle(-3, 185, 271, 5),
            };

            form.Controls.Add(borderPanel);

            // initializing calendar cause functions don't work. die C#
            monthCalendar = new MonthCalendar
            {
                MaxSelectionCount = 1,
                CalendarDimensions = new Size(2, 1),
                Width = form.Width / 14,
                FirstDayOfWeek = Day.Monday,
                ShowToday = false,
                Height = form.Height / 19,
                MinDate = DateTime.Today.AddYears(-1),
                MaxDate = DateTime.Today.AddYears(5),
                BackColor = Color.White,
                Bounds = new Rectangle(20, 213, 0, 0),
                SelectionStart = calendarManager.selectedDate,
            };
            form.Controls.Add(monthCalendar);


            // initalizing button panel
            buttonPanel = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                Bounds = new Rectangle(-5, 400, 273, 160),
            };
            form.Controls.Add(buttonPanel);

            // initializing add reminder button
            button1 = new Button
            {
                Size = new Size(150, 50),
                Location = new Point(63, 25)
            };
            buttonPanel.Controls.Add(button1);

            // initializing search reminder button
            button2 = new Button
            {
                Size = new Size(150, 50),
                Location = new Point(63, 90)
            };
            buttonPanel.Controls.Add(button2);

            // initalizing reminder panel
            reminderPanel = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                Bounds = new Rectangle(268, 0, 415, 565),
            };

            ogImage = new Bitmap(img);
            resizedImage = new Bitmap(ogImage, reminderPanel.ClientSize);
            reminderPanel.BackgroundImage = resizedImage;
            reminderPanel.BackgroundImageLayout = ImageLayout.Center;

            form.Controls.Add(reminderPanel);
        }
    }

    // Reminder Class
    public class Reminder
    {
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }

        public Reminder(string title, DateTime time, string description)
        {
            Title = title;
            Time = time;
            Description = description;
        }
    }

    public class ReminderNode
    {
        public DateTime Key { get; set; } 
        public List<Reminder> Reminders { get; set; }
        public ReminderNode Left { get; set; }
        public ReminderNode Right { get; set; }

        public ReminderNode(DateTime key, Reminder reminder)
        {
            Key = key;
            Reminders = new List<Reminder> { reminder };
            Left = null;
            Right = null;
        }
    }

    public class ReminderBST
    {
        private ReminderNode root;

        public void Add(DateTime dateTime, Reminder reminder)
        {
            root = AddRecursive(root, dateTime.Date, reminder);
        }

        private ReminderNode AddRecursive(ReminderNode node, DateTime date, Reminder reminder)
        {
            if (node == null)
                return new ReminderNode(date, reminder);

            if (date < node.Key)
                node.Left = AddRecursive(node.Left, date, reminder);
            else if (date > node.Key)
                node.Right = AddRecursive(node.Right, date, reminder);
            else
            {
                InsertSortedByTime(node.Reminders, reminder);
            }

            return node;
        }

        private void InsertSortedByTime(List<Reminder> reminders, Reminder reminder)
        {
            int index = reminders.FindIndex(r => r.Time.TimeOfDay > reminder.Time.TimeOfDay);
            if (index < 0)
            {
                reminders.Add(reminder); 
            }
            else
            {
                reminders.Insert(index, reminder); 
            }
        }

        public List<Reminder> GetRemindersForDate(DateTime date)
        {
            ReminderNode node = SearchRecursive(root, date);
            return node != null ? node.Reminders : new List<Reminder>();
        }

        private ReminderNode SearchRecursive(ReminderNode node, DateTime date)
        {
            if (node == null || node.Key == date)
                return node;

            return date < node.Key ? SearchRecursive(node.Left, date) : SearchRecursive(node.Right, date);
        }

        public Reminder? GetSpecificReminder(DateTime date, DateTime time, string title)
        {
            ReminderNode node = SearchRecursive(root, date);

            if (node != null)
            {
                return node.Reminders.FirstOrDefault(r => r.Time == time && r.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            }
            return null;
        }

        public int CountRemindersForDate(DateTime date)
        {
            return GetRemindersForDate(date).Count;
        }

        public bool DeleteReminder(DateTime dateTime, Reminder reminder)
        {
            root = DeleteRecursive(root, dateTime.Date, reminder);
            return root != null;
        }

        public List<DateTime> GetAllDates()
        {
            List<DateTime> dates = new List<DateTime>();
            CollectDates(root, dates);
            return dates.Distinct().ToList(); // Ensure unique dates
        }

        private void CollectDates(ReminderNode node, List<DateTime> dates)
        {
            if (node == null) return;

            dates.Add(node.Key);
            CollectDates(node.Left, dates);
            CollectDates(node.Right, dates);
        }

        private ReminderNode DeleteRecursive(ReminderNode node, DateTime date, Reminder reminder)
        {
            if (node == null) return null;

            if (date < node.Key)
                node.Left = DeleteRecursive(node.Left, date, reminder);
            else if (date > node.Key)
                node.Right = DeleteRecursive(node.Right, date, reminder);
            else
            {
                node.Reminders.Remove(reminder);
                if (node.Reminders.Count == 0)
                {
                    if (node.Left == null) return node.Right;
                    if (node.Right == null) return node.Left;
                    ReminderNode temp = FindMin(node.Right);
                    node.Key = temp.Key;
                    node.Reminders = temp.Reminders;
                    node.Right = DeleteRecursive(node.Right, temp.Key, reminder);
                }
            }
            return node;
        }

        private ReminderNode FindMin(ReminderNode node)
        {
            while (node.Left != null)
                node = node.Left;
            return node;
        }
    }

    public partial class CalendarManager
    {
        private static CalendarManager _instance;
        private static readonly object _lock = new object();

        private ReminderBST reminderBST;
        public DateTime selectedDate = DateTime.Today;

        private CalendarManager()
        {
            reminderBST = new ReminderBST();
        }

        public static CalendarManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CalendarManager();
                    }
                    return _instance;
                }
            }
        }

        public void AddReminder(DateTime dateTime, Reminder reminder)
        {
            // Ensure we are adding the reminder to the correct date
            reminderBST.Add(dateTime, reminder);
        }

        public bool DeleteReminder(DateTime dateTime, Reminder reminder)
        {
            return reminderBST.DeleteReminder(dateTime, reminder);
        }

        public List<Reminder> GetRemindersForDate(DateTime date)
        {
            return reminderBST.GetRemindersForDate(date);
        }

        public Reminder? GetReminderDetails(DateTime date, DateTime time, string title)
        {
            return reminderBST.GetSpecificReminder(date, time, title);
        }

        public int GetTotalReminders()
        {
            return reminderBST.CountRemindersForDate(selectedDate);
        }

        public List<DateTime> GetAllReminderDates()
        {
            return reminderBST.GetAllDates();
        }

        public bool EditReminder(DateTime date, DateTime oldTime, string oldTitle, Reminder updatedReminder)
        {
            Reminder? existingReminder = GetReminderDetails(date, oldTime, oldTitle);

            if (existingReminder != null)
            {
                DeleteReminder(date, existingReminder);

                AddReminder(updatedReminder.Time, updatedReminder);
                return true;
            }
            return false;
        }

        public List<Reminder> SearchReminders(string? title = null, DateTime? date = null, string? reminderStatus = null, string? timeSelection = null, DateTime? startTime = null, DateTime? endTime = null)
        {
            List<Reminder> results = new List<Reminder>();
            DateTime searchDate = selectedDate;

            foreach (var reminder in reminderBST.GetRemindersForDate(searchDate))
            {
                bool matchesTitle = title == null || reminder.Title.Contains(title, StringComparison.OrdinalIgnoreCase);
                bool matchesStatus = true;

                // reminder status filtering
                if (reminderStatus != null)
                {
                    if (reminderStatus == "Upcoming" && reminder.Time <= DateTime.Now)
                        matchesStatus = false;
                    if (reminderStatus == "Passed" && reminder.Time > DateTime.Now)
                        matchesStatus = false;
                }

                bool matchesTime = true;

                // time selection filtering
                if (matchesStatus && timeSelection != null)
                {
                    switch (timeSelection)
                    {
                        case "Specific Time":
                            if (startTime.HasValue && reminder.Time.TimeOfDay != startTime.Value.TimeOfDay)
                                matchesTime = false;
                            break;

                        case "Before/After Time":
                            if (startTime.HasValue && reminder.Time.TimeOfDay <= startTime.Value.TimeOfDay)
                                matchesTime = false;
                            break;

                        case "Time Range":
                            if (startTime.HasValue && endTime.HasValue &&
                                (reminder.Time.TimeOfDay < startTime.Value.TimeOfDay || reminder.Time.TimeOfDay > endTime.Value.TimeOfDay))
                            {
                                matchesTime = false;
                            }
                            break;
                    }
                }

                if (matchesTitle && matchesStatus && matchesTime)
                {
                    results.Add(reminder);
                }
            }

            return results;
        }

    }

    public partial class AdditReminder
    {
        public TextBox titleTextBox, descriptionTextBox;
        public Label headerLabel, titleLabel, descriptionLabel, timeLabel;
        public Label colonLabel;
        public ComboBox hourBox, minuteBox, apmBox;

        public AdditReminder(Common_Components windowSetup)
        {
            // initializing header label
            headerLabel = new Label
            {
                Font = new Font("Castellar", 25, FontStyle.Regular),
                Location = new Point(52, 55),
                Size = new Size(320, 35)
            };
            windowSetup.reminderPanel.Controls.Add(headerLabel);

            // initializing title label
            titleLabel = new Label
            {
                Font = new Font("Bradley Hand ITC", 20, FontStyle.Bold),
                Text = "Title",
                Location = new Point(25, 130),
                Size = new Size(200, 40)
            };

            windowSetup.reminderPanel.Controls.Add(titleLabel);

            // Initialize the title text box
            titleTextBox = new TextBox
            {
                PlaceholderText = "Enter Reminder Title",
                Font = new Font("Microsoft Jhenghei", 15),
                Size = new Size(349, 31),
                Location = new Point(30, 170),
                BorderStyle = BorderStyle.Fixed3D,
                Multiline = true,
                WordWrap = true,
                ScrollBars = ScrollBars.Vertical
            };
            windowSetup.reminderPanel.Controls.Add(titleTextBox);

            // description label
            descriptionLabel = new Label
            {
                Font = new Font("Bradley Hand ITC", 20, FontStyle.Bold),
                Text = "Description",
                Location = new Point(25, 220),
                Size = new Size(200, 40)
            };
            windowSetup.reminderPanel.Controls.Add(descriptionLabel);

            // Initialize the description text box
            descriptionTextBox = new TextBox
            {
                PlaceholderText = "Enter Reminder Description",
                Font = new Font("Microsoft Jhenghei", 15),
                Size = new Size(349, 150),
                Location = new Point(30, 260),
                BorderStyle = BorderStyle.Fixed3D,
                Multiline = true,
                WordWrap = true,
                ScrollBars = ScrollBars.Vertical
            };
            windowSetup.reminderPanel.Controls.Add(descriptionTextBox);

            // time label
            timeLabel = new Label
            {
                Font = new Font("Bradley Hand ITC", 20, FontStyle.Bold),
                Text = "Time Range",
                Location = new Point(25, 430),
                Size = new Size(200, 40)
            };
            windowSetup.reminderPanel.Controls.Add(timeLabel);


            // hour box
            hourBox = new ComboBox
            {
                Location = new Point(70, 485),
                Size = new Size(60, 60),
                Font = new Font("Comic Sans MS", 15),
                DropDownStyle = ComboBoxStyle.DropDownList,

            };
            windowSetup.reminderPanel.Controls.Add(hourBox);

            for (int i = 1; i < 13; i++)
            {
                hourBox.Items.Add(i.ToString("D2"));
            }

            hourBox.SelectedItem = "12";

            // colonLabel
            colonLabel = new Label
            {
                Font = new Font("Bradley Hand ITC", 40, FontStyle.Bold),
                Text = ":",
                Location = new Point(130, 460),
                Size = new Size(30, 70)
            };
            windowSetup.reminderPanel.Controls.Add(colonLabel);

            //  minute box

            minuteBox = new ComboBox
            {
                Location = new Point(175, 485),
                Size = new Size(60, 60),
                Font = new Font("Comic Sans MS", 15),
                DropDownStyle = ComboBoxStyle.DropDownList,
            };
            windowSetup.reminderPanel.Controls.Add(minuteBox);

            for (int i = 0; i < 60; i++)
            {
                minuteBox.Items.Add(i.ToString("D2"));
            }

            minuteBox.SelectedItem = "00";

            // am/pm box
            apmBox = new ComboBox
            {
                Location = new Point(265, 485),
                Size = new Size(80, 60),
                Font = new Font("Comic Sans MS", 15),
                DropDownStyle = ComboBoxStyle.DropDownList,


            };
            apmBox.Items.Add("AM");
            apmBox.Items.Add("PM");
            apmBox.SelectedItem = "PM";
            windowSetup.reminderPanel.Controls.Add(apmBox);
        }

        // getting selected time
        public DateTime GetSelectedTime(CalendarManager calendarManager)
        {
            int hour = int.Parse(hourBox.SelectedItem.ToString());
            int minute = int.Parse(minuteBox.SelectedItem.ToString());
            string apm = apmBox.SelectedItem.ToString();

            if (apm == "PM" && hour != 12)
            {
                hour += 12;
            }
            else if (apm == "AM" && hour == 12)
            {
                hour = 0;
            }

            DateTime reminderTime = new DateTime(
                calendarManager.selectedDate.Year,
                calendarManager.selectedDate.Month,
                calendarManager.selectedDate.Day,
                hour,
                minute,
                0);

            return reminderTime;
        }

        public void Erase()
        {
            titleLabel.Visible = false;
            titleTextBox.Visible = false;
            descriptionLabel.Visible = false;
            descriptionTextBox.Visible = false;
            headerLabel.Visible = false;
            timeLabel.Visible = false;
            colonLabel.Visible = false;
            hourBox.Visible = false;
            apmBox.Visible = false;
            minuteBox.Visible = false;
        }
    }



}
