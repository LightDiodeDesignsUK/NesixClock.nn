using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
using System.Text;

namespace Clockv2.nn
{

    public partial class NESixClock : Form
    {
        System.Windows.Forms.Timer refreshTimer;
        ClockGlobals gGlobals = new();
        SegmentClockCtl? segClock = null;
        string[] numerals = { "0", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };
        public List<ClockIPLog.IPLogEntry> logEntries = new List<ClockIPLog.IPLogEntry>();
        public int ColorSchemeNo = 2;
        public static Color TransparentColor = Color.FromArgb(0x00223344);
        
        /// ICON //
        System.Windows.Forms.Timer t = new() { Interval = 100 };
        static double pi = (double)Math.PI;
        static double twoPI = Math.PI * 2;
        static Pen penSec = new Pen(Brushes.DarkOliveGreen, 1);
        static Pen penMin = new Pen(Brushes.White, 4);
        static Pen penHr = new Pen(Brushes.Cyan, 5);
        static Pen penCircle = new Pen(Brushes.Orange, 2);
        static Font? fTime;

        /// ICON //


        public static float SegClockMargin = 8;
        public static PointF SegClockPos = new PointF(100, 100);

        //  0 = MainPaintColor  // 1 = Main Border Color    // 2 = Main Spoke Color
        //  3 = HandEndsColor   // 4 = Centre Spot Color    // 5 = MinsHandColor
        // 6 = Hour Hand  Color // 7   =   SecondHandColor    // 8 = TextColor
        // 9 = MainBackColor//
        const int iMainPaintColor = 0; const int iMainBorderColor = 1; const int iMainSpokeColor = 2;
        const int iHandEndsColor = 3; const int iCentreSpotColor = 4; const int iMinsHandColor = 5;
        const int iHourHandColor = 6; const int iSecondHandColor = 7; const int iTextColor = 8;
        const int iMainBackColor = 9; const int iFaceTextColor = 10;

        public static Rectangle SegClockRectangle = new Rectangle(0, 0, 300, 300);
        public enum ColorScheme { BW = 0, WB = 1, Grey = 2, BW2 = 3, Colorful = 4 }
        public static List<List<SolidBrush>> ColorSchemes = new List<List<SolidBrush>>
        {
            //BW
            new List<SolidBrush>
            {
                new SolidBrush(Color.Black),    //  MainPaintColor          0
                new SolidBrush(Color.Black),     //  MainBorderColor        1
                new SolidBrush(Color.White),    //  MainSpokeColor          2
                new SolidBrush(Color.White),    //  HandEndsColor           3
                new SolidBrush(Color.White),    //  CentreSpotColor         4
                new SolidBrush(Color.White), //  MinsHandColor              5
                new SolidBrush(Color.LightGray),     //  HourHandColor      6
                new SolidBrush(Color.Gray),      //  SecondHandColor        7
                new SolidBrush(Color.White) ,   //  TextColor               8
                new SolidBrush(Color.Black) ,           //  MainBackColor   9
                new SolidBrush(Color.White),                            // 10
                new SolidBrush(Color.White),   //  7 Segment FillColor      11
                new SolidBrush(Color.Black),   //  7 Segment Outline         12
                new SolidBrush(Color.Black),   //  7 Segment Back,            13
                new SolidBrush(Color.FromArgb (64, Color.DarkBlue)),   //  7 Segment MouseOver,      14
                new SolidBrush(Color.Blue)   //  7 Segment Clock Border,      15
            },
            //WB
            new List<SolidBrush>
            {
                new SolidBrush(Color.Gray),    //  MainPaintColor  0
                new SolidBrush(Color.Gray),     //  MainBorderColor 1
                new SolidBrush(Color.Black),    //  MainSpokeColor  2
                new SolidBrush(Color.Black),    //  HandEndsColor   3
                new SolidBrush(Color.DarkOliveGreen),    //  CentreSpotColor 4
                new SolidBrush(Color.Black),     //  MinsHandColor   5
                new SolidBrush(Color.Black),     //  HourHandColor   6
                new SolidBrush(Color.Black),      //  SecondHandColor 7
                new SolidBrush(Color.Black) ,   //  TextColor       8
                new SolidBrush(Color.White) ,   //  MainBackColor   9
                new SolidBrush(Color.Black),   //  FaceTextColor   10}
                new SolidBrush(Color. Black),   //  7 Segment FillColor   11}
                new SolidBrush(Color. White),  //  7 Segment Outline   12}
                new SolidBrush(Color. White),   //  7 Segment Back   13}
                new SolidBrush(Color.Blue),   //  7 Segment MouseOver,      14
                new SolidBrush(Color.Blue)   //  7 Segment Clock Border,   15
            },
            //Grey
            new List<SolidBrush>()
            {
                new SolidBrush(Color.DarkGray ),    //0 Main Paint
                new SolidBrush(Color.Black),        //1
                new SolidBrush(Color.LightGray), //2 MainSpokeColor
                new SolidBrush(SystemColors.GrayText),      //3 HandEndsColor
                new SolidBrush(Color.Gray),         //4     CentreSpotColor
                new SolidBrush(Color.DarkGray),     //5     MinsHandColor
                new SolidBrush(Color.LightGray),    //6     HourHandColor
                new SolidBrush(Color.White),        //7     SecondHand
                new SolidBrush(Color.White),        //8     Text Color
                new SolidBrush(Color.DarkGray),     //9     Main Back
                new SolidBrush(Color.LightGray),    //10    Face Text
                new SolidBrush(Color. DarkGray),    //  7 Segment FillColor   11}
                new SolidBrush(Color. LightGray),   //  7 Segment Outline   12}
                new SolidBrush(Color. DarkGray),   //  7 Segment Back   13}
                new SolidBrush(Color.DarkBlue),   //  7 Segment MouseOver,      14
                new SolidBrush(Color.Blue)   //  7 Segment Clock Border,   15
            },
            ///////// BW2 //////////
            new List<SolidBrush>()
            {   new SolidBrush(Color.DarkCyan), //0
                new SolidBrush(Color.Yellow),   //1
                new SolidBrush(Color.White),   //2
                new SolidBrush(Color.White),   //3
                new SolidBrush(Color.Gray),   //4
                new SolidBrush(Color.White),   //5
                new SolidBrush(Color.LightGray),   //6
                new SolidBrush(Color.White),   //7
                new SolidBrush(Color.White),   //8
                new SolidBrush(Color.Black),   //9
                new SolidBrush(Color.White),   //10
                new SolidBrush(Color.Red),   //  7 Segment FillColor   11}
                new SolidBrush(Color.Red),   //  7 Segment Outline   12}
                new SolidBrush(Color.Black),   //  7 Segment Back   13}
                new SolidBrush(Color.DarkBlue),   //  7 Segment MouseOver,      14
                new SolidBrush(Color.Blue)   //  7 Segment Clock Border,   15
            },
            //////////// Colorful //////////
            new List<SolidBrush>()
            {   new SolidBrush(Color.DeepSkyBlue),    //  MainPaintColo  0
                new SolidBrush(Color.Gray),     //  MainBorder 1
                new SolidBrush(Color.DeepPink),    //  MainSpoke  2
                new SolidBrush(Color.DarkBlue),    //  HandEnds   3
                new SolidBrush(Color.DarkOliveGreen),    //  CentreSpot 4
                new SolidBrush(Color.DarkOrange),     //  MinsHand   5
                new SolidBrush(Color.Blue),     //  HourHand   6
                new SolidBrush(Color.Red),      //  SecondHand 7
                new SolidBrush(Color.White) ,   //  Text       8
                //new SolidBrush(Color.LightGoldenrodYellow) ,   //  MainBack   9
                new SolidBrush(Color.DeepSkyBlue) ,   //  Segment Backcolor   9
                new SolidBrush(Color. Cyan),   //  FaceTextColor   10}

                new SolidBrush(Color. Cyan),   //  7 Segment FillColor   11}
                new SolidBrush(Color. Magenta),   //  7 Segment Outline   12}
                new SolidBrush(Color.DarkGreen),   //  7 Segment Back   13}
                new SolidBrush(Color.Blue),   //  7 Segment MouseOver,      14
                new SolidBrush(Color.Blue)   //  7 Segment Clock Border,   15
                
            }

        };
        public enum SchemeMembers
        {
            MainPaintColor = 0, MainBorderColor = 1, MainSpokeColor = 2, HandEndsColor = 3,
            CentreSpotColor = 4, MinsHandColor = 5, HourHandColor = 6, SecondHandColor = 7, TextColor = 8,
            MainBackColor = 9, FaceTextColor = 10, _7SegFill = 11, _7SegOutline = 12, _7SegBack = 13, _7SegHover = 14, _7SegClockBorder = 15
        }
        int iStartupCount = -1;
        StringFormat noWrapCentredFormat = new StringFormat(StringFormatFlags.NoWrap) { Alignment = StringAlignment.Center };
        public NESixClock()
        {

            Color[] colorStripes = ClockGlobals.colorStripes;
            AlarmWatcher.Start();
            NSHTTP.Startup();
            InitializeComponent();
            Application.AddMessageFilter(new ScrollMessageFilter(this.ClockPBox));
            refreshTimer = new System.Windows.Forms.Timer() { Interval = 900, Enabled = true };
            refreshTimer.Tick += T_Tick;
            List<Color> colors = new List<Color>();
            uint nCol;

            for (uint n = 0; n < 96; n += 2)
            { nCol = n; colors.Add(Color.Black); }

            for (uint n = 0; n < 64; n += 2)
            { nCol = n; colors.Add(Color.White); }

            for (uint n = 0; n < 96; n += 2)
            { nCol = n; colors.Add(Color.Black); }

            for (uint n = 0; n < 256; n += 2)
            { nCol = n; colors.Add(Color.FromArgb(255, 127, (byte)(255 - nCol), 255 - (byte)nCol)); }

            for (uint n = 0; n < 256; n += 2)
            { nCol = n; colors.Add(Color.FromArgb(255, (byte)(255 - nCol), 127, (byte)nCol)); }
            for (uint n = 0; n < 256; n += 2)
            { nCol = n; colors.Add(Color.FromArgb(255, 127, (byte)(255 - nCol), 255 - (byte)nCol)); }

            for (uint n = 0; n < 256; n += 2)
            { nCol = n; colors.Add(Color.FromArgb(255, 0, (byte)nCol, 0)); }
            for (uint n = 0; n < 256; n += 2)
            { nCol = n; colors.Add(Color.FromArgb(255, 0, 255 - (byte)nCol, 0)); }

            for (uint n = 0; n < 256; n += 2)
            { nCol = n; colors.Add(Color.FromArgb(255, (byte)nCol, 0, 0)); }
            for (uint n = 0; n < 256; n += 2)
            { nCol = n; colors.Add(Color.FromArgb(255, 255 - (byte)nCol, 0, 0)); }

            colorStripes = colors.ToArray();
            ClockGlobals.colorStripes = colorStripes;
            colors.Clear();
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
            NSXClock_Resize(this, e);
        }

