using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronLib.Utility
{
    public interface ISign
    {
        int Row { get; set; }
        int Column { get; set; }
        double Value { get; set; }
        double Weight { get; set; }

        Action<double, double> SignalTransmission { get; set; }

        void Signal(double value, double weight);
    }
}
