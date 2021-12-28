using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DZ_4
{
    class Program
    {
        static int m1 = 7, m2 = 9, k1 = 2,k2=3;
        static bool hasNextSochet(List<int> s, int m)//m-размерность слова
        {
            for (int i = s.Count - 1; i > 0; i--)
                if (s[i] != s[i - 1] + 1) return true;
            if (s[s.Count - 1] == m - 1) return false;
            return true;
        }

        static void NextSochet(List<int> s, int m,int k) //к-размерность сочетания
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

        public static void NextArrange(List<string> arr, List<string> ReArr, int L)// L - разница между длиной перестановки и длиной и размещения
        {
            
            for (int i = 0; i < arr.Count; i++)
                arr[i] = ReArr[i];
            for (int i = 0; i < L; i++)
                if (hasNextRearrangement(ReArr))
                    NextRearrangement(ReArr);
        }
        
        public static void Connect(List<int> sochet, List<string> word, int j, int m)
        {
            int Index = 0;
            for (int i = 0; i < m; i++)
                if (word[i] == "")
                {
                    if (sochet.IndexOf(Index) != -1)
                        word[i] = alf[j];
                    Index++;
                }
        }
        public static void ConnectArr(List<string> arrange, List<string> word, int m)
        { int indexAr = 0;
            for (int i = 0; i < m; i++)
                if (word[i] == "")
                {
                    word[i] = arrange[indexAr];
                    indexAr++;
                }
        }

        public static void Print(List<string> slovo, StreamWriter file, int m)
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
        public static StreamWriter file1 = new StreamWriter(@"Dz4_1.txt");//для размещений с повторениями
        public static StreamWriter file2 = new StreamWriter(@"Dz4_2.txt");
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
            List<string> perest1 = new List<string>();
            List<string> perest2 = new List<string>();
            List<int> sochet = new List<int>();
            List<int> sochet1 = new List<int>();

            List<int> sochet2 = new List<int>();
            List<int> sochet21 = new List<int>();
            List<int> sochet22 = new List<int>();

            for (int i = 0; i < m1 - k1-k2; i++)
            {
                arrange1.Add("");
            }
            for (int i = 0; i < k1; i++)
            {
                sochet.Add(0);
                sochet2.Add(0);
                sochet21.Add(0);
                arrange2.Add("");
            }
            for (int i = 0; i < k2; i++)
            {
                sochet1.Add(0);
                sochet22.Add(0);
            }

            for (int i = 0; i < m1; i++)
                word1.Add("");
            for (int i = 0; i < m2; i++)
                word2.Add("");
            
            for (int i = 0; i < 4; i++)
                perest1.Add("");
            for (int i = 0; i < 3; i++)
                perest2.Add("");

            file1.WriteLine("Все слова длины 7,в которых ровно одна буква повторяется 2 раза, ровно одна буква повторяется 3 раза:");
            file2.WriteLine("Все слова длины 9,в которых ровно две буквы повторяются 2 раза, ровно одна буква повторяется 3 раза:");
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < k1; i++)//обновили сочетания для новой буквы
                {
                    sochet[i] = 0;
                    sochet2[i] = 0;
                }
                
                IterAlf1 = new List<string>();
                while (hasNextSochet(sochet, m1))
                {
                    NextSochet(sochet, m1,k1);

                    for (int n = 0; n < 6; n++)
                        if (n != j)
                        {
                            for (int i = 0; i < k2; i++)//обновили сочетания для новой буквы
                                sochet1[i] = i;
                            sochet1[k2 - 1] = k2 - 2;
                            for (int i = 0; i < 6; i++)
                                if (i != j && i != n)
                                    IterAlf1.Add(alf[i]);//убрали букву из алфавита 2

                            while (hasNextSochet(sochet1, 5))
                            {
                                NextSochet(sochet1, 5, k2);

                                for (int i = 0; i < 4; i++)
                                    perest1[i] = IterAlf1[i];

                                NextArrange(arrange1, perest1, 2);
                                for (int i = 0; i < m1; i++)
                                    word1[i] = "";
                                Connect(sochet, word1, j, m1);
                                Connect(sochet1, word1, n, m1);
                                ConnectArr(arrange1, word1, m1);
                                Print(word1, file1, m1);
                                while (hasNextRearrangement(perest1))
                                {
                                    NextArrange(arrange1, perest1, 2);
                                    for (int i = 0; i < m1; i++)
                                        word1[i] = "";
                                    Connect(sochet, word1, j, m1);
                                    Connect(sochet1, word1, n, m1);
                                    ConnectArr(arrange1, word1, m1);
                                    Print(word1, file1, m1);
                                }

                            }
                        }      
                }

                //второй пункт
                while (hasNextSochet(sochet2, m2))
                {
                    NextSochet(sochet2, m2,k1);

                    for (int n = j + 1; n < 6; n++)
                    {
                        for (int i = 0; i < k1; i++)//обновили сочетания для новой буквы
                            sochet21[i] = 0;
                        while (hasNextSochet(sochet21, 7))
                        {
                            NextSochet(sochet21, 7, k1);

                            for (int p = 0; p < 6; p++)
                                if (p != j && p != n)
                                {
                                    for (int i = 0; i < k2; i++)//обновили сочетания для новой буквы
                                        sochet22[i] = i;
                                    sochet22[k2 - 1] = k2 - 2;

                                    IterAlf2 = new List<string>();
                                    for (int i = 0; i < 6; i++)
                                        if (i != j && i != n && i != p)
                                            IterAlf2.Add(alf[i]);//убрали буквы из алфавита 

                                    while (hasNextSochet(sochet22, 5))
                                    {
                                        NextSochet(sochet22, 5,k2);
                                        for (int i = 0; i < 3; i++)
                                            perest2[i] = IterAlf2[i];
                                        
                                        NextArrange(arrange2, perest2, 1);
                                        for (int i = 0; i < m2; i++)
                                            word2[i] = "";
                                        Connect(sochet2, word2, j, m2);
                                        Connect(sochet21, word2, n, m2);
                                        Connect(sochet22, word2, p, m2);
                                        ConnectArr(arrange2, word2, m2);
                                        Print(word2, file2, m2);
                                        while (hasNextRearrangement(perest2))
                                        {
                                           
                                            NextArrange(arrange2, perest2,1);
                                            
                                            for (int i = 0; i < m2; i++)
                                                word2[i] = "";
                                            Connect(sochet2, word2, j, m2);
                                            Connect(sochet21, word2, n, m2);
                                            Connect(sochet22, word2, p, m2);
                                            ConnectArr(arrange2, word2, m2);
                                            Print(word2, file2, m2);
                                        }
                                    }
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
