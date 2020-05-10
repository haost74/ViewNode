using Audio;
using Perceptron.Model;
using Perceptron.ModelView;
using PerceptronLib.Nodes;
using Psql;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Interop;
using System.Windows.Shapes;

namespace Perceptron
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainAudio ma = null;//new MainAudio();
        Init init = null;
        public static MainWindow main = null;
        RequirePsql req = new RequirePsql();
        public MainWindow()
        {
            main = this;
            req.Error += Error;
            InitializeComponent();
            DataContext = new Main();
            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;

        }

        private void Error(string obj)
        {

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

            Button_Click(null, null);

            /*
             select * from binance where datetime = '03-04-2020 22:44:143'and bidask = true order by param desc limit 5 
             select * from binance where datetime = '03-04-2020 22:44:143'and bidask = false order by param  limit 5;


            select * from binance where datetime = '03-04-2020 22:44:143' and bidask = true order by param  desc limit 5;
            select * from binance where datetime = '03-04-2020 22:44:143' and bidask = false order by param limit 5;

            select datetime, count(id) from binance group by datetime;
            select datetime from binance group by datetime;





             */

            string sql = "select * from binance where datetime = '03-04-2020 22:44:143' and bidask = true order by param  desc limit 5";
            string sql1 = "select * from binance where datetime = '03-04-2020 22:44:143' and bidask = false order by param limit 5;";
            List<Binance> resList = new List<Binance>();

            //var res = req.GetArray(new Binance(), "select min(param) from binance where datetime = '03-04-2020 22:44:143'and bidask = false order by param DESC;");
            var res = req.GetArray(new Binance(), sql);
            //var t = Convert.ToDateTime(res[0].datetime);
            res.ContinueWith(sqlRes =>
            {
                req.GetArray(new Binance(), sql1)
                .ContinueWith(res1 => 
                {
                    resList = new List<Binance>(sqlRes.Result);
                    resList.AddRange(res1.Result);
                });
            });
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
            init = new Init(row, column, height, wigth, mainCanvas);
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

        private void Button_Click_Run(object sender, RoutedEventArgs e)
        {
            if (init == null) return;

            FrameRun run = new FrameRun(init, null);
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
