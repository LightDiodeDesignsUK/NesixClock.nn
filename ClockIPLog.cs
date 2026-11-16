using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clockv2.nn
{
    public partial class ClockIPLog : Form
    {
        NESixClock ownForm;
        public ClockIPLog(NESixClock nsXC)
        {
            ownForm = nsXC;
            InitializeComponent();
        }
        public struct IPLogEntry
        {
            public string sAddr, sPort, sDesc, sURL;
            public DateTime DateTime;

        }
        private void ClockIPLog_Load(object sender, EventArgs e)
        {

        }
        public void UpdateLogList()
        {
            listView1.Invoke(new updateChangesDel(updateChanges));
        }

        private delegate void updateChangesDel();
        private void updateChanges()
        {
            listView1.SuspendLayout();
            listView1.Items.Clear();

            foreach (IPLogEntry iple in ownForm.logEntries)
            {
                ListViewItem lvi = new ListViewItem(new string[]
                        {   iple.sAddr, iple.sPort, iple.DateTime.ToShortDateString() + " " +
                        iple.DateTime.ToLongTimeString(), iple.sURL
                        });
                listView1.Items.Add(lvi);
            }
            listView1.View = View.Details;
            listView1.ResumeLayout();
        }
        public void AddToIPLogSafe(ListViewItem lv)
        {
            listView1.Invoke(new addToLogDEL(addToLog), lv);
        }
        public delegate void addToLogDEL(ListViewItem lvitem);
        public void addToLog(ListViewItem lvAdd)
        {
            listView1.Items.Add(lvAdd);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            ownForm.logEntries.Clear();
        }
    }
}
