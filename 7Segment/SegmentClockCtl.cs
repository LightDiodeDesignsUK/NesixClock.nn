using System.ComponentModel;
using static Clockv2.nn.NESixClock;
using System.Drawing;

namespace Clockv2.nn
{
    public partial class SegmentClockCtl : UserControl
    {
        public NESixClock ownerNSXClock;
        public void DoPaint(Graphics gCaller)
        {
            //g.FillRectangle(Brushes.Tan, g.VisibleClipBounds);
            

            for (int i = 0; i < SegmentsCount; i++)
            {
                Bitmap btmap = new Bitmap((int)(gCaller.VisibleClipBounds.Width / SegmentsCount), (int)gCaller.VisibleClipBounds.Height-32);
                Digits[i].DoPaintingDigit(Graphics.FromImage(btmap));
                int cellWid = (int)((gCaller.VisibleClipBounds.Width - 32 ) / SegmentsCount) ;
                int cellHeight = (int)((gCaller.VisibleClipBounds.Height - 32)); 
                gCaller.DrawImage(btmap, (int)(cellWid * i) + 16, 16, btmap.Width, btmap.Height);
                btmap.Dispose();
            }
            int iPenWid = (this.Height + this.Width) / 156;
            Pen pBorder = new(ColorSchemes[ownerNSXClock.ColorSchemeNo][(int)SchemeMembers._7SegOutline].Color, iPenWid);

            if (ownerNSXClock.Am7SegBordered)
            {
                RectangleF borderRec = gCaller.VisibleClipBounds;
                borderRec.X += 8;   borderRec.Y += 8;
                borderRec.Width -= 16;   borderRec.Height -= 16;
                gCaller.DrawRectangle(pBorder, borderRec);
            }
        }//
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsClockAutoShow
        {
            get { return tDigitClock.Enabled; }
            set { tDigitClock.Enabled = value; }
        }
        public NESixClock? OwnerForm;
        public SegmentClockCtl(NESixClock ownerNesixClock1)
        {
            InitializeComponent();
            this.ownerNSXClock = ownerNesixClock1;
            AutoScaleMode = AutoScaleMode.None;
        }
        public PictureBox[] DigitsBox = new PictureBox[8] ;
        public SevenSegmentCtl[] Digits = new SevenSegmentCtl[8];

        private void SegmentClockCtl_Load(object sender, EventArgs e)
        {
            BackColor = Color.Black;
        }
        public float SegmentsCount = 8;
        public void STartup()
        {
            this.MinimumSize = Size.Subtract(OwnerForm.Size, new Size(20, 52));
            this.AutoScaleDimensions = this.Size;
            SuspendLayout();
            foreach (Control seven in this.Controls) this.Controls.Remove(seven);
            foreach (PictureBox seven in this.Controls) this.Controls.Remove(seven);

            float wid = this.Width;
            float hei = this.Height;
            float hei2 = this.Height;

            

            for (int diginum = 0; diginum < 8; diginum++)
            {
                if (DigitsBox[diginum] == null)
                {
                    if (Digits[diginum] == null)
                    {
                        Digits[diginum] = new SevenSegmentCtl(ownerNSXClock);
                        Digits[diginum].OwnerControl = this;
                        Digits[diginum].Tag = diginum;
                    }

                    DigitsBox[diginum] = new ();
                    DigitsBox[diginum].Tag = (diginum);
                    int digiWidth = (int)(wid / SegmentsCount)- 256;
                    DigitsBox[diginum].Left = 64 + (diginum * digiWidth);
                    DigitsBox[diginum].Top = 0;
                    DigitsBox[diginum].Width = (int)(wid / SegmentsCount) - 16;

                    DigitsBox[diginum].Height = (int)hei - 84;
                    //Digits[0].Height = (int)hei - 4;

                    DigitsBox[diginum].DoubleClick += SegmentClockCtl_DoubleClick;
                    DigitsBox[diginum].BorderStyle = BorderStyle.FixedSingle;
                    //DigitsBox[diginum].BackColor = Color.White;
                    

                    DigitsBox[diginum].Click += SegmentClockCtl_Click;
                    
                    DigitsBox[diginum].Paint += DigitsBox_Paint;
                    this.Controls.Add(DigitsBox[diginum]);
                }
                
                    

                
                if (diginum == 0) diginum = 0;
                Digits[diginum] = new SevenSegmentCtl(ownerNSXClock);

                int sgW = (int)(wid / SegmentsCount) - 256;
                    Digits[diginum].Left = 128 + (diginum * sgW);
                     Digits[diginum].Top = 124;
                    //Digits[diginum].Width = (int)(wid / SegmentsCount) - 256;
                    //Digits[diginum].Height = (int)hei - 4;
                    
                
                
                
            }
            if (tDigitClock.Interval != 1000)
            {
                tDigitClock.Interval = 1000;  tDigitClock.Tick += TDigitClock_Tick;
                tDigitClock.Start();
            }
             ;
            

            ResumeLayout(false);
        }

        private void DigitsBox_Paint(object? sender, PaintEventArgs e)
        {
            PictureBox? sent = (PictureBox?)sender;
            int index = (int?)sent?.Tag ?? 0;
            Bitmap bm = new Bitmap(sent?.Width ?? 0, (sent?.Height - 84) ?? 0);

            
            //Digits[index].DoPainting(Graphics.FromImage(bm));
            Digits[index].DoPaintingDigit(e.Graphics);
            //e.Graphics.DrawImage(bm,0,0);
        }

