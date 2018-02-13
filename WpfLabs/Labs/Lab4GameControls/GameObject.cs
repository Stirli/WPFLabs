using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

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
            this.IsEnabled = true;
        }

        public bool IsActive { get; set; }

        public Rect ObjectRect
        {
            get { return this._objectRect; }
            set
            {
                if (value.Equals(this._objectRect)) return;
                this._objectRect = value;
                this.OnPropertyChanged("ObjectRect");
            }
        }

        public BitmapSource Image
        {
            get { return this._image; }
            set
            {
                if (Equals(value, this._image)) return;
                this._image = value;
                this.OnPropertyChanged("Image");
            }
        }

        public string State
        {
            get { return this._state; }
            protected set
            {
                if (value == this._state) return;
                this._state = value;
                this.OnPropertyChanged("State");
            }
        }

        public bool IsEnabled
        {
            get
            {
                return this._isEnabled;
            }

            set
            {
                if (value == this._isEnabled) return;
                this._isEnabled = value;
                this.OnPropertyChanged("IsEnabled");
            }
        }

        public virtual void Init() { }
        public virtual void Update() { }
        public virtual void Destroy()
        {
            this.Image = BitmapFrame.Create(new Uri("pack://application:,,,/Lab4GameControls;component/Assets/bum.png", UriKind.RelativeOrAbsolute));
        }


        #region реализация INotifyPrepertyChanged 

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
