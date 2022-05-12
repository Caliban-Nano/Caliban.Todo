using System.IO;
using System.Text;
using System.Threading.Tasks;
using Caliban.Todo.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Caliban.Todo.Tests.Data
{
    [TestClass]
    public sealed class TodoRepositoryTests
    {
        private const string Todo = @".\Todo.md";
        private const string Test = @".\Test.md";
        
        [TestInitialize]
        public void Setup()
        {
            File.WriteAllText(Test, GetTestContent());
        }

        [TestMethod]
        public async Task LoadAsyncTest()
        {
            var model = await TodoRepository.LoadAsync(Todo);

            Assert.IsNotNull(model);
            Assert.AreEqual(model.Items.Count, 3);

            for (var i = 0; i < model.Items.Count; i++)
            {
                Assert.AreEqual(model.Items[i], $"Test {i + 1}");
            }
        }

        [TestMethod]
        public async Task SaveAsyncTest()
        {
            var model = new TodoModel();

            for (var i = 1; i < 4; i++)
            {
                model.Items.Add($"Test {i}");
            }

            await TodoRepository.SaveAsync(Todo, model);

            var todo = await File.ReadAllLinesAsync(Todo);
            var test = await File.ReadAllLinesAsync(Test);

            Assert.AreEqual(todo.Length, test.Length);

            for (var i = 0; i < todo.Length; i++)
            {
                Assert.AreEqual(todo[i], test[i]);
            }
        }

        private string GetTestContent()
        {
            var test = new StringBuilder();

            test.AppendLine("# TODO");

            for (var i = 1; i < 4; i++)
            {
                test.AppendLine($"* Test {i}");
            }

            return test.ToString();
        }
    }
}
