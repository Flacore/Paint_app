using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace paint.Items.Square
{
    class square
    {
        private Color color;
        private Point start;
        private Point end;
        private int size;

        public square() {
            color = Color.Black;
            int size = 10;
        }

        public square(int size_) {
            color = Color.Black;
            int size = size_;
        }

        public square(Color color_)
        {
            color = color_;
            int size = 10;
        }

        public square(int size_, Color color_)
        {
            color = color_;
            int size = size_;
        }

        public void setStart(Point pt) {
            start = pt;
            end = pt;
        }

        public void addPoint(Point pt) {
            end = pt;
        }

        public void setEnd(Point pt) {
            end = pt;
        }

        public void setColor(Color color_)
        {
            color = color_;
        }

        public void setSize(int size_)
        {
            size = size_;
        }

        public void Draw(Graphics g)
        {
            Pen pn = new Pen(color, size);
            Rectangle rect = new Rectangle(start.X, start.Y, end.X - start.X, end.Y - start.Y);
            g.DrawRectangle(pn, rect);
        }
    }
}
