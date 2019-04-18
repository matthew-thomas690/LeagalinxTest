using System;
using System.Collections.Generic;
using System.Text;

namespace name_sorter
{
    public interface INameDataParser
    {
        IEnumerable<FullName> Parse(IEnumerable<string> nameData);
    }
}
