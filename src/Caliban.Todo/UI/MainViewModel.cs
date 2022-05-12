using System.Collections.ObjectModel;
using System.Windows.Input;
using Caliban.Nano.Contracts;
using Caliban.Nano.UI;
using Caliban.Todo.Data;

namespace Caliban.Todo.UI
{
    /// <summary>
    /// Todo list main view model.
    /// </summary>
    public sealed class MainViewModel : ViewModel, IWindow
    {
        /// <summary>
        /// The window display name.
        /// </summary>
        public string DisplayName => "TODO";
        
        /// <summary>
        /// The todo add command.
        /// </summary>
        public ICommand AddCommand { get; }
        
        /// <summary>
        /// The todo del command.
        /// </summary>
        public ICommand DelCommand { get; }

        /// <summary>
        /// The todo list model.
        /// </summary>
        public TodoModel Model { get; private set; } = new();

        #region Dedicated UI properties

        /// <summary>
        /// The UI todo header.
        /// </summary>
        public string Header => Model.Header;

        /// <summary>
        /// The UI todo items.
        /// </summary>
        public ObservableCollection<string> Items => Model.Items;

        /// <summary>
        /// The UI item input.
        /// </summary>
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

        /// <summary>
        /// Initializes the view model with its commands.
        /// </summary>
        public MainViewModel()
        {
            AddCommand = new Command<string>(AddTodo, CanTodo);
            DelCommand = new Command<string>(DelTodo);
        }

        /// <summary>
        /// Closes the window.
        /// </summary>
        /// <returns></returns>
        public static async Task CloseWindow()
        {
            await WindowManager.CloseWindowAsync();
        }

        /// <summary>
        /// Loads the todo model on activation.
        /// </summary>
        /// <returns>True if activation was successful; otherwise false.</returns>
        public override async Task<bool> OnActivate()
        {
            Model = await TodoRepository.LoadAsync(App.Todofile, false);

            NotifyPropertyChanged("Header");
            NotifyPropertyChanged("Items");

            return await base.OnActivate();
        }

        /// <summary>
        /// Saves the todo model on deactivation.
        /// </summary>
        /// <returns>True if deactivation was successful; otherwise false.</returns>
        public override async Task<bool> OnDeactivate()
        {
            await TodoRepository.SaveAsync(App.Todofile, Model);

            return await base.OnDeactivate();
        }

        /// <summary>
        /// Returns if an item can be added.
        /// </summary>
        /// <param name="todo">The todo item.</param>
        /// <returns>True if the item can be added; otherwise false.</returns>
        private bool CanTodo(string? todo)
        {
            return !string.IsNullOrWhiteSpace(todo);
        }

        /// <summary>
        /// Adds a todo item.
        /// </summary>
        /// <param name="todo">The todo item.</param>
        private void AddTodo(string? todo)
        {
            Input = "";

            if (todo is not null)
            {
                Model.Items.Add(todo);
            }
        }

        /// <summary>
        /// Deletes a todo item.
        /// </summary>
        /// <param name="todo">The todo item.</param>
        private void DelTodo(string? todo)
        {
            if (todo is not null)
            {
                Model.Items.Remove(todo);
            }
        }
    }
}
