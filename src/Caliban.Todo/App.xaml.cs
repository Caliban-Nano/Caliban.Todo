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
        /// <summary>
        /// The todo list file path.
        /// </summary>
        public static readonly string Todofile = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\TODO.md");

        /// <summary>
        /// Bootstraps the Caliban.Nano framework and shows the main view model.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        void OnStartup(object sender, StartupEventArgs e)
        {
            new Bootstrap().Show<MainViewModel>();
        }
    }
}
