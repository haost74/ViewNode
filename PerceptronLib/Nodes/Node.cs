using System;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using PerceptronLib.Utility;

namespace PerceptronLib.Nodes
{
    public class Node : ISign
    {
        public double Value { get; set; } = 0;

        public double Weight { get; set; } = 0;

        private List<Tuple<double, double>> input = new List<Tuple<double, double>>();
        public List<Tuple<double, double>> Input
        {
            get { return input; }
            set
            {
                input = value;
            }
        }

        private double output = 0;
        public double Output
        {
            get { return output; }
            set
            {
                output = value;
            }
        }

         Action<double, double> ISign.SignalTransmission { get; set; }

        public void Signal(double value, double weight)
        {

        }

    }


}
