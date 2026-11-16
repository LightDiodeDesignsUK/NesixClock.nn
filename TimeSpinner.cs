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
    public partial class TimeSpinner : UserControl
    {
        public delegate void SpinnerSpan(object sender, TimeOnly newValue);
        public event SpinnerSpan SpinnerSpanChanged;
        public TimeSpinner()
        {
            InitializeComponent();

        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string sTime
        {
            get
            {
                return timeH1.ToString() + timeH2.ToString() + ":" + timeM1.ToString() + timeM2.ToString();
            }
            set
            {

            }
        }

        TimeOnly tTime = TimeOnly.FromDateTime(DateTime.Now);
        int timeH1, timeH2, timeM1, timeM2;
        public TimeOnly? lastTime = TimeOnly.FromTimeSpan(TimeSpan.Zero);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TimeOnly TimeTime
        {
            get
            {
                try
                { return TimeOnly.Parse(sTime); }
                catch { return TimeOnly.Parse("00:00"); }
            }
            set
            {
                try
                {
                    string sT = value.ToString("HH:mm");
                    timeH1 = int.Parse(sT[0].ToString());
                    timeH2 = int.Parse(sT[1].ToString());
                    timeM1 = int.Parse(sT[3].ToString());
                    timeM2 = int.Parse(sT[4].ToString());

                    doRefresh();

                }
                catch
                {
                    timeH1 = 1; timeH2 = 2; timeM1 = 3; timeM2 = 0;
                }
            }
        }
        private void TimeSpinner_Resize(object sender, EventArgs e)
        {
            hr1D.Top = hr.Bottom; hr2D.Top = hr.Bottom; m1D.Top = min.Bottom; m2D.Top = min.Bottom;

            hr1D.Left = hr.Left;
            hr2D.Left = hr1D.Left + hr1D.Width;
            m1D.Left = min.Left;
            m2D.Left = m1D.Left + m1D.Width;
        }
        //public new bool AutoSize = true;
        private void TimeSpinner_Load(object sender, EventArgs e)
        {
            Width = 96; Height = 50;
        }

        private void hr1Up_Click(object sender, EventArgs e)
        {
            timeH1++; timeH1 %= 3;
            if (timeH2 > 4) timeH1 = 1;
            doRefresh();
            triggerEvent(TimeTime);
        }

        private void hr2up_Click(object sender, EventArgs e)
        {
            timeH2++;
            if (timeH1 == 2) timeH2 %= 4;
            else timeH2 %= 10;
            doRefresh();
            triggerEvent(TimeTime);
        }
        public override string ToString()
        {
            return TimeTime.ToString("HH:mm");
        }
        /// 
        /// 
        /// 
        void doRefresh()
        {
            string sT = TimeTime.ToString("HH:mm");
            hr.Text = sT[0].ToString() + " " + sT[1].ToString();
            min.Text = sT[3].ToString() + " " + sT[4].ToString();
            
        }
        void triggerEvent(TimeOnly tOnly)
        {
            if (lastTime != tOnly)
                SpinnerSpanChanged?.Invoke(this, tOnly);
            lastTime = tOnly;
        }

        private void minU1_Click(object sender, EventArgs e)
        {
            timeM1++;
            timeM1 %= 6;
            doRefresh();
            triggerEvent(TimeTime);
        }

        private void minU2_Click(object sender, EventArgs e)
        {
            timeM2++;
            timeM2 %= 10;
            if (timeM2 == 0)
            {
                timeM1++; timeM1 %= 6;
            }
            doRefresh();
            triggerEvent(TimeTime);
        }

        private void m2D_Click(object sender, EventArgs e)
        {
            timeM2--;
            timeM2 %= 10;
            if (timeM2 < 0)
            {
                timeM2 = 9;
                timeM1--;
                if (timeM1 < 0)
                {
                    timeM1 = 5;
                }
            }

            doRefresh();
            triggerEvent(TimeTime);
        }

        private void m1D_Click(object sender, EventArgs e)
        {
            timeM1--;
            timeM1 %= 6;
            if (timeM1 < 0) timeM1 = 5;
            doRefresh();
            triggerEvent(TimeTime);
        }

        private void hr1D_Click(object sender, EventArgs e)
        {
            if (timeH2 >= 4)
            {
                timeH1--; if (timeH1 < 0) timeH1 = 1;
            }
            else
            {
                timeH1--; if (timeH1 < 0) timeH1 = 2;
            }
            doRefresh();
            triggerEvent(TimeTime);
        }

        private void hr2D_Click(object sender, EventArgs e)
        {
            timeH2--;
            if (timeH1 == 2)
                    { if (timeH2 < 0) timeH2 = 4; }
                    else
            {
                if (timeH2 < 0) timeH2 = 9;
            }
            doRefresh();
            triggerEvent(TimeTime);
        }
    }
}
