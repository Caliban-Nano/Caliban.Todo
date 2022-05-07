using Caliban.Nano.Contracts;
using Caliban.Nano.UI;
using Caliban.Todo.Models;
using Caliban.Todo.Parser;

namespace Caliban.Todo.Shell
{
    public sealed class ShellViewModel : ViewModel, IWindow
    {
        public string DisplayName => "TODO";

        public TodoList Todo;

        public ShellViewModel()
        {
            Todo = Markdown.Import(@".\TODO.md");
        }
    }
}
