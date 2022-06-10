using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Caliban.Nano.Data;

namespace Caliban.Todo.Data
{
    /// <summary>
    /// Todo list model repository.
    /// </summary>
    public sealed class TodoModel : Model.Repository
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
        /// Request the todo model from a markdown file.
        /// </summary>
        /// <param name="key">The filename.</param>
        /// <returns>An asynchronous task.</returns>
        public override async Task Request(object? key = null)
        {
            ArgumentNullException.ThrowIfNull(key, nameof(key));

            var lines = await File.ReadAllLinesAsync((string)key, Encoding.UTF8);

            Header = lines.First(x => Regex.IsMatch(x, @"^\#"));
            Header = Regex.Replace(Header, @"^\#\s", "");

            foreach (var line in lines.Where(x => Regex.IsMatch(x, @"^\*")))
            {
                Items.Add(Regex.Replace(line, @"^\*\s", ""));
            }

            await base.Request(key);
        }

        /// <summary>
        /// Persists the todo model into a markdown file.
        /// </summary>
        /// <param name="key">The filename.</param>
        /// <returns>An asynchronous task.</returns>
        public override async Task Persist(object? key = null)
        {
            ArgumentNullException.ThrowIfNull(key, nameof(key));

            await File.WriteAllTextAsync((string)key, ToString(), Encoding.UTF8);

            await base.Persist(key);
        }

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
