using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Lab5
{
    public class ShapeData : INotifyPropertyChanged
    {
        private Brush _background;
        private Brush _border;
        private double _borderWidth;
        private Point _position;

        public ShapeData()
        {
            this.Background = Brushes.Cyan;
            this.Border = Brushes.Black;
            this.BorderWidth = 1;
        }

        public ShapeData(ShapeData shape)
        {
            this.Position = shape.Position;
            this.Background = shape.Background;
            this.Border = shape.Border;
            this.BorderWidth = shape.BorderWidth;
        }

        public Point Position
        {
            get { return this._position; }
            set
            {
                if (value.Equals(this._position)) return;
                this._position = value;
                this.OnPropertyChanged("Position");
            }
        }

        public Brush Background
        {
            get { return this._background; }
            set
            {
                if (Equals(value, this._background)) return;
                this._background = value;
                this.OnPropertyChanged("Background");
            }
        }

        public Brush Border
        {
            get { return this._border; }
            set
            {
                if (Equals(value, this._border)) return;
                this._border = value;
                this.OnPropertyChanged("Border");
            }
        }

        public double BorderWidth
        {
            get { return this._borderWidth; }
            set
            {
                if (value.Equals(this._borderWidth)) return;
                this._borderWidth = value;
                this.OnPropertyChanged("BorderWidth");
            }
        }

        public ShapeData Clone()
        {
            return new ShapeData(this);
        }

        public static explicit operator ShapeData(string str)
        {
            string[] strs = str.Split(',');
            BrushConverter bc = new BrushConverter();
            return new ShapeData
            {
                Position = new Point(double.Parse(strs[0], CultureInfo.InvariantCulture),
                    double.Parse(strs[1], CultureInfo.InvariantCulture)),
                Background = bc.ConvertFrom(strs[2]) as Brush,
                Border = bc.ConvertFrom(strs[3]) as Brush,
                BorderWidth = double.Parse(strs[4])
            };
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0},{1},{2},{3}", this.Position, this.Background, this.Border, this.BorderWidth);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}