using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace name_sorter
{
    public class LegalinXNameDataParser : INameDataParser
    {
        private ILogger logger;
        public LegalinXNameDataParser(ILogger<LegalinXNameDataParser> logger)
        {
            this.logger = logger;
        }

        public IEnumerable<FullName> Parse(IEnumerable<string> nameData)
        {
            var splitChars = new char[] { ' ' };
            var result = new List<FullName>();
            string[] splitLine = null;

            foreach (var line in nameData)
            {
                try
                {
                    splitLine = line.Split(splitChars);

                    if (splitLine.Length < 2)
                    {
                        throw new Exception("line must have at least one given name");
                    }

                    if (splitLine.Length > 4)
                    {
                        throw new Exception("line can only have at most three given names");
                    }

                    var givenNames = new Dictionary<int, string>();

                    for (int i = 0; i < splitLine.Length - 1; i++)
                    {
                        givenNames.Add(i + 1, splitLine[i]);
                    }

                    result.Add(new FullName(splitLine.Last(), givenNames));
                }
                catch(Exception ex)
                {
                    logger.LogError(ex, ex.Message, splitLine);
                    throw;
                }
            }

            return result;
        }
    }
}
