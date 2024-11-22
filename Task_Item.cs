using System;
using System.Drawing;
using System.Windows.Forms;

namespace RemindersStudyHub
{
    public class TaskItem
    {
        public int TaskId { get; set; } // Add this for DB identification
        public string TaskName { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }

        public TaskItem(string taskName, DateTime dueDate, string priority)
        {
            TaskName = taskName;
            DueDate = dueDate;
            Priority = priority;
        }

        public TaskItem(int taskId, string taskName, DateTime dueDate, string priority)
        {
            TaskId = taskId;
            TaskName = taskName;
            DueDate = dueDate;
            Priority = priority;
        }
    }

}
