using System.IO;
using System.Threading.Tasks;
using Caliban.Todo.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Caliban.Todo.Tests.Data
{
    [TestClass]
    public class TodoRepositoryTests
    {
        private const string Todo = @".\Todo.md";
        private const string Test = @".\Test.md";

        [TestMethod]
        [DeploymentItem(@"Data\Todo.md")]
        public async Task LoadAsyncTest()
        {
            var model = await TodoRepository.LoadAsync(Todo);

            Assert.IsNotNull(model);
            Assert.AreEqual(model.Count, 3);

            for (var i = 0; i < model.Count; i++)
            {
                Assert.AreEqual(model[i], $"Test {i + 1}");
            }
        }

        [TestMethod]
        [DeploymentItem(@"Data\Todo.md")]
        public async Task SaveAsyncTest()
        {
            await TodoRepository.SaveAsync(Todo, new TodoModel
            {
                "Test 1",
                "Test 2",
                "Test 3"
            });

            var todo = await File.ReadAllLinesAsync(Todo);
            var test = await File.ReadAllLinesAsync(Test);

            Assert.AreEqual(todo.Length, test.Length);

            for (var i = 0; i < todo.Length; i++)
            {
                Assert.AreEqual(todo[i], test[i]);
            }
        }
    }
}
