namespace Clockv2.nn
{
    partial class TimeSpinner
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
            hr1Up = new Button();
            hr2up = new Button();
            minU2 = new Button();
            minU1 = new Button();
            hr = new Label();
            min = new Label();
            m2D = new Button();
            m1D = new Button();
            hr2D = new Button();
            hr1D = new Button();
            SuspendLayout();
            // 
            // hr1Up
            // 
            hr1Up.Font = new Font("Dubai", 9F, FontStyle.Bold, GraphicsUnit.Point);
            hr1Up.Location = new Point(6, 0);
            hr1Up.Margin = new Padding(0);
            hr1Up.Name = "hr1Up";
            hr1Up.Size = new Size(16, 17);
            hr1Up.TabIndex = 0;
            hr1Up.Text = "^";
            hr1Up.TextAlign = ContentAlignment.TopCenter;
            hr1Up.UseVisualStyleBackColor = true;
            hr1Up.Click += hr1Up_Click;
            // 
            // hr2up
            // 
            hr2up.Font = new Font("Dubai", 9F, FontStyle.Bold, GraphicsUnit.Point);
            hr2up.Location = new Point(28, 0);
            hr2up.Margin = new Padding(15, 17, 15, 17);
            hr2up.Name = "hr2up";
            hr2up.Size = new Size(16, 17);
            hr2up.TabIndex = 1;
            hr2up.Text = "^";
            hr2up.TextAlign = ContentAlignment.TopCenter;
            hr2up.UseVisualStyleBackColor = true;
            hr2up.Click += hr2up_Click;
            // 
            // minU2
            // 
            minU2.Font = new Font("Dubai", 9F, FontStyle.Bold, GraphicsUnit.Point);
            minU2.Location = new Point(81, 0);
            minU2.Margin = new Padding(15, 17, 15, 17);
            minU2.Name = "minU2";
            minU2.Size = new Size(16, 18);
            minU2.TabIndex = 3;
            minU2.Text = "^";
            minU2.TextAlign = ContentAlignment.TopCenter;
            minU2.UseVisualStyleBackColor = true;
            minU2.Click += minU2_Click;
            // 
            // minU1
            // 
            minU1.Font = new Font("Dubai", 9F, FontStyle.Bold, GraphicsUnit.Point);
            minU1.Location = new Point(52, 0);
            minU1.Margin = new Padding(15, 17, 15, 17);
            minU1.Name = "minU1";
            minU1.Size = new Size(16, 18);
            minU1.TabIndex = 2;
            minU1.Text = "^";
            minU1.TextAlign = ContentAlignment.TopCenter;
            minU1.UseVisualStyleBackColor = true;
            minU1.Click += minU1_Click;
            // 
            // hr
            // 
            hr.BorderStyle = BorderStyle.FixedSingle;
            hr.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point);
            hr.Location = new Point(-1, 16);
            hr.Margin = new Padding(15, 0, 15, 0);
            hr.Name = "hr";
            hr.Size = new Size(45, 18);
            hr.TabIndex = 4;
            hr.Text = "1 2";
            hr.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // min
            // 
            min.BorderStyle = BorderStyle.FixedSingle;
            min.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point);
            min.Location = new Point(50, 16);
            min.Margin = new Padding(0);
            min.Name = "min";
            min.Size = new Size(47, 18);
            min.TabIndex = 5;
            min.Text = "3 0";
            min.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // m2D
            // 
            m2D.Font = new Font("Dubai", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            m2D.Location = new Point(74, 26);
            m2D.Margin = new Padding(15, 17, 15, 17);
            m2D.Name = "m2D";
            m2D.RightToLeft = RightToLeft.Yes;
            m2D.Size = new Size(23, 23);
            m2D.TabIndex = 9;
            m2D.Text = "V";
            m2D.UseVisualStyleBackColor = true;
            m2D.Click += m2D_Click;
            // 
            // m1D
            // 
            m1D.Font = new Font("Dubai", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            m1D.Location = new Point(52, 26);
            m1D.Margin = new Padding(15, 17, 15, 17);
            m1D.Name = "m1D";
            m1D.RightToLeft = RightToLeft.Yes;
            m1D.Size = new Size(23, 23);
            m1D.TabIndex = 8;
            m1D.Text = "V";
            m1D.UseVisualStyleBackColor = true;
            m1D.Click += m1D_Click;
            // 
            // hr2D
            // 
            hr2D.Font = new Font("Dubai", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            hr2D.Location = new Point(21, 26);
            hr2D.Margin = new Padding(15, 17, 15, 17);
            hr2D.Name = "hr2D";
            hr2D.RightToLeft = RightToLeft.Yes;
            hr2D.Size = new Size(23, 23);
            hr2D.TabIndex = 7;
            hr2D.Text = "V";
            hr2D.UseVisualStyleBackColor = true;
            hr2D.Click += hr2D_Click;
            // 
            // hr1D
            // 
            hr1D.Font = new Font("Dubai", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            hr1D.Location = new Point(0, 26);
            hr1D.Margin = new Padding(15, 17, 15, 17);
            hr1D.Name = "hr1D";
            hr1D.RightToLeft = RightToLeft.Yes;
            hr1D.Size = new Size(23, 23);
            hr1D.TabIndex = 6;
            hr1D.Text = "V";
            hr1D.UseVisualStyleBackColor = true;
            hr1D.Click += hr1D_Click;
            // 
            // TimeSpinner
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = SystemColors.ButtonHighlight;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(min);
            Controls.Add(hr);
            Controls.Add(m2D);
            Controls.Add(m1D);
            Controls.Add(hr2D);
            Controls.Add(hr1D);
            Controls.Add(minU2);
            Controls.Add(minU1);
            Controls.Add(hr2up);
            Controls.Add(hr1Up);
            Font = new Font("Segoe UI Symbol", 48F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(0);
            Name = "TimeSpinner";
            Size = new Size(96, 46);
            Load += TimeSpinner_Load;
            Resize += TimeSpinner_Resize;
            ResumeLayout(false);
        }

        #endregion

        private Button hr1Up;
        private Button hr2up;
        private Button minU2;
        private Button minU1;
        private Label hr;
        private Label min;
        private Button m2D;
        private Button m1D;
        private Button hr2D;
        private Button hr1D;
    }
}
