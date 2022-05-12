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
        /// (Awaitable) Loads the todo model from a Markdown file.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="throwOnError">If an exception should be thrown.</param>
        /// <returns>The filled todo model.</returns>
        public static async Task<TodoModel> LoadAsync(string path, bool throwOnError = true)
        {
            var model = new TodoModel();

            try
            {
                var lines = await File.ReadAllLinesAsync(path, Encoding.UTF8);

                var header = lines.First(x => Regex.IsMatch(x, @"^\#"));

                model.Header = Regex.Replace(header, @"^\#\s", "");

                foreach (var line in lines.Where(x => Regex.IsMatch(x, @"^\*")))
                {
                    model.Items.Add(Regex.Replace(line, @"^\*\s", ""));
                }
            }
            catch (Exception)
            {
                if (throwOnError) throw;
            }

            return model;
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
