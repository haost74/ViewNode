using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml;

namespace Perceptron.Utility
{
    public class ConvertToCode
    {
        public void Convert(string codeXaml)
        {
            StringReader stringReader = new StringReader(codeXaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Popup popup = (Popup)XamlReader.Load(xmlReader);
        }

        public void Test()
        {
            Button originalButton = new Button();
            originalButton.Height = 50;
            originalButton.Width = 100;
            originalButton.Background = Brushes.AliceBlue;
            originalButton.Content = "Click Me";

            // Save the Button to a string.
            string savedButton = XamlWriter.Save(originalButton);

            // Load the button
            StringReader stringReader = new StringReader(savedButton);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Button readerLoadButton = (Button)XamlReader.Load(xmlReader);
        }
    }
}
