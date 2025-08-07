using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot_11_module.Utilities
{
    public static class ProcessString
    {
        
            
        public static int GetLength(string symbolString)
        {
            if (symbolString is not null)
            {
                return symbolString.Length;
            }
            return 0;
        }

        public static int GetSum(string symbolString) 
        {
            int sum = 0;
            foreach (string s in symbolString.Split(" ")) {
                if (int.TryParse(s, out var parsedDigit))
                {
                    sum += parsedDigit;
                }
                else
                {
                    return 0;
                }
            }
            return sum;
        }
    }
}
