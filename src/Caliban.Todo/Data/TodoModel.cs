using System.Collections.ObjectModel;
using System.Text;

namespace Caliban.Todo.Data
{
    public class TodoModel : ObservableCollection<string>
    {
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("# TODO");

            foreach (var item in this)
            {
                sb.AppendLine($"* {item}");
            }

            return sb.ToString();
        }
    }
}
