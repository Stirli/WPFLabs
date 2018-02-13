using System;
using System.Windows;

namespace Lab4Game
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void GameControl_OnWin(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Вы победили!", "Поздравляем!");
        }

        private void GameControl_OnGameOver(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Мимо, попробуйте еще раз.", "Печалька!");
        }
    }
}
