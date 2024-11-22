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
    public partial class Notepad : Form
    {
        private Database db;
        private HistoryStack<RichTextState> undostack = new HistoryStack<RichTextState>();
        private readonly HistoryStack<RichTextState> redostack = new HistoryStack<RichTextState>();
        private int lastStringLen = 0;
        private HistoryStack<String> words = new HistoryStack<String>();
        private Functions function = new Functions();
        //private RichTextBox richtextbox1 = new RichTextBox();
        public Notepad()
        {
            InitializeComponent();
            CaptureState();
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            db = new Database("your_path_to_databsae");
            db.CreateNotesTable();
        }

        

        private void CaptureState()
        {
            RichTextState state = new RichTextState(richTextBox1.Rtf);
            undostack.Push(state);
            redostack.Clear(); // Clear redo stack after a new action
            lastStringLen = richTextBox1.Text.Length; // Update last capture length
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > lastStringLen && richTextBox1.Text.EndsWith(" "))
            {
                CaptureState();
            }
        }

        private void Notepad_Load(object sender, EventArgs e)
        {
            CaptureState();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)//font 
        {
            string text_font = comboBox2.SelectedItem.ToString();
            using (Font testFont = new Font(text_font, 12))
            {
                if (richTextBox1.SelectionFont != null)
                {
                    Font current = richTextBox1.SelectionFont;
                    richTextBox1.SelectionFont = new Font(text_font, current.Size, current.Style);
                }
                else
                {
                    MessageBox.Show("Selected font is not available on this system.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) //undo
        {
            if (undostack.Count() > 1)
            {
                RichTextState currentState = undostack.Pop();
                redostack.Push(currentState);
                RichTextState lastState = undostack.Peek();
                richTextBox1.Rtf = lastState.Rtf;
            }
            else
            {
                MessageBox.Show("No actions to undo.");
            }
        }

        private void button2_Click(object sender, EventArgs e) //redo 
        {
            if(redostack.Count() > 0)
            {
                RichTextState redoState = redostack.Pop();
                undostack.Push(redoState);
                richTextBox1.Rtf = redoState.Rtf;
            }
            else
            {
                MessageBox.Show("No actions to redo.");
            }
        }

        private void button3_Click(object sender, EventArgs e) //word count 
        {
            string content = richTextBox1.Text;
            int wordCount = function.CountWords(content, words);
            MessageBox.Show("The word count is: " + wordCount);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //
        {
            int size = int.Parse(comboBox1.SelectedItem.ToString());
            if (richTextBox1.SelectionFont != null)
            {
                Font current = richTextBox1.SelectionFont;
                richTextBox1.SelectionFont = new Font(current.FontFamily, size, current.Style);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) // alignment 
        {
            string align = comboBox3.SelectedItem.ToString();
            if (align == "Left")
            {
                richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            }
            else if (align == "Right")
            {
                richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
            }
            else if (align == "Center")
            {
                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            }
        }

        private void button6_Click(object sender, EventArgs e) // bold 
        {
            Font currentFont = richTextBox1.SelectionFont;
            FontStyle newFontStyle;

            if (currentFont.Bold)
            {
                newFontStyle = currentFont.Style & ~FontStyle.Bold;
            }
            else
            {
                newFontStyle = currentFont.Style | FontStyle.Bold;
            }
            richTextBox1.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
        }

        private void button5_Click(object sender, EventArgs e) // italic 
        {
            Font currentFont = richTextBox1.SelectionFont;
            FontStyle newFontStyle;

            if (currentFont.Italic)
            {
                newFontStyle = currentFont.Style & ~FontStyle.Italic;
            }
            else
            {
                newFontStyle = currentFont.Style | FontStyle.Italic;
            }
            richTextBox1.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
        }

        private void button4_Click(object sender, EventArgs e) // underline 
        {
            Font currentFont = richTextBox1.SelectionFont;
            FontStyle newFontStyle;

            if (currentFont.Underline)
            {
                newFontStyle = currentFont.Style & ~FontStyle.Underline;
            }
            else
            {
                newFontStyle = currentFont.Style | FontStyle.Underline;
            }
            richTextBox1.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
        }

        private void button7_Click(object sender, EventArgs e) // size
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                Font newFont = new Font(currentFont.FontFamily, currentFont.Size, FontStyle.Regular);

                richTextBox1.SelectionFont = newFont;
            }
        }

        private void button8_Click(object sender, EventArgs e) // save 
        {
            string noteName = textBox1.Text;
            string noteContent = richTextBox1.Rtf;

            if (!string.IsNullOrEmpty(noteName) && !string.IsNullOrEmpty(noteContent))
            {
                db.SaveNote(noteName, noteContent);
                MessageBox.Show("Note saved successfully.");

            }
            else
            {
                MessageBox.Show("Please provide a name and content for the note.");
            }
        }

        private void button9_Click(object sender, EventArgs e) //load 
        {
            string noteName = textBox1.Text;  // TextBox for note name

            if (!string.IsNullOrEmpty(noteName))
            {
                string noteContent = db.GetNoteContentByName(noteName);

                if (!string.IsNullOrEmpty(noteContent))
                {
                    richTextBox1.Rtf = noteContent;
                    MessageBox.Show("Note opened successfully.");
                }
                else
                {
                    MessageBox.Show("Note not found. Please check the name.");
                }
            }
            else
            {
                MessageBox.Show("Please enter the name of the note.");
            }
        }
    }
}
