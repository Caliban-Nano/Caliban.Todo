using System.Windows;

namespace Caliban.Todo.UI
{
    /// <summary>
    /// Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            MouseDown += delegate
            {
                DragMove();
            };
        }
    }
}
