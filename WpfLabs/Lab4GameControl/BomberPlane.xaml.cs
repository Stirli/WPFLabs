using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lab4GameControl
{
    /// <summary>
    /// Логика взаимодействия для BomberPlane.xaml
    /// </summary>
    public partial class BomberPlane : UserControl
    {
        public BomberPlane()
        {
            InitializeComponent();
            //_context = new Context();
            //_context.Bomber.Image = GetBitmapSource("/Assets/bomber.png");
            //_context.Bomb.Image = GetBitmapSource("/Assets/bomb.png");
            //foreach (GameObject gameObject in _context.GameObjects)
            //{
            //    Image image = new Image();
            //    image.SetBinding(Canvas.TopProperty, new Binding(gameObject.Name + ".Y"));
            //    image.SetBinding(Canvas.LeftProperty, new Binding(gameObject.Name + ".X"));
            //    image.SetBinding(Image.SourceProperty, new Binding(gameObject.Name + ".Image"));
            //    image.SetBinding(Image.VisibilityProperty, new Binding(gameObject.Name + ".IsVisible") { Converter = new BooleanToVisibilityConverter() });
            //    image.Height = 60;
            //    gameArea.Children.Add(image);
            //}
            //TargetDestroyed += (sender, args) => _context.Bomb.OnDestroyed();
            //DataContext = _context;
        }

        private static BitmapSource GetBitmapSource(string resName)
        {
            Uri bitmapUri = new Uri("pack://application:,,,/Lab4GameControl;component" + resName, UriKind.Absolute);
            BitmapSource bitmapSource = new BitmapImage(bitmapUri);
            return bitmapSource;
        }

        public void StartGame()
        {
            
        }

        public void StopGame()
        {
            
        }

        public void Fire()
        {
            
        }

        private void Image_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            HitTestResult res = VisualTreeHelper.HitTest(gameArea, e.GetPosition(gameArea));
            if (res != null)
            {
                var el = res.VisualHit as Image;
                    MessageBox.Show(el.Name);
            }
        }
    }
}
