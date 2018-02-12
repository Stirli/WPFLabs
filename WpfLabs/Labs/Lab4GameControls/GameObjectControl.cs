using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Lab4GameControls
{
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    class GameObjectControl : UserControl
    {
        public GameObjectControl()
        {
            Image image = new Image(){Stretch = Stretch.Fill};
            image.SetBinding(Image.SourceProperty, new Binding("Sprite") { Source = this });
            this.Content = image;
            this.VerticalContentAlignment = VerticalAlignment.Stretch;
            this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            SetBinding(Canvas.LeftProperty, new Binding("Left") { Source = this });
            SetBinding(Canvas.TopProperty, new Binding("Top") { Source = this });
            SizeChanged += (sender, args) =>
                Margin = new Thickness(-args.NewSize.Width / 2, -args.NewSize.Height / 2, 0, 0);
        }



        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Left.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof(double), typeof(GameObjectControl), new UIPropertyMetadata(0.0));



        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Top.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register("Top", typeof(double), typeof(GameObjectControl), new UIPropertyMetadata(0.0));



        public BitmapSource Sprite
        {
            get { return (BitmapSource)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("Sprite", typeof(BitmapSource), typeof(GameObjectControl), new UIPropertyMetadata(null));

        public void Destroy()
        {
            Visibility = Visibility.Collapsed;
        }
    }
}