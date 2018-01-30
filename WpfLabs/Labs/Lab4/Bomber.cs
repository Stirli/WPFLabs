namespace Lab4
{
    class Bomber : GameObjectBase
    {
        public override void Update()
        {
            var rectangle = Rectangle;
            rectangle.X += 1;
            if (rectangle.X >= SceneSize.Width)
            {
                rectangle.X = - Rectangle.Width;
            }

            Rectangle = rectangle;
        }
    }
}