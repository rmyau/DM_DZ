using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DZ_8
{
    class Program
    {
        
        static void Main(string[] args)
        {   
            string nameFile1="Graph1.txt";
            string nameFile2 = "Graph2.txt";
            string[] matr1;
            string[] matr2;
            matr1 = File.ReadAllLines(nameFile1);
            matr2 = File.ReadAllLines(nameFile2);

            bool Aftomorf = true;
            for (int i = 0; i < matr1.Length; i++)
                if (String.Compare(matr1[i], matr2[i]) != 0)
                {
                    Aftomorf = false;
                    break;
                }
            if (Aftomorf==false)
                Console.WriteLine("Преобразование не является автоморфизмом");
            else Console.WriteLine("Преобразование является автоморфизмом");
            Console.ReadKey();

        }
    }
}