        string sLastTextTime = "";
        bool isTitleBarOn = true;
        public void T_Tick(object? sender, EventArgs e)
        {
            if (iStartupCount-- == 0)
            {
                NSXClock_Resize(this, e);
                this.Refresh();
            }
            if (iStartupCount < 0) iStartupCount = -1;

            if (!isStarted)
            {
                if (ClockSettings.Default.ClockSize != Size.Empty)
                {
                    this.Size = ClockSettings.Default.ClockSize;
                    this.Visible = true;
                }
                this.Location = ClockSettings.Default.ClockLocation;
                if ((Location.X <= 0) | Location.Y <= 0)
                {
                    Location = new Point(0, 0);
                    ClockSettings.Default.ClockLocation = Location;
                }
                isStarted = true;
            }
            string sNewTime = DateTime.Now.ToString("ddd yy-MM-dd");
            if (isDateShown.Checked)
            {
                if (!isTitleBarOn)
                {
                    if (!this.ControlBox) this.ControlBox = true;
                    isTitleBarOn = true;
                }


                if (sLastTextTime != sNewTime)
                {
                    this.Text = sNewTime;
                    sLastTextTime = sNewTime;
                }
            }
            else
            {
                sNewTime = "";
                if (isTitleBarOn)
                {
                    if (this.ControlBox) this.ControlBox = false;
                    isTitleBarOn = false;
                    //sLastTextTime = "";

                }
                if (sLastTextTime != sNewTime)
                {
                    this.Text = "";
                }
                sLastTextTime = "";
                isTitleBarOn = false;
            }
            RectangleF rf = this.CreateGraphics().ClipBounds;
            if (rf == RectangleF.Empty)
            { }
            else
            {
                if (ClockGlobals.MainBMP == null)
                {
                    //bBackBuffer = new Bitmap((int)e.Graphics.ClipBounds.Width, (int)e.Graphics.ClipBounds.Height);
                    if (ClockPBox.Height < 100 | ClockPBox.Width < 100)
                    {
                        ClockGlobals.MainBMP = new Bitmap(800, 600, PixelFormat.Format32bppArgb);
                    }
                    else
                    {
                        ClockGlobals.MainBMP = new Bitmap((int)ClockPBox.Width, (int)ClockPBox.Height, PixelFormat.Format32bppArgb);
                    }
                }
                try
                {
                    PaintEventArgs peNew = new PaintEventArgs(Graphics.FromImage(ClockGlobals.MainBMP), new Rectangle(0, 0, ClockGlobals.MainBMP.Width, ClockGlobals.MainBMP.Height));
                    doMakeFrame(peNew);
                    ClockPBox.Invalidate();
                }
                catch { }
            }
        }
        ComboBox cbMenuStrip = new ComboBox() { Width = 300, Height = 100, Text = "Spiral" };

        NSHTTP? webServer;
        Font TimeFont;
        DateTime dtStartTime = DateTime.Now;
        System.Windows.Forms.Timer? iconTimer;
        private void NSXClock_Load(object sender, EventArgs e)
        {
            AutoScaleDimensions = this.Size;
            AutoScaleMode = AutoScaleMode.None;
            isHalfSevenSeg.Checked = ClockSettings.Default.SevenSegHalf;
            isfullSevenSeg.Checked = ClockSettings.Default.SevenSegFull;
            filledText.Checked = ClockSettings.Default.FilledText;
            //segmentClockCtl1.STartup();
            isFixedDome.Checked = ClockSettings.Default.IsFixedDome;
            isPulsedDome.Checked = ClockSettings.Default.IsPulsedDome;
            IsInverse.Checked = ClockSettings.Default.IsInverse;
            isSeconds.Checked = ClockSettings.Default.IsSeconds;
            if (iconTimer == null)
            {
                iconTimer = new() { Interval = 1000 };
                iconTimer.Tick += IconT_Tick; iconTimer.Start();
            }

            textSizeMenuSelection = ClockSettings.Default.FontSetting;
            switch (textSizeMenuSelection)
            {
                case (0): { smallFont_Click(this, e); break; }
                case (1): { medFont_Click(this, e); break; }
                case (2): { largeFont_Click(this, e); break; }
                case (3): { largerFont_Click(this, e); break; }
                case (4): { autoFontSize_Click(this, e); break; }
            }
            ClockGlobals.doAlarmsLoad();
            SchemePick.SelectedIndex = ClockSettings.Default.ColorListIndex;
            TimeFont = new Font(Font.FontFamily, 63, FontStyle.Regular);
            ClockPBox.BorderStyle = BorderStyle.FixedSingle;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.AllowTransparency = true;
            this.BackColor = TransparentColor;
            this.TransparencyKey = TransparentColor;
            ClockPBox.BackColor = TransparentColor;
            this.ControlBox = (ClockSettings.Default.IsDateTitle);
            this.Text = "";
            doStartup1();
            SetTextFont(64); // needs saving as a setting
            //doStartup2();
            iStartupCount = 1;
        }

        /// <summary>
        /// Returns values from checked menu items
        /// </summary>
        public bool Am7Seg { get { return is7segment.Checked; } }

        public bool Am7SegBordered { get { return isBorderedMenu.Checked; } }
        public bool AmTransparent7Seg { get { return isTransparentMenu.Checked; } }
        public bool AmEnds { get { return isEnds.Checked; } }
        public bool AmSeconds { get { return isSeconds.Checked; } }
        public bool AmAnalogue { get { return isAnalog.Checked; } }
        public bool AmAnimated { get { return isAnimated.Checked; } }


        public int AmColorSchemeNo { get { return SchemePick.SelectedIndex; } }
        //
        bool isStarted = false;
        void doStartup1()
        {
            is7segment.Checked = ClockSettings.Default.IsSevenSeg;
            isEnds.Checked = ClockSettings.Default.IsEnds;
            isSeconds.Checked = ClockSettings.Default.IsSeconds;
            isAnalog.Checked = ClockSettings.Default.IsAnalog;
            isOrdinals.Checked = ClockSettings.Default.IsOrdinals;
            isNumbers.Checked = ClockSettings.Default.IsNumbers;
            isNumerals.Checked = ClockSettings.Default.IsNumerals;
            isHandsOn.Checked = ClockSettings.Default.IsHands;
            isDateShown.Checked = ClockSettings.Default.IsDateTitle;
            isTextOn.Checked = ClockSettings.Default.IsText;
            isNumerals.Checked = ClockSettings.Default.IsNumerals;
            SchemePick.SelectedIndex = ClockSettings.Default.ColorListIndex;
            Face = null;
            if (!isHandsOn.Checked & !isTextOn.Checked) { isShowHands = true; }

            if (isTextOn.Checked & ClockSettings.Default.TimerInterval > 1000)
            { SetTimerInterval(1000); }
            else
            { SetTimerInterval(ClockSettings.Default.TimerInterval); }
            isSeconds_Click(this, new EventArgs());
            is7segment_Click(this, new EventArgs());
            isHandsOn_Click(this, new EventArgs());
        }
        void doStartup2()
        {
            isAnalog_Click(this, new EventArgs());
            isOrdinals_Click(this, new EventArgs());
            isOrdinals_Click(this, new EventArgs());
            isHandsOn_Click(this, new EventArgs());
            isDateShown_Click(this, new EventArgs());
            isNumbers_Click(this, new EventArgs());
            isNumerals_Click(this, new EventArgs());
            is7segment_Click(this, new EventArgs());
            isSeconds_Click(this, new EventArgs());
            is7segment_Click(this, new EventArgs());

            //////////////////////////////////////////

            // if (isAnalog.Checked)
            // { refreshTimer.Interval = 60; vScrollBar1.Value = 60; }
            // webServer = new NSHTTP(888, this);
            //  webServer.OwnerPicBox = this.pictureBox1;
            //   schemeNo = 0;
            //SchemePick.SelectedIndex = 0;
            isHandsOn_Click(this, new EventArgs());
            SetTimerInterval(ClockSettings.Default.TimerInterval);

        }
        static Pen NealsPen = new Pen(Brushes.DarkGreen, 1);
        const float gridsX = 30; const float gridsY = 30;
        Pen NealsPen2 = new Pen(Brushes.Gray, 1);
        const float gridsizeX = 50; const float gridsizeY = 50;
        uint iBlue = 127; uint iRed = 0; uint iGreen = 255;
        bool isShowIntervalValue = false;
        public DateTime ShowIntvaltimer = DateTime.Now;
        static Bitmap bmpIcon;
        ///   ICON TIMER    ///////////////////
        ///   
        double minutesRads = 0, secondsRads = 0, hoursRads = 0;
        Icon ic;

        string sLastIconTime = "00:00";
        
        private void IconT_Tick(object? sender, EventArgs e)
        {
            if (sLastIconTime == DateTime.Now.ToString("HH:mm")) return;
            sLastIconTime = DateTime.Now.ToString("HH:mm");
            bmpIcon?.Dispose();
            bmpIcon = new(width: 48, height: 48);
            Graphics g = Graphics.FromImage(bmpIcon);
            float halfX = bmpIcon.Width / 2, halfY = bmpIcon.Height / 2;
            float qalfX = 24, qalfY = 24;
            float qqalfX = 8, qqalfY = 16;

            double second = DateTime.Now.Second; second %= 60;
            double minute = DateTime.Now.Minute; minute %= 60;
            double hour = DateTime.Now.Hour;
            if (1 == 2)
            {
                g.FillRectangle(Brushes.Black, 0, 0, 64, 64);
                g.DrawEllipse(Pens.Black, 0, 0, 64, 64);
                //g.DrawEllipse(penCircle, 1,1,28,28);

                fTime = new Font(FontFamily.GenericMonospace, 38, FontStyle.Bold, GraphicsUnit.Pixel);
                g.DrawString((hour.ToString("00")), fTime, Brushes.White, qqalfX - 3, -6);
                g.DrawString(minute.ToString("00"), fTime, Brushes.White, qqalfX - 3, halfY - 6);
                fTime.Dispose();

                secondsRads = ((second + 45) / 60) * twoPI;
                minutesRads = ((minute + 45) / 60) * twoPI;
                hoursRads = (((hour + 9) % 12) / 12) * twoPI;

                float secondX = (float)(Math.Cos(secondsRads) * halfX) + halfX;
                float secondY = (float)(Math.Sin(secondsRads) * halfX) + halfY;
                float minuteX = (float)(Math.Cos(minutesRads) * qalfX) + halfX;
                float minuteY = (float)(Math.Sin(minutesRads) * qalfX) + halfY;
                float hourX = (float)(Math.Cos(hoursRads) * qqalfX) + halfX;
                float hourY = (float)(Math.Sin(hoursRads) * qqalfY) + halfY;

                PointF halfPoint = new PointF(halfX, halfY); PointF p2 = new PointF(secondX, secondY);
                //g.DrawLine(penSec, p1, p2);

                p2 = new PointF(minuteX, minuteY);
                g.DrawLine(penMin, halfPoint, p2);

                p2 = new PointF(hourX, hourY);
                g.DrawLine(penHr, halfPoint, p2);
            }

            // sb.Dispose();
            g.Dispose();
            try
            {
                if (ic == null)
                {
                    if (this.Icon != null)
                    {
                        ic = new Icon(this.Icon, Icon.Width, Icon.Height);
                    }
                    else
                    { }
                }

                bmpIcon = SegmentIcon.getIconBitmap(this);
                //  Icon?.Dispose();
                if (DateTime.Now > dtStartTime.AddSeconds(2))
                {
                    try
                    {
                        //float ffff = 1 / (halfX - halfX);
                        ic = Icon.FromHandle(bmpIcon.GetHicon());
                        ClockGlobals.appIcon = ic;
                        //
                        //public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes? imageAttr, DrawImageAbort? callback);




                        //// public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes? imageAttr, DrawImageAbort? callback);
                        ClockPBox.CreateGraphics().DrawImage(bmpIcon, new Rectangle(0, 0, Width, Height), new Rectangle(0, 0, 400, 400), GraphicsUnit.Pixel);



                        this.Icon = ic;
                        try
                        {
                        //    File.Delete("D:\\neals\\1.ico");
                        }
                        catch { }
                        //FileStream fs = new System.IO.FileStream("D:\\neals\\1.ico", FileMode.CreateNew, FileAccess.Write);
                        //ic.Save(fs);
                        //fs.Close();
                    }
                    catch (Exception eee)
                    {
                        // System.Diagnostics.Process.Start(Application.ExecutablePath);
                       // Application.Exit();
                        return;
                    }
                    //ic.Dispose();
                    ic = null;
                }
                GC.Collect();
            

            }
            catch (Exception eee) { string seee = eee.Message; }
            g = null;
            fTime?.Dispose();
        }
            float xMax;
            float yMax;
            float xHMax;
            float yHMax;
            double hPI = Math.PI / 2;
            Bitmap? Face;
            int lastSecond = 61;
        
        public static Random Rnd = new Random();



