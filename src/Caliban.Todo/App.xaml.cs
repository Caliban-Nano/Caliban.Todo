using System.Reflection;
using System.Windows;
using Caliban.Nano;
using Caliban.Todo.Shell;

namespace Caliban.Todo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void OnStartup(object sender, StartupEventArgs e)
        {
            new Bootstrap()
                .AddAssembly(Assembly.GetExecutingAssembly())
                .AddNamespace("Caliban.Todo.Shell")
                .Show<ShellViewModel>();
        }
    }
}
