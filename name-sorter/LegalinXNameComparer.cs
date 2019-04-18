using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace name_sorter
{
    public class LegalinXNameComparer : IComparer<FullName>
    {
        public int Compare(FullName x, FullName y)
        {
            var comparisonResult = x.Surname.CompareTo(y.Surname);

            if (comparisonResult == 0)
            {
                for (int i = 1; i <= 3; i++)
                {
                    var myGivenName = string.Empty;
                    var otherGivenName = string.Empty;

                    if (x.GivenNames.ContainsKey(i))
                    {
                        myGivenName = x.GivenNames[i];
                    }

                    if (y.GivenNames.ContainsKey(i))
                    {
                        otherGivenName = y.GivenNames[i];
                    }

                    comparisonResult = myGivenName.CompareTo(otherGivenName);

                    if (comparisonResult != 0)
                    {
                        break;
                    }
                }
            }

            return comparisonResult;
        }
    }
}
