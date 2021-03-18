using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook_v2._2
{
    class TimeUtilClass
    {
        public static void PrintTime()
        {
            Console.WriteLine(DateTime.Now.ToShortTimeString());
        }
        public static void PrintDate()
        {
            Console.WriteLine(DateTime.Today.ToShortDateString());
        }
    }
}
