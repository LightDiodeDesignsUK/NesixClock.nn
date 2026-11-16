using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Clockv2.nn
{
    public class NealsCheck : Control
    {
        public Color CheckColor = Color.Black;
        public Color NotCheckColor = SystemColors.Window;
        public bool Checked = false;
        public void OnLoad(object? sender, EventArgs ea)
        {
            this.Size = new Size(32, 32);
            backgroundPicture.Size = this.Size;
            backgroundPicture.Paint += NealsCheck_Paint;
            Invalidate();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // NealsCheck
            // 
            Font = new Font("Wingdings", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ResumeLayout(false);
        }

        private void NealsCheck_Resize(object? sender, EventArgs e)
        {
            //backgroundPicture.Size = this.Size;
        }

        public bool AutoCheck = false;
        public delegate void changedArgs(object? sender, EventArgs ea);
        public event changedArgs? CheckedChanged;
        public PictureBox backgroundPicture;
        public NealsCheck()
        {
            backgroundPicture = new PictureBox();
            backgroundPicture.Click += OnClick;
            this.Resize += NealsCheck_Resize;
            backgroundPicture.Dock = DockStyle.Fill;
            backgroundPicture.Paint += NealsCheck_Paint;
            this.Controls.Add(backgroundPicture);
            backgroundPicture.BringToFront();
        }
        public new string? Text;
        void OnClick(object? sender, EventArgs ea)
        {
            if (true) //AutoCheck)
            {
                Checked = !Checked;
                CheckedChanged?.Invoke(this, ea);
            }
        }
        private void NealsCheck_Paint(object? sender, PaintEventArgs e)
        {
            //SizeF r = e.Graphics.ClipBounds.Size;
            SizeF r = this.Size;
            if (r == SizeF.Empty) return;
            e.Graphics.FillRectangle(Brushes.White, RectangleF.FromLTRB(0, 0, r.Width, r.Height));
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(1, 1, (int)(r.Width - 2), (int)(r.Height - 2)));

            Font wingDings = new Font("Wingdings", 10);
            if (Checked)
            {
                e.Graphics.FillEllipse(new SolidBrush(CheckColor), new RectangleF((int)(r.Width / 4), r.Height / 8, r.Width / 2, (r.Height / 4 * 3)));
                string sTick = "" + ((char)0xFC);
                e.Graphics.DrawString(((char)0xFC).ToString(), wingDings, Brushes.White, (int)(r.Width / 4), r.Height / 3);
            }
            
            FontFamily f = this.Font.FontFamily;
            if (!Checked) e.Graphics.FillEllipse(new SolidBrush(NotCheckColor), new RectangleF((int)(r.Width / 4), r.Height / 8, r.Width / 2, (r.Height / 4 * 3)));
            if (!Checked) e.Graphics.DrawString("X", Font, Brushes.Black, (int)(r.Width / 4), r.Height / 4);
            wingDings.Dispose();
        }
    }
}
