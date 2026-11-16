namespace Clockv2.nn
{
    partial class ValueEditor
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
            ValueTBox = new TextBox();
            label1 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // ValueTBox
            // 
            ValueTBox.Dock = DockStyle.Fill;
            ValueTBox.Font = new Font("Courier New", 18F, FontStyle.Bold, GraphicsUnit.Point);
            ValueTBox.Location = new Point(0, 29);
            ValueTBox.Multiline = true;
            ValueTBox.Name = "ValueTBox";
            ValueTBox.ScrollBars = ScrollBars.Both;
            ValueTBox.Size = new Size(320, 68);
            ValueTBox.TabIndex = 0;
            ValueTBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Courier New", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(320, 29);
            label1.TabIndex = 1;
            label1.Text = "Edit Value";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Bottom;
            button1.Location = new Point(0, 97);
            button1.Name = "button1";
            button1.Size = new Size(320, 23);
            button1.TabIndex = 2;
            button1.Text = "&OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ValueEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(320, 120);
            Controls.Add(ValueTBox);
            Controls.Add(button1);
            Controls.Add(label1);
            Name = "ValueEditor";
            Text = "ValueEditor";
            Load += ValueEditor_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Button button1;
        public TextBox ValueTBox;
    }
}