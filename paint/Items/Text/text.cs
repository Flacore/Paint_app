using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace paint.Items.Text
{
    class text
    {
        private Color color;
        private string texts;
        private Point pt;
        private int size;

        private bool active;
        private bool selected;

        public text()
        {
            color = Color.Black;
            int size = 10;
            active = true;
            selected = false;
        }

        public text(int size_)
        {
            color = Color.Black;
            int size = size_;
            active = true;
            selected = false;
        }

        public text(Color color_)
        {
            color = color_;
            int size = 10;
            active = true;
            selected = false;
        }

        public text(int size_, Color color_)
        {
            color = color_;
            int size = size_;
            active = true;
            selected = false;
        }

        public bool isActive() {
            return active;
        }

        public void cancelActive() {
            active = false;
        }

        public void cancelSelection() {
            selected = false;
        }

        public void selectPoint(Point pt_) {
            if (active)
            {
                if (pt_.X + 5 > pt.X && pt.X > pt_.X - 5
                    && pt_.Y + 5 > pt.Y && pt.Y > pt_.Y - 5) {
                    selected = true;
                }
            }
        }

        public void changePoint(Point pt_) {
            if (selected && active)
                pt = pt_;
        }

        public void setColor(Color color_)
        {
            color = color_;
        }

        public void setSize(int size_)
        {
            size = size_;
        }

        public void setPoint(Point pt_) {
            pt = pt_;
        }

        public void setString(string text_) {
            texts = text_;
        }

        public void Draw(Graphics g)
        {
            Font fnt = new Font("Times new romane", size);
            g.DrawString(texts, fnt, new SolidBrush(color), pt.X, pt.Y);
            if (active){
                var rect = new Rectangle(new Point(pt.X - 5, pt.Y - 5), new Size(10, 10));
                if (selected)
                    g.FillRectangle(Brushes.Green, rect);
                else
                    g.FillRectangle(Brushes.DarkOrange, rect);
                g.DrawRectangle(Pens.Black, rect);
            }

        }
    }
}
