using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handin2
{
    public class DisplaySimulator : IDisplay
    {
        public int test = 0;
        public void print(string tekst)
        {
            test = 1;
            Console.WriteLine(tekst);
        }
    }
}
