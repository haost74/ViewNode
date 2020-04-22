using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Perceptron.Utility
{
    public class GenerateLine
    {
        public Line Generate(double x, double y)
        {
            Line res = new Line();
            res.SnapsToDevicePixels = true;
            res.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            res.StrokeThickness = 0.8;

            return res;
        }

        public Line newLine(double x1, double y1, double x2, double y2, Brush brush, Canvas can)
        {
            Line line = new Line();

            line.X1 = x1;
            line.X2 = x2;
            line.Y1 = y1;
            line.Y2 = y2;

            //line.X1 = x1;
            //line.X2 = y2;
            //line.Y1 = y1;
            //line.Y2 = x2;

            line.StrokeThickness = 1;
            line.Stroke = brush;

            // https://stackoverflow.com/questions/2879033/how-do-you-draw-a-line-on-a-canvas-in-wpf-that-is-1-pixel-thick
            line.SnapsToDevicePixels = true;
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            Canvas.SetZIndex(line, -1);
            can.Children.Add(line);

            return line;
        }
    }
}
