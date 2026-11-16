using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
namespace Clockv2.nn
{
    partial class ScheduleForm : Form
    {
        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ScheduleForm));
            toolStrip1 = new ToolStrip();
            loadBtn = new ToolStripButton();
            saveBtn = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripButton1 = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            splitContainer1 = new SplitContainer();
            eventsListV = new ListView();
            dateCell = new ColumnHeader();
            timeCell = new ColumnHeader();
            reminder = new ColumnHeader();
            descCell = new ColumnHeader();
            detailsSplit = new SplitContainer();
            label2 = new Label();
            reminderDSpin = new DateSpinner();
            reminderTspin = new TimeSpinner();
            isReminded = new NealsCheck();
            eventDSpin = new DateSpinner();
            eventTSpin = new TimeSpinner();
            label1 = new Label();
            descTextBox = new TextBox();
            yearLbl = new Label();
            toolStrip1.SuspendLayout();
            ((ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((ISupportInitialize)detailsSplit).BeginInit();
            detailsSplit.Panel1.SuspendLayout();
            detailsSplit.Panel2.SuspendLayout();
            detailsSplit.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Font = new Font("Courier New", 12F, FontStyle.Regular, GraphicsUnit.Point);
            toolStrip1.Items.AddRange(new ToolStripItem[] { loadBtn, saveBtn, toolStripSeparator1, toolStripButton1 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1155, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // loadBtn
            // 
            loadBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            loadBtn.Image = (Image)resources.GetObject("loadBtn.Image");
            loadBtn.ImageTransparentColor = Color.Magenta;
            loadBtn.Name = "loadBtn";
            loadBtn.Size = new Size(52, 22);
            loadBtn.Text = "&Load";
            loadBtn.Click += loadBtn_Click;
            // 
            // saveBtn
            // 
            saveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            saveBtn.Image = (Image)resources.GetObject("saveBtn.Image");
            saveBtn.ImageTransparentColor = Color.Magenta;
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(52, 22);
            saveBtn.Text = "&Save";
            saveBtn.Click += saveBtn_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(42, 22);
            toolStripButton1.Text = "Add";
            toolStripButton1.Click += addButton_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new Point(0, 397);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1155, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 25);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(eventsListV);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(detailsSplit);
            splitContainer1.Size = new Size(1155, 372);
            splitContainer1.SplitterDistance = 717;
            splitContainer1.TabIndex = 2;
            // 
            // eventsListV
            // 
            eventsListV.Columns.AddRange(new ColumnHeader[] { dateCell, timeCell, reminder, descCell });
            eventsListV.Dock = DockStyle.Fill;
            eventsListV.Font = new Font("Courier New", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            eventsListV.FullRowSelect = true;
            eventsListV.GridLines = true;
            eventsListV.Location = new Point(0, 0);
            eventsListV.MultiSelect = false;
            eventsListV.Name = "eventsListV";
            eventsListV.Size = new Size(717, 372);
            eventsListV.TabIndex = 0;
            eventsListV.UseCompatibleStateImageBehavior = false;
            eventsListV.View = View.Details;
            eventsListV.SelectedIndexChanged += eventsListV_SelectedIndexChanged;
            // 
            // dateCell
            // 
            dateCell.Text = "Date";
            dateCell.Width = 75;
            // 
            // timeCell
            // 
            timeCell.Text = "Time";
            // 
            // reminder
            // 
            reminder.Text = "Reminder";
            reminder.Width = 120;
            // 
            // descCell
            // 
            descCell.Text = "Desc";
            descCell.Width = 450;
            // 
            // detailsSplit
            // 
            detailsSplit.Dock = DockStyle.Fill;
            detailsSplit.Location = new Point(0, 0);
            detailsSplit.Name = "detailsSplit";
            detailsSplit.Orientation = Orientation.Horizontal;
            // 
            // detailsSplit.Panel1
            // 
            detailsSplit.Panel1.Controls.Add(yearLbl);
            detailsSplit.Panel1.Controls.Add(label2);
            detailsSplit.Panel1.Controls.Add(reminderDSpin);
            detailsSplit.Panel1.Controls.Add(reminderTspin);
            detailsSplit.Panel1.Controls.Add(isReminded);
            detailsSplit.Panel1.Controls.Add(eventDSpin);
            detailsSplit.Panel1.Controls.Add(eventTSpin);
            detailsSplit.Panel1.Controls.Add(label1);
            // 
            // detailsSplit.Panel2
            // 
            detailsSplit.Panel2.Controls.Add(descTextBox);
            detailsSplit.Size = new Size(434, 372);
            detailsSplit.SplitterDistance = 82;
            detailsSplit.TabIndex = 0;
            // 
            // label2
            // 
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(160, 25);
            label2.TabIndex = 6;
            label2.Text = "Date       Time";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // reminderDSpin
            // 
            reminderDSpin.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            reminderDSpin.BackColor = SystemColors.ButtonHighlight;
            reminderDSpin.BorderStyle = BorderStyle.FixedSingle;
            reminderDSpin.DateString = "11-01-2000";
            reminderDSpin.Font = new Font("Segoe UI Symbol", 48F, FontStyle.Regular, GraphicsUnit.Point);
            reminderDSpin.Location = new Point(238, 32);
            reminderDSpin.Margin = new Padding(0);
            reminderDSpin.Name = "reminderDSpin";
            reminderDSpin.Size = new Size(96, 50);
            reminderDSpin.TabIndex = 3;
            reminderDSpin.DateSpinChanged += remDSpin_DateChanged;
            // 
            // reminderTspin
            // 
            reminderTspin.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            reminderTspin.BackColor = SystemColors.ButtonHighlight;
            reminderTspin.BorderStyle = BorderStyle.FixedSingle;
            reminderTspin.Font = new Font("Segoe UI Symbol", 48F, FontStyle.Regular, GraphicsUnit.Point);
            reminderTspin.Location = new Point(334, 31);
            reminderTspin.Margin = new Padding(0);
            reminderTspin.Name = "reminderTspin";
            reminderTspin.Size = new Size(96, 50);
            reminderTspin.sTime = "00:00";
            reminderTspin.TabIndex = 4;
            reminderTspin.SpinnerSpanChanged += reminderTspin_SpinnerSpanChanged;
            // 
            // isReminded
            // 
            isReminded.Location = new Point(373, 0);
            isReminded.Name = "isReminded";
            isReminded.Size = new Size(25, 20);
            isReminded.TabIndex = 0;
            isReminded.CheckedChanged += isReminded_CheckedChanged;
            isReminded.Click += isReminded_Click;
            // 
            // eventDSpin
            // 
            eventDSpin.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            eventDSpin.BackColor = SystemColors.ButtonHighlight;
            eventDSpin.BorderStyle = BorderStyle.FixedSingle;
            eventDSpin.DateString = "11-01-2000";
            eventDSpin.Font = new Font("Segoe UI Symbol", 48F, FontStyle.Regular, GraphicsUnit.Point);
            eventDSpin.Location = new Point(0, 31);
            eventDSpin.Margin = new Padding(0);
            eventDSpin.Name = "eventDSpin";
            eventDSpin.Size = new Size(96, 50);
            eventDSpin.TabIndex = 1;
            eventDSpin.DateSpinChanged += eventDateSpinChanged;
            // 
            // eventTSpin
            // 
            eventTSpin.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            eventTSpin.BackColor = SystemColors.ButtonHighlight;
            eventTSpin.BorderStyle = BorderStyle.FixedSingle;
            eventTSpin.Font = new Font("Segoe UI Symbol", 48F, FontStyle.Regular, GraphicsUnit.Point);
            eventTSpin.Location = new Point(96, 32);
            eventTSpin.Margin = new Padding(0);
            eventTSpin.Name = "eventTSpin";
            eventTSpin.Size = new Size(96, 50);
            eventTSpin.sTime = "00:00";
            eventTSpin.TabIndex = 2;
            eventTSpin.SpinnerSpanChanged += eventTSpin_SpinnerSpanChanged;
            // 
            // label1
            // 
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Location = new Point(260, 0);
            label1.Name = "label1";
            label1.Size = new Size(170, 25);
            label1.TabIndex = 5;
            label1.Text = "Reminder?";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // descTextBox
            // 
            descTextBox.Dock = DockStyle.Fill;
            descTextBox.Font = new Font("Courier New", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            descTextBox.Location = new Point(0, 0);
            descTextBox.Multiline = true;
            descTextBox.Name = "descTextBox";
            descTextBox.Size = new Size(434, 286);
            descTextBox.TabIndex = 0;
            descTextBox.TextChanged += descTextBox_TextChanged;
            // 
            // yearLbl
            // 
            yearLbl.BorderStyle = BorderStyle.FixedSingle;
            yearLbl.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point);
            yearLbl.Location = new Point(166, 0);
            yearLbl.Name = "yearLbl";
            yearLbl.Size = new Size(88, 32);
            yearLbl.TabIndex = 7;
            yearLbl.Text = "2024";
            yearLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ScheduleForm
            // 
            ClientSize = new Size(1155, 419);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Name = "ScheduleForm";
            Load += ScheduleForm_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            detailsSplit.Panel1.ResumeLayout(false);
            detailsSplit.Panel2.ResumeLayout(false);
            detailsSplit.Panel2.PerformLayout();
            ((ISupportInitialize)detailsSplit).EndInit();
            detailsSplit.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private DateSpinner eventDSpin;
        private TimeSpinner eventTSpin;
        private DateSpinner reminderDSpin;
        private TimeSpinner reminderTspin;
        private ToolStrip toolStrip1;
        private StatusStrip statusStrip1;
        private SplitContainer splitContainer1;
        private ListView eventsListV;
        private ColumnHeader dateCell;
        private ColumnHeader descCell;
        private SplitContainer detailsSplit;
        private TextBox descTextBox;
        private ColumnHeader timeCell;
        private NealsCheck isReminded;
        private ToolStripButton loadBtn;
        private ToolStripButton saveBtn;
        private Label label1;
        private ColumnHeader reminder;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButton1;
        private Label label2;
        private Label yearLbl;
    }
    #endregion
}