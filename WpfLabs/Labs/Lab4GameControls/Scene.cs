using System;
using System.ComponentModel;
using System.Threading;

namespace Lab4GameControls
{
    internal class Scene : INotifyPropertyChanged, IDisposable
    {
        private Timer _t;
        private Bomb _bomb;

        public Panzer Panzer { get; set; }

        public Bomber Bomber { get; set; }

        public Bomb Bomb
        {
            get { return this._bomb; }
            set
            {
                if (Equals(value, this._bomb)) return;
                this._bomb = value;
                this.OnPropertyChanged("Bomb");
            }
        }

        public bool IsBuisy { get; set; }
        public Action OnWin { get; set; }
        public Action OnGameOver { get; set; }

        public void Init()
        {
            this.Panzer.Init();
            this.Bomber.Init();
        }

        public void Start()
        {
            if (this.IsBuisy) return;
            Counter c = new Counter();
            this._t = new Timer(
                state =>
                {
                    this.Bomber.Update();

                    if (!this.Bomb.IsEnabled) return;
                    this.Bomb.Update();
                    if (this.Bomb.ObjectRect.IntersectsWith(this.Panzer.ObjectRect))
                    {
                        if (this.Bomb.IsActive)
                        {
                            this.Bomb.IsActive = false;
                            this.Bomb.Destroy();
                            this.Panzer.Destroy();
                        }

                        if (c.Value >= 50)
                        {
                            this.Bomb.IsEnabled = false;
                            this.Panzer.IsEnabled = false;
                            this.OnWin();
                            this.Pause();
                            this.Init();
                        }
                    }
                    else
                        if (this.Bomb.ObjectRect.Y < 0)
                        {
                            if (this.Bomb.IsActive)
                            {
                                this.Bomb.IsActive = false;
                                this.Bomb.Destroy();
                            }

                            if (c.Value >= 50)
                            {
                                this.Bomb.IsEnabled = false;
                                this.OnGameOver();
                                c.Value = 0;
                            }
                        }
                },
                null,
                0,
                10);

            this.IsBuisy = true;
        }

        public void Pause()
        {
            if (this.IsBuisy)
            {
                this._t.Dispose();
                this.IsBuisy = false;
            }
        }

        public void Dispose()
        {
            if (this._t != null)
            {
                this._t.Dispose();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
