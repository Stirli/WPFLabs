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
using System.Windows.Shapes;

namespace Lab4Game
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
