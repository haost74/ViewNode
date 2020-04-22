using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace Perceptron.Utility
{
    public class GenerateCircle
    {
        public static Popup sPopup = null;
        private double radius = double.NaN;
        public Brush Stroke = Brushes.Black;
        public double StrokeThickness = 0.5;
        BrushConverter bc = new BrushConverter();
        Brush Fill = Brushes.Transparent;

        public Action<UIElement> Action;

        Popup codePopup = null;

        public GenerateCircle(double radius)
        {
            this.radius = double.IsNaN(radius) ? 20 : radius;
            Fill = (Brush)bc.ConvertFrom("#1ABC9C");

        }

        public Ellipse GetEllipse(string str)
        {
            var res = new Ellipse
            {
                Width = radius,
                Height = radius,
                Stroke = this.Stroke,
                StrokeThickness = this.StrokeThickness,
                Fill = this.Fill                
           };

            
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
            
            return res;
        }


        private void Cb_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox el = (CheckBox)sender;
            StackPanel sp = (StackPanel)el.Parent;            
            Action?.Invoke(((Popup)sp.Parent).PlacementTarget);

        }

        private void TryClosePopupParent(object o)
        {
            while (o != null)
            {
                Popup p = (o as Popup);
                if (p == null)
                {
                    o = (o as FrameworkElement).Parent;
                }
                else
                {
                    p.IsOpen = false;
                    break;
                }
            }
        }

        private void Res_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var circle = (Ellipse)sender;
            if(sPopup !=null)
            {
                sPopup.IsOpen = false;
            }
            sPopup = codePopup;
            codePopup.IsOpen = true;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="el">Elipses</param>
        /// <param name="sourceObject">Main</param>
        /// <param name="PropertyPath">name property</param>
        public void Binding(UIElement el, object sourceObject, string PropertyPath, DependencyProperty property)
        {
            Binding binding = new Binding();
            binding.Path = new PropertyPath(PropertyPath);
            binding.Source = sourceObject;  // view model?

            BindingOperations.SetBinding(el, TextBlock.TextProperty, binding);            
        }
    }
}
