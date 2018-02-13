using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

using Microsoft.Win32;

namespace Lab5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainContext context;

        public MainWindow()
        {
            this.InitializeComponent();
            this.context = this.Resources["MainContext"] as MainContext;
        }

        private void SetBrushCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            BrushDialog bd = new BrushDialog { ResultData = this.context.ShapeData.Clone() };
            if (bd.ShowDialog() == true) this.context.ShapeData = bd.ResultData.Clone();
        }

        private void SaveAsCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            FileDialog fd = this.GetFileDialog<SaveFileDialog>();
            if (fd.ShowDialog() == true)
            {
                this.Save(fd.FileName);
                MessageBox.Show("Успех");
            }
        }

        private void SaveCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.context.CanSave;
        }

        private void OpenCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            FileDialog fd = this.GetFileDialog<OpenFileDialog>();
            if (fd.ShowDialog() == true)
            {
                this.Load(fd.FileName);
            }
        }

        private void AboutCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            new About().ShowDialog();
        }

        public void Load(string path)
        {
            this.context.Shapes.Clear();
            this.DrawCanvas.Children.Clear();
            IEnumerable<string> lines = File.ReadLines(path);
            foreach (string line in lines)
            {
                ShapeData shapeData = (ShapeData)line;
                this.context.Shapes.Add(shapeData);
                this.DrawCanvas.Children.Add(this.MakePolygon(shapeData));
            }

            this.context.FileInfo = new FileInfo(path);
        }

        public void Save(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (ShapeData shape in this.context.Shapes)
                {
                    sw.WriteLine(shape.ToString());
                }
            }

            this.context.FileInfo = new FileInfo(path);
        }

        /**********************************************************************/
        #region Canvas events

        /**********************************************************************/
        private void DrawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(sender as IInputElement);
            this.context.ShapeData.Position = position;
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(this.DrawCanvas);

            ShapeData shape = this.context.ShapeData.Clone();

            shape.Position = position;

            Polygon polygon = this.MakePolygon(shape);

            this.DrawCanvas.Children.Add(polygon);
            this.context.Shapes.Add(shape);
        }

        private Polygon MakePolygon(ShapeData shape)
        {
            Polygon polygon = new Polygon
                                  {
                                      Fill = shape.Background,
                                      Stroke = shape.Border,
                                      StrokeThickness = shape.BorderWidth,
                                      Points = new PointCollection
                                                   {
                                                       new Point(0, 10),
                                                       new Point(0, 30),
                                                       new Point(100, 30),
                                                       new Point(100, 40),
                                                       new Point(120, 20),
                                                       new Point(100, 0),
                                                       new Point(100, 10)
                                                   }
                                  };

            polygon.SetValue(Canvas.LeftProperty, shape.Position.X);

            polygon.SetValue(Canvas.TopProperty, shape.Position.Y);

            return polygon;
        }

        #endregion

        private FileDialog GetFileDialog<T>()
            where T : FileDialog, new()
        {
            return new T
                       {
                           InitialDirectory = this.context.FileInfo.DirectoryName,
                           FileName = this.context.FileInfo.Name,
                           Filter = "Файлы Vector Image Format (vif)|*.vif|Все файлы|*.*"
                       };
        }
    }
}