using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace RemindersStudyHub
{
    public partial class LoginPage : Form
    {
        public Panel imagePanel;
        public TextBox idBox, passBox;
        public Label welcome, idLabel, passLabel;
        public Button loginButton;
        private Database db;

        public LoginPage()
        {
            InitializeComponent();
            this.BackColor = SystemColors.GradientInactiveCaption;
            this.Height = 550;
            this.Width = 860;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            db = new Database("your_path_to_databsae");
            db.CreateUserTable();

            // initalizing image panel
            imagePanel = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                Bounds = new Rectangle(-5, -5, 485, 540),
            };

            Bitmap ogImage = new Bitmap("login1.png");
            Bitmap resizedImage = new Bitmap(ogImage, imagePanel.ClientSize);
            imagePanel.BackgroundImage = resizedImage;
            imagePanel.BackgroundImageLayout = ImageLayout.Center;
            this.Controls.Add(imagePanel);

            welcome = new Label
            {
                Text = "WELCOME",
                Font = new Font("Segoe UI", 30),
                Size = new Size(353, 45),
                Location = new Point(565, 55)
            };
            this.Controls.Add(welcome);

            idLabel = new Label
            {
                Text = "LOGIN ID",
                Font = new Font("Segoe UI", 12),
                Size = new Size(230, 30),
                Location = new Point(505, 145)
            };
            this.Controls.Add(idLabel);

            passLabel = new Label
            {
                Text = "PASSWORD",
                Font = new Font("Segoe UI", 12),
                Size = new Size(230, 30),
                Location = new Point(505, 245)
            };
            this.Controls.Add(passLabel);

            idBox = new TextBox
            {
                Size = new Size(305, 41),
                Location = new Point(508, 175),
                PlaceholderText = "Enter ID",
                Font = new Font("Segoe UI", 13),
            };

            passBox = new TextBox
            {
                Size = new Size(305, 41),
                Location = new Point(508, 275),
                PlaceholderText = "Enter Password",
                Font = new Font("Segoe UI", 13),
            };

            loginButton = new Button()
            {
                BackColor = Color.White,
                Size = new Size(85, 42),
                Location = new Point(620, 352),
                Font = new Font("Segoe UI", 15),
                Text = "LOGIN"
            };

            loginButton.Click += LoginButton_Click;



            this.Controls.Add(loginButton);
            this.Controls.Add(passBox);
            this.Controls.Add(idBox);
        }

        private void LoginButton_Click(object? sender, EventArgs e)
        {
            string Name = idBox.Text;
            string Password = passBox.Text;

            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Password))
            {
                db.SaveUser(Name, Password);
                HomePage homepage = new HomePage();
                homepage.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Please provide credentials to login ");
            }
        }


    }
}
