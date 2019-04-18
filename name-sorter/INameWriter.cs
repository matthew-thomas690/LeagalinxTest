using System;
using System.Collections.Generic;
using System.Text;

namespace name_sorter
{
    public interface INameWriter
    {
        void Execute(IEnumerable<FullName> names);
    }
}
