using System.Collections.Generic;
using System.Windows.Shapes;

namespace PerceptronLib.Nodes
{
    public class ViewNode : Node
    {
        private Ellipse ellipse = null;
        public Ellipse Ellipse
        {
            get { return ellipse; }
            set
            {
                ellipse = value;
            }
        }

        private List<Line> lines = new List<Line>();
        public List<Line> List
        {
            get { return lines; }
            set
            {
                lines = value;
            }
        }
    }
}
