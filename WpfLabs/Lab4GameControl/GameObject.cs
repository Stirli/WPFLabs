using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using Lab4GameControl.Annotations;

namespace Lab4GameControl
{
    public abstract class GameObject : INotifyPropertyChanged
    {
        private bool _isVisible;
        private BitmapSource _image;
        private int _x;
        private int _y;

        public int X
        {
            get { return _x; }
            set
            {
                if (value == _x) return;
                _x = value;
                OnPropertyChanged("X");
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                if (value == _y) return;
                _y = value;
                OnPropertyChanged("Y");
            }
        }

        public BitmapSource Image
        {
            get { return _image; }
            set
            {
                if (Equals(value, _image)) return;
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (value == _isVisible) return;
                _isVisible = value;
                if (!IsVisible)
                    OnHide();
                OnPropertyChanged("IsVisible");
            }
        }

        public abstract void Update();

        public virtual void OnHide()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Airplane : GameObject
    {
        public GameObject Bomb { get; set; }

        public Airplane()
        {
        }

        public override void Update()
        {
            X += 1;
            if (X > 1300)
            {
                X = -450;
            }
        }

        public void Fire()
        {
            Bomb.X = X + 150;
            Bomb.Y = Y + 50;
            Bomb.IsVisible = true;
        }
    }

    public class Bomb : GameObject
    {
        private double y = 0.5;
        public override void Update()
        {
            if (IsVisible)
            {
                X += 1;
                Y += (int)(y+=0.05);
            }
            if (Y > 600)
            {
                y = 0;
                IsVisible = false;
            }
        }

        public override void OnHide()
        {

        }
    }
}
