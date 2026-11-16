using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clockv2.nn
{
    public partial class ScheduleForm : Form
    {

        public ScheduleForm()
        {
            this.InitializeComponent();
            //Clock.
        }


        delegate void showFrm();
        void showForm()
        {
            this.Activate();
        }

        public void ShowFormSafely()
        {
            if (this.InvokeRequired) this.Invoke(new showFrm(showForm));
            else
            {
                Application.Run(this);
            }
        }

        private void mainSplitter_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void eventsListV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventsListV.SelectedItems.Count > 0)
            {
                string s = eventsListV.SelectedItems[0].Name.ToString();
                int i = eventsListV.SelectedItems[0].Index;
                eventTSpin.TimeTime = ClockGlobals.SchedEventList[i].Time;
                eventDSpin.DateString = ClockGlobals.SchedEventList[i].DateString;
                descTextBox.Text = ClockGlobals.SchedEventList[i].Description;
                reminderDSpin.DateDate = DateOnly.Parse(ClockGlobals.SchedEventList[i].ReminderDT.ToShortDateString());
                reminderTspin.TimeTime = TimeOnly.FromDateTime(ClockGlobals.SchedEventList[i].ReminderDT);
                isReminded.Checked = ClockGlobals.SchedEventList[i].ReminderIsOn;
                yearLbl.Text = ClockGlobals.SchedEventList[i].Year.ToString();
            }
        }

        private void desc_Changed(object sender, EventArgs e)
        {
            string s = eventsListV.SelectedItems[0].Name.ToString();
            int i = eventsListV.SelectedItems[0].Index;
            ClockGlobals.SchedEventList[i].Description = descTextBox.Text;
            if (eventsListV.SelectedIndices.Count > 0)
            {
                eventsListV.Items[eventsListV.SelectedIndices[0]].SubItems["descCell"].Text = descTextBox.Text;
            }

        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            ClockGlobals.SaveCalendatEvents();
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            ClockGlobals.doSchedListLoad();

        }

        private void eventDSpin_DateChanged(object sender, DateOnly newValue)
        {
            ClockGlobals.SchedEventList[eventsListV.SelectedIndices[0]].DateString = newValue.ToShortDateString();
            doRefreshListViewSubItems(eventsListV.SelectedIndices[0]);
        }

        private void eventTspin_SpinnerChanged(object sender, TimeOnly newValue)
        {
            string s = eventsListV.SelectedItems[0].Name.ToString();
            int i = eventsListV.SelectedItems[0].Index;
            ClockGlobals.SchedEventList[i].Time = eventTSpin.TimeTime;
            doRefreshListViewSubItems(i);
        }
        void doRefreshListViewSubItems(int listIndex)
        {
            if (eventsListV.SelectedIndices.Count > 0)
            {
                eventsListV.Items[eventsListV.SelectedIndices[0]].SubItems["dateCell"].Text = ClockGlobals.SchedEventList[listIndex].DateString;
                eventsListV.Items[eventsListV.SelectedIndices[0]].SubItems["timeCell"].Text = ClockGlobals.SchedEventList[listIndex].Time.ToShortTimeString();
                eventsListV.Items[eventsListV.SelectedIndices[0]].SubItems["descCell"].Text = ClockGlobals.SchedEventList[listIndex].Description;
                eventsListV.Items[eventsListV.SelectedIndices[0]].SubItems["reminder"].Text = ClockGlobals.SchedEventList[listIndex].ReminderDateString + " " + ClockGlobals.SchedEventList[listIndex].ReminderDT.ToShortTimeString();
            }
        }

        private Button button1;

        private void eventDateSpinChanged(object sender, DateOnly newValue)
        {
            ClockGlobals.SchedEventList[eventsListV.SelectedIndices[0]].DateString = newValue.ToShortDateString();
            doRefreshListViewSubItems(eventsListV.SelectedIndices[0]);
        }

        private void ScheduleForm_Load(object sender, EventArgs e)
        {
            fSmall = new Font(this.Font.FontFamily, 7f, FontStyle.Bold);
            doRefreshListView();
        }
        Font fSmall; 
        void doRefreshListView()
        {
            if (ClockGlobals.SchedEventList == null) ClockGlobals.doSchedListLoad();
            eventsListV.Items.Clear();
            int i = 1;
            foreach (SchedEvent cEvt in ClockGlobals.SchedEventList)
            {
                ListViewItem lv = new ListViewItem();
                lv.UseItemStyleForSubItems = true;
                lv.SubItems.Clear();
                string sDate = cEvt.Day.ToString() + "/" + cEvt.Month.ToString() + "/" + cEvt.Year.ToString();
                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem(lv, cEvt.Time.ToShortTimeString())
                { Text = sDate, Name = "dateCell" };
                lv.SubItems[0] = subItem;

                string sTime = cEvt.Time.ToString();
                ListViewItem.ListViewSubItem subItem2 = new ListViewItem.ListViewSubItem(lv, sTime)
                { Text = sTime, Name = "timeCell" };
                lv.UseItemStyleForSubItems = false;
                lv.SubItems.Add(subItem2);

                string sRTime = cEvt.ReminderDT.ToString();
                ListViewItem.ListViewSubItem subItem4 = new ListViewItem.ListViewSubItem(lv, sRTime)
                { Text = sRTime, Name = "reminder" };
                lv.SubItems.Add(subItem4);

                string? sDesc = cEvt.Description;

                ListViewItem.ListViewSubItem subItem3 = new ListViewItem.ListViewSubItem(lv, sDesc)
                { Text = cEvt.Description, Name = "descCell", Font = fSmall };
                lv.SubItems.Add(subItem3);
                eventsListV.Items.Add(lv);

                eventsListV.MultiSelect = false;
                eventsListV.FullRowSelect = true;
                eventsListV.HeaderStyle = ColumnHeaderStyle.Nonclickable;
                try
                {

                    eventsListV.Items[lv.Name].Selected = true;
                }
                catch { }
                i++;

            }
        }


        private void remDSpin_DateChanged(object sender, DateOnly newValue)
        {
            string sdt = newValue.ToShortDateString() + " " + ClockGlobals.SchedEventList[eventsListV.SelectedIndices[0]].ReminderDT.ToShortTimeString();
            DateTime dt = DateTime.Parse(sdt);
            ClockGlobals.SchedEventList[eventsListV.SelectedIndices[0]].ReminderDT = dt;
            doRefreshListViewSubItems(eventsListV.SelectedIndices[0]);
        }

        private void reminderTspin_SpinnerSpanChanged(object sender, TimeOnly newValue)
        {
            DateTime dt = DateTime.Parse(ClockGlobals.SchedEventList[eventsListV.SelectedIndices[0]].ReminderDT.ToShortDateString() + " " + newValue.ToShortTimeString());
            ClockGlobals.SchedEventList[eventsListV.SelectedIndices[0]].ReminderDT = dt;
            doRefreshListViewSubItems(eventsListV.SelectedIndices[0]);
        }

        private void isReminded_Click(object sender, EventArgs e)
        { }
        private void isReminded_CheckedChanged(object sender, EventArgs ea)
        {
            //isReminded.Checked =!isReminded.Checked;
            ClockGlobals.SchedEventList[eventsListV.SelectedIndices[0]].ReminderIsOn = isReminded.Checked;
            isReminded.Refresh();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            List<SchedEvent> tmpevents = ClockGlobals.SchedEventList.ToList();
            SchedEvent sch = new SchedEvent()
            {
                DateString = DateTime.Now.ToShortDateString(),
                Time = TimeOnly.FromDateTime(DateTime.Now),
                Description = "New Event...",
                ReminderDT = DateTime.Now.AddMinutes(1),
                ReminderIsOn = true,
                LastTriggered = DateTime.Now
            };
            tmpevents.Add(sch);
            ClockGlobals.SchedEventList = tmpevents.ToArray();
            doRefreshListView();
        }

        private void descTextBox_TextChanged(object sender, EventArgs e)
        {
            ClockGlobals.SchedEventList[eventsListV.SelectedIndices[0]].Description = descTextBox.Text;
            doRefreshListViewSubItems(eventsListV.SelectedIndices[0]);
        }

        private void eventTSpin_SpinnerSpanChanged(object sender, TimeOnly newValue)
        {
            ClockGlobals.SchedEventList[eventsListV.SelectedIndices[0]].Time = eventTSpin.TimeTime;
            doRefreshListViewSubItems(eventsListV.SelectedIndices[0]);
        }
    }
}

