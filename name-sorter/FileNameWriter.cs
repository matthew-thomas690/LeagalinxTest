using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace name_sorter
{
    public class FileNameWriter : INameWriter
    {
        private string filePath;
        public FileNameWriter(string filePath)
        {
            this.filePath = filePath;
        }

        public void Execute(IEnumerable<FullName> names)
        {
            using (var stream = File.Create(filePath))
            {
                using (TextWriter tw = new StreamWriter(stream))
                {
                    foreach(var name in names)
                    {
                        tw.WriteLine(name.GetFullNameForDisplay());
                    }
                }
            }
        }
    }
}
