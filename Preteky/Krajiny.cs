using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preteky
{
  public  class Krajiny
    {
        public string[] krajiny { get; set; }

        public Krajiny()
        {
            krajiny = new string[257];
            using (StreamReader sr = new StreamReader("krajiny.txt"))
            {
                int i = 0;
                while (sr.Peek() >= 0)
                {
                    krajiny[i] = sr.ReadLine();
                    i++;
                }
            }
        }
    }
}
