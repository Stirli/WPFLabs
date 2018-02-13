using System;
using System.Windows;

namespace Lab4GameControls
{
    class Bomber : GameObject
    {
        private Counter counter;

        public override void Init()
        {
            Rect rect = this.ObjectRect;
            rect.Location = new Point(0, 600);
            this.ObjectRect = rect;
            this.counter = new Counter { Start = -200, Step = 1, End = 1280 };
            this.IsEnabled = true;
        }

        public override void Update()
        {
            Rect rect = this.ObjectRect;
            rect.X = Math.Round(this.counter.Value);
            this.ObjectRect = rect;
            this.State = string.Format("Координаты самолета: {0}", this.ObjectRect.Location);
        }
    }
}