        public SolidBrush? HandEndColor, MainPaintColor, MainTextColor, MainBorderColor, SecondHandColor, NextSpokeColor, CentreSpotColor, MinsHandColor, HourHandColor, TextOuterColor, MainBackColor, FaceTextColor;
        Bitmap _bBackBuffer = null; // now in Globals
        private void NSXClockPicbox_Paint(object sender, PaintEventArgs e)
        {
            if (ClockGlobals.MainBMP == null)
            {
                if (ClockPBox.Height <= 100 | ClockPBox.Width <= 100)
                { ClockGlobals.MainBMP = new Bitmap(800, 600, PixelFormat.Format32bppArgb); }
                else ClockGlobals.MainBMP = new Bitmap(ClockPBox.Width, ClockPBox.Height, PixelFormat.Format32bppArgb);
            }
            try
            {
                
                e.Graphics.DrawImage(ClockGlobals.MainBMP, e.ClipRectangle, 0, 0, ClockGlobals.MainBMP.Width, ClockGlobals.MainBMP.Height, GraphicsUnit.Pixel);
            }
            catch { }
            //b.Dispose();
            ClockPBox.ResumeLayout();
        }
        string sTime = "";
        Pen? innerElipsePen;
        SizeF timeTextSize(Graphics g)
        {
            SizeF szTimef = g.MeasureString(sTime, TimeFont, new SizeF(g.VisibleClipBounds.Width, g.VisibleClipBounds.Height), noWrapCentredFormat);
            return szTimef;
        }
        float SegLeftPos, SegRightPos, SegTopPos, SegBottomPos;
        float segPosDeltaX = 4f, segPosDeltaY = 8;
        void doMakeFrame(PaintEventArgs e)
        {
            Color[] colorStripes = ClockGlobals.colorStripes;
            if (isStarted && !AmAnimated && !isSeconds.Checked && !isPulsedDome.Checked && !isTextOn.Checked)
            {

                SetTimerInterval(10000);
            }
            innerElipsePen = new Pen(ColorSchemes[ColorSchemeNo][iHandEndsColor], 3);

            bool isEndsOn = isEnds.Checked;
            MainPaintColor = ColorSchemes[ColorSchemeNo][iMainPaintColor];
            MainBorderColor = ColorSchemes[ColorSchemeNo][iMainBorderColor];
            SecondHandColor = ColorSchemes[ColorSchemeNo][iSecondHandColor];
            NextSpokeColor = ColorSchemes[ColorSchemeNo][iHandEndsColor];
            HandEndColor = ColorSchemes[ColorSchemeNo][iHandEndsColor];
            CentreSpotColor = ColorSchemes[ColorSchemeNo][iCentreSpotColor];
            MinsHandColor = ColorSchemes[ColorSchemeNo][iMinsHandColor];
            HourHandColor = ColorSchemes[ColorSchemeNo][iHourHandColor];
            TextOuterColor = ColorSchemes[ColorSchemeNo][iTextColor];
            MainBackColor = ColorSchemes[ColorSchemeNo][iMainBackColor];
            FaceTextColor = ColorSchemes[ColorSchemeNo][iFaceTextColor];
            MainTextColor = ColorSchemes[ColorSchemeNo][iTextColor];

            //e.Graphics.FillRectangle(MainPaintColor, e.ClipRectangle);

            try
            {
                xMax = e.ClipRectangle.Width;
                yMax = e.ClipRectangle.Height;
                xHMax = e.ClipRectangle.Width / 2;
                yHMax = e.ClipRectangle.Height / 2;


                if (AmAnimated)
                {
                    if (segClock != null)
                    {
                        SegClockRectangle = new Rectangle((int)SegClockPos.X, (int)SegClockPos.Y, segClock.Size.Width, segClock.Size.Height);
                        SegClockPos.X += segPosDeltaX;
                        SegClockPos.Y += segPosDeltaY;

                        segClock?.TAnimclock_Tick(this, e);
                        SegLeftPos = SegClockPos.X;
                        SegTopPos = SegClockPos.Y;
                        SegRightPos = SegClockPos.X + segClock.Width;
                        SegBottomPos = SegClockPos.Y + segClock.Height;

                        //if (SegBottomPos >= this.Height) { segPosDeltaY = -segPosDeltaY; }
                        //if (SegRightPos >= this.Width) { segPosDeltaX = -segPosDeltaX; }

                        if (SegLeftPos <= 0)
                        {
                            if (segPosDeltaX < 0)
                            {
                                segPosDeltaX = (float)(Rnd.NextDouble() * 46) + 15;
                                if (segPosDeltaX < 0) segPosDeltaX = -segPosDeltaX;
                            }
                        }
                        if (SegRightPos > Width)
                        {
                            segPosDeltaX = (float)(Rnd.NextDouble() * 46) + 15;
                            if (segPosDeltaX > 0) segPosDeltaX = -segPosDeltaX;
                        }


                        if (SegTopPos <= 0)
                        {
                            if (segPosDeltaY < 0)
                            {
                                segPosDeltaY = (float)(Rnd.NextDouble() * 44) + 15;
                                if (segPosDeltaY < 0) { segPosDeltaY = -segPosDeltaY; }
                            }
                        }
                        if (SegBottomPos >= this.Height)
                        {
                            segPosDeltaY = (float)(Rnd.NextDouble() * 44) + 15;
                            if (segPosDeltaY > 0) segPosDeltaY = -segPosDeltaY;
                        }
                    }


                    SegClockPos = new PointF(SegLeftPos, SegTopPos);
                }


                ////// failed

                if ((Face == null))
                {

                    Face = new Bitmap(e.ClipRectangle.Width, e.ClipRectangle.Height);
                    Graphics gf = Graphics.FromImage(Face);

                    if (ColorSchemeNo > 0) gf.FillRectangle(MainBackColor, e.ClipRectangle);
                    PointF halfPoint = new PointF(xHMax, yHMax);



                    gf.DrawRectangle(new Pen(MainBorderColor, 4), e.ClipRectangle);
                    if (isHandsOn.Checked)
                    {
                        for (double dRad = 0; isHandsOn.Checked & (dRad < Math.PI * 2); dRad += Math.PI / 30)
                        {

                            iRed += 50;

                            if (isOrdinals.Checked)
                            {
                                doSpoke(dRad, gf, 0.775f, 0.95f, 1, 1, MainTextColor.Color, false, false, "", false);
                                //doSpoke(dRad, gf, 0.6f, 0.75f, 1, 1, Color.DarkKhaki, false, "",false);
                            }
                        }

                    }


                    //iBlue = 127; iRed = 0; iGreen = 12; // (uint) DateTime.Now.Millisecond;
                    //for (double dRad = 0; dRad < Math.PI * 2; dRad += Math.PI / 3000)
                    //{
                    //   iRed = 125;//  colorStripes[iCount % colorStripes.Length].R;
                    //  iGreen = 125;// colorStripes[iCount % colorStripes.Length].G;
                    // iBlue = 192; // colorStripes[iCount % colorStripes.Length].B;
                    //if ((iCount % 800) == 0)
                    //{   doSpoke(dRad, g, 0.7f, 0.9f, 1, 4, Color.White, false);
                    //    iCount = 0;
                    //}
                    //else doSpoke(dRad, g, 0.8f, 0.9f, 1, 1, Color.DarkKhaki, false);

                    //iCount+= 18;
                    //}
                    if (e.Graphics.ClipBounds.Size == Size.Empty)
                    {
                        return;
                    }


                    //pictureBox1.DrawToBitmap(Face, e.ClipRectangle);
                    //g.DrawString(DateTime.Now.ToLongTimeString(), Font, Brushes.Yellow, 20, 20);

                    /////   
                    /// FIXED DOME //
                    /// 

                    if (isHandsOn.Checked && isFixedDome.Checked)
                    {
                        float distance = xMax + yMax / 64;
                        gf.FillEllipse(CentreSpotColor, xHMax - 10, yHMax - 10, 20, 20);
                        gf.DrawEllipse(innerElipsePen, xMax / 8, yMax / 8, xMax / 4 * 3, yMax / 4 * 3);
                        float f2 = 0;
                        for (float f = 0; f < (distance); f += 2f)
                        {
                            f2 = (f / distance) * 6.28f;
                            float fX = (f / distance) / 2;
                            fX *= xHMax;
                            float fY = (f / distance) / 2;
                            fY *= yHMax;
                            innerElipsePen = null;
                            double dRGBa = Math.Cos(f2) * 256;
                            double dRGBr = Math.Cos(f2 + f) * 256;
                            double dRGBg = Math.Cos(f2 + (f * 1.5f)) * 256;
                            double dRGBb = Math.Cos(f2 + (f * 2)) * 256;
                            int fa = (int)(byte)dRGBa;
                            int fr = (int)(byte)dRGBr;
                            int fg = (int)(byte)dRGBg;
                            int fb = (int)(byte)dRGBb;
                            innerElipsePen = new Pen(Color.FromArgb(fa, fr, fg, (byte)fb), 1);
                            gf.DrawEllipse(innerElipsePen, xHMax - fX, yHMax - fY, fX * 2, fY * 2);
                            innerElipsePen = null;
                        }
                    }
                    int iCardinalF = 12;

                    for (double dRad = 0; isHandsOn.Checked & dRad < Math.PI * 2; dRad += Math.PI / 6)
                    {

                        //iRed += 50;
                        if (isHandsOn.Checked)
                            doSpoke(dRad - (Math.PI / 2), gf, 0.65f, 0.7f, 1, 0, Color.White, false, false, iCardinalF.ToString().Trim(), rotateToolStripMenuItem.Checked);

                        iCardinalF++; iCardinalF = (iCardinalF > 12) ? iCardinalF - 12 : iCardinalF;

                    }
                    e.Graphics.Clear(Color.Black);
                    e.Graphics.DrawImage(Face, 0, 0);
                }


                else
                {
                    //if (Face != null) 
                    e.Graphics.Clear(Color.Black);
                    e.Graphics.DrawImage(Face, 0, 0);
                }

                double dStep = Math.PI / 256;
                int iSpir = DateTime.Now.Millisecond / 8;
                iSpir += DateTime.Now.Second * 128;
                while (iSpir < 0) iSpir += colorStripes.Length + 1;
                ///////



                if (AmAnimated & !AmFull7Seg)
                {
                    for (double dSpiral = Math.PI * 42; dSpiral >= 1; dSpiral -= ((dSpiral / 1020) + 0.00125))
                    {
                        if ((DateTime.Now.Second) % 5 == 0)
                        {
                            iSpir += 1; iSpir %= colorStripes.Length;

                        }
                        else
                        {
                            iSpir += 1; iSpir %= colorStripes.Length;
                        }


                        Color c = colorStripes[iSpir];
                        if ((DateTime.Now.Second) % 5 != 0)
                            c = Color.FromArgb(c.A & 192, c.R, c.B, c.G);
                        else
                            c = Color.FromArgb(c.A | 192, c.R, c.B, c.G);

                        double dPos = dSpiral / 350;

                        //double dRnd = (Rnd.NextDouble() * 128);
                        double dSpiral2 = dSpiral + (DateTime.Now.Millisecond / 250);
                        drawDot(dSpiral2, e.Graphics, (float)dPos, 4, c);
                        //drawDot(dSpiral, e.Graphics, (float)dPos + (DateTime.Now.Millisecond / 2000), 34, c);
                        //drawDot(dSpiral + dRnd, e.Graphics, (float)dPos, 2, c);

                    }
                }
                //////////////
                //////////
                if (AmAnimated & !AmFull7Seg)
                {

                    for (double dSpiral = Math.PI * 52; dSpiral >= 0; dSpiral -= ((dSpiral / 140) + 0.0125))
                    {
                        if ((DateTime.Now.Second) % 5 == 0)
                        {
                            iSpir += 1; iSpir %= colorStripes.Length;

                        }
                        else
                        {
                            iSpir += 1; iSpir %= colorStripes.Length;
                        }


                        Color c = colorStripes[iSpir];
                        if ((DateTime.Now.Second) % 5 != 0)
                            c = Color.FromArgb(c.A & 192, c.R, c.B, c.G);
                        else
                            c = Color.FromArgb(c.A | 192, c.R, c.B, c.G);

                        double dPos = (dSpiral / 356 + 0.4);
                        double dRnd = (Rnd.NextDouble() * 128);
                        //drawDot(dSpiral + dRnd, e.Graphics, (float)dPos, 6, c);
                        double dSecMod = ((float)(DateTime.Now.Second - 15) / 15 * hPI);
                        drawDot(dSpiral + dSecMod, e.Graphics, (float)dPos, 10, c);
                        double dSpiral22 = dSpiral - (DateTime.Now.Second / 30 * 3.14) + dSecMod;
                        drawDot(dSpiral22, e.Graphics, (float)dPos, 15, c);

                        drawDot(dSpiral22, e.Graphics, (float)(dPos * 1.25), 1, c);
                        drawDot(-dSpiral22 * 2, e.Graphics, (float)(dPos * -1.45), 4, c);
                    }
                }

                //////////////
                if (AmAnimated & !AmFull7Seg)
                {
                    dStep = Math.PI / (1024 * 2);
                    iSpir = DateTime.Now.Millisecond / 4;
                    iSpir += DateTime.Now.Second * 256;
                    if (iSpir < 0) iSpir += colorStripes.Length + 1;
                    for (double dSpiral = 0; dSpiral < Math.PI * 512; dSpiral += dStep)
                    {
                        iSpir++; iSpir %= colorStripes.Length;
                        Color c = colorStripes[iSpir];
                        double dPos = dSpiral / 768;
                        //    drawDot(dSpiral, e.Graphics, (float)dPos, 1, c);

                    }
                }

                /////////////
                float fSeconds = 0;// (float)((DateTime.Now.Second) - 15) / 60;
                float fmSec = 2;
                fSeconds = (float)((DateTime.Now.Second) - 15) / 60;
                if (AmAnalogue)
                {

                    fmSec = ((float)DateTime.Now.Millisecond) / 64000;
                    fSeconds += fmSec;
                }

                fSeconds *= (float)(Math.PI * 2);


                if (isHandsOn.Checked & isSeconds.Checked) doSpoke(fSeconds, e.Graphics, 0.05f, 1, 1, 4, SecondHandColor.Color, true, isEndsOn, "", false);

                int iCountTick = 127;
                //if((DateTime.Now.Second & 1) == 1 ) iCountTick += (56);
                //iCountTick += (int) (256-  (DateTime.Now.Millisecond / 2.5));

                if (isAnimated.Checked & !Am7Seg)
                {
                    for (double dRad = 0; dRad < Math.PI * 2; dRad += Math.PI / 1024)
                    {
                        iCountTick %= 480;
                        iCountTick += 10;
                        Color c = colorStripes[iCountTick % colorStripes.Length];

                        doSpoke(dRad - 0.11, e.Graphics, 0.8f, 0.95f, 1, 1, c, false, false, "", false);
                    }
                }

                //////  
                //////  Pulsed Dome
                //////  
                Graphics g = e.Graphics;
                if (isHandsOn.Checked && isPulsedDome.Checked)
                {
                    g.FillEllipse(CentreSpotColor, xHMax - 10, yHMax - 10, 20, 20);
                    g.DrawEllipse(innerElipsePen, xMax / 8, yMax / 8, xMax / 4 * 3, yMax / 4 * 3);
                    float f2 = DateTime.Now.Second / 4;

                    float distance = Math.Max(xMax / 2, yMax / 2) / 10;

                    for (float f = 0; f < (distance); f += 0.25f)
                    {
                        float fDistRatio = f / distance;
                        float distX = fDistRatio * xHMax / 2;
                        float distY = fDistRatio * yHMax / 2;
                        float fStep = 0.251f;
                        f2 += fStep;
                        g.DrawEllipse(innerElipsePen, xHMax - distX, yHMax - distY, distX * 2, distY * 2);
                        innerElipsePen = null;
                        double dSec = Math.Min(DateTime.Now.Millisecond, 250);
                        dSec /= 1024;
                        double dRGBa = 255;// Math.Cos(f2 + dSec) * 256;
                        double dRGBr = Math.Cos((f2 / 4) + dSec) * 256;
                        double dRGBg = Math.Cos((f2 / 2) + dSec) * 256;
                        double dRGBb = Math.Cos(f2) * 256;
                        int fa = 255; // (int)(byte)dRGBa;
                        int fr = (int)(byte)dRGBr;
                        int fg = (int)(byte)dRGBg;
                        int fb = (int)(byte)dRGBb;
                        innerElipsePen = new Pen(Color.FromArgb(fa, fr, fg, fb)); // innerElipsePen = new Pen(Color.FromArgb(fa, fa, fa, (byte)fa), 2);
                        //innerElipsePen = new Pen(Color.FromArgb(255, 0, 255, 255), 1); 
                        //innerElipsePen = new Pen(Color.FromArgb(fa, fa, fa, (byte)fa), 2);
                    }
                }

                //////////////////////
                /////////  Cardinals
                /////////  //////  
                if (true)
                {
                    int iCardinal = 12;

                    for (double dRad = 0; isHandsOn.Checked & (dRad < Math.PI * 2); dRad += Math.PI / 6)
                    {

                        //iRed += 50;
                        doSpoke(dRad - (Math.PI / 2), e.Graphics, 0.8f, 0.976f, 1, 3, Color.White, false, false, "", false);
                        iCardinal++; iCardinal = (iCardinal > 12) ? iCardinal - 12 : iCardinal;
                    }

                    ////////// numbers //
                    iCardinal = 12;
                    for (double dRad = 0; isHandsOn.Checked & dRad < Math.PI * 2; dRad += Math.PI / 6)
                    {

                        //iRed += 50;
                        if (isHandsOn.Checked)
                            doSpoke(dRad - (Math.PI / 2), e.Graphics, 0.65f, 0.7f, 1, 0, Color.White, false, false, "", rotateToolStripMenuItem.Checked);

                        iCardinal++; iCardinal = (iCardinal > 12) ? iCardinal - 12 : iCardinal;

                    }
                }
                /////////////////////
                float fMinute = (float)((DateTime.Now.Minute) - 15) / 60;
                fMinute += ((float)(DateTime.Now.Second) / 60) / 60;
                fMinute *= (float)(Math.PI * 2);
                float fEndSize = this.Width / 90;
                if (isHandsOn.Checked)
                {
                    if (isEndsOn)
                    {
                        if (AmInverse)
                        {
                            doSpoke(fMinute, e.Graphics, 0.061f, 0.87f, 1, 10, MinsHandColor.Color, true, isEndsOn, "", false);
                            doSpoke(fMinute, e.Graphics, 0.061f, 0.8345f, 1, 6, MainBackColor.Color, false, isEndsOn, "", false);
                        }
                        else
                        {
                            doSpoke(fMinute, e.Graphics, 0.061f, 0.87f, 1, 10, MainBackColor.Color, true, isEndsOn, "", false);
                            doSpoke(fMinute, e.Graphics, 0.061f, 0.88f, 1, 6, MinsHandColor.Color, false, isEndsOn, "", false);
                        }
                    }
                    else
                    {
                        if (IsInverse.Checked)
                        {
                            doSpoke(fMinute, e.Graphics, 0.061f, 0.90f, 1, 14, MinsHandColor.Color, true, isEndsOn, "", false);
                            doSpoke(fMinute, e.Graphics, 0.061f, 0.89f, 1, 2, MainBackColor.Color, false, isEndsOn, "", false);
                        }
                        else
                        {
                            doSpoke(fMinute, e.Graphics, 0.061f, 0.87f, 1, 18, MainBackColor.Color, true, isEndsOn, "", false);
                            doSpoke(fMinute, e.Graphics, 0.061f, 0.88f, 1, 6, MinsHandColor.Color, false, isEndsOn, "", false);
                        }
                    }
                }

                float fHours = (float)((DateTime.Now.Hour) - 15) / 12;
                fHours += (((float)DateTime.Now.Minute) / 60) / 12;
                fHours *= (float)(Math.PI * 2);
                if (isHandsOn.Checked)
                {
                    if (IsInverse.Checked)
                    {
                        doSpoke(fHours, e.Graphics, 0.05f, 0.65f, 1, 12, HourHandColor.Color, true, isEndsOn, "", false);
                        doSpoke(fHours, e.Graphics, 0.05f, 0.65f, 1, 1, MainBackColor.Color, true, isEndsOn, "", false);
                    }
                    else
                    {
                        //  doSpoke(fHours, e.Graphics, 0.05f, 0.45f, 1, 10, MainBackColor.Color, true, isEndsOn, "", false);
                        doSpoke(fHours, e.Graphics, 0.05f, 0.65f, 1, 2, HourHandColor.Color, true, isEndsOn, "", false);
                    }
                }

                sTime = DateTime.Now.ToLongTimeString();
                Font fontFon = new Font(Font.FontFamily, 64, FontStyle.Regular);
                Font fontTimerVal = new Font(FontFamily.GenericMonospace, 24, FontStyle.Bold);
                Font fontCountdown = new Font(FontFamily.GenericMonospace, 24, FontStyle.Bold);


                string sMinutes = sTime.Substring(sTime.IndexOf(':') + 1).Substring(0, 2);
                /////////////////////////////////////
                //sMinutes = sTime; /////////  JUST FOR FUN
                /////////////////////////////////////
                /////////////////////////////////////
                //float stringWid = e.Graphics.MeasureString(sTime.Substring(sTime.Length - 2, 2), fontFon).Width;
                float minuteWidH = e.Graphics.MeasureString(sMinutes, fontFon, 400).Width / 2;
                float minuteWid = e.Graphics.MeasureString(sMinutes, fontFon, 400).Width;
                float minuteHeiH = e.Graphics.MeasureString(sMinutes, fontFon, 400).Height / 2;
                float TimeHeiH = e.Graphics.MeasureString(sTime, fontFon, 400).Width / 2;

                sTime = isSeconds.Checked ? DateTime.Now.ToString("HH:mm:ss\r\nddd dd MMM") : DateTime.Now.ToString("HH:mm");
                if (isTextOn.Checked)
                {

                    SizeF szTimef = timeTextSize(e.Graphics);
                    //szTimef.Width *= 2; szTimef.Height = 2;

                    float offsetX = szTimef.Width / 2, offsetY = szTimef.Height / 2;
                    float timeX = xHMax - offsetX, timeY = yHMax - offsetY;
                    timeX = xHMax; timeY = 16;
                    if (is7segment.Checked)
                    {
                        if (segClock == null)
                        {
                            segClock = new SegmentClockCtl(this);
                            segClock.AutoScaleMode = AutoScaleMode.None;
                            //segClock.BackColor = Color.Transparent;
                            segClock.OwnerForm = this;
                            segClock.Margin = new Padding(2);

                            //this.Controls.Add(segClock);
                            segClock.Size = this.Size; segClock.Visible = true;
                            segClock.AutoSizeChanged += SegClock_AutoSizeChanged;
                            segClock.Height -= 54;
                            segClock.Width -= 48;
                            segClock.Location = new Point(16, 8);
                        }

                        Bitmap bmpSegClock = new Bitmap(segClock.Width, segClock.Height);
                        if (AmTransparent7Seg)
                        {
                            Graphics.FromImage(bmpSegClock).Clear(TransparentColor);
                            this.TransparencyKey = TransparentColor;
                            bmpSegClock.MakeTransparent(TransparentColor);
                        }
                        else
                        {
                            Graphics.FromImage(bmpSegClock).Clear(ColorSchemes[ColorSchemeNo][(int)SchemeMembers._7SegBack].Color);
                        }
                        segClock.DoPaint(Graphics.FromImage(bmpSegClock));
                        //cvbnvcnb;
                        e.Graphics.DrawImage(bmpSegClock, SegClockPos);


                        //segClock.TDigitClock_Tick(segClock, new EventArgs() { });
                        //Graphics gi = Graphics.FromHdc(segClock.CreateGraphics().GetHdc());
                        //Image i = Image.FromHbitmap( segClock.Handle);
                        //e.Graphics.DrawImage(i, 0, 0);
                    }

                    else
                    {
                        if (segClock != null)
                        {
                            {
                                segClock.Visible = false;
                                segClock.Dispose();
                                segClock = null;
                            }
                        }
                        if (!Am7Seg)
                        {
                            if (IsInverse.Checked)
                            {

                                e.Graphics.DrawString(sTime, TimeFont, FaceTextColor, timeX, timeY, noWrapCentredFormat);
                                e.Graphics.DrawString(sTime, TimeFont, FaceTextColor, timeX + 3, timeY, noWrapCentredFormat);
                                e.Graphics.DrawString(sTime, TimeFont, FaceTextColor, timeX, timeY + 3, noWrapCentredFormat);
                                e.Graphics.DrawString(sTime, TimeFont, FaceTextColor, timeX + 3, timeY + 3, noWrapCentredFormat);
                                if (!filledText.Checked) e.Graphics.DrawString(sTime, TimeFont, MainPaintColor, timeX, timeY, noWrapCentredFormat);
                            }
                            else
                            {
                                e.Graphics.DrawString(sTime, TimeFont, MainPaintColor, timeX, timeY, noWrapCentredFormat);
                                e.Graphics.DrawString(sTime, TimeFont, MainPaintColor, timeX + 3, timeY, noWrapCentredFormat);
                                e.Graphics.DrawString(sTime, TimeFont, MainPaintColor, timeX, timeY + 3, noWrapCentredFormat);
                                e.Graphics.DrawString(sTime, TimeFont, MainPaintColor, timeX + 3, timeY + 3, noWrapCentredFormat);
                                if (!filledText.Checked) e.Graphics.DrawString(sTime, TimeFont, FaceTextColor, timeX, timeY, noWrapCentredFormat);

                            }
                        }


                        int undYPos = (int)yMax - 5;
                        for (int nX = 0; nX < xMax; nX += 1)
                        {
                            e.Graphics.DrawEllipse(Pens.Aqua, nX, undYPos, 10, 10);
                        }
                        if (1 == 2)
                        {
                            for (float nSin = 0; nSin < 100; nSin += 1f)
                            {
                                float nMod = (int)(((Math.Sin(textUnderlineXPos) / 30) + 2) * 10);
                                e.Graphics.DrawEllipse(Pens.Beige, (textUnderlineXPos + nSin) % (int)xMax, undYPos - (nMod + 20), 40, 40);
                                textUnderlineXPos += 0.1f; textUnderlineXPos %= (int)xMax;
                            }
                        }
                    }

                    if ((DateTime.Now - ShowIntvaltimer).TotalSeconds < 3)
                    {
                        e.Graphics.DrawString(refreshTimer.Interval.ToString(), fontTimerVal, Brushes.Wheat, 10f, 10f);

                    }
                    fontTimerVal.Dispose();
                    TimeSpan tsDiff = (DateTime.Parse("5/11/2024 05:00") - DateTime.Now);
                    string sDaysToGo = tsDiff.Days.ToString() + "d:" + tsDiff.Hours + "h:" + tsDiff.Minutes + "m";

                    //if ((DateTime.Now.Millisecond ) <= 5) 
                    SizeF rEl = e.Graphics.MeasureString(sDaysToGo, fontCountdown, 1000);
                    try
                    {
                    }
                    catch { }

                    //e.Graphics.DrawString(sMinutes, fontFon, Brushes.White, (xMax - 10) - (minuteWid), 10);
                    fontTimerVal.Dispose();

                }
            }
            catch { }
            //
            //pictureBox1.ResumeLayout();


        }

