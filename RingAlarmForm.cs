using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Clockv2.nn
{
    
    public partial class RingAlarmForm : Form
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle Rect);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool Repaint);
        public const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        string sAlarmText = "";
        DateTime startedAt = DateTime.Now;
        public RingAlarmForm(string sText)
        {
            sAlarmText = sText;
            InitializeComponent();

        }

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Interval = 20 };
        bool flashed = false;
        
        

        private void playsound()
        {

            try
            {
                ClockGlobals.soundPlayer?.PlayLooping();
            }
            catch { }
        }
        private void RingAlarmForm_Load(object sender, EventArgs e)
        {
            if (ClockGlobals.wavFS == null) ClockGlobals.wavFS = File.OpenRead("d:\\neals\\ClockAlarm.WAV");
            if (ClockGlobals.soundPlayer == null) ClockGlobals.soundPlayer = new SoundPlayer(ClockGlobals.wavFS) { LoadTimeout = 250 };
            new TaskFactory().StartNew(playsound);
            this.CenterToScreen();
            this.BringToFront();
            SetForegroundWindow(this.Handle);
            timer.Tick += Timer_Tick;
            timer.Start();
            startedAt = DateTime.Now;

            if(components == null) components = new System.ComponentModel.Container();
            

            notifyIcon1.Icon = ClockGlobals.appIcon;

            notifyIcon1.Text = DateTime.Now.ToShortTimeString() + "\r\n" + DateTime.Now.ToShortDateString()
                 + "\r\n" + DateTime.Now.DayOfWeek.ToString();
            notifyIcon1.BalloonTipText = sAlarmText;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = DateTime.Now.ToShortTimeString();

            notifyIcon1.ShowBalloonTip(15);
            

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (startedAt < (DateTime.Now.AddMinutes(-1)))
            {
                timer.Stop();
                timer.Dispose();
                this.Close();
            }
            pictureBox1.Invalidate();
        }

        private void RingAlarmForm_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }
        RectangleF picSize = RectangleF.Empty;
        Brush bru = new SolidBrush(Color.Red);
        Color col = Color.Blue;
        int stripeStart = 0;
        double dTwoPi = Math.PI * 2;
        double colorStripeNum = 0;
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            stripeStart += 20;
            stripeStart = (stripeStart > 10240) ? 0 : stripeStart;
            Graphics g = e.Graphics;
            col = Color.FromArgb(255, 255, 0, 0);
            for (int i = 0; i < stripeStart; i++)
            { col = Color.FromArgb(255, col.B, col.R, col.G); }

            picSize = e.Graphics.VisibleClipBounds;
            float fStep = 1; // picSize.Height / ;

            for (float fStripe = fStep; fStripe < picSize.Height + 5; fStripe += fStep)
            {
                double dDeg = ((fStripe + stripeStart) / 406);
                double dDeg2 = (fStripe + stripeStart / 512);
                double dRed = (Math.Sin(dDeg) * 255);
                double dGreen = (Math.Cos(dDeg) * 255);
                double dBlue = (Math.Sin(dDeg2) * 255);
                byte red = (byte)dRed;
                byte green = (byte)dGreen;
                byte blue = (byte)dBlue;

                col = Color.FromArgb(red, green, blue);
                bru = new SolidBrush(col);


                RectangleF rect = new RectangleF(10f, fStripe, picSize.Width / 4, fStep);
                g.FillRectangle(bru, rect);

                col = Color.FromArgb(blue, blue, green);
                bru = new SolidBrush(col);

                rect = new RectangleF(picSize.Width / 4 * 3, fStripe, picSize.Width / 4, fStep);
                g.FillRectangle(bru, rect);

                col = Color.FromArgb(green, red, blue);
                bru = new SolidBrush(col);


                rect = new RectangleF(10f, fStripe, picSize.Width / 4, fStep);
                g.FillRectangle(bru, rect);

                col = Color.FromArgb(blue, red, green);
                bru = new SolidBrush(col);


                rect = new RectangleF(picSize.Width / 4, picSize.Height - fStripe, fStripe, fStep);
                g.FillRectangle(bru, rect);
            }



            ///////TEXT////////
            ///////TEXT////////
            ///////TEXT////////
            picSize = e.Graphics.VisibleClipBounds;
            SizeF szF = g.MeasureString(sAlarmText, this.Font, (int)picSize.Width, StringFormat.GenericTypographic);
            int xS = ((int)(picSize.Width / 2));
            xS -= (int)(szF.Width / 2);
            int yS = ((int)(picSize.Height / 2));
            yS -= (int)(szF.Height / 2);

            g.DrawString(sAlarmText, this.Font, Brushes.Black, xS + 6, yS);
            g.DrawString(sAlarmText, this.Font, Brushes.Black, xS, yS + 6);
            g.DrawString(sAlarmText, this.Font, Brushes.Black, xS + 6, yS + 6);
            g.DrawString(sAlarmText, this.Font, Brushes.Black, xS, yS);
            Font fF = this.Font;
            double dCirc2 = 0;
            int colorStripeNum2 = (int)colorStripeNum;
            for (double dCirc1 = 0; dCirc1 < (dTwoPi); dCirc1 += 0.314)
            {
                picSize = e.Graphics.VisibleClipBounds;
                fF = new Font(this.Font.FontFamily, (float)(dCirc1 * 25) + 32f);
                szF = g.MeasureString(sAlarmText, fF, (int)picSize.Width, StringFormat.GenericTypographic);
                xS = ((int)(picSize.Width / 2));
                xS -= (int)(szF.Width / 2);
                yS = ((int)(picSize.Height / 2));
                yS -= (int)(szF.Height / 2);
                dCirc2 += 0.314;

                xS += ((int)(Math.Sin(circ1 + dCirc2) * 14));
                yS += ((int)(Math.Cos(circ1 + dCirc2) * 14));
                colorStripeNum2 += 10; colorStripeNum2 %= ClockGlobals.colorStripes.Length;
                g.DrawString(sAlarmText, fF, new SolidBrush(ClockGlobals.colorStripes[(int)colorStripeNum2]), xS, yS);
            }
            circ1 += 0.314;
            fF = null;
            colorStripeNum += 6.205;
        }
        double circ1 = 0;
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            RingAlarmForm_DoubleClick(this, e);
        }

        private void RingAlarmForm_KeyUp(object sender, KeyEventArgs e)
        {
            RingAlarmForm_DoubleClick(this, e);
        }

        private void RingAlarmForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { ClockGlobals.soundPlayer.Stop(); ClockGlobals.soundPlayer.Dispose(); } catch { }
        }
    }
}