using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.Model
{
    public class Init
    {
        PerceptronLib.Utility.Matrix<PerceptronLib.Nodes.ViewNode> matrixs = null;
        /// <summary>
        /// ширина Cnvas
        /// </summary>
        private double width = 0;
        /// <summary>
        /// высота Cnvas
        /// </summary>
        private double height = 0;

        public Init(int row, int column, double height, double width)
        {
            this.height = height;
            this.width = width;
            matrixs = new PerceptronLib.Utility.Matrix<PerceptronLib.Nodes.ViewNode>(row, column);
        }

        /// <summary>
        /// расчитывает количество 
        /// шаго всего для деления 
        /// по вертикали и по горизонтали Canvas
        /// </summary>
        /// <returns></returns>
        private (double w, double h) CountallStep()
        {
            return  ( 10, 5);
        }
    }
}
