using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Caliban.Nano.Contracts;
using Caliban.Nano.UI;
using Caliban.Todo.Data;

namespace Caliban.Todo.UI
{
    /// <summary>
    /// Todo list view model.
    /// </summary>
    public sealed class TodoViewModel : ViewModel, IWindow
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
        /// The todo list items.
        /// </summary>
        public ObservableCollection<string> Items => Todo.Items;

        /// <summary>
        /// The UI item input.
        /// </summary>
        public string? Input
        {
            get => _input;
            set => SetProperty(ref _input, value);
        }

        private string? _input;

        private TodoModel Todo => ModelAs<TodoModel>();

        /// <summary>
        /// Initializes the view model with its commands.
        /// </summary>
        public TodoViewModel()
        {
            AddCommand = new Command<string>(AddTodo, CanTodo);
            DelCommand = new Command<string>(DelTodo);
        }

        /// <summary>
        /// Minimizes the window.
        /// </summary>
        public void MinimizeWindow()
        {
            ViewAs<Window>().WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Closes the window.
        /// </summary>
        /// <returns>An asynchronous task.</returns>
        public async Task CloseWindow()
        {
            await WindowManager.CloseAsync();
        }

        /// <summary>
        /// Loads the todo model on activation.
        /// </summary>
        /// <returns>True if activation was successful; otherwise false.</returns>
        public override async Task<bool> OnActivate()
        {
            await Todo.Request(App.Todofile);

            return await base.OnActivate();
        }

        /// <summary>
        /// Saves the todo model on deactivation.
        /// </summary>
        /// <returns>True if deactivation was successful; otherwise false.</returns>
        public override async Task<bool> OnDeactivate()
        {
            await Todo.Persist(App.Todofile);

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
                Todo.Items.Add(todo);
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
                Todo.Items.Remove(todo);
            }
        }
    }
}
