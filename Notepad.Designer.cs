namespace RemindersStudyHub
{
    partial class Notepad
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            richTextBox1 = new RichTextBox();
            panel1 = new Panel();
            label1 = new Label();
            textBox1 = new TextBox();
            button9 = new Button();
            button8 = new Button();
            label3 = new Label();
            button2 = new Button();
            button7 = new Button();
            button6 = new Button();
            button4 = new Button();
            button5 = new Button();
            comboBox3 = new ComboBox();
            button3 = new Button();
            button1 = new Button();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = SystemColors.InactiveBorder;
            richTextBox1.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBox1.Location = new Point(223, 5);
            richTextBox1.Margin = new Padding(2);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(657, 466);
            richTextBox1.TabIndex = 6;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientActiveCaption;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(button9);
            panel1.Controls.Add(button8);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button7);
            panel1.Controls.Add(button6);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(comboBox3);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(comboBox2);
            panel1.Controls.Add(comboBox1);
            panel1.Location = new Point(6, 5);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(211, 466);
            panel1.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(11, 308);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(85, 19);
            label1.TabIndex = 8;
            label1.Text = "Enter Name ";
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.Menu;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Font = new Font("Trebuchet MS", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(13, 340);
            textBox1.Margin = new Padding(2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(181, 25);
            textBox1.TabIndex = 38;
            // 
            // button9
            // 
            button9.BackColor = SystemColors.GradientInactiveCaption;
            button9.FlatStyle = FlatStyle.Flat;
            button9.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button9.ForeColor = Color.Black;
            button9.Location = new Point(13, 423);
            button9.Margin = new Padding(2);
            button9.Name = "button9";
            button9.Size = new Size(180, 34);
            button9.TabIndex = 37;
            button9.Text = "LOAD NOTE";
            button9.UseVisualStyleBackColor = false;
            button9.Click += button9_Click;
            // 
            // button8
            // 
            button8.BackColor = SystemColors.GradientInactiveCaption;
            button8.FlatStyle = FlatStyle.Flat;
            button8.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button8.ForeColor = Color.Black;
            button8.Location = new Point(13, 378);
            button8.Margin = new Padding(2);
            button8.Name = "button8";
            button8.Size = new Size(180, 34);
            button8.TabIndex = 36;
            button8.Text = "SAVE NOTE";
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.Location = new Point(41, 8);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(109, 25);
            label3.TabIndex = 7;
            label3.Text = "NOTE PAD ";
            // 
            // button2
            // 
            button2.BackColor = SystemColors.GradientInactiveCaption;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.Black;
            button2.Location = new Point(117, 134);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(77, 32);
            button2.TabIndex = 33;
            button2.Text = "REDO";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button7
            // 
            button7.BackColor = SystemColors.GradientInactiveCaption;
            button7.FlatStyle = FlatStyle.Flat;
            button7.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button7.ForeColor = Color.Black;
            button7.Location = new Point(15, 218);
            button7.Margin = new Padding(2);
            button7.Name = "button7";
            button7.Size = new Size(180, 32);
            button7.TabIndex = 32;
            button7.Text = "CLEAR ";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // button6
            // 
            button6.BackColor = SystemColors.GradientInactiveCaption;
            button6.FlatStyle = FlatStyle.Flat;
            button6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button6.ForeColor = Color.Black;
            button6.Location = new Point(15, 182);
            button6.Margin = new Padding(2);
            button6.Name = "button6";
            button6.Size = new Size(45, 32);
            button6.TabIndex = 31;
            button6.Text = "B";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.GradientInactiveCaption;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            button4.ForeColor = Color.Black;
            button4.Location = new Point(149, 182);
            button4.Margin = new Padding(2);
            button4.Name = "button4";
            button4.Size = new Size(45, 32);
            button4.TabIndex = 30;
            button4.Text = "U";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackColor = SystemColors.GradientInactiveCaption;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            button5.ForeColor = Color.Black;
            button5.Location = new Point(79, 182);
            button5.Margin = new Padding(2);
            button5.Name = "button5";
            button5.Size = new Size(45, 32);
            button5.TabIndex = 29;
            button5.Text = "I";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // comboBox3
            // 
            comboBox3.BackColor = SystemColors.GradientInactiveCaption;
            comboBox3.FlatStyle = FlatStyle.Flat;
            comboBox3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox3.ForeColor = SystemColors.InactiveCaptionText;
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "Left", "Center", "Right" });
            comboBox3.Location = new Point(88, 89);
            comboBox3.Margin = new Padding(2);
            comboBox3.Name = "comboBox3";
            comboBox3.RightToLeft = RightToLeft.No;
            comboBox3.Size = new Size(107, 23);
            comboBox3.TabIndex = 27;
            comboBox3.Text = "ALIGNMENT";
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.GradientInactiveCaption;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.Black;
            button3.Location = new Point(14, 262);
            button3.Margin = new Padding(2);
            button3.Name = "button3";
            button3.Size = new Size(180, 32);
            button3.TabIndex = 26;
            button3.Text = "COUNT WORDS";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.GradientInactiveCaption;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.Black;
            button1.Location = new Point(15, 134);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(77, 32);
            button1.TabIndex = 24;
            button1.Text = "UNDO";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // comboBox2
            // 
            comboBox2.BackColor = SystemColors.GradientInactiveCaption;
            comboBox2.FlatStyle = FlatStyle.Popup;
            comboBox2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox2.ForeColor = SystemColors.InactiveCaptionText;
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Arial Rounded MT", "MS Reference Sans Serif", "Comic Sans MS", "Lucida Calligraphy", "Poor Richard", "Segoe UI", "Times New Roman" });
            comboBox2.Location = new Point(15, 49);
            comboBox2.Margin = new Padding(2);
            comboBox2.Name = "comboBox2";
            comboBox2.RightToLeft = RightToLeft.No;
            comboBox2.Size = new Size(181, 23);
            comboBox2.TabIndex = 2;
            comboBox2.Text = "FONT";
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // comboBox1
            // 
            comboBox1.BackColor = SystemColors.GradientInactiveCaption;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.ForeColor = SystemColors.InactiveCaptionText;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "8", "10", "12", "14", "16", "18", "20", "22", "24", "26", "28", "30", "32", "34", "36" });
            comboBox1.Location = new Point(15, 89);
            comboBox1.Margin = new Padding(2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(62, 23);
            comboBox1.TabIndex = 1;
            comboBox1.Text = "SIZE";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // Notepad
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(885, 477);
            Controls.Add(richTextBox1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "Notepad";
            Text = "Notepad";
            Load += Notepad_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBox1;
        private Panel panel1;
        private Label label1;
        private TextBox textBox1;
        private Button button9;
        private Button button8;
        private Label label3;
        private Button button2;
        private Button button7;
        private Button button6;
        private Button button4;
        private Button button5;
        private ComboBox comboBox3;
        private Button button3;
        private Button button1;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
    }
}