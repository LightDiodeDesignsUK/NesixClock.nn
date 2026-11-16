namespace Clockv2.nn
{
    partial class DateSpinner
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            day1Up = new Button();
            day2up = new Button();
            minU1 = new Button();
            dayTXT = new Label();
            monthTXT = new Label();
            m1D = new Button();
            day2D = new Button();
            day1D = new Button();
            SuspendLayout();
            // 
            // day1Up
            // 
            day1Up.Font = new Font("Dubai", 9F, FontStyle.Bold, GraphicsUnit.Point);
            day1Up.Location = new Point(6, 0);
            day1Up.Margin = new Padding(0);
            day1Up.Name = "day1Up";
            day1Up.Size = new Size(16, 17);
            day1Up.TabIndex = 0;
            day1Up.Text = "^";
            day1Up.TextAlign = ContentAlignment.TopCenter;
            day1Up.UseVisualStyleBackColor = true;
            day1Up.Click += day1Up_Click;
            // 
            // day2up
            // 
            day2up.Font = new Font("Dubai", 9F, FontStyle.Bold, GraphicsUnit.Point);
            day2up.Location = new Point(28, 0);
            day2up.Margin = new Padding(15, 17, 15, 17);
            day2up.Name = "day2up";
            day2up.Size = new Size(16, 17);
            day2up.TabIndex = 1;
            day2up.Text = "^";
            day2up.TextAlign = ContentAlignment.TopCenter;
            day2up.UseVisualStyleBackColor = true;
            day2up.Click += day2up_Click;
            // 
            // minU1
            // 
            minU1.Font = new Font("Dubai", 9F, FontStyle.Bold, GraphicsUnit.Point);
            minU1.Location = new Point(65, 0);
            minU1.Margin = new Padding(15, 17, 15, 17);
            minU1.Name = "minU1";
            minU1.Size = new Size(16, 18);
            minU1.TabIndex = 2;
            minU1.Text = "^";
            minU1.TextAlign = ContentAlignment.TopCenter;
            minU1.UseVisualStyleBackColor = true;
            minU1.Click += monthUp_Click;
            // 
            // dayTXT
            // 
            dayTXT.BorderStyle = BorderStyle.FixedSingle;
            dayTXT.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dayTXT.Location = new Point(-1, 16);
            dayTXT.Margin = new Padding(15, 0, 15, 0);
            dayTXT.Name = "dayTXT";
            dayTXT.Size = new Size(45, 18);
            dayTXT.TabIndex = 4;
            dayTXT.Text = "1 2";
            dayTXT.TextAlign = ContentAlignment.MiddleCenter;
            dayTXT.Click += dayTXT_Click;
            // 
            // monthTXT
            // 
            monthTXT.BorderStyle = BorderStyle.FixedSingle;
            monthTXT.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point);
            monthTXT.Location = new Point(50, 16);
            monthTXT.Margin = new Padding(0);
            monthTXT.Name = "monthTXT";
            monthTXT.Size = new Size(47, 18);
            monthTXT.TabIndex = 5;
            monthTXT.Text = "JAN";
            monthTXT.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // m1D
            // 
            m1D.Font = new Font("Dubai", 8.249999F, FontStyle.Bold, GraphicsUnit.Point);
            m1D.Location = new Point(65, 26);
            m1D.Margin = new Padding(15, 17, 15, 17);
            m1D.Name = "m1D";
            m1D.RightToLeft = RightToLeft.Yes;
            m1D.Size = new Size(16, 23);
            m1D.TabIndex = 8;
            m1D.Text = "V";
            m1D.UseVisualStyleBackColor = true;
            m1D.Click += monthDown_Click;
            // 
            // day2D
            // 
            day2D.Font = new Font("Dubai", 8.249999F, FontStyle.Bold, GraphicsUnit.Point);
            day2D.Location = new Point(21, 26);
            day2D.Margin = new Padding(15, 17, 15, 17);
            day2D.Name = "day2D";
            day2D.RightToLeft = RightToLeft.Yes;
            day2D.Size = new Size(23, 23);
            day2D.TabIndex = 7;
            day2D.Text = "V";
            day2D.UseVisualStyleBackColor = true;
            day2D.Click += day2D_Click;
            // 
            // day1D
            // 
            day1D.Font = new Font("Dubai", 8.249999F, FontStyle.Bold, GraphicsUnit.Point);
            day1D.Location = new Point(0, 26);
            day1D.Margin = new Padding(15, 17, 15, 17);
            day1D.Name = "day1D";
            day1D.RightToLeft = RightToLeft.Yes;
            day1D.Size = new Size(23, 23);
            day1D.TabIndex = 6;
            day1D.Text = "V";
            day1D.UseVisualStyleBackColor = true;
            day1D.Click += day1D_Click;
            // 
            // DateSpinner
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = SystemColors.ButtonHighlight;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(monthTXT);
            Controls.Add(dayTXT);
            Controls.Add(m1D);
            Controls.Add(day2D);
            Controls.Add(day1D);
            Controls.Add(minU1);
            Controls.Add(day2up);
            Controls.Add(day1Up);
            Font = new Font("Segoe UI Symbol", 48F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(0);
            Name = "DateSpinner";
            Size = new Size(96, 52);
            Load += DateSpinner_Load;
            Resize += DateSpinner_Resize;
            ResumeLayout(false);
        }

        #endregion

        private Button day1Up;
        private Button day2up;
        private Button minU1;
        private Label dayTXT;
        private Label monthTXT;
        private Button m1D;
        private Button day2D;
        private Button day1D;
    }
}
