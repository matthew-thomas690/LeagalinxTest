using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace name_sorter.Tests
{
    [TestClass]
    public class NameSorterTests
    {

        [TestMethod]
        public void LegalinXNameComparerTest()
        {
            var Name1 = new FullName("A", new Dictionary<int, string>() { { 1, "B" } });
            var Name2 = new FullName("A", new Dictionary<int, string>() { { 1, "B" }, { 2, "C" } });
            var Name3 = new FullName("A", new Dictionary<int, string>() { { 1, "B" }, { 2, "C" }, { 3, "D" } });
            var Name4 = new FullName("A", new Dictionary<int, string>() { { 1, "B" }, { 2, "C" }, { 3, "D" } });
            var Name5 = new FullName("W", new Dictionary<int, string>() { { 1, "X" }, { 2, "Y" }, { 3, "Z" } });

            IComparer<FullName> comparer = new LegalinXNameComparer();

            Assert.AreEqual<int>(comparer.Compare(Name1, Name2), -1);
            Assert.AreEqual<int>(comparer.Compare(Name1, Name3), -1);
            Assert.AreEqual<int>(comparer.Compare(Name3, Name4), 0);
            Assert.AreEqual<int>(comparer.Compare(Name3, Name5), -1);
            Assert.AreEqual<int>(comparer.Compare(Name5, Name3), 1);
        }

        [TestMethod]
        public void LegalinXNameDataParserTest()
        {
            List<string> testData = new List<string>();
            testData.Add("AA BB CC DD");
            testData.Add("AA CC DD");
            testData.Add("AA DD");

            INameDataParser dataParser = new LegalinXNameDataParser(new MockLogger());
            var result = dataParser.Parse(testData);
            Assert.AreEqual<int>(result.Count(), 3);
            Assert.IsTrue(result.Select(x => x.GetFullNameForDisplay()).Contains("AA BB CC DD"));
            Assert.IsTrue(result.Select(x => x.GetFullNameForDisplay()).Contains("AA CC DD"));
            Assert.IsTrue(result.Select(x => x.GetFullNameForDisplay()).Contains("AA DD"));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "line must have at least one given name")]
        public void LegalinXNameDataParserNoGivenNameTest()
        {
            List<string> testData = new List<string>();
            testData.Add("DD");

            INameDataParser dataParser = new LegalinXNameDataParser(new MockLogger());
            var result = dataParser.Parse(testData);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "line can only have at most three given names")]
        public void LegalinXNameDataParserToManyGivenNamesTest()
        {
            List<string> testData = new List<string>();
            testData.Add("AA BB CC DD EE");

            INameDataParser dataParser = new LegalinXNameDataParser(new MockLogger());
            var result = dataParser.Parse(testData);
        }

        private class MockLogger : ILogger<LegalinXNameDataParser>
        {
            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return false;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {

            }
        }
    }


}
