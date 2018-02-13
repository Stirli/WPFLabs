using System;

namespace Lab4GameControls
{
    class Counter
    {
        public Counter()
        {
            this.Step = 1;
            this.End = double.MaxValue;
        }

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

            set {
                this._value = value; }
        }
    }
}
