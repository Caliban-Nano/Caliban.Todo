using Caliban.Nano.Contracts;
using Caliban.Nano.UI;
using Caliban.Todo.UI.Models;

namespace Caliban.Todo.UI.ViewModels
{
    public sealed class TodoViewModel : ViewModel, IWindow
    {
        public string DisplayName => "TODO";

        public TodoModel? TodoModel;

        public string? Header => TodoModel?.Title;

        public List<(TodoType, string)>? Items => TodoModel?.Items;

        public string? Input;

        //public ICommand WriteCommand = new Command<string>(
        //    (text) => AppendItem(text),
        //    (text) => !string.IsNullOrWhiteSpace(text)
        //);

        public TodoViewModel()
        {
            TodoModel = new TodoModel("");
        }

        //public void AppendItem(string text)
        //{
        //    Items.Add(text);

        //    NotifyPropertyChanged(() => Items);
        //}
    }
}
