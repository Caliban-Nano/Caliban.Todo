namespace Caliban.Todo.Models
{
    public enum TodoType { Title, Todo, Done };

    public class TodoList : List<(TodoType, string)>
    {
    }
}
