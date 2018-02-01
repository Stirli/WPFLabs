using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Lab4.Annotations;

namespace Lab4
{
    class Context
    {
        private readonly ObservableCollection<GameObjectBase> _gameObjects;

        public IEnumerable<GameObjectBase> GameObjects
        {
            get { return _gameObjects; }
        }

        public ObservableCollection<string> Log { get; set; }

        public Context(Size sceneSize)
        {
            GameObjectBase.SceneSize = sceneSize;
            _gameObjects = new ObservableCollection<GameObjectBase>();
        }

        public void CreateGameObject<T>(T gameObjectBase, Dispatcher dispatcher) where T : GameObjectBase, new()
        {
            _gameObjects.Add(gameObjectBase);
            gameObjectBase.DestroingCallback = () =>
            {
                dispatcher.BeginInvoke(new Action(() => _gameObjects.Remove(gameObjectBase)));
            };
        }

        public void UpdateObjects()
        {
            foreach (GameObjectBase gameObject in GameObjects.ToList())
            {
                gameObject.Update();
            }
        }
    }


    abstract class GameObjectBase : INotifyPropertyChanged
    {
        public static Size SceneSize { get; internal set; }

        private Rect _rectangle;

        public string Name { get; set; }

        public BitmapFrame Image
        {
            get { return _image; }
            set
            {
                if (Equals(value, _image)) return;
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        public BitmapFrame[] Images { get; set; }


        public Rect Rectangle
        {
            get { return _rectangle; }
            set
            {
                if (value.Equals(_rectangle)) return;
                _rectangle = value;

                OnPropertyChanged("Rectangle");
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        protected GameObjectBase()
        {
            Name = string.Empty;
        }

        protected void WithDelay(Action action, int delay)
        {
            Timer t = new Timer(state => action());
            
        }


        internal void Update()
        {
            OnUpdate();
        }

        protected virtual void OnUpdate()
        {
        }

        public virtual void OnColision(GameObjectBase destroyableGameObject)
        {

        }

        #region OnDestroy

        internal Action DestroingCallback;
        private BitmapFrame _image;

        public void Destroy()
        {
            if (DestroingCallback != null) DestroingCallback();
        }

        #endregion

        #region PropertyChanged

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
