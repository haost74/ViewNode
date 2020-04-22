using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerceptronLib.Nodes
{
    class Neuron
    {
        /// <summary>
        /// номер нейрона в Perceptron
        /// </summary>
        public Tuple<int, int> NumNode { get; set; }
        /// <summary>
        /// входные сигналы нейрона
        /// item1 - значение сигнала
        /// item2 - вес
        /// </summary>
        public List<Tuple<double, double>> Input { get; set; } = new List<Tuple<double, double>>();
        public Neuron(int numRow = 0, int numCoumn = 0)
        {
            NumNode = new Tuple<int, int>(numRow, numCoumn);
        }

        public List<double> Init(out double output)
        {
            List<double> res = new List<double>();
            for(int i = 0; i < Input.Count; ++i)
            {
                res.Add(Sum(Input[i]));
            }
            output = Sigmoid(res.Sum());
            return res;
        }

        private double Sum(Tuple<double, double> xw)
        {
            //res = Math.Abs(xw.Item1) * Math.Abs(xw.Item2) * Math.Cos(xw.Item1*xw.Item2);
            return xw.Item1 * xw.Item2;
        }
        /// <summary>
        /// функция активации
        /// </summary>
        /// <param name="re">сумма умножения вксов и значений входных параметров</param>
        /// <returns>результат</returns>
        private double Sigmoid(double re)
        {
            return 1 / 1 + Math.Pow(Math.E, re);
        }
    }
}
