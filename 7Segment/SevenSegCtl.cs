using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Xsl;
using static Clockv2.nn.NESixClock;
//using Windows.Graphics.Printing3D;

namespace Clockv2.nn
{
    public partial class SevenSegmentCtl : UserControl
    {
        public RectangleF? SegMaxMinRectangleUnscaled
        {
            get
            {
                float minX = 0xFFFF, minY = 0xFFFF, maxX = 0, maxY = 0;
                for (int ii = 1; ii < 12; ii++)
                {
                    bool isDrawnSeg = (digits[Number].Contains(ii));
                    if (isDrawnSeg)
                    {
                        foreach (polySegment psg in polySegments)
                        {
                            foreach (PointF ppp in psg.unScaledPoints)
                            {
                                minX = Math.Min(minX, ppp.X);
                                minY = Math.Min(minY, ppp.Y);
                                maxX = Math.Max(maxX, ppp.X);
                                maxY = Math.Max(maxY, ppp.Y);
                            }
                        }
                    }
                }
                _activeSegmentsRect = new RectangleF(minX, minY, maxX, maxY);
                return _activeSegmentsRect;
            }
        }
        public SevenSegmentCtl(NESixClock ownerNSX)
        {
            InitializeComponent();
            OwnerNSXClock = ownerNSX;
        }
        public Control OwnerControl;
        public static NESixClock? OwnerNSXClock;
        public int testNum = 0;
        public  segment[] squareSegments =
        {
            new segment() { X1 = 20, W1 = 20, Y1 = 90, H1 = 5,    index = 0 },  //  dot

            new segment() { X1 = 15, W1 = 65, Y1 = 0, H1 = 5,   index = 1 },    // Top bar
            new segment() { X1 = 15, W1 = 65, Y1 = 50, H1 = 5,   index = 2 },   // Middle bar
            new segment() { X1 = 15, W1 = 65, Y1 = 95, H1 = 5,   index = 3 },   // Bottom bar

            new segment() { X1 = 5, W1 = 20, Y1 = 7, H1 = 40,    index = 4 },   // Top left
            new segment() { X1 = 74, W1 = 20, Y1 = 7, H1 = 40,   index = 5 },   // Top right
            new segment() { X1 = 5, W1 = 20, Y1 = 57, H1 = 36,   index = 6 },   // Bottom left
            new segment() { X1 = 74, W1 = 20, Y1 = 57, H1 = 36,  index = 7},    // Bottom right
            
            new segment() { X1 = 30, W1 = 40, Y1 = 30, H1 = 20,  index = 8},    // Colon1 top
            new segment() { X1 = 30, W1 = 40, Y1 = 60, H1 = 20,  index = 9},    // Colon1 bottom

            new segment() { X1 = 30, W1 = 20, Y1 = 7, H1 = 40,    index = 10 },   // Top centred left vertical
            new segment() { X1 = 30, W1 = 20, Y1 = 57, H1 = 36,   index = 11 },   // Bottom centred left vertical
        };


