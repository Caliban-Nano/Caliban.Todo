using System.IO;
using System.Threading.Tasks;
using Caliban.Todo.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Caliban.Todo.Tests.Data
{
    [TestClass]
    public sealed class TodoRepositoryTests
    {
        private const string Todofile = @".\Todo.md";
        
        [TestMethod]
        public void LoadTest()
        {
            var model = TodoRepository.Load(GetTestData());

            Assert.IsNotNull(model);
            Assert.AreEqual(model.Items.Count, 3);

            for (var i = 0; i < model.Items.Count; i++)
            {
                Assert.AreEqual(model.Items[i], $"Test {i + 1}");
            }
        }

        [TestMethod]
        public async Task SaveTest()
        {
            var model = new TodoModel();

            for (var i = 1; i < 4; i++)
            {
                model.Items.Add($"Test {i}");
            }

            await TodoRepository.SaveAsync(Todofile, model);

            var todo = await File.ReadAllLinesAsync(Todofile);
            var test = GetTestData();

            Assert.AreEqual(todo.Length, test.Length);

            for (var i = 0; i < todo.Length; i++)
            {
                Assert.AreEqual(todo[i], test[i]);
            }
        }

        private static string[] GetTestData()
        {
            return new string[]
            {
                "# TODO",
                "* Test 1",
                "* Test 2",
                "* Test 3"
            };
        }
    }
}
