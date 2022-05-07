using System.IO;
using Caliban.Todo.DataAccess.Markdown;

namespace Caliban.Todo.Business
{
    public class Todo
    {
        private IEnumerable<IMarkdownObject>? _objects;

        public async Task LoadListAsync()
        {
            if (File.Exists(App.Todofile))
            {
                _objects = await Markdown.ImportAsync(App.Todofile);
            }
        }

        public async Task SaveListAsync()
        {
            if (_objects is not null)
            {
                await Markdown.ExportAsync(App.Todofile, _objects);
            }
        }
    }
}
