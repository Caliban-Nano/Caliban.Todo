using System.IO;
using System.Threading.Tasks;
using Caliban.Todo.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Caliban.Todo.Tests.Data
{
    [TestClass]
    public sealed class TodoModelTests
    {
        private const string Testfile = @".\Test.md";
        private readonly string[] Testdata = new[] {
            "# TODO",
            "* Test 1",
            "* Test 2",
            "* Test 3"
        };

        [TestMethod]
        public async Task RequestTest()
        {
            var model = new TodoModel();

            await model.Request(Testfile);

            Assert.AreEqual(model.Items.Count, 3);

            for (var i = 0; i < model.Items.Count; i++)
            {
                Assert.AreEqual(model.Items[i], $"Test {i + 1}");
            }
        }

        [TestMethod]
        public async Task PersistTest()
        {
            var model = new TodoModel();

            for (var i = 1; i < 4; i++)
            {
                model.Items.Add($"Test {i}");
            }

            await model.Persist(Testfile);

            var lines = await File.ReadAllLinesAsync(Testfile);

            Assert.AreEqual(lines.Length, Testdata.Length);

            for (var i = 0; i < lines.Length; i++)
            {
                Assert.AreEqual(lines[i], Testdata[i]);
            }
        }
    }
}
