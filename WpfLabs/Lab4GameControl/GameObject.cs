using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Lab4GameControl.Annotations;

namespace Lab4GameControl
{
    public abstract class GameObject
    {
        private double _x;
        private double _y;

        public double X
        {
            get { return _x; }
            set
            {
                if (value.Equals(_x)) return;
                _x = value;
            }
        }

        public double Y
        {
            get { return _y; }
            set
            {
                if (value.Equals(_y)) return;
                _y = value;
            }
        }


       

        protected GameObject()
        {
            var visual = new Image();
            //visual.SetBinding(System.Windows.Controls.Image.SourceProperty, new Binding("Image"));
            //AddVisualChild(visual);
        }

        public abstract void Update();

        public event ObjectMovedEventHandler ObjectMoved;

        protected virtual void OnObjectMoved(ObjectMovedEventHandlerArgs args)
        {
            var handler = ObjectMoved;
            if (handler != null) handler(this, args);
        }
    }

    public delegate void ObjectMovedEventHandler(object sender, ObjectMovedEventHandlerArgs args);

    public class ObjectMovedEventHandlerArgs
    {
    }

    public class Bomber : GameObject
    {
        public Bomber()
        {
            //Uri bitmapUri = new Uri("/Assets/bomb.png", UriKind.Relative);
            //Image = new BitmapImage(bitmapUri);
        }

        public override void Update()
        {

        }
    }
}
