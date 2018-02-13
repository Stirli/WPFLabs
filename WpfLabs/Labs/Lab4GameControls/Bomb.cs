using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Lab4GameControls
{
    class Bomb : GameObject
    {
        public override void Init()
        {
            this.counter = new Counter() { Step = .02, End = double.MaxValue };
            this.Image = BitmapFrame.Create(new Uri("pack://application:,,,/Lab4GameControls;component/Assets/bomb.png", UriKind.RelativeOrAbsolute));
            this.IsEnabled = true;
            this.IsActive = true;
        }

        private Counter counter;

        public override void Update()
        {
            if (this.IsActive)
            {
                Rect rect = this.ObjectRect;
                rect.X += 1;
                double y = this.counter.Value;
                rect.Y -= y;
                this.ObjectRect = rect;
                this.State = string.Format("Скорость бомбы: {0:F}", Math.Sqrt(1 + y * y));
            }
        }
    }
}