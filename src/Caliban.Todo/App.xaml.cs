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
        public static string Todofile { get; private set; } = GetUserPath("TODO.md");

        /// <summary>
        /// Bootstraps the Caliban.Nano framework and shows the main view model.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        void OnStartup(object sender, StartupEventArgs e)
        {
            new Bootstrap().Show<TodoViewModel>();
        }

        /// <summary>
        /// Returns the user specific path of the given filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>The user path.</returns>
        private static string GetUserPath(string filename)
        {
            return Environment.ExpandEnvironmentVariables($@"%USERPROFILE%\{filename}");
        }
    }
}
