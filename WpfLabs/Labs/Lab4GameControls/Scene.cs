using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Lab4GameControls.Annotations;

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
            get { return _bomb; }
            set
            {
                if (Equals(value, _bomb)) return;
                _bomb = value;
                OnPropertyChanged("Bomb");
            }
        }

        public bool IsBuisy { get; set; }
        public Action OnWin { get; set; }
        public Action OnGameOver { get; set; }

        public void Init()
        {
            Panzer.Init();
            Bomber.Init();
        }

        public void Start()
        {
            if (IsBuisy) return;
            Counter c = new Counter();
            _t = new Timer(
                state =>
                {
                    Bomber.Update();

                    if (!Bomb.IsEnabled) return;
                    Bomb.Update();
                    if (Bomb.ObjectRect.IntersectsWith(Panzer.ObjectRect))
                    {
                        if (Bomb.IsActive)
                        {
                            Bomb.IsActive = false;
                            Bomb.Destroy();
                            Panzer.Destroy();
                        }
                        if (c.Value >= 50)
                        {
                            Bomb.IsEnabled = false;
                            Panzer.IsEnabled = false;
                            OnWin();
                            Pause();
                            Init();
                        }
                    }
                    else
                        if (Bomb.ObjectRect.Y < 0)
                        {
                            if (Bomb.IsActive)
                            {
                                Bomb.IsActive = false;
                                Bomb.Destroy();
                            }

                            if (c.Value >= 50)
                            {
                                Bomb.IsEnabled = false;
                                OnGameOver();
                                c.Value = 0;
                            }
                        }
                },
                null,
                0,
                10);

            IsBuisy = true;
        }

        public void Pause()
        {
            if (IsBuisy)
            {
                _t.Dispose();
                IsBuisy = false;
            }
        }

        public void Dispose()
        {
            if (_t != null)
            {
                _t.Dispose();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
