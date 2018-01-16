using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Lab3
{
    public class Values
    {
        private double _xStop;
        private double _xStart;
        private double _n;
        private double _step;

        // TODO В WPF валидация данных делается через интерфрейс IDataErrorInfo
        // TODO То как это предлагается делать в методичке - полная (_Y_)

        public double XStart
        {
            get { return _xStart; }
            set
            {
                if (value > XStop)
                    throw new ArgumentException("Конечное значение меньше начального.");
                _xStart = value;
            }
        }

        public double XStop
        {
            get { return _xStop; }
            set
            {
                if (value < XStart)
                    throw new ArgumentException("Конечное значение меньше начального.");
                _xStop = value;
            }
        }

        public double Step
        {
            get { return _step; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Шаг должен быть больше 0.");
                _step = value;
            }
        }

        public double N
        {
            get { return _n; }
            set
            {
                if (value < 1)
                    throw new ArgumentException("N должно быть больше 0.");
                _n = value;
            }
        }

        public ObservableCollection<string> Results { get; set; }

        public Values()
        {
            Results = new ObservableCollection<string>();
        }
    }

}
