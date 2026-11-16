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
    public partial class _7segmentTester : Form
    {
        public _7segmentTester()
        {
            InitializeComponent();
        }
        System.Windows.Forms.Timer tDigitClock = new System.Windows.Forms.Timer();

        private void _7segmentTester_Load(object sender, EventArgs e)
        {
            tDigitClock.Interval = 1000;
            tDigitClock.Tick += TDigitClock_Tick;
            tDigitClock.Start();
            
            ///////
            ///

            //this.segmentClockCtl1.OwnerForm = this;
            ////////
            //segmentClockCtl1.STartup();
        }

        private void TDigitClock_Tick(object? sender, EventArgs e)
        {
            return;//
            int dig6 = DateTime.Now.Second % 10;
            int dig5 = DateTime.Now.Second / 10;
            dig5 = dig5 % 10;

            int dig4 = DateTime.Now.Minute % 10;

            int dig3 = DateTime.Now.Minute / 10;
            dig3 = dig3 % 10;

            int dig2 = DateTime.Now.Hour % 10;

            int dig1 = DateTime.Now.Hour / 10;
            dig1 = dig1 % 10;

            segmentClockCtl1.Digits[0].Number = dig1;
            segmentClockCtl1.Digits[1].Number = dig2;
            segmentClockCtl1.Digits[2].Number = 11;
            segmentClockCtl1.Digits[3].Number = dig3;
            segmentClockCtl1.Digits[4].Number = dig4;
            segmentClockCtl1.Digits[5].Number = 11;
            segmentClockCtl1.Digits[7].Number = dig5;
            segmentClockCtl1.Digits[8].Number = dig6;


        }

        public void segmentClockCtl1_DoubleClick(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case (FormWindowState.Maximized):
                    { WindowState = FormWindowState.Normal; break; }
                case (FormWindowState.Normal):
                    { WindowState = FormWindowState.Maximized; break; }
            }
        }

        private void segmentClockCtl1_Load(object sender, EventArgs e)
        {

        }
    }
}
