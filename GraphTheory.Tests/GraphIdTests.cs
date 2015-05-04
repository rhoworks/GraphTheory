using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphTheory.Tests
{
    [TestFixture]
    public class GraphIdTests
    {
        [Test]
        public void New_Just_Works()
        {
            dynamic g = new object(); // todo: use GraphId object
            Assert.That(g, Is.Not.Null);
        }
    }
}
