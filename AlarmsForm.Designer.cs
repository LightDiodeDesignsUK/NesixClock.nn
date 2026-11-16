namespace Clockv2.nn
{
    partial class AlarmsForm
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
            topPanel = new Panel();
            button1 = new Button();
            writeAlarmsBtn = new Button();
            onTitle = new Label();
            daysTitle = new Label();
            descTitle = new Label();
            timeTitle = new Label();
            xTitle = new Button();
            bottomPanel = new Panel();
            topPanel.SuspendLayout();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BorderStyle = BorderStyle.FixedSingle;
            topPanel.Controls.Add(button1);
            topPanel.Controls.Add(writeAlarmsBtn);
            topPanel.Controls.Add(onTitle);
            topPanel.Controls.Add(daysTitle);
            topPanel.Controls.Add(descTitle);
            topPanel.Controls.Add(timeTitle);
            topPanel.Controls.Add(xTitle);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(929, 36);
            topPanel.TabIndex = 8;
            // 
            // button1
            // 
            button1.Font = new Font("Courier New", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(848, 1);
            button1.Margin = new Padding(6, 5, 6, 5);
            button1.Name = "button1";
            button1.Size = new Size(27, 32);
            button1.TabIndex = 15;
            button1.Text = "+";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // writeAlarmsBtn
            // 
            writeAlarmsBtn.Font = new Font("Courier New", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            writeAlarmsBtn.Location = new Point(876, -1);
            writeAlarmsBtn.Margin = new Padding(6, 5, 6, 0);
            writeAlarmsBtn.Name = "writeAlarmsBtn";
            writeAlarmsBtn.Size = new Size(45, 32);
            writeAlarmsBtn.TabIndex = 14;
            writeAlarmsBtn.Text = "&Save";
            writeAlarmsBtn.UseVisualStyleBackColor = true;
            writeAlarmsBtn.Click += writeAlarmsBtn_Click;
            // 
            // onTitle
            // 
            onTitle.BorderStyle = BorderStyle.FixedSingle;
            onTitle.Font = new Font("Courier New", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            onTitle.Location = new Point(763, 0);
            onTitle.Margin = new Padding(6, 0, 6, 5);
            onTitle.Name = "onTitle";
            onTitle.Size = new Size(40, 32);
            onTitle.TabIndex = 13;
            onTitle.Text = "O";
            onTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // daysTitle
            // 
            daysTitle.BorderStyle = BorderStyle.FixedSingle;
            daysTitle.Font = new Font("Courier New", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            daysTitle.Location = new Point(532, 0);
            daysTitle.Margin = new Padding(6, 5, 6, 5);
            daysTitle.Name = "daysTitle";
            daysTitle.Size = new Size(233, 32);
            daysTitle.TabIndex = 12;
            daysTitle.Text = "Days";
            daysTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // descTitle
            // 
            descTitle.BorderStyle = BorderStyle.FixedSingle;
            descTitle.Font = new Font("Courier New", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            descTitle.Location = new Point(111, 0);
            descTitle.Margin = new Padding(6, 5, 6, 5);
            descTitle.Name = "descTitle";
            descTitle.Size = new Size(426, 32);
            descTitle.TabIndex = 10;
            descTitle.Text = "Description";
            descTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // timeTitle
            // 
            timeTitle.BorderStyle = BorderStyle.FixedSingle;
            timeTitle.Font = new Font("Courier New", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            timeTitle.Location = new Point(0, 0);
            timeTitle.Margin = new Padding(6, 5, 6, 5);
            timeTitle.Name = "timeTitle";
            timeTitle.Size = new Size(113, 32);
            timeTitle.TabIndex = 9;
            timeTitle.Text = "Time";
            timeTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // xTitle
            // 
            xTitle.Font = new Font("Courier New", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            xTitle.Location = new Point(799, 0);
            xTitle.Margin = new Padding(6, 5, 6, 5);
            xTitle.Name = "xTitle";
            xTitle.Size = new Size(37, 32);
            xTitle.TabIndex = 8;
            xTitle.Text = "X";
            xTitle.UseVisualStyleBackColor = true;
            // 
            // bottomPanel
            // 
            bottomPanel.BorderStyle = BorderStyle.FixedSingle;
            bottomPanel.Dock = DockStyle.Fill;
            bottomPanel.Location = new Point(0, 36);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(929, 240);
            bottomPanel.TabIndex = 9;
            bottomPanel.Scroll += bottomPanel_Scroll;
            bottomPanel.Resize += bottomPanel_Resize;
            // 
            // AlarmsForm
            // 
            AutoScaleDimensions = new SizeF(13F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(929, 276);
            Controls.Add(bottomPanel);
            Controls.Add(topPanel);
            Font = new Font("Courier New", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            Margin = new Padding(6, 5, 6, 5);
            Name = "AlarmsForm";
            Text = "Alarms";
            Load += AlarmsForm_Load;
            Resize += AlarmsForm_Resize;
            topPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel topPanel;
        private Button writeAlarmsBtn;
        private Label onTitle;
        private Label daysTitle;
        private Label descTitle;
        private Label timeTitle;
        private Button xTitle;
        private Panel bottomPanel;
        private Button button1;
    }
}