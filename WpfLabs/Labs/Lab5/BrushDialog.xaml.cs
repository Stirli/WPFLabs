using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Lab5
{
    /// <summary>
    /// Логика взаимодействия для BrushDialog.xaml
    /// </summary>
    public partial class BrushDialog : Window
    {
        public BrushDialog()
        {
            this.InitializeComponent();
            Binding binding = new Binding("ResultData") { Source = this };
            this.MainContainer.SetBinding(DataContextProperty, binding);
        }

        public ShapeData ResultData
        {
            get { return (ShapeData)this.GetValue(ResultDataProperty); }
            set {
                this.SetValue(ResultDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ResultData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResultDataProperty =
            DependencyProperty.Register("ResultData", typeof(ShapeData), typeof(BrushDialog), new UIPropertyMetadata(new ShapeData()));



        private void OkCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void OkCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.IsValid(this.MainContainer);
        }

        private bool IsValid(DependencyObject obj)
        {
            // The dependency object is valid if it has no errors and all
            // of its children (that are dependency objects) are error-free.
            return !Validation.GetHasError(obj) &&
                   LogicalTreeHelper.GetChildren(obj)
                       .OfType<DependencyObject>()
                       .All(this.IsValid);
        }

    }
}
