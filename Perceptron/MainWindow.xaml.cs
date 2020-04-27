using Audio;
using Perceptron.Model;
using Perceptron.ModelView;
using Perceptron.Utility;
using PerceptronLib.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Perceptron
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainAudio ma = null;//new MainAudio();
        public static MainWindow main = null;
        public MainWindow()
        {
            main = this;
            InitializeComponent();
            DataContext = new Main();
            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;

        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            if (ma != null)
                ma.StopRec();
        }

        int[,] input = new int[,] { { 1, 0 }, { 1, 1 }, { 0, 1 }, { 0, 0 } };
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (ma != null)
            {
                ma.outputFilename = "test.wav";
                ma.StartRec();
            }

        }

        private async Task Init()
        {
            PerceptronLib.Perceptron perceptron = null;
            await Task.Run(() =>
            {
                perceptron = new PerceptronLib.Perceptron(10, 10);
            });
        }

        public Main GetDataContext
        {
            get { return (Main)DataContext; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainCanvas.Children.Clear();
            mainCanvas_MouseLeftButtonDown(null, null);

            double height = mainCanvas.ActualHeight;
            double wigth = mainCanvas.ActualWidth;

            PerceptronLib.Perceptron perceptron = null;
            int row = GetDataContext.Row;
            int column = GetDataContext.Column;
            Init init = new Init(row, column, height, wigth, mainCanvas);
            return;
            MapLine ml = new MapLine();

            var _x = 2 * GetDataContext.Column + 1;
            var _y = 2 * GetDataContext.Row + 1;
            double x = Math.Round((wigth - 300) / _x);
            double y = Math.Round((height - 300) / (_y));
            ml.radius = x <= y ? x / 2 : y / 2;
            ml.map = new Tuple<double, double>[GetDataContext.Row, GetDataContext.Column];
            int numRow = -1;
            int numColumn = 0;
            List<Tuple<double, double>> maps = new List<Tuple<double, double>>();
            bool isbool = true;
            for (int i = 0; i < _y; ++i)
            {
                if (i % 2 > 0)
                {
                    numRow++;
                    numColumn = 0;
                }
                for (int j = 0; j < _x; ++j)
                {
                    if (i % 2 > 0 && j % 2 > 0)
                    {
                        try
                        {
                            GenerateCircle circle = new GenerateCircle(x <= y ? x : y);
                            //var el = circle.GetEllipse(numRow, numColumn++);
                            var el = circle.GetEllipse($"X = {x * j} Y = {y * i}");

                            circle.Action += Checed;
                            el.SetValue(Canvas.LeftProperty, x * j);
                            el.SetValue(Canvas.TopProperty, y * i);
                            mainCanvas.Children.Add(el);
                            int ex = numRow;
                            int ey = numColumn;
                            ml.map[ex, ey] = new Tuple<double, double>(x * j + ml.radius, y * i + ml.radius);
                            maps.Add(new Tuple<double, double>(x * j + ml.radius, y * i + ml.radius));
                            numColumn++;

                        }
                        catch (Exception ex)
                        {
                            var s = numRow;
                            var h = numColumn;
                        }

                    }
                }

            }

            var temp = maps.GroupBy((z) => z.Item1).ToList();

            List<Tuple<double, double>> res = null;
            foreach (var group in temp)
            {
                if (res != null)
                {
                    var temps = group.ToList();
                    foreach (var root in temps)
                        foreach (var dop in res)
                        {
                            GenerateLine gl = new GenerateLine();
                            gl.newLine(root.Item1, root.Item2, dop.Item1, dop.Item2, Brushes.Black, mainCanvas);
                        }

                }
                res = group.ToList();
            }
        }

        private void Checed(UIElement el)
        {
            var ellipse = (Ellipse)el;

        }
        public void mainCanvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ViewNode.sPopup != null)
            {
                ViewNode.sPopup.IsOpen = false;
            }
        }
    }

    public static class PopupCloser
    {
        public static void CloseAllPopups()
        {
            foreach (Window window in Application.Current.Windows)
            {
                CloseAllPopups(window);
            }
        }

        public static void CloseAllPopups(Window window)
        {
            IntPtr handle = new WindowInteropHelper(window).Handle;
            EnumChildWindows(handle, ClosePopup, IntPtr.Zero);
        }

        private static bool ClosePopup(IntPtr hwnd, IntPtr lParam)
        {
            HwndSource source = HwndSource.FromHwnd(hwnd);
            if (source != null)
            {
                Popup popup = source.RootVisual as Popup;
                if (popup != null)
                {
                    popup.IsOpen = false;
                }
            }
            return true; // to continue enumeration
        }

        private delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);
    }
}
