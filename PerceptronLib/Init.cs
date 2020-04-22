using PerceptronLib.Nodes;
using PerceptronLib.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronLib
{

    public class Perceptron
    {
        private int[,] input = null;
        Utility.ParamMin pm = new Utility.ParamMin();
        public Matrix<Node> nodes = null;

        public Perceptron(int Row, int Column)
        {
            //Neuron2 nr = new Neuron2();
            //nr.init();
            nodes = Matrix<Node>.CreateIdentityMatrix(Row, Column);
            
        }
    }


    /*
     
        static void Main(string[] args)
        {
            int[,] input = new int[,] { {1,0}, {1,1}, {0,1}, {0,0} };
            int[] outputs = { 0, 1, 0, 0 };

            Random r = new Random();

            double[] weights = { r.NextDouble(), r.NextDouble(), r.NextDouble() };

            double learningRate = 1;
            double totalError = 1;

            while (totalError > 0.2)
            {
                totalError = 0;
                for (int i = 0; i < 4; i++)
                {
                    int output = calculateOutput(input[i, 0], input[i, 1], weights);

                    int error = outputs[i] - output;

                    weights[0] += learningRate * error * input[i, 0];
                    weights[1] += learningRate * error * input[i, 1];
                    weights[2] += learningRate * error * 1;

                    totalError += Math.Abs(error);
                }

            } 

            Console.WriteLine("Results:");
            for (int i = 0; i < 4; i++)
                Console.WriteLine(calculateOutput(input[i, 0], input[i, 1], weights));

            Console.ReadLine();

        }

        private static int calculateOutput(double input1, double input2, double[] weights)
        {
            double sum = input1 * weights[0] + input2 * weights[1] + 1 * weights[2];
            return (sum >= 0) ? 1 : 0;
        }
    }
         
         */
}
