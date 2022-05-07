namespace Caliban.Todo.UI.Models
{
    public enum TodoType { Title, Todo, Done };

    public class TodoModel
    {
        public string Title { get; init; }

        public readonly List<(TodoType, string)> Items = new();

        public TodoModel(string title)
        {
            Title = title;
        }
    }
}
