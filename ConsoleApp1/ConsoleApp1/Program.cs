using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            HashMap<string, string> hashMap = new HashMap<string, string>();

            hashMap.Add("sup", "a");
            hashMap.Add("hi", "b");

            hashMap.Remove("hi");            

            hashMap.Clear();
        }
    }
}
