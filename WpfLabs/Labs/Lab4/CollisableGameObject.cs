using System;
using System.Windows.Media.Imaging;

namespace Lab4
{
    class Panzer : GameObjectBase
    {
        private double d;
        private bool isdead;

        protected override void OnUpdate()
        {
            if (isdead) return;
            d += .01;
            var rectangle = Rectangle;
             rectangle.X += (Math.Sin(d));
            if (rectangle.X >= SceneSize.Width)
            {
                rectangle.X = -Rectangle.Width;
            }

            Rectangle = rectangle;

        }

        public override void OnColision(GameObjectBase obj)
        {
            Log.Write(Name + obj.Name + ".OnColision()");
            if (obj.Name.Equals("Бомба"))
            {
                //Image = Images[1];
                WithDelay(Destroy,1500);
                isdead = true;
            }
        }
    }

    class Bomb : GameObjectBase
    {
        private bool isdead;
        protected override void OnUpdate()
        {
            if (isdead) return;
            var rectangle = Rectangle;
            rectangle.Y -= 1;
            if (rectangle.X >= SceneSize.Width)
            {
                rectangle.X = -Rectangle.Width;
            }

            Rectangle = rectangle;
        }

        public override void OnColision(GameObjectBase obj)
        {
            if (!obj.Name.Equals("Самолет"))
            {
                //Image = Images[1];
                isdead = true;
                WithDelay(Destroy,1500);
            }
        }
    }

    class StaticObject : GameObjectBase
    {
    }
}