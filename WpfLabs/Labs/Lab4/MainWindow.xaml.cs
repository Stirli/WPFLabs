using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lab4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            /*
                    Visual visual = ListBox.ItemContainerGenerator.ContainerFromItem(item) as Visual;
                        VisualTreeHelper.HitTest(visual, item2.Rectangle.);
             */

            var targetImg = BitmapFrame.Create(new Uri("pack://application:,,,/Lab4;component/Assets/target.png", UriKind.RelativeOrAbsolute));
            var bomberImg = BitmapFrame.Create(new Uri("pack://application:,,,/Lab4;component/Assets/bomber.png", UriKind.RelativeOrAbsolute));
            var bumImg = BitmapFrame.Create(new Uri("pack://application:,,,/Lab4;component/Assets/bum.png", UriKind.RelativeOrAbsolute));
            var bombImg = BitmapFrame.Create(new Uri("pack://application:,,,/Lab4;component/Assets/bomb.png", UriKind.RelativeOrAbsolute));
            Context context = new Context(ListBox.RenderSize);
            Log.Write = (str) =>
            {
                try
                {
                    context.Log.Insert(0, DateTime.Now + " : " + str);
                }
                catch (Exception exception)
                {
                    Log.Write(exception.Message);
                }
            };
            StaticObject ground = new StaticObject(); context.CreateGameObject(ground, Dispatcher);
            ground.Rectangle = new Rect(0, 0, ListBox.RenderSize.Width, 49);
            Bomber bomber = new Bomber
            {
                Image = bomberImg,
                Rectangle = new Rect(50, 500, 200, 100),
                Name = "Самолет"
            };
            context.CreateGameObject(bomber, Dispatcher);
            Panzer panzer = new Panzer
            {
                Images = new[] { targetImg, bumImg },
                Image = targetImg,
                Rectangle = new Rect(700, 50, 200, 150),
                Name = "Танк"
            };
            context.CreateGameObject(panzer, Dispatcher);
            Timer t = new Timer(10);
            t.Elapsed += (s, args) =>
            {
                List<GameObjectBase> list = context.GameObjects.ToList();
                context.UpdateObjects();

                for (var i = 1; i < list.Count; i++)
                {
                    var item = list[i];
                    if(item.Name=="Бомба")
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (i == j)
                            continue;
                        var item2 = list[j];
                        if (item.Rectangle.IntersectsWith(item2.Rectangle))
                        {
                            item.OnColision(item2);
                            item2.OnColision(item);
                            //Dispatcher.Invoke(new Action(() =>
                            //{
                            //}));
                        }
                    }
                }
            };

            shoot = () =>
            {
                Point point = bomber.Rectangle.TopLeft;
                point.Offset(50, -100);
                Bomb bomb = new Bomb
                {
                    Image = bombImg,
                    Images = new[] { bombImg, bumImg },
                    Rectangle = new Rect(point, new Size(30, 30)),
                    Name = "Бомба"
                };
                context.CreateGameObject(bomb, Dispatcher);
            };

            t.Start();

            context.Log = new ObservableCollection<string>();
            DataContext = context;
        }

        private Action shoot;
        private void ListBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            shoot();
        }
    }
}
