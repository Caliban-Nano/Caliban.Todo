using Caliban.Nano.Contracts;
using Caliban.Nano.UI;

namespace Caliban.Todo.Shell
{
    public class ShellViewModel : ViewModel, IWindow
    {
        public string DisplayName => "Todo";

        public ShellViewModel()
        {
        }
    }
}
