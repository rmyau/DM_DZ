using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DZ_3
{
    class Program
    {
        static int m1 = 5,m2=6, k = 2;
        static bool hasNextSochet(List<int> s,int m)
        {
            for (int i = s.Count - 1; i > 0; i--)
                if (s[i] != s[i - 1] + 1) return true;
            if (s[s.Count - 1] == m - 1) return false;
            return true;
        }

        static void NextSochet(List<int> s,int m)
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

        public static void NextArrange(List<string> arr, List<string> ReArr,int L)// L - разница между длиной перестановки и длиной и размещения
        {
            NextRearrangement(ReArr);
            for (int i = 0; i < arr.Count; i++)
                arr[i] = ReArr[i];
            for (int i=0;i<L;i++)
            if (hasNextRearrangement(ReArr))
                    NextRearrangement(ReArr);
        }
        public static void Connect(List<int> sochet, List<string> arrange, List<string> word, int indAlf,int m)
        {
            int indexAr = 0;
            for (int i = 0; i < m; i++)//обновили слово
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
        public static void ConnectForTwo(List<int> MainSochet, List<int> sochet2, List<string> arrange2, List<string> word, int m, int j, int n)
        {
            int indexAr = 0;
            for (int i = 0; i < m; i++)//обновили слово
                word[i] = "";

            for (int i = 0; i < k; i++)
                word[MainSochet[i]] = alf[j];

            int indexHelp = 0;//номер пустой позиции в слове
            for (int i = 0; i < m; i++)
                if (word[i] == "")
                {
                    if (sochet2.IndexOf(indexHelp) != -1)
                        word[i] = alf[n];
                    else
                    {
                        word[i] = arrange2[indexAr];
                        indexAr++;
                    }
                    indexHelp++;
                }
        }

        public static void Print(List<string> slovo, StreamWriter file,int m)
        {
            string s = "";
            for (int i = 0; i < m; i++)
                s += slovo[i];
            file.WriteLine(s);
            //Console.WriteLine(s);
        }
        public static List<string> alf = new List<string>();
        public static List<string> IterAlf1 = new List<string>();//для одной буквы повторяющейся
        public static List<string> IterAlf2 = new List<string>(); // для двух повторящихся букв
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
            List<string> perest2 = new List<string>();
            List<int> sochet = new List<int>();
            List<int> sochet2 = new List<int>();
            
            for (int i = 0; i < m1 - k; i++)
            {if (i < k)
                {
                    sochet.Add(0);
                    sochet2.Add(0);
                    arrange2.Add("");
                }
                    arrange1.Add("");
               
            }

            for (int i = 0; i < m1; i++)
            {
                word1.Add("");
                word2.Add("");
                perest.Add("");
            }
            word2.Add("");//тк длина второго слова = 6
            for (int i = 0; i < 4; i++)
                perest2.Add("");

            file1.WriteLine("Все слова длины 5 с повтором одной буквы 2 раза:");
            file2.WriteLine("Все слова длины 6, в которых две буквы повторяются два раза, остальные буквы не повторяются:");
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < k; i++)//обновили сочетания для новой буквы
                    sochet[i]=0;
                IterAlf1 = new List<string>();
                for (int i = 0; i < 6; i++)
                    if (i != j)
                        IterAlf1.Add(alf[i]);//убрали букву из алфавита

                while (hasNextSochet(sochet, m1))
                {
                    NextSochet(sochet, m1);

                    for (int i = 0; i < m1; i++)
                        perest[i] = IterAlf1[i];
                    NextArrange(arrange1, perest,1);
                    Connect(sochet, arrange1, word1, j,m1);
                    Print(word1, file1,m1);
                    while (hasNextRearrangement(perest))
                    {
                        NextArrange(arrange1, perest,1);
                        Connect(sochet, arrange1, word1, j,m1);
                        Print(word1, file1,m1);
                    }

                    //начало второго пункта

                    for (int n = j + 1; n < 6; n++)
                    {
                        for (int i = 0; i < k; i++)//обновили сочетания для новой буквы
                            sochet2[i] = 0;
                        IterAlf2 = new List<string>();
                        for (int i = 0; i < 6; i++)
                            if (i != j && i != n)
                                IterAlf2.Add(alf[i]);//убрали букву из алфавита 2

                        while (hasNextSochet(sochet2, 4))
                        {
                            NextSochet(sochet2, 4);

                            for (int i = 0; i < 4; i++)
                                perest2[i] = IterAlf2[i];
                            
                            NextArrange(arrange2, perest2,1);
                           
                            ConnectForTwo(sochet,sochet2, arrange2, word2, m2,j,n);
                            Print(word2, file2,m2);
                            while (hasNextRearrangement(perest2))
                            {
                                NextArrange(arrange2, perest2,1);
                                ConnectForTwo(sochet, sochet2, arrange2, word2, m2, j, n);
                                Print(word2, file2, m2);
                            }
                        }
                    }
                }
            }


            file1.Close();
            file2.Close();
            //Console.ReadKey();
        }
    }
}