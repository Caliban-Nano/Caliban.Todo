using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Caliban.Todo.Models;

namespace Caliban.Todo.Parser
{
    /// <summary>
    /// Using markdown for todo list persistence.
    /// </summary>
    public static class Markdown
    {
        public static TodoList Import(string path)
        {
            var todo = new TodoList();

            foreach (var line in File.ReadLines(path, Encoding.UTF8))
            {
                if (Regex.IsMatch(line, @"^#"))
                {
                    todo.Add((TodoType.Title, Regex.Replace(line, @"^#", "").Trim()));
                }

                if (Regex.IsMatch(line, @"^\*\s[^~]"))
                {
                    todo.Add((TodoType.Todo, Regex.Replace(line, @"^\*", "").Trim()));
                }

                if (Regex.IsMatch(line, @"^\*\s~"))
                {
                    todo.Add((TodoType.Done, Regex.Replace(line, @"^\*\s~", "").Trim('~')));
                }
            }

            return todo;
        }

        public static void Export(string path, TodoList todo)
        {
            var lines = new List<string>();

            foreach (var item in todo)
            {
                lines.Add(item.Item1 switch
                {
                    TodoType.Title => $"# {item.Item2}",
                    TodoType.Todo  => $"* {item.Item2}",
                    TodoType.Done  => $"* ~{item.Item2}~",
                    _              => item.Item2 
                });
            }

            File.WriteAllLines(path, lines, Encoding.UTF8);
        }
    }
}
