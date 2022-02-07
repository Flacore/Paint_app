using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace paint.Items.Bitmapp
{
    class bitmapp
    {
        private Bitmap bp;

        public bitmapp()
        {
        }

        public bitmapp(Bitmap bp_) {
            bp = bp_;
        }

        public void setBitmap(Bitmap bp_) {
            bp = bp_;
        }

        public void Draw(Graphics g)
        {
            g.DrawImageUnscaledAndClipped(bp, new Rectangle(0, 0, 1491, 650));
        }
    }
}
