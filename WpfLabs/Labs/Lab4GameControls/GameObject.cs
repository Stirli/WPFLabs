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
        private Point _position;
        private string _state;

        public Point Position
        {
            get { return _position; }
            set
            {
                if (value.Equals(_position)) return;
                _position = value;
                OnPropertyChanged("Position");
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


        public bool IsEnabled { get; set; }
        public virtual void Update() { }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class Panzer : GameObject
    {
        private string _state;

        Counter c = new Counter { Step = .03, End = Math.PI * 2 };

        public override void Update()
        {
            Point point = Position;
            point.X += Math.Sin(c.Value) * 2;
            Position = point;
        }
    }

    class Bomber : GameObject
    {
        private Counter c;

        public Bomber()
        {
            c = new Counter {Start = -200,Step = 2, End = 1280 };
        }

        public override void Update()
        {
            Point point = Position;
            point.X = c.Value;
            Position = point;
        }
    }
}
