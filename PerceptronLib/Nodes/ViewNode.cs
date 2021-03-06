﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace PerceptronLib.Nodes
{
    public class ViewNode : Node
    {
        public static List<Tuple<double, double>> Coordinates =
            new List<Tuple<double, double>>();
        public double XMap = 0;
        public double YMap = 0;
        private List<Line> lines = new List<Line>();
        public List<Line> List
        {
            get { return lines; }
            set
            {
                lines = value;
            }
        }

        public static Popup sPopup = null;
        private double radius = double.NaN;
        public Brush Stroke = Brushes.Black;
        public double StrokeThickness = 0.5;
        public Brush Fill = Brushes.Transparent;
        private BrushConverter bc = new BrushConverter();
        public Popup codePopup = null;
        public Action<UIElement, UIElement> Action;
        public Ellipse Ellipse = null;
        public Ellipse GetEllipse(string str, double radius)
        {
            var res = new Ellipse
            {
                Width = radius,
                Height = radius,
                Stroke = this.Stroke,
                StrokeThickness = this.StrokeThickness,
                Fill = this.Fill
            };


            //res.GotFocus += Res_GotFocus;
            //res.LostFocus += Res_GotFocus;
            res.MouseEnter += Res_GotFocus;
            res.MouseLeave += Res_GotFocus;
            res.Cursor = Cursors.Hand;
            res.MouseLeftButtonDown += Res_MouseLeftButtonDown;
            codePopup = new Popup();
            codePopup.Height = 76;
            codePopup.Width = 180;

            codePopup.Effect =
                        new DropShadowEffect
                        {
                            Color = new Color { A = 255, R = 255, G = 255, B = 0 },
                            Direction = 320,
                            ShadowDepth = 0,
                            Opacity = 1
                        };
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            TextBlock popupText = new TextBlock();
            popupText.Text = str;
            //popupText.Background = Brushes.LightBlue;
            //popupText.Foreground = Brushes.Blue;
            sp.Children.Add(popupText);

            CheckBox cb = new CheckBox();
            cb.Content = "Влючить узел";
            cb.Height = 35;
            cb.IsChecked = true;
            cb.Click += Cb_Checked;

            sp.Children.Add(cb);
            sp.Background = (Brush)bc.ConvertFrom("#E5E7E9");
            codePopup.Child = sp;

            codePopup.Placement = PlacementMode.MousePoint;
            codePopup.PlacementTarget = res;
            Ellipse = res;
            return res;
        }

        bool isFocuse = false;
        private void Res_GotFocus(object sender, RoutedEventArgs e)
        {
            var ellipse = (Ellipse)sender;
            isFocuse = !isFocuse;
            if(isFocuse)
            {
                ellipse.Fill = (Brush)bc.ConvertFrom("#ff0000");
            }
            else
            {
                ellipse.Fill = (Brush)bc.ConvertFrom("#00ffbf");
            }

            ActionLine?.BeginInvoke(isFocuse, ellipse, callBack, ellipse);
        }

        private void callBack(IAsyncResult ar)
        {
            
        }

        public Action<bool, Ellipse> ActionLine;

        private void Cb_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox el = (CheckBox)sender;
            StackPanel sp = (StackPanel)el.Parent;
            Action?.Invoke(((Popup)sp.Parent).PlacementTarget, (Popup)sp.Parent);

        }

        private void Res_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sPopup != null)
            {
                sPopup.IsOpen = false;
            }
            sPopup = codePopup;
            codePopup.IsOpen = true;

        }

        public static Line newLine(double x1, double y1, double x2, double y2, Brush brush, Canvas can)
        {
            
            Line line = new Line();

            line.X1 = x1;
            line.X2 = x2;
            line.Y1 = y1;
            line.Y2 = y2;

            //line.X1 = x1;
            //line.X2 = y2;
            //line.Y1 = y1;
            //line.Y2 = x2;

            line.StrokeThickness = 1;
            line.Stroke = brush;

            // https://stackoverflow.com/questions/2879033/how-do-you-draw-a-line-on-a-canvas-in-wpf-that-is-1-pixel-thick
            line.SnapsToDevicePixels = true;
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            Canvas.SetZIndex(line, -1);
            can.Children.Add(line);

            return line;
        }


        public override void Signal(double value, double weight)
        {
            //base.Signal(value, weight);
        }

    }
}
