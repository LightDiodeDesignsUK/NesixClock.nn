using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Clockv2.nn
{
    public partial class AlarmsForm : Form
    {

        public AlarmsForm()
        {
            InitializeComponent();
        }



        
        private void AlarmsForm_Load(object sender, EventArgs e)
        {
            ClockGlobals.doAlarmsLoad();
            alarmStruct[]? alarms = ClockGlobals.alarmList?.ToArray();
            bottomPanel.BorderStyle = BorderStyle.FixedSingle;
            doStartupLayout();
        }
        List<displayRecord> displayRecords = new List<displayRecord>();
        alarmStruct[]? alarms;
        
        void doRefresh()
        {
            for (int ir = 0; ir < displayRecords.Count(); ir++)
            {
                displayRecord dr = displayRecords[ir];
                try
                {
                    int? iT = (int?)((TimeSpinner)dr.TimeSpin).Tag;
                    dr.daysDays.DayMask = ClockGlobals.alarmList[(int)iT].DayMask;

                    dr.cbEnabled.Checked = ClockGlobals.alarmList[(int)iT].IsEnabled;
                    dr.cbEnabled.Refresh();

                    dr.TimeSpin.sTime = ClockGlobals.alarmList[(int)iT].AlarmTime;


                }
                catch { }
            }
        }
        void doStartupLayout()
        {

            foreach (displayRecord ds in displayRecords)
            {
                try
                {
                    bottomPanel.Controls.Remove(ds.daysDays);
                    bottomPanel.Controls.Remove(ds.TimeSpin);
                    bottomPanel.Controls.Remove(ds.lbDesc);
                    bottomPanel.Controls.Remove(ds.cbEnabled);
                    bottomPanel.Controls.Remove(ds.lbX);

                }
                catch { }
            }
            displayRecords.Clear();
            int xl1 = timeTitle.Left, xl2 = descTitle.Left, xl3 = daysTitle.Left;
            int yLbl = 2;

            for (int i = 0; i < ClockGlobals.alarmList?.Length; i++)
            {
                alarmStruct a = ClockGlobals.alarmList[i];

                displayRecord rec = new displayRecord(a);
                NealsCheck cbEnabled = new NealsCheck()
                    { Tag = i, Top = yLbl, Width = timeTitle.Width, Text = Convert.ToString(a.DayMask, 16), Height = 16, BackColor = Color.White };
                cbEnabled.Visible = true;
                cbEnabled.CheckColor = Color.Green;
                cbEnabled.NotCheckColor = Color.Red;

                //Label lblTime = new Label()
                //{ Tag = i, Top = yLbl, Left = xl1, Width = timeTitle.Width, Text = a.AlarmTime, BackColor = Color.White };

                ///////////////
                TimeSpinner timeSpin = new TimeSpinner()
                { Tag = i, Top = yLbl, Left = 0, Text = a.AlarmTime, BackColor = Color.White };
                rec.TimeSpin = timeSpin;
                string sKey = "timeSpin" + i.ToString().Trim();
                rec.TimeSpin.Name = sKey;
                rec.TimeSpin.SpinnerSpanChanged += TimeSpin_SpinnerSpanChanged;
                rec.TimeSpin.TimeTime = TimeOnly.Parse(ClockGlobals.alarmList[i].AlarmTime);
                bottomPanel.Controls.Add(rec.TimeSpin);
                rec.TimeSpin.Width = timeTitle.Width;
                //bottomPanel.Controls[sKey].Height = 44;
                //rec.TimeSpin.Height = 44;

                //////////////////////
                Label lblDesc = new Label()
                { Tag = i, Top = yLbl, Left = xl2, Width = descTitle.Width, Text = a.Description, BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Height = timeSpin.Height };

                Label lbX = new Label()
                {
                    Tag = i,
                    Top = yLbl,
                    Height = xTitle.Height,
                    Left = xTitle.Left,
                    Width = xTitle.Width,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = "X",
                    ForeColor = Color.Red,
                    Font = this.Font,
                    BorderStyle = BorderStyle.FixedSingle
                };
                lbX.Click += LbX_Click;
                rec.lbX = lbX;

                DaysControl daysControl = new DaysControl();
                daysControl.Tag = i;
                daysControl.BorderStyle = BorderStyle.FixedSingle;
                daysControl.Top = yLbl;
                daysControl.AutoScaleMode = AutoScaleMode.None;
                daysControl.AutoSize = false;
                daysControl.AutoSize2 = true;
                daysControl.Left = xl3;
                daysControl.AutoScaleMode = AutoScaleMode.None;
                daysControl.Width = daysTitle.Width;
                
                daysControl.MinimumSize = new Size(daysTitle.Size.Width, timeSpin.Height);
                daysControl.MaximumSize = new Size(daysTitle.Size.Width, timeSpin.Height);
                daysControl.Width = daysTitle.Width;
                daysControl.Height = timeSpin.Height;
                daysControl.Text = a.ShortDaysString;
                daysControl.BackColor = Color.White;
                daysControl.DayMask = a.DayMask;
                daysControl.AutoSize2 = true;
                
                daysControl.AutoScaleMode = AutoScaleMode.None; daysControl.AutoSize = false;
                

                daysControl.OnChanged += DaysControl_OnChanged;


                //  lblTime.Click += LblTime_Click;
                lblDesc.Click += LblDesc_Click;
                
                

                //bottomPanel.Controls.Add(rec.lbTime);



                ////////////////////////////////////
                rec.daysDays = daysControl;
                //rec.daysDays.Width = 86;// daysTitle.Width;
                //rec.daysDays.Width = 86;// daysTitle.Width;
                rec.daysDays.Left = daysTitle.Left;
                rec.daysDays.AutoSize2 = true;
                rec.daysDays.Height = rec.TimeSpin.Height;



                bottomPanel.Controls.Add(rec.daysDays);

                ////////////////////////////////

                rec.cbEnabled = cbEnabled;
                rec.cbEnabled.Left = onTitle.Left;
                rec.cbEnabled.Width = onTitle.Width;
                rec.cbEnabled.Resize += cbEnabled.OnLoad;
                rec.cbEnabled.Height = onTitle.Height;
                rec.cbEnabled.AutoCheck = true;
                rec.cbEnabled.CheckedChanged += CbEnabled_Changed;
                rec.cbEnabled.Checked = ClockGlobals.alarmList[i].IsEnabled;



                rec.cbEnabled = cbEnabled;
                rec.lbDesc = lblDesc;
                rec.daysDays = daysControl;
                //rec.lbTime = lblTime;
                displayRecords.Add(rec);

                ////////////
                /////////////

                rec.lbX = lbX;
                bottomPanel.AutoSize = false;
                bottomPanel.Controls.Add(rec.daysDays);
                bottomPanel.Controls.Add(rec.cbEnabled);
                //rec.TimeSpin.Height = 44;
                bottomPanel.Controls.Add(rec.lbDesc);
                bottomPanel.Controls.Add(rec.lbX);
                bottomPanel.Height = rec.lbDesc.Bottom + 32;
                rec.cbEnabled.BringToFront();
                rec.daysDays.Invalidate();
                //////////////////
                /////////////////////

                yLbl += (timeSpin.Height) + 2;

            }
            bottomPanel.AutoScroll = true;
            doRefresh();
            bottomPanel.Invalidate();

        }

        private void TimeSpin_SpinnerSpanChanged(object sender, TimeOnly newValue)
        {
            int iTag = (int)((TimeSpinner)sender).Tag;
            ClockGlobals.alarmList[iTag].AlarmTime = newValue.ToString("HH:mm");
        }

        private void LbX_Click(object? sender, EventArgs e)
        {
            int tag = (int)((Control)sender).Tag;
            List<alarmStruct> tmpList = new List<alarmStruct>();
            int iNumAfter = ClockGlobals.alarmList.Length - tag;
            tmpList.AddRange(new ArraySegment<alarmStruct>(ClockGlobals.alarmList, 0, tag));
            try
            {
                tmpList.AddRange(new ArraySegment<alarmStruct>(ClockGlobals.alarmList, tag + 1, iNumAfter - 1));
            }
            catch (Exception ex) { }
            ClockGlobals.alarmList = tmpList.ToArray();
            doStartupLayout();
        }

        private void CbEnabled_Changed(object? sender, EventArgs e)
        {
            int tag = (int)((NealsCheck)sender).Tag;
            ClockGlobals.alarmList[tag].IsEnabled = !ClockGlobals.alarmList[tag].IsEnabled;
            ClockGlobals.alarmList[tag].lastTriggered = DateTime.FromBinary(0);
            doRefresh();
        }

        private void DaysControl_OnChanged(object sender, byte newMask)
        {
            int tag = (int)((DaysControl)sender).Tag;
            ClockGlobals.alarmList[tag].DayMask = newMask;
            string sDays = ClockGlobals.alarmList[tag].ShortDaysString;
            ((DaysControl)sender).DayMask = newMask;
            displayRecords[tag].cbEnabled.Text = Convert.ToString(newMask, 16);
        }

        private void LblDesc_Click(object? sender, EventArgs e)
        {
            string sText = ((Label)sender).Text;
            int iAlarm = (int)((Label)sender).Tag;
            ValueEditor vl = new ValueEditor(sText)
            {
                LabelValue = "Description"
            };
            vl.ShowDialog();
            ((Label)sender).Text = vl.ValueTBox.Text;
            int recNo = (int)((Label)sender).Tag;

            ClockGlobals.alarmList[recNo].Description = vl.ValueTBox.Text;

            vl.Close();
        }



        alarmStruct currentRecordAlarm;
        private void LblTime_Click(object? sender, EventArgs e)
        {
            string sText = ((Label)sender).Text;
            int iAlarm = (int)((Label)sender).Tag;
            ValueEditor vl = new ValueEditor(sText)
            {
                LabelValue = "TIME"
            };
            vl.ShowDialog();
            ((Label)sender).Text = vl.ValueTBox.Text;
            int recNo = (int)((Label)sender).Tag;

            ClockGlobals.alarmList[recNo].AlarmTime = vl.ValueTBox.Text;

            vl.Close();
        }



        private void writeAlarmsBtn_Click(object sender, EventArgs e)
        {
            //ClockGlobals.alarmList[1].Enabled = true;
            string sAlarms = ClockGlobals.AlarmsToString();
            ClockSettings.Default.AlarmTimes = sAlarms;
            ClockSettings.Default.Save();
        }

        private void bottomPanel_Scroll(object sender, ScrollEventArgs e)
        {
            bottomPanel.Refresh();
        }

        private void AlarmsForm_Resize(object sender, EventArgs e)
        {
            doRefresh();
        }

        private void bottomPanel_Resize(object sender, EventArgs e)
        {

        }
        private void addToAlarms(alarmStruct ass)
        {
            List<alarmStruct> tmp1 = ClockGlobals.alarmList.ToList();
            tmp1.Add(ass);
            ClockGlobals.alarmList = tmp1.ToArray();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sAlarm = (DateTime.Now.AddSeconds(10)).ToShortTimeString();
            addToAlarms(new alarmStruct()
            {
                AlarmTime = sAlarm,
                DayMask = 127,
                Description = "NEW ALARM\r\n" + sAlarm,
                IsEnabled = false,
                lastTriggered = DateTime.FromBinary(0)
            });

            doStartupLayout();
            bottomPanel.ScrollControlIntoView(displayRecords.Last<displayRecord>().TimeSpin);

        }

    }
    class displayRecord
    {
        public displayRecord(alarmStruct a)
        {
            alarm = a;
        }
        public TimeSpinner TimeSpin;
        public alarmStruct? alarm;
        //public Label? lbTime, lbDesc;
        public Label? lbDesc;
        public DaysControl daysDays;
        public NealsCheck cbEnabled;
        public Label lbX;
    }
}
