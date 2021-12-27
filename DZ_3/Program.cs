﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DZ_3
{
    class Program
    {
        static int m = 5, k = 2;
        static bool hasNextSochet(List<int> s)
        {
            for (int i = s.Count - 1; i > 0; i--)
                if (s[i] != s[i - 1] + 1) return true;
            if (s[s.Count - 1] == m - 1) return false;
            return true;
        }

        static void NextSochet(List<int> s)
        {
            if (s[k - 1] != (m - 1)) s[k - 1] += 1;
            else
            {
                int index = k - 1;
                while (s[index] == s[index - 1] + 1 && index > 0)
                    index--;
                s[index - 1]++;
                for (int i = index + 1; i < k; i++)
                    s[i] = s[i - 1] + 1;
            }

        }
        public static void NextRearrangement(List<string> ReArr)//perestnovka
        {
            int index = ReArr.Count - 2;
            while (alf.IndexOf(ReArr[index]) > alf.IndexOf(ReArr[index + 1]))
                index--;
            int s = index + 1;
            while (s < ReArr.Count - 1 && (alf.IndexOf(ReArr[s + 1]) > alf.IndexOf(ReArr[index])))
                s++;
            string t = ReArr[index];//поменяли местами полученный и наименьший справа
            ReArr[index] = ReArr[s];
            ReArr[s] = t;

            for (int i = 0; i < (ReArr.Count - index - 1) / 2; i++)
            {
                t = ReArr[ReArr.Count - 1 - i];
                ReArr[ReArr.Count - 1 - i] = ReArr[index + i + 1];
                ReArr[index + i + 1] = t;
            }

        }
        public static bool hasNextRearrangement(List<string> ReArr)
        {
            int index = ReArr.Count - 1;
            while (index > 0 && (alf.IndexOf(ReArr[index]) < alf.IndexOf(ReArr[index - 1])))
                index--;
            if (index == 0) return false;
            else return true;
        }

        public static void NextArrange(List<string> arr, List<string> ReArr)
        {
            NextRearrangement(ReArr);
            for (int i = 0; i < m - k; i++)
                arr[i] = ReArr[i];
            if (hasNextRearrangement(ReArr))
                NextRearrangement(ReArr);
        }
        //public static void NewAlf(List<string> alf, List<string> ChangeAlf, int indAlf)
        //{ ChangeAlf = new List<string>();
        //    for (int i = 0; i < 6; i++)
        //        if (i != indAlf)
        //            ChangeAlf.Add(alf[i]);
        //}
        public static void Connect(List<int> sochet, List<string> arrange, List<string> word, int indAlf)
        {
            int indexAr = 0;
            for (int i = 0; i < m; i++)
                word[i] = "";

            for (int i = 0; i < k; i++)
                word[sochet[i]] = alf[indAlf];
            for (int i = 0; i < m; i++)
                if (word[i] == "")
                {
                    word[i] = arrange[indexAr];
                    indexAr++;
                }
        }

        public static void Print(List<string> slovo, StreamWriter file)
        {
            string s = "";
            for (int i = 0; i < m; i++)
                s += slovo[i];
            file.WriteLine(s);
            //Console.WriteLine(s);
        }
        public static List<string> alf = new List<string>();
        public static List<string> IterAlf = new List<string>();
        public static List<string> word1 = new List<string>();
        public static List<string> word2 = new List<string>();
        public static StreamWriter file1 = new StreamWriter(@"Dz2_1.txt");//для размещений с повторениями
        public static StreamWriter file2 = new StreamWriter(@"Dz2_2.txt");
        static void Main(string[] args)
        {
            alf.Add("a");
            alf.Add("b");
            alf.Add("c");
            alf.Add("d");
            alf.Add("e");
            alf.Add("f");
            
            List<string> arrange1 = new List<string>();
            List<string> arrange2 = new List<string>();
            List<string> perest = new List<string>();
            List<int> sochet = new List<int>();
            for (int i = 0; i < m - k; i++)
            {if (i < k)
                    sochet.Add(0);
                arrange1.Add("");
                arrange2.Add("");
            }

            for (int i = 0; i < m; i++)
            {
                word1.Add("");
                word2.Add("");
                perest.Add("");
            }
            file1.WriteLine("Все слова длины 5 с повтором одной буквы 2 раза:");
            file2.WriteLine("Все слова длины 6, в которых две буквы повторяются два раза, остальные буквы не повторяются:");
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < k; i++)//обновили сочетания для новой буквы
                    sochet[i]=0;
                IterAlf = new List<string>();
                for (int i = 0; i < 6; i++)
                    if (i != j)
                        IterAlf.Add(alf[i]);//убрали букву из алфавита

                while (hasNextSochet(sochet))
                {
                    NextSochet(sochet);

                    for (int i = 0; i < m; i++)
                        perest[i] = IterAlf[i];
                    NextArrange(arrange2, perest);
                    Connect(sochet, arrange2, word2,j);
                    Print(word2, file1);
                    while (hasNextRearrangement(perest))
                    {
                        NextArrange(arrange2, perest);
                        Connect(sochet, arrange2, word2,j);
                        Print(word2, file1);
                    }
                }
            }

            file1.Close();
            file2.Close();
            //Console.ReadKey();
        }
    }
}