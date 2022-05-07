using System.Windows;
using Caliban.Nano;
using Caliban.Todo.UI;

namespace Caliban.Todo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly string Todofile = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\TODO.md");

        void OnStartup(object sender, StartupEventArgs e)
        {
            new Bootstrap().Show<NoteViewModel>();
        }
    }
}
