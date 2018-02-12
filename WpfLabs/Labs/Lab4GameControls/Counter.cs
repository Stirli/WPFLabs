using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4GameControls
{
    class Counter
    {
        private double _value;
        public double Start { get; set; }
        public double Step { get; set; }
        public double End { get; set; }

        public double Value
        {
            get
            {
                double d = _value += Step;
                if (d >= End)
                {
                    d = _value = Start;
                }

                return d;
            }
        }
    }
}
