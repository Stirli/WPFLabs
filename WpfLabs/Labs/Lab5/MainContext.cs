using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace Lab5
{
    public class MainContext:INotifyPropertyChanged
    {
        private ShapeData _shapeData;
        private ObservableCollection<ShapeData> _shapes;
        private FileInfo _fileInfo;

        public ShapeData ShapeData
        {
            get { return this._shapeData; }
            set
            {
                if (Equals(value, this._shapeData)) return;
                this._shapeData = value;
                this.OnPropertyChanged("ShapeData");
            }
        }

        public ObservableCollection<ShapeData> Shapes
        {
            get { return this._shapes; }
            set
            {
                if (Equals(value, this._shapes)) return;
                this._shapes = value;
                this.OnPropertyChanged("Shapes");
            }
        }

        public FileInfo FileInfo
        {
            get { return this._fileInfo; }
            set
            {
                if (Equals(value, this._fileInfo)) return;
                this._fileInfo = value;
                this.OnPropertyChanged("FileInfo");
            }
        }


        public MainContext()
        {
            this.ShapeData = new ShapeData();
            this.Shapes = new ObservableCollection<ShapeData>();
            this.FileInfo = new FileInfo("Безымянный.vif");
        }


        public void AddShape()
        {
            this.Shapes.Add(this.ShapeData.Clone());
        }
        
        public bool CanSave
        {
            get
            {
                return this.Shapes.Count > 0;
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
