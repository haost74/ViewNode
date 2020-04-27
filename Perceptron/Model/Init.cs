using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        //на сколько частей разбита активная часть Canvas по горизонтали
        private int allStepWigth = 0;
        //на сколько частей разбита активная часть Canvas по вертикали
        private int allStepHeight = 0;

        private double actionFieldW = 0;
        private double actionFieldH = 0;

        private double radius = 0;
        private Canvas mainCanvas = null;
        private delegate void DisplayHandler();

        /// <summary>
        /// создание матрицы 
        /// </summary>
        /// <param name="row">количество введеных пользователем строк</param>
        /// <param name="column">количество введеных пользователем столбцов</param>
        /// <param name="height">размер по высоте Canvas</param>
        /// <param name="width">размер по ширине Canvas</param>
        public Init(int row, int column, double height, double width, Canvas canvas)
        {
            canvas.Children.Clear();
            DisplayHandler dh = new DisplayHandler(callback);
            mainCanvas = canvas;
            this.height = height;
            this.width = width;
            allStepWigth = Algo((_x) => { return 2 * _x + 1; }, row);
            allStepHeight = Algo((_x) => { return 2 * _x + 1; }, column);
            actionFieldW = Algo(_x => { return Math.Round((_x - 300) / allStepWigth); }, height);
            actionFieldH = Algo(_x => { return Math.Round((_x - 300) / allStepHeight); }, width);

            radius = actionFieldW < actionFieldH ? actionFieldW : actionFieldH;
            matrixs = new PerceptronLib.Utility.Matrix<PerceptronLib.Nodes.ViewNode>(row, column);
            BrushConverter bc = new BrushConverter();

            for (int i = 0; i < matrixs.Row; ++i)
            {
                for (int j = 0; j < matrixs.Column; ++j)
                {
                    var node = new PerceptronLib.Nodes.ViewNode();
                    node.Row = i + 1;
                    node.Column = j + 1;
                    node.Fill = (Brush)bc.ConvertFrom("#E5E7E9");
                    var el = node.GetEllipse($"X = {node.Column} Y = {node.Row}", radius);
                    
                    AddCanvas(el, i * radius + radius + (i * radius), j * radius + radius + (j * radius));
                    matrixs[i, j] = node;
                }
            }

            dh.BeginInvoke(null, null);
        }

        private void callback()
        {
            for (int i = 0; i < matrixs.Column; ++i)
            {
                if (i <= matrixs.Row - 2)
                    for (int j = 0; j < matrixs.Column; ++j)
                    {
                        var ovner = matrixs[i, j];
                        var node = matrixs[i + 1, j];
                    }
            }
        }


        //private void AddLine()
        //{
        //    //for (int i = 0; i < matrixs.Row; ++i)
        //    //{
        //    //    for (int j = 0; j < matrixs.Column; ++j)
        //    //    { 

        //    //    }
        //    //}

        //    for (int j = 0; j < matrixs.Column; ++j)
        //    {

        //    }

        //}

        private void AddCanvas(UIElement el, double x, double y)
        {
            if (mainCanvas == null) return;
            el.SetValue(Canvas.LeftProperty, y);
            el.SetValue(Canvas.TopProperty, x);
            mainCanvas.Children.Add(el);
        }

        /// <summary>
        /// расчитывает количество 
        /// шаго всего для деления 
        /// по вертикали и по горизонтали Canvas
        /// </summary>
        /// <returns></returns>
        private (double w, double h) CountallStep()
        {
            return (10, 5);
        }

        /// <summary>
        ///на сколько частей всего
        /// поделен Canvas
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private int AllStep(int n) => 2 * n + 1;

        private T Algo<T>(Func<T, T> func, T x) => func(x);

        private (int, string) GetR(double x, double y) => (10, "");
    }
}
