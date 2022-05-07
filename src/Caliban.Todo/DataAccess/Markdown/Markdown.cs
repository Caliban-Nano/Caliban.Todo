using System.IO;
using System.Text;
using Caliban.Todo.DataAccess.Markdown.Objects;

namespace Caliban.Todo.DataAccess.Markdown
{
    /// <summary>
    /// Using markdown for todo list persistence.
    /// </summary>
    public static class Markdown
    {
        public static async Task<IEnumerable<IMarkdownObject>> ImportAsync(string path)
        {
            var objects = new List<IMarkdownObject>();

            var lines = await File.ReadAllLinesAsync(path, Encoding.UTF8);

            foreach (var line in lines)
            {
                if (Title.Is(line))
                {
                    objects.Add(new Title(line));
                }
                else if (Objects.Todo.Is(line))
                {
                    objects.Add(new Objects.Todo(line));
                }
                else if (Done.Is(line))
                {
                    objects.Add(new Done(line));
                }
            }

            return objects;
        }

        public static async Task ExportAsync(string path, IEnumerable<IMarkdownObject> objects)
        {
            var lines = objects.Select(x => x.ToString() ?? "");

            await File.WriteAllLinesAsync(path, lines, Encoding.UTF8);
        }
    }
}
