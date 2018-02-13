using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Lab4GameControls
{
    class Bomb : GameObject
    {
        public override void Init()
        {
            counter = new Counter() { Step = .02, End = double.MaxValue };
            Image = BitmapFrame.Create(new Uri("pack://application:,,,/Lab4GameControls;component/Assets/bomb.png", UriKind.RelativeOrAbsolute));
            IsEnabled = true;
            IsActive = true;
        }

        private Counter counter;

        public override void Update()
        {
            if (IsActive)
            {
                Rect rect = ObjectRect;
                rect.X += 1;
                double y = counter.Value;
                rect.Y -= y;
                ObjectRect = rect;
                State = string.Format("Скорость бомбы: {0:F}",Math.Sqrt(1 + y * y));
            }
        }
    }
}