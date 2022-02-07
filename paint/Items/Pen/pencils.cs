using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace paint.Items.Pencils
{
    class pencils
    {
        private Color color;
        private Point pts;
        private int size;
        private GraphicsPath gp;
        private int n;

        public pencils(){
            color = Color.Black;
            size = 10;
            gp = new GraphicsPath();
            n = 0;
        }
        public pencils(int size_)
        {
            color = Color.Black;
            size = size_;
            gp = new GraphicsPath();
            n = 0;
        }

        public pencils(Color color_)
        {
            color = color_;
            size = 10;
            gp = new GraphicsPath();
            n = 0;
        }

        public pencils(Color color_, int size_)
        {
            color = color_;
            size = size_;
            gp = new GraphicsPath();
            n = 0;
        }

        public void setColor(Color color_)
        {
            color = color_;
        }

        public void setSize(int size_)
        {
            size = size_;
        }

        public void addPoint_start(Point pt)
        {
            if (n > 0)
                gp.AddLine(pts, pt);
            pts = pt;
            n++;
        }

        public void addPoint(Point pt) {
            if (n > 0)
                gp.AddLine(pts, pt);
            pts = pt;
            n++;
        }

        public void addPoint_end(Point pt)
        {
            if (n > 0)
                gp.AddLine(pts, pt);
            pts = pt;
            n++;
        }

        public void Draw(Graphics g)
        {
            if (n > 0)
            {
                Pen pn = new Pen(color, size);

                if (n == 0)
                {
                    g.DrawLine(pn, pts, pts);
                }
                else {
                    g.DrawPath(pn, gp);
                }
                
            }
        }
    }
}
