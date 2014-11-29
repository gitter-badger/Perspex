using Perspex;
using Perspex.Controls;
using Perspex.Controls.Primitives;
using Perspex.Diagnostics;
using Perspex.Layout;
using Perspex.Media;
using Perspex.Media.Imaging;
using Perspex.Windows;
using Splat;

namespace TestApplication
{
    class TestLogger : ILogger
    {
        public LogLevel Level
        {
            get;
            set;
        }

        public void Write(string message, LogLevel logLevel)
        {
            if ((int)logLevel < (int)Level) return;
            System.Diagnostics.Debug.WriteLine(message);
        }
    }

    class Item
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    class Node
    {
        public Node()
        {
            this.Children = new PerspexList<Node>();
        }

        public string Name { get; set; }
        public PerspexList<Node> Children { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //LogManager.Enable(new TestLogger());
            //LogManager.Instance.LogLayoutMessages = true;

            App application = new App();

            Window window = new Window
            {
                Content = new Grid
                {
                    Width = 300,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    ColumnDefinitions = new ColumnDefinitions
                    {
                        new ColumnDefinition(GridLength.Auto),
                        new ColumnDefinition(1, GridUnitType.Star),
                        new ColumnDefinition(GridLength.Auto),
                    },
                    Children = new Controls
                    {
                        new Button
                        {
                            Content = "Auto1",
                            [Grid.ColumnProperty] = 0,
                        },
                        new Button
                        {
                            Content = "Star",
                            [Grid.ColumnProperty] = 1,
                        },
                        new Button
                        {
                            Content = "Auto2",
                            [Grid.ColumnProperty] = 2,
                        },
                    }
                },
            };

            window.Show();
            Application.Current.Run(window);
        }
    }
}
