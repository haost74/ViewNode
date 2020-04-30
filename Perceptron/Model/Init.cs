using PerceptronLib.Nodes;
using PerceptronLib.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

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
            //canvas.Children.Clear();
            ClearAsync(canvas)
                .ContinueWith(_ =>
                {
                    Thread.Sleep(200);
                    MainWindow.main.Dispatcher.BeginInvoke((ThreadStart)delegate ()
                    {
                        Drav(row, column, height, width, canvas);
                    });
                });
        }

        private void Drav(int row, int column, double height, double width, Canvas canvas)
        {
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
            matrixs = Matrix<PerceptronLib.Nodes.ViewNode>.CreateIdentityMatrix(row, column);
            BrushConverter bc = new BrushConverter();
            for (int i = 0; i < matrixs.Row; ++i)
            {
                for (int j = 0; j < matrixs.Column; ++j)
                {
                    var node = new PerceptronLib.Nodes.ViewNode();
                    node.Row = i + 1;
                    node.Column = j + 1;
                    node.Fill = Config.Cfg.Colors.ColorEllipse;//(Brush)bc.ConvertFrom("#E5E7E9");
                    var el = node.GetEllipse($"X = {node.Column} Y = {node.Row}", radius);
                    node.ActionLine += ActionLine;
                    node.Action += ActionCheckBox;
                    //var el = node.GetEllipse($"X = {i * radius + radius + (i * radius)} Y = {j * radius + radius + (j * radius)}", radius);
                    node.XMap = i * radius + radius + (i * radius);
                    node.YMap = j * radius + radius + (j * radius);
                    ViewNode.Coordinates.Add(new Tuple<double, double>(node.XMap, node.YMap));
                    AddCanvas(el, node.XMap, node.YMap);
                    matrixs[i, j] = node;



                }
            }

            dh.BeginInvoke(null, null);
        }


        private void ActionCheckBox(UIElement obj, UIElement obj1)
        {
            List<Line> resLine = new List<Line>();
            var el = obj as Ellipse;
            if (el != null)
            {
                resLine = GetAllLine(el);
            }

            var pp = obj1 as Popup;
            if (pp == null) return;
            var sp = pp.Child as StackPanel;
            if (sp == null) return;
            if (sp.Children.Count > 1)
            {
                var cb = sp.Children[1] as CheckBox;
                if (cb == null) return;
                if (cb.IsChecked ?? false)
                {
                    for (int i = 0; i < resLine.Count; ++i)
                    {
                        //resLine[i].Stroke = arg1 ? (Brush)bc.ConvertFrom("#FF0033") : Brushes.Black;
                        resLine[i].StrokeThickness = 1;

                    }
                }
                else
                {
                    for (int i = 0; i < resLine.Count; ++i)
                    {
                        //resLine[i].Stroke = arg1 ? (Brush)bc.ConvertFrom("#FF0033") : Brushes.Black;
                        resLine[i].StrokeThickness = 0;
                    }
                }
            }
        }

        private List<Line> GetAllLine(Ellipse el)
        {
            var x = (double)el.GetValue(Canvas.LeftProperty);
            var y = (double)el.GetValue(Canvas.TopProperty);
            List<Line> resLine;
            if (!dicLines.TryGetValue(new Tuple<double, double>(y, x), out resLine))
                resLine = new List<Line>();
            return resLine;
        }

        private BrushConverter bc = new BrushConverter();
        private async void ActionLine(bool arg1, Ellipse arg2)
        {
            Popup pp = null;
            bool isRefresh = true;
            for (int i = 0; i < matrixs.Row;  ++i)
                for (int j = 0; j < matrixs.Column; ++j)
                {
                    ViewNode node = matrixs[i, j];
                    if (node.Ellipse == arg2)
                    {
                       await MainWindow.main.Dispatcher.BeginInvoke((ThreadStart)delegate ()
                        {
                            pp = node.codePopup;
                            var sp = (StackPanel)pp.Child;
                            var cb = (CheckBox)sp.Children[1];
                            isRefresh = cb.IsChecked ?? false;
                        });
                    }
                }

            if (isRefresh)
               await MainWindow.main.Dispatcher.BeginInvoke((ThreadStart)delegate ()
                {
                    var x = (double)arg2.GetValue(Canvas.LeftProperty);
                    var y = (double)arg2.GetValue(Canvas.TopProperty);
                    List<Line> resLine;
                    if (dicLines.TryGetValue(new Tuple<double, double>(y, x), out resLine))
                    {
                        for (int i = 0; i < resLine.Count; ++i)
                        {
                            resLine[i].Stroke = arg1 ? (Brush)bc.ConvertFrom("#FF0033") : Brushes.Black;
                            resLine[i].StrokeThickness = arg1 ? 3 : 1;

                        }
                    }
                });
        }
        //List<Line> allLines = new List<Line>();
        Dictionary<Tuple<double, double>, List<Line>> dicLines =
            new Dictionary<Tuple<double, double>, List<Line>>();
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
                            var line = ViewNode.newLine(temp.Item2 + radius / 2, temp.Item1 + radius / 2, el.Item2 + radius / 2, el.Item1 + radius / 2, Brushes.Black, mainCanvas);
                            var resList = new List<Line>();
                            if (dicLines.TryGetValue(temp, out resList))
                            {
                                resList.Add(line);
                            }
                            else
                            {
                                resList = new List<Line>();
                                resList.Add(line);
                                dicLines.Add(temp, resList);
                            }
                        });
                    }

                }
            });

        }



        private async Task ClearAsync(Canvas canvas)
        {
            await Task.Run(() =>
            {
                MainWindow.main.Dispatcher.BeginInvoke((ThreadStart)delegate ()
                {
                    canvas.Children.Clear();
                });
            });
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
