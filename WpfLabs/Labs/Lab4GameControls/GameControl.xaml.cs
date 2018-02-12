using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab4GameControls
{
    /// <summary>
    /// Логика взаимодействия для GameControl.xaml
    /// </summary>
    public partial class GameControl : UserControl
    {

        public GameControl()
        {
            this.InitializeComponent();
            this.Context.Init();
        }


        private void StartCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Context.Start();
        }

        private void StartCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !this.Context.IsBuisy;
        }

        private void StopCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.Context.IsBuisy;
        }

        private void StopCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Context.Stop();
        }

        private void FireCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Rect bombObjectRect = this.Context.Bomb.ObjectRect;
            bombObjectRect.Location = this.Context.Bomber.ObjectRect.Location;
            this.Context.Bomb.ObjectRect = bombObjectRect;
            this.Context.Bomb.IsEnabled = true;
        }

        private void FireCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
