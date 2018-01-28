using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Lab5.Annotations;

namespace Lab5
{
    public class MainContext:INotifyPropertyChanged
    {
        private ShapeData _shapeData;
        private ObservableCollection<ShapeData> _shapes;
        private FileInfo _fileInfo;

        public ShapeData ShapeData
        {
            get { return _shapeData; }
            set
            {
                if (Equals(value, _shapeData)) return;
                _shapeData = value;
                OnPropertyChanged("ShapeData");
            }
        }

        public ObservableCollection<ShapeData> Shapes
        {
            get { return _shapes; }
            set
            {
                if (Equals(value, _shapes)) return;
                _shapes = value;
                OnPropertyChanged("Shapes");
            }
        }

        public FileInfo FileInfo
        {
            get { return _fileInfo; }
            set
            {
                if (Equals(value, _fileInfo)) return;
                _fileInfo = value;
                OnPropertyChanged("FileInfo");
            }
        }


        public MainContext()
        {
            ShapeData = new ShapeData();
            Shapes = new ObservableCollection<ShapeData>();
            FileInfo = new FileInfo("Безымянный.vif");
        }


        public void AddShape()
        {
            Shapes.Add(ShapeData.Clone());
        }



        public void Load(string path)
        {
            Shapes.Clear();
            IEnumerable<string> lines = File.ReadLines(path);
            foreach (string line in lines)
            {
                ShapeData shapeData = (ShapeData)line;
                Shapes.Add(shapeData);
            }
            FileInfo = new FileInfo(path);
        }
        public void Save(string path)
        {
            File.WriteAllLines(path, Shapes.ToList().ConvertAll(input => input.ToString()));
            FileInfo = new FileInfo(path);
        }

        public bool CanSave
        {
            get
            {
                return Shapes.Count > 0;
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
