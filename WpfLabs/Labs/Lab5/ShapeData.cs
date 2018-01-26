using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Media;
using Lab5.Annotations;

namespace Lab5
{
    public class ShapeData:INotifyPropertyChanged
    {
        private Brush _background;
        private Brush _border;
        private double _borderWidth;

        public ShapeData()
        {
            Background = Brushes.Cyan;
            Border = Brushes.Black;
            BorderWidth = 1;
        }

        public ShapeData(ShapeData shape)
        {
            X = shape.X;
            Y = shape.Y;
            Background = shape.Background;
            Border = shape.Border;
            BorderWidth = shape.BorderWidth;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public Brush Background
        {
            get { return _background; }
            set
            {
                if (Equals(value, _background)) return;
                _background = value;
                OnPropertyChanged("Background");
            }
        }

        public Brush Border
        {
            get { return _border; }
            set
            {
                if (Equals(value, _border)) return;
                _border = value;
                OnPropertyChanged("Border");
            }
        }

        public double BorderWidth
        {
            get { return _borderWidth; }
            set
            {
                if (value.Equals(_borderWidth)) return;
                _borderWidth = value;
                OnPropertyChanged("BorderWidth");
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
                X = double.Parse(strs[0]),
                Y = double.Parse(strs[1]),
                Background = bc.ConvertFrom(strs[2]) as Brush,
                Border = bc.ConvertFrom(strs[3]) as Brush,
                BorderWidth = double.Parse(strs[4])
            };
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4}", X, Y, Background, Border, BorderWidth);
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