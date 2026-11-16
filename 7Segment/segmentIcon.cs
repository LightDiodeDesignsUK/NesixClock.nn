
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Clockv2.nn
{
    public static class SegmentIcon
    {
        static Size IntIconSize = new(96, 96);
        static Size OutIconSize = new(24, 24);
        public static int LastSeg1 = -1, LastSeg2 = -1, LastSeg3 = -1, LastSeg4 = -1;
        static SevenSegmentCtl seg1, seg2,seg3,seg4;
        static Bitmap? bmpIntIcon = null;
        static Bitmap? bmpOutIcon = null;
        
        public static Bitmap getIconBitmap(NESixClock dummyForm)
        {
            

            if (bmpIntIcon == null)
            { bmpIntIcon = new Bitmap(IntIconSize.Width, IntIconSize.Height); }
            
            int intIconWidth = IntIconSize.Width;
            int intIconHeight = IntIconSize.Width;
            
            int segmentWidth = IntIconSize.Width / 2;
            int segmentHeight = IntIconSize.Width / 2;
            if (seg1 == null) seg1 = new (dummyForm) { Left = 0, Width = segmentWidth, Top = 0, Height = segmentHeight };
            if (seg2 == null) seg2 = new (dummyForm) { Left = 0, Width = segmentWidth, Top = 0, Height = segmentHeight };
            if (seg3 == null) seg3 = new (dummyForm) { Left = 0, Width = segmentWidth, Top = segmentHeight, Height = segmentHeight };
            if (seg4 == null) seg4 = new (dummyForm) { Left = 0, Width = segmentWidth, Top = segmentHeight, Height = segmentHeight };
            string dH = DateTime.Now.Hour.ToString("00");
            string dM = DateTime.Now.Minute.ToString("00");


            int seg1Number = int.Parse(new string(dH.Substring(0, 1)));
            int seg2Number = int.Parse(new string(dH.Substring(1, 1)));
            int seg3Number = int.Parse(new string(dM.Substring(0, 1)));
            int seg4Number = int.Parse(new string(dM.Substring(1, 1)));

            seg1.gap = seg2.gap = seg3.gap = seg4.gap = 0;
            

            

            if (true) //seg1Number != LastSeg1)
            {
                Graphics.FromImage(bmpIntIcon).FillRectangle(Brushes.Red, 0, 0, bmpIntIcon.Width, bmpIntIcon.Height);
                Graphics g1 = seg1.CreateGraphics();
                seg1.Number = int.Parse(new string(dH.Substring(0, 1)));
                seg1.DoPaintingDigit(g1);
                seg1.DrawToBitmap(bmpIntIcon, new(new Point(0, 0), seg1.Size));
                LastSeg1 = seg1Number;
            }
            if (true) //seg2Number != LastSeg2)
            {
                Graphics g2 = seg2.CreateGraphics();
                Graphics.FromImage(bmpIntIcon).FillRectangle(Brushes.Red, segmentWidth, 0, segmentWidth, segmentHeight);
                seg2.Number = int.Parse(new string(dH.Substring(1, 1)));
                seg2.DoPaintingDigit(g2);
                seg2.DrawToBitmap(bmpIntIcon, new(new Point(segmentWidth, 0), seg2.Size));
                LastSeg2 = seg2Number;

            }
            if (true) //seg3Number != LastSeg3)
            {
                Graphics.FromImage(bmpIntIcon).FillRectangle(Brushes.Red, segmentWidth, segmentHeight, 0, segmentHeight);
                Graphics g3 = seg3.CreateGraphics();
                seg3.Number = int.Parse(new string(dM.Substring(0, 1)));
                seg3.DoPaintingDigit(g3);
                seg3.DrawToBitmap(bmpIntIcon, new(new Point(0, segmentHeight), seg3.Size));
                LastSeg3 = seg3Number;
            }
            if (true) //seg4Number != LastSeg4)
            {
                Graphics g4 = seg4.CreateGraphics();
                Graphics.FromImage(bmpIntIcon).FillRectangle(Brushes.Red, segmentWidth, segmentHeight, segmentWidth, segmentHeight);
                seg4.Number = int.Parse(new string(dM.Substring(1, 1)));
                seg4.DoPaintingDigit(g4);
                seg4.DrawToBitmap(bmpIntIcon, new(new Point(segmentWidth, segmentHeight), seg4.Size));
                LastSeg4 = seg4Number;
            }
            

            seg1.segWidth = seg1.segHeight = seg2.segWidth = seg2.segHeight = seg3.segWidth =
            seg3.segHeight = seg4.segWidth = seg4.segHeight = 24;


            

            
            Bitmap bIcon2 = new Bitmap(OutIconSize.Width, OutIconSize.Height);

            //seg1.Dispose(); seg2.Dispose(); seg3.Dispose(); seg4.Dispose();

            Graphics.FromImage(bIcon2).DrawImage(bmpIntIcon,   new Rectangle(0, 0, OutIconSize.Width, OutIconSize.Height),new Rectangle(0,0,IntIconSize.Width, IntIconSize.Width), GraphicsUnit.Pixel);
            if(ClockSettings.Default.IsInverse)  InvertImageColour(ref bIcon2);
            //bIcon2.Save(@"d:\neals\bIcon2.bmp", ImageFormat.Bmp);
            return bIcon2;
        }
        static ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                {
            new float[] {-1,  0,  0,  0,  0},
            new float[] { 0, -1,  0,  0,  0},
            new float[] { 0,  0, -1,  0,  0},
            new float[] { 0,  0,  0,  1,  0},
            new float[] { 1,  1,  1,  0,  1}
                });
        public static void InvertImageColour(ref Bitmap source)
        {
            //Bitmap dest = new Bitmap(source.Width, source.Height);
            using (Graphics g = Graphics.FromImage(source))
            {
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);
                g.DrawImage(source,
                    new Rectangle(0, 0, source.Width, source.Height),
                    0, 0, source.Width, source.Height,
                    GraphicsUnit.Pixel, attributes);
            }
        }
    }
}
