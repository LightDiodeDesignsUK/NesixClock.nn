namespace Clockv2.nn

{
    public partial class SevenSegmentCtl
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
            SuspendLayout();
            // 
            // _7Segment
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Black;
            Margin = new Padding(4, 4, 4, 4);
            Name = "_7Segment";
            Size = new Size(541, 508);
            Load += _7Segment_Load;
            Click += _7Segment_Click;
            MouseMove += _7Segment_MouseMove;
            ResumeLayout(false);
        }

        #endregion
    }
}
