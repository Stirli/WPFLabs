using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Lab4.Annotations;

namespace Lab4
{
    class ContextGameObject
    {
        private readonly ObservableCollection<GameObjectBase> _gameObjects;

        public IEnumerable<GameObjectBase> GameObjects
        {
            get { return _gameObjects; }
        }

        public ObservableCollection<string> Log { get; set; }

        public ContextGameObject(Size sceneSize)
        {
            GameObjectBase.SceneSize = sceneSize;
            _gameObjects = new ObservableCollection<GameObjectBase>();
        }

        public T CreateGameObject<T>() where T : GameObjectBase, new()
        {
            var gameObjectBase = new T();
            _gameObjects.Add(gameObjectBase);
            gameObjectBase.DestroingCallback = () => _gameObjects.Remove(gameObjectBase);
            return gameObjectBase;
        }
    }


    abstract class GameObjectBase : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public static Size SceneSize { get; internal set; }

        private Rect _rectangle;

        public BitmapFrame Image { get; set; }
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

        public abstract void Update();


        internal Action DestroingCallback;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Destroy()
        {
            Log.Write(Name + ".Destroy()");
            if (DestroingCallback != null) DestroingCallback();
        }


        public virtual void OnColision(GameObjectBase destroyableGameObject)
        {

        }

    }
}
