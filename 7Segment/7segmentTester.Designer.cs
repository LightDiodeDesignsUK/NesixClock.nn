namespace Clockv2.nn
{
    partial class _7segmentTester
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
            segmentClockCtl1 = new SegmentClockCtl(new NESixClock());
            SuspendLayout();
            // 
            // segmentClockCtl1
            // 
            segmentClockCtl1.BorderStyle = BorderStyle.FixedSingle;
            segmentClockCtl1.Dock = DockStyle.Fill;
            segmentClockCtl1.IsClockAutoShow = true;
            segmentClockCtl1.Location = new Point(0, 0);
            segmentClockCtl1.Name = "segmentClockCtl1";
            segmentClockCtl1.Size = new Size(1126, 536);
            segmentClockCtl1.TabIndex = 0;
            segmentClockCtl1.Load += segmentClockCtl1_Load;
            segmentClockCtl1.DoubleClick += segmentClockCtl1_DoubleClick;
            // 
            // _7segmentTester
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1126, 536);
            ControlBox = false;
            Controls.Add(segmentClockCtl1);
            Name = "_7segmentTester";
            Load += _7segmentTester_Load;
            ResumeLayout(false);
        }

        #endregion

        private SegmentClockCtl segmentClockCtl1;
    }
}