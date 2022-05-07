using System.Windows.Input;
using Caliban.Nano.Contracts;
using Caliban.Nano.UI;
using Caliban.Todo.Data;
using Caliban.Todo.Logic;

namespace Caliban.Todo.UI
{
    public class NoteViewModel : ViewModel, IWindow
    {
        public string DisplayName => "TODO";
        public ICommand TodoCommand { get; }
        public NoteModel? Items
        {
            get => _items;
            set
            {
                _items = value;

                NotifyPropertyChanged();
            }
        }
        public string? Input
        {
             get => _input;
             set
             {
                 _input = value;

                 NotifyPropertyChanged();
             }
        }

        private NoteModel? _items;
        private string? _input;

        public NoteViewModel()
        {
            TodoCommand = new Command<string>(AddTodo, CanTodo);
        }

        public override async Task<bool> OnActivate()
        {
            Items = await Markdown.ImportAsync(App.Todofile);

            return await base.OnActivate();
        }
        
        public override async Task<bool> OnDeactivate()
        {
            await Markdown.ExportAsync(App.Todofile, Items!);

            return await base.OnDeactivate();
        }

        private void AddTodo(string? todo)
        {
            Input = "";

            Items?.Add(todo!);
        }

        private bool CanTodo(string? todo)
        {
            return !string.IsNullOrWhiteSpace(todo);
        }
    }
}
