using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Win32;

namespace Lab5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainContext context;
        public MainWindow()
        {
            InitializeComponent();
            context = Resources["MainContext"] as MainContext;
        }



        private void SetBrushCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            BrushDialog bd = new BrushDialog { ResultData = context.ShapeData.Clone() };
            if (bd.ShowDialog() == true)
                context.ShapeData = bd.ResultData.Clone();
        }

        private void SaveAsCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            FileDialog fd = GetFileDialog<SaveFileDialog>();
            if (fd.ShowDialog() == true)
            {
                context.Save(fd.FileName);
                MessageBox.Show("Успех");
            }
        }

        private void SaveCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = context.CanSave;
        }

        private void OpenCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            FileDialog fd = GetFileDialog<OpenFileDialog>();
            if (fd.ShowDialog() == true)
            {
                context.Load(fd.FileName);
            }
        }


        private void AboutCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            new About().ShowDialog();
        }

        /**********************************************************************/
        #region Canvas events
        /**********************************************************************/
        private void DrawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(sender as IInputElement);
            context.ShapeData.Position = position;
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                context.AddShape();
            }
        }

        #endregion


        private FileDialog GetFileDialog<T>() where T : FileDialog, new()
        {
            return new T { InitialDirectory = context.FileInfo.DirectoryName, FileName = context.FileInfo.Name, Filter = "Файлы Vector Image Format (vif)|*.vif|Все файлы|*.*" };
        }
    }
}