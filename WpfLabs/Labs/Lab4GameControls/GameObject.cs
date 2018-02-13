using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using Lab4GameControls.Annotations;

namespace Lab4GameControls
{
    public class GameObject : INotifyPropertyChanged
    {
        private Rect _objectRect;
        private string _state;
        private bool _isEnabled;
        private BitmapSource _image;

        public GameObject()
        {
            IsEnabled = true;
        }

        public bool IsActive { get; set; }

        public Rect ObjectRect
        {
            get { return _objectRect; }
            set
            {
                if (value.Equals(_objectRect)) return;
                _objectRect = value;
                OnPropertyChanged("ObjectRect");
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

        public string State
        {
            get { return _state; }
            protected set
            {
                if (value == _state) return;
                _state = value;
                OnPropertyChanged("State");
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }

            set
            {
                if (value == _isEnabled) return;
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        public virtual void Init() { }
        public virtual void Update() { }
        public virtual void Destroy()
        {
            Image = BitmapFrame.Create(new Uri("pack://application:,,,/Lab4GameControls;component/Assets/bum.png", UriKind.RelativeOrAbsolute));
        }


        #region реализация INotifyPrepertyChanged 

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
