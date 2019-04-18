using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace name_sorter
{
    public class FullName 
    {
        public FullName(string surname, IReadOnlyDictionary<int, string> givenNames)
        {
            GivenNames = givenNames;
            Surname = surname;
        }

        public IReadOnlyDictionary<int, string> GivenNames { get; private set; }
        public string Surname { get; private set; }

        public string GetFullNameForDisplay()
        {
            string displayname = "";

            for (int i = 1; i <= 3; i++)
            {
                if (!GivenNames.ContainsKey(i))
                {
                    break;
                }

                displayname += GivenNames[i] + " ";
            }

            displayname += Surname;

            return displayname;
        }
    }
}
