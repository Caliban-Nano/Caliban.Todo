using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Caliban.Todo.Logic;

namespace Caliban.Todo.Data
{
    /// <summary>
    /// An overly simplistic Markdown parser for persistence.
    /// </summary>
    public static class Markdown
    {
        public static async Task<NoteModel> ImportAsync(string path)
        {
            var model = new NoteModel();

            if (File.Exists(path))
            {
                var lines = await File.ReadAllLinesAsync(path, Encoding.UTF8);

                foreach (var line in lines.Where(x => Regex.IsMatch(x, @"^\*")))
                {
                    model.Add(Regex.Replace(line, @"^\*\s", ""));
                }

            }

            return model;
        }

        public static async Task ExportAsync(string path, NoteModel model)
        {
            var lines = new List<string>() { "# TODO" };
            
            lines.AddRange(model.Select(item => $"* {item}"));

            await File.WriteAllLinesAsync(path, lines, Encoding.UTF8);
        }
    }
}
