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

namespace RemindersStudyHub
{
    public partial class HomePage : Form
    {
        private Panel imagePanel;
        private Button b1, b2, b3, b4;
        public HomePage()
        {
            InitializeComponent();
            this.BackColor = SystemColors.GradientInactiveCaption;
            this.Height = 600;
            this.Width = 890;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // initalizing image panel
            imagePanel = new Panel
            {
                Bounds = new Rectangle(38, 30, 800, 501),
            };

            Bitmap ogImage = new Bitmap("home.jpeg");
            Bitmap resizedImage = new Bitmap(ogImage, imagePanel.ClientSize);
            imagePanel.BackgroundImage = resizedImage;
            imagePanel.BackgroundImageLayout = ImageLayout.Center;
            this.Controls.Add(imagePanel);

            Font setfont = new Font("Trebuchet MS", 13, FontStyle.Bold);
            Size setsize = new Size(285, 80);
            int x1 = 106;
            int x2 = 416;
            int y1 = 208;
            int y2 = 328;

            b1 = new Button
            {
                Text = "NOTEPAD",
                Font = setfont,
                Size = setsize,
                Location = new Point (x1, y1)
            };
            b1.Click += B1_Click;
            

            b2 = new Button
            {
                Text = "GRADE TRACKER",
                Font = setfont,
                Size = setsize,
                Location = new Point(x2, y1)
            };
            b2.Click += B2_Click;

            b3 = new Button
            {
                Text = "REMINDERS",
                Font =setfont,
                Size = setsize,
                Location = new Point(x1,y2)
            };
            b3.Click += B3_Click;

            b4 = new Button
            {
                Font = setfont,
                Text = "TO-DO LIST",
                Size = setsize,
                Location = new Point(x2, y2)
            };

            b4.Click += B4_Click;


            imagePanel.Controls.Add(b1);
            imagePanel.Controls.Add(b2);
            imagePanel.Controls.Add(b3);
            imagePanel.Controls.Add(b4);
        }

        private void B4_Click(object? sender, EventArgs e)
        {
            var nextForm = new ToDo();
            nextForm.Show();
        }

        private void B3_Click(object? sender, EventArgs e)
        {
            var nextForm = new StartPage();
            nextForm.Show();
        }

        private void B2_Click(object? sender, EventArgs e)
        {
            var nextForm = new GradeTracker();
            nextForm.Show();
        }

        private void B1_Click(object? sender, EventArgs e)
        {
            var nextForm = new Notepad();
            nextForm.Show();
            
        }

        
    }
}
