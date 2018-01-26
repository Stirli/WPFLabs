using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Lab5
{
    public class BrushParamsContext
    {
        public ShapeData Result { get; set; }
        public ObservableCollection<Brush> Brushes { get; set; }

        public BrushParamsContext()
        {
            Result = new ShapeData();
            Brushes = new ObservableCollection<Brush>();
            for (byte b = 0; b <= 255; b += 32)
            {
                for (byte g = 0; g <= 255; g += 32)
                {
                    for (byte r = 0; r <= 255; r += 32)
                    {
                        Brushes.Add(new SolidColorBrush(Color.FromRgb(r, g, b)));
                    }
                }
            }
            //for (int i = 0; i < 360; i++)
            //{
            //    Brushes.Add(new SolidColorBrush(ColorFromHSV(i, 1, 1)));
            //}
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            byte v = Convert.ToByte(value);
            byte p = Convert.ToByte(value * (1 - saturation));
            byte q = Convert.ToByte(value * (1 - f * saturation));
            byte t = Convert.ToByte(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
    }
}
