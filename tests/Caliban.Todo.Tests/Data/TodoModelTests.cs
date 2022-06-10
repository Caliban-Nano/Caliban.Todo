using System;
using Caliban.Todo.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Caliban.Todo.Tests.Data
{
    [TestClass]
    public sealed class TodoModelTests
    {
        private readonly string[] Markdown = new[] {
            "# TODO",
            "* Test 1",
            "* Test 2",
            "* Test 3",
            ""
        };

        [TestMethod]
        public void MarkdownTest()
        {
            var model = new TodoModel
            {
                Header = "TODO"
            };

            model.Items.Add("Test 1");
            model.Items.Add("Test 2");
            model.Items.Add("Test 3");

            var markdown = string.Join(Environment.NewLine, Markdown);

            Assert.AreEqual(markdown, model.ToString());
        }
    }
}
