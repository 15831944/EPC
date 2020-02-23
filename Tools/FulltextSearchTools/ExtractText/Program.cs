using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractText
{
    class Program
    {
        static void Main(string[] args)
        {
            Log4NetConfig.Configure();
            Formula.AppStart.Start();
            new Task().Convert();
        }
    }
}
