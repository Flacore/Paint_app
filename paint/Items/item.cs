using Antialiasing_app.Curves;
using paint.Items.Bitmapp;
using paint.Items.Circle;
using paint.Items.Line;
using paint.Items.Pencils;
using paint.Items.Square;
using paint.Items.Text;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace paint.Items
{
    class item
    {
        private int type;

        private pencils iPencil; //1
        private square iSquare; //2
        private text iText; //3
        private line iLine; //4
        private elipse iElipse; //5
        private BezierCurve bezierCurve; //6
        private bitmapp bmap; //7


        public item(int type_) {
            switch (type_) {
                case 1:
                    iPencil = new pencils();
                    type = 1;
                    break;
                case 2:
                    iSquare = new square();
                    type = 2;
                    break;
                case 3:
                    iText = new text();
                    type = 3;
                    break;
                case 4:
                    iLine = new line();
                    type = 4;
                    break;
                case 5:
                    iElipse = new elipse();
                    type = 5;
                    break;
                case 6:
                    bezierCurve = new BezierCurve();
                    type = 6;
                    break;
                case 7:
                    bmap = new bitmapp();
                    type = 7;
                    break;
            }
        }

        public int getType() {
            return type;
        }

        public void setColor(Color clr)
        {
            switch (type)
            {
                case 1:
                    iPencil.setColor(clr);
                    break;
                case 2:
                    iSquare.setColor(clr);
                    break;
                case 3:
                    iText.setColor(clr);
                    break;
                case 4:
                    iLine.setColor(clr);
                    break;
                case 5:
                    iElipse.setColor(clr);
                    break;
                case 6:
                    bezierCurve.setColor(clr);
                    break;
            }
        }

        public void setSize(int size_)
        {
            switch (type)
            {
                case 1:
                    iPencil.setSize(size_);
                    break;
                case 2:
                    iSquare.setSize(size_);
                    break;
                case 3:
                    iText.setSize(size_);
                    break;
                case 4:
                    iLine.setSize(size_);
                    break;
                case 5:
                    iElipse.setSize(size_);
                    break;
                case 6:
                    bezierCurve.setSize(size_);
                    break;
            }
        }

        public void addStart(Point pt) {
            switch (type)
            {
                case 1:
                    iPencil.addPoint_start(pt);
                    break;
                case 2:
                    iSquare.setStart(pt);
                    break;
                case 3:
                    iText.setPoint(pt);
                    break;
                case 4:
                    iLine.setStart(pt);
                    break;
                case 5:
                    iElipse.setStart(pt);
                    break;
                case 6:
                    bezierCurve.addStart(pt);
                    break;
            }
        }

        public void addPoint(Point pt)
        {
            switch (type)
            {
                case 1:
                    iPencil.addPoint(pt);
                    break;
                case 2:
                    iSquare.addPoint(pt);
                    break;
                case 3:
                    iText.setPoint(pt);
                    break;
                case 4:
                    iLine.addPoint(pt);
                    break;
                case 5:
                    iElipse.addPoint(pt);
                    break;
                case 6:
                    bezierCurve.addPoint(pt);
                    break;
            }
        }

        public void addEnd(Point pt)
        {
            switch (type)
            {
                case 1:
                    iPencil.addPoint_end(pt);
                    break;
                case 2:
                    iSquare.setEnd(pt);
                    break;
                case 3:
                    iText.setPoint(pt);
                    break;
                case 4:
                    iLine.setEnd(pt);
                    break;
                case 5:
                    iElipse.setEnd(pt);
                    break;
                case 6:
                    bezierCurve.addEnd(pt);
                    break;
            }
        }

        public void setText(string text) {
            if (type == 3)
                iText.setString(text);
        }

        public void setBitmap(Bitmap bp) {
            if (type == 7)
                bmap.setBitmap(bp);
        }

        public void Draw(Graphics g)
        {
            switch (type)
            {
                case 1:
                    iPencil.Draw(g);
                    break;
                case 2:
                    iSquare.Draw(g);
                    break;
                case 3:
                    iText.Draw(g);
                    break;
                case 4:
                    iLine.Draw(g);
                    break;
                case 5:
                    iElipse.Draw(g);
                    break;
                case 6:
                    bezierCurve.Draw(g);
                    break;
                case 7:
                    bmap.Draw(g);
                    break;
            }
        }
    }
}
