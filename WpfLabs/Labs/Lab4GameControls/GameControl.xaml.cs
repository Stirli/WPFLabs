using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab4GameControls
{
    using System.Threading;

    /// <summary>
    /// Логика взаимодействия для GameControl.xaml
    /// </summary>
    public partial class GameControl : UserControl
    {
        public GameControl()
        {
            InitializeComponent();
        }

        private void StartCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Context.Start();
        }

        private void StartCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !Context.IsBuisy;
        }

        private void StopCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Context.IsBuisy;
        }

        private void StopCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Context.Stop();
        }
    }
}
