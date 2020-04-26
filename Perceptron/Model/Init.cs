using System;
using System.Windows;
using System.Windows.Controls;

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
        Canvas mainCanvas = null;

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
            mainCanvas = canvas;
            this.height = height;
            this.width = width;
            allStepWigth = Algo((_x) => { return 2 * _x + 1; }, column);
            allStepHeight = Algo((_x) => { return 2 * _x + 1; }, row);
            actionFieldW = Algo(_x => { return Math.Round((_x - 300) / allStepWigth); }, width);
            actionFieldH = Algo(_x => { return Math.Round((_x - 300) / allStepWigth); }, height);

            radius = actionFieldW < actionFieldH ? actionFieldW : actionFieldH;
            matrixs = new PerceptronLib.Utility.Matrix<PerceptronLib.Nodes.ViewNode>(column, row);

            for(int i = 0; i < matrixs.Column; ++i)
            {
                for(int j = 0; j < matrixs.Row; ++j)
                {
                    var node = new PerceptronLib.Nodes.ViewNode();
                    node.Row = i + 1;
                    node.Column = j + 1;
                    var el = node.GetEllipse($"X = {node.Column} Y = {node.Row}", radius);
                    
                    AddCanvas(el, i*radius + radius + (i*radius), j*radius + radius + (j*radius));
                    matrixs[j, i] = node;
                }
            }

            //for (int i = 0; i < allStepWigth; ++i)
            //{
            //    stepRow += radius;
            //    stepColumn = 0;
            //    if(i%2 != 0 && i < row)
            //    {
            //        for(int j = 0; j < allStepHeight; ++j)
            //        {
            //            stepColumn += radius;
            //            if(j%2 != 0 && j < column)
            //            {                            
            //                var node = new PerceptronLib.Nodes.ViewNode();
            //                node.Row = ++x;
            //                node.Column = ++y;

            //                var el = node.GetEllipse($"X = {x} Y = {y}", radius);
            //                AddCanvas(el, stepRow, stepColumn);
            //                matrixs[x, y] = node;
            //            }
            //        }
            //    }
            //}
        }


        private void AddCanvas(UIElement el, double x, double y)
        {
            if (mainCanvas == null) return;
            el.SetValue(Canvas.LeftProperty, x);
            el.SetValue(Canvas.TopProperty, y);
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
