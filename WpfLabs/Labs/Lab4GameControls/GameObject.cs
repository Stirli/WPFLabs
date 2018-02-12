using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using Lab4GameControls.Annotations;

namespace Lab4GameControls
{
    class GameObject : INotifyPropertyChanged
    {
        private Rect objectRect;
        private string _state;

        public GameObject()
        {
            this.IsEnabled = true;
        }

        private bool isEnabled;

        public Rect ObjectRect
        {
            get { return this.objectRect; }
            set
            {
                if (value.Equals(this.objectRect)) return;
                this.objectRect = value;
                this.OnPropertyChanged("ObjectRect");
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
                return this.isEnabled;
            }

            set
            {
                if (value == this.isEnabled) return;
                this.isEnabled = value;
                this.OnPropertyChanged("IsEnabled");
            }
        }

        public virtual void Update() { }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class Panzer : GameObject
    {
        private string _state;

        Counter c = new Counter { Step = .02, End = Math.PI * 2 };

        public override void Update()
        {
            if (this.IsEnabled)
            {
                Rect rect = this.ObjectRect;
                rect.X += Math.Sin(this.c.Value) * 4;
                this.ObjectRect = rect;
            }
        }
    }

    class Bomber : GameObject
    {
        private Counter c;

        public Bomber()
        {
            this.c = new Counter { Start = -200, Step = 2, End = 1280 };
        }

        public override void Update()
        {
            Rect rect = this.ObjectRect;
            rect.X = Math.Round(this.c.Value);
            this.ObjectRect = rect;
        }
    }

    class Bomb : GameObject
    {

        public override void Update()
        {
            if (this.IsEnabled)
            {
                Rect rect = this.ObjectRect;
                rect.Y -= 2;
                this.ObjectRect = rect;
            }
        }
    }
}
