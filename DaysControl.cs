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
    public partial class DaysControl : UserControl
    {
        public delegate void OnChangedDel(object sender, byte newMask);
        public event OnChangedDel OnChanged;
        public bool AutoSize2 = true;
        public DaysControl()
        {
            this.MinimumSize = new Size(200, 32);
            this.MaximumSize = new Size(200, 32);
            InitializeComponent();
        }
        byte dayMask = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public byte DayMask
        {
            set
            { dayMask = value; this.Controls.Clear(); DaysControl_Load(this, new EventArgs()); }
            get { return dayMask; }
        }
        List<NealsCheck> dayList = new List<NealsCheck>();
        List<NealsCheck> cbList = new List<NealsCheck>();
        private void DaysControl_Load(object sender, EventArgs e)
        {
            cbList.Clear();
            SuspendLayout();
            float chkWidth = this.Width / 8;
            for (int i = 1; i <= 7; i++)
            {
                NealsCheck cb = new NealsCheck()
                {
                    AutoSize = false,
                    Top = 1,
                    Height = this.Height,
                    Width = (int) chkWidth,
                    Text = "",
                    AutoCheck = true,
                    MaximumSize = new Size((int) chkWidth, 32),
                    MinimumSize = new Size((int) chkWidth, 32)
                };
                if (AutoSize2) cb.Left = (int)(((float)  i) * chkWidth);
                if (!AutoSize2) cb.Left = (int)(((float)i ) * 28);

                if (((DayMask >> (i-1)) & 1) == 1)
                    cb.Checked = true;

                cb.Tag = i;
                cb.CheckedChanged += Cb_CheckedChanged;
                cbList.Add(cb);
                this.Controls.Add(cb);
                cb.BringToFront();
            }
            ResumeLayout();
            DaysControl_Resize(this, e);
            Refresh();
        }

        private void Cb_CheckedChanged(object? sender, EventArgs e)
        {
            byte bRet = 0;
            for(int i = 0; i<7; i++)
            {
                byte bOld = DayMask;
                bRet |= (byte)(cbList[i].Checked ? (1 << i) : 0);
            }
            OnChanged.Invoke(this, bRet);
        }

        private void DaysControl_Resize(object sender, EventArgs e)
        {
            //Control[] cc = cbList.ToArray();
            float chkWidth = this.Width / 7;
            foreach (NealsCheck c in cbList)
            {
                c.AutoSize = false;
                if(c.GetType() == typeof(NealsCheck))
                {
                    c.MaximumSize = new Size((int)chkWidth, this.Height);
                    c.MinimumSize = new Size((int)chkWidth, 32);
                    c.Size = new Size((int)chkWidth, 32);
                    if (!AutoSize2) return;
                    float fTag = (float) (int) c.Tag;
                    c.Left = (int)(chkWidth * (fTag - 1));
                    c.Width = (int)chkWidth;
                    c.Height = this.Height;
                }
            }
        }
    }
}

