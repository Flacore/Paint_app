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

        public text()
        {
            color = Color.Black;
            int size = 10;
        }

        public text(int size_)
        {
            color = Color.Black;
            int size = size_;
        }

        public text(Color color_)
        {
            color = color_;
            int size = 10;
        }

        public text(int size_, Color color_)
        {
            color = color_;
            int size = size_;
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
        }
    }
}
