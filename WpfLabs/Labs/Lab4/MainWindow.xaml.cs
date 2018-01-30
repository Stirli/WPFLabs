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

            var img1 = BitmapFrame.Create(new Uri("pack://application:,,,/Lab4;component/Assets/target.png", UriKind.RelativeOrAbsolute));
            var img2 = BitmapFrame.Create(new Uri("pack://application:,,,/Lab4;component/Assets/bomber.png", UriKind.RelativeOrAbsolute));
            var img3 = BitmapFrame.Create(new Uri("pack://application:,,,/Lab4;component/Assets/bum.png", UriKind.RelativeOrAbsolute));
            var img4 = BitmapFrame.Create(new Uri("pack://application:,,,/Lab4;component/Assets/bomb.png", UriKind.RelativeOrAbsolute));
            ContextGameObject context = new ContextGameObject(ListBox.RenderSize);
            Log.Write = (str) =>
            {
                try
                {
                    context.Log.Insert(0,DateTime.Now + " : " + str);
                }
                catch (Exception exception)
                {
                    Log.Write(exception.Message);
                }
            };
            var ground = context.CreateGameObject<StaticObject>();
            ground.Rectangle = new Rect(0, 0, ListBox.RenderSize.Width,49);
            var bomber = context.CreateGameObject<Bomber>();
            bomber.Image = img2;
            bomber.Rectangle = new Rect(50, 500, 200, 150);
            bomber.Name = "Самолет";
            var panzer = context.CreateGameObject<Panzer>();
            panzer.Images = new[] { img1, img3 };
            panzer.Image = img1;
            panzer.Rectangle = new Rect(700, 50, 200, 150);
            panzer.Name = "Танк";

            Timer t = new Timer(10);
            t.Elapsed += (s, args) =>
            {
                List<GameObjectBase> list = context.GameObjects.ToList();
                for (var i = 0; i < list.Count; i++)
                {
                    var item = list[i];
                    item.Update();
                }
                for (var i = 0; i < list.Count; i++)
                {
                    var item = list[i];

                    for (int j = 0; j < list.Count; j++)
                    {
                        if (i == j)
                            continue;
                        var item2 = list[j];
                        if (item.Rectangle.IntersectsWith(item2.Rectangle))
                        {
                            UIElement el = ListBox.ItemContainerGenerator.ContainerFromItem(item) as UIElement;
                            Dispatcher.Invoke(new Action(() =>
                            {
                                item.OnColision(item2);
                                item2.OnColision(item);
                            }));
                        }
                    }
                }
            };
            shoot = () =>
            {
                var bomb = context.CreateGameObject<Bomb>();
                bomb.Image = img4;
                bomb.Images = new[] { img4, img3 };
                bomb.Rectangle = new Rect(bomber.Rectangle.TopLeft, new Size(50, 100));
                bomb.Name = "Бомба";
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
