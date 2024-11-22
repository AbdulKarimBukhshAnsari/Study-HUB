using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RemindersStudyHub
{
    public class CustomQueue
    {
        private LinkedList<TaskItem> taskQueue;

        public CustomQueue()
        {
            taskQueue = new LinkedList<TaskItem>();
        }

        // Add a task to the queue
        public void Enqueue(TaskItem task)
        {
            taskQueue.AddLast(task);
        }

        // Remove a specific task from the queue
        public void Remove(TaskItem task)
        {
            taskQueue.Remove(task);
        }

        // Get all tasks in the queue
        public LinkedList<TaskItem> GetAllTasks()
        {
            return taskQueue;
        }
    }
}
