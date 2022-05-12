using System.Collections.ObjectModel;
using System.Text;

namespace Caliban.Todo.Data
{
    /// <summary>
    /// Todo list model.
    /// </summary>
    public sealed class TodoModel
    {
        /// <summary>
        /// The todo list header.
        /// </summary>
        public string Header { get; set; } = "TODO";

        /// <summary>
        /// The todo list items.
        /// </summary>
        public readonly ObservableCollection<string> Items = new();

        /// <summary>
        /// Returns the todo list as Markdown.
        /// </summary>
        /// <returns>The todo list as string.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"# {Header}");

            foreach (var item in Items)
            {
                sb.AppendLine($"* {item}");
            }

            return sb.ToString();
        }
    }
}
