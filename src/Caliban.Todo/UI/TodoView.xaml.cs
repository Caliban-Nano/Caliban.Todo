using System.Windows;

namespace Caliban.Todo.UI
{
    /// <summary>
    /// Interaktionslogik für TodoView.xaml
    /// </summary>
    public partial class TodoView : Window
    {
        public TodoView()
        {
            InitializeComponent();

            MouseDown += delegate { DragMove(); };
        }
    }
}
