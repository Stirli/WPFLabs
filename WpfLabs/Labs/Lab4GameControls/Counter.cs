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
                double d = this._value += this.Step;
                if (d >= this.End)
                {
                    d = this._value = this.Start;
                }

                return d;
            }
        }
    }
}
