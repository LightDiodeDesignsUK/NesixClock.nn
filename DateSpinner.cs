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
    public partial class DateSpinner : UserControl
    {
        public delegate void DateSpinnerSpan(object sender, DateOnly newValue);
        public event DateSpinnerSpan DateSpinChanged;
        public DateSpinner()
        {
            InitializeComponent();

        }
        public int YearNum = 2000;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DateString
        {
            get
            {
                return day1.ToString() + day2.ToString() + "-" + monthNum.ToString("00-") + YearNum.ToString();
            }
            set
            {
                try
                {
                    string sDT = DateOnly.Parse(value).ToString();
                    day1 = int.Parse(sDT[0].ToString());
                    day2 = int.Parse(sDT[1].ToString());
                    monthNum = int.Parse((sDT[3].ToString() + sDT[4].ToString()).ToString());
                    YearNum = int.Parse(sDT[6].ToString() + sDT[7].ToString() + sDT[8].ToString() + sDT[9].ToString());
                    doRefreshForm();
                }
                catch
                {
                    day1 = 1; day2 = 1; monthNum = 1; YearNum = 11;
                }
            }
        }

        DateOnly dDate = DateOnly.FromDateTime(DateTime.Now);
        int day1 = 1, day2 = 1, monthNum = 1;
        public DateOnly lastDate = DateOnly.FromDateTime(DateTime.Now);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateOnly DateDate
        {
            get
            {
                if (day1 + day2 == 0)
                { day1 = 0; day2 = 1; }
                if (day1 == 3 & day2 == 9)
                { day1 = 0; day2 = 1; }
                string s = day1.ToString() + day2.ToString() + "-" + monthNum.ToString() + "-" + DateTime.Now.Year.ToString();

                return DateOnly.Parse(s);
            }
            set
            {
                try
                {
                    monthNum = value.Month;
                    day1 = int.Parse(value.Day.ToString("00").Substring(0, 1));
                    day2 = int.Parse(value.Day.ToString("00").Substring(1, 1));
                    doRefreshForm();

                }
                catch (Exception eee)
                {
                    string s = eee.ToString();
                }
            }
        }
        private void DateSpinner_Resize(object sender, EventArgs e)
        {
            day1D.Top = dayTXT.Bottom; day2D.Top = dayTXT.Bottom; m1D.Top = monthTXT.Bottom;

            day1D.Left = dayTXT.Left;
            day2D.Left = day1D.Left + day1D.Width;
            //m1D.Left = min.Left;
        }
        //public new bool AutoSize = true;
        private void DateSpinner_Load(object sender, EventArgs e)
        {
            Width = 96; Height = 50;
        }

        private void day1Up_Click(object sender, EventArgs e)
        {
            day1++;
            if (day1 > 2)
            {
                day1 = 0;
                if (day2 == 0) day2 = 1;
            }
            doRefreshForm();
            triggerEvent(DateDate);
        }

        private void day2up_Click(object sender, EventArgs e)
        {
            day2++;
            int dayNum = (day1 * 10) + day2;
            int monthDays = ClockGlobals.DaysInMonth(monthNum, DateTime.Now.Year);
            if (dayNum > monthDays)
            {
                day1 = 0; day2 = 1;
            }
            doRefreshForm();
            triggerEvent(DateDate);
        }
        public override string ToString()
        {
            return DateDate.ToString("dd:MMM");
        }
        /// 
        /// 
        /// 
        
        void doRefreshForm()
        {
            string sT = DateDate.ToString("dd-MMM");
            dayTXT.Text = sT[0].ToString() + " " + sT[1].ToString();
            monthTXT.Text = DateDate.ToString("MMM");
        }
        void triggerEvent(DateOnly dOnly)
        {
            if (lastDate != dOnly)
                DateSpinChanged?.Invoke(this, dOnly);
            lastDate = dOnly;
        }

        private void monthUp_Click(object sender, EventArgs e)
        {
            monthNum %= 12;
            monthNum++;

            doRefreshForm();
            triggerEvent(DateDate);
        }



        private void monthDown_Click(object sender, EventArgs e)
        {
            monthNum--;
            if (monthNum < 1) monthNum = 12;
            doRefreshForm();
            triggerEvent(DateDate);
        }

        private void day1D_Click(object sender, EventArgs e)
        {
            day1--; if (day1 < 0) day1 = 0;
            if (day1 + day2 == 0) day1 = 1;
            doRefreshForm();
            triggerEvent(DateDate);
        }

        private void day2D_Click(object sender, EventArgs e)
        {
            day2--;
            if (day2 < 0) day2 = 9;
            doRefreshForm();
            triggerEvent(DateDate); ;
        }

        private void dayTXT_Click(object sender, EventArgs e)
        {

        }
    }
}
