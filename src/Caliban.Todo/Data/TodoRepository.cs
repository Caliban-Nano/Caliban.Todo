using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Caliban.Todo.Data
{
    /// <summary>
    /// An overly simplistic Markdown parser for persistence.
    /// </summary>
    public static class TodoRepository
    {
        public static async Task<TodoModel> LoadAsync(string path, bool throwOnError = true)
        {
            var model = new TodoModel();

            try
            {
                var lines = await File.ReadAllLinesAsync(path, Encoding.UTF8);

                var title = lines.First(x => Regex.IsMatch(x, @"^\#"));

                model.Title = Regex.Replace(title, @"^\#\s", "");

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

        public static async Task SaveAsync(string path, TodoModel model)
        {
            await File.WriteAllTextAsync(path, model.ToString(), Encoding.UTF8);
        }
    }
}
