using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab5
{
    /// <summary>
    /// Логика взаимодействия для BrushDialog.xaml
    /// </summary>
    public partial class BrushDialog : Window
    {

        public BrushDialog()
        {
            InitializeComponent();
        }



        public ShapeData Result
        {
            get { return (ShapeData)GetValue(ResultProperty); }
            set { SetValue(ResultProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Result.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResultProperty =
            DependencyProperty.Register("Result", typeof(ShapeData), typeof(BrushDialog), new UIPropertyMetadata(null));

    }

    
}
