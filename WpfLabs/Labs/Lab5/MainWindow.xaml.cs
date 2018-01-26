using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Win32;

namespace Lab5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ShapeData _shape;
        private List<ShapeData> _shapes;

        public MainWindow()
        {
            InitializeComponent();
            _shape = new ShapeData();
            _shapes = new List<ShapeData>();
            SetBinding(TitleProperty, new Binding("FileInfo.Name") { Source = this, StringFormat = "VPaint - {0}" });
            ToolBar.DataContext = _shape;
        }

        public FileInfo FileInfo
        {
            get { return (FileInfo)GetValue(FileInfoProperty); }
            set { SetValue(FileInfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FileInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileInfoProperty =
            DependencyProperty.Register("FileInfo", typeof(FileInfo), typeof(MainWindow), new UIPropertyMetadata(new FileInfo("Безымянный.vif")));



        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Point position = e.GetPosition(DrawCanvas);
                ShapeData shape = _shape.Clone();
                shape.X = position.X;
                shape.Y = position.Y;
                _shapes.Add(shape);
                Polygon polygon = MakePolygon(shape);
                DrawCanvas.Children.Add(polygon);
            }
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
            polygon.SetValue(Canvas.LeftProperty, shape.X);
            polygon.SetValue(Canvas.TopProperty, shape.Y);
            return polygon;
        }

        private void CommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            BrushDialog bd = new BrushDialog { ResultData = _shape.Clone() };
            if (bd.ShowDialog() == true)
                ToolBar.DataContext = _shape = bd.ResultData.Clone();
        }

        private void SaveAsCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            FileDialog fd = GetFileDialog<SaveFileDialog>();
            if (fd.ShowDialog() == true)
            {
                File.WriteAllLines((FileInfo = new FileInfo(fd.FileName)).FullName, _shapes.ConvertAll(input => input.ToString()));
                MessageBox.Show("Успех");
            }
        }

        private void SaveCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _shapes.Count > 0;
        }

        private void OpenCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            FileDialog fd = GetFileDialog<OpenFileDialog>();
            if (fd.ShowDialog() == true)
            {
                _shapes.Clear();
                DrawCanvas.Children.Clear();
                IEnumerable<string> lines = File.ReadLines((FileInfo = new FileInfo(fd.FileName)).FullName);
                foreach (string line in lines)
                {
                    ShapeData shapeData = (ShapeData)line;
                    _shapes.Add(shapeData);
                    Polygon polygon = MakePolygon(shapeData);
                    DrawCanvas.Children.Add(polygon);
                }
            }
        }

        private void SaveCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            File.WriteAllLines(FileInfo.FullName, _shapes.ConvertAll(input => input.ToString()));
        }

        private FileDialog GetFileDialog<T>() where T : FileDialog, new()
        {
            return new T { InitialDirectory = FileInfo.DirectoryName, FileName = FileInfo.Name, Filter = "Файлы Vector Image Format (vif)|*.vif|Все файлы|*.*" };
        }

        private void AboutCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            new About().ShowDialog();
        }
    }
}