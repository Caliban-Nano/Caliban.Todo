using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Caliban.Todo.Data
{
    /// <summary>
    /// An overly simplistic Markdown repository.
    /// </summary>
    public static class TodoRepository
    {
        /// <summary>
        /// Loads the todo model from Markdown data.
        /// </summary>
        /// <param name="lines">The data lines.</param>
        /// <returns>The filled todo model.</returns>
        public static TodoModel Load(string[] data)
        {
            var model = new TodoModel();

            var header = data.First(x => Regex.IsMatch(x, @"^\#"));

            model.Header = Regex.Replace(header, @"^\#\s", "");

            foreach (var line in data.Where(x => Regex.IsMatch(x, @"^\*")))
            {
                model.Items.Add(Regex.Replace(line, @"^\*\s", ""));
            }

            return model;
        }

        /// <summary>
        /// (Awaitable) Loads the todo model from a Markdown file.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="throwOnError">If an exception should be thrown.</param>
        /// <returns>The filled todo model.</returns>
        public static async Task<TodoModel> LoadAsync(string path, bool throwOnError = true)
        {
            try
            {
                return Load(await File.ReadAllLinesAsync(path, Encoding.UTF8));
            }
            catch (Exception)
            {
                if (throwOnError) throw;
            }

            return new TodoModel(); // Safe default
        }

        /// <summary>
        /// (Awaitable) Saves the todo model to a Markdown file.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="model">The todo model.</param>
        public static async Task SaveAsync(string path, TodoModel model)
        {
            await File.WriteAllTextAsync(path, model.ToString(), Encoding.UTF8);
        }
    }
}
