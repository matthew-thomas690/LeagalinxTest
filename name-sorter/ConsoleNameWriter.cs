using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace name_sorter
{
    public class ConsoleNameWriter : INameWriter
    {
        private TextWriter consoleTextWriter;
        public ConsoleNameWriter(TextWriter consoleTextWriter)
        {
            this.consoleTextWriter = consoleTextWriter;
        }

        public void Execute(IEnumerable<FullName> names)
        {
            foreach(var name in names)
            {
                consoleTextWriter.WriteLine(name.GetFullNameForDisplay());       
            }

            consoleTextWriter.Flush();
        }
    }
}
