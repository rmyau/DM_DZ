﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DZ_1
{
    class Program
    {
        static bool hasNextSochet(List<int> s)
        {
            for (int i = s.Count - 1; i > 0; i--)
                if (s[i] != s[i - 1] + 1) return true;
            if (s[s.Count - 1] == alfSize - 1) return false;
            return true;
        }

        static void NextSochet(List<int> s)
        {
            int k = s.Count;
            if (s[k - 1] != (alfSize - 1)) s[k - 1] += 1;
            else
            {
                int index = k - 1;
                while (s[index] == s[index - 1] + 1 && index > 0)
                    index--;
                s[index - 1]++;
                
                for (int i = index; i < k; i++)
                    s[i] = s[i - 1] + 1;
                

            }

        }
        static bool hasNextArrangeRepeat(List<string> s)
        {
            for (int i = s.Count-1; i >= 0; i--)
                if (s[i] != alf[alf.Count - 1]) return true;
            return false;
        }
        static void NextArrangeRepeat(List<string> s)
        {
            int index = s.Count-1;
            while (s[index] == alf[alf.Count - 1] && index > 0)
            {
                s[index] = alf[1];
                index--;
            }
            int ind_in_alf = alf.IndexOf(s[index]);
            s[index] = alf[ind_in_alf + 1];


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

        public static void NextArrange(List<string> arr, List<string> ReArr, int L)
        {
            for (int i = 0; i < arr.Count; i++)
                arr[i] = ReArr[i];
            for (int i=0; i < L; i++)
                if (hasNextRearrangement(ReArr))
                    NextRearrangement(ReArr);
        }
        public static bool HasNextSochetRepeat(List<int> soch)
        {
            
            for (int i = soch.Count - 1; i >= 0; i--)
                if (soch[i] != alfSize - 1) return true;
            return false;

        }
        
        public static void NextSochetRepeat(List<int> sochet)
        {
            if (sochet[sochet.Count - 1] == alfSize - 1)
            {
                int index = sochet.Count - 1;
                while (sochet[index] == alfSize-1 && index > 0)
                    index--;
                if (index==0 ||sochet[index - 1] == alfSize - 1)
                    sochet[index]++;
                else sochet[index - 1]++;
                for (int i = index; i < sochet.Count; i++)
                    sochet[i] = sochet[index];
            }
            else sochet[sochet.Count - 1] ++;
        }
        public static bool HasNextSubset(List<int> sub)
        {
            if (hasNextSochet(sub)) return true;
            return false;
        }
        public static void NextSubset(List<int> sub)
        {
            NextSochet(sub);
        }
        public static void Print(List<string> slovo, StreamWriter file)
        {
            string s = "";
            for (int i = 0; i < slovo.Count; i++)
                s += slovo[i];
            file.WriteLine(s);
           // Console.WriteLine(s);

        }
        public static int Factorial(int x)
        {
            if (x == 1 || x==0) return 1;
            else return x * Factorial(x - 1);
        }
        static List<string> alf = new List<string>();
        static int alfSize;
       
        static void Main(string[] args) {
            alf.Add("a");
            alf.Add("b");
            alf.Add("c");
            alf.Add("d");
            alf.Add("e");
            alf.Add("f");
            alfSize = alf.Count;
            StreamWriter fileArrangeRepeat = new StreamWriter(@"ArrangeRepeat.txt");//для размещений с повторениями по к элементов
            StreamWriter filePerest = new StreamWriter(@"Perestanovki.txt");//для перестановок
            StreamWriter fileArrangeK = new StreamWriter(@"ArrangeK.txt");//для размещений по к элементов
            StreamWriter fileSubset = new StreamWriter(@"Subset.txt");//для подмножеств
            StreamWriter fileSochetK = new StreamWriter(@"SochetK.txt");//для сочетаний по к элементов
            StreamWriter fileSochetRep = new StreamWriter(@"SochetRepeat.txt");//для сочетаний с повторениями


            //построение всех размещений с повторениями по к элементов
            List<string> arrange = new List<string>();
            for (int i = 0; i < alfSize; i++)
            {
                arrange.Add("");
                for (int j = 0; j < arrange.Count; j++)
                    arrange[j] = alf[0];
                Print(arrange, fileArrangeRepeat);
                while (hasNextArrangeRepeat(arrange))
                {
                    NextArrangeRepeat(arrange);
                    Print(arrange, fileArrangeRepeat);
                }                

            }
            fileArrangeRepeat.Close();

            //построение всех перестановок
            List<string> perest = new List<string>();
            for (int i = 0; i < alfSize; i++)
                perest.Add(alf[i]);
            Print(perest, filePerest);
            while (hasNextRearrangement(perest))
            {
                NextRearrangement(perest);
                Print(perest, filePerest);
            }
            filePerest.Close();

            //построение всех размещений по к элементов
            arrange = new List<string>();
            for (int j = 0; j < alfSize; j++)
            {
                arrange.Add("");
                perest = new List<string>();
                for (int i = 0; i < alfSize; i++)
                    perest.Add(alf[i]);
                NextArrange(arrange, perest, Factorial(alfSize - arrange.Count));
                Print(arrange, fileArrangeK);
                while (hasNextRearrangement(perest))
                {
                    NextArrange(arrange, perest, Factorial(alfSize - arrange.Count));
                    Print(arrange, fileArrangeK);
                }

            }
            fileArrangeK.Close();

            //Построение всех подмножеств

            //Построение всех сочетаний по к элеметов
            List<int> sochet = new List<int>();
            sochet = new List<int>();
            List<string> sochWord = new List<string>();
            for (int i = 0; i < alfSize; i++)
            {
                sochet.Add(0);
                sochWord.Add("");
                for (int j = 0; j < sochet.Count; j++)
                { sochet[j] = j;
                    sochWord[j] = alf[j];
                }
                Print(sochWord, fileSochetK);
                Print(sochWord, fileSubset);
                while (hasNextSochet(sochet))
                {   
                    NextSochet(sochet);
                    for (int j = 0; j < sochet.Count; j++)
                        sochWord[j] = alf[sochet[j]];
                    Print(sochWord, fileSochetK);
                    Print(sochWord, fileSubset);
                }
            }

            fileSochetK.Close();
            fileSubset.Close();


            //Сочетания с повторениями
            sochet = new List<int>();
            sochWord = new List<string>();
            
            for (int j = 0; j < alfSize; j++)
            {
                sochet.Add(0);
                sochWord.Add("");

                for (int i = 0; i < sochet.Count; i++)
                {
                    sochet[i] = 0;
                    sochWord[i] = "";
                }
                for (int i = 0; i < sochWord.Count; i++)
                    sochWord[i] = alf[sochet[i]];

                Print(sochWord, fileSochetRep);
                while (HasNextSochetRepeat(sochet))
                {
                    NextSochetRepeat(sochet);
                    for (int i = 0; i < sochWord.Count; i++)
                    {
                        
                        sochWord[i] = alf[sochet[i]];
                        Console.Write(sochWord[i]);
                    }
                    Console.WriteLine();
                    Print(sochWord, fileSochetRep);
                }
                
            }

            fileSochetRep.Close();
            Console.ReadKey();
        }
    }
}
