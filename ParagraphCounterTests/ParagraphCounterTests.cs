using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using ParagraphCount;

namespace ParagraphCounterTests
{
    [TestClass]
    public class ParagraphCounterTests
    {
        ParagraphCounter _counter;
        [TestInitialize]
        public void Setup()
        {
            _counter = new ParagraphCounter();
        }

        [TestMethod]
        public void DefaultSeparators_ReturnsExpectedResult()
        {
            var input = "Hello! World? This, a string, is a \"Hello World\" string.";
            var result = _counter.GetWordCounts(input);
            Assert.IsTrue(result.TryGetValue("world", out int count) && count == 2);
            Assert.IsTrue(result.TryGetValue("string", out count) && count == 2);
        }

        [TestMethod]
        public void CustomSeparators_ReturnsExpectedResult()
        {
            var input = "Hello! World? This, a string, is a \"Hello World\" string.";
            var separators = " !?.";
            var result = _counter.GetWordCounts(input, separators);

            //Naive separator definition results in common punctuation messing up the result
            Assert.IsFalse(result.TryGetValue("world", out int count) && count == 2);
            Assert.IsTrue(result.TryGetValue("world", out count) && count == 1);
            Assert.IsTrue(result.TryGetValue("world\"", out count) && count == 1);

            Assert.IsFalse(result.TryGetValue("string", out count) && count == 2);
            Assert.IsTrue(result.TryGetValue("string,", out count) && count == 1);
            Assert.IsTrue(result.TryGetValue("string", out count) && count == 1);            
        }


        [TestMethod]
        public void MessySeparators_ReturnsUnexpectedResult()
        {
            var input = "Hello! World? This, a string, is a \"Hello World\" string.";
            var separators = "os!?.";
            var result = _counter.GetWordCounts(input, separators);
            
            //Separator string with typos (and notably, no space char) results in unintelligible output
            Assert.IsTrue(result.TryGetValue("hell", out int count) && count == 1);
            Assert.IsTrue(result.TryGetValue(" w", out count) && count == 2);
            Assert.IsTrue(result.TryGetValue("tring, i", out count) && count == 1);
        }
    }
}
