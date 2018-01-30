using System;
using System.Windows.Media.Imaging;

namespace Lab4
{
    class Panzer : GameObjectBase
    {
        private double d;

        public override void Update()
        {
            d += .01;
            var rectangle = Rectangle;
            // rectangle.X += (Math.Sin(d));
            if (rectangle.X >= SceneSize.Width)
            {
                rectangle.X = -Rectangle.Width;
            }

            Rectangle = rectangle;

            if (BeforeDestroing())
                Destroy();
        }

        int timer = 0;

        protected bool BeforeDestroing()
        {
            if (timer == 200)
            {
                Image = Images[1];
                return false;
            }

            if (timer >= 0)
            {
                return false;
            }

            return true;
        }
    }

    class Bomb : GameObjectBase
    {
        private int timer = 200;
        public double d = .01;
        public override void Update()
        {
            d += .01;
            var rectangle = Rectangle;
            rectangle.Y -= 1;
            if (rectangle.X >= SceneSize.Width)
            {
                rectangle.X = -Rectangle.Width;
            }

            Rectangle = rectangle;

            if (BeforeDestroing())
                Destroy();
        }

        protected bool BeforeDestroing()
        {
            if (timer-- == 200)
            {
                Image = Images[1];
                return false;
            }

            if (timer >= 0)
            {
                return false;
            }

            return true;
        }

        public override void OnColision(GameObjectBase obj)
        {
            Log.Write(Name + obj.Name + ".OnColision()");
            if (obj.Name.Equals("Танк"))
                obj.Destroy();
            if (!obj.Name.Equals("Самолет"))
                Destroy();
        }
    }

    class StaticObject : GameObjectBase
    {
        public override void Update()
        {

        }
    }
}