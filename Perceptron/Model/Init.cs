using PerceptronLib.Nodes;
using PerceptronLib.Utility;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            ViewNode.Coordinates.Clear();
            mainCanvas = canvas;
            this.height = height;
            this.width = width;
            allStepWigth = Algo((_x) => { return 2 * _x + 1; }, row);
            allStepHeight = Algo((_x) => { return 2 * _x + 1; }, column);
            actionFieldW = Algo(_x => { return Math.Round((_x - 300) / allStepWigth); }, height);
            actionFieldH = Algo(_x => { return Math.Round((_x - 300) / allStepHeight); }, width);

            radius = actionFieldW < actionFieldH ? actionFieldW : actionFieldH;
            //matrixs = new PerceptronLib.Utility.Matrix<PerceptronLib.Nodes.ViewNode>(row, column);
            matrixs = Matrix<PerceptronLib.Nodes.ViewNode>.CreateIdentityMatrix(row, column); ;
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
                    //var el = node.GetEllipse($"X = {i * radius + radius + (i * radius)} Y = {j * radius + radius + (j * radius)}", radius);
                    node.XMap = i * radius + radius + (i * radius);
                    node.YMap = j * radius + radius + (j * radius);
                    ViewNode.Coordinates.Add(new Tuple<double, double>(node.XMap, node.YMap));
                    AddCanvas(el, node.XMap, node.YMap);
                    matrixs[i, j] = node;

                    //if (i > 0)
                    //{
                    //    //var old = ViewNode.Coordinates.Where(x => x.Item2 == ViewNode.Coordinates[i].Item2 - radius * 2).ToList();
                    //    List<ViewNode> temp = new List<ViewNode>();
                    //    matrixs.ProcessFunctionOverData((n, m) =>
                    //    {
                    //        var mp = node.YMap - radius * 2;
                    //        if (n == i - 1)
                    //        {
                    //            temp.Add(matrixs[n, m]);
                    //        }
                    //    });


                    //    foreach (var nd in temp)
                    //    {
                    //        ViewNode.newLine(node.YMap + radius / 2, node.XMap + radius / 2, nd.YMap + radius / 2, nd.XMap + radius / 2, Brushes.Black, mainCanvas);
                    //    }
                    //}
                }
            }

            dh.BeginInvoke(null, null);
        }



        private void callback()
        {
            Task.Run(() =>
            {
                for (int i = 0; i < ViewNode.Coordinates.Count; ++i)
                {
                    var temp = ViewNode.Coordinates[i];
                    var old = ViewNode.Coordinates.Where(x => x.Item2 == ViewNode.Coordinates[i].Item2 - radius * 2).ToList();

                    foreach (var el in old)
                    {
                        MainWindow.main.Dispatcher.BeginInvoke((ThreadStart)delegate ()
                        {
                            ViewNode.newLine(temp.Item2 + radius / 2, temp.Item1 + radius / 2, el.Item2 + radius / 2, el.Item1 + radius / 2, Brushes.Black, mainCanvas);
                        });
                    }

                }
            });

        }


        private ViewNode GetNode(double x, double y)
        {
            ViewNode res = null;
            if (matrixs != null)
                for (int i = 0; i < matrixs.Row; ++i)
                    for (int j = 0; j < matrixs.Column; ++j)
                    {
                        if (matrixs[i, j].XMap == x && matrixs[i, j].YMap == y)
                        {
                            res = matrixs[i, j];
                            return res;
                        }
                    }
            return res;
        }

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
