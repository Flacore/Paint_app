using System.Windows.Forms;

namespace Editor2D
{
    public class DoubleBufferPanel : Panel
    {
        public DoubleBufferPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | //Do not erase the background, reduce flicker
                 ControlStyles.OptimizedDoubleBuffer | //Double buffering
                 ControlStyles.UserPaint, //Use a custom redraw event to reduce flicker
                 true);
        }
    }
}