        float nAnimWidth = 30, nAnimHeight = 10;
        public float nAnimThetaDelta = 1;
        public float nAnimTheta2Delta = 1;
        float pi = (float) Math.PI;

        float[] thetas = new float[8];

        public void TAnimclock_Tick(object? sender, EventArgs e)
        {
            this.SuspendLayout();
            nAnimThetaDelta = pi / (SegmentsCount * 16);
            nAnimTheta2Delta = pi / (SegmentsCount * 24);

            if (thetas[0] == 0 && thetas[2] == 0)
            {
                for (int nDigit = 0; nDigit < SegmentsCount; nDigit++)
                {
                    thetas[nDigit] = (pi / SegmentsCount) * (nDigit );
                }
            }
            if (!OwnerForm.AmAnimated) return;
            String sWids = "";
            for (int nDigit = 0; nDigit < SegmentsCount; nDigit++)
            {
                thetas[nDigit] += nAnimThetaDelta;
                thetas[nDigit] += nAnimTheta2Delta;
                Digits[nDigit].SetupSegments();
                float sin = (float)(Math.Sin(thetas[nDigit]));
                float cos = (float)(Math.Cos(thetas[nDigit] * 4));
                nAnimWidth = (((sin) * 10)) + 15;
                nAnimHeight = (((cos) * 10) + 15);
                Digits[nDigit].segHeight = nAnimHeight;
                Digits[nDigit].segWidth = nAnimWidth;
                
            }

            
            SuspendLayout();
            Bitmap bm;
            for (int nD = 0; nD < SegmentsCount; nD++)
            {
                bm = new Bitmap(DigitsBox[nD].Size.Width, DigitsBox[nD].Size.Height - 64);
                Digits[nD].DoPaintingDigit(Graphics.FromImage(bm));
                DigitsBox[nD].CreateGraphics().DrawImage(bm, Digits[nD].Left, Digits[nD].Top);
                bm.Dispose();
            }
            ////////////
            ResumeLayout();     
            
        }

        System.Windows.Forms.Timer tDigitClock = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer tAnimclock = new System.Windows.Forms.Timer() { Interval = 1000 };

        private int lastDig1 = -1, lastDig2 = -1, lastDig3 = -1,
            lastDig4 = -1, lastDig5 = -1, lastDig6 = -1, lastDig7 = -1, lastDig8 = -1;
        public void TDigitClock_Tick(object? sender, EventArgs e)
        {
            SuspendLayout();

            int dig1 = (DateTime.Now.Hour / 10) % 10;
            int dig2 = DateTime.Now.Hour % 10;

            int dig3 = 11;

            int dig4 = DateTime.Now.Minute / 10 % 10;
            int dig5 = DateTime.Now.Minute % 10;

            int dig6 = 11;

            int dig7 = (DateTime.Now.Second / 10) % 10;
            int dig8 = DateTime.Now.Second % 10;





            if (dig1 != lastDig1)
            {
                lastDig1 = dig1;
                Digits[0].Number = dig1;
                Digits[0].Invalidate();
                DigitsBox[0].Invalidate();
            }
            if (dig2 != lastDig2)
            {
                lastDig2 = dig2;
                Digits[1].Number = dig2;
                Digits[1].Invalidate();
                DigitsBox[1].Invalidate();
            }
            if (dig3 != lastDig3)
            {
                Digits[2].Number = dig3;
                lastDig3 = dig3;
            }
            if (dig4 != lastDig4)
            {
                lastDig4 = dig4;
                Digits[3].Number = dig4;
                Digits[3].Invalidate();
            }
            if (dig5 != lastDig5)
            {
                lastDig5 = dig5;
                Digits[4].Number = dig5;
                Digits[4].Invalidate();
            }

            if (dig6 != lastDig6)
            {
                lastDig6 = dig6;
                Digits[5].Number = dig6;
            }
            if (dig7 != lastDig7)
            {
                lastDig7 = dig7;
                Digits[6].Number = dig7;
                Digits[6].Invalidate();
            }
            if (dig8 != lastDig8)
            {
                lastDig8 = dig8;
                Digits[7].Number = dig8;
                Digits[7].Invalidate();
            }
            ResumeLayout();
        }

        private void SegmentClockCtl_Resize(object sender, EventArgs e)
        {
            foreach (PictureBox ss in Controls)
            {
                this.Controls.Remove(ss);
            }
            foreach (Control ss in Controls)
            {
                this.Controls.Remove(ss);
            }
            foreach(PictureBox ss in Controls) this.Controls.Remove(ss);
            this.STartup();
            lastDig1 = -2; lastDig2 = -2; lastDig3 = -2; lastDig4 = -2;
            lastDig5 = -2; lastDig6 = -2;
            TDigitClock_Tick(this, new EventArgs());
        }

        public void SegmentClockCtl_DoubleClick(object? sender, EventArgs e)
        {
            OwnerForm.Invoke(OwnerForm.segmentClockCtl1_DoubleClick, new object[] { this, e });
        }

        public void SegmentClockCtl_Click(object? sender, EventArgs e)
        {

            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Right)
            {
                OwnerForm.ClockPBox.ContextMenuStrip?.Show(me.Location);
            }
        }
    }
}
