namespace Caliban.Todo.DataAccess.Markdown.Objects
{
    public abstract class Base : IMarkdownObject
    {
        public Base(string data)
        {
            Data = Parse(data);
        }

        protected abstract string Parse(string line);
    }
}
