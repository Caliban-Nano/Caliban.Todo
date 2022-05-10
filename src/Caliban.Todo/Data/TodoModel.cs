using System.Collections.ObjectModel;
using System.Text;

namespace Caliban.Todo.Data
{
    public class TodoModel
    {
        public string Title { get; set; } = "";

        public readonly ObservableCollection<string> Items = new();

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"# {Title}");

            foreach (var item in Items)
            {
                sb.AppendLine($"* {item}");
            }

            return sb.ToString();
        }
    }
}
