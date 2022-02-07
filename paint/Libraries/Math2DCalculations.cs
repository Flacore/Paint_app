using System.Drawing;

namespace Antialiasing_app.Tools
{
    public static class Math2DCalculations
    {
        public static float xMin = -296.0f;
        public static float xMax = 296.0f;
        public static float yMin = -225.0f;
        public static float yMax = 225.0f;
        
        private static int uMin = 0;
        private static int uMax = 592;
        private static int vMin = 450;
        private static int vMax = 0;

        /// <summary>
        /// Transforms world coordinates to screen coordinates
        /// </summary>
        /// <param name="worldPoint">Point in world coordinates</param>
        /// <returns>Transformed point in screen coordinates</returns>
        public static Point WorldCoordToWindowCoord(PointF worldPoint)
        {
            return new Point(
                (int)(((worldPoint.X - xMin) / (xMax - xMin)) * (uMax - uMin)) + uMin,
                (int)(((worldPoint.Y - yMin) / (yMax - yMin)) * (vMax - vMin)) + vMin
            );
        }

    }
}
