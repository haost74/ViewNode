using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Perceptron.Utility
{
    public class Colors
    {
        //(Brush)bc.ConvertFrom("#E5E7E9");
        private BrushConverter bc = new BrushConverter();
        private string actionColorEllipse = "#ff0000";
        public Brush ActionColorEllipse
        {
            get { return (Brush)bc.ConvertFrom(actionColorEllipse);}
        }

        private string colorEllipse = "#00ffbf";
        public Brush ColorEllipse
        {
            get { return (Brush)bc.ConvertFrom(colorEllipse); }
        }
    }
}
