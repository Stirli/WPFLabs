using System;
using System.Threading;
using System.Windows;

namespace Lab4GameControls
{
    using System.ComponentModel;

    using Lab4GameControls.Annotations;

    internal class Scene : INotifyPropertyChanged, IDisposable
    {
        private Timer t;

        private Panzer panzer;

        private Bomb bomb;

        private Bomber bomber;

        public Panzer Panzer
        {
            get
            {
                return this.panzer;
            }

            set
            {
                if (Equals(value, this.panzer)) return;
                this.panzer = value;
                this.OnPropertyChanged("Panzer");
            }
        }

        public Bomber Bomber
        {
            get
            {
                return this.bomber;
            }

            set
            {
                if (Equals(value, this.bomber)) return;
                this.bomber = value;
                this.OnPropertyChanged("Bomber");
            }
        }

        public Bomb Bomb
        {
            get
            {
                return this.bomb;
            }

            set
            {
                if (Equals(value, this.bomb)) return;
                this.bomb = value;
                this.OnPropertyChanged("Bomb");
            }
        }

        public bool IsBuisy { get; set; }

        public void Init()
        {
            Rect objectRect = this.Panzer.ObjectRect;
            objectRect.Location = new Point(600, 50);
            this.Panzer.ObjectRect = objectRect;

            Rect rect = this.Bomber.ObjectRect;
            rect.Location = new Point(0, 600);
            this.Bomber.ObjectRect = rect;

            this.Bomb = new Bomb();
        }

        public void Start()
        {
            if (!this.IsBuisy)
            {
                this.t = new Timer(
                    state =>
                    {
                        this.Panzer.Update();
                        this.Bomber.Update();
                        this.Bomb.Update();
                        if (this.Bomb.ObjectRect.IntersectsWith(this.Panzer.ObjectRect))
                        {
                            this.Bomb.IsEnabled = false;
                            this.Panzer.IsEnabled = false;
                            this.Stop();
                        }
                    },
                    null,
                    0,
                    10);

                this.IsBuisy = true;
            }
        }

        public void Stop()
        {
            if (this.IsBuisy)
            {
                this.t.Dispose();
                this.IsBuisy = false;
            }
        }

        public void Dispose()
        {
            if (this.t != null)
            {
                this.t.Dispose();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
