using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Clockv2.nn
{
    public static class consts
    {
        public const UInt32 WM_MOUSEWHEEL = 0x20a;
        public const UInt32 WM_KEYDOWN = 0x0100;
    }

    public class ScrollMessageFilter : IMessageFilter
    {
        public ScrollMessageFilter(Control OwnerCtl)
        {
            ownerCtl = OwnerCtl;
        }
        Control ownerCtl;
        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point pt);
     //   [DllImport("user32.dll")]
     //   private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        IntPtr ownerhWnd = IntPtr.Zero;
        int count = 0;
        public bool PreFilterMessage(ref Message m)
        {
            if(ownerCtl.Parent == null) { return false; }
            try
            {
                count = ((NESixClock)(ownerCtl.Parent)).GetTimerInterval();
                if (m.HWnd == IntPtr.Zero) return false;
                if (m.Msg == consts.WM_KEYDOWN)
                { 

                }
                else
                if (m.Msg == consts.WM_MOUSEWHEEL)
                {
                    // WM_MOUSEWHEEL, find the control at screen position m.LParam
                    Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                    IntPtr hWnd = WindowFromPoint(pos);
                    if (ownerCtl.Handle != IntPtr.Zero)
                    {
                        if (ownerCtl.Handle == m.HWnd)
                        {
                            if (Control.FromHandle(hWnd) != null)
                            {
                                //SendMessage(hWnd, m.Msg, m.WParam, m.LParam);
                                string sSwitch = Convert.ToString(m.WParam.ToInt64(), 16);
                                switch (sSwitch)
                                {
                                    case ("780000"):
                                        {
                                            count -= count / 10;
                                            break;
                                        }
                                    case ("ff880000"):
                                        {
                                            count += count / 10;
                                            break;
                                        }
                                    case ("ffffffffff880000"):  // int64?
                                        {
                                            count += count / 10;
                                            break;
                                        }
                                }
                            }
                            count = Math.Max(count, 20);
                            count = Math.Min(count, 2500);
                            ((NESixClock)(ownerCtl.Parent)).SetTimerInterval(count);
                            
                            ((NESixClock)(ownerCtl.Parent)).T_Tick(this, new EventArgs());
                            ((PictureBox)(ownerCtl)).Parent.Invalidate();
                            ((PictureBox)(ownerCtl)).Invalidate();
                            return true;
                        }

                    }
                }
            }
            catch
            { }
            return false;
        }
        
            
        
    }
}