        /// <summary>
        /// Polygon segment elements.
        /// Defines the points for (most of) the
        /// segments on a 7/8/9/10 segment display.
        /// </summary>
        public polySegment[] polySegments2 =
        {
            new polySegment()
            {
                outlineColor=Color.IndianRed, index = 0, Desc = "Dot", unScaledPoints = new List<PointF>()
                {
                    new PointF(20f, 90f) ,
                    new PointF(20f, 95f) ,
                    new PointF(40f, 95f) ,
                    new PointF(40f, 90f) ,
                    new PointF(20f, 90f)
                }
            }
        ,
            new polySegment()
            {
                outlineColor=Color.Orange, index = 1, Desc = "Top Bar", unScaledPoints = new List<PointF>()
                {

                    new Point(10,0),
                    new Point(90,0),
                    new Point(98,3),
                    new Point(80,8),
                    new Point(19,8),
                    new Point(0,3)
                }
            }
        ,

            new polySegment()
        {
            outlineColor=Color.Yellow, index = 2, Desc = "Middle bar", unScaledPoints = new List<PointF>()
            {
                new Point(20,   47),
                new Point(82,   47),
                new Point(95,   50),
                new Point(82,   53),

                new Point(18,   53),
                new Point(3,   50),
                //new Point(18,   47),
            }
        },

            new polySegment()
        {
            outlineColor=Color.Green, index = 3, Desc="Bottom bar", unScaledPoints = new List<PointF>()
            {
                new PointF(15, 100),
                new PointF(0, 96),
                new PointF(15, 92),
                new PointF(82, 92),
                new PointF(97, 96),
                new PointF(82, 100),
            }
        }
        ,
            new polySegment()
        {
            outlineColor=Color.AliceBlue, index = 4, Desc="Top left", unScaledPoints = new List<PointF>()
            {
                new PointF(0, 4),
                new PointF(0, 50),

                new PointF(20, 46),
                new PointF(20, 9.5f),

            }
        }
        ,
            new polySegment()
        {
            outlineColor=Color.White, index = 5, Desc="Top right", unScaledPoints = new List<PointF>()
            {
                new PointF(99, 4),
                new PointF(99, 50),
                new PointF(79, 45.75f),
                new PointF(79, 9.5f ),

            }
        }
        ,
            new polySegment()
        {
            outlineColor=Color.Violet, index=6, Desc="Bottom left", unScaledPoints = new List<PointF>()
            {
                new Point(0, 50 ),
                new Point(20, 54),
                new Point(20, 90),
                new Point(0, 95),
            }
        }
        ,

            new polySegment()
            {
                outlineColor=Color.Cyan, Desc="Bottom right", index=7,
                unScaledPoints = new List<PointF>()
                {
                    new Point(98, 50),
                    new Point(98, 95),
                    new Point(80, 90),
                    new Point(80, 54 )

                }
            }
            ,
            new polySegment()
            {
                Desc="ColonTop",
                outlineColor=Color.Wheat,  index=8,
                unScaledPoints = new List<PointF>()
                {
                    new Point(30, 20),
                    new Point(70, 20),
                    new Point(70, 30),
                    new Point(30, 30),
                }
            }
            ,

            new polySegment()
            {
                Desc="Colon bottom" ,
                index = 9, outlineColor=Color.Beige,
                unScaledPoints = new List<PointF>()
                {
                    new Point(30, 70),
                    new Point(70, 70),
                    new Point(70, 80),
                    new Point(30, 80),
                }
            },

            new polySegment()
            {
                Desc="Top centred left vertical",
                index = 10, outlineColor=Color.Crimson,
                unScaledPoints = new List<PointF>()
                {
                    new PointF(30, 1),
                    new PointF(42, 0),
                    new PointF(54, 1),
                    new PointF(54, 48.5f),
                    new PointF(48, 50),
                    new PointF(42, 49f),
                    new PointF(36, 50),
                    new PointF(30, 48.5f)
                }

            },
            new polySegment()
            {
                Desc="Bottom centred left vertical",
                index=11, outlineColor=Color.Magenta,
                unScaledPoints = new List<PointF>()
                {
                    new PointF(30, 99),
                    new PointF(42, 100),
                    new PointF(54, 99),

                    new PointF(54, 49),
                    new PointF(48, 50.5f),
                    new PointF(42, 49),
                    new PointF(36, 50.5f),
                    new PointF(30, 49)
                }
            }   // 
            
        };
        /// 
        ///////////////
        const float segWidthStart = 20, segHeightStart = 8;
        ///////////////
        public float gap = 2.5f;
        public float segWidth = 20, HsegWidth = segWidthStart / 2, QsegWidth = segWidthStart / 4, dSegWidth = segWidthStart * 2;
        public float segHeight = segHeightStart, HsegHeight = segHeightStart / 2, QsegHeight = segHeightStart / 4;
        //////
        /////////

