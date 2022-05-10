using System.Windows.Input;
using Caliban.Nano.Contracts;
using Caliban.Nano.UI;
using Caliban.Todo.Data;

namespace Caliban.Todo.UI
{
    public class MainViewModel : ViewModel, IWindow
    {
        public string DisplayName => "TODO";
        public ICommand AddCommand { get; }
        public ICommand DelCommand { get; }
        public TodoModel Items
        {
            get => _items;
            set
            {
                if (_items != value)
                {
                    _items = value;

                    NotifyPropertyChanged();
                }
            }
        }
        public string? Input
        {
             get => _input;
             set
             {
                if (_input != value)
                {
                    _input = value;

                    NotifyPropertyChanged();
                }
            }
        }

        private TodoModel _items = new();
        private string? _input;

        public MainViewModel()
        {
            AddCommand = new Command<string>(AddTodo, CanTodo);
            DelCommand = new Command<string>(DelTodo);
        }

        public override async Task<bool> OnActivate()
        {
            Items = await TodoRepository.LoadAsync(App.Todofile, false);

            return await base.OnActivate();
        }
        
        public override async Task<bool> OnDeactivate()
        {
            await TodoRepository.SaveAsync(App.Todofile, Items);

            return await base.OnDeactivate();
        }

        private bool CanTodo(string? todo)
        {
            return !string.IsNullOrWhiteSpace(todo);
        }

        private void AddTodo(string? todo)
        {
            Input = "";

            Items.Add(todo!);
        }

        private void DelTodo(string? todo)
        {
            Items.Remove(todo!);
        }
    }
}
