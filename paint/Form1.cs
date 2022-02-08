using paint.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace paint
{
    public partial class AppSpace : Form
    {
        /*
        //1. Pencil
        //2. Sqare
        //3. Text
        //4. Line
        //5. Elipse
        //6. Bezier Curve
        //7. Bitmap
        //8. Color picker
         */

        private bool mouse_down;

        private int active;
        private Color chosen_color;

        private int font_size;
        private int line_width;

        private List<item> items;

        public AppSpace()
        {
            InitializeComponent();
            InitializedApplication();
        }

        private void InitializedApplication() {
            active = 1;

            mouse_down = false;

            items = new List<item>();

            chosen_color = Color.Black;
            colorpicked.BackColor = chosen_color;

            font_size = 10;
            font_resizer.Value = font_size;

            line_width = 5;
            linewidth_resizer.Value = line_width;

            hideHelpButtons();

            clearAllButtons();
            draw_button.BackColor = Color.Silver;

        }

        private void reject_button_Click(object sender, EventArgs e)
        {
            //TODO reject 
            if (active == 3)
                items.RemoveAt(items.Count - 1);
            hideHelpButtons();
            panelcenter.Invalidate();
        }

        private void accept_button_Click(object sender, EventArgs e)
        {
            //TODO accept
            if (active == 3)
                items[items.Count - 1].cancelActive();
            hideHelpButtons();
            panelcenter.Invalidate();
        }

        private void erase_button_Click(object sender, EventArgs e)
        {
            active = 1;
            chosen_color = Color.White;
            colorpicked.BackColor = chosen_color;
            clearAllButtons();
            erase_button.BackColor = Color.Silver;
            if (mouse_down)
            {
                items.RemoveAt(items.Count - 1);
                mouse_down = false;
            }
        }

        private void draw_button_Click(object sender, EventArgs e)
        {
            active = 1;
            clearAllButtons();
            draw_button.BackColor = Color.Silver;
            if (mouse_down)
            {
                items.RemoveAt(items.Count - 1);
                mouse_down = false;
            }
        }

        private void text__button_Click(object sender, EventArgs e)
        {
            active = 3;
            clearAllButtons();
            text__button.BackColor = Color.Silver;
            if (mouse_down)
            {
                items.RemoveAt(items.Count - 1);
                mouse_down = false;
            }
        }

        private void copycolor_button_Click(object sender, EventArgs e)
        {
            active = 8;
            clearAllButtons();
            copycolor_button.BackColor = Color.Silver;
            if (mouse_down)
            {
                items.RemoveAt(items.Count - 1);
                mouse_down = false;
            }
        }

        private void line__button_Click(object sender, EventArgs e)
        {
            active = 4;
            clearAllButtons();
            line__button.BackColor = Color.Silver;
            if (mouse_down)
            {
                items.RemoveAt(items.Count - 1);
                mouse_down = false;
            }
        }

        private void bezier__button_Click(object sender, EventArgs e)
        {
            active = 6;
            clearAllButtons();
            bezier__button.BackColor = Color.Silver;
            if (mouse_down)
            {
                items.RemoveAt(items.Count - 1);
                mouse_down = false;
            }
            showHelpButtons();
            //TODO bezier
        }

        private void circle_button_Click(object sender, EventArgs e)
        {
            active = 5;
            clearAllButtons();
            circle_button.BackColor = Color.Silver;
            if (mouse_down)
            {
                items.RemoveAt(items.Count - 1);
                mouse_down = false;
            }
        }

        private void sqare_button_Click(object sender, EventArgs e)
        {
            active = 2;
            clearAllButtons();
            sqare_button.BackColor = Color.Silver;
            if (mouse_down) {
                items.RemoveAt(items.Count - 1);
                mouse_down = false;
            }
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            if (items.Count > 0)
            {
                if(items[items.Count -1].getType() == 3)
                    hideHelpButtons();
                items.RemoveAt(items.Count - 1);
            }
            panelcenter.Invalidate();
            mouse_down = false;
        }

        private void ereseall_button_Click(object sender, EventArgs e)
        {
            hideHelpButtons();
            items.Clear();
            panelcenter.Invalidate();
            mouse_down = false;
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            int width = panelcenter.Size.Width;
            int height = panelcenter.Size.Height;

            Bitmap bm = new Bitmap(width, height);
            panelcenter.DrawToBitmap(bm, new Rectangle(0, 0, width, height));

            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Bitmap Image (.bmp)|*.bmp";
            sf.ShowDialog();
            var path = sf.FileName;
            if(path.Length>0)
                bm.Save(path);
        }

        private void load_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Bitmap Image (.bmp)|*.bmp";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                items.Clear();
                items.Add(new item(7));
                items[0].setBitmap(new Bitmap(openFileDialog.FileName));
            }
        }

        private void export_button_Click(object sender, EventArgs e)
        {
            int width = panelcenter.Size.Width;
            int height = panelcenter.Size.Height;

            Bitmap bm = new Bitmap(width, height);
            panelcenter.DrawToBitmap(bm, new Rectangle(0, 0, width, height));

            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";
            sf.ShowDialog();
            var path = sf.FileName;

            if (path.Length > 0)
                bm.Save(path);
        }

        private void whitecolore_Click(object sender, EventArgs e)
        {
            colorpicked.BackColor = Color.White;
            chosen_color = Color.White;
        }

        private void lightgreycolore_Click(object sender, EventArgs e)
        {
            colorpicked.BackColor = Color.LightGray;
            chosen_color = Color.LightGray;
        }

        private void greycolore_Click(object sender, EventArgs e)
        {
            colorpicked.BackColor = Color.DarkGray;
            chosen_color =Color.DarkGray;
        }

        private void darkgreycolore_Click(object sender, EventArgs e)
        {
            colorpicked.BackColor = Color.Gray;
            chosen_color = Color.Gray;
        }

        private void blackcolore_Click(object sender, EventArgs e)
        {
            colorpicked.BackColor = Color.Black;
            chosen_color = Color.Black;
        }

        private void pinkcolore_Click(object sender, EventArgs e)
        {
            colorpicked.BackColor = Color.Pink;
            chosen_color = Color.Pink;
        }

        private void redcolore_Click(object sender, EventArgs e)
        {
            colorpicked.BackColor = Color.Red;
            chosen_color = Color.Red;
        }

        private void purplecolore_Click(object sender, EventArgs e)
        {
            colorpicked.BackColor = Color.Purple;
            chosen_color = Color.Purple;
        }

        private void orangecolore_Click(object sender, EventArgs e)
        {
            colorpicked.BackColor = Color.Orange;
            chosen_color = Color.Orange;
        }

        private void yellowcolore_Click(object sender, EventArgs e)
        {
            colorpicked.BackColor = Color.Yellow;
            chosen_color = Color.Yellow;
        }

        private void greencolore_Click(object sender, EventArgs e)
        {
            colorpicked.BackColor = Color.Green;
            chosen_color = Color.Green;
        }

        private void bluecolore_Click(object sender, EventArgs e)
        {
            colorpicked.BackColor = Color.Blue;
            chosen_color = Color.Blue;
        }

        private void browncolore_Click(object sender, EventArgs e)
        {
            colorpicked.BackColor = Color.Brown;
            chosen_color = Color.Brown;
        }

        private void color_button_Click(object sender, EventArgs e)
        {
            ColorDialog colorPickingDialog = new ColorDialog();

            if (colorPickingDialog.ShowDialog() == DialogResult.OK)
                chosen_color = colorPickingDialog.Color;

            colorpicked.BackColor = chosen_color;
        }

        private void font_resizer_ValueChanged(object sender, EventArgs e)
        {
            font_size = (int)font_resizer.Value;
            if (accept_button.Visible)
            {
                if(active == 3)
                    items[items.Count - 1].setSize(font_size);
                panelcenter.Invalidate();
            }
        }

        private void linewidth_resizer_ValueChanged(object sender, EventArgs e)
        {
            line_width = (int)linewidth_resizer.Value;
        }

        private void panelcenter_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_down = true;
            if (active == 1)
            {
                items.Add(new item(1));
                items[items.Count - 1].addStart(new Point(e.Location.X, e.Location.Y));
                items[items.Count - 1].setColor(chosen_color);
                items[items.Count - 1].setSize(line_width);
            }
            if (active == 2)
            {
                items.Add(new item(2));
                items[items.Count - 1].addStart(new Point(e.Location.X, e.Location.Y));
                items[items.Count - 1].setColor(chosen_color);
                items[items.Count - 1].setSize(line_width);
            }
            if (active == 3) {
                if (items.Count > 0 && items[items.Count - 1].getType() == 3)
                {
                    items[items.Count - 1].selectPoint(new Point(e.Location.X, e.Location.Y));
                }
                else {
                    mouse_down = false;
                }
            }
            if (active == 4)
            {
                items.Add(new item(4));
                items[items.Count - 1].addStart(new Point(e.Location.X, e.Location.Y));
                items[items.Count - 1].setColor(chosen_color);
                items[items.Count - 1].setSize(line_width);
            }
            if (active == 5)
            {
                items.Add(new item(5));
                items[items.Count - 1].addStart(new Point(e.Location.X, e.Location.Y));
                items[items.Count - 1].setColor(chosen_color);
                items[items.Count - 1].setSize(line_width);
            }
            panelcenter.Invalidate();
        }

        private void panelcenter_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouse_down)
            {
                if (active == 1)
                {
                    items[items.Count - 1].addEnd(new Point(e.Location.X, e.Location.Y));
                }
                if (active == 2)
                {
                    items[items.Count - 1].addEnd(new Point(e.Location.X, e.Location.Y));
                }
                if (active == 3)
                {
                    items[items.Count - 1].cancelSelection();
                }
                if (active == 4)
                {
                    items[items.Count - 1].addEnd(new Point(e.Location.X, e.Location.Y));
                }
                if (active == 5)
                {
                    items[items.Count - 1].addEnd(new Point(e.Location.X, e.Location.Y));
                }
                if (active == 8)
                {
                    getColor(new Point(e.Location.X, e.Location.Y));
                }
            }
            mouse_down = false;
            panelcenter.Invalidate();
        }

        private void panelcenter_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse_down)
            {
                if (active == 1)
                {
                    items[items.Count - 1].addPoint(new Point(e.Location.X, e.Location.Y));
                }
                if (active == 2)
                {
                    items[items.Count - 1].addPoint(new Point(e.Location.X, e.Location.Y));
                }
                if (active == 3) {
                    items[items.Count - 1].changePoint(new Point(e.Location.X, e.Location.Y));
                }
                if (active == 4)
                {
                    items[items.Count - 1].addPoint(new Point(e.Location.X, e.Location.Y));
                }
                if (active == 5)
                {
                    items[items.Count - 1].addPoint(new Point(e.Location.X, e.Location.Y));
                }
                panelcenter.Invalidate();
            }
        }

        private void panelcenter_MouseClick(object sender, MouseEventArgs e)
        {
            if (active == 3 && !accept_button.Visible) {
                showHelpButtons();
                items.Add(new item(3));
                items[items.Count - 1].addStart(new Point(e.Location.X, e.Location.Y));
                items[items.Count - 1].setColor(chosen_color);
                items[items.Count - 1].setSize(font_size);
                items[items.Count - 1].setText("text");
                panelcenter.Invalidate();
            }
            if (active == 8) {
                getColor(new Point(e.Location.X,e.Location.Y));
            }
        }

        private void panelcenter_Paint(object sender, PaintEventArgs e)
        {
            foreach (var itm in items) {
                itm.Draw(e.Graphics);
            }
        }

        private void getColor(Point pt) {
            int width = panelcenter.Size.Width;
            int height = panelcenter.Size.Height;

            Bitmap bm = new Bitmap(width, height);
            panelcenter.DrawToBitmap(bm, new Rectangle(0, 0, width, height));
            Color clr = bm.GetPixel(pt.X, pt.Y);

            colorpicked.BackColor = clr;
            chosen_color = clr;
        }

        private void clearAllButtons() {
            erase_button.BackColor = Color.Gainsboro;
            draw_button.BackColor = Color.Gainsboro;
            text__button.BackColor = Color.Gainsboro;
            copycolor_button.BackColor = Color.Gainsboro;
            line__button.BackColor = Color.Gainsboro;
            bezier__button.BackColor = Color.Gainsboro;
            circle_button.BackColor = Color.Gainsboro;
            sqare_button.BackColor = Color.Gainsboro;
        }

        private void hideHelpButtons() {
            reject_button.Visible = false;
            accept_button.Visible = false;
            textText.Visible = false;
        }

        private void showHelpButtons()
        {
            reject_button.Visible = true;
            accept_button.Visible = true;
            textText.Visible = true;
        }

        private void panelcenter_MouseHover(object sender, EventArgs e)
        {
        }

        private void textText_TextChanged(object sender, EventArgs e)
        {
            if (accept_button.Visible)
            {
                if (active == 3)
                    items[items.Count - 1].setText(textText.Text);
                panelcenter.Invalidate();
            }
        }
    }
}