        /// <summary>
        /// 
        /// </summary>
        public polySegment[] polySegments;
        RectangleF? _activeSegmentsRect = null;
        public void SetupSegments()
        {
            HsegHeight = segHeight / 2; QsegHeight = segHeight / 4;
            HsegWidth = segWidth / 2; QsegWidth = segWidth / 4;
            if (OwnerNSXClock.AmAnimated)
            {
                polySegments = new polySegment[]
                {
                    new polySegment()
                    {
                        outlineColor = Color.IndianRed,
                        index = 0,
                        Desc = "Dot2",
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(20f, 90f),
                            new PointF(20f, 95f),
                            new PointF(40f, 95f),
                            new PointF(40f, 90f),
                            new PointF(20f, 90f)
                        }
                    }
                    ,
                    new polySegment()
                    {
                        outlineColor = Color.Orange,
                        index = 1,
                        Desc = "Top Bar2",
                        unScaledPoints = new List<PointF>()
                        {
                            //done
                            new PointF(segWidth, 0),
                            new PointF(100 - segWidth, 0),
                            new PointF(100 - gap, HsegHeight),
                            new PointF(100 - segWidth, segHeight),

                            new PointF(segWidth, segHeight),
                            new PointF(0, HsegHeight)
                        }
                    }
                    ,

                    new polySegment()
                    {
                        outlineColor = Color.Yellow,
                        index = 2,
                        Desc = "Middle bar2",
                        unScaledPoints = new List<PointF>()
                        {

                            new PointF(100 - (segWidth + gap), (50) - HsegHeight),
                            new PointF(100 - gap, 50),
                            new PointF(100 - (segWidth + gap), (50) + HsegHeight),

                            new PointF(segWidth + gap, 50 + HsegHeight),
                            new PointF(gap, 50),
                            new PointF(segWidth + gap, 50 - HsegHeight),
                        }
                    }
                    ,
                    new polySegment()
                    {
                        outlineColor = Color.Green,
                        index = 3,
                        Desc = "Bottom bar",
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(segWidth, 100),
                            new PointF(0, 100 - HsegHeight),
                            new PointF(segWidth, 100 - segHeight),
                            new PointF((100 - segWidth) - gap, 100 - segHeight),
                            new PointF(100, 100 - HsegHeight),
                            new PointF((100 - segWidth) - gap, 100),
                        }
                    }
                    ,
                    new polySegment()
                    {
                        outlineColor = Color.AliceBlue,
                        index = 4,
                        Desc = "Top left",
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(0, HsegHeight + gap),
                            new PointF(0, 50 - gap),

                            new PointF(segWidth, (50 - HsegHeight) - gap),
                            new PointF(segWidth, segHeight + gap),

                        }
                    }
                    ,
                    new polySegment()
                    {
                        outlineColor = Color.White,
                        index = 5,
                        Desc = "Top right",
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(100, HsegHeight + gap),
                            new PointF(100, 50 - gap),
                            new PointF(100 - segWidth, (50 - HsegHeight) - gap),
                            new PointF(100 - segWidth, segHeight + gap),

                        }
                    }
                    ,
                    new polySegment()
                    {
                        outlineColor = Color.Violet,
                        index = 6,
                        Desc = "Bottom left",
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(0, 50f + gap),
                            new PointF(segWidth, 50 + gap + HsegHeight),
                            new PointF(segWidth, (100 - segHeight) - gap),
                            new PointF(0, (100 - HsegHeight) - gap)
                        }
                    }
                    ,
                    new polySegment()
                    {
                        outlineColor = Color.Cyan,
                        Desc = "Bottom right",
                        index = 7,
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(100, 50 + gap),
                            new PointF(100, (100 - gap) - HsegHeight),
                            new PointF(100 - segWidth, (100 - segHeight) - gap),
                            new PointF(100 - segWidth, 50 + HsegHeight + gap)

                        }
                    }
                    ,
                    new polySegment()
                    {
                        Desc = "ColonTop",
                        outlineColor = Color.Wheat,
                        index = 8,
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(30 + QsegWidth, 20 + HsegHeight),
                            new PointF(70 + QsegWidth, 20 + HsegHeight),
                            new PointF(70+QsegWidth, 30 + HsegHeight),
                            new PointF(30 + QsegWidth, 30 + HsegHeight),
                        }
                    }
                    ,

                    new polySegment()
                    {
                        Desc = "Colon bottom",
                        index = 9,
                        outlineColor = Color.Beige,
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(30, 70),
                            new PointF(70, 70),
                            new PointF(70, 80),
                            new PointF(30, 80),
                        }
                    }
                    ,
                    new polySegment()
                    {
                        Desc = "Top centred left vertical",
                        index = 10,
                        outlineColor = Color.Crimson,
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(30, 0),
                            new PointF(30 + segWidth, HsegHeight),
                            new PointF(30 + segWidth, 50f),
                            new PointF(30 + HsegWidth + QsegWidth, 50f - gap),
                            new PointF(30 + HsegWidth, 50f),
                            new PointF(30 + QsegWidth, 50f - gap),
                            new PointF(30, 50f)
                        }
                    }
                    ,
                    new polySegment()
                    {
                        Desc = "Bottom centred left vertical",
                        index = 11,
                        outlineColor = Color.Magenta,
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(30, 100 - 0),
                            new PointF(30 + segWidth, 100 - HsegHeight),
                            new PointF(30 + segWidth, 51f),
                            new PointF(30 + HsegWidth + QsegWidth, 51f - gap),
                            new PointF(30 + HsegWidth, 51f),
                            new PointF(30 + QsegWidth, 51f - gap),
                            new PointF(30, 50f)
                        }
                    }   // 
                };

                // Colon squiggles    30 20  & 30 70
                polySegments[8]?.unScaledPoints?.Clear();
                polySegments[9]?.unScaledPoints?.Clear();
                float sinF = 0;

                //Bottom
                for (int i = 0; i <= 30; i+=2)
                {
                    sinF = (float)Math.Sin((double)(i + (NESixClock.SegClockRectangle.Left / 8)) / 2) * QsegHeight;
                    polySegments[8].unScaledPoints?.Add(new PointF(30 + i , 40 + sinF));
                    polySegments[9].unScaledPoints?.Add(new PointF(30 + i , 80 + sinF));
                }
                //Right upwards
                for (int i = 20; i >=0; i-=2)
                {
                    sinF = (float)Math.Sin((double)(i + (NESixClock.SegClockRectangle.Top / 8)) / 4) * QsegWidth;
                    polySegments[8].unScaledPoints?.Add(new PointF(60 - sinF, 40 - i));
                    polySegments[9].unScaledPoints?.Add(new PointF(60 - sinF, 80 - i));
                }
                // Top Leftwards
                for (int i = 30; i >= 0; i-=2)
                {
                    sinF = (float)Math.Sin((double)(i + (NESixClock.SegClockRectangle.Left / 8)) / 4) * QsegHeight;
                    polySegments[8].unScaledPoints?.Add(new PointF(30 + i, 20 - sinF));
                    polySegments[9].unScaledPoints?.Add(new PointF(30 + i, 60 + sinF));
                }
                // Left Downwards
                for (int i = 0; i <= 20; i+=2)
                {
                    sinF = (float)Math.Sin((double)(i + (NESixClock.SegClockRectangle.Top / 8) / 2)) * QsegWidth;
                    polySegments[8].unScaledPoints?.Add(new PointF(30 + sinF, 20 + i));
                    polySegments[9].unScaledPoints?.Add(new PointF(30 + sinF, 80 - i));
                }

            }
            else
            {
                polySegments = new polySegment[]
                {
                    new polySegment()
                    {
                        outlineColor = Color.IndianRed,
                        index = 0,
                        Desc = "Dot2",
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(20f, 90f),
                            new PointF(20f, 95f),
                            new PointF(40f, 95f),
                            new PointF(40f, 90f),
                            new PointF(20f, 90f)
                        }
                    }
                    ,
                    new polySegment()
                    {
                        outlineColor = Color.Orange,
                        index = 1,
                        Desc = "Top Bar2",
                        unScaledPoints = new List<PointF>()
                        {
                            //done
                            new PointF(segWidth, 0),
                            new PointF(100 - segWidth, 0),
                            new PointF(100 - gap, HsegHeight),
                            new PointF(100 - segWidth, segHeight),

                            new PointF(segWidth, segHeight),
                            new PointF(0, HsegHeight)
                        }
                    }
                    ,

                    new polySegment()
                    {
                        outlineColor = Color.Yellow,
                        index = 2,
                        Desc = "Middle bar2",
                        unScaledPoints = new List<PointF>()
                        {

                            new PointF(100 - (segWidth + gap), (50) - HsegHeight),
                            new PointF(100 - gap, 50),
                            new PointF(100 - (segWidth + gap), (50) + HsegHeight),

                            new PointF(segWidth + gap, 50 + HsegHeight),
                            new PointF(gap, 50),
                            new PointF(segWidth + gap, 50 - HsegHeight),
                        }
                    }
                    ,
                    new polySegment()
                    {
                        outlineColor = Color.Green,
                        index = 3,
                        Desc = "Bottom bar",
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(segWidth, 100),
                            new PointF(0, 100 - HsegHeight),
                            new PointF(segWidth, 100 - segHeight),
                            new PointF((100 - segWidth) - gap, 100 - segHeight),
                            new PointF(100, 100 - HsegHeight),
                            new PointF((100 - segWidth) - gap, 100),
                        }
                    }
                    ,
                    new polySegment()
                    {
                        outlineColor = Color.AliceBlue,
                        index = 4,
                        Desc = "Top left",
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(0, HsegHeight + gap),
                            new PointF(0, 50 - gap),

                            new PointF(segWidth, (50 - HsegHeight) - gap),
                            new PointF(segWidth, segHeight + gap),

                        }
                    }
                    ,
                    new polySegment()
                    {
                        outlineColor = Color.White,
                        index = 5,
                        Desc = "Top right",
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(100, HsegHeight + gap),
                            new PointF(100, 50 - gap),
                            new PointF(100 - segWidth, (50 - HsegHeight) - gap),
                            new PointF(100 - segWidth, segHeight + gap),

                        }
                    }
                    ,
                    new polySegment()
                    {
                        outlineColor = Color.Violet,
                        index = 6,
                        Desc = "Bottom left",
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(0, 50f + gap),
                            new PointF(segWidth, 50 + gap + HsegHeight),
                            new PointF(segWidth, (100 - segHeight) - gap),
                            new PointF(0, (100 - HsegHeight) - gap)
                        }
                    }
                    ,
                    new polySegment()
                    {
                        outlineColor = Color.Cyan,
                        Desc = "Bottom right",
                        index = 7,
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(100, 50 + gap),
                            new PointF(100, (100 - gap) - HsegHeight),
                            new PointF(100 - segWidth, (100 - segHeight) - gap),
                            new PointF(100 - segWidth, 50 + HsegHeight + gap)

                        }
                    }
                    ,
                    new polySegment()
                    {
                        Desc = "ColonTop",
                        outlineColor = Color.Wheat,
                        index = 8,
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(30, 20),
                            new PointF(70, 20),
                            new PointF(70, 30),
                            new PointF(30, 30),
                        }
                    }
                    ,

                    new polySegment()
                    {
                        Desc = "Colon bottom",
                        index = 9,
                        outlineColor = Color.Beige,
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(30, 70),
                            new PointF(70, 70),
                            new PointF(70, 80),
                            new PointF(30, 80),
                        }
                    }
                    ,
                    new polySegment()
                    {
                        Desc = "Top centred left vertical",
                        index = 10,
                        outlineColor = Color.Crimson,
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(30, 0),
                            new PointF(30 + segWidth, HsegHeight),
                            new PointF(30 + segWidth, 50f),
                            new PointF(30 + HsegWidth + QsegWidth, 50f - gap),
                            new PointF(30 + HsegWidth, 50f),
                            new PointF(30 + QsegWidth, 50f - gap),
                            new PointF(30, 50f)
                        }
                    }
                    ,
                    new polySegment()
                    {
                        Desc = "Bottom centred left vertical",
                        index = 11,
                        outlineColor = Color.Magenta,
                        unScaledPoints = new List<PointF>()
                        {
                            new PointF(30, 100 - 0),
                            new PointF(30 + segWidth, 100 - HsegHeight),
                            new PointF(30 + segWidth, 51f),
                            new PointF(30 + HsegWidth + QsegWidth, 51f - gap),
                            new PointF(30 + HsegWidth, 51f),
                            new PointF(30 + QsegWidth, 51f - gap),
                            new PointF(30, 50f)
                        }
                    }   // 
                };
            }
            
        }
            /// 
        
        static List<List<int>> digits = new List<List<int>>();
        private void _7Segment_Load(object sender, EventArgs e)
        {
            SetupSegments();
            this.Paint += _7Segment_Paint;
            if (digits.Count == 0)
            {
                digits.Add(new List<int> { 1, 3, 4, 5, 6, 7 });     //0
                digits.Add(new List<int> { 10, 11 }); // { 5, 7 }); //1 Centred 1
                digits.Add(new List<int> { 1, 2, 3, 5, 6 });       //2
                digits.Add(new List<int> { 1, 2, 3, 5, 7 });       //3
                digits.Add(new List<int> { 2, 4, 5, 7 });         //4
                digits.Add(new List<int> { 1, 2, 3, 4, 7 });      //5
                digits.Add(new List<int> { 1, 2, 3, 4, 6, 7 });   //6
                digits.Add(new List<int> { 1, 5, 7 });            //7
                digits.Add(new List<int> { 1, 2, 3, 4, 5, 6, 7 });    //8
                digits.Add(new List<int> { 1, 2, 3, 4, 5, 7 });      //9
                digits.Add(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }); // test 10
                digits.Add(new List<int> { 8, 9 }); // Colon1     
            }
        }


        int _num = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Number
        {
            get { return _num; }
            set
            {
                if (_num != value % 13)
                {
                    _num = value % 13;

                    this.DoPaintingDigit(this.CreateGraphics());
                }
            }
        }
        private void _7Segment_Paint(object? sender, PaintEventArgs e)
        {
            AutoScaleMode = AutoScaleMode.None;

            //_7Segment.DefaultBackColor = SystemColors.Control;
            SetupSegments();

            //Bitmap scr = new Bitmap((int) e.Graphics.VisibleClipBounds.Width, (int) e.Graphics.VisibleClipBounds.Height);

            DoPaintingDigit(e.Graphics);
            //Graphics backBuffer = Graphics.FromImage(scr);

            //DoPainting(backBuffer);
            //e.Graphics.DrawImage(scr,0f,0f);
            //backBuffer.Dispose();   scr.Dispose();

        }
        Color backColor = Color.White;
        List<Color> backColors = new List<Color>()
        {
            Color.Black,
            Color.White,
            Color.LightGray,
            Color.Black,
            Color.Maroon   };
        Bitmap bmpSeg;
        int lastNumber = 0;
        public void DoPaintingDigit(Graphics graphics)
        {
            if (OwnerNSXClock.AmTransparent7Seg)
            {
                backColor = TransparentColor;
            }
            else
            {
                backColor = NESixClock.ColorSchemes[OwnerNSXClock.ColorSchemeNo][(int)NESixClock.SchemeMembers._7SegBack].Color;
            }
            if ((bmpSeg == null)) 
            {
                try
                {
                    bmpSeg = new((int)graphics.VisibleClipBounds.Width, (int)graphics.VisibleClipBounds.Height);
                }
                catch { return; }
            }
            if (bmpSeg.Width != graphics.VisibleClipBounds.Width || bmpSeg.Height != graphics.VisibleClipBounds.Height )
            {
                try
                {
                    bmpSeg = new((int)graphics.VisibleClipBounds.Width, (int)graphics.VisibleClipBounds.Height);
                }
                catch { return; }
            }


            
            if (OwnerNSXClock.IsMouseOverClock && !OwnerNSXClock.AmFull7Seg)
            {
                backColor = NESixClock.ColorSchemes[OwnerNSXClock.ColorSchemeNo][(int)NESixClock.SchemeMembers._7SegHover].Color;
            }
            else
            {
                if(!OwnerNSXClock.AmTransparent7Seg)
                backColor = NESixClock.ColorSchemes[OwnerNSXClock.ColorSchemeNo][(int)NESixClock.SchemeMembers._7SegBack].Color;
            }
            
            Graphics bmpGraphics = Graphics.FromImage(bmpSeg);
            
            bmpGraphics.Clear(NESixClock.TransparentColor);
            
            bool isDrawnSeg = false;
            float wid = graphics.VisibleClipBounds.Width;
            float hei = graphics.VisibleClipBounds.Height;
            Color polygonColortmp = NESixClock.ColorSchemes[OwnerNSXClock.ColorSchemeNo][(int)SchemeMembers._7SegFill].Color; // (int)NESixClock.SchemeMembers._7SegFill];
            
            SolidBrush? polySegSuggestedOutline = null;
            SetupSegments();
            SolidBrush? polygonClearColor = new SolidBrush(backColor);
            SetupSegments();

            for (int ii = 1; ii < 12; ii++)
            {
                polySegSuggestedOutline = new SolidBrush(polySegments[ii].outlineColor);
                isDrawnSeg = (digits[Number].Contains(ii));
                segment scaled = new segment();
                polySegment.SegWid = wid;
                polySegment.SegHei = hei;
                polySegment.xMargin = wid / 16;
                polySegment.yMargin = hei / 16;
                segment showSeg = squareSegments[ii];

                Pen? polyOutlineColor = new Pen(polygonClearColor,5);
                PointF[] angles = polySegments[ii].GetScaledPoints(0, 100);
                bmpGraphics.FillPolygon(polygonClearColor, angles);
                bmpGraphics.DrawPolygon(polyOutlineColor, angles);
                //polyOutlineColor = new Pen( Color.Cyan,2);
                //if(ii<10)  bmpGraphics.DrawPolygon(polyOutlineColor, angles);

            }
            for (int ii = 1; ii < 12; ii++)
            {
                isDrawnSeg = (digits[Number].Contains(ii));
                segment scaled = new segment();
                polySegment.SegWid = wid;
                polySegment.SegHei = hei;
                polySegment.xMargin = wid / 24;
                polySegment.yMargin = hei / 24;
                segment showSeg = squareSegments[ii];

                
                
                if (isDrawnSeg)
                {
                    PointF[] angles = polySegments[ii].GetScaledPoints(0, 100);
                    //bmpGraphics.FillRectangle(Brushes.Green, 0, 0, bmp.Size.Width, bmp.Size.Height  );
                    SolidBrush polygonColor = new SolidBrush(NESixClock.ColorSchemes[OwnerNSXClock.ColorSchemeNo][(int)SchemeMembers._7SegFill].Color); // (int)NESixClock.SchemeMembers._7SegFill];);
                    bmpGraphics.FillPolygon(polygonColor, angles);
                    Pen? polyOutlineColor = polyOutlineColor = new Pen(NESixClock.ColorSchemes[OwnerNSXClock.ColorSchemeNo][(int)NESixClock.SchemeMembers._7SegOutline].Color, (wid / 48));
                    bmpGraphics.DrawPolygon(polyOutlineColor, angles);
                }
                graphics.DrawImage(bmpSeg, 0, 0, bmpGraphics.VisibleClipBounds.Width, bmpGraphics.VisibleClipBounds.Height);
                //polygonColor?.Dispose();
                polySegSuggestedOutline?.Dispose();
            }
        }

        private void _7Segment_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Right)
            {
                ((SegmentClockCtl)OwnerControl).SegmentClockCtl_Click(sender, me);
            }
        }

        private void _7Segment_MouseMove(object sender, MouseEventArgs e)
        {
            OwnerNSXClock.Invoke(OwnerNSXClock.ClockPBox_MouseMove, new object[] { OwnerNSXClock, e });
        }
    }


    public class segment
    {
        public int index;
        public float X1, Y1, W1, H1;
        public float X2, Y2, W2, H2;
        public float _xR, Hei, _yB;
        public float calcX1right { get { return X1 + W1; } }
        public float calcY1bot { get { return Y1 + H1; } }
    }
    public class polySegment
    {
        public static float SegHei, SegWid;
        public static float xMargin = -1, xRmargin = -1;
        public static float yMargin = -1, yBottomMargin = -1;
        public float xMin = 0, xMax = 100;
        public string Desc = "";
        public int index;
        public Color outlineColor;
        public List<PointF>? unScaledPoints;
        public PointF[] GetScaledPoints(float MinX, float MaxX)
        {
            xMin = MinX;    xMax = MaxX;
            if ((SegHei == 0) || SegWid == 0 || xMargin<0 || yMargin < 0) return new PointF[0];
            return scaledPoints.ToArray();
        }
        List<PointF> scaledPoints
        {
            get
            {
                List<PointF> newPoints = new();
                PointF newPointf = new PointF();
                for (int iSP = 0; iSP < unScaledPoints.Count; iSP++)
                {
                    PointF p = unScaledPoints[iSP];
                    float fXX = (float)p.X;
                    fXX = Math.Min(fXX, xMax);
                    fXX = Math.Max(fXX, xMin);

                    float fYY = (float)p.Y;

                    newPointf.X = (fXX / 100) * (SegWid - (xMargin * 2)) + xMargin ;
                    newPointf.Y = (fYY / 100) * (SegHei - (yMargin * 2)) + yMargin;
                    newPoints.Add(Point.Round(newPointf));
                }
                return newPoints;
            }


        }
    }
}