        private void SegClock_AutoSizeChanged(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        float textUnderlineXPos = 20;
        void doSpoke(double dRads, Graphics eg, float fStart, float fEnd, int stepsize, int spokeWidth, Color color, bool backLight, bool drawEnds, string sSpoke, bool rotateText)
        {

            try
            {
                List<PointF> ptslist = new List<PointF>();
                //ptslist.Add(new PointF(xHMax, yHMax));
                int degrees = (int)(dRads / Math.PI * 180);
                for (float edgeMargin = fStart; edgeMargin <= fEnd; edgeMargin += 0.05f)
                {
                    float x2 = (float)Math.Cos(dRads);
                    x2 *= (xHMax * edgeMargin);
                    float y2 = (float)Math.Sin(dRads);
                    y2 *= (yHMax * edgeMargin);
                    x2 += xHMax; y2 += yHMax;

                    ptslist.Add(new PointF(x2, y2));
                }

                PointF[] pts = ptslist.ToArray();
                PointF lastPoint = new PointF(0, 0);
                for (int iLine = 1; iLine < pts.Length; iLine += stepsize)
                {
                    //if((iLine & 2)==2) 
                    NealsPen2.Width = spokeWidth;

                    //NealsPen2.Color = Color.FromArgb(255, (int)iRed, (int)iGreen, (int)iBlue);

                    if (backLight)
                    {
                        if (IsInverse.Checked) NealsPen2.Color = Color.Black;
                        else NealsPen2.Color = Color.White;
                        NealsPen2.Width += 4;
                        eg.DrawLine(NealsPen2, pts[iLine - 1], pts[iLine]);
                        NealsPen2.Width -= 4;
                    }


                    NealsPen2.Color = (color == Color.DarkKhaki) ? Color.FromArgb(255, (int)iRed, (int)iGreen, (int)iBlue) : color;
                    if (sSpoke == "") eg.DrawLine(NealsPen2, pts[iLine - 1], pts[iLine]);
                    lastPoint = pts[iLine];
                }
                if (sSpoke != "")
                {
                    int iNumber = int.Parse(sSpoke);
                    if (isNumerals.Checked)
                        sSpoke = numerals[iNumber];
                    if (!isNumbers.Checked) sSpoke = "";
                    Size charsize = eg.MeasureString(sSpoke, cardinalFont).ToSize();
                    Bitmap b = new Bitmap(charsize.Width, charsize.Height);
                    Graphics gb = Graphics.FromImage(b);
                    float cX = 5; float cY = 5;

                    cX = lastPoint.X - (b.Width / 2);
                    cY = lastPoint.Y - (b.Height / 2);

                    if (rotateText)
                    {
                        Matrix x = new Matrix();
                        if (iNumber <= 3 || iNumber > 8)
                        {
                            x.RotateAt((float)(((dRads + (Math.PI / 2)) / Math.PI) * 180), new PointF(b.Width / 2, (b.Height / 2)));
                            gb.Transform = x;
                        }
                        else if (iNumber > 3 || iNumber < 8)
                        {
                            x.RotateAt(180 + (float)(((dRads + (Math.PI / 2)) / Math.PI) * 180), new PointF(b.Width / 2, (b.Height / 2)));
                            gb.Transform = x;
                        }
                        else
                        {
                            //  x.RotateAt(90 + (float)(((dRads + (Math.PI / 2)) / Math.PI) * 180), new PointF(b.Width / 2, (b.Height / 2)));
                            // gb.Transform = x;
                        }
                    }
                    gb.DrawString(sSpoke, cardinalFont, FaceTextColor, 0, 0);
                    //gb.RotateTransform((float) (dRads * 180));

                    ///////

                    //'rotates in the center 

                    ///////


                    eg.DrawImage(b, cX, cY);


                }
                if (drawEnds)
                {
                    float fEnds2 = this.Width / 24;
                    float fEnds22 = fEnds2 / 2;
                    eg.FillEllipse(HandEndColor, lastPoint.X - fEnds22, lastPoint.Y - fEnds22, fEnds2, fEnds2);
                    eg.DrawEllipse(Pens.White, lastPoint.X - fEnds22, lastPoint.Y - fEnds22, fEnds2, fEnds2);


                }
            }
            catch { }
        }
        Font cardinalFont = new Font(FontFamily.GenericSansSerif, 36, FontStyle.Bold);
        private void NSXClock_Resize(object sender, EventArgs e)
        {

            if (segClock != null)
            {

                this.BackColor = Color.Black;

                if (isQSevenSeg.Checked)
                {
                    segClock.Size = new Size((this.Width / 4), (this.Height / 4));
                    SegClockPos = new PointF((this.Width / 64), (this.Height / 64));
                }
                else if (isHalfSevenSeg.Checked)
                {
                    segClock.Size = Size.Round(new SizeF((this.Width * 0.5f), (this.Height * 0.5f)));
                    SegClockPos = new PointF((this.Width / 8), (this.Height / 8));
                }
                else if (isfullSevenSeg.Checked)
                {
                    segClock.Size = new Size((this.Width), (this.Height));
                    SegClockPos = new PointF(2, 2);
                }
                else segClock.Size = new Size(this.Width - 4, this.Height - 4);

                SegClockRectangle = new Rectangle((int)SegClockPos.X, (int)SegClockPos.Y, segClock.Size.Width, segClock.Size.Height);
                segClock.Visible = true;
                segClock.AutoScaleMode = AutoScaleMode.None;
                if (isTitleBarOn) segClock.Height -= 24;
                else segClock.Height -= 32;
                segClock.Width -= 24;

                segClock.Location = new Point(8, 8);
            }
            float tf = this.Width / 10;
            tf += 2;
            if (isAutoTextSize) TimeFont = new Font(this.Font.FontFamily, (float)tf, FontStyle.Bold);
            saveSize();
            Face = null;
            ClockGlobals.MainBMP = null;
            ClockPBox.Invalidate();
            if (segClock != null)
                SegClockRectangle = new Rectangle(Point.Round(SegClockPos), segClock.Size);
        }
        void drawDot(double dRads, Graphics eg, float fPos, int spokeWidth, Color color)
        {

            try
            {
                List<PointF> ptslist = new List<PointF>();
                //ptslist.Add(new PointF(xHMax, yHMax));
                int degrees = (int)(dRads / Math.PI * 180);
                float edgeMargin = fPos;

                float x2 = (float)Math.Cos(dRads);
                x2 *= (xHMax * edgeMargin);
                float y2 = (float)Math.Sin(dRads);
                y2 *= (yHMax * edgeMargin);
                x2 += xHMax; y2 += yHMax;

                ptslist.Add(new PointF(x2, y2));
                ptslist.Add(new PointF(x2 + 1, y2));


                PointF[] pts = ptslist.ToArray();
                PointF lastPoint = new PointF(0, 0);
                NealsPen2.Width = spokeWidth;
                //NealsPen2.Color = Color.FromArgb(255, (int)iRed, (int)iGreen, (int)iBlue);
                NealsPen2.Color = (color == Color.DarkKhaki) ? Color.FromArgb(255, (int)iRed, (int)iGreen, (int)iBlue) : color;
                Brush nealsBrush = new SolidBrush(NealsPen2.Color);
                //eg.DrawLine(NealsPen2, pts[0], pts[1]);
                eg.FillEllipse(nealsBrush, pts[0].X, pts[0].Y, spokeWidth, spokeWidth);
            }
            catch { }
        }
        bool isMaximised = false;
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (this.isMaximised)
            {
                isMaximised = false;
                this.Location = preMaxPos;
                this.Size = preMaxSize;
            }
            else
            {
                isMaximised = true;
                preMaxPos = this.Location;
                preMaxSize = this.Size;
                Rectangle screenSize = Screen.FromControl(this).WorkingArea;
                this.Location = new Point(0, 0);
                this.Size = new Size(screenSize.Width, screenSize.Height);
            }
        }
        Point preMaxPos = ClockSettings.Default.ClockLocation;
        Size preMaxSize = ClockSettings.Default.ClockSize;
        private void spiralOptionsMenu_Click(object sender, EventArgs e)
        {

        }

