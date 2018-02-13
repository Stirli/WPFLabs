using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

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
            this.Context.OnWin = this.OnWin;
            this.Context.OnGameOver = this.OnGameOver;
            this.Context.Init();
        }

        public event EventHandler Win;
        public event EventHandler GameOver;



        public string SelectedObjectInfo
        {
            get { return (string)this.GetValue(SelectedObjectInfoProperty); }
            set {
                this.SetValue(SelectedObjectInfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedObjectInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedObjectInfoProperty =
            DependencyProperty.Register("SelectedObjectInfo", typeof(string), typeof(GameControl), new UIPropertyMetadata(string.Empty));



        private void StartCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Context.Start();
        }

        private void StartCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !this.Context.IsBuisy;
        }

        private void PauseCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Context.Pause();
        }

        private void PauseCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.Context.IsBuisy;
        }

        private void ResetCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Context.Init();
        }

        private void ResetCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void FireCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Rect bombObjectRect = this.Context.Bomb.ObjectRect;
            bombObjectRect.Location = this.Context.Bomber.ObjectRect.Location;
            this.Context.Bomb.ObjectRect = bombObjectRect;
            this.Context.Bomb.Init();
        }

        private void FireCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.Context.IsBuisy && !this.Context.Bomb.IsEnabled;
        }

        protected virtual void OnWin()
        {
            var handler = this.Win;
            if (handler != null) this.Dispatcher.Invoke(DispatcherPriority.Normal, handler, this, EventArgs.Empty);
        }

        protected virtual void OnGameOver()
        {
            var handler = this.GameOver;
            if (handler != null) this.Dispatcher.Invoke(DispatcherPriority.Normal, handler, this, EventArgs.Empty);
        }

        private void Element_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.SetBinding(SelectedObjectInfoProperty, new Binding("State") { Source = (sender as FrameworkElement).DataContext });
        }
    }
}
