using PerceptronLib.Utility;
using System;
using System.Collections.Generic;

namespace PerceptronLib.Nodes
{
    public class Neuron2
    {
        public int X = 2;
        public int[] N = new int[] { 5, 3, 1 };
        int Xn = 20;
        int P = 2;
        int left = 0;
        int right = 1;
        ParamMin pm = new ParamMin();


        private int L = 0;
        private double[][][] w = null;
        private int[] S = new int[0];
        private int[] outA = new int[0];
        private int[] Ts = new int[0];
        private string[,] Er = new string[,] { { "Итерация", "Ошибка" } };
        private int interator = 0;

        public Neuron2()
        {
            L = N.Length;
            pm.rn = new Random();
            pm.min = -0.5;
            pm.max = 0.5;
        }

        public double[][][] init()
        {
            w = new double[L][][];

            for(int i = 0; i < w.Length; ++i)
            {
                w[i] = new double[N[i]][];
                for(int j = 0; j < w[i].Length; ++j)
                {
                    var n = i > 0 ? N[i - 1] : X;
                    w[i][j] = new double[n];
                    for (int k = 0; k < n; ++k)
                    {
                        w[i][j][k] = pm.ran();
                    }
                }
            }
            return w;
        }

        public void neuron()
        {
            S = new int[N.Length];
            outA = new int[N.Length];
            for(int i = 0; i < N.Length; ++i)
            {
                //S[i] = new int[]
            }
        }

    }
}
