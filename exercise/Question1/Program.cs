using System;
using System.Text.RegularExpressions;

namespace exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Question1(9527));
            Console.WriteLine(Question1(3345678));
            Console.WriteLine(Question1(-1234.45));
            Console.ReadKey();
        }


        static string Question1(double inString){
           return Regex.Replace(inString.ToString(),@"(\d)(?=(\d{3})+(?!\d))","$1,");
        }


    }
}


// 正規劃 , 建立索引


// 架構面解