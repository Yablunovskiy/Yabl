using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Fight2_Yabl
{
    public class ABC
    {
        Hashtable Kod = new Hashtable();

        public ABC()
        {
            Kod['A'] = 1;
            Kod['B'] = 2;
            Kod['C'] = 3;
            Kod['D'] = 4;
            Kod['E'] = 5;
            Kod['F'] = 6;
            Kod['G'] = 7;
            Kod['H'] = 8;
            Kod['I'] = 9;
            Kod['J'] = 10;
        }

        public int GetKod(char c)
        {
            return (int)Kod[c];
        }
    }
}