        private void webserverToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ShowIntvaltimer = DateTime.Now;
            if (!webserverCheck.Checked) { webServer.Close(); webServer = null; }
            if (webserverCheck.Checked)
            {
                if (webServer == null)
                {
                    webServer = new NSHTTP(888, this);
                    webServer.OwnerPicBox = this.ClockPBox;
                    //
                }
                webServer.Start();

            }
        }

        private void webserverToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        public ClockIPLog logForm = null;
        private void showLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showLogToolStripMenuItem.Checked)
            {
                if (logForm == null)
                {
                    logForm = new ClockIPLog(this);
                    logForm.Show();
                    logForm.FormClosed += LogForm_Closed;
                }
            }
            else
            {
                logForm?.Close();
                logForm = null;
            }
        }

        private void LogForm_Closed(object sender, FormClosedEventArgs e)
        {
            logForm = null;
            showLogToolStripMenuItem.Checked = false;
        }

        void isInverse_Click(object sender, EventArgs e)
        {
            Face = null;
            ClockSettings.Default.IsInverse = IsInverse.Checked;
            ClockSettings.Default.Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void schemePick_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isStarted)
            {
                try
                {
                    innerElipsePen?.Dispose();
                    innerElipsePen = null;
                }
                catch { }
            }
            ColorSchemeNo = (SchemePick.SelectedIndex);
            Face = null;
            try
            {
                PaintEventArgs peNew = new PaintEventArgs(Graphics.FromImage(ClockGlobals.MainBMP), new Rectangle(0, 0, ClockGlobals.MainBMP.Width, ClockGlobals.MainBMP.Height));
                doMakeFrame(peNew);
            }
            catch { }
            if (isStarted)
            {
                ClockSettings.Default.ColorListIndex = SchemePick.SelectedIndex;
                ClockSettings.Default.Save();
            }
        }

        private void spiralOptionsMenu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        void setFormSize(float x, float y)
        {
            Width = (int)(x * 1.15f); Height = (int)(y * 1.15f);
        }
        bool isShowHands
        {
            get { return isHandsOn.Checked; }
            set { isHandsOn.Checked = value; }
        }
        private void isHandsOn_Click(object sender, EventArgs e)
        {
            if (isShowHands)
            {
                this.MinimumSize = new Size(100, 100);
                isTextOn.Checked = true;
            }
            if (is7segment.Checked)
            {
                if (segClock != null)
                    this.MinimumSize = new Size((int)segClock.SegmentsCount * 44, 88);
            }
            if (isTextOn.Checked && !isShowHands)
            {
                SizeF sf = timeTextSize(ClockPBox.CreateGraphics());
                if (isStarted)
                {
                    setFormSize(sf.Width + 80, sf.Height + 40);
                }
            }

            Face = null;
            if (!isShowHands)
            {
                refreshTimer.Interval = 250;
            }
            ClockSettings.Default.IsHands = isShowHands;
            ClockSettings.Default.IsText = isTextOn.Checked;
            ClockSettings.Default.Save();
        }
        private void isAnimated_Click(object sender, EventArgs e)
        {
            //if (isAnimated.Checked) vScrollBar1.Visible = true; 
            //else                    vScrollBar1.Visible = false;

            if (isAnimated.Checked & !Am7Seg)
            {
                SetTimerInterval(50);
            }
            else if (isAnimated.Checked & Am7Seg)
            {
                SetTimerInterval(20);
            }
            if (!isAnimated.Checked & !AmAnalogue)
            {
                SetTimerInterval(1000);
            }
            ClockSettings.Default.IsAnimated = isAnimated.Checked;
            ClockSettings.Default.Save();
        }

        private void isTextOn_Click(object sender, EventArgs e)
        {
            if (!isTextOn.Checked) isShowHands = true;

            ClockSettings.Default.IsText = isTextOn.Checked;
            ClockSettings.Default.IsHands = isHandsOn.Checked;
            ClockSettings.Default.Save();
            if (isTextOn.Checked && !isShowHands)
            {
                this.Size = (timeTextSize(this.CreateGraphics()) * 1.2f).ToSize();
            }
        }

        private void isAnalog_Click(object sender, EventArgs e)
        {
            Face = null;
            if (AmAnalogue)
            {
                isSeconds.Checked = true;
                ClockSettings.Default.IsSeconds = isSeconds.Checked;
                ClockSettings.Default.Save();
            }
            if (AmAnalogue & !isAnimated.Checked)
            {
                SetTimerInterval(60);
            }
            if (!AmAnalogue & !isAnimated.Checked)
            {
                SetTimerInterval(1000);
            }
        }

        private void isNumbers_CheckedChanged(object sender, EventArgs e)
        {
            Face = null;
            ClockSettings.Default.IsNumbers = isNumbers.Checked;
            ClockSettings.Default.Save();
        }

        private void isOrdinals_Click(object sender, EventArgs e)
        {
            Face = null;
            ClockSettings.Default.IsOrdinals = isOrdinals.Checked;
            ClockSettings.Default.Save();
        }
        int nLastInterval = 0;
        public void SetTimerInterval(int i)
        {
            i = Math.Min(i, 2500);
            if (nLastInterval != i)
            {
                refreshTimer.Interval = i;
                ClockSettings.Default.TimerInterval = i;
                ShowIntvaltimer = DateTime.Now;
                ClockSettings.Default.Save();
                nLastInterval = i;
                T_Tick(this, new EventArgs());
            }
        }
        public int GetTimerInterval()
        { return refreshTimer.Interval; }
        private void isSeconds_Click(object sender, EventArgs e)
        {
            if (isSeconds.Checked)
            {
                isAnalog.Visible = true;
                if (segClock != null) segClock.SegmentsCount = 8;
                NSXClock_Resize(this, e);

            }
            else
            {
                isAnalog.Checked = false;
                if (segClock != null)
                    segClock.SegmentsCount = 5;


                NSXClock_Resize(this, e);
            }
            ClockSettings.Default.IsSeconds = isSeconds.Checked;
            ClockSettings.Default.Save();
        }

        public class NSHTTP
        {
            static ClockGlobals httpGlobals = new ClockGlobals();
            HttpListener serverSocket;
            public PictureBox OwnerPicBox;

            NESixClock ownerForm;
            int portNum = 0;
            public NSHTTP(int port, NESixClock nSXClock)
            {
                ownerForm = nSXClock;
                portNum = port;
            }
            TaskFactory tfServer;
            public void Start()
            {
                serverSocket = new HttpListener();
                tfServer = new TaskFactory();
                tfServer.StartNew(doServerLoop);
            }
            bool ServerOK = true;
            public void Close()
            {
                try
                {
                    serverSocket.Close();
                }
                catch { }
                ServerOK = false;
                tfServer = null;
            }
            void showError(string Message)
            {
                MessageBox.Show(Message);
                ownerForm.webserverCheck.Checked = false;
            }

            private delegate void showErrDel(string Message);
            async void doServerLoop()
            {
                try
                {
                    //serverSocket.Prefixes.Add("http://*:888/"); 
                    serverSocket.Prefixes.Add("http://*:" + portNum.ToString().Trim() + "/");
                    serverSocket.IgnoreWriteExceptions = true;
                    System.Security.Authentication.ExtendedProtection.ServiceNameCollection lp = serverSocket.DefaultServiceNames;
                    serverSocket.Start();

                    //this.Close();
                }
                catch (Exception ee)
                {
                    ownerForm.Invoke(new showErrDel(showError), new object[] { ee.ToString() });
                }
                while (ServerOK)
                {
                    try
                    {
                        HttpListenerContext res = await serverSocket.GetContextAsync();
                        string sH = HttpResponseHeader.Server.ToString();
                        ClockIPLog.IPLogEntry le = new ClockIPLog.IPLogEntry();
                        le.sPort = res.Request.RemoteEndPoint.Port.ToString();
                        le.sAddr = res.Request.RemoteEndPoint.Address.ToString();
                        le.DateTime = DateTime.Now;
                        le.sURL = Uri.UnescapeDataString(res.Request.Url.AbsoluteUri);
                        ownerForm.logEntries.Add(le);
                        if (ownerForm != null)
                        {
                            try
                            {
                                if (ownerForm.logForm != null)
                                {
                                    ownerForm.logForm.UpdateLogList();
                                    while (ownerForm.logEntries.Count > 16)
                                    { ownerForm.logEntries.RemoveAt(0); }
                                }
                            }
                            catch { }
                        }
                        res.Response.AddHeader(sH, "NEALS");

                        ContextProcessor cp = new ContextProcessor(res, OwnerPicBox, ClockGlobals.MainBMP);
                    }
                    catch { ServerOK = false; }
                }
            }
            public static void Startup()
            {

            }
        }
        public class ContextProcessor
        {
            public HttpListenerContext HLC;
            public PictureBox ownerPicBx;
            public Bitmap webImage;
            public ContextProcessor(HttpListenerContext hlc, PictureBox pbox, Bitmap backBitmap)
            {
                ownerPicBx = pbox;
                HLC = hlc;
                new TaskFactory().StartNew(DoServing);
                webImage = ClockGlobals.MainBMP;
            }
            bool isThreadOK = true;
            async public void DoServing()
            {
                byte[] bOut;
                while (isThreadOK)
                {
                    try
                    {
                        byte[] bIn = new byte[4096];
                        string rURI = HLC.Request.Url.LocalPath;
                    }
                    catch (Exception ex)
                    {
                        HLC.Response.StatusCode = 500;
                        string sOut1 = "<html><head></head>ERROR<hr/>" + ex.ToString() + "<hr/></html>";
                        bOut = Encoding.ASCII.GetBytes(sOut1.ToCharArray(), 0, sOut1.Length);
                        HLC.Response.Headers.Add("Content-Length", bOut.Length.ToString());
                        HLC.Response.Headers.Add("Cache-Control", "no-store");
                        HLC.Response.Headers.Add("Expires", "0");


                        string s = "";
                        foreach (HttpResponseHeader hs in HLC.Response.Headers)
                        {
                            s += hs.ToString();
                        }
                        HLC.Response.OutputStream.Write(bOut, 0, bOut.Length);

                        return;
                    }
                    HLC.Response.StatusCode = 200;
                    string sOut = "<html><head></head><hr/>" + "OK" + "<hr/></html>";
                    string sFile = HLC.Request.Url.Segments[HLC.Request.Url.Segments.Count() - 1];
                    string sExt = "htm";
                    if (!sFile.Contains(".")) sFile = "neals3.htm";
                    sExt = sFile.Substring(sFile.LastIndexOf("."));
                    string sLocalFile = sFile;
                    string sURL = HLC.Request.RawUrl;
                    string sParams = "";
                    foreach (string s in HLC.Request.QueryString)
                    {
                        sParams += HLC.Request.QueryString[s];
                    }
                    // (HLC.Request.Url.LocalPath.EndsWith(".jpg"))
                    bOut = new byte[0];
                    switch (sExt.ToLower())
                    {
                        case (".jpg"):
                            {
                                int clientWidth = 300, clientHeight = 300;
                                try
                                {
                                    clientWidth = int.Parse(HLC.Request.QueryString["Width"].Replace("\"", ""));
                                    clientHeight = int.Parse(HLC.Request.QueryString["Height"].Replace("\"", ""));
                                }
                                catch { }
                                Graphics g = ownerPicBx.CreateGraphics();
                                Bitmap bm = new Bitmap((int)(clientWidth * 0.9), (int)(clientHeight * 0.8));
                                //Bitmap bm = new Bitmap(ownerPicBx.Size.Width, ownerPicBx.Size.Height);
                                lastBM = bm;

                                try
                                {
                                    if (webImage != null)
                                        Graphics.FromImage(lastBM).DrawImage(webImage, new Rectangle(0, 0, lastBM.Width, lastBM.Height), 0, 0, webImage.Width, webImage.Height, GraphicsUnit.Pixel);
                                }
                                catch { }

                                lastBM = bm;
                                MemoryStream mstr = new MemoryStream();
                                lastBM.Save(mstr, ImageFormat.Jpeg);
                                mstr.Seek(0, 0);
                                bOut = new BinaryReader(mstr).ReadBytes((int)mstr.Length);
                                //bOut = System.IO.File.ReadAllBytes("d:\\pics\\19000Views.jpg");
                                // HLC.Response.Headers.Set("content-length", bOut.Length.ToString());
                                HLC.Response.Headers.Add("Content-Type", "image/jpeg");

                                break;
                            }
                        case ".htm":
                            {
                                string ssss = "";
                                try
                                {
                                    bOut = File.ReadAllBytes(sLocalFile);
                                    ssss = Encoding.ASCII.GetString(bOut);
                                    ssss = ssss.Replace("<NSXTime/>", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                                    ssss = ssss.Replace("<NSXDate/>", DateTime.Now.ToLongDateString());
                                    bOut = Encoding.ASCII.GetBytes(ssss);
                                }
                                catch (Exception eee)
                                {
                                    sOut = "<html><head></head><body>ERROR 404<hr/>\r\n";
                                    sOut += "<p>" + eee.Message + "</p><hr/>\r\n";
                                    sOut += "</body></html>\r\n";
                                    bOut = Encoding.UTF8.GetBytes(sOut.ToCharArray(), 0, sOut.Length);
                                    HLC.Response.StatusCode = 404;
                                }
                                HLC.Response.ContentEncoding = Encoding.UTF8;
                                HLC.Response.Headers.Add("Content-Type", "text/html");
                                break;
                            }
                    }
                    HLC.Response.ContentLength64 = bOut.Length;
                    HLC.Response.Headers[HttpResponseHeader.Expires] = "0";

                    HLC.Response.Headers.Remove(HttpResponseHeader.Date);

                    HLC.Response.Headers[HttpResponseHeader.Date] = "11 Oct 2021 13:09:15 GMT";
                    HLC.Response.Headers[HttpResponseHeader.Server] = "NEALS";
                    HLC.Response.Headers[HttpResponseHeader.CacheControl] = "no-cache, no-store";
                    HLC.Response.Headers[HttpResponseHeader.Pragma] = "no-cache";
                    string ss = HLC.Response.Headers[HttpResponseHeader.Date];
                    HLC.Response.Headers[HttpResponseHeader.Connection] = "close";
                    //HLC.Response.cach
                    try
                    {
                        HLC.Response.OutputStream.Write(bOut, 0, bOut.Length);
                    }
                    catch { }
                    return;
                    break;
                }
            }
            Bitmap lastBM;

            private delegate void ddd();

            void doDraw()
            {
                //ownerPicBx.DrawToBitmap(lastBM, new Rectangle(0,0,300,300));

            }
        }

        private void isNumbers_Click(object sender, EventArgs e)
        {
            ClockSettings.Default.IsNumbers = isNumbers.Checked;
            ClockSettings.Default.Save();
        }

        private void isNumerals_Click(object sender, EventArgs e)
        {

            ClockSettings.Default.IsNumerals = isNumerals.Checked;
            ClockSettings.Default.Save();
            Face = null;
        }

        private void isDateShown_Click(object sender, EventArgs e)
        {
            ClockSettings.Default.IsDateTitle = isDateShown.Checked;
            ClockSettings.Default.Save();
            Face = null;
        }

        private void NSXClock_Move(object sender, EventArgs e)
        {
            saveSize();
            Face = null;
        }
        void saveSize()
        {
            if (isStarted)
            {
                ClockSettings.Default.ClockSize = this.Size;
                ClockSettings.Default.ClockLocation = this.Location;
                //Point p = ClockSettings.Default.ClockLocationX;
                ClockSettings.Default.Save();
            }
        }

        private void endsMenu_Click(object sender, EventArgs e)
        {
            ClockSettings.Default.IsEnds = isEnds.Checked;
            ClockSettings.Default.Save();
        }
        int textFontNo = 2;
        int[] textFontSizes = { 32, 48, 64 };

        private void SetTextFont(int tf)
        {
            TimeFont?.Dispose();
            textFontNo = tf; TimeFont = new Font(this.Font.FontFamily, (float)tf, FontStyle.Bold);
            if (isTextOn.Checked && !isHandsOn.Checked)
            {
                SizeF sf = timeTextSize(this.CreateGraphics());
                if (isStarted)
                {
                    setFormSize(sf.Width, sf.Height);
                }
            }
        }
        int textSizeMenuSelection = 3;
        private void smallFont_Click(object Sender, EventArgs e)
        {
            textSizeMenuSelection = 0;
            ClockSettings.Default.FontSetting = textSizeMenuSelection;
            ClockSettings.Default.Save();
            isAutoTextSize = false;
            switchOffTextsizeChecks();
            smallMenu.Checked = true;
            SetTextFont(32);
            if (isTextOn.Checked && !isHandsOn.Checked)
            {
                SizeF sf = timeTextSize(this.CreateGraphics());
                if (isStarted)
                {
                    setFormSize(sf.Width, sf.Height);
                }
            }
        }
        private void medFont_Click(object sender, EventArgs e)
        {

            textSizeMenuSelection = 1;
            ClockSettings.Default.FontSetting = textSizeMenuSelection;
            ClockSettings.Default.Save();
            isAutoTextSize = false;
            switchOffTextsizeChecks();
            medTextMenu.Checked = true;
            SetTextFont(64);
            if (isTextOn.Checked && !isHandsOn.Checked)
            {
                SizeF sf = timeTextSize(this.CreateGraphics());
                if (isStarted)
                {
                    setFormSize(sf.Width, sf.Height);
                }
            }
        }
        private void largeFont_Click(object sender, EventArgs e)
        {
            textSizeMenuSelection = 2;
            Clockv2.nn.ClockSettings.Default.FontSetting = textSizeMenuSelection;
            Clockv2.nn.ClockSettings.Default.Save();
            isAutoTextSize = false;
            switchOffTextsizeChecks();
            largeTextMenu.Checked = true;
            SetTextFont(128);
            if (isTextOn.Checked && !isHandsOn.Checked)
            {
                SizeF sf = timeTextSize(ClockPBox.CreateGraphics());
                if (isStarted)
                {
                    setFormSize(sf.Width, sf.Height);
                }
            }
        }
        private void largerFont_Click(object sender, EventArgs e)
        {
            textSizeMenuSelection = 3;
            Clockv2.nn.ClockSettings.Default.FontSetting = textSizeMenuSelection;
            Clockv2.nn.ClockSettings.Default.Save();
            isAutoTextSize = false;
            switchOffTextsizeChecks();
            largerTextMenu.Checked = true;
            SetTextFont(192);
            if (isTextOn.Checked && !isHandsOn.Checked)
            {
                SizeF sf = timeTextSize(ClockPBox.CreateGraphics());
                if (isStarted)
                {
                    setFormSize(sf.Width, sf.Height);
                }
            }
        }
        private void isCentreDome_Click(object sender, EventArgs e)
        {
            Face = null;
        }

        private void isFixedDome_Click(object sender, EventArgs e)
        {
            if (isFixedDome.Checked) isPulsedDome.Checked = false;
            Face = null;

            Clockv2.nn.ClockSettings.Default.IsFixedDome = isFixedDome.Checked;
            Clockv2.nn.ClockSettings.Default.IsPulsedDome = isPulsedDome.Checked;
            Clockv2.nn.ClockSettings.Default.Save();
        }

        private void isPulsedDome_Click(object sender, EventArgs e)
        {
            Clockv2.nn.ClockSettings.Default.IsFixedDome = isFixedDome.Checked;
            Clockv2.nn.ClockSettings.Default.IsPulsedDome = isPulsedDome.Checked;
            Clockv2.nn.ClockSettings.Default.Save();
            if (isPulsedDome.Checked)
            {
                isFixedDome.Checked = false;
                SetTimerInterval(20);
            }
            else
            {
                if (!isAnimated.Checked && !isSeconds.Checked)
                {
                    SetTimerInterval(10000);
                }
            }
            Face = null;

        }

        private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Face = null;
        }

        private void autoFontSize_Click(object sender, EventArgs e)
        {

            textSizeMenuSelection = 4;
            Clockv2.nn.ClockSettings.Default.FontSetting = textSizeMenuSelection;
            Clockv2.nn.ClockSettings.Default.Save();
            switchOffTextsizeChecks();
            autoTextSize.Checked = true;
            this.Invalidate(true);
            isAutoTextSize = true;
            this.Refresh();
            NSXClock_Resize(this, new EventArgs());
            Face = null;

        }
        bool isAutoTextSize
        {
            get { return autoTextSize.Checked; }
            set { autoTextSize.Checked = value; }
        }
        void switchOffTextsizeChecks()
        {
            largeTextMenu.Checked = false;
            largerTextMenu.Checked = false;
            medTextMenu.Checked = false;
            smallMenu.Checked = false;
            autoTextSize.Checked = false;
        }

        private void NSXClock_DoubleClick(object sender, EventArgs e)
        {

        }

        private void makeTests_Click(object sender, EventArgs e)
        {

        }
        AlarmsForm af;
        public void shoAlarms()
        {
            if (af == null) af = new AlarmsForm() { Left = (this.Left + ((int)(Width / 4))), Top = this.Top };
            af.MinimumSize = new Size(300, 300);
            Application.Run(af);
        }
        ScheduleForm acf;
        public void shoAlarmsCal()
        {
            if (acf == null)
            {
                acf = new ScheduleForm() { Left = (this.Left + ((int)(Width / 4))), Top = this.Top };
                Application.Run(acf);
            }
            else
                acf.ShowFormSafely();
        }

        private void dailyWeeklyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TaskFactory().StartNew(shoAlarms);
        }

        private void Calendar_Click(object sender, EventArgs e)
        {
            new TaskFactory().StartNew(shoAlarmsCal);
        }

        private void is7segment_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)is7segment).Checked)
            {
                if(segClock!=null) segClock.AutoScaleMode = AutoScaleMode.None;
                this.AutoScaleMode = AutoScaleMode.None;
                NSXClock_Resize(this, new EventArgs());
                if(isfullSevenSeg.Checked) isfullSevenSeg_Click(sender, EventArgs.Empty);
                this.Invalidate();
            }
            else
            {
                if (segClock != null)
                {
                    this.Controls.Remove(segClock);
                    segClock.Visible = false;
                    segClock.Dispose();

                }
                ;
            }
            ClockSettings.Default.IsSevenSeg = is7segment.Checked;
            ClockSettings.Default.Save();
        }

        private void TopLeft7SegMenu_Click(object sender, EventArgs e)
        {
            isfullSevenSeg.Checked = !isQSevenSeg.Checked;
            NSXClock_Resize(this, new EventArgs());
        }

        private void isfullSevenSeg_Click(object sender, EventArgs e)
        {
            if (isfullSevenSeg.Checked)
            {
                isQSevenSeg.Checked = false;
                isHalfSevenSeg.Checked = false;
            }
            else
            {
                isQSevenSeg.Checked = false;
                //isHalfSevenSeg.Checked = true;
            }
            ClockSettings.Default.SevenSegFull = isfullSevenSeg.Checked;
            ClockSettings.Default.Save();
            //NSXClock_Resize(this, new EventArgs());
        }

        private void isHalfSevenSeg_Click(object sender, EventArgs e)
        {
            isfullSevenSeg.Checked = !isHalfSevenSeg.Checked;
            isQSevenSeg.Checked = false;
            NSXClock_Resize(this, new EventArgs());
            ClockSettings.Default.SevenSegHalf = isHalfSevenSeg.Checked;
            ClockSettings.Default.Save();
        }
        public bool IsMouseOverClock = false;
        private void ClockPBox_MouseHover(object sender, EventArgs e)
        {

            Point mouPos = new Point(Cursor.Position.X - this.Left, Cursor.Position.Y - this.Top);
            if (SegClockRectangle.Contains(mouPos))
            {
                IsMouseOverClock = true;
                //Cursor = Cursors.Default;
                Point pNew = new Point();
                pNew.X = Cursor.Position.X - lastMousePos.X;
                pNew.Y = Cursor.Position.Y - lastMousePos.Y;

                if (pNew.X != 0 || pNew.Y != 0)
                {
                    if (isMouseDown)
                    {
                        if (!(pNew.X > 15) && !(pNew.Y > 15) &&
                            !(pNew.X > 15) && !(pNew.Y < -15))
                        {
                            SegClockPos.X += pNew.X; SegClockPos.Y += pNew.Y;
                            if (SegClockPos.X < SegClockMargin) SegClockPos.X = SegClockMargin;
                            if (SegClockPos.Y < SegClockMargin) SegClockPos.Y = SegClockMargin;


                            //if(Too BIG) make less big


                            SegClockRectangle = new Rectangle(Point.Round(SegClockPos), segClock.Size);
                        }


                    }
                }
                lastMousePos = Cursor.Position;
            }
            else
            {
                IsMouseOverClock = false;
                Cursor = Cursors.Default;
            }
        }
        Point lastMousePos = new Point();
        bool isMouseDownLeftSizer = false, isMouseDownRightSizer = false, isMouseDownTopSizer = false, isMouseDownBottomSizer = false;
        enum sizeModes { none, leftX, rightX, topY, bottomY, move };
        sizeModes cursorSizeMode = sizeModes.none;
        public Size clockSize => ClockPBox.Size;
        public void ClockPBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Am7Seg) return;    // moving 7seg, no point t
            if (e.Button == MouseButtons.None &&
                (isMouseDownLeftSizer || isMouseDownRightSizer || isMouseDownTopSizer || isMouseDownBottomSizer)
                )
            {
                isMouseDown = false;
                // button released..
                isMouseDownLeftSizer = false; isMouseDownRightSizer = false;
                isMouseDownTopSizer = false; isMouseDownBottomSizer = false;
            }
            Point cursorPos = this.PointToScreen(e.Location);
            Rectangle sizeModeInnerRect;
            Rectangle sizeModeOuterRect = SegClockRectangle;

            sizeModeOuterRect.X -= 30; sizeModeOuterRect.Y -= 30; sizeModeOuterRect.Width += 30; sizeModeOuterRect.Height += 30;

            sizeModeInnerRect = sizeModeOuterRect;

            sizeModeInnerRect.X += 30; sizeModeInnerRect.Y += 30; sizeModeInnerRect.Width -= 60; sizeModeInnerRect.Height -= 60;

            if (sizeModeOuterRect.Contains(e.Location)
                || isMouseDownLeftSizer || isMouseDownRightSizer || isMouseDownTopSizer || isMouseDownBottomSizer)
            {
                if (isMouseDownLeftSizer || isMouseDownRightSizer || isMouseDownRightSizer || isMouseDownBottomSizer ||
                    sizeModeOuterRect.Contains(e.Location)

                    )    //Borders for mouse clock-sizing
                {
                    if (isMouseDownLeftSizer || (e.Location.X <= (sizeModeInnerRect.Left) && e.Location.X >= (sizeModeOuterRect.Left)))
                    {
                        cursorSizeMode = sizeModes.leftX;
                        Cursor = Cursors.SizeWE;
                        isMouseDownLeftSizer = isMouseDown;
                    }
                    if (isMouseDownTopSizer || (e.Location.Y >= (sizeModeOuterRect.Top) && e.Location.Y <= (sizeModeInnerRect.Top)))
                    {
                        cursorSizeMode = sizeModes.topY;
                        Cursor = Cursors.SizeNS;
                        isMouseDownTopSizer = isMouseDown;
                    }
                    if (isMouseDownRightSizer || (e.Location.X >= (sizeModeInnerRect.Right)) && (e.Location.X <= (sizeModeOuterRect.Right)))
                    {
                        cursorSizeMode = sizeModes.rightX;
                        Cursor = Cursors.SizeWE;
                        isMouseDownRightSizer = isMouseDown;
                    }
                    if (isMouseDownBottomSizer || (e.Location.Y >= (sizeModeInnerRect.Bottom) && e.Location.Y <= (sizeModeOuterRect.Bottom)))
                    {
                        cursorSizeMode = sizeModes.bottomY;
                        Cursor = Cursors.SizeNS;
                        if (isMouseDown) isMouseDownBottomSizer = isMouseDown;
                    }
                    if (sizeModeInnerRect.Contains(e.Location) &&
                        !(isMouseDownLeftSizer || isMouseDownRightSizer  // not these
                        || isMouseDownTopSizer || isMouseDownBottomSizer) // just for moving
                        )
                    {
                        cursorSizeMode = sizeModes.move;
                        try
                        {
                            Cursor = Cursors.NoMove2D;
                        }
                        catch { }
                    }
                }
            }
            else
            {
                cursorSizeMode = sizeModes.none;
                Cursor = Cursors.Default;
            }

            if (!sizeModeOuterRect.Contains(e.Location)
                &&
                !(isMouseDownLeftSizer || isMouseDownRightSizer || isMouseDownTopSizer || isMouseDownBottomSizer))
            {
                cursorSizeMode = sizeModes.none;
            }


            if (sizeModeOuterRect.Contains(e.Location))
            {
                IsMouseOverClock = true;

                Point pNew = new Point();
                pNew.X = e.Location.X - lastMousePos.X;
                pNew.Y = e.Location.Y - lastMousePos.Y;

                if (pNew.X != 0 || pNew.Y != 0)
                {
                    if (cursorSizeMode == sizeModes.move)
                    {
                        Cursor = Cursors.NoMove2D;
                        if (e.Button == MouseButtons.Left)
                        {
                            Cursor = Cursors.NoMove2D;
                            if (!(pNew.X > 765) && !(pNew.Y > 765) &&
                                !(pNew.X > 765) && !(pNew.Y < -765))
                            {
                                SegClockPos.X += pNew.X; SegClockPos.Y += pNew.Y;
                                if (SegClockPos.X < 0) SegClockPos.X = 2;
                                if (SegClockPos.Y < 0) SegClockPos.Y = 2;
                                SegClockRectangle = new Rectangle(Point.Round(SegClockPos), segClock.Size);
                            }
                        }
                        else Cursor = Cursors.NoMove2D;
                    }
                    //Cursor = Cursors.Default;
                    if (!sizeModeInnerRect.Contains(e.Location))
                    {
                        if (cursorSizeMode != sizeModes.none)
                        {
                            if (e.Button == MouseButtons.Left || isMouseDownLeftSizer || isMouseDownRightSizer || isMouseDownTopSizer || isMouseDownBottomSizer)
                            {
                                //   Cursor = Cursors.SizeAll;
                                if (!(pNew.X > 62) && !(pNew.Y > 62) &&
                                    !(pNew.X > 62) && !(pNew.Y < -62))

                                {
                                    if (cursorSizeMode == sizeModes.move)
                                    {
                                        SegClockPos.X += pNew.X;
                                        SegClockPos.Y -= pNew.Y;
                                        //cursorPos.X += pNew.X;
                                        //cursorPos.Y += pNew.Y;
                                    }

                                    if (cursorSizeMode == sizeModes.leftX || isMouseDownLeftSizer)
                                    {
                                        SegClockPos.X += pNew.X;
                                        segClock.Width -= pNew.X;
                                        isMouseDownLeftSizer = true;
                                        //cursorPos.X += pNew.X;
                                    }


                                    if (cursorSizeMode == sizeModes.rightX || isMouseDownRightSizer)
                                    {
                                        segClock.Width += pNew.X;
                                        //cursorPos.X += pNew.X;
                                        isMouseDownRightSizer = true;
                                    }
                                    if (cursorSizeMode == sizeModes.topY || isMouseDownTopSizer)
                                    {
                                        SegClockPos.Y += pNew.Y;
                                        segClock.Height -= pNew.Y;
                                        //cursorPos.Y += pNew.Y;
                                        isMouseDownTopSizer = true;
                                    }
                                    if (cursorSizeMode == sizeModes.bottomY || isMouseDownBottomSizer)
                                    {
                                        segClock.Height += pNew.Y;
                                        //cursorPos.Y += pNew.Y;
                                        isMouseDownBottomSizer = true;
                                    }


                                    float segClockRightMax = segClock.Width + segClock.Left;
                                    float segClockBottomMax = segClock.Height + segClock.Top;
                                    segClockBottomMax -= SegClockMargin; segClockRightMax -= SegClockMargin;


                                    if (SegClockPos.X < SegClockMargin) SegClockPos.X = SegClockMargin;
                                    if (SegClockPos.Y < SegClockMargin) SegClockPos.Y = SegClockMargin;


                                    if ((SegClockPos.X + segClock.Width) > (ClockPBox.Width - (SegClockMargin)))
                                        segClock.Width = segClock.Width - (int)(SegClockMargin);

                                    if ((SegClockPos.Y + segClock.Height) > (ClockPBox.Height - (SegClockMargin)))
                                        segClock.Height = segClock.Height - (int)(SegClockMargin);


                                    SegClockRectangle = new Rectangle(Point.Round(SegClockPos), segClock.Size);
                                    //Cursor.Position.Offset(new Point(this.Location.X  , this.Location.Y ));

                                    //Cursor.Position = Point.Round( cursorPos);
                                    ClockPBox.Invalidate();
                                    //                                    this.segClock.DoPaint(this.CreateGraphics());
                                }
                            }
                        }
                        if (cursorSizeMode == sizeModes.move)
                        {
                            if (e.Button == MouseButtons.Left || isMouseDown)
                            {
                                //   Cursor = Cursors.SizeAll;
                                if (!(pNew.X > 82) && !(pNew.Y > 82) &&
                                    !(pNew.X > 82) && !(pNew.Y < -82))

                                {
                                    if (cursorSizeMode == sizeModes.leftX)
                                    {
                                        SegClockPos.X += pNew.X;
                                        segClock.Width -= pNew.X;
                                    }
                                    if (cursorSizeMode == sizeModes.rightX)
                                    {
                                        segClock.Width += pNew.X;
                                    }
                                    if (cursorSizeMode == sizeModes.topY)
                                    {
                                        SegClockPos.Y += pNew.Y;
                                        segClock.Height -= pNew.Y;
                                    }
                                    if (cursorSizeMode == sizeModes.bottomY)
                                    {
                                        segClock.Height += pNew.Y;
                                    }




                                    if (SegClockPos.X < SegClockMargin) SegClockPos.X = SegClockMargin;
                                    if (SegClockPos.Y < SegClockMargin) SegClockPos.Y = SegClockMargin;

                                    if ((SegClockPos.X + segClock.Width) > ClockPBox.Width)
                                        segClock.Width = (int)(SegClockPos.X + segClock.Width) - (int)SegClockMargin;

                                    if ((SegClockPos.Y + segClock.Height) > ClockPBox.Height)
                                    {
                                        segClock.Height = (int)(SegClockPos.Y + segClock.Height) - (int)SegClockMargin;

                                    }

                                    SegClockRectangle = new Rectangle(Point.Round(SegClockPos), segClock.Size);
                                    this.ClockPBox.Invalidate();
                                }
                            }
                            ClockPBox.Invalidate();
                        }
                    }
                }
                lastMousePos = e.Location;
            }
            else
            {
                IsMouseOverClock = false;
                if (cursorSizeMode == sizeModes.none) Cursor = Cursors.Default;
            }
        }
        bool isMouseDown = false;
        private void NESixClock_MouseHover(object sender, EventArgs e)
        {
            if (SegClockRectangle.Contains(Cursor.Position))
            {
                IsMouseOverClock = true;
                //Cursor = Cursors.Default;
                Point pNew = new Point();
                pNew.X = Cursor.Position.X - lastMousePos2.X;
                pNew.Y = Cursor.Position.Y - lastMousePos2.Y;

                if (pNew.X != 0 || pNew.Y != 0)
                {
                    if (isMouseDown)
                    {
                        if (!(pNew.X > 5) && !(pNew.Y > 5) &&
                            !(pNew.X > 5) && !(pNew.Y < -5))
                        {
                            SegClockPos.X += pNew.X; SegClockPos.Y += pNew.Y;
                            if (SegClockPos.X < 0) SegClockPos.X = 2;
                            if (SegClockPos.Y < 0) SegClockPos.Y = 2;


                            if (SegClockPos.X < SegClockMargin) SegClockPos.X = 2;
                            if (SegClockPos.Y < SegClockMargin) SegClockPos.Y = 2;

                            if ((SegClockPos.X + segClock.Width) > ClockPBox.Width)
                                segClock.Width = (int)(SegClockPos.X + segClock.Width) - (int)SegClockMargin;

                            if ((SegClockPos.Y + segClock.Height) > ClockPBox.Height)
                            {
                                segClock.Height = (int)(SegClockPos.Y + segClock.Height) - (int)SegClockMargin;

                            }
                            SegClockRectangle = new Rectangle(Point.Round(SegClockPos), segClock.Size);
                        }


                    }
                }
                lastMousePos2 = Cursor.Position;
            }
            else
            {
                IsMouseOverClock = false;
                Cursor = Cursors.Default;
            }
        }
        Point lastMousePos2 = new Point();
        private void NESixClock_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true; // (e.Button == MouseButtons.Left);
        }

        private void NESixClock_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void isTransparentMenu_Click(object sender, EventArgs e)
        {

        }

        private void filledText_Click(object sender, EventArgs e)
        {
            ClockSettings.Default.FilledText = filledText.Checked;
            ClockSettings.Default.Save();
        }

        public bool AmFull7Seg { get { return isfullSevenSeg.Checked; } }
        public bool AmEigth7Seg { get { return isQSevenSeg.Checked; } }
        public bool AmInverse { get { return IsInverse.Checked; } }
    }

}
