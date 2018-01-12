using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab4GameControl
{
    /// <summary>
    /// Логика взаимодействия для BomberPlane.xaml
    /// </summary>
    public partial class BomberPlane : UserControl
    {
        private Context _context;
        public BomberPlane()
        {
            InitializeComponent();
            _context = new Context();
            _context.Bomber.Image = GetBitmapSource("/Assets/bomber.png");
            _context.Bomb.Image = GetBitmapSource("/Assets/bomb.png");
            DataContext = _context;
        }

        private static BitmapSource GetBitmapSource(string resName)
        {
            Uri bitmapUri = new Uri("pack://application:,,,/Lab4GameControl;component" +resName, UriKind.Absolute);
            BitmapSource bitmapSource = new BitmapImage(bitmapUri);
            return bitmapSource;
        }

        public void Update()
        {
            _context.Update();
        }

        public void OnFire()
        {
            _context.Bomber.Fire();
        }
    }
}
