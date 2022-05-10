using System.Collections.ObjectModel;
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
        public TodoModel Model { get; private set; } = new();

        #region UI Properties
        public ObservableCollection<string> Items => Model.Items;
        public string Title => Model.Title;
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

        private string? _input;
        #endregion

        public MainViewModel()
        {
            AddCommand = new Command<string>(AddTodo, CanTodo);
            DelCommand = new Command<string>(DelTodo);
        }

        public override async Task<bool> OnActivate()
        {
            Model = await TodoRepository.LoadAsync(App.Todofile, false);

            NotifyPropertyChanged("Title");
            NotifyPropertyChanged("Items");

            return await base.OnActivate();
        }
        
        public override async Task<bool> OnDeactivate()
        {
            await TodoRepository.SaveAsync(App.Todofile, Model);

            return await base.OnDeactivate();
        }

        private bool CanTodo(string? todo)
        {
            return !string.IsNullOrWhiteSpace(todo);
        }

        private void AddTodo(string? todo)
        {
            Input = "";

            if (todo is not null)
            {
                Items.Add(todo);
            }
        }

        private void DelTodo(string? todo)
        {
            if (todo is not null)
            {
                Items.Remove(todo);
            }
        }
    }
}
