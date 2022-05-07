using System.Text.RegularExpressions;

namespace Caliban.Todo.DataAccess.Markdown.Objects
{
    public class Todo : Base
    {
        public Todo(string data)
            : base(data) { }

        public override string ToString()
        {
            return $"* {Data}";
        }

        public static bool Is(string line)
        {
            return Regex.IsMatch(line, @"^\*\s[^~]");
        }

        protected override string Parse(string line)
        {
            return Regex.Replace(line, @"^\*", "").Trim();
        }
    }
}
