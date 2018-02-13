using System;
using System.Windows;

namespace Lab4GameControls
{
    class Bomber : GameObject
    {
        private Counter counter;

        public override void Init()
        {
            Rect rect = ObjectRect;
            rect.Location = new Point(0, 600);
            ObjectRect = rect;
            counter = new Counter { Start = -200, Step = 1, End = 1280 };
            IsEnabled = true;
        }

        public override void Update()
        {
            Rect rect = ObjectRect;
            rect.X = Math.Round(counter.Value);
            ObjectRect = rect;
            State = string.Format("Координаты самолета: {0}", ObjectRect.Location);
        }
    }
}