using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using Lab4GameControls.Annotations;

namespace Lab4GameControls
{
    class Scene : DependencyObject, IDisposable
    {
        private Timer t;
        public GameObject Panzer { get; set; }
        public GameObject Bomber { get; set; }

        public bool IsBuisy { get; set; }


        public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Width.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(Scene), new UIPropertyMetadata(0.0));
        
        public double Height
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Height.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(double), typeof(Scene), new UIPropertyMetadata(0.0));

        public void Start()
        {
            if (!IsBuisy)
            {
                t = new Timer(
                     state =>
                     {
                         Panzer.Update();
                         Bomber.Update();
                     }, null, 0, 30);
                IsBuisy = true;
            }
        }

        public void Stop()
        {
            if (IsBuisy)
            {
                t.Dispose();
                IsBuisy = false;
            }
        }

        public void Dispose()
        {
            if (t != null) t.Dispose();
        }
    }
}
