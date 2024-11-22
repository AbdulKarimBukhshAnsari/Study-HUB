using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemindersStudyHub
{
    public static class Settings
    {
        public static void Apply(Form form)
        {
            form.Size = new Size(906, 513);
            form.StartPosition = FormStartPosition.Manual; // Manual position
            form.Location = new Point(350, 65);
            form.BackgroundImageLayout = ImageLayout.Tile;
        }
    }

    public class HistoryStack<T>
    {
        private List<T> Items;
        public HistoryStack()
        {
            Items = new List<T>();
        }
        public void Push(T item) //push items into stack 
        {
            Items.Add(item);
        }
        public T Pop() // remove items from stack 
        {
            if (Items.Count > 0)
            {
                T lastItem = Items[Items.Count - 1];
                Items.RemoveAt(Items.Count - 1);
                return lastItem;
            }
            else
            {
                throw new InvalidOperationException("Stack is empty.");
            }
        }
        public T Peek() // get the last added item from stack 
        {
            if (Items.Count > 0)
            {
                return Items[Items.Count - 1];
            }
            else
            {
                throw new InvalidOperationException("Stack is empty.");
            }
        }
        public int Count() // count items in stack 
        {
            return Items.Count;
        }       
        public bool CanUndo() // check if we can undo 
        {
            return Items.Count > 1; 
        }        
        public bool CanRedo() // check if we can redo 
        {
            return Items.Count > 0; 
        }
        public void Clear() // clear the stack, used to clear redo stack
        {
            Items.Clear();
        }
    }

    public class RichTextState
    {
        public string Rtf { get; set; }

        public RichTextState(string rtf)
        {
            Rtf = rtf;
        }

        
    }
    public class Functions
    {
        public int CountWords(string content, HistoryStack<string> words) // word count
        {
            string[] wordArray = content.Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in wordArray)
            {
                words.Push(word);
            }

            return words.Count();
        }
       
    }

    



}
